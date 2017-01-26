using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Delegates.Extensions;
using static Delegates.Helper.DelegateHelper;

namespace Delegates
{
    /// <summary>
    /// Creates delegates for types members
    /// </summary>
    public static partial class DelegateFactory
    {
        /// <summary>
        /// Creates delegate for non-void instance method with unspecified number of parameters passed as array of 
        /// objects from instance as object
        /// </summary>
        /// <param name="source">Type with defined method</param>
        /// <param name="name">Name of method</param>
        /// <param name="paramsTypes">Types of parameters</param>
        /// <returns>Delegate for non-void instance method</returns>
        public static Func<object, object[], object> InstanceMethod(this Type source,
              string name, params Type[] paramsTypes)
        {
            return InstanceGenericMethod<Func<object, object[], object>>(source, name, null, paramsTypes);
        }

        /// <summary>
        /// Creates delegate for generic instance method with single type parameter
        /// </summary>
        /// <typeparam name="TDelegate">Delegate compatible with instance method signature</typeparam>
        /// <typeparam name="TParam1">Instance method type parameter</typeparam>
        /// <param name="name">Name of method</param>
        /// <returns>Delegate for instance method</returns>
        public static TDelegate InstanceMethod<TDelegate, TParam1>(string name)
            where TDelegate : class
        {
            return InstanceMethod<TDelegate>(name, typeof(TParam1));
        }

        /// <summary>
        /// Creates delegate for generic instance method with two type parameters
        /// </summary>
        /// <typeparam name="TDelegate">Delegate compatible with instance method signature</typeparam>
        /// <typeparam name="TParam1">First instance method type parameter</typeparam>
        /// <typeparam name="TParam2">Second instance method type parameter</typeparam>
        /// <param name="name">Name of method</param>
        /// <returns>Delegate for instance method</returns>
        public static TDelegate InstanceMethod<TDelegate, TParam1, TParam2>(string name)
            where TDelegate : class
        {
            return InstanceMethod<TDelegate>(name, typeof(TParam1), typeof(TParam2));
        }

        /// <summary>
        /// Creates delegate for generic instance method with three type parameters
        /// </summary>
        /// <typeparam name="TDelegate">Delegate compatible with instance method signature</typeparam>
        /// <typeparam name="TParam1">First instance method type parameter</typeparam>
        /// <typeparam name="TParam2">Second instance method type parameter</typeparam>
        /// <typeparam name="TParam3">Third instance method type parameter</typeparam>
        /// <param name="name">Name of method</param>
        /// <returns>Delegate for instance method</returns>
        public static TDelegate InstanceMethod<TDelegate, TParam1, TParam2, TParam3>(string name)
            where TDelegate : class
        {
            return InstanceMethod<TDelegate>(name, typeof(TParam1), typeof(TParam2), typeof(TParam3));
        }

        /// <summary>
        /// Creates delegate for (generic) instance method
        /// </summary>
        /// <typeparam name="TDelegate">Delegate compatible with instance method signature</typeparam>
        /// <param name="name">Name of method</param>
        /// <param name="typeParameters">Collection of type parameters for generic method</param>
        /// <returns>Delegate for instance method</returns>
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

        /// <summary>
        /// Creates delegate for generic instance method with single type parameter
        /// </summary>
        /// <typeparam name="TDelegate">
        /// Delegate compatible with instance method signature or with object as source
        /// </typeparam>
        /// <typeparam name="TParam1">Type parameter of generic method</typeparam>
        /// <param name="source">Type with defined method</param>
        /// <param name="name">Name of method</param>
        /// <returns>Delegate for generic instance method</returns>
        public static TDelegate InstanceMethod<TDelegate, TParam1>(this Type source, string name)
            where TDelegate : class
        {
            return source.InstanceMethod<TDelegate>(name, new[] { typeof(TParam1) });
        }

        /// <summary>
        /// Creates delegate for generic instance method with two type parameters
        /// </summary>
        /// <typeparam name="TDelegate">
        /// Delegate compatible with instance method signature or with object as source
        /// </typeparam>
        /// <typeparam name="TParam1">First type parameter of generic method</typeparam>
        /// <typeparam name="TParam2">Second type parameter of generic method</typeparam>
        /// <param name="source">Type with defined method</param>
        /// <param name="name">Name of method</param>
        /// <returns>Delegate for generic instance method</returns>
        public static TDelegate InstanceMethod<TDelegate, TParam1, TParam2>(this Type source, string name)
            where TDelegate : class
        {
            return source.InstanceMethod<TDelegate>(name, new[] { typeof(TParam1), typeof(TParam2) });
        }

        /// <summary>
        /// Creates delegate for generic instance method with three type parameters
        /// </summary>
        /// <typeparam name="TDelegate">
        /// Delegate compatible with instance method signature or with object as source
        /// </typeparam>
        /// <typeparam name="TParam1">First type parameter of generic method</typeparam>
        /// <typeparam name="TParam2">Second type parameter of generic method</typeparam>
        /// <typeparam name="TParam3">Third type parameter of generic method</typeparam>
        /// <param name="source">Type with defined method</param>
        /// <param name="name">Name of method</param>
        /// <returns>Delegate for generic instance method</returns>
        public static TDelegate InstanceMethod<TDelegate, TParam1, TParam2, TParam3>(this Type source, string name)
            where TDelegate : class
        {
            return source.InstanceMethod<TDelegate>(name, new[] { typeof(TParam1), typeof(TParam2), typeof(TParam3) });
        }

        /// <summary>
        /// Creates delegate for (generic) instance method with three type parameters
        /// </summary>
        /// <typeparam name="TDelegate">
        /// Delegate compatible with instance method signature or with object as source
        /// </typeparam>
        /// <param name="source">Type with defined method</param>
        /// <param name="name">Name of method</param>
        /// <param name="typeParams">Collection of type parameters for generic method</param>
        /// <returns>Delegate for (generic) instance method</returns>
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
                var lamdaParams = expressions.GetLambdaExprParams(sourceParameter);
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
        /// <summary>
        /// Obsolete
        /// </summary>
        /// <typeparam name="TDelegate"></typeparam>
        /// <param name="methodName"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Creates delegate for void instance method with unspecified number of parameters passed as array of 
        /// objects from instance as object
        /// </summary>
        /// <param name="source">Type with defined method</param>
        /// <param name="name">Name of method</param>
        /// <param name="paramsTypes">Types of parameters</param>
        /// <returns>Delegate for void instance method</returns>
        public static Action<object, object[]> InstanceMethodVoid(this Type source, string name, params Type[] paramsTypes)
        {
            return InstanceGenericMethod<Action<object, object[]>>(source, name, null, paramsTypes);
        }
    }
}