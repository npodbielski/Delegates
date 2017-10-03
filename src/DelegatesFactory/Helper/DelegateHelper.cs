// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DelegateHelper.cs" company="Natan Podbielski">
//   Copyright (c) 2016 - 2017 Natan Podbielski. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Reflection;
using Delegates.Extensions;

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
                throw new ArgumentException($"TDelegate type parameter must derive from {DelegateType.FullName}");
            }
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
                        $"have return type of void. Either use appropriate method of {typeof(DelegateFactory).Name} " +
                        $"for void methods delegates or change TDelegate type for void return type (i.e. " +
                        $"{typeof(Action<>).Name} instead of {typeof(Func<>).Name}).");
                }
                if (delegateReturnType == typeof(void))
                {
                    throw new ArgumentException(
                        $"TDelegate return type is void and method found in {method.DeclaringType.FullName} have " +
                        $"return type of {method.ReturnType.FullName}. Either use appropriate method of " +
                        $"{typeof(DelegateFactory).Name} for non-void methods delegates or change TDelegate type for" +
                        $" non-void");
                }
            }
        }

        public static Type[] GetDelegateArguments<TDelegate>() where TDelegate : class
        {
            var delegateType = typeof(TDelegate);
            var invokeMethod = CheckInvokeMethod(delegateType);
            return invokeMethod.GetParameters().Select(p => p.ParameterType).ToArray();
        }

        public static bool IsEventHandler<TDelegate>()
        {
            return typeof(TDelegate).GetGenericTypeDefinition() == typeof(EventHandler<>);
        }

        public static Type GetDelegateReturnType<TDelegate>() where TDelegate : class
        {
            var delegateType = typeof(TDelegate);
            var invokeMethod = CheckInvokeMethod(delegateType);
            return invokeMethod.ReturnType;
        }

        public static Type GetDelegateFirstParameter(this Type delegateType)
        {
            var invokeMethod = CheckInvokeMethod(delegateType);
            var parameters = invokeMethod.GetParameters();
            if (parameters.Length >= 1)
            {
                return parameters.First().ParameterType;
            }
            throw new ArgumentException("TDelegate type do not have parameters. Check if Delegate is defined correctly!");
        }

        public static Type GetDelegateSecondParameter(this Type delegateType)
        {
            var invokeMethod = CheckInvokeMethod(delegateType);
            var parameters = invokeMethod.GetParameters();
            if (parameters.Length == 2)
            {
                return parameters.Skip(1).Take(1).First().ParameterType;
            }
            throw new ArgumentException(
                "TDelegate type do not have 2 parameters. Check if Delegate is defined correctly!");
        }

        private static MethodInfo CheckInvokeMethod(Type delegateType)
        {
            var invokeMethod = delegateType.GetTypeInfo().GetMethod("Invoke");
            if (invokeMethod == null)
            {
                throw new ArgumentException(
                    $"TDelegate type do not have Invoke method. Check if delegate base class is {typeof(Delegate).FullName}.");
            }
            return invokeMethod;
        }

        public static bool IsCompatible<TDelegate>(Type @delegate)
        {
            var firstInvoke = CheckInvokeMethod(typeof(TDelegate));
            var secondInvoke = CheckInvokeMethod(@delegate);
            var parametersCorrect = true;
            var firstMethodParameters = firstInvoke.GetParameters();
            var secondMethodParameters = secondInvoke.GetParameters();
            parametersCorrect &= firstMethodParameters.Length == secondMethodParameters.Length;
            if (parametersCorrect)
            {
                for (var index = 0; index < firstMethodParameters.Length; index++)
                {
                    var parameter = firstMethodParameters[index];
                    parametersCorrect &= parameter.ParameterType == secondMethodParameters[index].ParameterType;
                    if (!parametersCorrect)
                    {
                        throw new ArgumentException($"Type of delegate {typeof(TDelegate).FullName} has incompatible " +
                                                    $"parameter of {parameter.ParameterType.FullName} at index {index} " +
                                                    $"with {secondMethodParameters[index]} of target delegate {@delegate.FullName}");
                    }
                }
            }
            var returnTypeCompatible = firstInvoke.ReturnType == secondInvoke.ReturnType;
            if (!returnTypeCompatible)
            {
                throw new ArgumentException($"Type of delegate {typeof(TDelegate).FullName} has incompatible " +
                                            $"return type of {firstInvoke.ReturnType.FullName} with " +
                                            $"{secondInvoke.ReturnType.FullName} of target delegate {@delegate.FullName}");
            }
            return parametersCorrect;
        }

        public static void IsEventArgsTypeCorrect(Type destinationArgs, Type sourceArgs, bool allowBase)
        {
            if (!allowBase && destinationArgs != sourceArgs
                ||allowBase && !sourceArgs.CanBeAssignedFrom(destinationArgs))
            {
                throw new ArgumentException(
                    $"Provided event args type \'{sourceArgs.Name}\' is not compatible with expected type " +
                    $"\'{destinationArgs.Name}\'");
            }
        }
    }
}