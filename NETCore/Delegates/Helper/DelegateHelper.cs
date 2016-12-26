using System;
#if NETCORE||NET45
using System.Reflection; 
#else
using Delegates.Extensions;
#endif

namespace Delegates.Helper
{
    internal static class DelegateHelper
    {
        private static readonly Type DelegateType = typeof(Delegate);

        public static void CheckDelegate<TDelegate>() where TDelegate : class
        {
            if (!typeof(TDelegate).GetTypeInfo().IsSubclassOf(DelegateType))
            {
                throw new ArgumentException("TDelegate type param must derive from " + DelegateType.FullName);
            }
        }
    }
}
