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
        /// Creates delegate to non-void generic instance method with unspecified number of parameters passed as array of objects from 
        /// instance as object 
        /// </summary>
        /// <param name="source">Type with defined method</param>
        /// <param name="name">Name of method</param>
        /// <param name="paramsTypes">Types of parameters</param>
        /// <param name="typeParams">Type parameters for generic instance method</param>
        /// <returns>Delegate for non-void generic instance method</returns>
        public static Func<object, object[], object> InstanceGenericMethod(this Type source,
            string name, Type[] paramsTypes, Type[] typeParams)
        {
            return InstanceGenericMethod<Func<object, object[], object>>(source, name, typeParams, paramsTypes);
        }

        /// <summary>
        /// Creates delegate to void generic instance method with unspecified number of parameters passed as array 
        /// of objects from instance as object
        /// </summary>
        /// <param name="source">Type with defined method</param>
        /// <param name="name">Name of method</param>
        /// <param name="paramsTypes">Types of parameters</param>
        /// <param name="typeParams">Type parameters for generic instance method</param>
        /// <returns>Delegate for void generic instance method</returns>
        public static Action<object, object[]> InstanceGenericMethodVoid(this Type source,
            string name, Type[] paramsTypes, Type[] typeParams)
        {
            return InstanceGenericMethod<Action<object, object[]>>(source, name, typeParams, paramsTypes);
        }
        
        /// <summary>
        /// Creates delegate to generic instance method with unspecified number of parameters passed as array 
        /// of objects from instance as object. 
        /// </summary>
        /// <typeparam name="TDelegate">Either Action{object,object[]} or Function{object,object[],object}</typeparam>
        /// <param name="source">Type with defined method</param>
        /// <param name="name">Name of method</param>
        /// <param name="paramsTypes">Types of parameters</param>
        /// <param name="typeParams">Type parameters for generic instance method</param>
        /// <returns>Delegate for void generic instance method</returns>
        /// <remarks>
        /// Intended for internal use.
        /// </remarks>
#if !NETCORE
        internal
#else
        public
#endif
            static TDelegate InstanceGenericMethod<TDelegate>(this Type source,
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
    }
}