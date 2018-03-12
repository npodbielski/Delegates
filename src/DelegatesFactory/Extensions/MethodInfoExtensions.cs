// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MethodInfoExtensions.cs" company="Natan Podbielski">
//   Copyright (c) 2016 - 2018 Natan Podbielski. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

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
#if NET4 || NET45 || NET46 || NETCORE || PORTABLE || STANDARD
            DelegateHelper.CheckDelegateReturnType<TDelegate>(method);
            return method.CreateDelegate(typeof(TDelegate)) as TDelegate;
#elif NET35
            return Delegate.CreateDelegate(typeof(TDelegate), method, true) as TDelegate;
#endif
        }

        public static Delegate CreateDelegate(this MethodInfo method, Type delegateType)
        {
#if NET45 || NET46 || NETCORE|| STANDARD
            return method.CreateDelegate(delegateType);
#elif NET35 || NET4
            return Delegate.CreateDelegate(delegateType, method, true);
#elif PORTABLE
            return Delegate.CreateDelegate(delegateType, method);
#endif
        }

        public static void IsEventArgsTypeCorrect(this MethodInfo method, Type eventArgsType)
        {
            var argsType = method.GetParameters()[0].ParameterType.GetMethod("Invoke")
                .GetParameters()[1].ParameterType;
            DelegateHelper.IsEventArgsTypeCorrect(argsType, eventArgsType, false);
        }

        public static void IsEventArgsTypeCorrect<TEventArgs>(this MethodInfo method)
        {
            IsEventArgsTypeCorrect(method, typeof(TEventArgs));
        }
    }
}