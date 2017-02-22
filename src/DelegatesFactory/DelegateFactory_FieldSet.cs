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
#if !NET35
        /// <summary>
        /// Creates delegate for setting instance field value in structure type passed by reference as object
        /// </summary>
        /// <typeparam name="TField">Type of field</typeparam>
        /// <param name="source"></param>
        /// <param name="fieldName">Field name</param>
        /// <returns>Delegate for setting instance field value</returns>
        public static StructSetActionRef<object, TField> FieldSetStructRef<TField>(this Type source,
            string fieldName)
        {
            var fieldInfo = source.GetFieldInfo(fieldName, false);
            if (fieldInfo != null && !fieldInfo.IsInitOnly)
            {
                var sourceParam = Expression.Parameter(typeof(object).MakeByRefType(), "source");
                ParameterExpression valueParam;
                Expression valueExpr;
                if (fieldInfo.FieldType == typeof(TField))
                {
                    valueParam = Expression.Parameter(fieldInfo.FieldType, "value");
                    valueExpr = valueParam;
                }
                else
                {
                    valueParam = Expression.Parameter(typeof(TField), "value");
                    valueExpr = Expression.Convert(valueParam, fieldInfo.FieldType);
                }
                var structVariable = Expression.Variable(source, "struct");
                var body = Expression.Block(typeof(void), new[] { structVariable },
                    Expression.Assign(structVariable, Expression.Convert(sourceParam, source)),
                    Expression.Assign(Expression.Field(structVariable, fieldInfo), valueExpr),
                    Expression.Assign(sourceParam, Expression.Convert(structVariable, typeof(object)))
                );
                var lambda = Expression.Lambda<StructSetActionRef<object, TField>>(body, sourceParam, valueParam);
                return lambda.Compile();
            }
            return null;
        }

        /// <summary>
        /// Creates delegate for setting instance field value in structure passed by value as object.
        /// Returns new value with changed field value.
        /// </summary>
        /// <typeparam name="TField">Type of field</typeparam>
        /// <param name="source">Type with defined field</param>
        /// <param name="fieldName">Field name</param>
        /// <returns>Delegate for setting instance field value</returns>
        public static StructSetAction<object, TField> FieldSetStruct<TField>(this Type source, string fieldName)
        {
            var fieldInfo = source.GetFieldInfo(fieldName, false);
            if (fieldInfo != null && !fieldInfo.IsInitOnly)
            {
                var sourceParam = Expression.Parameter(typeof(object), "source");
                ParameterExpression valueParam;
                Expression valueExpr;
                if (fieldInfo.FieldType == typeof(TField))
                {
                    valueParam = Expression.Parameter(fieldInfo.FieldType, "value");
                    valueExpr = valueParam;
                }
                else
                {
                    valueParam = Expression.Parameter(typeof(TField), "value");
                    valueExpr = Expression.Convert(valueParam, fieldInfo.FieldType);
                }
                var structVariable = Expression.Variable(source, "struct");
                var body = Expression.Block(typeof(object), new[] { structVariable },
                    Expression.Assign(structVariable, Expression.Convert(sourceParam, source)),
                    Expression.Assign(Expression.Field(structVariable, fieldInfo), valueExpr),
                    Expression.Assign(sourceParam, Expression.Convert(structVariable, typeof(object)))
                );
                var lambda = Expression.Lambda<StructSetAction<object, TField>>(body, sourceParam, valueParam);
                return lambda.Compile();
            }
            return null;
        }

        /// <summary>
        /// Creates delegate for setting instance field value in instance by passed by object
        /// </summary>
        /// <typeparam name="TField">Type of field</typeparam>
        /// <param name="source">Type with defined field</param>
        /// <param name="fieldName">Field name</param>
        /// <returns>Delegate for setting instance field value</returns>
        public static Action<object, TField> FieldSet<TField>(this Type source, string fieldName)
        {
            var fieldInfo = source.GetFieldInfo(fieldName, false);
            if (fieldInfo != null && !fieldInfo.IsInitOnly)
            {
                var sourceParam = Expression.Parameter(typeof(object), "source");
                Expression valueExpr;
                var valueParam = Expression.Parameter(typeof(TField), "value");
                if (fieldInfo.FieldType == typeof(TField))
                {
                    valueExpr = valueParam;
                }
                else
                {
                    valueExpr = Expression.Convert(valueParam, fieldInfo.FieldType);
                }
                var lambda = Expression.Lambda<Action<object, TField>>(
                    Expression.Assign(Expression.Field(Expression.Convert(sourceParam, source), fieldInfo), valueExpr),
                    sourceParam, valueParam);
                return lambda.Compile();
            }
            return null;
        }

        /// <summary>
        /// Creates delegate for setting instance field value in structure by passed by type as reference
        /// </summary>
        /// <typeparam name="TSource">Source type with defined field</typeparam>
        /// <typeparam name="TField">Type of field</typeparam>
        /// <param name="fieldName">Field name</param>
        /// <returns>Delegate for setting instance field value</returns>
        public static StructSetActionRef<TSource, TField> FieldSetStruct<TSource, TField>(string fieldName)
            where TSource : struct
        {
            var source = typeof(TSource);
            var fieldInfo = source.GetFieldInfo(fieldName, false);
            if (fieldInfo != null && !fieldInfo.IsInitOnly)
            {
                Expression sourcExpr;
                var sourceParam = Expression.Parameter(typeof(TSource).MakeByRefType(), "source");
                if (source == typeof(TSource))
                {
                    sourcExpr = sourceParam;
                }
                else
                {
                    sourcExpr = Expression.Convert(sourceParam, source);
                }
                var valueParam = Expression.Parameter(typeof(TField), "value");
                Expression valueExpr;
                var structVariable = Expression.Variable(source, "struct");
                if (fieldInfo.FieldType == typeof(TField))
                {
                    valueExpr = valueParam;
                }
                else
                {
                    valueExpr = Expression.Convert(valueParam, fieldInfo.FieldType);
                }
                var body = Expression.Block(typeof(void), new[] { structVariable },
                    Expression.Assign(structVariable, sourcExpr),
                    Expression.Assign(Expression.Field(structVariable, fieldInfo), valueExpr),
                    Expression.Assign(sourceParam, Expression.Convert(structVariable, typeof(TSource)))
                );
                var lambda = Expression.Lambda<StructSetActionRef<TSource, TField>>(body, sourceParam, valueParam);
                return lambda.Compile();
            }
            return null;
        }

        /// <summary>
        /// Creates delegate for setting instance field value in instance
        /// </summary>
        /// <typeparam name="TSource">Source type with defined field</typeparam>
        /// <typeparam name="TField">Type of field</typeparam>
        /// <param name="fieldName">Field name</param>
        /// <returns>Delegate for setting instance field value</returns>
        public static Action<TSource, TField> FieldSet<TSource, TField>(string fieldName)
            where TSource : class
        {
            var source = typeof(TSource);
            var fieldInfo = source.GetFieldInfo(fieldName, false);
            if (fieldInfo != null && !fieldInfo.IsInitOnly)
            {
                var sourceParam = Expression.Parameter(source, "source");
                var valueParam = Expression.Parameter(typeof(TField), "value");
                var lambda = Expression.Lambda<Action<TSource, TField>>(
                    Expression.Assign(Expression.Field(sourceParam, fieldInfo), valueParam),
                    sourceParam, valueParam);
                return lambda.Compile();
            }
            return null;
        }

        /// <summary>
        /// Creates delegate for setting instance field value as object in instance as object
        /// </summary>
        /// <param name="source">Type with defined field</param>
        /// <param name="fieldName">Field name</param>
        /// <returns>Delegate for setting instance field value</returns>
        public static Action<object, object> FieldSet(this Type source, string fieldName)
        {
            var fieldInfo = source.GetFieldInfo(fieldName, false);
            if (fieldInfo != null && !fieldInfo.IsInitOnly)
            {
                var sourceParam = Expression.Parameter(typeof(object), "source");
                var valueParam = Expression.Parameter(typeof(object), "value");
                var convertedValueExpr = Expression.Convert(valueParam, fieldInfo.FieldType);
                Expression returnExpression =
                    Expression.Assign(Expression.Field(Expression.Convert(sourceParam, source), fieldInfo),
                        convertedValueExpr);
                if (!fieldInfo.FieldType.IsClassType())
                {
                    returnExpression = Expression.Convert(returnExpression, typeof(object));
                }
                var lambda = Expression.Lambda<Action<object, object>>(returnExpression, sourceParam, valueParam);
                return lambda.Compile();
            }
            return null;
        }

        /// <summary>
        /// Creates delegate for setting instance field value as object in structure passed by reference as object
        /// </summary>
        /// <param name="source">Type with defined field</param>
        /// <param name="fieldName">Field name</param>
        /// <returns>Delegate for setting instance field value</returns>
        public static StructSetActionRef<object, object> FieldSetStructRef(this Type source, string fieldName)
        {
            var fieldInfo = source.GetFieldInfo(fieldName, false);
            if (fieldInfo != null && !fieldInfo.IsInitOnly)
            {
                var sourceParam = Expression.Parameter(typeof(object).MakeByRefType(), "source");
                var valueParam = Expression.Parameter(typeof(object), "value");
                Expression valueExpr;
                if (fieldInfo.FieldType != typeof(object))
                {
                    valueExpr = Expression.Convert(valueParam, fieldInfo.FieldType);
                }
                else
                {
                    valueExpr = valueParam;
                }
                var structVariable = Expression.Variable(source, "struct");
                Expression returnExpression = Expression.Assign(Expression.Field(structVariable, fieldInfo), valueExpr);
                if (!fieldInfo.FieldType.IsClassType())
                {
                    returnExpression = Expression.Convert(returnExpression, typeof(object));
                }
                var body = Expression.Block(typeof(void), new[] { structVariable },
                    Expression.Assign(structVariable, Expression.Convert(sourceParam, source)),
                    returnExpression,
                    Expression.Assign(sourceParam, Expression.Convert(structVariable, typeof(object)))
                );
                var lambda = Expression.Lambda<StructSetActionRef<object, object>>(body, sourceParam, valueParam);
                return lambda.Compile();
            }
            return null;
        }

        /// <summary>
        /// Creates delegate for setting instance field value as object in structure passed as object.
        /// Returns new value with changed field value.
        /// </summary>
        /// <param name="source">Type with defined field</param>
        /// <param name="fieldName">Field name</param>
        /// <returns>Delegate for setting instance field value</returns>
        public static StructSetAction<object, object> FieldSetStruct(this Type source, string fieldName)
        {
            var fieldInfo = source.GetFieldInfo(fieldName, false);
            if (fieldInfo != null && !fieldInfo.IsInitOnly)
            {
                var sourceParam = Expression.Parameter(typeof(object), "source");
                var valueParam = Expression.Parameter(typeof(object), "value");
                Expression valueExpr;
                if (fieldInfo.FieldType != typeof(object))
                {
                    valueExpr = Expression.Convert(valueParam, fieldInfo.FieldType);
                }
                else
                {
                    valueExpr = valueParam;
                }
                var structVariable = Expression.Variable(source, "struct");
                Expression returnExpression = Expression.Assign(Expression.Field(structVariable, fieldInfo), valueExpr);
                if (!fieldInfo.FieldType.IsClassType())
                {
                    returnExpression = Expression.Convert(returnExpression, typeof(object));
                }
                var body = Expression.Block(typeof(object), new[] { structVariable },
                    Expression.Assign(structVariable, Expression.Convert(sourceParam, source)),
                    returnExpression,
                    Expression.Assign(sourceParam, Expression.Convert(structVariable, typeof(object)))
                );
                var lambda = Expression.Lambda<StructSetAction<object, object>>(body, sourceParam, valueParam);
                return lambda.Compile();
            }
            return null;
        }

        /// <summary>
        /// Creates delegate for setting instance field value in instance as object
        /// </summary>
        /// <typeparam name="TField">Type of field</typeparam>
        /// <param name="source">Type with defined field</param>
        /// <param name="fieldName">Field name</param>
        /// <returns>Delegate for setting instance field value</returns>
        [Obsolete]
        public static Action<object, TField> FieldSetWithCast<TField>(this Type source, string fieldName)
        {
            return source.FieldSet(fieldName) as Action<object, TField>;
        }

        /// <summary>
        /// Creates delegate for setting instance field value in instance
        /// </summary>
        /// <typeparam name="TSource">Source type with defined field</typeparam>
        /// <typeparam name="TField">Type of field</typeparam>
        /// <param name="fieldName">Field name</param>
        /// <returns>Delegate for setting instance field value</returns>
        [Obsolete]
        public static Action<TSource, TField> FieldSetWithCast<TSource, TField>(string fieldName)
        {
            return typeof(TSource).FieldSet(fieldName) as Action<TSource, TField>;
        }
#endif
    }
}