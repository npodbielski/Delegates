// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DelegateFactory.cs" company="Natan Podbielski">
//   Copyright (c) 2016 - 2016 Natan Podbielski. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Delegates.CustomDelegates;
using Delegates.Extensions;

namespace Delegates
{
    public static class DelegateFactory
    {
        private const string AddAccessor = "add";
        private const string Item = "Item";

        private const string RemoveAccessor = "remove";

        private static readonly MethodInfo EventHandlerFactoryMethodInfo =
            typeof(DelegateFactory).GetMethod("EventHandlerFactory");

        private static readonly Dictionary<WeakReference<object>, WeakReference<object>> EventsProxies =
            new Dictionary<WeakReference<object>, WeakReference<object>>();

        public static TDelegate Contructor<TDelegate>() where TDelegate : class
        {
            var source = GetFuncDelegateReturnType<TDelegate>();
            var ctrArgs = GetFuncDelegateArguments<TDelegate>();
            var constructorInfo = GetConstructorInfo(source, ctrArgs);
            if (constructorInfo == null)
            {
                return null;
            }
            var parameters = ctrArgs.Select(Expression.Parameter).ToList();
            return Expression.Lambda(Expression.New(constructorInfo, parameters), parameters).Compile() as TDelegate;
        }

        public static Func<object[], object> Contructor(this Type source, params Type[] ctrArgs)
        {
            var constructorInfo = GetConstructorInfo(source, ctrArgs);
            if (constructorInfo == null)
            {
                return null;
            }
            var argsArray = Expression.Parameter(typeof(object[]));
            var paramsExpression = new Expression[ctrArgs.Length];
            for (var i = 0; i < ctrArgs.Length; i++)
            {
                var argType = ctrArgs[i];
                paramsExpression[i] =
                    Expression.Convert(Expression.ArrayIndex(argsArray, Expression.Constant(i)), argType);
            }
            Expression returnExpression = Expression.New(constructorInfo, paramsExpression);
            if (!source.IsClass)
            {
                returnExpression = Expression.Convert(returnExpression, typeof(object));
            }
            return (Func<object[], object>)Expression.Lambda(returnExpression, argsArray).Compile();
        }

        public static TDelegate Contructor<TDelegate>(this Type source)
            where TDelegate : class
        {
            var ctrArgs = GetFuncDelegateArguments<TDelegate>();
            var constructorInfo = GetConstructorInfo(source, ctrArgs);
            if (constructorInfo == null)
            {
                return null;
            }
            var parameters = ctrArgs.Select(Expression.Parameter).ToList();
            Expression returnExpression = Expression.New(constructorInfo, parameters);
            if (!source.IsClass)
            {
                returnExpression = Expression.Convert(returnExpression, typeof(object));
            }
            return Expression.Lambda(returnExpression, parameters).Compile() as TDelegate;
        }

        public static Func<TSource> DefaultContructor<TSource>()
        {
            return Contructor<Func<TSource>>();
        }

        public static Func<object> DefaultContructor(this Type type)
        {
            return type.Contructor<Func<object>>();
        }

        public static Delegate DelegateIndexerGet(Type source, Type returnType,
            params Type[] indexTypes)
        {
            var indexerInfo = GetIndexerPropertyInfo(source, indexTypes);
            if (indexerInfo?.GetMethod == null)
            {
                return null;
            }
            var sourceObjectParam = Expression.Parameter(typeof(object));
            var paramsExpression = new ParameterExpression[indexTypes.Length];
            for (var i = 0; i < indexTypes.Length; i++)
            {
                var indexType = indexTypes[i];
                paramsExpression[i] = Expression.Parameter(indexType);
            }
            return Expression.Lambda(
                Expression.Call(Expression.Convert(sourceObjectParam, source), indexerInfo.GetMethod, paramsExpression),
                new[] { sourceObjectParam }.Concat(paramsExpression)).Compile();
        }

        public static TDelegate DelegateIndexerSet<TDelegate>(Type source, Type returnType,
            params Type[] indexTypes)
            where TDelegate : class
        {
            var indexerInfo = GetIndexerPropertyInfo(source, indexTypes);
            if (indexerInfo?.SetMethod == null)
            {
                return null;
            }
            ParameterExpression sourceObjectParam;
            if (source.IsClass)
            {
                sourceObjectParam = Expression.Parameter(typeof(object));
            }
            else
            {
                sourceObjectParam = Expression.Parameter(typeof(object).MakeByRefType());
            }
            var valueParam = Expression.Parameter(returnType);
            var indexExpressions = new ParameterExpression[indexTypes.Length];
            for (var i = 0; i < indexTypes.Length; i++)
            {
                var indexType = indexTypes[i];
                indexExpressions[i] = Expression.Parameter(indexType);
            }
            var callArgs = indexExpressions.Concat(new[] { valueParam }).ToArray();
            var paramsExpressions = new[] { sourceObjectParam }.Concat(callArgs);
            var structVariable = Expression.Variable(source, "struct");
            Expression blockExpr;
            if (!source.IsClass)
            {
                blockExpr = Expression.Block(typeof(object), new[] { structVariable },
                        Expression.Assign(structVariable, Expression.Convert(sourceObjectParam, source)),
                        Expression.Call(structVariable, indexerInfo.SetMethod, callArgs),
                        Expression.Assign(sourceObjectParam, Expression.Convert(structVariable, typeof(object)))
                    );
            }
            else
            {
                blockExpr = Expression.Call(Expression.Convert(sourceObjectParam, source),
                    indexerInfo.SetMethod, callArgs);
            }
            return Expression.Lambda<TDelegate>(blockExpr, paramsExpressions).Compile();
        }

        public static Action<TSource, EventHandler<TEventArgs>> EventAdd<TSource, TEventArgs>(string eventName)
        {
            return EventAccessor<TSource, TEventArgs>(eventName, AddAccessor);
        }

        public static Action<object, EventHandler<TEventArgs>> EventAdd<TEventArgs>(
            this Type source, string eventName)
        {
            return EventAccessor<TEventArgs>(source, eventName, AddAccessor);
        }

        public static Action<TSource, Action<TSource, object>> EventAdd<TSource>(string eventName)
        {
            return typeof(TSource).EventAddImpl<Action<TSource, Action<TSource, object>>>(eventName);
        }

        public static Action<object, Action<object, object>> EventAdd(this Type source, string eventName)
        {
            return source.EventAddImpl<Action<object, Action<object, object>>>(eventName);
        }

        public static EventHandler<TEventArgs> EventHandlerFactory<TEventArgs, TSource>(
            object handler, bool isRemove)
            where TEventArgs : class
        {
            EventHandler<TEventArgs> newEventHandler;
            var haveKey = false;
            var kv = EventsProxies.FirstOrDefault(k =>
            {
                object keyTarget;
                k.Key.TryGetTarget(out keyTarget);
                if (Equals(keyTarget, handler))
                {
                    haveKey = true;
                    return true;
                }
                return false;
            });
            if (haveKey)
            {
                object fromCache;
                EventsProxies[kv.Key].TryGetTarget(out fromCache);
                newEventHandler = (EventHandler<TEventArgs>)fromCache;
                if (isRemove)
                {
                    EventsProxies.Remove(kv.Key);
                    return newEventHandler;
                }
            }
            if (!isRemove)
            {
                var action = handler as Action<TSource, object>;
                if (action != null)
                {
                    newEventHandler = (s, a) => action((TSource)s, a);
                }
                else
                {
                    newEventHandler = new EventHandler<TEventArgs>((Action<object, object>)handler);
                }
                EventsProxies[new WeakReference<object>(handler)] = new WeakReference<object>(newEventHandler);
                return newEventHandler;
            }
            return null;
        }

        public static Action<object, EventHandler<TEventArgs>> EventRemove<TEventArgs>(
            this Type source, string eventName)
        {
            return EventAccessor<TEventArgs>(source, eventName, RemoveAccessor);
        }

        public static Action<TSource, EventHandler<TEventArgs>> EventRemove<TSource, TEventArgs>(string eventName)
        {
            return EventAccessor<TSource, TEventArgs>(eventName, RemoveAccessor);
        }

        public static Action<TSource, Action<TSource, object>> EventRemove<TSource>(string eventName)
        {
            return typeof(TSource).EventRemoveImpl<Action<TSource, Action<TSource, object>>>(eventName);
        }

        public static Action<object, Action<object, object>> EventRemove(this Type source, string eventName)
        {
            return source.EventRemoveImpl<Action<object, Action<object, object>>>(eventName);
        }

        public static Func<TSource, TField> FieldGet<TSource, TField>(string fieldName)
        {
            return typeof(TSource).FieldGetImpl<Func<TSource, TField>>(fieldName);
        }

        public static StructGetFunc<TSource, TField> FieldGetStruct<TSource, TField>(string fieldName)
            where TSource : struct
        {
            return typeof(TSource).FieldGetImpl<StructGetFunc<TSource, TField>>(fieldName, true);
        }

        public static Func<object, TField> FieldGet<TField>(this Type source,
            string fieldName)
        {
            return source.FieldGetImpl<Func<object, TField>>(fieldName);
        }

        private static TDelegate FieldGetImpl<TDelegate>(this Type source, string fieldName, bool byRef = false)
            where TDelegate : class
        {
            var fieldInfo = GetFieldInfo(source, fieldName);
            if (fieldInfo != null)
            {
                var sourceTypeInDelegate = GetFuncDelegateArguments<TDelegate>().First();
                Expression instanceExpression;
                ParameterExpression sourceParam;
                if (sourceTypeInDelegate != source)
                {
                    sourceParam = Expression.Parameter(typeof(object));
                    instanceExpression = Expression.Convert(sourceParam, source);
                }
                else
                {
                    if (byRef && source.IsValueType)
                    {
                        sourceParam = Expression.Parameter(source.MakeByRefType());
                        instanceExpression = sourceParam;
                    }
                    else
                    {
                        sourceParam = Expression.Parameter(source);
                        instanceExpression = sourceParam;
                    }
                }
                Expression returnExpression = Expression.Field(instanceExpression, fieldInfo);
                if (!fieldInfo.FieldType.IsClass)
                {
                    returnExpression = Expression.Convert(returnExpression, GetFuncDelegateReturnType<TDelegate>());
                }
                var lambda = Expression.Lambda<TDelegate>(returnExpression, sourceParam);
                var fieldGetImpl = lambda.Compile();
                return fieldGetImpl;
            }
            return null;
        }

        public static Func<object, object> FieldGet(this Type source, string fieldName)
        {
            return source.FieldGetImpl<Func<object, object>>(fieldName);
        }

        [Obsolete]
        public static Func<object, TField> FieldGet2<TField>(this Type source,
            string fieldName)
        {
            var fieldInfo = GetFieldInfo(source, fieldName);
            if (fieldInfo != null)
            {
                var sourceParam = Expression.Parameter(typeof(object));
                Expression returnExpression = Expression.Field(Expression.Convert(sourceParam, source), fieldInfo);
                var lambda = Expression.Lambda(returnExpression, sourceParam);
                return (Func<object, TField>)lambda.Compile();
            }
            return null;
        }

        public static StructSetActionRef<object, TProperty> FieldSetStructRef<TProperty>(this Type source,
            string fieldName)
        {
            var fieldInfo = GetFieldInfo(source, fieldName);
            if (fieldInfo != null && !fieldInfo.IsInitOnly)
            {
                var sourceParam = Expression.Parameter(typeof(object).MakeByRefType());
                ParameterExpression valueParam;
                Expression valueExpr;
                if (fieldInfo.FieldType == typeof(TProperty))
                {
                    valueParam = Expression.Parameter(fieldInfo.FieldType);
                    valueExpr = valueParam;
                }
                else
                {
                    valueParam = Expression.Parameter(typeof(TProperty));
                    valueExpr = Expression.Convert(valueParam, fieldInfo.FieldType);
                }
                var structVariable = Expression.Variable(source, "struct");
                var body = Expression.Block(typeof(void), new[] { structVariable },
                    Expression.Assign(structVariable, Expression.Convert(sourceParam, source)),
                    Expression.Assign(Expression.Field(structVariable, fieldInfo), valueExpr),
                    Expression.Assign(sourceParam, Expression.Convert(structVariable, typeof(object)))
                );
                var lambda = Expression.Lambda<StructSetActionRef<object, TProperty>>(body, sourceParam, valueParam);
                return lambda.Compile();
            }
            return null;
        }

        public static StructSetAction<object, TProperty> FieldSetStruct<TProperty>(this Type source, string fieldName)
        {
            var fieldInfo = GetFieldInfo(source, fieldName);
            if (fieldInfo != null && !fieldInfo.IsInitOnly)
            {
                var sourceParam = Expression.Parameter(typeof(object));
                ParameterExpression valueParam;
                Expression valueExpr;
                if (fieldInfo.FieldType == typeof(TProperty))
                {
                    valueParam = Expression.Parameter(fieldInfo.FieldType);
                    valueExpr = valueParam;
                }
                else
                {
                    valueParam = Expression.Parameter(typeof(TProperty));
                    valueExpr = Expression.Convert(valueParam, fieldInfo.FieldType);
                }
                var structVariable = Expression.Variable(source, "struct");
                var body = Expression.Block(typeof(object), new[] { structVariable },
                    Expression.Assign(structVariable, Expression.Convert(sourceParam, source)),
                    Expression.Assign(Expression.Field(structVariable, fieldInfo), valueExpr),
                    Expression.Assign(sourceParam, Expression.Convert(structVariable, typeof(object)))
                );
                var lambda = Expression.Lambda<StructSetAction<object, TProperty>>(body, sourceParam, valueParam);
                return lambda.Compile();
            }
            return null;
        }

        public static Action<object, TProperty> FieldSet<TProperty>(this Type source, string fieldName)
        {
            var fieldInfo = GetFieldInfo(source, fieldName);
            if (fieldInfo != null && !fieldInfo.IsInitOnly)
            {
                var sourceParam = Expression.Parameter(typeof(object));
                Expression valueExpr;
                var valueParam = Expression.Parameter(typeof(TProperty));
                if (fieldInfo.FieldType == typeof(TProperty))
                {
                    valueExpr = valueParam;
                }
                else
                {
                    valueExpr = Expression.Convert(valueParam, fieldInfo.FieldType);
                }
                var lambda = Expression.Lambda<Action<object, TProperty>>(
                    Expression.Assign(Expression.Field(Expression.Convert(sourceParam, source), fieldInfo), valueExpr),
                    sourceParam, valueParam);
                return lambda.Compile();
            }
            return null;
        }

        //TODO: write test when we create delegates with source as interface with property or method instead of concrete class with implementation
        public static StructSetActionRef<TSource, TProperty> FieldSetStruct<TSource, TProperty>(string fieldName)
            where TSource : struct
        {
            var source = typeof(TSource);
            var fieldInfo = GetFieldInfo(source, fieldName);
            if (fieldInfo != null && !fieldInfo.IsInitOnly)
            {
                Expression sourcExpr;
                var sourceParam = Expression.Parameter(typeof(TSource).MakeByRefType());
                if (source == typeof(TSource))
                {
                    sourcExpr = sourceParam;
                }
                else
                {
                    sourcExpr = Expression.Convert(sourceParam, source);
                }
                var valueParam = Expression.Parameter(typeof(TProperty), "value");
                Expression valueExpr;
                var structVariable = Expression.Variable(source, "struct");
                if (fieldInfo.FieldType == typeof(TProperty))
                {
                    valueExpr = valueParam;
                }
                else
                {
                    valueExpr = Expression.Convert(valueParam, fieldInfo.FieldType);
                }
                var body = Expression.Block(typeof(void), new[] { structVariable },
                    Expression.Assign(structVariable, sourcExpr),
                    Expression.Assign(Expression.Field(structVariable, fieldInfo), valueExpr),
                    Expression.Assign(sourceParam, Expression.Convert(structVariable, typeof(TSource)))
                );
                var lambda = Expression.Lambda<StructSetActionRef<TSource, TProperty>>(body, sourceParam, valueParam);
                return lambda.Compile();
            }
            return null;
        }

        public static Action<TSource, TProperty> FieldSet<TSource, TProperty>(string fieldName)
            where TSource : class
        {
            var source = typeof(TSource);
            var fieldInfo = GetFieldInfo(source, fieldName);
            if (fieldInfo != null && !fieldInfo.IsInitOnly)
            {
                var sourceParam = Expression.Parameter(source);
                var valueParam = Expression.Parameter(typeof(TProperty));
                var lambda = Expression.Lambda<Action<TSource, TProperty>>(
                    Expression.Assign(Expression.Field(sourceParam, fieldInfo), valueParam),
                    sourceParam, valueParam);
                return lambda.Compile();
            }
            return null;
        }

        public static Action<object, object> FieldSet(this Type source, string fieldName)
        {
            var fieldInfo = GetFieldInfo(source, fieldName);
            if (fieldInfo != null && !fieldInfo.IsInitOnly)
            {
                var sourceParam = Expression.Parameter(typeof(object));
                var valueParam = Expression.Parameter(typeof(object));
                var convertedValueExpr = Expression.Convert(valueParam, fieldInfo.FieldType);
                Expression returnExpression =
                    Expression.Assign(Expression.Field(Expression.Convert(sourceParam, source), fieldInfo),
                        convertedValueExpr);
                if (!fieldInfo.FieldType.IsClass)
                {
                    returnExpression = Expression.Convert(returnExpression, typeof(object));
                }
                var lambda = Expression.Lambda<Action<object, object>>(returnExpression, sourceParam, valueParam);
                return lambda.Compile();
            }
            return null;
        }

        public static StructSetActionRef<object, object> FieldSetStructRef(this Type source, string fieldName)
        {
            var fieldInfo = GetFieldInfo(source, fieldName);
            if (fieldInfo != null && !fieldInfo.IsInitOnly)
            {
                var sourceParam = Expression.Parameter(typeof(object).MakeByRefType());
                var valueParam = Expression.Parameter(typeof(object));
                Expression valueExpr;
                if (fieldInfo.FieldType != typeof(object))
                {
                    valueExpr = Expression.Convert(valueParam, fieldInfo.FieldType);
                }
                else
                {
                    valueExpr = valueParam;
                }
                var structVariable = Expression.Variable(source, "struct");
                Expression returnExpression = Expression.Assign(Expression.Field(structVariable, fieldInfo), valueExpr);
                if (!fieldInfo.FieldType.IsClass)
                {
                    returnExpression = Expression.Convert(returnExpression, typeof(object));
                }
                var body = Expression.Block(typeof(void), new[] { structVariable },
                    Expression.Assign(structVariable, Expression.Convert(sourceParam, source)),
                    returnExpression,
                    Expression.Assign(sourceParam, Expression.Convert(structVariable, typeof(object)))
                );
                var lambda = Expression.Lambda<StructSetActionRef<object, object>>(body, sourceParam, valueParam);
                return lambda.Compile();
            }
            return null;
        }

        public static StructSetAction<object, object> FieldSetStruct(this Type source, string fieldName)
        {
            var fieldInfo = GetFieldInfo(source, fieldName);
            if (fieldInfo != null && !fieldInfo.IsInitOnly)
            {
                var sourceParam = Expression.Parameter(typeof(object));
                var valueParam = Expression.Parameter(typeof(object));
                Expression valueExpr;
                if (fieldInfo.FieldType != typeof(object))
                {
                    valueExpr = Expression.Convert(valueParam, fieldInfo.FieldType);
                }
                else
                {
                    valueExpr = valueParam;
                }
                var structVariable = Expression.Variable(source, "struct");
                Expression returnExpression = Expression.Assign(Expression.Field(structVariable, fieldInfo), valueExpr);
                if (!fieldInfo.FieldType.IsClass)
                {
                    returnExpression = Expression.Convert(returnExpression, typeof(object));
                }
                var body = Expression.Block(typeof(object), new[] { structVariable },
                    Expression.Assign(structVariable, Expression.Convert(sourceParam, source)),
                    returnExpression,
                    Expression.Assign(sourceParam, Expression.Convert(structVariable, typeof(object)))
                );
                var lambda = Expression.Lambda<StructSetAction<object, object>>(body, sourceParam, valueParam);
                return lambda.Compile();
            }
            return null;
        }

        [Obsolete]
        public static Action<object, TProperty> FieldSetWithCast<TProperty>(this Type source, string fieldName)
        {
            return source.FieldSet(fieldName) as Action<object, TProperty>;
        }

        [Obsolete]
        public static Action<TSource, TProperty> FieldSetWithCast<TSource, TProperty>(string fieldName)
        {
            return typeof(TSource).FieldSet(fieldName) as Action<TSource, TProperty>;
        }

        public static Func<TSource, TIndex, TReturn> IndexerGet<TSource, TReturn, TIndex>()
            where TSource : class
        {
            var propertyInfo = GetIndexerPropertyInfo(typeof(TSource), new[] { typeof(TIndex) });
            return (Func<TSource, TIndex, TReturn>)
                propertyInfo?.GetMethod?.CreateDelegate(typeof(Func<TSource, TIndex, TReturn>));
        }

        public static StructIndex1GetFunc<TSource, TIndex, TReturn> IndexerGetStruct<TSource, TReturn, TIndex>()
            where TSource : struct
        {
            var propertyInfo = GetIndexerPropertyInfo(typeof(TSource), new[] { typeof(TIndex) });
            return (StructIndex1GetFunc<TSource, TIndex, TReturn>)
                propertyInfo?.GetMethod?.CreateDelegate(typeof(StructIndex1GetFunc<TSource, TIndex, TReturn>));
        }

        public static Func<TSource, TIndex, TIndex2, TReturn> IndexerGet<TSource, TReturn, TIndex, TIndex2>()
            where TSource : class
        {
            var propertyInfo = GetIndexerPropertyInfo(typeof(TSource),
                new[] { typeof(TIndex), typeof(TIndex2) });
            return (Func<TSource, TIndex, TIndex2, TReturn>)
                propertyInfo?.GetMethod?.CreateDelegate(typeof(Func<TSource, TIndex, TIndex2, TReturn>));
        }

        public static StructIndex2GetFunc<TSource, TIndex, TIndex2, TReturn>
            IndexerGetStruct<TSource, TReturn, TIndex, TIndex2>()
            where TSource : struct
        {
            var propertyInfo = GetIndexerPropertyInfo(typeof(TSource),
                new[] { typeof(TIndex), typeof(TIndex2) });
            return (StructIndex2GetFunc<TSource, TIndex, TIndex2, TReturn>)
                propertyInfo?.GetMethod?.CreateDelegate(typeof(StructIndex2GetFunc<TSource, TIndex, TIndex2, TReturn>));
        }

        public static Func<TSource, TIndex, TIndex2, TIndex2, TReturn> IndexerGet
            <TSource, TReturn, TIndex, TIndex2, TIndex3>() where TSource : class
        {
            var propertyInfo = GetIndexerPropertyInfo(typeof(TSource),
                new[] { typeof(TIndex), typeof(TIndex2), typeof(TIndex3) });
            return (Func<TSource, TIndex, TIndex2, TIndex2, TReturn>)
                propertyInfo?.GetMethod?.CreateDelegate(typeof(Func<TSource, TIndex, TIndex2, TIndex2, TReturn>));
        }

        public static StructIndex3GetFunc<TSource, TIndex, TIndex2, TIndex2, TReturn> IndexerGetStruct
            <TSource, TReturn, TIndex, TIndex2, TIndex3>() where TSource : struct
        {
            var propertyInfo = GetIndexerPropertyInfo(typeof(TSource),
                new[] { typeof(TIndex), typeof(TIndex2), typeof(TIndex3) });
            return (StructIndex3GetFunc<TSource, TIndex, TIndex2, TIndex2, TReturn>)
                propertyInfo?.GetMethod?.CreateDelegate(
                    typeof(StructIndex3GetFunc<TSource, TIndex, TIndex2, TIndex2, TReturn>));
        }

        public static Func<object, TIndex, TReturn> IndexerGet<TReturn, TIndex>(this Type source)
        {
            var indexType = typeof(TIndex);
            return (Func<object, TIndex, TReturn>)DelegateIndexerGet(source, typeof(TReturn), indexType);
        }

        public static Func<object, TIndex, TIndex2, TReturn> IndexerGet<TReturn, TIndex, TIndex2>(this Type source)
        {
            var indexType = typeof(TIndex);
            var indexType2 = typeof(TIndex2);
            return
                (Func<object, TIndex, TIndex2, TReturn>)
                DelegateIndexerGet(source, typeof(TReturn), indexType, indexType2);
        }

        public static Func<object, TIndex, TIndex2, TIndex3, TReturn> IndexerGet<TReturn, TIndex, TIndex2, TIndex3>(
            this Type source)
        {
            var indexType = typeof(TIndex);
            var indexType2 = typeof(TIndex2);
            var indexType3 = typeof(TIndex3);
            return
                (Func<object, TIndex, TIndex2, TIndex3, TReturn>)
                DelegateIndexerGet(source, typeof(TReturn), indexType, indexType2, indexType3);
        }

        public static Func<object, object[], object> IndexerGet(this Type source, Type returnType,
            params Type[] indexTypes)
        {
            var propertyInfo = GetIndexerPropertyInfo(source, indexTypes);
            if (propertyInfo?.GetMethod == null)
            {
                return null;
            }
            var sourceObjectParam = Expression.Parameter(typeof(object));
            var indexesParam = Expression.Parameter(typeof(object[]));
            var paramsExpression = new Expression[indexTypes.Length];
            for (var i = 0; i < indexTypes.Length; i++)
            {
                var indexType = indexTypes[i];
                paramsExpression[i] = Expression.Convert(Expression.ArrayIndex(indexesParam, Expression.Constant(i)),
                    indexType);
            }
            Expression returnExpression =
                Expression.Call(Expression.Convert(sourceObjectParam, source), propertyInfo.GetMethod, paramsExpression);
            if (!propertyInfo.PropertyType.IsClass)
            {
                returnExpression = Expression.Convert(returnExpression, typeof(object));
            }
            return (Func<object, object[], object>)Expression.Lambda(
                returnExpression, sourceObjectParam, indexesParam).Compile();
        }

        public static Func<object, object, object> IndexerGet(this Type source, Type returnType, Type indexType)
        {
            var propertyInfo = GetIndexerPropertyInfo(source, new[] { indexType });
            if (propertyInfo?.GetMethod == null)
            {
                return null;
            }
            var sourceObjectParam = Expression.Parameter(typeof(object));
            var indexObjectParam = Expression.Parameter(typeof(object));
            Expression returnExpression = Expression.Call(Expression.Convert(sourceObjectParam, source),
                propertyInfo.GetMethod, Expression.Convert(indexObjectParam, indexType));
            if (!propertyInfo.PropertyType.IsClass)
            {
                returnExpression = Expression.Convert(returnExpression, typeof(object));
            }
            return (Func<object, object, object>)
                Expression.Lambda(returnExpression, sourceObjectParam, indexObjectParam).Compile();
        }

        public static Action<object, TIndex, TReturn> IndexerSet<TReturn, TIndex>(this Type source)
        {
            var indexType = typeof(TIndex);
            return DelegateIndexerSet<Action<object, TIndex, TReturn>>
                (source, typeof(TReturn), indexType);
        }

        public static StructIndex1SetAction<object, TIndex, TReturn> IndexerSetStruct<TReturn, TIndex>(this Type source)
        {
            var indexType = typeof(TIndex);
            return DelegateIndexerSet<StructIndex1SetAction<object, TIndex, TReturn>>
                (source, typeof(TReturn), indexType);
        }

        public static Action<object, TIndex, TIndex2, TReturn> IndexerSet<TReturn, TIndex, TIndex2>(this Type source)
        {
            var indexType = typeof(TIndex);
            var indexType2 = typeof(TIndex2);
            return DelegateIndexerSet<Action<object, TIndex, TIndex2, TReturn>>
                (source, typeof(TReturn), indexType, indexType2);
        }

        public static StructIndex2SetAction<object, TIndex, TIndex2, TReturn> IndexerSetStruct<TReturn, TIndex, TIndex2>
            (this Type source)
        {
            var indexType = typeof(TIndex);
            var indexType2 = typeof(TIndex2);
            return DelegateIndexerSet<StructIndex2SetAction<object, TIndex, TIndex2, TReturn>>
                (source, typeof(TReturn), indexType, indexType2);
        }

        public static Action<object, TIndex, TIndex2, TIndex3, TReturn> IndexerSet<TReturn, TIndex, TIndex2, TIndex3>(
            this Type source)
        {
            var indexType = typeof(TIndex);
            var indexType2 = typeof(TIndex2);
            var indexType3 = typeof(TIndex3);
            return DelegateIndexerSet<Action<object, TIndex, TIndex2, TIndex3, TReturn>>
                (source, typeof(TReturn), indexType, indexType2, indexType3);
        }

        public static StructIndex3SetAction<object, TIndex, TIndex2, TIndex3, TReturn>
            IndexerSetStruct<TReturn, TIndex, TIndex2, TIndex3>(this Type source)
        {
            var indexType = typeof(TIndex);
            var indexType2 = typeof(TIndex2);
            var indexType3 = typeof(TIndex3);
            return DelegateIndexerSet<StructIndex3SetAction<object, TIndex, TIndex2, TIndex3, TReturn>>
                (source, typeof(TReturn), indexType, indexType2, indexType3);
        }

        public static Action<TSource, TIndex, TProperty> IndexerSet<TSource, TIndex, TProperty>()
            where TSource : class
        {
            var sourceType = typeof(TSource);
            var propertyInfo = GetIndexerPropertyInfo(sourceType, new[] { typeof(TIndex) });
            return (Action<TSource, TIndex, TProperty>)
                propertyInfo?.SetMethod?.CreateDelegate(typeof(Action<TSource, TIndex, TProperty>));
        }

        public static StructIndex1SetAction<TSource, TIndex, TProperty> IndexerSetStruct<TSource, TIndex, TProperty>()
            where TSource : struct
        {
            var sourceType = typeof(TSource);
            var propertyInfo = GetIndexerPropertyInfo(sourceType, new[] { typeof(TIndex) });
            return (StructIndex1SetAction<TSource, TIndex, TProperty>)
                propertyInfo?.SetMethod?.CreateDelegate(typeof(StructIndex1SetAction<TSource, TIndex, TProperty>));
        }

        public static Action<TSource, TIndex, TIndex2, TReturn> IndexerSet<TSource, TReturn, TIndex, TIndex2>()
            where TSource : class
        {
            var propertyInfo = GetIndexerPropertyInfo(typeof(TSource),
                new[] { typeof(TIndex), typeof(TIndex2) });
            return (Action<TSource, TIndex, TIndex2, TReturn>)
                propertyInfo?.SetMethod?.CreateDelegate(typeof(Action<TSource, TIndex, TIndex2, TReturn>));
        }

        public static StructIndex2SetAction<TSource, TIndex, TIndex2, TReturn> IndexerSetStruct<TSource, TReturn, TIndex, TIndex2>()
            where TSource : struct
        {
            var propertyInfo = GetIndexerPropertyInfo(typeof(TSource),
                new[] { typeof(TIndex), typeof(TIndex2) });
            return (StructIndex2SetAction<TSource, TIndex, TIndex2, TReturn>)
                propertyInfo?.SetMethod?.CreateDelegate(typeof(StructIndex2SetAction<TSource, TIndex, TIndex2, TReturn>));
        }

        public static Action<TSource, TIndex, TIndex2, TIndex2, TReturn> IndexerSet
            <TSource, TReturn, TIndex, TIndex2, TIndex3>()
        {
            var propertyInfo = GetIndexerPropertyInfo(typeof(TSource),
                new[] { typeof(TIndex), typeof(TIndex2), typeof(TIndex3) });
            return (Action<TSource, TIndex, TIndex2, TIndex2, TReturn>)
                propertyInfo?.SetMethod?.CreateDelegate(
                    typeof(Action<TSource, TIndex, TIndex2, TIndex2, TReturn>));
        }

        public static StructIndex3SetAction<TSource, TIndex, TIndex2, TIndex2, TReturn> IndexerSetStruct
            <TSource, TReturn, TIndex, TIndex2, TIndex3>() where TSource : struct
        {
            var propertyInfo = GetIndexerPropertyInfo(typeof(TSource),
                new[] { typeof(TIndex), typeof(TIndex2), typeof(TIndex3) });
            return (StructIndex3SetAction<TSource, TIndex, TIndex2, TIndex2, TReturn>)
                propertyInfo?.SetMethod?.CreateDelegate(
                    typeof(StructIndex3SetAction<TSource, TIndex, TIndex2, TIndex2, TReturn>));
        }

        public static Action<object, object[], object> IndexerSet(this Type source, Type returnType,
            params Type[] indexTypes)
        {
            return source.IndexerSetObjectsImpl<Action<object, object[], object>>(returnType, indexTypes);
        }

        public static StructIndexesSetAction<object, object> IndexerSetStruct(this Type source, Type returnType,
            params Type[] indexTypes)
        {
            return source.IndexerSetObjectsImpl<StructIndexesSetAction<object, object>>(returnType, indexTypes);
        }

        private static TDelegate IndexerSetObjectsImpl<TDelegate>(this Type source, Type returnType,
            params Type[] indexTypes)
            where TDelegate : class
        {
            var indexerInfo = GetIndexerPropertyInfo(source, indexTypes);
            if (indexerInfo?.SetMethod == null)
            {
                return null;
            }
            ParameterExpression sourceObjectParam;
            if (source.IsClass)
            {
                sourceObjectParam = Expression.Parameter(typeof(object));
            }
            else
            {
                sourceObjectParam = Expression.Parameter(typeof(object).MakeByRefType());
            }
            var indexesParam = Expression.Parameter(typeof(object[]));
            var valueParam = Expression.Parameter(typeof(object));
            var paramsExpression = new Expression[indexTypes.Length + 1];
            for (var i = 0; i < indexTypes.Length; i++)
            {
                var indexType = indexTypes[i];
                paramsExpression[i] = Expression.Convert(Expression.ArrayIndex(indexesParam, Expression.Constant(i)),
                    indexType);
            }
            paramsExpression[indexTypes.Length] = Expression.Convert(valueParam, returnType);
            Expression returnExpression;
            if (source.IsClass)
            {
                returnExpression = Expression.Call(Expression.Convert(sourceObjectParam, source), 
                    indexerInfo.SetMethod, paramsExpression); 
            }
            else
            {
                var structVariable = Expression.Variable(source, "struct");
                returnExpression = Expression.Block(typeof(object), new[] { structVariable },
                    Expression.Assign(structVariable, Expression.Convert(sourceObjectParam, source)),
                    Expression.Call(structVariable, indexerInfo.SetMethod, paramsExpression),
                    Expression.Assign(sourceObjectParam, Expression.Convert(structVariable, typeof(object)))
                );
            }
            return Expression.Lambda<TDelegate>(returnExpression, sourceObjectParam, indexesParam, valueParam).Compile();
        }

        public static Func<object, object[], object> InstanceGenericMethod(this Type source,
            string name, Type[] paramsTypes, Type[] typeParams)
        {
            return InstanceGenericMethod<Func<object, object[], object>>(source, name, typeParams, paramsTypes);
        }

        public static TDelegate InstanceGenericMethod<TDelegate>(this Type source,
            string name, Type[] typeParams, Type[] paramsTypes)
            where TDelegate : class
        {
            var methodInfo = GetMethodInfo(source, name, paramsTypes, typeParams);
            if (methodInfo == null)
            {
                return null;
            }
            var argsArray = Expression.Parameter(typeof(object[]));
            var sourceParameter = Expression.Parameter(typeof(object));
            var paramsExpression = new Expression[paramsTypes.Length];
            for (var i = 0; i < paramsTypes.Length; i++)
            {
                var argType = paramsTypes[i];
                paramsExpression[i] =
                    Expression.Convert(Expression.ArrayIndex(argsArray, Expression.Constant(i)), argType);
            }
            Expression returnExpression = Expression.Call(Expression.Convert(sourceParameter, source),
                methodInfo, paramsExpression);
            if (methodInfo.ReturnType != typeof(void) && !methodInfo.ReturnType.IsClass)
            {
                returnExpression = Expression.Convert(returnExpression, typeof(object));
            }
            return Expression.Lambda(returnExpression, sourceParameter, argsArray).Compile() as TDelegate;
        }

        public static Action<object, object[]> InstanceGenericMethodVoid(this Type source,
            string name, Type[] paramsTypes, Type[] typeParams)
        {
            return InstanceGenericMethod<Action<object, object[]>>(source, name, typeParams, paramsTypes);
        }

        public static Func<object, object[], object> InstanceMethod(this Type source,
            string name, params Type[] paramsTypes)
        {
            return InstanceGenericMethod<Func<object, object[], object>>(source, name, null, paramsTypes);
        }

        public static TDelegate InstanceMethod<TDelegate, TParam1>(string name)
            where TDelegate : class
        {
            return InstanceMethod<TDelegate>(name, typeof(TParam1));
        }

        public static TDelegate InstanceMethod<TDelegate, TParam1, TParam2>(string name)
            where TDelegate : class
        {
            return InstanceMethod<TDelegate>(name, typeof(TParam1), typeof(TParam2));
        }

        public static TDelegate InstanceMethod<TDelegate, TParam1, TParam2, TParam3>(string name)
            where TDelegate : class
        {
            return InstanceMethod<TDelegate>(name, typeof(TParam1), typeof(TParam2), typeof(TParam3));
        }

        public static TDelegate InstanceMethod<TDelegate>(string name, params Type[] typeParameters)
            where TDelegate : class
        {
            var paramsTypes = GetFuncDelegateArguments<TDelegate>();
            var source = paramsTypes.First();
            paramsTypes = paramsTypes.Skip(1).ToArray();
            var methodInfo = GetMethodInfo(source, name, paramsTypes, typeParameters);
            return methodInfo?.CreateDelegate(typeof(TDelegate)) as TDelegate;
        }

        public static TDelegate InstanceMethod<TDelegate, TParam1>(this Type source, string name)
            where TDelegate : class
        {
            return source.InstanceMethod<TDelegate>(name, new[] { typeof(TParam1) });
        }

        public static TDelegate InstanceMethod<TDelegate, TParam1, TParam2>(this Type source, string name)
            where TDelegate : class
        {
            return source.InstanceMethod<TDelegate>(name, new[] { typeof(TParam1), typeof(TParam2) });
        }

        public static TDelegate InstanceMethod<TDelegate, TParam1, TParam2, TParam3>(this Type source, string name)
            where TDelegate : class
        {
            return source.InstanceMethod<TDelegate>(name, new[] { typeof(TParam1), typeof(TParam2), typeof(TParam3) });
        }

        public static TDelegate InstanceMethod<TDelegate>(this Type source, string name, Type[] typeParams = null)
            where TDelegate : class
        {
            var delegateParams = GetFuncDelegateArguments<TDelegate>();
            var instanceParam = delegateParams[0];
            delegateParams = delegateParams.Skip(1).ToArray();
            var methodInfo = GetMethodInfo(source, name, delegateParams, typeParams);
            if (methodInfo == null)
            {
                return null;
            }
            Delegate deleg;
            if (instanceParam == source)
            {
                deleg = methodInfo.CreateDelegate(typeof(TDelegate));
            }
            else
            {
                var sourceParameter = Expression.Parameter(typeof(object));
                var expressions = delegateParams.Select(Expression.Parameter).ToArray();
                Expression returnExpression = Expression.Call(Expression.Convert(sourceParameter, source),
                    methodInfo, expressions.Cast<Expression>());
                if (methodInfo.ReturnType != typeof(void) && !methodInfo.ReturnType.IsClass)
                {
                    returnExpression = Expression.Convert(returnExpression, typeof(object));
                }
                var lamdaParams = new[] { sourceParameter }.Concat(expressions);
                deleg = Expression.Lambda(returnExpression, lamdaParams).Compile();
            }
            return deleg as TDelegate;
        }

        [Obsolete]
        public static TDelegate InstanceMethodExpr<TDelegate>(string methodName) where TDelegate : class
        {
            var source = typeof(TDelegate).GenericTypeArguments[0];
            var param = Expression.Parameter(source);
            var parameters = new List<ParameterExpression> { param };
            var @params = GetFuncDelegateArguments<TDelegate>().Skip(1);
            foreach (var type in @params)
            {
                parameters.Add(Expression.Parameter(type));
            }
            var lambda = Expression.Lambda(Expression.Call(param, methodName, null,
                parameters.Skip(1).Cast<Expression>().ToArray()), parameters);
            return lambda.Compile() as TDelegate;
        }

        public static Action<object, object[]> InstanceMethodVoid(this Type source,
            string name, params Type[] paramsTypes)
        {
            return InstanceGenericMethod<Action<object, object[]>>(source, name, null, paramsTypes);
        }

        public static Func<object, TProperty> PropertyGet<TProperty>(this Type source, string propertyName)
        {
            return source.PropertyGetImpl<Func<object, TProperty>>(propertyName);
        }

        public static Func<object, object> PropertyGet(this Type source, string propertyName)
        {
            return source.PropertyGetImpl<Func<object, object>>(propertyName);
        }

        public static Func<TSource, TProperty> PropertyGet<TSource, TProperty>(PropertyInfo propertyInfo)
            where TSource : class
        {
            return PropertyGet<TSource, TProperty>(null, propertyInfo);
        }

        public static Func<TSource, TProperty> PropertyGet<TSource, TProperty>(string propertyName)
            where TSource : class
        {
            return PropertyGet<TSource, TProperty>(propertyName, null);
        }

        [Obsolete]
        public static Func<TSource, TProperty> PropertyGet2<TSource, TProperty>(this Type source,
            string propertyName)
        {
            var p = Expression.Parameter(source);
            var lambda = Expression.Lambda(Expression.Property(p, propertyName), p);
            return (Func<TSource, TProperty>)lambda.Compile();
        }

        public static Action<TSource, TProperty> PropertySet<TSource, TProperty>(this TSource source,
            string propertyName)
        {
            return PropertySet<TSource, TProperty>(propertyName);
        }

        public static Action<object, TProperty> PropertySet<TProperty>(this Type source, string propertyName)
        {
            var propertyInfo = GetPropertyInfo(source, propertyName);
            if (propertyInfo?.SetMethod == null)
            {
                return null;
            }
            var sourceObjectParam = Expression.Parameter(typeof(object));
            ParameterExpression propertyValueParam;
            Expression valueExpression;
            if (propertyInfo.PropertyType == typeof(TProperty))
            {
                propertyValueParam = Expression.Parameter(propertyInfo.PropertyType);
                valueExpression = propertyValueParam;
            }
            else
            {
                propertyValueParam = Expression.Parameter(typeof(TProperty));
                valueExpression = Expression.Convert(propertyValueParam, propertyInfo.PropertyType);
            }
            var @delegate =
                Expression.Lambda(
                    Expression.Call(Expression.Convert(sourceObjectParam, source), propertyInfo.SetMethod,
                        valueExpression), sourceObjectParam, propertyValueParam).Compile();
            return (Action<object, TProperty>)@delegate;
        }

        public static StructSetActionRef<object, TProperty> PropertySetStructRef<TProperty>
            (this Type source, string propertyName)
        {
            var propertyInfo = GetPropertyInfo(source, propertyName);
            if (propertyInfo?.SetMethod == null)
            {
                return null;
            }
            var sourceObjectParam = Expression.Parameter(typeof(object).MakeByRefType());
            ParameterExpression propertyValueParam;
            Expression valueExpression;
            if (propertyInfo.PropertyType == typeof(TProperty))
            {
                propertyValueParam = Expression.Parameter(propertyInfo.PropertyType);
                valueExpression = propertyValueParam;
            }
            else
            {
                propertyValueParam = Expression.Parameter(typeof(TProperty));
                valueExpression = Expression.Convert(propertyValueParam, propertyInfo.PropertyType);
            }
            var structVariable = Expression.Variable(source, "struct");
            var blockExpr = Expression.Block(typeof(void), new[] { structVariable },
                Expression.Assign(structVariable, Expression.Convert(sourceObjectParam, source)),
                Expression.Call(structVariable, propertyInfo.SetMethod, valueExpression),
                Expression.Assign(sourceObjectParam, Expression.Convert(structVariable, typeof(object)))
            );
            var @delegate =
                Expression.Lambda<StructSetActionRef<object, TProperty>>(blockExpr, sourceObjectParam,
                    propertyValueParam).Compile();
            return @delegate;
        }

        public static StructSetAction<object, TProperty> PropertySetStruct<TProperty>
            (this Type source, string propertyName)
        {
            var propertyInfo = GetPropertyInfo(source, propertyName);
            if (propertyInfo?.SetMethod == null)
            {
                return null;
            }
            var sourceObjectParam = Expression.Parameter(typeof(object));
            ParameterExpression propertyValueParam;
            Expression valueExpression;
            if (propertyInfo.PropertyType == typeof(TProperty))
            {
                propertyValueParam = Expression.Parameter(propertyInfo.PropertyType);
                valueExpression = propertyValueParam;
            }
            else
            {
                propertyValueParam = Expression.Parameter(typeof(TProperty));
                valueExpression = Expression.Convert(propertyValueParam, propertyInfo.PropertyType);
            }
            var structVariable = Expression.Variable(source, "struct");
            var blockExpr = Expression.Block(typeof(object), new[] { structVariable },
                Expression.Assign(structVariable, Expression.Convert(sourceObjectParam, source)),
                Expression.Call(structVariable, propertyInfo.SetMethod, valueExpression),
                Expression.Assign(sourceObjectParam, Expression.Convert(structVariable, typeof(object)))
            );
            var @delegate =
                Expression.Lambda<StructSetAction<object, TProperty>>(blockExpr, sourceObjectParam, propertyValueParam)
                    .Compile();
            return @delegate;
        }

        public static Action<object, object> PropertySet(this Type source, string propertyName)
        {
            return source.PropertySet<object>(propertyName);
        }

        public static StructSetActionRef<object, object> PropertySetStructRef(this Type source, string propertyName)
        {
            return source.PropertySetStructRef<object>(propertyName);
        }

        public static StructSetAction<object, object> PropertySetStruct(this Type source, string propertyName)
        {
            return source.PropertySetStruct<object>(propertyName);
        }

        public static Action<TSource, TProperty> PropertySet<TSource, TProperty>(string propertyName)
        {
            var source = typeof(TSource);
            var propertyInfo = GetPropertyInfo(source, propertyName);
            return
                (Action<TSource, TProperty>)propertyInfo?.SetMethod?.CreateDelegate(typeof(Action<TSource, TProperty>));
        }

        public static StructSetActionRef<TSource, TProperty> PropertySetStructRef<TSource, TProperty>(
            string propertyName)
        {
            var source = typeof(TSource);
            var propertyInfo = GetPropertyInfo(source, propertyName);
            return
                (StructSetActionRef<TSource, TProperty>)
                propertyInfo?.SetMethod?.CreateDelegate(typeof(StructSetActionRef<TSource, TProperty>));
        }

        public static Action<EventHandler<TEvent>> StaticEventAdd<TEvent>(this Type source, string eventName)
        {
            var eventInfo = (source.GetEvent(eventName)
                             ?? source.GetEvent(eventName, BindingFlags.NonPublic))
                            ?? source.GetEvent(eventName, BindingFlags.NonPublic | BindingFlags.Public);
            return
                (Action<EventHandler<TEvent>>)eventInfo?.AddMethod.CreateDelegate(typeof(Action<EventHandler<TEvent>>));
        }

        public static Func<TField> StaticFieldGet<TSource, TField>(string fieldName)
        {
            var source = typeof(TSource);
            return source.StaticFieldGet<TField>(fieldName);
        }

        public static Func<TField> StaticFieldGet<TField>(this Type source,
            string fieldName)
        {
            var fieldInfo = GetStaticFieldInfo(source, fieldName);
            if (fieldInfo != null)
            {
                var lambda = Expression.Lambda(Expression.Field(null, fieldInfo));
                return (Func<TField>)lambda.Compile();
            }
            return null;
        }

        public static Func<object> StaticFieldGet(this Type source, string fieldName)
        {
            var fieldInfo = GetStaticFieldInfo(source, fieldName);
            if (fieldInfo != null)
            {
                Expression returnExpression = Expression.Field(null, fieldInfo);
                if (!fieldInfo.FieldType.IsClass)
                {
                    returnExpression = Expression.Convert(returnExpression, typeof(object));
                }
                var lambda = Expression.Lambda(returnExpression);
                return (Func<object>)lambda.Compile();
            }
            return null;
        }

        [Obsolete]
        public static Func<TField> StaticFieldGetExpr<TSource, TField>(string fieldName)
        {
            var source = typeof(TSource);
            var lambda = Expression.Lambda(Expression.Field(null, source, fieldName));
            return (Func<TField>)lambda.Compile();
        }

        public static Action<TField> StaticFieldSet<TSource, TField>(string fieldName)
        {
            var source = typeof(TSource);
            return source.StaticFieldSet<TField>(fieldName);
        }

        public static Action<TField> StaticFieldSet<TField>(this Type source, string fieldName)
        {
            var fieldInfo = GetStaticFieldInfo(source, fieldName);
            if (fieldInfo != null && !fieldInfo.IsInitOnly)
            {
                var valueParam = Expression.Parameter(typeof(TField));
                var lambda = Expression.Lambda(typeof(Action<TField>),
                    Expression.Assign(Expression.Field(null, fieldInfo), valueParam), valueParam);
                return (Action<TField>)lambda.Compile();
            }
            return null;
        }

        public static Action<object> StaticFieldSet(this Type source, string fieldName)
        {
            var fieldInfo = GetStaticFieldInfo(source, fieldName);
            if (fieldInfo != null && !fieldInfo.IsInitOnly)
            {
                var valueParam = Expression.Parameter(typeof(object));
                var convertedValueExpr = Expression.Convert(valueParam, fieldInfo.FieldType);
                var lambda = Expression.Lambda(typeof(Action<object>),
                    Expression.Assign(Expression.Field(null, fieldInfo), convertedValueExpr), valueParam);
                return (Action<object>)lambda.Compile();
            }
            return null;
        }

        public static Func<object[], object> StaticGenericMethod(this Type source,
            string name, Type[] paramsTypes, Type[] typeParams)
        {
            return StaticMethod<Func<object[], object>>(source, name, typeParams, paramsTypes);
        }

        public static Action<object[]> StaticGenericMethodVoid(this Type source,
            string name, Type[] paramsTypes, Type[] typeParams)
        {
            return StaticMethod<Action<object[]>>(source, name, typeParams, paramsTypes);
        }

        public static TDelegate StaticMethod<TSource, TDelegate>(string name)
            where TDelegate : class
        {
            return typeof(TSource).StaticMethod<TDelegate>(name);
        }

        public static TDelegate StaticMethod<TSource, TDelegate, TParam1>(string name)
            where TDelegate : class
        {
            return typeof(TSource).StaticMethod<TDelegate>(name, typeof(TParam1));
        }

        public static TDelegate StaticMethod<TSource, TDelegate, TParam1, TParam2>(string name)
            where TDelegate : class
        {
            return typeof(TSource).StaticMethod<TDelegate>(name, typeof(TParam1), typeof(TParam2));
        }

        public static TDelegate StaticMethod<TSource, TDelegate, TParam1, TParam2, TParam3>(string name)
            where TDelegate : class
        {
            return typeof(TSource).StaticMethod<TDelegate>(name, typeof(TParam1), typeof(TParam2), typeof(TParam3));
        }

        public static TDelegate StaticMethod<TSource, TDelegate>(string name, params Type[] typeParameters)
            where TDelegate : class
        {
            return typeof(TSource).StaticMethod<TDelegate>(name, typeParameters);
        }

        public static TDelegate StaticMethod<TDelegate, TParam1>(this Type source, string name)
            where TDelegate : class
        {
            return source.StaticMethod<TDelegate>(name, typeof(TParam1));
        }

        public static TDelegate StaticMethod<TDelegate, TParam1, TParam2>(this Type source, string name)
            where TDelegate : class
        {
            return source.StaticMethod<TDelegate>(name, typeof(TParam1), typeof(TParam2));
        }

        public static TDelegate StaticMethod<TDelegate, TParam1, TParam2, TParam3>(this Type source, string name)
            where TDelegate : class
        {
            return source.StaticMethod<TDelegate>(name, typeof(TParam1), typeof(TParam2), typeof(TParam3));
        }

        public static TDelegate StaticMethod<TDelegate>(this Type source, string name, params Type[] typeParameters)
            where TDelegate : class
        {
            var paramsTypes = GetFuncDelegateArguments<TDelegate>();
            var methodInfo = GetStaticMethodInfo(source, name, paramsTypes, typeParameters);
            return methodInfo?.CreateDelegate(typeof(TDelegate)) as TDelegate;
        }

        public static TDelegate StaticMethod<TDelegate>(this Type source, string name)
            where TDelegate : class
        {
            return source.StaticMethod<TDelegate>(name, null);
        }

        public static Func<object[], object> StaticMethod(this Type source,
            string name, params Type[] paramsTypes)
        {
            return StaticMethod<Func<object[], object>>(source, name, null, paramsTypes);
        }

        public static TDelegate StaticMethod<TDelegate>(this Type source,
            string name, Type[] typeParams, Type[] paramsTypes)
            where TDelegate : class
        {
            var methodInfo = GetStaticMethodInfo(source, name, paramsTypes, typeParams);
            if (methodInfo == null)
            {
                return null;
            }
            var argsArray = Expression.Parameter(typeof(object[]));
            var paramsExpression = new Expression[paramsTypes.Length];
            for (var i = 0; i < paramsTypes.Length; i++)
            {
                var argType = paramsTypes[i];
                paramsExpression[i] =
                    Expression.Convert(Expression.ArrayIndex(argsArray, Expression.Constant(i)), argType);
            }
            Expression returnExpression = Expression.Call(methodInfo, paramsExpression);
            if (methodInfo.ReturnType != typeof(void) && !methodInfo.ReturnType.IsClass)
            {
                returnExpression = Expression.Convert(returnExpression, typeof(object));
            }
            return Expression.Lambda(returnExpression, argsArray).Compile() as TDelegate;
        }

        public static Action<object[]> StaticMethodVoid(this Type source,
            string name, params Type[] paramsTypes)
        {
            return StaticMethod<Action<object[]>>(source, name, null, paramsTypes);
        }

        public static Func<TProperty> StaticPropertyGet<TSource, TProperty>(string propertyName)
        {
            return typeof(TSource).StaticPropertyGet<TProperty>(propertyName);
        }

        public static Func<TProperty> StaticPropertyGet<TProperty>(this Type source, string propertyName)
        {
            var propertyInfo = GetStaticPropertyInfo(source, propertyName);
            return (Func<TProperty>)propertyInfo?.GetMethod?.CreateDelegate(typeof(Func<TProperty>));
        }

        public static Func<object> StaticPropertyGet(this Type source, string propertyName)
        {
            var propertyInfo = GetStaticPropertyInfo(source, propertyName);
            if (propertyInfo?.GetMethod == null)
            {
                return null;
            }
            Expression returnExpression = Expression.Call(propertyInfo.GetMethod);
            if (!propertyInfo.PropertyType.IsClass)
            {
                returnExpression = Expression.Convert(returnExpression, typeof(object));
            }
            return (Func<object>)Expression.Lambda(returnExpression).Compile();
        }

        [Obsolete]
        public static Func<TProperty> StaticPropertyGetExpr<TProperty>(this Type source, string propertyName)
        {
            var lambda = Expression.Lambda(Expression.Property(null, source, propertyName));
            return (Func<TProperty>)lambda.Compile();
        }

        public static Action<TProperty> StaticPropertySet<TSource, TProperty>(string propertyName)
        {
            return typeof(TSource).StaticPropertySet<TProperty>(propertyName);
        }

        public static Action<TProperty> StaticPropertySet<TProperty>(this Type source, string propertyName)
        {
            var propertyInfo = GetStaticPropertyInfo(source, propertyName);
            return (Action<TProperty>)propertyInfo?.SetMethod?.CreateDelegate(typeof(Action<TProperty>));
        }

        public static Action<object> StaticPropertySet(this Type source, string propertyName)
        {
            var propertyInfo = GetStaticPropertyInfo(source, propertyName);
            if (propertyInfo?.SetMethod == null)
            {
                return null;
            }
            var valueParam = Expression.Parameter(typeof(object));
            var convertedValue = Expression.Convert(valueParam, propertyInfo.PropertyType);
            Expression returnExpression = Expression.Call(propertyInfo.SetMethod, convertedValue);
            return (Action<object>)Expression.Lambda(returnExpression, valueParam).Compile();
        }

        private static Action<object, EventHandler<TEventArgs>> EventAccessor<TEventArgs>
            (Type source, string eventName, string accessorName)
        {
            var accessor = GetEventInfoAccessor(eventName, source, accessorName);
            if (accessor != null)
            {
                var instanceParameter = Expression.Parameter(typeof(object));
                var delegateTypeParameter = Expression.Parameter(typeof(EventHandler<TEventArgs>));
                var lambda = Expression.Lambda(Expression.Call(Expression.Convert(instanceParameter, source),
                        accessor, delegateTypeParameter),
                    instanceParameter, delegateTypeParameter);
                return (Action<object, EventHandler<TEventArgs>>)lambda.Compile();
            }
            return null;
        }

        private static Action<TSource, EventHandler<TEventArgs>> EventAccessor<TSource, TEventArgs>
            (string eventName, string accessorName)
        {
            var sourceType = typeof(TSource);
            var accessor = GetEventInfoAccessor(eventName, sourceType, accessorName);
            return (Action<TSource, EventHandler<TEventArgs>>)
                accessor?.CreateDelegate(typeof(Action<TSource, EventHandler<TEventArgs>>));
        }

        private static TDelegate EventAccessorImpl<TDelegate>(Type source, string eventName, string accessorName)
            where TDelegate : class
        {
            var eventInfo = GetEventInfo(eventName, source);
            if (eventInfo != null)
            {
                var accessor = accessorName == AddAccessor ? eventInfo.AddMethod : eventInfo.RemoveMethod;
                var eventArgsType = eventInfo.EventHandlerType.GetGenericArguments()[0];
                var instanceParameter = Expression.Parameter(typeof(object));
                var delegateTypeParameter = Expression.Parameter(typeof(object));
                var methodCallExpression =
                    Expression.Call(EventHandlerFactoryMethodInfo.MakeGenericMethod(eventArgsType, source),
                        delegateTypeParameter, Expression.Constant(accessorName == RemoveAccessor));
                var lambda = Expression.Lambda(Expression.Call(Expression.Convert(instanceParameter, source),
                        accessor, methodCallExpression),
                    instanceParameter, delegateTypeParameter);
                return lambda.Compile() as TDelegate;
            }
            return null;
        }

        private static TDelegate EventAddImpl<TDelegate>(this Type source, string eventName)
            where TDelegate : class
        {
            return EventAccessorImpl<TDelegate>(source, eventName, AddAccessor);
        }

        private static TDelegate EventRemoveImpl<TDelegate>(this Type source, string eventName)
            where TDelegate : class
        {
            return EventAccessorImpl<TDelegate>(source, eventName, RemoveAccessor);
        }

        private static ConstructorInfo GetConstructorInfo(Type source, Type[] types)
        {
            return (source.GetConstructor(BindingFlags.Public, null, types, null) ??
                    source.GetConstructor(BindingFlags.NonPublic, null, types, null)) ??
                   source.GetConstructor(
                       BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance, null,
                       types, null);
        }

        private static EventInfo GetEventInfo(string eventName, Type sourceType)
        {
            return (sourceType.GetEvent(eventName)
                    ?? sourceType.GetEvent(eventName, BindingFlags.NonPublic))
                   ?? sourceType.GetEvent(eventName,
                       BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
        }

        private static MethodInfo GetEventInfoAccessor(string eventName, Type sourceType, string accessor)
        {
            var eventInfo = GetEventInfo(eventName, sourceType);
            return accessor == AddAccessor ? eventInfo?.AddMethod : eventInfo?.RemoveMethod;
        }

        private static FieldInfo GetFieldInfo(Type source, string fieldName)
        {
            var fieldInfo = (source.GetField(fieldName) ??
                             source.GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic)) ??
                            source.GetField(fieldName,
                                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            return fieldInfo;
        }

        private static Type[] GetFuncDelegateArguments<TDelegate>() where TDelegate : class
        {
            IEnumerable<Type> arguments = typeof(TDelegate).GenericTypeArguments;
            return arguments.Reverse().Skip(1).Reverse().ToArray();
        }

        private static Type GetFuncDelegateReturnType<TDelegate>() where TDelegate : class
        {
            return typeof(TDelegate).GenericTypeArguments.Last();
        }

        private static PropertyInfo GetIndexerPropertyInfo(Type source, Type[] indexesTypes,
            string indexerName = null)
        {
            indexerName = indexerName ?? Item;

            var properties = source.GetProperties().Concat(
                source.GetProperties(BindingFlags.NonPublic)).Concat(
                source.GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)).ToArray();
            if (indexerName == Item)
            {
                var firstIndexerInfo = properties.FirstOrDefault(p => p.GetIndexParameters().Length > 0);
                if (firstIndexerInfo != null && firstIndexerInfo.Name != indexerName)
                {
                    indexerName = firstIndexerInfo.Name;
                }
            }
            var indexerInfo = properties.FirstOrDefault(p => p.Name == indexerName
                && IndexParametersEquals(p.GetIndexParameters(), indexesTypes));
            if (indexerInfo != null)
            {
                return indexerInfo;
            }
            return null;
        }

        private static bool IndexParametersEquals(ParameterInfo[] first, Type[] second)
        {
            if (first.Length != second.Length)
            {
                return false;
            }
            var indexParametersEquals = first.Select((t, i) => t.ParameterType == second[i]).All(p => p);
            return indexParametersEquals;
        }

        private static MethodInfo GetMethodInfo(Type source, string name, Type[] parametersTypes,
            Type[] typeParameters = null)
        {
            MethodInfo methodInfo = null;
            try
            {
                methodInfo =
                    (source.GetMethod(name, BindingFlags.Instance | BindingFlags.Public, null, parametersTypes, null) ??
                     source.GetMethod(name, BindingFlags.Instance | BindingFlags.NonPublic, null, parametersTypes, null)) ??
                    source.GetMethod(name, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public, null,
                        parametersTypes, null);
            }
            catch (AmbiguousMatchException)
            {
                //swallow and test generics
            }
            //check for generic methods
            if (typeParameters != null)
            {
                var ms = source.GetMethods(BindingFlags.Instance | BindingFlags.Public)
                    .Concat(source.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance))
                    .Concat(source.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance));
                foreach (var m in ms)
                {
                    if (m.Name == name && m.IsGenericMethod)
                    {
                        var parameters = m.GetParameters();
                        var genericArguments = m.GetGenericArguments();
                        var parametersTypesValid = parameters.Length == parametersTypes.Length;
                        parametersTypesValid &= genericArguments.Length == typeParameters.Length;
                        if (!parametersTypesValid)
                        {
                            continue;
                        }
                        for (var index = 0; index < parameters.Length; index++)
                        {
                            var parameterInfo = parameters[index];
                            var parameterType = parametersTypes[index];
                            if (parameterInfo.ParameterType != parameterType
                                && parameterInfo.ParameterType.IsGenericParameter
                                && !parameterInfo.ParameterType.CanBeAssignedFrom(parameterType))
                            {
                                parametersTypesValid = false;
                                break;
                            }
                        }
                        for (var index = 0; index < genericArguments.Length; index++)
                        {
                            var genericArgument = genericArguments[index];
                            var typeParameter = typeParameters[index];
                            if (!genericArgument.CanBeAssignedFrom(typeParameter))
                            {
                                parametersTypesValid = false;
                                break;
                            }
                        }
                        if (parametersTypesValid)
                        {
                            methodInfo = m.MakeGenericMethod(typeParameters);
                            break;
                        }
                    }
                }
            }
            return methodInfo;
        }

        private static PropertyInfo GetPropertyInfo(Type source, string propertyName)
        {
            var propertyInfo = source.GetProperty(propertyName) ??
                               source.GetProperty(propertyName, BindingFlags.NonPublic) ??
                               source.GetProperty(propertyName,
                                   BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            return propertyInfo;
        }

        private static FieldInfo GetStaticFieldInfo(Type source, string fieldName)
        {
            var fieldInfo = (source.GetField(fieldName, BindingFlags.Static) ??
                             source.GetField(fieldName, BindingFlags.Static | BindingFlags.NonPublic)) ??
                            source.GetField(fieldName,
                                BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
            return fieldInfo;
        }

        private static MethodInfo GetStaticMethodInfo(Type source, string name, Type[] parametersTypes,
            Type[] typeParameters = null)
        {
            MethodInfo methodInfo = null;
            try
            {
                methodInfo = (source.GetMethod(name, BindingFlags.Static, null, parametersTypes, null) ??
                              source.GetMethod(name, BindingFlags.Static | BindingFlags.NonPublic, null, parametersTypes,
                                  null)) ??
                             source.GetMethod(name, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public,
                                 null, parametersTypes, null);
            }
            catch (AmbiguousMatchException)
            {
                //swallow and test generics
            }
            //check for generic methods
            if (typeParameters != null)
            {
                var ms = source.GetMethods(BindingFlags.Static)
                    .Concat(source.GetMethods(BindingFlags.NonPublic | BindingFlags.Static))
                    .Concat(source.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static));
                foreach (var m in ms)
                {
                    if (m.Name == name && m.IsGenericMethod)
                    {
                        var parameters = m.GetParameters();
                        var genericArguments = m.GetGenericArguments();
                        var parametersTypesValid = parameters.Length == parametersTypes.Length;
                        parametersTypesValid &= genericArguments.Length == typeParameters.Length;
                        if (!parametersTypesValid)
                        {
                            continue;
                        }
                        for (var index = 0; index < parameters.Length; index++)
                        {
                            var parameterInfo = parameters[index];
                            var parameterType = parametersTypes[index];
                            if (parameterInfo.ParameterType != parameterType
                                && parameterInfo.ParameterType.IsGenericParameter
                                && !parameterInfo.ParameterType.CanBeAssignedFrom(parameterType))
                            {
                                parametersTypesValid = false;
                                break;
                            }
                        }
                        for (var index = 0; index < genericArguments.Length; index++)
                        {
                            var genericArgument = genericArguments[index];
                            var typeParameter = typeParameters[index];
                            if (!genericArgument.CanBeAssignedFrom(typeParameter))
                            {
                                parametersTypesValid = false;
                                break;
                            }
                        }
                        if (parametersTypesValid)
                        {
                            methodInfo = m.MakeGenericMethod(typeParameters);
                            break;
                        }
                    }
                }
            }
            return methodInfo;
        }

        private static PropertyInfo GetStaticPropertyInfo(Type source, string propertyName)
        {
            var propertyInfo = (source.GetProperty(propertyName, BindingFlags.Static) ??
                                source.GetProperty(propertyName, BindingFlags.Static | BindingFlags.NonPublic)) ??
                               source.GetProperty(propertyName,
                                   BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
            return propertyInfo;
        }

        private static Func<TSource, TProperty> PropertyGet<TSource, TProperty>(string propertyName = null,
            PropertyInfo propertyInfo = null)
            where TSource : class
        {
            var source = typeof(TSource);
            propertyInfo = propertyInfo ?? GetPropertyInfo(source, propertyName);
            return (Func<TSource, TProperty>)propertyInfo?.GetMethod?.CreateDelegate(typeof(Func<TSource, TProperty>));
        }

        public static StructGetFunc<TSource, TProperty> PropertyGetStruct<TSource, TProperty>(
            string propertyName = null)
            where TSource : struct
        {
            var source = typeof(TSource);
            var propertyInfo = GetPropertyInfo(source, propertyName);
            return
                (StructGetFunc<TSource, TProperty>)
                propertyInfo?.GetMethod?.CreateDelegate(typeof(StructGetFunc<TSource, TProperty>));
        }

        private static TDelegate PropertyGetImpl<TDelegate>(this Type source, string propertyName)
            where TDelegate : class
        {
            var propertyInfo = GetPropertyInfo(source, propertyName);
            if (propertyInfo?.GetMethod == null)
            {
                return null;
            }
            var sourceObjectParam = Expression.Parameter(typeof(object));
            Expression returnExpression =
                Expression.Call(Expression.Convert(sourceObjectParam, source), propertyInfo.GetMethod);
            if (!propertyInfo.PropertyType.IsClass)
            {
                returnExpression = Expression.Convert(returnExpression, GetFuncDelegateReturnType<TDelegate>());
            }
            return Expression.Lambda(returnExpression, sourceObjectParam).Compile() as TDelegate;
        }
    }
}