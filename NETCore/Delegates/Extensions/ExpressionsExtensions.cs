// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExpressionsExtensions.cs" company="Natan Podbielski">
//   Copyright (c) 2016 - 2016 Natan Podbielski. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Delegates.Extensions
{
    public static class ExpressionsExtensions
    {
        public static
#if NET35
            Expression[]
#else
            List<ParameterExpression>
#endif
            GetNewExprParams(this List<ParameterExpression> parameters)
        {
            return parameters
#if NET35
                .Cast<Expression>().ToArray()
#endif
                ;
        }

        public static
#if NET35
            ParameterExpression[]
#else
            List<ParameterExpression>
#endif
            GetLambdaExprParams(this List<ParameterExpression> parameters)
        {
            return parameters
#if NET35
                .ToArray()
#endif
                ;
        }

        public static
#if NET35
            ParameterExpression[]
#else
            IEnumerable<ParameterExpression>
#endif
            GetCallExprParams(this IEnumerable<ParameterExpression> parameters, ParameterExpression sourceParam)
        {
            return new[] {sourceParam}.Concat(parameters)
#if NET35
                .ToArray()
#endif
                ;
        }

        public static
#if NET35
            Expression[]
#else
            IEnumerable<ParameterExpression>
#endif
            GetCallExprParams(this IEnumerable<ParameterExpression> parameters)
        {
            return parameters
#if NET35
                .Cast<Expression>().ToArray()
#endif
                ;
        }
    }
}