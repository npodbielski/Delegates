using System;
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
        /// Creates delegate to static method with single type parameter
        /// </summary>
        /// <typeparam name="TSource">Type with defined method</typeparam>
        /// <typeparam name="TDelegate">Delegate compatible with method</typeparam>
        /// <typeparam name="TParam1">Method type parameter</typeparam>
        /// <param name="name">Name of method</param>
        /// <returns>Delegate for generic static method</returns>
        public static TDelegate StaticMethod<TSource, TDelegate, TParam1>(string name)
            where TDelegate : class
        {
            return typeof(TSource).StaticMethod<TDelegate>(name, typeof(TParam1));
        }

        /// <summary>
        /// Creates delegate to static method with two type parameters
        /// </summary>
        /// <typeparam name="TSource">Type with defined method</typeparam>
        /// <typeparam name="TDelegate">Delegate compatible with method</typeparam>
        /// <typeparam name="TParam1">First type parameter</typeparam>
        /// <typeparam name="TParam2">Second type parameter</typeparam>
        /// <param name="name">Name of method</param>
        /// <returns>Delegate for generic static method</returns>
        public static TDelegate StaticMethod<TSource, TDelegate, TParam1, TParam2>(string name)
            where TDelegate : class
        {
            return typeof(TSource).StaticMethod<TDelegate>(name, typeof(TParam1), typeof(TParam2));
        }

        /// <summary>
        /// Creates delegate to static method with three type parameters
        /// </summary>
        /// <typeparam name="TSource">Type with defined method</typeparam>
        /// <typeparam name="TDelegate">Delegate compatible with method</typeparam>
        /// <typeparam name="TParam1">First type parameter</typeparam>
        /// <typeparam name="TParam2">Second type parameter</typeparam>
        /// <typeparam name="TParam3">Third type parameter</typeparam>
        /// <param name="name">Name of method</param>
        /// <returns>Delegate for generic static method</returns>
        public static TDelegate StaticMethod<TSource, TDelegate, TParam1, TParam2, TParam3>(string name)
            where TDelegate : class
        {
            return typeof(TSource).StaticMethod<TDelegate>(name, typeof(TParam1), typeof(TParam2), typeof(TParam3));
        }

        /// <summary>
        /// Creates delegate to static method with unspecified number of type parameters
        /// </summary>
        /// <typeparam name="TSource">Type with defined method</typeparam>
        /// <typeparam name="TDelegate">Delegate compatible with method</typeparam>
        /// <param name="name">Name of method</param>
        /// <param name="typeParameters">Type parameters for generic static method</param>
        /// <returns>Delegate for generic static method</returns>
        public static TDelegate StaticMethod<TSource, TDelegate>(string name, params Type[] typeParameters)
            where TDelegate : class
        {
            return typeof(TSource).StaticMethod<TDelegate>(name, typeParameters);
        }

        /// <summary>
        /// Creates delegate to static method with single type parameter
        /// </summary>
        /// <typeparam name="TDelegate">Delegate compatible with method</typeparam>
        /// <typeparam name="TParam1">Method type parameter</typeparam>
        /// <param name="source">Type with defined method</param>
        /// <param name="name">Name of method</param>
        /// <returns>Delegate for generic static method</returns>
        public static TDelegate StaticMethod<TDelegate, TParam1>(this Type source, string name)
            where TDelegate : class
        {
            return source.StaticMethod<TDelegate>(name, typeof(TParam1));
        }

        /// <summary>
        /// Creates delegate to static method with single type parameter
        /// </summary>
        /// <typeparam name="TDelegate">Delegate compatible with method</typeparam>
        /// <typeparam name="TParam1">First type parameter</typeparam>
        /// <typeparam name="TParam2">Second type parameter</typeparam>
        /// <param name="source">Type with defined method</param>
        /// <param name="name">Name of method</param>
        /// <returns>Delegate for generic static method</returns>
        public static TDelegate StaticMethod<TDelegate, TParam1, TParam2>(this Type source, string name)
            where TDelegate : class
        {
            return source.StaticMethod<TDelegate>(name, typeof(TParam1), typeof(TParam2));
        }

        /// <summary>
        /// Creates delegate to static method with single type parameter
        /// </summary>
        /// <typeparam name="TDelegate">Delegate compatible with method</typeparam>
        /// <typeparam name="TParam1">First type parameter</typeparam>
        /// <typeparam name="TParam2">Second type parameter</typeparam>
        /// <typeparam name="TParam3">Third type parameter</typeparam>
        /// <param name="source">Type with defined method</param>
        /// <param name="name">Name of method</param>
        /// <returns>Delegate for generic static method</returns>
        public static TDelegate StaticMethod<TDelegate, TParam1, TParam2, TParam3>(this Type source, string name)
            where TDelegate : class
        {
            return source.StaticMethod<TDelegate>(name, typeof(TParam1), typeof(TParam2), typeof(TParam3));
        }

        /// <summary>
        /// Creates delegate to static method with unspecified number of type parameters
        /// </summary>
        /// <typeparam name="TDelegate">Delegate compatible with method</typeparam>
        /// <param name="source">Type with defined method</param>
        /// <param name="name">Name of method</param>
        /// <param name="typeParameters">Type parameters for generic static method</param>
        /// <returns>Delegate for generic static method</returns>
        public static TDelegate StaticMethod<TDelegate>(this Type source, string name, params Type[] typeParameters)
            where TDelegate : class
        {
            CheckDelegate<TDelegate>();
            var paramsTypes = GetDelegateArguments<TDelegate>();
            var methodInfo = source.GetMethodInfo(name, paramsTypes, typeParameters, true);
            return methodInfo?.CreateDelegate<TDelegate>();
        }

        /// <summary>
        /// Creates delegate to non-void generic static method with unspecified number of parameters passed as array 
        /// of objects
        /// </summary>
        /// <param name="source">Type with defined method</param>
        /// <param name="name">Name of method</param>
        /// <param name="paramsTypes">Types of parameters</param>
        /// <param name="typeParams">Type parameters for generic static method</param>
        /// <returns>Delegate for non-void generic static method</returns>
        public static Func<object[], object> StaticGenericMethod(this Type source,
         string name, Type[] paramsTypes, Type[] typeParams)
        {
            return StaticMethod<Func<object[], object>>(source, name, typeParams, paramsTypes);
        }

        /// <summary>
        /// Creates delegate to void generic static method with unspecified number of parameters passed as array 
        /// of objects
        /// </summary>
        /// <param name="source">Type with defined method</param>
        /// <param name="name">Name of method</param>
        /// <param name="paramsTypes">Types of parameters</param>
        /// <param name="typeParams">Type parameters for generic static method</param>
        /// <returns>Delegate for void generic static method</returns>
        public static Action<object[]> StaticGenericMethodVoid(this Type source,
            string name, Type[] paramsTypes, Type[] typeParams)
        {
            return StaticMethod<Action<object[]>>(source, name, typeParams, paramsTypes);
        }
    }
}