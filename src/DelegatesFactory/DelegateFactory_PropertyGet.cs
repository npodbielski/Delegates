using System;
using System.Linq.Expressions;
using System.Reflection;
using Delegates.CustomDelegates;
using Delegates.Extensions;
using static Delegates.Helper.DelegateHelper;

namespace Delegates
{
    /// <summary>
    /// Creates delegates for types members
    /// </summary>
    public static partial class DelegateFactory
    {
        //TODO: Consider PropertyGet(string path="Property1.NestedProperty.SecondNestedProperty")
        //TODO: Consider NestedValueGet(string path="Property1._nestedfield.SecondNestedProperty._secondNestedField")
        /// <summary>
        /// Creates delegate to instance property getter from instance as object with return type of property type
        /// </summary>
        /// <typeparam name="TProperty">Type of property</typeparam>
        /// <param name="source">Type with defined property</param>
        /// <param name="propertyName">Name of property</param>
        /// <returns>Delegate for instance property getter</returns>
        public static Func<object, TProperty> PropertyGet<TProperty>(this Type source, string propertyName)
        {
            return source.PropertyGetImpl<Func<object, TProperty>>(propertyName);
        }

        /// <summary>
        /// Creates delegate to instance property getter from instance as object with return type of object
        /// </summary>
        /// <param name="source">Type with defined property</param>
        /// <param name="propertyName">Name of property</param>
        /// <returns>Delegate for instance property getter</returns>
        public static Func<object, object> PropertyGet(this Type source, string propertyName)
        {
            return source.PropertyGetImpl<Func<object, object>>(propertyName);
        }

        /// <summary>
        /// Creates delegate to instance property getter with return type of property type
        /// </summary>
        /// <typeparam name="TSource">Type with defined property</typeparam>
        /// <typeparam name="TProperty">Type of property</typeparam>
        /// <param name="propertyName">Name of property</param>
        /// <returns>Delegate for instance property getter</returns>
        public static Func<TSource, TProperty> PropertyGet<TSource, TProperty>(string propertyName)
            where TSource : class
        {
            return PropertyGet<TSource, TProperty>(propertyName, null);
        }

        /// <summary>
        /// Obsolete
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="source"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        [Obsolete]
        public static Func<TSource, TProperty> PropertyGet2<TSource, TProperty>(this Type source,
            string propertyName)
        {
            var p = Expression.Parameter(source, "source");
            var lambda = Expression.Lambda(Expression.Property(p, propertyName), p);
            return (Func<TSource, TProperty>)lambda.Compile();
        }

        /// <summary>
        /// Creates delegate to instance property getter from structure passed by reference with return type of 
        /// property type
        /// </summary>
        /// <typeparam name="TSource">Type with defined property</typeparam>
        /// <typeparam name="TProperty">Type of property</typeparam>
        /// <param name="propertyName">Name of property</param>
        /// <returns>Delegate for instance property getter</returns>
        public static StructGetFunc<TSource, TProperty> PropertyGetStruct<TSource, TProperty>(
            string propertyName = null)
            where TSource : struct
        {
            var source = typeof(TSource);
            var propertyInfo = source.GetPropertyInfo(propertyName, false);
            return
                propertyInfo?.GetMethod?.CreateDelegate<StructGetFunc<TSource, TProperty>>();
        }

        private static TDelegate PropertyGetImpl<TDelegate>(this Type source, string propertyName)
            where TDelegate : class
        {
            var propertyInfo = source.GetPropertyInfo(propertyName, false);
            if (propertyInfo?.GetMethod == null)
            {
                return null;
            }
            var sourceObjectParam = Expression.Parameter(typeof(object), "source");
            Expression returnExpression =
                Expression.Call(Expression.Convert(sourceObjectParam, source), propertyInfo.GetMethod);
            if (!propertyInfo.PropertyType.IsClassType())
            {
                returnExpression = Expression.Convert(returnExpression, GetDelegateReturnType<TDelegate>());
            }
            return Expression.Lambda<TDelegate>(returnExpression, sourceObjectParam).Compile();
        }

        private static Func<TSource, TProperty> PropertyGet<TSource, TProperty>(string propertyName = null,
           PropertyInfo propertyInfo = null)
           where TSource : class
        {
            var source = typeof(TSource);
#if NET35 || NET4 || PORTABLE
            var cpropertyInfo = propertyInfo != null ? new CPropertyInfo(propertyInfo)
                : source.GetPropertyInfo(propertyName, false);
            return cpropertyInfo.GetMethod?.CreateDelegate<Func<TSource, TProperty>>();
#else
            propertyInfo = propertyInfo ?? source.GetPropertyInfo(propertyName, false);
            return propertyInfo?.GetMethod?.CreateDelegate<Func<TSource, TProperty>>();
#endif
        }
    }
}