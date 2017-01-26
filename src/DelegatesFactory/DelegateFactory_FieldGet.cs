using System;
using System.Linq;
using System.Linq.Expressions;
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
        /// <summary>
        /// Creates delegate for retrieving instance field value
        /// </summary>
        /// <typeparam name="TSource">Source type with defined field</typeparam>
        /// <typeparam name="TField">Type of field</typeparam>
        /// <param name="fieldName">Field name</param>
        /// <returns>Delegate for retrieving instance field value</returns>
        public static Func<TSource, TField> FieldGet<TSource, TField>(string fieldName)
        {
            return typeof(TSource).FieldGetImpl<Func<TSource, TField>>(fieldName);
        }

#if !NET35
        /// <summary>
        /// Creates delegate for retrieving instance field value from structure type by reference
        /// </summary>
        /// <typeparam name="TSource">Source value type with defined field</typeparam>
        /// <typeparam name="TField">Type of field</typeparam>
        /// <param name="fieldName">Field name</param>
        /// <returns>Delegate for retrieving instance field value</returns>
        public static StructGetFunc<TSource, TField> FieldGetStruct<TSource, TField>(string fieldName)
            where TSource : struct
        {
            return typeof(TSource).FieldGetImpl<StructGetFunc<TSource, TField>>(fieldName, true);
        }
#endif

        /// <summary>
        /// Creates delegate for retrieving instance field value
        /// </summary>
        /// <typeparam name="TField">Type of field</typeparam>
        /// <param name="source">Type with defined field</param>
        /// <param name="fieldName">Field name</param>
        /// <returns>Delegate for retrieving instance field value</returns>
        public static Func<object, TField> FieldGet<TField>(this Type source,
            string fieldName)
        {
            return source.FieldGetImpl<Func<object, TField>>(fieldName);
        }
        
        /// <summary>
        /// Creates delegate for retrieving instance field value as object from source instance as object
        /// </summary>
        /// <param name="source">Type with defined field</param>
        /// <param name="fieldName">Field name</param>
        /// <returns>Delegate for retrieving instance field value</returns>
        public static Func<object, object> FieldGet(this Type source, string fieldName)
        {
            return source.FieldGetImpl<Func<object, object>>(fieldName);
        }

        /// <summary>
        /// Creates delegate for retrieving instance field value from source instance as object
        /// </summary>
        /// <typeparam name="TField">Type of field</typeparam>
        /// <param name="source">Type with defined field</param>
        /// <param name="fieldName">Field name</param>
        /// <returns>Delegate for retrieving instance field value</returns>
        [Obsolete]
        public static Func<object, TField> FieldGet2<TField>(this Type source,
            string fieldName)
        {
            var fieldInfo = source.GetFieldInfo(fieldName, false);
            if (fieldInfo != null)
            {
                var sourceParam = Expression.Parameter(typeof(object), "source");
                Expression returnExpression = Expression.Field(Expression.Convert(sourceParam, source), fieldInfo);
                var lambda = Expression.Lambda(returnExpression, sourceParam);
                return (Func<object, TField>)lambda.Compile();
            }
            return null;
        }
        
        private static TDelegate FieldGetImpl<TDelegate>(this Type source, string fieldName, bool byRef = false)
          where TDelegate : class
        {
            var fieldInfo = source.GetFieldInfo(fieldName, false);
            if (fieldInfo != null)
            {
                var sourceTypeInDelegate = GetDelegateArguments<TDelegate>().First();
                Expression instanceExpression;
                ParameterExpression sourceParam;
                if (sourceTypeInDelegate.IsByRef == false ?
                    sourceTypeInDelegate != source :
                    sourceTypeInDelegate.GetElementType() != source)
                {
                    sourceParam = Expression.Parameter(typeof(object), "source");
                    instanceExpression = Expression.Convert(sourceParam, source);
                }
                else
                {
                    if (byRef && source.IsValueType())
                    {
                        sourceParam = Expression.Parameter(source.MakeByRefType(), "source");
                        instanceExpression = sourceParam;
                    }
                    else
                    {
                        sourceParam = Expression.Parameter(source, "source");
                        instanceExpression = sourceParam;
                    }
                }
                Expression returnExpression = Expression.Field(instanceExpression, fieldInfo);
                if (!fieldInfo.FieldType.IsClassType())
                {
                    returnExpression = Expression.Convert(returnExpression, GetDelegateReturnType<TDelegate>());
                }
                var lambda = Expression.Lambda<TDelegate>(returnExpression, sourceParam);
                var fieldGetImpl = lambda.Compile();
                return fieldGetImpl;
            }
            return null;
        }
    }
}