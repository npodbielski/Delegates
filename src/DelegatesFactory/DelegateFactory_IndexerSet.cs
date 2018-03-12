// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DelegateFactory_IndexerSet.cs" company="Natan Podbielski">
//   Copyright (c) 2016 - 2018 Natan Podbielski. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
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
        ///     Creates delegate for indexer set accessor at specified index from instance as object
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
        ///     Creates delegate for indexer set accessor at specified index from structure as object passed by reference
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
        ///     Creates delegate for indexer set accessor at specified two indexes from instance as object
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
        ///     Creates delegate for indexer set accessor at specified two indexes from structure as object passed by reference
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
        ///     Creates delegate for indexer set accessor at specified three indexes from instance as object
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
        ///     Creates delegate for indexer set accessor at specified three indexes from structure as object
        ///     passed by reference
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
        ///     Creates delegate for indexer set accessor at specified three indexes from instance as object
        /// </summary>
        /// <typeparam name="TSource">Source type with defined indexer</typeparam>
        /// <typeparam name="TValue">Indexer value type</typeparam>
        /// <typeparam name="TIndex">Index parameter type</typeparam>
        /// <returns>Delegate for indexer set accessor with specified index</returns>
        public static Action<TSource, TIndex, TValue> IndexerSet<TSource, TValue, TIndex>()
            where TSource : class
        {
            return IndexerSetFromSetMethod<Action<TSource, TIndex, TValue>, TSource, TValue>
                (new[] {typeof(TIndex)});
        }

        /// <summary>
        ///     Creates delegate for indexer set accessor at specified index from structure passed by reference
        /// </summary>
        /// <typeparam name="TSource">Source type with defined indexer</typeparam>
        /// <typeparam name="TValue">Indexer value type</typeparam>
        /// <typeparam name="TIndex">Index parameter type</typeparam>
        /// <returns>Delegate for indexer set accessor with specified index</returns>
        public static StructIndex1SetAction<TSource, TIndex, TValue> IndexerSetStruct<TSource, TValue, TIndex>()
            where TSource : struct
        {
            return IndexerSetFromSetMethod<StructIndex1SetAction<TSource, TIndex, TValue>, TSource, TValue>
                (new[] {typeof(TIndex)});
        }

        /// <summary>
        ///     Creates delegate for indexer set accessor with two indexes
        /// </summary>
        /// <typeparam name="TSource">Source type with defined indexer</typeparam>
        /// <typeparam name="TValue">Indexer value type</typeparam>
        /// <typeparam name="TIndex">First index parameter type</typeparam>
        /// <typeparam name="TIndex2">Second index parameter type</typeparam>
        /// <returns>Delegate for indexer set accessor with two indexes</returns>
        public static Action<TSource, TIndex, TIndex2, TValue> IndexerSet<TSource, TValue, TIndex, TIndex2>()
            where TSource : class
        {
            return IndexerSetFromSetMethod<Action<TSource, TIndex, TIndex2, TValue>, TSource, TValue>
                (new[] {typeof(TIndex), typeof(TIndex2)});
        }

        /// <summary>
        ///     Creates delegate for indexer set accessor with two indexes from structure passed by reference
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
            return IndexerSetFromSetMethod<
                    StructIndex2SetAction<TSource, TIndex, TIndex2, TValue>, TSource, TValue>
                (new[] {typeof(TIndex), typeof(TIndex2)});
        }

        /// <summary>
        ///     Creates delegate for indexer set accessor with three indexes from instance
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
            return IndexerSetFromSetMethod<Action<TSource, TIndex, TIndex2, TIndex3, TValue>, TSource, TValue>
                (new[] {typeof(TIndex), typeof(TIndex2), typeof(TIndex3)});
        }

        /// <summary>
        ///     Creates delegate for indexer set accessor with three indexes from structure passed by reference
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
            return IndexerSetFromSetMethod<
                    StructIndex3SetAction<TSource, TIndex, TIndex2, TIndex3, TValue>, TSource, TValue>
                (new[] {typeof(TIndex), typeof(TIndex2), typeof(TIndex3)});
        }

        private static TDelegate DelegateIndexerSet<TDelegate>(Type source, Type returnType,
            params Type[] indexTypes) where TDelegate : class
        {
            var indexerInfo = source.GetIndexerPropertyInfo(indexTypes);
            if (indexerInfo?.SetMethod == null) return null;
            ParameterExpression sourceObjectParam;
            if (source.GetTypeInfo().IsClass || source.GetTypeInfo().IsInterface)
                sourceObjectParam = Expression.Parameter(typeof(object), "source");
            else
                sourceObjectParam = Expression.Parameter(typeof(object).MakeByRefType(), "source");
            var valueParam = Expression.Parameter(returnType, "value");
            var indexExpressions = new ParameterExpression[indexTypes.Length];
            for (var i = 0; i < indexTypes.Length; i++)
            {
                var indexType = indexTypes[i];
                indexExpressions[i] = Expression.Parameter(indexType, "i" + i);
            }

            Expression valueArg = null;
            if (returnType == indexerInfo.PropertyType)
                valueArg = valueParam;
            else if (returnType.IsValidReturnType(indexerInfo.PropertyType))
                valueArg = Expression.Convert(valueParam, indexerInfo.PropertyType);
            var callArgs = indexExpressions.Concat(new[] {valueArg}).ToArray();
            var paramsExpressions = new[] {sourceObjectParam}.Concat(indexExpressions)
                .Concat(new[] {valueParam});
            Expression blockExpr;
            if (!(source.GetTypeInfo().IsClass || source.GetTypeInfo().IsInterface))
            {
#if !NET35
                var structVariable = Expression.Variable(source, "struct");
                blockExpr = Expression.Block(typeof(object), new[] {structVariable},
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

        private static TDelegate IndexerSetConvertValue<TDelegate, TSource, TValue>
            (MethodInfo setMethod, Type indexerType, ParameterExpression[] indexParams)
        {
            var sourceParam = Expression.Parameter(typeof(TSource), "source");
            var valueParam = Expression.Parameter(typeof(TValue), "value");
            var callExpress = Expression.Call(sourceParam, setMethod, indexParams.Cast<Expression>().Concat(
                new[] {Expression.Convert(valueParam, indexerType)}));
            var lamdba = Expression.Lambda<TDelegate>(callExpress, new[] {sourceParam}.Concat(indexParams)
                .Concat(new[] {valueParam}));
            return lamdba.Compile();
        }

        private static TDelegate IndexerSetFromSetMethod<TDelegate, TSource, TValue>(Type[] indexTypes)
            where TDelegate : class
        {
            var sourceType = typeof(TSource);
            var propertyInfo = sourceType.GetIndexerPropertyInfo(indexTypes);
            if (propertyInfo != null)
            {
                if (propertyInfo.PropertyType == typeof(TValue))
                    return propertyInfo.SetMethod?.CreateDelegate<TDelegate>();
                if (propertyInfo.PropertyType.IsValidReturnType(typeof(TValue)))
                {
                    var indexParams = new ParameterExpression[indexTypes.Length];
                    for (var i = 0; i < indexTypes.Length; i++)
                    {
                        var indexType = indexTypes[i];
                        indexParams = new[] {Expression.Parameter(indexType, "index" + i)};
                    }

                    return IndexerSetConvertValue<TDelegate, TSource, TValue>
                        (propertyInfo.SetMethod, propertyInfo.PropertyType, indexParams);
                }
            }

            return null;
        }
    }
}