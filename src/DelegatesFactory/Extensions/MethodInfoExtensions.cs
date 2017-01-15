using System;
using System.Reflection;
using Delegates.Helper;

namespace Delegates.Extensions
{
    internal static class MethodInfoExtensions
    {
        public static TDelegate CreateDelegate<TDelegate>(this MethodInfo method)
            where TDelegate : class
        {
#if NET45 || NETCORE || NET4 || PORTABLE||STANDARD
            DelegateHelper.CheckDelegateReturnType<TDelegate>(method);
            return method.CreateDelegate(typeof(TDelegate)) as TDelegate;
#elif NET35      
            return Delegate.CreateDelegate(typeof(TDelegate), method, true) as TDelegate;
#endif
        }

        public static Delegate CreateDelegate(this MethodInfo method, Type delegateType)
        {
#if NET45 || NETCORE||STANDARD
            return method.CreateDelegate(delegateType);
#elif NET35 || NET4
            return Delegate.CreateDelegate(delegateType, method, true);
#elif PORTABLE
            return Delegate.CreateDelegate(delegateType, method);
#endif
        }

        public static void IsEventArgsTypeCorrect<TEventArgs>(this MethodInfo method)
        {
            var argsType = method.GetParameters()[0].ParameterType;
            if (argsType != typeof(TEventArgs))
            {
                throw new ArgumentException("Provided event args type '" + typeof(TEventArgs).Name + "' is not compatible with expected type '" + argsType.Name + "'");
            }
        }
    }
}