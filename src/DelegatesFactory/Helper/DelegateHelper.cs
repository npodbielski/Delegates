using System;
using System.Linq;
using System.Reflection;
#if !(NETCORE || NET45)
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

        public static Type[] GetDelegateArguments<TDelegate>() where TDelegate : class
        {
            var invokeMethod = typeof(TDelegate).GetTypeInfo().GetMethod("Invoke");
            if (invokeMethod == null)
            {
                throw new ArgumentException(
                    $"TDelegate type do not have Invoke method. Check if delegate base class is {typeof(Delegate).FullName}.");
            }
            return invokeMethod.GetParameters().Select(p => p.ParameterType).ToArray();
        }

        public static Type GetDelegateReturnType<TDelegate>() where TDelegate : class
        {
            var invokeMethod = typeof(TDelegate).GetTypeInfo().GetMethod("Invoke");
            return invokeMethod.ReturnType;
        }

        public static void CheckDelegateReturnType<TDelegate>(MethodInfo method) where TDelegate : class
        {
            var delegateReturnType = GetDelegateReturnType<TDelegate>();
            if (delegateReturnType != method.ReturnType)
            {
                if (method.ReturnType == typeof(void))
                {
                    throw new ArgumentException(
                        $"TDelegate return type is non-void and method found in {method.DeclaringType.FullName} " +
                        $"have return type of void. Either use apropriate method of {typeof(DelegateFactory).Name} " +
                        $"for void methods delegates or change TDelegate type for void return type (i.e. " +
                        $"{typeof(Action<>).Name} instead of {typeof(Func<>).Name}).");
                }
                if (delegateReturnType == typeof(void))
                {
                    throw new ArgumentException(
                        $"TDelegate return type is void and method found in {method.DeclaringType.FullName} have " +
                        $"return type of {method.ReturnType.FullName}. Either use apropriate method of " +
                        $"{typeof(DelegateFactory).Name} for non-void methods delegates or change TDelegate type for" +
                        $" non-void");
                }
            }
        }
    }
}
