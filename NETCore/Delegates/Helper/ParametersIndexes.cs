using System;
using System.Linq;
using System.Reflection;

namespace Delegates.Helper
{
    internal static class ParametersIndexes
    {
        public static bool IndexParametersEquals(ParameterInfo[] first, Type[] second)
        {
            if (first.Length != second.Length)
            {
                return false;
            }
            var indexParametersEquals = first.Select((t, i) => t.ParameterType == second[i]).All(p => p);
            return indexParametersEquals;
        }
    }
}
