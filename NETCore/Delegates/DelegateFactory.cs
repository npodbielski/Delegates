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
using static Delegates.Helper.DelegateHelper;
#if NET45||NETCORE
using WeakReference = System.WeakReference<object>;
#endif

namespace Delegates
{
    public static class DelegateFactory
    {
        private static readonly MethodInfo EventHandlerFactoryMethodInfo =
            typeof(DelegateFactory).GetMethod("EventHandlerFactory");

        private static readonly Dictionary<WeakReference, WeakReference> EventsProxies =
            new Dictionary<WeakReference, WeakReference>();

        public static TDelegate Contructor<TDelegate>() where TDelegate : class
        {
            var source = GetDelegateReturnType<TDelegate>();
            var ctrArgs = GetDelegateArguments<TDelegate>();
            var constructorInfo = source.GetConstructorInfo(ctrArgs);
            if (constructorInfo == null)
            {
                return null;
            }
            var parameters = ctrArgs.GetParamsExprFromTypes();
            var ctorParams = parameters.GetNewExprParams();
            var lambdaParams = parameters.GetLambdaExprParams();
            return Expression.Lambda<TDelegate>(Expression.New(constructorInfo, ctorParams), lambdaParams)
                .Compile();
        }

        public static Func<object[], object> Contructor(this Type source, params Type[] ctrArgs)
        {
            var constructorInfo = source.GetConstructorInfo(ctrArgs);
            if (constructorInfo == null)
            {
                return null;
            }
            var argsArray = Expression.Parameter(typeof(object[]), "args");
            var paramsExpression = new Expression[ctrArgs.Length];
            for (var i = 0; i < ctrArgs.Length; i++)
            {
                var argType = ctrArgs[i];
                paramsExpression[i] =
                    Expression.Convert(Expression.ArrayIndex(argsArray, Expression.Constant(i)), argType);
            }
            Expression returnExpression = Expression.New(constructorInfo, paramsExpression);
            if (!source.IsClassType())
            {
                returnExpression = Expression.Convert(returnExpression, typeof(object));
            }
            return Expression.Lambda<Func<object[], object>>(returnExpression, argsArray).Compile();
        }

        public static TDelegate Contructor<TDelegate>(this Type source)
            where TDelegate : class
        {
            var ctrArgs = GetDelegateArguments<TDelegate>();
            var constructorInfo = source.GetConstructorInfo(ctrArgs);
            if (constructorInfo == null)
            {
                return null;
            }
            var parameters = ctrArgs.GetParamsExprFromTypes();
            var ctorParams = parameters.GetNewExprParams();
            var lambdaParams = parameters.GetLambdaExprParams();
            Expression returnExpression = Expression.New(constructorInfo, ctorParams);
            if (!source.IsClassType())
            {
                returnExpression = Expression.Convert(returnExpression, typeof(object));
            }
            return Expression.Lambda<TDelegate>(returnExpression, lambdaParams).Compile();
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
            var indexerInfo = source.GetIndexerPropertyInfo(indexTypes);
            if (indexerInfo?.GetMethod == null)
            {
                return null;
            }
            var sourceObjectParam = Expression.Parameter(typeof(object), "source");
            var paramsExpression = new ParameterExpression[indexTypes.Length];
            for (var i = 0; i < indexTypes.Length; i++)
            {
                var indexType = indexTypes[i];
                paramsExpression[i] = Expression.Parameter(indexType, "i" + i);
            }
            var getMethod = indexerInfo.GetMethod;
            var callParams = paramsExpression.GetCallExprParams(sourceObjectParam);
            return Expression.Lambda(
                Expression.Call(Expression.Convert(sourceObjectParam, source), getMethod, paramsExpression),
                callParams).Compile();
        }

        public static TDelegate DelegateIndexerSet<TDelegate>(Type source, Type returnType,
            params Type[] indexTypes)
            where TDelegate : class
        {
            var indexerInfo = source.GetIndexerPropertyInfo(indexTypes);
            if (indexerInfo?.SetMethod == null)
            {
                return null;
            }
            ParameterExpression sourceObjectParam;
            if (source.IsClassType() || source.GetTypeInfo().IsInterface)
            {
                sourceObjectParam = Expression.Parameter(typeof(object), "source");
            }
            else
            {
                sourceObjectParam = Expression.Parameter(typeof(object).MakeByRefType(), "source");
            }
            var valueParam = Expression.Parameter(returnType, "value");
            var indexExpressions = new ParameterExpression[indexTypes.Length];
            for (var i = 0; i < indexTypes.Length; i++)
            {
                var indexType = indexTypes[i];
                indexExpressions[i] = Expression.Parameter(indexType, "i" + i);
            }
            var callArgs = indexExpressions.Concat(new[] { valueParam }).ToArray();
            var paramsExpressions = new[] { sourceObjectParam }.Concat(callArgs);
            Expression blockExpr;
            if (!(source.IsClassType() || source.GetTypeInfo().IsInterface))
            {
#if !NET35
                var structVariable = Expression.Variable(source, "struct");
                blockExpr = Expression.Block(typeof(object), new[] { structVariable },
                    Expression.Assign(structVariable, Expression.Convert(sourceObjectParam, source)),
                    Expression.Call(structVariable, indexerInfo.SetMethod, callArgs),
                    Expression.Assign(sourceObjectParam, Expression.Convert(structVariable, typeof(object)))
                );
#else
                throw new ArgumentException("This is not supported for structures in .NET 3.5");
#endif
            }
            else
            {
                blockExpr = Expression.Call(Expression.Convert(sourceObjectParam, source),
                    indexerInfo.SetMethod, callArgs);
            }
            return Expression.Lambda<TDelegate>(blockExpr, paramsExpressions).Compile();
        }

        public static Action<TSource, EventHandler<TEventArgs>> EventAdd<TSource, TEventArgs>(string eventName)
#if NET35||NET4||PORTABLE
            where TEventArgs : EventArgs
#endif
        {
            return EventAccessor<TSource, TEventArgs>(eventName, TypeExtensions.AddAccessor);
        }

        public static Action<object, EventHandler<TEventArgs>> EventAdd<TEventArgs>(
            this Type source, string eventName)
#if NET35||NET4||PORTABLE
            where TEventArgs : EventArgs
#endif
        {
            return EventAccessor<TEventArgs>(source, eventName, TypeExtensions.AddAccessor);
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
            where TEventArgs :
#if NET35||NET4||PORTABLE
            EventArgs
#else
            class
#endif
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
                EventsProxies[new WeakReference(handler)] = new WeakReference(newEventHandler);
                return newEventHandler;
            }
            return null;
        }

        public static Action<object, EventHandler<TEventArgs>> EventRemove<TEventArgs>(
            this Type source, string eventName)
#if NET35||NET4||PORTABLE
            where TEventArgs : EventArgs
#endif
        {
            return EventAccessor<TEventArgs>(source, eventName, TypeExtensions.RemoveAccessor);
        }

        public static Action<TSource, EventHandler<TEventArgs>> EventRemove<TSource, TEventArgs>(string eventName)
#if NET35||NET4||PORTABLE
            where TEventArgs : EventArgs
#endif
        {
            return EventAccessor<TSource, TEventArgs>(eventName, TypeExtensions.RemoveAccessor);
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

#if !NET35
        public static StructGetFunc<TSource, TField> FieldGetStruct<TSource, TField>(string fieldName)
            where TSource : struct
        {
            return typeof(TSource).FieldGetImpl<StructGetFunc<TSource, TField>>(fieldName, true);
        }
#endif

        public static Func<object, TField> FieldGet<TField>(this Type source,
            string fieldName)
        {
            return source.FieldGetImpl<Func<object, TField>>(fieldName);
        }

        private static TDelegate FieldGetImpl<TDelegate>(this Type source, string fieldName, bool byRef = false)
            where TDelegate : class
        {
            var fieldInfo = source.GetFieldInfo(fieldName, false);
            if (fieldInfo != null)
            {
                var sourceTypeInDelegate = GetDelegateArguments<TDelegate>().First();
                Expression instanceExpression;
                ParameterExpression sourceParam;
                if (sourceTypeInDelegate.IsByRef == false ?
                    sourceTypeInDelegate != source :
                    sourceTypeInDelegate.GetElementType() != source)
                {
                    sourceParam = Expression.Parameter(typeof(object), "source");
                    instanceExpression = Expression.Convert(sourceParam, source);
                }
                else
                {
                    if (byRef && source.IsValueType())
                    {
                        sourceParam = Expression.Parameter(source.MakeByRefType(), "source");
                        instanceExpression = sourceParam;
                    }
                    else
                    {
                        sourceParam = Expression.Parameter(source, "source");
                        instanceExpression = sourceParam;
                    }
                }
                Expression returnExpression = Expression.Field(instanceExpression, fieldInfo);
                if (!fieldInfo.FieldType.IsClassType())
                {
                    returnExpression = Expression.Convert(returnExpression, GetDelegateReturnType<TDelegate>());
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
            var fieldInfo = source.GetFieldInfo(fieldName, false);
            if (fieldInfo != null)
            {
                var sourceParam = Expression.Parameter(typeof(object), "source");
                Expression returnExpression = Expression.Field(Expression.Convert(sourceParam, source), fieldInfo);
                var lambda = Expression.Lambda(returnExpression, sourceParam);
                return (Func<object, TField>)lambda.Compile();
            }
            return null;
        }

#if !NET35
        public static StructSetActionRef<object, TProperty> FieldSetStructRef<TProperty>(this Type source,
            string fieldName)
        {
            var fieldInfo = source.GetFieldInfo(fieldName, false);
            if (fieldInfo != null && !fieldInfo.IsInitOnly)
            {
                var sourceParam = Expression.Parameter(typeof(object).MakeByRefType(), "source");
                ParameterExpression valueParam;
                Expression valueExpr;
                if (fieldInfo.FieldType == typeof(TProperty))
                {
                    valueParam = Expression.Parameter(fieldInfo.FieldType, "value");
                    valueExpr = valueParam;
                }
                else
                {
                    valueParam = Expression.Parameter(typeof(TProperty), "value");
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
            var fieldInfo = source.GetFieldInfo(fieldName, false);
            if (fieldInfo != null && !fieldInfo.IsInitOnly)
            {
                var sourceParam = Expression.Parameter(typeof(object), "source");
                ParameterExpression valueParam;
                Expression valueExpr;
                if (fieldInfo.FieldType == typeof(TProperty))
                {
                    valueParam = Expression.Parameter(fieldInfo.FieldType, "value");
                    valueExpr = valueParam;
                }
                else
                {
                    valueParam = Expression.Parameter(typeof(TProperty), "value");
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
            var fieldInfo = source.GetFieldInfo(fieldName, false);
            if (fieldInfo != null && !fieldInfo.IsInitOnly)
            {
                var sourceParam = Expression.Parameter(typeof(object), "source");
                Expression valueExpr;
                var valueParam = Expression.Parameter(typeof(TProperty), "value");
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

        public static StructSetActionRef<TSource, TProperty> FieldSetStruct<TSource, TProperty>(string fieldName)
            where TSource : struct
        {
            var source = typeof(TSource);
            var fieldInfo = source.GetFieldInfo(fieldName, false);
            if (fieldInfo != null && !fieldInfo.IsInitOnly)
            {
                Expression sourcExpr;
                var sourceParam = Expression.Parameter(typeof(TSource).MakeByRefType(), "source");
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
            var fieldInfo = source.GetFieldInfo(fieldName, false);
            if (fieldInfo != null && !fieldInfo.IsInitOnly)
            {
                var sourceParam = Expression.Parameter(source, "source");
                var valueParam = Expression.Parameter(typeof(TProperty), "value");
                var lambda = Expression.Lambda<Action<TSource, TProperty>>(
                    Expression.Assign(Expression.Field(sourceParam, fieldInfo), valueParam),
                    sourceParam, valueParam);
                return lambda.Compile();
            }
            return null;
        }

        public static Action<object, object> FieldSet(this Type source, string fieldName)
        {
            var fieldInfo = source.GetFieldInfo(fieldName, false);
            if (fieldInfo != null && !fieldInfo.IsInitOnly)
            {
                var sourceParam = Expression.Parameter(typeof(object), "source");
                var valueParam = Expression.Parameter(typeof(object), "value");
                var convertedValueExpr = Expression.Convert(valueParam, fieldInfo.FieldType);
                Expression returnExpression =
                    Expression.Assign(Expression.Field(Expression.Convert(sourceParam, source), fieldInfo),
                        convertedValueExpr);
                if (!fieldInfo.FieldType.IsClassType())
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
            var fieldInfo = source.GetFieldInfo(fieldName, false);
            if (fieldInfo != null && !fieldInfo.IsInitOnly)
            {
                var sourceParam = Expression.Parameter(typeof(object).MakeByRefType(), "source");
                var valueParam = Expression.Parameter(typeof(object), "value");
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
                if (!fieldInfo.FieldType.IsClassType())
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
            var fieldInfo = source.GetFieldInfo(fieldName, false);
            if (fieldInfo != null && !fieldInfo.IsInitOnly)
            {
                var sourceParam = Expression.Parameter(typeof(object), "source");
                var valueParam = Expression.Parameter(typeof(object), "value");
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
                if (!fieldInfo.FieldType.IsClassType())
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
#endif

        public static Func<TSource, TIndex, TReturn> IndexerGet<TSource, TReturn, TIndex>()
            where TSource : class
        {
            var propertyInfo = typeof(TSource).GetIndexerPropertyInfo(new[] { typeof(TIndex) });
            return propertyInfo?.GetMethod?.CreateDelegate<Func<TSource, TIndex, TReturn>>();
        }

        public static StructIndex1GetFunc<TSource, TIndex, TReturn> IndexerGetStruct<TSource, TReturn, TIndex>()
            where TSource : struct
        {
            var propertyInfo = typeof(TSource).GetIndexerPropertyInfo(new[] { typeof(TIndex) });
            return propertyInfo?.GetMethod?.CreateDelegate<StructIndex1GetFunc<TSource, TIndex, TReturn>>();
        }

        public static Func<TSource, TIndex, TIndex2, TReturn> IndexerGet<TSource, TReturn, TIndex, TIndex2>()
            where TSource : class
        {
            var propertyInfo = typeof(TSource).GetIndexerPropertyInfo(new[] { typeof(TIndex), typeof(TIndex2) });
            return propertyInfo?.GetMethod?.CreateDelegate<Func<TSource, TIndex, TIndex2, TReturn>>();
        }

        public static StructIndex2GetFunc<TSource, TIndex, TIndex2, TReturn>
            IndexerGetStruct<TSource, TReturn, TIndex, TIndex2>()
            where TSource : struct
        {
            var propertyInfo = typeof(TSource).GetIndexerPropertyInfo(new[] { typeof(TIndex), typeof(TIndex2) });
            return propertyInfo?.GetMethod?.CreateDelegate<StructIndex2GetFunc<TSource, TIndex, TIndex2, TReturn>>();
        }

        public static Func<TSource, TIndex, TIndex2, TIndex2, TReturn> IndexerGet
            <TSource, TReturn, TIndex, TIndex2, TIndex3>() where TSource : class
        {
            var propertyInfo = typeof(TSource).GetIndexerPropertyInfo(
                new[] { typeof(TIndex), typeof(TIndex2), typeof(TIndex3) });
            return propertyInfo?.GetMethod?.CreateDelegate<Func<TSource, TIndex, TIndex2, TIndex2, TReturn>>();
        }

        public static StructIndex3GetFunc<TSource, TIndex, TIndex2, TIndex2, TReturn> IndexerGetStruct
            <TSource, TReturn, TIndex, TIndex2, TIndex3>() where TSource : struct
        {
            var propertyInfo = typeof(TSource).GetIndexerPropertyInfo(
                new[] { typeof(TIndex), typeof(TIndex2), typeof(TIndex3) });
            return propertyInfo?.GetMethod?.CreateDelegate<
                StructIndex3GetFunc<TSource, TIndex, TIndex2, TIndex2, TReturn>>();
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
            var propertyInfo = source.GetIndexerPropertyInfo(indexTypes);
            if (propertyInfo?.GetMethod == null)
            {
                return null;
            }
            var sourceObjectParam = Expression.Parameter(typeof(object), "source");
            var indexesParam = Expression.Parameter(typeof(object[]), "indexes");
            var paramsExpression = new Expression[indexTypes.Length];
            for (var i = 0; i < indexTypes.Length; i++)
            {
                var indexType = indexTypes[i];
                paramsExpression[i] = Expression.Convert(Expression.ArrayIndex(indexesParam, Expression.Constant(i)),
                    indexType);
            }
            Expression returnExpression =
                Expression.Call(Expression.Convert(sourceObjectParam, source), propertyInfo.GetMethod, paramsExpression);
            if (!propertyInfo.PropertyType.IsClassType())
            {
                returnExpression = Expression.Convert(returnExpression, typeof(object));
            }
            return Expression.Lambda<Func<object, object[], object>>(
                returnExpression, sourceObjectParam, indexesParam).Compile();
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
            var propertyInfo = sourceType.GetIndexerPropertyInfo(new[] { typeof(TIndex) });
            return propertyInfo?.SetMethod?.CreateDelegate<Action<TSource, TIndex, TProperty>>();
        }

        public static StructIndex1SetAction<TSource, TIndex, TProperty> IndexerSetStruct<TSource, TIndex, TProperty>()
            where TSource : struct
        {
            var sourceType = typeof(TSource);
            var propertyInfo = sourceType.GetIndexerPropertyInfo(new[] { typeof(TIndex) });
            return propertyInfo?.SetMethod?.CreateDelegate<StructIndex1SetAction<TSource, TIndex, TProperty>>();
        }

        public static Action<TSource, TIndex, TIndex2, TReturn> IndexerSet<TSource, TReturn, TIndex, TIndex2>()
            where TSource : class
        {
            var propertyInfo = typeof(TSource).GetIndexerPropertyInfo(new[] { typeof(TIndex), typeof(TIndex2) });
            return propertyInfo?.SetMethod?.CreateDelegate<Action<TSource, TIndex, TIndex2, TReturn>>();
        }

        public static StructIndex2SetAction<TSource, TIndex, TIndex2, TReturn> IndexerSetStruct
            <TSource, TReturn, TIndex, TIndex2>()
            where TSource : struct
        {
            var propertyInfo = typeof(TSource).GetIndexerPropertyInfo(new[] { typeof(TIndex), typeof(TIndex2) });
            return propertyInfo?.SetMethod?.CreateDelegate<StructIndex2SetAction<TSource, TIndex, TIndex2, TReturn>>();
        }

        public static Action<TSource, TIndex, TIndex2, TIndex2, TReturn> IndexerSet
            <TSource, TReturn, TIndex, TIndex2, TIndex3>()
        {
            var propertyInfo = typeof(TSource).GetIndexerPropertyInfo(new[] { typeof(TIndex), typeof(TIndex2), typeof(TIndex3) });
            return propertyInfo?.SetMethod?.CreateDelegate<Action<TSource, TIndex, TIndex2, TIndex2, TReturn>>();
        }

        public static StructIndex3SetAction<TSource, TIndex, TIndex2, TIndex2, TReturn> IndexerSetStruct
            <TSource, TReturn, TIndex, TIndex2, TIndex3>() where TSource : struct
        {
            var propertyInfo = typeof(TSource).GetIndexerPropertyInfo(
                new[] { typeof(TIndex), typeof(TIndex2), typeof(TIndex3) });
            return propertyInfo?.SetMethod?.CreateDelegate<StructIndex3SetAction<TSource, TIndex, TIndex2, TIndex2, TReturn>>();
        }

#if !NET35
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
            var indexerInfo = source.GetIndexerPropertyInfo(indexTypes);
            if (indexerInfo?.SetMethod == null)
            {
                return null;
            }
            ParameterExpression sourceObjectParam;
            if (source.IsClassType() || source.GetTypeInfo().IsInterface)
            {
                sourceObjectParam = Expression.Parameter(typeof(object), "source");
            }
            else
            {
                sourceObjectParam = Expression.Parameter(typeof(object).MakeByRefType(), "source");
            }
            var indexesParam = Expression.Parameter(typeof(object[]), "indexes");
            var valueParam = Expression.Parameter(typeof(object), "value");
            var paramsExpression = new Expression[indexTypes.Length + 1];
            for (var i = 0; i < indexTypes.Length; i++)
            {
                var indexType = indexTypes[i];
                paramsExpression[i] = Expression.Convert(Expression.ArrayIndex(indexesParam, Expression.Constant(i)),
                    indexType);
            }
            paramsExpression[indexTypes.Length] = Expression.Convert(valueParam, returnType);
            Expression returnExpression;
            if (source.IsClassType())
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
#endif

        public static Func<object, object[], object> InstanceGenericMethod(this Type source,
            string name, Type[] paramsTypes, Type[] typeParams)
        {
            return InstanceGenericMethod<Func<object, object[], object>>(source, name, typeParams, paramsTypes);
        }

        public static TDelegate InstanceGenericMethod<TDelegate>(this Type source,
            string name, Type[] typeParams, Type[] paramsTypes)
            where TDelegate : class
        {
            if (paramsTypes == null)
            {
                paramsTypes = new Type[0];
            }
            if (!(typeof(TDelegate) == typeof(Action<object, object[]>)
                 || typeof(TDelegate) == typeof(Func<object, object[], object>)))
            {
                throw new ArgumentException("This method only accepts delegates of types " +
                    typeof(Action<object, object[]>).FullName + " or " +
                    typeof(Func<object, object[], object>).FullName + ".");
            }
            var methodInfo = source.GetMethodInfo(name, paramsTypes, typeParams);
            if (methodInfo == null)
            {
                return null;
            }
            var argsArray = Expression.Parameter(typeof(object[]), "args");
            var sourceParameter = Expression.Parameter(typeof(object), "source");
            var paramsExpression = new Expression[paramsTypes.Length];
            for (var i = 0; i < paramsTypes.Length; i++)
            {
                var argType = paramsTypes[i];
                paramsExpression[i] =
                    Expression.Convert(Expression.ArrayIndex(argsArray, Expression.Constant(i)), argType);
            }
            Expression returnExpression = Expression.Call(Expression.Convert(sourceParameter, source),
                methodInfo, paramsExpression);
            if (methodInfo.ReturnType != typeof(void) && !methodInfo.ReturnType.IsClassType())
            {
                returnExpression = Expression.Convert(returnExpression, typeof(object));
            }
            CheckDelegateReturnType<TDelegate>(methodInfo);
            return Expression.Lambda<TDelegate>(returnExpression, sourceParameter, argsArray).Compile();
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
            CheckDelegate<TDelegate>();
            var paramsTypes = GetDelegateArguments<TDelegate>();
            var source = paramsTypes.First();
            if (source.GetTypeInfo().IsInterface && typeParameters != null && typeParameters.Length > 0)
            {
                return source.InstanceMethod<TDelegate>(name, typeParameters);
            }
            paramsTypes = paramsTypes.Skip(1).ToArray();
            var methodInfo = source.GetMethodInfo(name, paramsTypes, typeParameters);
            return methodInfo?.CreateDelegate<TDelegate>();
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
            var delegateParams = GetDelegateArguments<TDelegate>();
            var instanceParam = delegateParams[0];
            delegateParams = delegateParams.Skip(1).ToArray();
            var methodInfo = source.GetMethodInfo(name, delegateParams, typeParams);
            if (methodInfo == null)
            {
                return null;
            }
            TDelegate deleg;
            if (instanceParam == source &&
                //only if not generic interface method
                (!instanceParam.GetTypeInfo().IsInterface || (typeParams == null || typeParams.Length == 0)))
            {
                deleg = methodInfo.CreateDelegate<TDelegate>();
            }
            else if (instanceParam.CanBeAssignedFrom(source))
            {
                var sourceParameter = Expression.Parameter(GetDelegateArguments<TDelegate>().First(), "source");
                var expressions = delegateParams.GetParamsExprFromTypes();
                Expression returnExpression = Expression.Call(Expression.Convert(sourceParameter, source),
                    methodInfo, expressions.GetCallExprParams());
                if (methodInfo.ReturnType != typeof(void) && !methodInfo.ReturnType.IsClassType())
                {
                    returnExpression = Expression.Convert(returnExpression, GetDelegateReturnType<TDelegate>());
                }
                var lamdaParams = expressions.GetCallExprParams(sourceParameter);
                CheckDelegateReturnType<TDelegate>(methodInfo);
                deleg = Expression.Lambda<TDelegate>(returnExpression, lamdaParams).Compile();
            }
            else
            {
                throw new ArgumentException($"TDelegate type cannot have instance parameter of type {instanceParam.FullName}. This parameter type must be compatible with {source.FullName} type.");
            }
            return deleg;
        }

#if !NET35
        [Obsolete]
        public static TDelegate InstanceMethodExpr<TDelegate>(string methodName) where TDelegate : class
        {
            var source = typeof(TDelegate).GenericTypeArguments()[0];
            var param = Expression.Parameter(source, "source");
            var parameters = new List<ParameterExpression> { param };
            var @params = GetDelegateArguments<TDelegate>().Skip(1);
            var index = 0;
            foreach (var type in @params)
            {
                parameters.Add(Expression.Parameter(type, "p" + index++));
            }
            var lambda = Expression.Lambda(Expression.Call(param, methodName, null,
                parameters.Skip(1).Cast<Expression>().ToArray()), parameters);
            return lambda.Compile() as TDelegate;
        }
#endif

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
            var p = Expression.Parameter(source, "source");
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
            var propertyInfo = source.GetPropertyInfo(propertyName, false);
            if (propertyInfo?.SetMethod == null)
            {
                return null;
            }
            var sourceObjectParam = Expression.Parameter(typeof(object), "source");
            ParameterExpression propertyValueParam;
            Expression valueExpression;
            if (propertyInfo.PropertyType == typeof(TProperty))
            {
                propertyValueParam = Expression.Parameter(propertyInfo.PropertyType, "value");
                valueExpression = propertyValueParam;
            }
            else
            {
                propertyValueParam = Expression.Parameter(typeof(TProperty), "value");
                valueExpression = Expression.Convert(propertyValueParam, propertyInfo.PropertyType);
            }
            var @delegate =
                Expression.Lambda(
                    Expression.Call(Expression.Convert(sourceObjectParam, source), propertyInfo.SetMethod,
                        valueExpression), sourceObjectParam, propertyValueParam).Compile();
            return (Action<object, TProperty>)@delegate;
        }

#if !NET35
        public static StructSetActionRef<object, TProperty> PropertySetStructRef<TProperty>
            (this Type source, string propertyName)
        {
            var propertyInfo = source.GetPropertyInfo(propertyName, false);
            if (propertyInfo?.SetMethod == null)
            {
                return null;
            }
            var sourceObjectParam = Expression.Parameter(typeof(object).MakeByRefType(), "source");
            ParameterExpression propertyValueParam;
            Expression valueExpression;
            if (propertyInfo.PropertyType == typeof(TProperty))
            {
                propertyValueParam = Expression.Parameter(propertyInfo.PropertyType, "value");
                valueExpression = propertyValueParam;
            }
            else
            {
                propertyValueParam = Expression.Parameter(typeof(TProperty), "value");
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
            var propertyInfo = source.GetPropertyInfo(propertyName, false);
            if (propertyInfo?.SetMethod == null)
            {
                return null;
            }
            var sourceObjectParam = Expression.Parameter(typeof(object), "source");
            ParameterExpression propertyValueParam;
            Expression valueExpression;
            if (propertyInfo.PropertyType == typeof(TProperty))
            {
                propertyValueParam = Expression.Parameter(propertyInfo.PropertyType, "value");
                valueExpression = propertyValueParam;
            }
            else
            {
                propertyValueParam = Expression.Parameter(typeof(TProperty), "value");
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
#endif

        public static Action<object, object> PropertySet(this Type source, string propertyName)
        {
            return source.PropertySet<object>(propertyName);
        }

#if !NET35
        public static StructSetActionRef<object, object> PropertySetStructRef(this Type source, string propertyName)
        {
            return source.PropertySetStructRef<object>(propertyName);
        }

        public static StructSetAction<object, object> PropertySetStruct(this Type source, string propertyName)
        {
            return source.PropertySetStruct<object>(propertyName);
        }
#endif

        public static Action<TSource, TProperty> PropertySet<TSource, TProperty>(string propertyName)
        {
            var source = typeof(TSource);
            var propertyInfo = source.GetPropertyInfo(propertyName, false);
            return
                propertyInfo?.SetMethod?.CreateDelegate<Action<TSource, TProperty>>();
        }

        public static StructSetActionRef<TSource, TProperty> PropertySetStructRef<TSource, TProperty>(
            string propertyName)
        {
            var source = typeof(TSource);
            var propertyInfo = source.GetPropertyInfo(propertyName, false);
            return
                propertyInfo?.SetMethod?.CreateDelegate<StructSetActionRef<TSource, TProperty>>();
        }

        public static Action<EventHandler<TEvent>> StaticEventAdd<TEvent>(this Type source, string eventName)
#if NET35||NET4||PORTABLE
            where TEvent : EventArgs
#endif
        {
            var eventInfo = source.GetEventInfo(eventName);
            return eventInfo?.AddMethod.CreateDelegate<Action<EventHandler<TEvent>>>();
        }

        public static Func<TField> StaticFieldGet<TSource, TField>(string fieldName)
        {
            var source = typeof(TSource);
            return source.StaticFieldGet<TField>(fieldName);
        }

        public static Func<TField> StaticFieldGet<TField>(this Type source,
            string fieldName)
        {
            var fieldInfo = source.GetFieldInfo(fieldName, true);
            if (fieldInfo != null)
            {
                var lambda = Expression.Lambda(Expression.Field(null, fieldInfo));
                return (Func<TField>)lambda.Compile();
            }
            return null;
        }

        public static Func<object> StaticFieldGet(this Type source, string fieldName)
        {
            var fieldInfo = source.GetFieldInfo(fieldName, true);
            if (fieldInfo != null)
            {
                Expression returnExpression = Expression.Field(null, fieldInfo);
                if (!fieldInfo.FieldType.IsClassType())
                {
                    returnExpression = Expression.Convert(returnExpression, typeof(object));
                }
                var lambda = Expression.Lambda<Func<object>>(returnExpression);
                return lambda.Compile();
            }
            return null;
        }

#if  !NET35
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
            var fieldInfo = source.GetFieldInfo(fieldName, true);
            if (fieldInfo != null && !fieldInfo.IsInitOnly)
            {
                var valueParam = Expression.Parameter(typeof(TField), "value");
                var lambda = Expression.Lambda(typeof(Action<TField>),
                    Expression.Assign(Expression.Field(null, fieldInfo), valueParam), valueParam);
                return (Action<TField>)lambda.Compile();
            }
            return null;
        }

        public static Action<object> StaticFieldSet(this Type source, string fieldName)
        {
            var fieldInfo = source.GetFieldInfo(fieldName, true);
            if (fieldInfo != null && !fieldInfo.IsInitOnly)
            {
                var valueParam = Expression.Parameter(typeof(object), "value");
                var convertedValueExpr = Expression.Convert(valueParam, fieldInfo.FieldType);
                var lambda = Expression.Lambda(typeof(Action<object>),
                    Expression.Assign(Expression.Field(null, fieldInfo), convertedValueExpr), valueParam);
                return (Action<object>)lambda.Compile();
            }
            return null;
        }
#endif

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
            CheckDelegate<TDelegate>();
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
            CheckDelegate<TDelegate>();
            var paramsTypes = GetDelegateArguments<TDelegate>();
            var methodInfo = source.GetMethodInfo(name, paramsTypes, typeParameters, true);
            return methodInfo?.CreateDelegate<TDelegate>();
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

            if (paramsTypes == null)
            {
                paramsTypes = new Type[0];
            }
            if (!(typeof(TDelegate) == typeof(Action<object[]>)
                 || typeof(TDelegate) == typeof(Func<object[], object>)))
            {
                throw new ArgumentException("This method only accepts delegates of types " +
                    typeof(Action<object[]>).FullName + " or " +
                    typeof(Func<object[], object>).FullName + ".");
            }
            var methodInfo = source.GetMethodInfo(name, paramsTypes, typeParams, true);
            if (methodInfo == null)
            {
                return null;
            }
            var argsArray = Expression.Parameter(typeof(object[]), "args");
            var paramsExpression = new Expression[paramsTypes.Length];
            for (var i = 0; i < paramsTypes.Length; i++)
            {
                var argType = paramsTypes[i];
                paramsExpression[i] =
                    Expression.Convert(Expression.ArrayIndex(argsArray, Expression.Constant(i)), argType);
            }
            Expression returnExpression = Expression.Call(methodInfo, paramsExpression);
            if (methodInfo.ReturnType != typeof(void) && !methodInfo.ReturnType.IsClassType())
            {
                returnExpression = Expression.Convert(returnExpression, typeof(object));
            }
            CheckDelegateReturnType<TDelegate>(methodInfo);
            return Expression.Lambda<TDelegate>(returnExpression, argsArray).Compile();
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
            var propertyInfo = source.GetPropertyInfo(propertyName, true);
            return propertyInfo?.GetMethod?.CreateDelegate<Func<TProperty>>();
        }

        public static Func<object> StaticPropertyGet(this Type source, string propertyName)
        {
            var propertyInfo = source.GetPropertyInfo(propertyName, true);
            if (propertyInfo?.GetMethod == null)
            {
                return null;
            }
            Expression returnExpression = Expression.Call(propertyInfo.GetMethod);
            if (!propertyInfo.PropertyType.IsClassType())
            {
                returnExpression = Expression.Convert(returnExpression, typeof(object));
            }
            return Expression.Lambda<Func<object>>(returnExpression).Compile();
        }

#if !NET35
        [Obsolete]
        public static Func<TProperty> StaticPropertyGetExpr<TProperty>(this Type source, string propertyName)
        {
            var lambda = Expression.Lambda(Expression.Property(null, source, propertyName));
            return (Func<TProperty>)lambda.Compile();
        }
#endif

        public static Action<TProperty> StaticPropertySet<TSource, TProperty>(string propertyName)
        {
            return typeof(TSource).StaticPropertySet<TProperty>(propertyName);
        }

        public static Action<TProperty> StaticPropertySet<TProperty>(this Type source, string propertyName)
        {
            var propertyInfo = source.GetPropertyInfo(propertyName, true);
            return propertyInfo?.SetMethod?.CreateDelegate<Action<TProperty>>();
        }

        public static Action<object> StaticPropertySet(this Type source, string propertyName)
        {
            var propertyInfo = source.GetPropertyInfo(propertyName, true);
            if (propertyInfo?.SetMethod == null)
            {
                return null;
            }
            var valueParam = Expression.Parameter(typeof(object), "value");
            var convertedValue = Expression.Convert(valueParam, propertyInfo.PropertyType);
            Expression returnExpression = Expression.Call(propertyInfo.SetMethod, convertedValue);
            return (Action<object>)Expression.Lambda(returnExpression, valueParam).Compile();
        }

        private static Action<object, EventHandler<TEventArgs>> EventAccessor<TEventArgs>
            (Type source, string eventName, string accessorName)
#if NET35||NET4||PORTABLE
            where TEventArgs : EventArgs
#endif
        {
            var accessor = source.GetEventAccessor(eventName, accessorName);
            if (accessor != null)
            {
                var instanceParameter = Expression.Parameter(typeof(object), "source");
                var delegateTypeParameter = Expression.Parameter(typeof(EventHandler<TEventArgs>), "delegate");
                var lambda = Expression.Lambda(Expression.Call(Expression.Convert(instanceParameter, source),
                        accessor, delegateTypeParameter),
                    instanceParameter, delegateTypeParameter);
                return (Action<object, EventHandler<TEventArgs>>)lambda.Compile();
            }
            return null;
        }

        private static Action<TSource, EventHandler<TEventArgs>> EventAccessor<TSource, TEventArgs>
            (string eventName, string accessorName)
#if NET35||NET4||PORTABLE
            where TEventArgs : EventArgs
#endif
        {
            var sourceType = typeof(TSource);
            var accessor = sourceType.GetEventAccessor(eventName, accessorName);
            if (accessor != null)
            {
                accessor.IsEventArgsTypeCorrect<EventHandler<TEventArgs>>();
                return accessor.CreateDelegate<Action<TSource, EventHandler<TEventArgs>>>();
            }
            return null;
        }

        private static TDelegate EventAccessorImpl<TDelegate>(Type source, string eventName, string accessorName)
            where TDelegate : class
        {
            var eventInfo = source.GetEventInfo(eventName);
            if (eventInfo != null)
            {
                var accessor = accessorName == TypeExtensions.AddAccessor ? eventInfo.AddMethod : eventInfo.RemoveMethod;
                var eventArgsType = eventInfo.EventHandlerType.GenericTypeArguments()[0];
                var instanceParameter = Expression.Parameter(typeof(object), "source");
                var delegateTypeParameter = Expression.Parameter(typeof(object), "delegate");
                var methodCallExpression =
                    Expression.Call(EventHandlerFactoryMethodInfo.MakeGenericMethod(eventArgsType, source),
                        delegateTypeParameter, Expression.Constant(accessorName == TypeExtensions.RemoveAccessor));
                var lambda = Expression.Lambda<TDelegate>(Expression.Call(Expression.Convert(instanceParameter, source),
                        accessor, methodCallExpression),
                    instanceParameter, delegateTypeParameter);
                return lambda.Compile();
            }
            return null;
        }

        private static TDelegate EventAddImpl<TDelegate>(this Type source, string eventName)
            where TDelegate : class
        {
            return EventAccessorImpl<TDelegate>(source, eventName, TypeExtensions.AddAccessor);
        }

        private static TDelegate EventRemoveImpl<TDelegate>(this Type source, string eventName)
            where TDelegate : class
        {
            return EventAccessorImpl<TDelegate>(source, eventName, TypeExtensions.RemoveAccessor);
        }

        private static Func<TSource, TProperty> PropertyGet<TSource, TProperty>(string propertyName = null,
            PropertyInfo propertyInfo = null)
            where TSource : class
        {
            var source = typeof(TSource);
#if NET35 || NET4 || PORTABLE
            var cpropertyInfo = propertyInfo != null ? new CPropertyInfo(propertyInfo)
                : source.GetPropertyInfo(propertyName, false);
            return cpropertyInfo.GetMethod?.CreateDelegate<Func<TSource, TProperty>>();
#else
            propertyInfo = propertyInfo ?? source.GetPropertyInfo(propertyName, false);
            return propertyInfo?.GetMethod?.CreateDelegate<Func<TSource, TProperty>>();
#endif
        }

        public static StructGetFunc<TSource, TProperty> PropertyGetStruct<TSource, TProperty>(
            string propertyName = null)
            where TSource : struct
        {
            var source = typeof(TSource);
            var propertyInfo = source.GetPropertyInfo(propertyName, false);
            return
                propertyInfo?.GetMethod?.CreateDelegate<StructGetFunc<TSource, TProperty>>();
        }

        private static TDelegate PropertyGetImpl<TDelegate>(this Type source, string propertyName)
            where TDelegate : class
        {
            var propertyInfo = source.GetPropertyInfo(propertyName, false);
            if (propertyInfo?.GetMethod == null)
            {
                return null;
            }
            var sourceObjectParam = Expression.Parameter(typeof(object), "source");
            Expression returnExpression =
                Expression.Call(Expression.Convert(sourceObjectParam, source), propertyInfo.GetMethod);
            if (!propertyInfo.PropertyType.IsClassType())
            {
                returnExpression = Expression.Convert(returnExpression, GetDelegateReturnType<TDelegate>());
            }
            return Expression.Lambda<TDelegate>(returnExpression, sourceObjectParam).Compile();
        }
    }
}