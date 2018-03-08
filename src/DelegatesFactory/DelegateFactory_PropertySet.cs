// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DelegateFactory_PropertySet.cs" company="Natan Podbielski">
//   Copyright (c) 2016 - 2018 Natan Podbielski. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq.Expressions;
using Delegates.CustomDelegates;
using Delegates.Extensions;

namespace Delegates
{
    /// <summary>
    ///     Creates delegates for types members
    /// </summary>
    public static partial class DelegateFactory
    {
        /// <summary>
        ///     Creates delegate to instance property setter with value of property type
        /// </summary>
        /// <typeparam name="TSource">Type with defined property</typeparam>
        /// <typeparam name="TProperty">Type of property</typeparam>
        /// <param name="source">Type with defined property</param>
        /// <param name="propertyName">Name of property</param>
        /// <returns>Delegate for instance property setter</returns>
        public static Action<TSource, TProperty> PropertySet<TSource, TProperty>(this TSource source,
            string propertyName)
        {
            return PropertySet<TSource, TProperty>(propertyName);
        }

        /// <summary>
        ///     Creates delegate to instance property setter in instance as object with value of property type
        /// </summary>
        /// <typeparam name="TProperty">Type of property</typeparam>
        /// <param name="source">Type with defined property</param>
        /// <param name="propertyName">Name of property</param>
        /// <returns>Delegate for instance property setter</returns>
        public static Action<object, TProperty> PropertySet<TProperty>(this Type source, string propertyName)
        {
            var propertyInfo = source.GetPropertyInfo(propertyName, false);
            if (propertyInfo?.SetMethod == null) return null;
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

        /// <summary>
        ///     Creates delegate to instance property setter in instance as object with value as object
        /// </summary>
        /// <param name="source">Type with defined property</param>
        /// <param name="propertyName">Name of property</param>
        /// <returns>Delegate for instance property setter</returns>
        public static Action<object, object> PropertySet(this Type source, string propertyName)
        {
            return source.PropertySet<object>(propertyName);
        }

        /// <summary>
        ///     Creates delegate to instance property setter with value of property type
        /// </summary>
        /// <typeparam name="TSource">Type with defined property</typeparam>
        /// <typeparam name="TProperty">Type of property</typeparam>
        /// <param name="propertyName">Name of property</param>
        /// <returns>Delegate for instance property setter</returns>
        public static Action<TSource, TProperty> PropertySet<TSource, TProperty>(string propertyName)
        {
            var source = typeof(TSource);
            var propertyInfo = source.GetPropertyInfo(propertyName, false);
            return
                propertyInfo?.SetMethod?.CreateDelegate<Action<TSource, TProperty>>();
        }

        /// <summary>
        ///     Creates delegate to instance property setter in structure passed by reference with value of property
        ///     type
        /// </summary>
        /// <typeparam name="TSource">Type with defined property</typeparam>
        /// <typeparam name="TProperty">Type of property</typeparam>
        /// <param name="propertyName">Name of property</param>
        /// <returns>Delegate for instance property setter</returns>
        public static StructSetActionRef<TSource, TProperty> PropertySetStructRef<TSource, TProperty>(
            string propertyName)
        {
            var source = typeof(TSource);
            var propertyInfo = source.GetPropertyInfo(propertyName, false);
            return
                propertyInfo?.SetMethod?.CreateDelegate<StructSetActionRef<TSource, TProperty>>();
        }
    }
}