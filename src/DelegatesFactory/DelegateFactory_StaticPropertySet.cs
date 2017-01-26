using System;
using System.Linq.Expressions;
using Delegates.Extensions;

namespace Delegates
{
    /// <summary>
    /// Creates delegates for types members
    /// </summary>
    public static partial class DelegateFactory
    {
        /// <summary>
        /// Creates delegate to static property setter with value of property type
        /// </summary>
        /// <typeparam name="TSource">Type with defined property</typeparam>
        /// <typeparam name="TProperty">Type of property</typeparam>
        /// <param name="propertyName">Name of property</param>
        /// <returns>Delegate for static property setter</returns>
        public static Action<TProperty> StaticPropertySet<TSource, TProperty>(string propertyName)
        {
            return typeof(TSource).StaticPropertySet<TProperty>(propertyName);
        }

        /// <summary>
        /// Creates delegate to static property setter with value of property type
        /// </summary>
        /// <typeparam name="TProperty">Type of property</typeparam>
        /// <param name="source">Type with defined property</param>
        /// <param name="propertyName">Name of property</param>
        /// <returns>Delegate for static property setter</returns>
        public static Action<TProperty> StaticPropertySet<TProperty>(this Type source, string propertyName)
        {
            var propertyInfo = source.GetPropertyInfo(propertyName, true);
            return propertyInfo?.SetMethod?.CreateDelegate<Action<TProperty>>();
        }

        /// <summary>
        /// Creates delegate to static property setter with value of object
        /// </summary>
        /// <param name="source">Type with defined property</param>
        /// <param name="propertyName">Name of property</param>
        /// <returns>Delegate for static property setter</returns>
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
    }
}