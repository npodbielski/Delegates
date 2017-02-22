using Delegates.CustomDelegates;
using Delegates.Extensions;
using System;
using System.Linq.Expressions;

namespace Delegates
{
    /// <summary>
    /// Creates delegates for types members
    /// </summary>
    public static partial class DelegateFactory
    {
        /// <summary>
        /// Creates delegate for indexer get accessor at specified index
        /// </summary>
        /// <typeparam name="TSource">Source type with defined indexer</typeparam>
        /// <typeparam name="TReturn">Indexer return type</typeparam>
        /// <typeparam name="TIndex">Index parameter type</typeparam>
        /// <returns>Delegate for indexer get accessor at specified index</returns>
        public static Func<TSource, TIndex, TReturn> IndexerGet<TSource, TReturn, TIndex>()
            where TSource : class
        {
            var propertyInfo = typeof(TSource).GetIndexerPropertyInfo(new[] { typeof(TIndex) });
            return propertyInfo?.GetMethod?.CreateDelegate<Func<TSource, TIndex, TReturn>>();
        }

        /// <summary>
        /// Creates delegate for indexer get accessor at specified index
        /// </summary>
        /// <typeparam name="TSource">Source type with defined indexer</typeparam>
        /// <typeparam name="TReturn">Indexer return type</typeparam>
        /// <typeparam name="TIndex">First index parameter type</typeparam>
        /// <typeparam name="TIndex2">Second index parameter type</typeparam>
        /// <returns>Delegate for indexer get accessor with two indexes</returns>
        public static Func<TSource, TIndex, TIndex2, TReturn> IndexerGet<TSource, TReturn, TIndex, TIndex2>()
            where TSource : class
        {
            var propertyInfo = typeof(TSource).GetIndexerPropertyInfo(new[] { typeof(TIndex), typeof(TIndex2) });
            return propertyInfo?.GetMethod?.CreateDelegate<Func<TSource, TIndex, TIndex2, TReturn>>();
        }

        /// <summary>
        /// Creates delegate for indexer get accessor at specified three indexes from instance
        /// </summary>
        /// <typeparam name="TSource">Source type with defined indexer</typeparam>
        /// <typeparam name="TReturn">Indexer return type</typeparam>
        /// <typeparam name="TIndex">First index parameter type</typeparam>
        /// <typeparam name="TIndex2">Second index parameter type</typeparam>
        /// <typeparam name="TIndex3">Third index parameter type</typeparam>
        /// <returns>Delegate for indexer get accessor with two indexes</returns>
        public static Func<TSource, TIndex, TIndex2, TIndex2, TReturn> IndexerGet
            <TSource, TReturn, TIndex, TIndex2, TIndex3>() where TSource : class
        {
            var propertyInfo = typeof(TSource).GetIndexerPropertyInfo(
                new[] { typeof(TIndex), typeof(TIndex2), typeof(TIndex3) });
            return propertyInfo?.GetMethod?.CreateDelegate<Func<TSource, TIndex, TIndex2, TIndex2, TReturn>>();
        }

        /// <summary>
        /// Creates delegate for indexer get accessor at specified index from instance as object
        /// </summary>
        /// <typeparam name="TReturn">Indexer return type</typeparam>
        /// <typeparam name="TIndex">Index parameter type</typeparam>
        /// <param name="source">Type with defined indexer</param>
        /// <returns>Delegate for indexer get accessor at specified index</returns>
        public static Func<object, TIndex, TReturn> IndexerGet<TReturn, TIndex>(this Type source)
        {
            var indexType = typeof(TIndex);
            return (Func<object, TIndex, TReturn>)DelegateIndexerGet(source, indexType);
        }

        /// <summary>
        /// Creates delegate for indexer get accessor at specified two indexes from instance as object
        /// </summary>
        /// <typeparam name="TReturn">Indexer return type</typeparam>
        /// <typeparam name="TIndex">First index parameter type</typeparam>
        /// <typeparam name="TIndex2">Second index parameter type</typeparam>
        /// <param name="source">Type with defined indexer</param>
        /// <returns>Delegate for indexer get accessor with two indexes</returns>
        public static Func<object, TIndex, TIndex2, TReturn> IndexerGet<TReturn, TIndex, TIndex2>(this Type source)
        {
            var indexType = typeof(TIndex);
            var indexType2 = typeof(TIndex2);
            return
                (Func<object, TIndex, TIndex2, TReturn>)
                DelegateIndexerGet(source, indexType, indexType2);
        }

        /// <summary>
        /// Creates delegate for indexer get accessor at specified three indexes from instance as object
        /// </summary>
        /// <typeparam name="TReturn">Indexer return type</typeparam>
        /// <typeparam name="TIndex">First index parameter type</typeparam>
        /// <typeparam name="TIndex2">Second index parameter type</typeparam>
        /// <typeparam name="TIndex3">Third index parameter type</typeparam>
        /// <param name="source">Type with defined indexer</param>
        /// <returns>Delegate for indexer get accessor with three indexes</returns>
        public static Func<object, TIndex, TIndex2, TIndex3, TReturn> IndexerGet<TReturn, TIndex, TIndex2, TIndex3>(
            this Type source)
        {
            var indexType = typeof(TIndex);
            var indexType2 = typeof(TIndex2);
            var indexType3 = typeof(TIndex3);
            return
                (Func<object, TIndex, TIndex2, TIndex3, TReturn>)
                DelegateIndexerGet(source, indexType, indexType2, indexType3);
        }

        /// <summary>
        /// Creates delegate for indexer get accessor with unspecified number of indexes from instance as object
        /// </summary>
        /// <param name="source">Type with defined indexer</param>
        /// <param name="returnType">Return type of indexer</param>
        /// <param name="indexTypes">Collection of index parameters types</param>
        /// <returns>Delegate for indexer get accessor with array of indexes</returns>
        /// <remarks>
        /// <paramref name="returnType"/> parameter is not necessary, but for compatibility new method was created.
        /// This one will be removed in next release.
        /// </remarks>
        [Obsolete("Use IndexerGetNew method instead")]
        public static Func<object, object[], object> IndexerGet(this Type source, Type returnType,
            params Type[] indexTypes)
        {
            return source.IndexerGetNew(indexTypes);
        }

        /// <summary>
        /// Creates delegate for indexer get accessor with unspecified number of indexes from instance as object
        /// </summary>
        /// <param name="source">Type with defined indexer</param>
        /// <param name="indexTypes">Collection of index parameters types</param>
        /// <returns>Delegate for indexer get accessor with array of indexes</returns>
        public static Func<object, object[], object> IndexerGetNew(this Type source, params Type[] indexTypes)
        {
            var propertyInfo = source.GetIndexerPropertyInfo(indexTypes);
            if (propertyInfo?.GetMethod == null)
            {
                return null;
            }
            var sourceObjectParam = Expression.Parameter(typeof(object), "source");
            var indexesParam = Expression.Parameter(typeof(object[]), "indexes");
            var paramsExpression = new Expression[indexTypes.Length];
            for (var i = 0; i < indexTypes.Length; i++)
            {
                var indexType = indexTypes[i];
                paramsExpression[i] = Expression.Convert(Expression.ArrayIndex(indexesParam, Expression.Constant(i)),
                    indexType);
            }
            Expression returnExpression =
                Expression.Call(Expression.Convert(sourceObjectParam, source), propertyInfo.GetMethod, paramsExpression);
            if (!propertyInfo.PropertyType.IsClassType())
            {
                returnExpression = Expression.Convert(returnExpression, typeof(object));
            }
            return Expression.Lambda<Func<object, object[], object>>(
                returnExpression, sourceObjectParam, indexesParam).Compile();
        }

        /// <summary>
        /// Creates delegate for indexer get accessor at specified index from structure passed by reference
        /// </summary>
        /// <typeparam name="TSource">Source type with defined indexer</typeparam>
        /// <typeparam name="TReturn">Indexer return type</typeparam>
        /// <typeparam name="TIndex">Index parameter type</typeparam>
        /// <returns>Delegate for indexer get accessor at specified index</returns>
        public static StructIndex1GetFunc<TSource, TIndex, TReturn> IndexerGetStruct<TSource, TReturn, TIndex>()
            where TSource : struct
        {
            var propertyInfo = typeof(TSource).GetIndexerPropertyInfo(new[] { typeof(TIndex) });
            return propertyInfo?.GetMethod?.CreateDelegate<StructIndex1GetFunc<TSource, TIndex, TReturn>>();
        }

        /// <summary>
        /// Creates delegate for indexer get accessor at specified two indexes from structure passed by reference
        /// </summary>
        /// <typeparam name="TSource">Source type with defined indexer</typeparam>
        /// <typeparam name="TReturn">Indexer return type</typeparam>
        /// <typeparam name="TIndex">First index parameter type</typeparam>
        /// <typeparam name="TIndex2">Second index parameter type</typeparam>
        /// <returns>Delegate for indexer get accessor with two indexes</returns>
        public static StructIndex2GetFunc<TSource, TIndex, TIndex2, TReturn>
            IndexerGetStruct<TSource, TReturn, TIndex, TIndex2>()
            where TSource : struct
        {
            var propertyInfo = typeof(TSource).GetIndexerPropertyInfo(new[] { typeof(TIndex), typeof(TIndex2) });
            return propertyInfo?.GetMethod?.CreateDelegate<StructIndex2GetFunc<TSource, TIndex, TIndex2, TReturn>>();
        }

        /// <summary>
        /// Creates delegate for indexer get accessor at specified three indexes from structure passed by reference
        /// </summary>
        /// <typeparam name="TSource">Source type with defined indexer</typeparam>
        /// <typeparam name="TReturn">Indexer return type</typeparam>
        /// <typeparam name="TIndex">First index parameter type</typeparam>
        /// <typeparam name="TIndex2">Second index parameter type</typeparam>
        /// <typeparam name="TIndex3">Third index parameter type</typeparam>
        /// <returns>Delegate for indexer get accessor with two indexes</returns>
        public static StructIndex3GetFunc<TSource, TIndex, TIndex2, TIndex2, TReturn> IndexerGetStruct
            <TSource, TReturn, TIndex, TIndex2, TIndex3>() where TSource : struct
        {
            var propertyInfo = typeof(TSource).GetIndexerPropertyInfo(
                new[] { typeof(TIndex), typeof(TIndex2), typeof(TIndex3) });
            return propertyInfo?.GetMethod?.CreateDelegate<
                StructIndex3GetFunc<TSource, TIndex, TIndex2, TIndex2, TReturn>>();
        }

        /// <summary>
        /// Creates delegate for indexer get accessor
        /// </summary>
        /// <param name="source">Type with defined indexer</param>
        /// <param name="indexTypes">Collection of indexer index parameters</param>
        /// <returns>Delegate for indexer get accessor</returns>
        private static Delegate DelegateIndexerGet(Type source,
            params Type[] indexTypes)
        {
            var indexerInfo = source.GetIndexerPropertyInfo(indexTypes);
            if (indexerInfo?.GetMethod == null)
            {
                return null;
            }
            var sourceObjectParam = Expression.Parameter(typeof(object), "source");
            var paramsExpression = new ParameterExpression[indexTypes.Length];
            for (var i = 0; i < indexTypes.Length; i++)
            {
                var indexType = indexTypes[i];
                paramsExpression[i] = Expression.Parameter(indexType, "i" + i);
            }
            var getMethod = indexerInfo.GetMethod;
            var lambdaParams = paramsExpression.GetLambdaExprParams(sourceObjectParam);
            return Expression.Lambda(
                Expression.Call(Expression.Convert(sourceObjectParam, source), getMethod, paramsExpression),
                lambdaParams).Compile();
        }
    }
}