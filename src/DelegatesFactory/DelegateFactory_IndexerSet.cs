using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
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
        /// Creates delegate for indexer set accessor at specified index from instance as object
        /// </summary>
        /// <typeparam name="TValue">Indexer value type</typeparam>
        /// <typeparam name="TIndex">Index parameter type</typeparam>
        /// <param name="source">Type with defined indexer</param>
        /// <returns>Delegate for indexer set accessor with single index</returns>
        public static Action<object, TIndex, TValue> IndexerSet<TValue, TIndex>(this Type source)
        {
            var indexType = typeof(TIndex);
            return DelegateIndexerSet<Action<object, TIndex, TValue>>
                (source, typeof(TValue), indexType);
        }

        /// <summary>
        /// Creates delegate for indexer set accessor at specified index from structure as object passed by reference
        /// </summary>
        /// <typeparam name="TValue">Indexer value type</typeparam>
        /// <typeparam name="TIndex">Index parameter type</typeparam>
        /// <param name="source">Type with defined indexer</param>
        /// <returns>Delegate for indexer set accessor with single index</returns>
        public static StructIndex1SetAction<object, TIndex, TValue> IndexerSetStruct<TValue, TIndex>(this Type source)
        {
            var indexType = typeof(TIndex);
            return DelegateIndexerSet<StructIndex1SetAction<object, TIndex, TValue>>
                (source, typeof(TValue), indexType);
        }

        /// <summary>
        /// Creates delegate for indexer set accessor at specified two indexes from instance as object
        /// </summary>
        /// <typeparam name="TValue">Indexer value type</typeparam>
        /// <typeparam name="TIndex">First index parameter type</typeparam>
        /// <typeparam name="TIndex2">Second index parameter type</typeparam>
        /// <param name="source">Type with defined indexer</param>
        /// <returns>Delegate for indexer set accessor with two indexes</returns>
        public static Action<object, TIndex, TIndex2, TValue> IndexerSet<TValue, TIndex, TIndex2>(this Type source)
        {
            var indexType = typeof(TIndex);
            var indexType2 = typeof(TIndex2);
            return DelegateIndexerSet<Action<object, TIndex, TIndex2, TValue>>
                (source, typeof(TValue), indexType, indexType2);
        }

        /// <summary>
        /// Creates delegate for indexer set accessor at specified two indexes from structure as object passed by reference
        /// </summary>
        /// <typeparam name="TValue">Indexer value type</typeparam>
        /// <typeparam name="TIndex">First index parameter type</typeparam>
        /// <typeparam name="TIndex2">Second index parameter type</typeparam>
        /// <param name="source">Type with defined indexer</param>
        /// <returns>Delegate for indexer set accessor with two indexes</returns>
        public static StructIndex2SetAction<object, TIndex, TIndex2, TValue> IndexerSetStruct<TValue, TIndex, TIndex2>
            (this Type source)
        {
            var indexType = typeof(TIndex);
            var indexType2 = typeof(TIndex2);
            return DelegateIndexerSet<StructIndex2SetAction<object, TIndex, TIndex2, TValue>>
                (source, typeof(TValue), indexType, indexType2);
        }

        /// <summary>
        /// Creates delegate for indexer set accessor at specified three indexes from instance as object
        /// </summary>
        /// <typeparam name="TValue">Indexer value type</typeparam>
        /// <typeparam name="TIndex">First index parameter type</typeparam>
        /// <typeparam name="TIndex2">Second index parameter type</typeparam>
        /// <typeparam name="TIndex3">Third index parameter type</typeparam>
        /// <param name="source">Type with defined indexer</param>
        /// <returns>Delegate for indexer set accessor with three indexes</returns>
        public static Action<object, TIndex, TIndex2, TIndex3, TValue> IndexerSet<TValue, TIndex, TIndex2, TIndex3>(
            this Type source)
        {
            var indexType = typeof(TIndex);
            var indexType2 = typeof(TIndex2);
            var indexType3 = typeof(TIndex3);
            return DelegateIndexerSet<Action<object, TIndex, TIndex2, TIndex3, TValue>>
                (source, typeof(TValue), indexType, indexType2, indexType3);
        }

        /// <summary>
        /// Creates delegate for indexer set accessor at specified three indexes from structure as object
        /// passed by reference
        /// </summary>
        /// <typeparam name="TValue">Indexer value type</typeparam>
        /// <typeparam name="TIndex">First index parameter type</typeparam>
        /// <typeparam name="TIndex2">Second index parameter type</typeparam>
        /// <typeparam name="TIndex3">Third index parameter type</typeparam>
        /// <param name="source">Type with defined indexer</param>
        /// <returns>Delegate for indexer set accessor with three indexes</returns>
        public static StructIndex3SetAction<object, TIndex, TIndex2, TIndex3, TValue>
            IndexerSetStruct<TValue, TIndex, TIndex2, TIndex3>(this Type source)
        {
            var indexType = typeof(TIndex);
            var indexType2 = typeof(TIndex2);
            var indexType3 = typeof(TIndex3);
            return DelegateIndexerSet<StructIndex3SetAction<object, TIndex, TIndex2, TIndex3, TValue>>
                (source, typeof(TValue), indexType, indexType2, indexType3);
        }

        /// <summary>
        /// Creates delegate for indexer set accessor at specified three indexes from instance as object
        /// </summary>
        /// <typeparam name="TSource">Source type with defined indexer</typeparam>
        /// <typeparam name="TValue">Indexer value type</typeparam>
        /// <typeparam name="TIndex">Index parameter type</typeparam>
        /// <returns>Delegate for indexer set accessor with specified index</returns>
        public static Action<TSource, TIndex, TValue> IndexerSet<TSource, TValue, TIndex>()
            where TSource : class
        {
            var sourceType = typeof(TSource);
            var propertyInfo = sourceType.GetIndexerPropertyInfo(new[] { typeof(TIndex) });
            return propertyInfo?.SetMethod?.CreateDelegate<Action<TSource, TIndex, TValue>>();
        }

        /// <summary>
        /// Creates delegate for indexer set accessor at specified index from structure passed by reference
        /// </summary>
        /// <typeparam name="TSource">Source type with defined indexer</typeparam>
        /// <typeparam name="TValue">Indexer value type</typeparam>
        /// <typeparam name="TIndex">Index parameter type</typeparam>
        /// <returns>Delegate for indexer set accessor with specified index</returns>
        public static StructIndex1SetAction<TSource, TIndex, TValue> IndexerSetStruct<TSource, TValue, TIndex>()
            where TSource : struct
        {
            var sourceType = typeof(TSource);
            var propertyInfo = sourceType.GetIndexerPropertyInfo(new[] { typeof(TIndex) });
            return propertyInfo?.SetMethod?.CreateDelegate<StructIndex1SetAction<TSource, TIndex, TValue>>();
        }

        /// <summary>
        /// Creates delegate for indexer set accessor with two indexes
        /// </summary>
        /// <typeparam name="TSource">Source type with defined indexer</typeparam>
        /// <typeparam name="TValue">Indexer value type</typeparam>
        /// <typeparam name="TIndex">First index parameter type</typeparam>
        /// <typeparam name="TIndex2">Second index parameter type</typeparam>
        /// <returns>Delegate for indexer set accessor with two indexes</returns>
        public static Action<TSource, TIndex, TIndex2, TValue> IndexerSet<TSource, TValue, TIndex, TIndex2>()
            where TSource : class
        {
            var propertyInfo = typeof(TSource).GetIndexerPropertyInfo(new[] { typeof(TIndex), typeof(TIndex2) });
            return propertyInfo?.SetMethod?.CreateDelegate<Action<TSource, TIndex, TIndex2, TValue>>();
        }

        /// <summary>
        /// Creates delegate for indexer set accessor with two indexes from structure passed by reference
        /// </summary>
        /// <typeparam name="TSource">Source type with defined indexer</typeparam>
        /// <typeparam name="TValue">Indexer value type</typeparam>
        /// <typeparam name="TIndex">First index parameter type</typeparam>
        /// <typeparam name="TIndex2">Second index parameter type</typeparam>
        /// <returns>Delegate for indexer set accessor with two indexes</returns>
        public static StructIndex2SetAction<TSource, TIndex, TIndex2, TValue> IndexerSetStruct
            <TSource, TValue, TIndex, TIndex2>()
            where TSource : struct
        {
            var propertyInfo = typeof(TSource).GetIndexerPropertyInfo(new[] { typeof(TIndex), typeof(TIndex2) });
            return propertyInfo?.SetMethod?.CreateDelegate<StructIndex2SetAction<TSource, TIndex, TIndex2, TValue>>();
        }

        /// <summary>
        /// Creates delegate for indexer set accessor with three indexes from instance
        /// </summary>
        /// <typeparam name="TSource">Source type with defined indexer</typeparam>
        /// <typeparam name="TValue">Indexer value type</typeparam>
        /// <typeparam name="TIndex">First index parameter type</typeparam>
        /// <typeparam name="TIndex2">Second index parameter type</typeparam>
        /// <typeparam name="TIndex3">Third index parameter type</typeparam>
        /// <returns>Delegate for indexer set accessor with three indexes</returns>
        public static Action<TSource, TIndex, TIndex2, TIndex3, TValue> IndexerSet
            <TSource, TValue, TIndex, TIndex2, TIndex3>()
        {
            var propertyInfo = typeof(TSource).GetIndexerPropertyInfo(new[] { typeof(TIndex), typeof(TIndex2), typeof(TIndex3) });
            return propertyInfo?.SetMethod?.CreateDelegate<Action<TSource, TIndex, TIndex2, TIndex3, TValue>>();
        }

        /// <summary>
        /// Creates delegate for indexer set accessor with three indexes from structure passed by reference
        /// </summary>
        /// <typeparam name="TSource">Source type with defined indexer</typeparam>
        /// <typeparam name="TValue">Indexer value type</typeparam>
        /// <typeparam name="TIndex">First index parameter type</typeparam>
        /// <typeparam name="TIndex2">Second index parameter type</typeparam>
        /// <typeparam name="TIndex3">Third index parameter type</typeparam>
        /// <returns>Delegate for indexer set accessor with three indexes</returns>
        public static StructIndex3SetAction<TSource, TIndex, TIndex2, TIndex3, TValue> IndexerSetStruct
            <TSource, TValue, TIndex, TIndex2, TIndex3>() where TSource : struct
        {
            var propertyInfo = typeof(TSource).GetIndexerPropertyInfo(
                new[] { typeof(TIndex), typeof(TIndex2), typeof(TIndex3) });
            return propertyInfo?.SetMethod?.CreateDelegate<StructIndex3SetAction<TSource, TIndex, TIndex2, TIndex3, TValue>>();
        }

#if !NET35
        /// <summary>
        /// Creates delegate for indexer set accessor with unspecified number of indexes from instance as object
        /// </summary>
        /// <param name="source">Type with defined indexer</param>
        /// <param name="indexTypes">Collection of index parameters types</param>
        /// <returns>Delegate for indexer set accessor with unspecified number of indexes</returns>
        public static Action<object, object[], object> IndexerSetNew(this Type source, params Type[] indexTypes)
        {
            return source.IndexerSetObjectsImpl<Action<object, object[], object>>(indexTypes);
        }

        /// <summary>
        /// Creates delegate for indexer set accessor with unspecified number of indexes from instance as object
        /// </summary>
        /// <param name="source">Type with defined indexer</param>
        /// <param name="returnType">Type of value to set</param>
        /// <param name="indexTypes">Collection of index parameters types</param>
        /// <returns>Delegate for indexer set accessor with unspecified number of indexes</returns>
        /// <remarks>
        /// <paramref name="returnType"/> parameter is not necessary, but for compatibility new method was created.
        /// This one will be removed in next release.
        /// </remarks>
        [Obsolete("Use IndexerSetNew instead")]
        public static Action<object, object[], object> IndexerSet(this Type source, Type returnType,
            params Type[] indexTypes)
        {
            return source.IndexerSetObjectsImpl<Action<object, object[], object>>(indexTypes);
        }

        /// <summary>
        /// Creates delegate for indexer set accessor with unspecified number of indexes from instance as object
        /// </summary>
        /// <param name="source">Type with defined indexer</param>
        /// <param name="indexTypes">Collection of index parameters types</param>
        /// <returns>Delegate for indexer set accessor with unspecified number of indexes</returns>
        public static StructIndexesSetAction<object, object> IndexerSetStructNew(this Type source, 
            params Type[] indexTypes)
        {
            return source.IndexerSetObjectsImpl<StructIndexesSetAction<object, object>>(indexTypes);
        }

        /// <summary>
        /// Creates delegate for indexer set accessor with unspecified number of indexes from instance as object
        /// </summary>/// <param name="source">Type with defined indexer</param>
        /// <param name="returnType">Not used</param>
        /// <param name="indexTypes">Collection of index parameters types</param>
        /// <returns>Delegate for indexer set accessor with unspecified number of indexes</returns>
        /// <remarks>
        /// <paramref name="returnType"/> parameter is not necessary, but for compatibility new method was created.
        /// This one will be removed in next release.
        /// </remarks>
        [Obsolete("Use IndexerSetStructNew instead")]
        public static StructIndexesSetAction<object, object> IndexerSetStruct(this Type source, Type returnType,
            params Type[] indexTypes)
        {
            return source.IndexerSetObjectsImpl<StructIndexesSetAction<object, object>>(indexTypes);
        }

        private static TDelegate IndexerSetObjectsImpl<TDelegate>(this Type source,
            params Type[] indexTypes)
            where TDelegate : class
        {
            var indexerInfo = source.GetIndexerPropertyInfo(indexTypes);
            if (indexerInfo?.SetMethod == null)
            {
                return null;
            }
            ParameterExpression sourceObjectParam;
            if (source.IsClassType() || source.GetTypeInfo().IsInterface)
            {
                sourceObjectParam = Expression.Parameter(typeof(object), "source");
            }
            else
            {
                sourceObjectParam = Expression.Parameter(typeof(object).MakeByRefType(), "source");
            }
            var indexesParam = Expression.Parameter(typeof(object[]), "indexes");
            var valueParam = Expression.Parameter(typeof(object), "value");
            var paramsExpression = new Expression[indexTypes.Length + 1];
            for (var i = 0; i < indexTypes.Length; i++)
            {
                var indexType = indexTypes[i];
                paramsExpression[i] = Expression.Convert(Expression.ArrayIndex(indexesParam, Expression.Constant(i)),
                    indexType);
            }
            paramsExpression[indexTypes.Length] = Expression.Convert(valueParam, indexerInfo.PropertyType);
            Expression returnExpression;
            if (source.IsClassType())
            {
                returnExpression = Expression.Call(Expression.Convert(sourceObjectParam, source),
                    indexerInfo.SetMethod, paramsExpression);
            }
            else
            {
                var structVariable = Expression.Variable(source, "struct");
                returnExpression = Expression.Block(typeof(object), new[] { structVariable },
                    Expression.Assign(structVariable, Expression.Convert(sourceObjectParam, source)),
                    Expression.Call(structVariable, indexerInfo.SetMethod, paramsExpression),
                    Expression.Assign(sourceObjectParam, Expression.Convert(structVariable, typeof(object)))
                );
            }
            return Expression.Lambda<TDelegate>(returnExpression, sourceObjectParam, indexesParam, valueParam).Compile();
        }
#endif
        private static TDelegate DelegateIndexerSet<TDelegate>(Type source, Type returnType,
            params Type[] indexTypes)
            where TDelegate : class
        {
            //TODO: check if returnType value is correct
            var indexerInfo = source.GetIndexerPropertyInfo(indexTypes);
            if (indexerInfo?.SetMethod == null)
            {
                return null;
            }
            ParameterExpression sourceObjectParam;
            if (source.IsClassType() || source.GetTypeInfo().IsInterface)
            {
                sourceObjectParam = Expression.Parameter(typeof(object), "source");
            }
            else
            {
                sourceObjectParam = Expression.Parameter(typeof(object).MakeByRefType(), "source");
            }
            var valueParam = Expression.Parameter(returnType, "value");
            var indexExpressions = new ParameterExpression[indexTypes.Length];
            for (var i = 0; i < indexTypes.Length; i++)
            {
                var indexType = indexTypes[i];
                indexExpressions[i] = Expression.Parameter(indexType, "i" + i);
            }
            var callArgs = indexExpressions.Concat(new[] { valueParam }).ToArray();
            var paramsExpressions = new[] { sourceObjectParam }.Concat(callArgs);
            Expression blockExpr;
            if (!(source.IsClassType() || source.GetTypeInfo().IsInterface))
            {
#if !NET35
                var structVariable = Expression.Variable(source, "struct");
                blockExpr = Expression.Block(typeof(object), new[] { structVariable },
                    Expression.Assign(structVariable, Expression.Convert(sourceObjectParam, source)),
                    Expression.Call(structVariable, indexerInfo.SetMethod, callArgs),
                    Expression.Assign(sourceObjectParam, Expression.Convert(structVariable, typeof(object)))
                );
#else
                throw new ArgumentException("This is not supported for structures in .NET 3.5");
#endif
            }
            else
            {
                blockExpr = Expression.Call(Expression.Convert(sourceObjectParam, source),
                    indexerInfo.SetMethod, callArgs);
            }
            return Expression.Lambda<TDelegate>(blockExpr, paramsExpressions).Compile();
        }

    }
}