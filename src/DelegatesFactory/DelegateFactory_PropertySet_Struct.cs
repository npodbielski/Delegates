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
        ///     Creates delegate to instance property setter in structure passed by reference with value of
        ///     property type
        /// </summary>
        /// <typeparam name="TProperty">Type of property</typeparam>
        /// <param name="source">Type with defined property</param>
        /// <param name="propertyName">Name of property</param>
        /// <returns>Delegate for instance property setter</returns>
        public static StructSetActionRef<object, TProperty> PropertySetStructRef<TProperty>
            (this Type source, string propertyName)
        {
            var propertyInfo = source.GetPropertyInfo(propertyName, false);
            if (propertyInfo?.SetMethod == null) return null;
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
            var blockExpr = Expression.Block(typeof(void), new[] {structVariable},
                Expression.Assign(structVariable, Expression.Convert(sourceObjectParam, source)),
                Expression.Call(structVariable, propertyInfo.SetMethod, valueExpression),
                Expression.Assign(sourceObjectParam, Expression.Convert(structVariable, typeof(object)))
            );
            var @delegate =
                Expression.Lambda<StructSetActionRef<object, TProperty>>(blockExpr, sourceObjectParam,
                    propertyValueParam).Compile();
            return @delegate;
        }

        /// <summary>
        ///     Creates delegate to instance property setter in structure passed by value as object with value of
        ///     property type. Creates new instance with changed property.
        /// </summary>
        /// <typeparam name="TProperty">Type of property</typeparam>
        /// <param name="source">Type with defined property</param>
        /// <param name="propertyName">Name of property</param>
        /// <returns>Delegate for instance property setter</returns>
        public static StructSetAction<object, TProperty> PropertySetStruct<TProperty>
            (this Type source, string propertyName)
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

            var structVariable = Expression.Variable(source, "struct");
            var blockExpr = Expression.Block(typeof(object), new[] {structVariable},
                Expression.Assign(structVariable, Expression.Convert(sourceObjectParam, source)),
                Expression.Call(structVariable, propertyInfo.SetMethod, valueExpression),
                Expression.Assign(sourceObjectParam, Expression.Convert(structVariable, typeof(object)))
            );
            var @delegate =
                Expression.Lambda<StructSetAction<object, TProperty>>(blockExpr, sourceObjectParam, propertyValueParam)
                    .Compile();
            return @delegate;
        }
		
        /// <summary>
        ///     Creates delegate to instance property setter in structure passed by reference as object with value as
        ///     object
        /// </summary>
        /// <param name="source">Type with defined property</param>
        /// <param name="propertyName">Name of property</param>
        /// <returns>Delegate for instance property setter</returns>
        public static StructSetActionRef<object, object> PropertySetStructRef(this Type source, string propertyName)
        {
            return source.PropertySetStructRef<object>(propertyName);
        }

        /// <summary>
        ///     Creates delegate to instance property setter in structure passed by value as object with value of
        ///     object. Creates new instance with changed property.
        /// </summary>
        /// <param name="source">Type with defined property</param>
        /// <param name="propertyName">Name of property</param>
        /// <returns>Delegate for instance property setter</returns>
        public static StructSetAction<object, object> PropertySetStruct(this Type source, string propertyName)
        {
            return source.PropertySetStruct<object>(propertyName);
        }
    }
}