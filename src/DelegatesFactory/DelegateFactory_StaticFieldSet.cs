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
#if  !NET35
        /// <summary>
        /// Creates delegate for setting static field value
        /// </summary>
        /// <typeparam name="TSource">Source type with defined field</typeparam>
        /// <typeparam name="TField">Type of field</typeparam>
        /// <param name="fieldName">Field name</param>
        /// <returns>Delegate for setting static field value</returns>
        public static Action<TField> StaticFieldSet<TSource, TField>(string fieldName)
        {
            var source = typeof(TSource);
            return source.StaticFieldSet<TField>(fieldName);
        }

        /// <summary>
        /// Creates delegate for setting static field value
        /// </summary>
        /// <typeparam name="TField">Type of field</typeparam>
        /// <param name="source">Type with defined field</param>
        /// <param name="fieldName">Field name</param>
        /// <returns>Delegate for setting static field value</returns>
        public static Action<TField> StaticFieldSet<TField>(this Type source, string fieldName)
        {
            var fieldInfo = source.GetFieldInfo(fieldName, true);
            if (fieldInfo != null && !fieldInfo.IsInitOnly)
            {
                var valueParam = Expression.Parameter(typeof(TField), "value");
                var lambda = Expression.Lambda(typeof(Action<TField>),
                    Expression.Assign(Expression.Field(null, fieldInfo), valueParam), valueParam);
                return (Action<TField>)lambda.Compile();
            }
            return null;
        }

        /// <summary>
        /// Creates delegate for setting static field value
        /// </summary>
        /// <param name="source">Type with defined field</param>
        /// <param name="fieldName">Field name</param>
        /// <returns>Delegate for setting static field value</returns>
        public static Action<object> StaticFieldSet(this Type source, string fieldName)
        {
            var fieldInfo = source.GetFieldInfo(fieldName, true);
            if (fieldInfo != null && !fieldInfo.IsInitOnly)
            {
                var valueParam = Expression.Parameter(typeof(object), "value");
                var convertedValueExpr = Expression.Convert(valueParam, fieldInfo.FieldType);
                var lambda = Expression.Lambda(typeof(Action<object>),
                    Expression.Assign(Expression.Field(null, fieldInfo), convertedValueExpr), valueParam);
                return (Action<object>)lambda.Compile();
            }
            return null;
        }
#endif
    }
}