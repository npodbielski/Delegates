using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Cryptography;

namespace Delegates.Extensions
{
    public static class DelegateFactory
    {
        private const string Item = "Item";

        private static readonly MethodInfo EventHandlerFactoryWithKnownSourceMethodInfo =
            typeof(DelegateFactory).GetMethod("EventHandlerFactoryWithKnownSource");

        private static readonly MethodInfo EventHandlerFactoryWithObjectSourceMethodInfo =
            typeof(DelegateFactory).GetMethod("EventHandlerFactoryWithObjectSource");

        private static readonly Dictionary<WeakReference<object>, WeakReference<object>> EventsProxies =
            new Dictionary<WeakReference<object>, WeakReference<object>>();

        public static TDelegate Contructor<TSource, TDelegate>() where TDelegate : class
        {
            return Contructor<TSource, TDelegate>(GetFuncDelegateArguments<TDelegate>());
        }

        public static TDelegate Contructor<TSource, TDelegate>(params Type[] types) where TDelegate : class
        {
            var source = typeof(TSource);
            var constructorInfo = source.GetConstructor(BindingFlags.Public, null, types, null);
            if (constructorInfo == null)
            {
                constructorInfo = source.GetConstructor(BindingFlags.NonPublic, null, types, null);
            }
            if (constructorInfo == null)
            {
                constructorInfo =
                    source.GetConstructor(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance, null,
                        types, null);
            }
            List<ParameterExpression> parameters = new List<ParameterExpression>();
            var @params = GetFuncDelegateArguments<TDelegate>();
            foreach (var type in @params)
            {
                parameters.Add(Expression.Parameter(type));
            }
            return Expression.Lambda(Expression.New(constructorInfo, parameters), parameters).Compile() as TDelegate;
        }

        public static Func<TSource> DefaultContructor<TSource>()
        {
            return Contructor<TSource, Func<TSource>>(Type.EmptyTypes);
        }

        public static Action<TSource, EventHandler<TEvent>> EventAdd<TSource, TEvent>(string eventName)
        {
            var sourceType = typeof(TSource);
            var eventInfo = sourceType.GetEvent(eventName);
            if (eventInfo == null)
            {
                eventInfo = sourceType.GetEvent(eventName, BindingFlags.NonPublic);
            }
            if (eventInfo == null)
            {
                eventInfo = sourceType.GetEvent(eventName,
                    BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            }
            return
                (Action<TSource, EventHandler<TEvent>>)
                    eventInfo.AddMethod.CreateDelegate(typeof(Action<TSource, EventHandler<TEvent>>));
        }

        public static Action<TSource, Action<TSource, object>> EventAdd<TSource>(string eventName)
        {
            var source = typeof(TSource);
            var eventInfo = source.GetEvent(eventName);
            if (eventInfo == null)
            {
                eventInfo = source.GetEvent(eventName, BindingFlags.NonPublic);
            }
            if (eventInfo == null)
            {
                eventInfo = source.GetEvent(eventName,
                    BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            }
            var eventArgsType = eventInfo.EventHandlerType.GetGenericArguments()[0];
            var delegateType = typeof(Action<,>).MakeGenericType(typeof(TSource), typeof(object));
            var instanceParameter = Expression.Parameter(source);
            var delegateTypeParameter = Expression.Parameter(delegateType);
            var methodCallExpression =
                Expression.Call(EventHandlerFactoryWithKnownSourceMethodInfo.MakeGenericMethod(eventArgsType, source),
                    delegateTypeParameter);
            var c = Expression.Lambda(Expression.Call(instanceParameter, eventInfo.AddMethod, methodCallExpression
                ), instanceParameter, delegateTypeParameter).Compile();
            return c as Action<TSource, Action<TSource, object>>;
        }

        public static Action<object, Action<object, object>> EventAdd(this Type source, string eventName)
        {
            var eventInfo = source.GetEvent(eventName);
            if (eventInfo == null)
            {
                eventInfo = source.GetEvent(eventName, BindingFlags.NonPublic);
            }
            if (eventInfo == null)
            {
                eventInfo = source.GetEvent(eventName,
                    BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            }
            var eventArgsType = eventInfo.EventHandlerType.GetGenericArguments()[0];
            var delegateType = typeof(Action<,>).MakeGenericType(typeof(object), typeof(object));
            var instanceParameter = Expression.Parameter(typeof(object));
            var delegateTypeParameter = Expression.Parameter(delegateType);
            var methodCallExpression =
                Expression.Call(EventHandlerFactoryWithObjectSourceMethodInfo.MakeGenericMethod(eventArgsType),
                    delegateTypeParameter);
            var c =
                Expression.Lambda(
                    Expression.Call(Expression.Convert(instanceParameter, source), eventInfo.AddMethod,
                        methodCallExpression
                        ), instanceParameter, delegateTypeParameter).Compile();
            return c as Action<object, Action<object, object>>;
        }

        public static EventHandler<TEventArgs> EventHandlerFactoryWithKnownSource<TEventArgs, TSource>(
            Action<TSource, object> handler)
            where TEventArgs : class
            where TSource : class
        {
            EventHandler<TEventArgs> proxy;
            object keyTarget;
            var haveKey = false;
            var kv = EventsProxies.FirstOrDefault(k =>
            {
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
                object eventHandler;
                EventsProxies[kv.Key].TryGetTarget(out eventHandler);
                proxy = (EventHandler<TEventArgs>)eventHandler;
                return proxy;
            }
            EventHandler<TEventArgs> newEventHandler = (s, a) => handler((TSource)s, a);
            EventsProxies[new WeakReference<object>(handler)] = new WeakReference<object>(newEventHandler);
            proxy = newEventHandler;
            return proxy;
        }

        public static EventHandler<TEventArgs> EventHandlerFactoryWithObjectSource<TEventArgs>(
            Action<object, object> handler)
            where TEventArgs : class
        {
            EventHandler<TEventArgs> proxy;
            object keyTarget;
            var haveKey = false;
            var kv = EventsProxies.FirstOrDefault(k =>
            {
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
                object eventHandler;
                EventsProxies[kv.Key].TryGetTarget(out eventHandler);
                proxy = (EventHandler<TEventArgs>)eventHandler;
                return proxy;
            }
            EventHandler<TEventArgs> newEventHandler = new EventHandler<TEventArgs>(handler);
            EventsProxies[new WeakReference<object>(handler)] = new WeakReference<object>(newEventHandler);
            proxy = newEventHandler;
            return proxy;
        }

        public static Action<TSource, EventHandler<TEvent>> EventRemove<TSource, TEvent>(string eventName)
        {
            var source = typeof(TSource);
            var eventInfo = source.GetEvent(eventName);
            if (eventInfo == null)
            {
                eventInfo = source.GetEvent(eventName, BindingFlags.NonPublic);
            }
            if (eventInfo == null)
            {
                eventInfo = source.GetEvent(eventName,
                    BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            }
            return
                (Action<TSource, EventHandler<TEvent>>)
                    eventInfo.RemoveMethod.CreateDelegate(typeof(Action<TSource, EventHandler<TEvent>>));
        }

        public static Action<TSource, Action<TSource, object>> EventRemove<TSource>(string eventName)
        {
            var source = typeof(TSource);
            var eventInfo = source.GetEvent(eventName);
            if (eventInfo == null)
            {
                eventInfo = source.GetEvent(eventName, BindingFlags.NonPublic);
            }
            if (eventInfo == null)
            {
                eventInfo = source.GetEvent(eventName,
                    BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            }
            var eventArgsType = eventInfo.EventHandlerType.GetGenericArguments()[0];
            var delegateType = typeof(Action<,>).MakeGenericType(typeof(TSource), typeof(object));
            var instanceParameter = Expression.Parameter(source);
            var delegateTypeParameter = Expression.Parameter(delegateType);
            var methodCallExpression =
                Expression.Call(EventHandlerFactoryWithKnownSourceMethodInfo.MakeGenericMethod(eventArgsType, source),
                    delegateTypeParameter);
            var c = Expression.Lambda(Expression.Call(instanceParameter, eventInfo.RemoveMethod, methodCallExpression
                ), instanceParameter, delegateTypeParameter).Compile();
            return c as Action<TSource, Action<TSource, object>>;
        }

        public static Action<object, Action<object, object>> EventRemove(this Type source, string eventName)
        {
            var eventInfo = source.GetEvent(eventName);
            if (eventInfo == null)
            {
                eventInfo = source.GetEvent(eventName, BindingFlags.NonPublic);
            }
            if (eventInfo == null)
            {
                eventInfo = source.GetEvent(eventName,
                    BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            }
            var eventArgsType = eventInfo.EventHandlerType.GetGenericArguments()[0];
            var delegateType = typeof(Action<,>).MakeGenericType(typeof(object), typeof(object));
            var instanceParameter = Expression.Parameter(typeof(object));
            var delegateTypeParameter = Expression.Parameter(delegateType);
            var methodCallExpression =
                Expression.Call(EventHandlerFactoryWithObjectSourceMethodInfo.MakeGenericMethod(eventArgsType),
                    delegateTypeParameter);
            var c =
                Expression.Lambda(
                    Expression.Call(Expression.Convert(instanceParameter, source), eventInfo.RemoveMethod,
                        methodCallExpression
                        ), instanceParameter, delegateTypeParameter).Compile();
            return c as Action<object, Action<object, object>>;
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

        public static Action<object, TProperty> FieldSetWithCast<TProperty>(this Type source, string fieldName)
        {
            return source.FieldSet(fieldName) as Action<object, TProperty>;
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

        public static Action<TSource, TProperty> FieldSetWithCast<TSource, TProperty>(string fieldName)
        {
            return typeof(TSource).FieldSet(fieldName) as Action<TSource, TProperty>;
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

        //public static Func<object, TIndex, TReturn> IndexerGet<TReturn, TIndex>(this Type source)
        //{
        //    Func<object, int, int> @delegate = (o, i) => ((TestClass)o)[i];
        //    var indexType = typeof(TIndex);
        //    var returnType = typeof(TReturn);
        //    var propertyInfo = GetIndexerPropertyInfo(source, returnType, new[] { indexType });
        //    var sourceObjectParam = Expression.Parameter(typeof(object));
        //    var paramExpression = Expression.Parameter(indexType);
        //    return (Func<object, TIndex, TReturn>)Expression.Lambda(
        //        Expression.Call(Expression.Convert(sourceObjectParam, source), propertyInfo.GetMethod, paramExpression), sourceObjectParam, paramExpression).Compile();
        //}

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

        public static TDelegate InstanceMethod<TDelegate>(string name)
            where TDelegate : class
        {
            var source = typeof(TDelegate).GenericTypeArguments[0];
            var intanceType = typeof(TDelegate).GenericTypeArguments.FirstOrDefault();
            if (intanceType != source)
            {
                throw new ArgumentException("Delegate type has incorect instance parameter");
            }
            var methodInfo = source.GetMethod(name);
            if (methodInfo == null)
            {
                methodInfo = source.GetMethod(name, BindingFlags.NonPublic);
            }
            if (methodInfo == null)
            {
                methodInfo = source.GetMethod(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            }
            var te = methodInfo.CreateDelegate(typeof(TDelegate)) as TDelegate;
            return te;
        }

        //public static TDelegate InstanceMethod<TDelegate>(this Type source,
        //    string name)
        //    where TDelegate : class
        //{
        //    var intanceType = typeof(TDelegate).GenericTypeArguments.FirstOrDefault();
        //    if (intanceType != source)
        //    {
        //        throw new ArgumentException("Delegate type has incorect instance parameter");
        //    }
        //    var methodInfo = source.GetMethod(name);
        //    if (methodInfo == null)
        //    {
        //        methodInfo = source.GetMethod(name, BindingFlags.NonPublic);
        //    }
        //    if (methodInfo == null)
        //    {
        //        methodInfo = source.GetMethod(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
        //    }
        //    var te = methodInfo.CreateDelegate(typeof(TDelegate)) as TDelegate;
        //    return te;
        //}
        public static TDelegate InstanceMethod2<TDelegate>(string methodName) where TDelegate : class
        {
            var source = typeof(TDelegate).GenericTypeArguments[0];
            var param = Expression.Parameter(source);
            List<ParameterExpression> parameters = new List<ParameterExpression> { param };
            var @params = GetFuncDelegateArguments<TDelegate>().Skip(1);
            foreach (var type in @params)
            {
                parameters.Add(Expression.Parameter(type));
            }
            var te = Expression.Lambda(Expression.Call(param, methodName, null,
                parameters.Skip(1).Cast<Expression>().ToArray()), parameters);
            return te.Compile() as TDelegate;
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

        public static Func<TField> StaticFieldGetExpr<TSource, TField>(string fieldName)
        {
            var source = typeof(TSource);
            var lambda = Expression.Lambda(Expression.Field(null, source, fieldName));
            return (Func<TField>)lambda.Compile();
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

        private static FieldInfo GetStaticFieldInfo(Type source, string fieldName)
        {
            var fieldInfo = (source.GetField(fieldName, BindingFlags.Static) ??
                             source.GetField(fieldName, BindingFlags.Static | BindingFlags.NonPublic)) ??
                             source.GetField(fieldName,
                                BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
            return fieldInfo;
        }

        private static FieldInfo GetFieldInfo(Type source, string fieldName)
        {
            var fieldInfo = (source.GetField(fieldName) ??
                                source.GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic)) ??
                                source.GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            return fieldInfo;
        }

        public static TDelegate StaticMethod<TDelegate>(string name)
            where TDelegate : class
        {
            return typeof(TDelegate).GenericTypeArguments[0].StaticMethod<TDelegate>(name);
        }

        public static TDelegate StaticMethod<TDelegate>(this Type source,
            string name)
            where TDelegate : class
        {
            var methodInfo = source.GetMethod(name, BindingFlags.Static);
            if (methodInfo == null)
            {
                methodInfo = source.GetMethod(name, BindingFlags.Static | BindingFlags.NonPublic);
            }
            if (methodInfo == null)
            {
                methodInfo = source.GetMethod(name, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
            }
            var te = methodInfo.CreateDelegate(typeof(TDelegate)) as TDelegate;
            return te;
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

        private static PropertyInfo GetStaticPropertyInfo(Type source, string propertyName)
        {
            var propertyInfo = (source.GetProperty(propertyName, BindingFlags.Static) ??
                                source.GetProperty(propertyName, BindingFlags.Static | BindingFlags.NonPublic)) ??
                                source.GetProperty(propertyName,
                                BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
            return propertyInfo;
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

        private static Type[] GetFuncDelegateArguments<TDelegate>() where TDelegate : class
        {
            return typeof(TDelegate).GenericTypeArguments.Reverse().Skip(1).Reverse().ToArray();
        }

        private static PropertyInfo GetPropertyInfo(Type source, string propertyName)
        {
            var propertyInfo = source.GetProperty(propertyName) ??
                                source.GetProperty(propertyName, BindingFlags.NonPublic) ??
                                source.GetProperty(propertyName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            return propertyInfo;
        }

        private static Func<TSource, TProperty> PropertyGet<TSource, TProperty>(string propertyName = null, PropertyInfo propertyInfo = null)
        {
            var source = typeof(TSource);
            propertyInfo = propertyInfo ?? GetPropertyInfo(source, propertyName);
            return (Func<TSource, TProperty>)propertyInfo?.GetMethod?.CreateDelegate(typeof(Func<TSource, TProperty>));
        }
    }

    //public static class DelegateFactory
    //{
    //    public static Func<TProperty> StaticPropertyGet<TSource, TProperty>(string methodName)
    //    {
    //        return null;
    //    }

    //    public static Func<TProperty> StaticPropertyGet<TProperty>(this Type source, string methodName)
    //    {
    //        return null;
    //    }
    //}
}