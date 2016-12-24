using System;
using System.Linq.Expressions;

namespace Delegates.Extensions
{
#if NET35
    public static class ExpressionExt
    {
        public static ParameterExpression Variable(Type type, string name)
        {
            if (type.IsByRef)
            {
                throw new ArgumentException("Type cannot be reference type!");
            }
            return Expression.Parameter(type, name);
        }
    }
#endif
}
