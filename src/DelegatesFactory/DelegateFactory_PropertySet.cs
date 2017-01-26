using System;
using System.Linq.Expressions;
using Delegates.CustomDelegates;
using Delegates.Extensions;

namespace Delegates
{
    /// <summary>
    /// Creates delegates for types members
    /// </summary>
    public static partial class DelegateFactory
    {
        /// <summary>
        /// Creates delegate to instance property setter with value of property type
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
        /// Creates delegate to instance property setter in instance as object with value of property type
        /// </summary>
        /// <typeparam name="TProperty">Type of property</typeparam>
        /// <param name="source">Type with defined property</param>
        /// <param name="propertyName">Name of property</param>
        /// <returns>Delegate for instance property setter</returns>
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
        /// <summary>
        /// Creates delegate to instance property setter in structure passed by reference with value of 
        /// property type
        /// </summary>
        /// <typeparam name="TProperty">Type of property</typeparam>
        /// <param name="source">Type with defined property</param>
        /// <param name="propertyName">Name of property</param>
        /// <returns>Delegate for instance property setter</returns>
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

        /// <summary>
        /// Creates delegate to instance property setter in structure passed by value as object with value of 
        /// property type. Creates new instance with changed property.
        /// </summary>
        /// <typeparam name="TProperty">Type of property</typeparam>
        /// <param name="source">Type with defined property</param>
        /// <param name="propertyName">Name of property</param>
        /// <returns>Delegate for instance property setter</returns>
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
        /// <summary>
        /// Creates delegate to instance property setter in instance as object with value as object
        /// </summary>
        /// <param name="source">Type with defined property</param>
        /// <param name="propertyName">Name of property</param>
        /// <returns>Delegate for instance property setter</returns>
        public static Action<object, object> PropertySet(this Type source, string propertyName)
        {
            return source.PropertySet<object>(propertyName);
        }

#if !NET35
        /// <summary>
        /// Creates delegate to instance property setter in structure passed by reference as object with value as
        /// object
        /// </summary>
        /// <param name="source">Type with defined property</param>
        /// <param name="propertyName">Name of property</param>
        /// <returns>Delegate for instance property setter</returns>
        public static StructSetActionRef<object, object> PropertySetStructRef(this Type source, string propertyName)
        {
            return source.PropertySetStructRef<object>(propertyName);
        }
        
        /// <summary>
        /// Creates delegate to instance property setter in structure passed by value as object with value of 
        /// object. Creates new instance with changed property.
        /// </summary>
        /// <param name="source">Type with defined property</param>
        /// <param name="propertyName">Name of property</param>
        /// <returns>Delegate for instance property setter</returns>
        public static StructSetAction<object, object> PropertySetStruct(this Type source, string propertyName)
        {
            return source.PropertySetStruct<object>(propertyName);
        }
#endif
        /// <summary>
        /// Creates delegate to instance property setter with value of property type
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
        /// Creates delegate to instance property setter in structure passed by reference with value of property 
        /// type
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