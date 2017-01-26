using System;
using System.Linq.Expressions;
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
        /// Creates delegate to static method
        /// </summary>
        /// <typeparam name="TSource">Type with defined method</typeparam>
        /// <typeparam name="TDelegate">Delegate compatible with method</typeparam>
        /// <param name="name">Name of method</param>
        /// <returns>Delegate for static method</returns>
        public static TDelegate StaticMethod<TSource, TDelegate>(string name)
               where TDelegate : class
        {
            CheckDelegate<TDelegate>();
            return typeof(TSource).StaticMethod<TDelegate>(name);
        }

        /// <summary>
        /// Creates delegate to static method
        /// </summary>
        /// <typeparam name="TDelegate">Delegate compatible with method</typeparam>
        /// <param name="source">Type with defined method</param>
        /// <param name="name">Name of method</param>
        /// <returns>Delegate for static method</returns>
        public static TDelegate StaticMethod<TDelegate>(this Type source, string name)
            where TDelegate : class
        {
            return source.StaticMethod<TDelegate>(name, null);
        }

        /// <summary>
        /// Creates delegate to non-void static method with unspecified number of parameters passed as array 
        /// of objects
        /// </summary>
        /// <param name="source">Type with defined method</param>
        /// <param name="name">Name of method</param>
        /// <param name="paramsTypes">Types of parameters</param>
        /// <returns>Delegate for static method</returns>
        public static Func<object[], object> StaticMethod(this Type source,
            string name, params Type[] paramsTypes)
        {
            return StaticMethod<Func<object[], object>>(source, name, null, paramsTypes);
        }

        /// <summary>
        /// Creates delegate to void static method with unspecified number of parameters passed as array of objects
        /// </summary>
        /// <param name="source">Type with defined method</param>
        /// <param name="name">Name of method</param>
        /// <param name="paramsTypes">Types of parameters</param>
        /// <returns>Delegate for void static method</returns>
        public static Action<object[]> StaticMethodVoid(this Type source, string name, params Type[] paramsTypes)
        {
            return StaticMethod<Action<object[]>>(source, name, null, paramsTypes);
        }

        /// <summary>
        /// Creates delegate to generic static method with unspecified number of parameters passed as array 
        /// of objects from instance as object. 
        /// </summary>
        /// <typeparam name="TDelegate">Either Action{object[]} or Function{object[],object}</typeparam>
        /// <param name="source">Type with defined method</param>
        /// <param name="name">Name of method</param>
        /// <param name="paramsTypes">Types of parameters</param>
        /// <param name="typeParams">Type parameters for generic instance method</param>
        /// <returns>Delegate for void generic static method</returns>
        /// <remarks>
        /// Intended for internal use.
        /// </remarks>
#if !NETCORE
        internal
#else
        public
#endif 
            static TDelegate StaticMethod<TDelegate>(this Type source,
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
    }
}