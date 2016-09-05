using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Delegates.Extensions
{
    public static class DelegateFactory
    {
        private const string AddAccessor = "add";
        private const string Item = "Item";

        private const string RemoveAccessor = "remove";
        private static readonly MethodInfo EventHandlerFactoryMethodInfo = typeof(DelegateFactory).GetMethod("EventHandlerFactory");

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
            var propertyInfo = GetIndexerPropertyInfo(source, returnType, indexTypes);
            if (propertyInfo?.GetMethod == null)
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
                        Expression.Call(Expression.Convert(sourceObjectParam, source), propertyInfo.GetMethod, paramsExpression),
                        new[] { sourceObjectParam }.Concat(paramsExpression)).Compile();
        }

        public static Delegate DelegateIndexerSet(Type source, Type returnType,
            params Type[] indexTypes)
        {
            var propertyInfo = GetIndexerPropertyInfo(source, returnType, indexTypes);
            if (propertyInfo?.SetMethod == null)
            {
                return null;
            }
            var sourceObjectParam = Expression.Parameter(typeof(object));
            var valueParam = Expression.Parameter(returnType);
            var indexExpressions = new ParameterExpression[indexTypes.Length];
            for (var i = 0; i < indexTypes.Length; i++)
            {
                var indexType = indexTypes[i];
                indexExpressions[i] = Expression.Parameter(indexType);
            }
            var callArgs = indexExpressions.Concat(new[] { valueParam }).ToArray();
            var paramsExpressions = new[] { sourceObjectParam }.Concat(callArgs);
            return Expression.Lambda(
                        Expression.Call(Expression.Convert(sourceObjectParam, source),
                            propertyInfo.SetMethod, callArgs), paramsExpressions).Compile();
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
            var source = typeof(TSource);
            return source.FieldGet(fieldName) as Func<TSource, TField>;
        }

        public static Func<object, TField> FieldGet<TField>(this Type source,
            string fieldName)
        {
            return source.FieldGet(fieldName) as Func<object, TField>;
        }

        public static Func<object, object> FieldGet(this Type source, string fieldName)
        {
            var fieldInfo = GetFieldInfo(source, fieldName);
            if (fieldInfo != null)
            {
                var sourceParam = Expression.Parameter(typeof(object));
                Expression returnExpression = Expression.Field(Expression.Convert(sourceParam, source), fieldInfo);
                if (!fieldInfo.FieldType.IsClass)
                {
                    returnExpression = Expression.Convert(returnExpression, typeof(object));
                }
                var lambda = Expression.Lambda(returnExpression, sourceParam);
                return (Func<object, object>)lambda.Compile();
            }
            return null;
        }

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

        public static Action<object, TProperty> FieldSet<TProperty>(this Type source, string fieldName)
        {
            var fieldInfo = GetFieldInfo(source, fieldName);
            if (fieldInfo != null && !fieldInfo.IsInitOnly)
            {
                var sourceParam = Expression.Parameter(typeof(object));
                var valueParam = Expression.Parameter(typeof(TProperty));
                var te = Expression.Lambda(typeof(Action<object, TProperty>),
                    Expression.Assign(Expression.Field(Expression.Convert(sourceParam, source), fieldInfo), valueParam),
                    sourceParam, valueParam);
                return (Action<object, TProperty>)te.Compile();
            }
            return null;
        }

        public static Action<TSource, TProperty> FieldSet<TSource, TProperty>(string fieldName)
        {
            var source = typeof(TSource);
            var fieldInfo = GetFieldInfo(source, fieldName);
            if (fieldInfo != null && !fieldInfo.IsInitOnly)
            {
                var sourceParam = Expression.Parameter(source);
                var valueParam = Expression.Parameter(typeof(TProperty));
                var te = Expression.Lambda(typeof(Action<TSource, TProperty>),
                    Expression.Assign(Expression.Field(sourceParam, fieldInfo), valueParam),
                    sourceParam, valueParam);
                return (Action<TSource, TProperty>)te.Compile();
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
                Expression returnExpression = Expression.Assign(Expression.Field(Expression.Convert(sourceParam, source), fieldInfo), convertedValueExpr);
                if (!fieldInfo.FieldType.IsClass)
                {
                    returnExpression = Expression.Convert(returnExpression, typeof(object));
                }
                var lambda = Expression.Lambda(typeof(Action<object, object>),
                    returnExpression, sourceParam, valueParam);
                return (Action<object, object>)lambda.Compile();
            }
            return null;
        }

        public static Action<object, TProperty> FieldSetWithCast<TProperty>(this Type source, string fieldName)
        {
            return source.FieldSet(fieldName) as Action<object, TProperty>;
        }

        public static Action<TSource, TProperty> FieldSetWithCast<TSource, TProperty>(string fieldName)
        {
            return typeof(TSource).FieldSet(fieldName) as Action<TSource, TProperty>;
        }

        public static Func<TSource, TIndex, TReturn> IndexerGet<TSource, TReturn, TIndex>()
        {
            var propertyInfo = GetIndexerPropertyInfo(typeof(TSource), typeof(TReturn), new[] { typeof(TIndex) });
            return (Func<TSource, TIndex, TReturn>)
                    propertyInfo?.GetMethod?.CreateDelegate(typeof(Func<TSource, TIndex, TReturn>));
        }

        public static Func<TSource, TIndex, TIndex2, TReturn> IndexerGet<TSource, TReturn, TIndex, TIndex2>()
        {
            var propertyInfo = GetIndexerPropertyInfo(typeof(TSource), typeof(TReturn), new[] { typeof(TIndex), typeof(TIndex2) });
            return (Func<TSource, TIndex, TIndex2, TReturn>)
                    propertyInfo?.GetMethod?.CreateDelegate(typeof(Func<TSource, TIndex, TIndex2, TReturn>));
        }

        public static Func<TSource, TIndex, TIndex2, TIndex2, TReturn> IndexerGet<TSource, TReturn, TIndex, TIndex2, TIndex3>()
        {
            var propertyInfo = GetIndexerPropertyInfo(typeof(TSource), typeof(TReturn), new[] { typeof(TIndex), typeof(TIndex2), typeof(TIndex3) });
            return (Func<TSource, TIndex, TIndex2, TIndex2, TReturn>)
                    propertyInfo?.GetMethod?.CreateDelegate(typeof(Func<TSource, TIndex, TIndex2, TIndex2, TReturn>));
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
            return (Func<object, TIndex, TIndex2, TReturn>)DelegateIndexerGet(source, typeof(TReturn), indexType, indexType2);
        }

        public static Func<object, TIndex, TIndex2, TIndex3, TReturn> IndexerGet<TReturn, TIndex, TIndex2, TIndex3>(this Type source)
        {
            var indexType = typeof(TIndex);
            var indexType2 = typeof(TIndex2);
            var indexType3 = typeof(TIndex3);
            return (Func<object, TIndex, TIndex2, TIndex3, TReturn>)DelegateIndexerGet(source, typeof(TReturn), indexType, indexType2, indexType3);
        }

        public static Func<object, object[], object> IndexerGet(this Type source, Type returnType, params Type[] indexTypes)
        {
            var propertyInfo = GetIndexerPropertyInfo(source, returnType, indexTypes);
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
                paramsExpression[i] = Expression.Convert(Expression.ArrayIndex(indexesParam, Expression.Constant(i)), indexType);
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
            var propertyInfo = GetIndexerPropertyInfo(source, returnType, new[] { indexType });
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
            return (Action<object, TIndex, TReturn>)DelegateIndexerSet(source, typeof(TReturn), indexType);
        }

        public static Action<object, TIndex, TIndex2, TReturn> IndexerSet<TReturn, TIndex, TIndex2>(this Type source)
        {
            var indexType = typeof(TIndex);
            var indexType2 = typeof(TIndex2);
            return (Action<object, TIndex, TIndex2, TReturn>)DelegateIndexerSet(source, typeof(TReturn), indexType, indexType2);
        }

        public static Action<object, TIndex, TIndex2, TIndex3, TReturn> IndexerSet<TReturn, TIndex, TIndex2, TIndex3>(this Type source)
        {
            var indexType = typeof(TIndex);
            var indexType2 = typeof(TIndex2);
            var indexType3 = typeof(TIndex3);
            return (Action<object, TIndex, TIndex2, TIndex3, TReturn>)DelegateIndexerSet(source, typeof(TReturn), indexType, indexType2, indexType3);
        }

        public static Action<TSource, TIndex, TProperty> IndexerSet<TSource, TIndex, TProperty>()
        {
            var sourceType = typeof(TSource);
            var propertyInfo = GetIndexerPropertyInfo(sourceType, typeof(TProperty), new[] { typeof(TIndex) });
            return (Action<TSource, TIndex, TProperty>)
                propertyInfo?.SetMethod?.CreateDelegate(typeof(Action<TSource, TIndex, TProperty>));
        }

        public static Action<TSource, TIndex, TIndex2, TReturn> IndexerSet<TSource, TReturn, TIndex, TIndex2>()
        {
            var propertyInfo = GetIndexerPropertyInfo(typeof(TSource), typeof(TReturn), new[] { typeof(TIndex), typeof(TIndex2) });
            return (Action<TSource, TIndex, TIndex2, TReturn>)
                    propertyInfo?.SetMethod?.CreateDelegate(typeof(Action<TSource, TIndex, TIndex2, TReturn>));
        }

        public static Action<TSource, TIndex, TIndex2, TIndex2, TReturn> IndexerSet<TSource, TReturn, TIndex, TIndex2, TIndex3>()
        {
            var propertyInfo = GetIndexerPropertyInfo(typeof(TSource), typeof(TReturn), new[] { typeof(TIndex), typeof(TIndex2), typeof(TIndex3) });
            return (Action<TSource, TIndex, TIndex2, TIndex2, TReturn>)
                    propertyInfo?.SetMethod?.CreateDelegate(
                        typeof(Action<TSource, TIndex, TIndex2, TIndex2, TReturn>));
        }

        public static Action<object, object[], object> IndexerSet(this Type source, Type returnType, params Type[] indexTypes)
        {
            var propertyInfo = GetIndexerPropertyInfo(source, returnType, indexTypes);
            if (propertyInfo?.SetMethod == null)
            {
                return null;
            }
            var sourceObjectParam = Expression.Parameter(typeof(object));
            var indexesParam = Expression.Parameter(typeof(object[]));
            var valueParam = Expression.Parameter(typeof(object));
            var paramsExpression = new Expression[indexTypes.Length + 1];
            for (var i = 0; i < indexTypes.Length; i++)
            {
                var indexType = indexTypes[i];
                paramsExpression[i] = Expression.Convert(Expression.ArrayIndex(indexesParam, Expression.Constant(i)), indexType);
            }
            paramsExpression[indexTypes.Length] = Expression.Convert(valueParam, returnType);
            Expression returnExpression =
                Expression.Call(Expression.Convert(sourceObjectParam, source), propertyInfo.SetMethod, paramsExpression);
            return (Action<object, object[], object>)Expression.Lambda(
                returnExpression, sourceObjectParam, indexesParam, valueParam).Compile();
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

        public static TDelegate InstanceMethod2<TDelegate>(string methodName) where TDelegate : class
        {
            var source = typeof(TDelegate).GenericTypeArguments[0];
            var param = Expression.Parameter(source);
            var parameters = new List<ParameterExpression> { param };
            var @params = GetFuncDelegateArguments<TDelegate>().Skip(1);
            foreach (var type in @params)
            {
                parameters.Add(Expression.Parameter(type));
            }
            var te = Expression.Lambda(Expression.Call(param, methodName, null,
                parameters.Skip(1).Cast<Expression>().ToArray()), parameters);
            return te.Compile() as TDelegate;
        }

        public static Action<object, object[]> InstanceMethodVoid(this Type source,
                    string name, params Type[] paramsTypes)
        {
            return InstanceGenericMethod<Action<object, object[]>>(source, name, null, paramsTypes);
        }

        public static Func<object, TProperty> PropertyGet<TProperty>(this Type source, string propertyName)
        {
            return source.PropertyGet(propertyName) as Func<object, TProperty>;
        }

        public static Func<object, object> PropertyGet(this Type source, string propertyName)
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
                returnExpression = Expression.Convert(returnExpression, typeof(object));
            }
            return (Func<object, object>)Expression.Lambda(returnExpression, sourceObjectParam).Compile();
        }

        public static Func<TSource, TProperty> PropertyGet<TSource, TProperty>(PropertyInfo propertyInfo)
        {
            return PropertyGet<TSource, TProperty>(null, propertyInfo);
        }

        public static Func<TSource, TProperty> PropertyGet<TSource, TProperty>(string propertyName)
        {
            return PropertyGet<TSource, TProperty>(propertyName, null);
        }

        public static Func<TSource, TProperty> PropertyGet2<TSource, TProperty>(this Type source,
            string propertyName)
        {
            var p = Expression.Parameter(source);
            var te = Expression.Lambda(Expression.Property(p, propertyName), p);
            return (Func<TSource, TProperty>)te.Compile();
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
            return (Action<object, TProperty>)Expression.Lambda(Expression.Call(Expression.Convert(sourceObjectParam, source), propertyInfo.SetMethod, valueExpression), sourceObjectParam, propertyValueParam).Compile();
        }

        public static Action<object, object> PropertySet(this Type source, string propertyName)
        {
            return source.PropertySet<object>(propertyName);
        }

        public static Action<TSource, TProperty> PropertySet<TSource, TProperty>(string propertyName)
        {
            var source = typeof(TSource);
            var propertyInfo = GetPropertyInfo(source, propertyName);
            return (Action<TSource, TProperty>)propertyInfo?.SetMethod?.CreateDelegate(typeof(Action<TSource, TProperty>));
        }

        public static Action<EventHandler<TEvent>> StaticEventAdd<TEvent>(this Type source, string eventName)
        {
            var eventInfo = source.GetEvent(eventName);
            if (eventInfo == null)
            {
                eventInfo = source.GetEvent(eventName, BindingFlags.NonPublic);
            }
            if (eventInfo == null)
            {
                eventInfo = source.GetEvent(eventName, BindingFlags.NonPublic | BindingFlags.Public);
            }
            return
                (Action<EventHandler<TEvent>>)eventInfo.AddMethod.CreateDelegate(typeof(Action<EventHandler<TEvent>>));
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
            if (fieldInfo != null)
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

        public static Func<TProperty> StaticPropertyGet2<TProperty>(this Type source, string propertyName)
        {
            var te = Expression.Lambda(Expression.Property(null, source, propertyName));
            return (Func<TProperty>)te.Compile();
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
            if (propertyInfo?.GetMethod == null)
            {
                return null;
            }
            var valueParam = Expression.Parameter(typeof(object));
            var convertedValue = Expression.Convert(valueParam, propertyInfo.PropertyType);
            Expression returnExpression = Expression.Call(propertyInfo.SetMethod, convertedValue);
            if (!propertyInfo.PropertyType.IsClass)
            {
                returnExpression = Expression.Convert(returnExpression, typeof(object));
            }
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
                                source.GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
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

        private static PropertyInfo GetIndexerPropertyInfo(Type source, Type returnType, Type[] indexesTypes,
                            string indexerName = null)
        {
            indexerName = indexerName ?? Item;

            var propertyInfo = (source.GetProperty(indexerName, returnType, indexesTypes) ??
                                source.GetProperty(indexerName, BindingFlags.NonPublic, null,
                                    returnType, indexesTypes, null)) ??
                                source.GetProperty(indexerName,
                                    BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance,
                                    null, returnType, indexesTypes, null);
            if (propertyInfo != null)
            {
                return propertyInfo;
            }
            var indexer = source.GetProperties().FirstOrDefault(p => p.GetIndexParameters().Length > 0);
            return indexer != null ? GetIndexerPropertyInfo(source, returnType, indexesTypes, indexer.Name) : null;
        }

        private static MethodInfo GetMethodInfo(Type source, string name, Type[] parametersTypes, Type[] typeParameters = null)
        {
            MethodInfo methodInfo = null;
            try
            {
                methodInfo = (source.GetMethod(name, BindingFlags.Instance | BindingFlags.Public, null, parametersTypes, null) ??
                                source.GetMethod(name, BindingFlags.Instance | BindingFlags.NonPublic, null, parametersTypes, null)) ??
                                source.GetMethod(name, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public, null, parametersTypes, null);
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
                                source.GetProperty(propertyName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
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

        private static MethodInfo GetStaticMethodInfo(Type source, string name, Type[] parametersTypes, Type[] typeParameters = null)
        {
            MethodInfo methodInfo = null;
            try
            {
                methodInfo = (source.GetMethod(name, BindingFlags.Static, null, parametersTypes, null) ??
                                source.GetMethod(name, BindingFlags.Static | BindingFlags.NonPublic, null, parametersTypes, null)) ??
                                source.GetMethod(name, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public, null, parametersTypes, null);
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

        private static Func<TSource, TProperty> PropertyGet<TSource, TProperty>(string propertyName = null, PropertyInfo propertyInfo = null)
        {
            var source = typeof(TSource);
            propertyInfo = propertyInfo ?? GetPropertyInfo(source, propertyName);
            return (Func<TSource, TProperty>)propertyInfo?.GetMethod?.CreateDelegate(typeof(Func<TSource, TProperty>));
        }
    }
}