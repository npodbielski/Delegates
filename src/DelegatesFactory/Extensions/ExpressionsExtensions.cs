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
    /// <summary>
    /// Expression extension methods class
    /// </summary>
    internal static class ExpressionsExtensions
    {
#if NET35
        /// <summary>
        /// Casts list of <see cref="ParameterExpression"/> to .NET framework version collection compatible with second 
        /// parameter of  
        /// <see cref="Expression.New(System.Reflection.ConstructorInfo,System.Linq.Expressions.Expression[])"/>
        /// method
        /// </summary>
        /// <param name="parameters">Collection of <see cref="ParameterExpression"/></param>
        /// <returns>Collection compatible with method
        /// <see cref="Expression.New(System.Reflection.ConstructorInfo,System.Linq.Expressions.Expression[])"/>
        /// second parameter.
        /// </returns>
        public static Expression[] GetNewExprParams(this List<ParameterExpression> parameters)
#else
        /// <summary>
        /// Casts list of <see cref="ParameterExpression"/> to .NET framework version collection compatible with second 
        /// parameter of  
        /// <see cref="Expression.New(System.Reflection.ConstructorInfo, System.Collections.Generic.IEnumerable{Expression})"/>
        /// method
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns>Collection compatible with method
        /// <see cref="Expression.New(System.Reflection.ConstructorInfo, System.Collections.Generic.IEnumerable{Expression})"/>
        /// second parameter.
        /// </returns>
        public static List<ParameterExpression> GetNewExprParams(this List<ParameterExpression> parameters)
#endif
        {
            return parameters
#if NET35
                .Cast<Expression>().ToArray()
#endif
                ;
        }

#if NET35
        /// <summary>
        /// Casts list of <see cref="ParameterExpression"/> to .NET framework version collection compatible with second parameter
        /// of <see cref="Expression.Lambda{TDelegate}(System.Linq.Expressions.Expression,System.Linq.Expressions.ParameterExpression[])"/>
        /// method.
        /// </summary>
        /// <param name="parameters">Collection of <see cref="ParameterExpression"/></param>
        /// <returns>Collection compatible with method
        /// <see cref="Expression.Lambda{TDelegate}(System.Linq.Expressions.Expression,System.Linq.Expressions.ParameterExpression[])"/>
        /// second parameter.
        /// </returns>
        public static ParameterExpression[] GetLambdaExprParams(this List<ParameterExpression> parameters)
#else
        /// <summary>
        /// Casts list of <see cref="ParameterExpression"/> to .NET framework version collection compatible with second parameter
        /// of <see cref="Expression.Lambda{TDelegate}(System.Linq.Expressions.Expression,System.Collections.Generic.IEnumerable{ParameterExpression})"/>
        /// method.
        /// </summary>
        /// <param name="parameters">Collection of <see cref="ParameterExpression"/></param>
        /// <returns>Collection compatible with method
        /// <see cref="Expression.Lambda{TDelegate}(System.Linq.Expressions.Expression,System.Collections.Generic.IEnumerable{ParameterExpression})"/>
        /// second parameter.
        /// </returns>
        public static List<ParameterExpression> GetLambdaExprParams(this List<ParameterExpression> parameters)
#endif
        {
            return parameters
#if NET35
                .ToArray()
#endif
                ;
        }

#if NET35
        /// <summary>
        /// Concatenates collection of <see cref="ParameterExpression"/> with instance parameter 
        /// <see cref="ParameterExpression"/> to .NET framework version collection compatible with second parameter
        /// of <see cref="Expression.Call(Expression, System.Reflection.MethodInfo,System.Linq.Expressions.Expression[])"/>
        /// method.
        /// </summary>
        /// <param name="parameters">Collection of call parameters</param>
        /// <param name="sourceParam">Call instance parameter</param>
        /// <returns>Collection compatible with method
        /// <see cref="Expression.Call(Expression, System.Reflection.MethodInfo,System.Linq.Expressions.Expression[])"/>
        /// second parameter.
        /// </returns>
        public static ParameterExpression[] GetLambdaExprParams(this IEnumerable<ParameterExpression> parameters, 
            ParameterExpression sourceParam)
#else
        /// <summary>
        /// Casts list of <see cref="ParameterExpression"/> with instance parameter 
        /// <see cref="ParameterExpression"/> to .NET framework version collection compatible with second parameter
        /// of <see cref="Expression.Lambda{TDelegate}(System.Linq.Expressions.Expression,System.Collections.Generic.IEnumerable{ParameterExpression})"/>
        /// method.
        /// </summary>
        /// <param name="parameters">Collection of <see cref="ParameterExpression"/></param>
        /// <param name="sourceParam">Source instance <see cref="ParameterExpression"/></param>
        /// <returns>Collection compatible with method
        /// <see cref="Expression.Lambda{TDelegate}(System.Linq.Expressions.Expression,System.Collections.Generic.IEnumerable{ParameterExpression})"/>
        /// second parameter.
        /// </returns>
        public static IEnumerable<ParameterExpression> GetLambdaExprParams(this IEnumerable<ParameterExpression> parameters, ParameterExpression sourceParam)
#endif
        {
            return new[] { sourceParam }.Concat(parameters)
#if NET35
                .ToArray()
#endif
                ;
        }

#if NET35
        /// <summary>
        /// Casts collection of <see cref="ParameterExpression"/> to .NET framework version collection compatible 
        /// with second parameter
        /// of <see cref="Expression.Call(System.Linq.Expressions.Expression,System.Reflection.MethodInfo,System.Linq.Expressions.Expression[])"/>
        /// method.
        /// </summary>
        /// <param name="parameters">Collection of call parameters</param>
        /// <returns>Collection compatible with method
        /// <see cref="Expression.Call(System.Linq.Expressions.Expression,System.Reflection.MethodInfo,System.Linq.Expressions.Expression[])"/>
        /// second parameter.
        /// </returns>
        public static Expression[] GetCallExprParams(this IEnumerable<ParameterExpression> parameters)
#else
        /// <summary>
        /// Casts list of <see cref="ParameterExpression"/> to .NET framework version collection compatible with second parameter
        /// of <see cref="Expression.Call(System.Linq.Expressions.Expression,System.Reflection.MethodInfo,System.Collections.Generic.IEnumerable{Expression})"/>
        /// method.
        /// </summary>
        /// <param name="parameters">Collection of <see cref="ParameterExpression"/></param>
        /// <returns>Collection compatible with method
        /// <see cref="Expression.Call(System.Linq.Expressions.Expression,System.Reflection.MethodInfo,System.Collections.Generic.IEnumerable{Expression})"/>
        /// second parameter.
        /// </returns>
        public static IEnumerable<ParameterExpression> GetCallExprParams(this IEnumerable<ParameterExpression> parameters)
#endif
        {
            return parameters
#if NET35
                .Cast<Expression>().ToArray()
#endif
                ;
        }
    }
}