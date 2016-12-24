// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeExtensions.cs" company="Natan Podbielski">
//   Copyright (c) 2016 - 2016 Natan Podbielski. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Delegates.Extensions
{
    public static class TypeExtensions
    {
        public static bool CanBeAssignedFrom(this Type destination, Type source)
        {
            if (source == null || destination == null)
            {
                return false;
            }

            if (destination == source || source.GetTypeInfo().IsSubclassOf(destination))
            {
                return true;
            }

            if (destination.GetTypeInfo().IsInterface)
            {
                return source.ImplementsInterface(destination);
            }

            if (!destination.IsGenericParameter)
            {
                return false;
            }

            var constraints = destination.GetTypeInfo().GetGenericParameterConstraints();
            return constraints.All(t1 => t1.CanBeAssignedFrom(source));
        }

        private static bool ImplementsInterface(this Type source, Type interfaceType)
        {
            while (source != null)
            {
                var interfaces = source.GetTypeInfo().GetInterfaces();
                if (interfaces.Any(i => i == interfaceType
                                        || i.ImplementsInterface(interfaceType)))
                {
                    return true;
                }

                source = source.GetTypeInfo().BaseType;
            }
            return false;
        }

        public static Type[] GenericTypeArguments(this Type source)
        {
#if NET35||NET4||PORTABLE
            return source.GetGenericArguments();
#elif NET45||NETCORE
            return source.GetTypeInfo().GenericTypeArguments;
#endif
        }

        public static bool IsTypeClass(this Type source)
        {
#if NET35||NET4||PORTABLE
            return source.IsClass;
#elif NET45||NETCORE
            return source.GetTypeInfo().IsClass;
#endif
        }

        public static bool IsValueType(this Type source)
        {
#if NET35||NET4||PORTABLE
            return source.IsValueType;
#elif NET45||NETCORE
            return source.GetTypeInfo().IsValueType;
#endif
        }

#if NET45 || NETCORE
        public static MethodInfo GetMethod(this Type source, string methodName)
        {
            return source.GetTypeInfo().GetMethod(methodName);
        }
#endif

#if NET35||NET4||PORTABLE
        public static Type GetTypeInfo(this Type source)
        {
            return source;
        }
#endif

        public static TDelegate CreateDelegate<TDelegate>(this MethodInfo method)
            where TDelegate : class
        {
#if NET45 || NETCORE||NET4||PORTABLE
            return method.CreateDelegate(typeof(TDelegate)) as TDelegate;
#elif NET35      
            return Delegate.CreateDelegate(typeof(TDelegate), method, true) as TDelegate;
#endif
        }

        public static Delegate CreateDelegate(this MethodInfo method, Type delegateType)
        {
#if NET45 || NETCORE
            return method.CreateDelegate(delegateType);
#elif NET35 || NET4
            return Delegate.CreateDelegate(delegateType, method, true);
#elif PORTABLE
            return Delegate.CreateDelegate(delegateType, method);
#endif
        }

        public static List<ParameterExpression> GetParamsFromTypes(this Type[] types)
        {
#if NET35
            var index = 0;
#endif
            var parameters = types
#if NET35
                .Select(a => Expression.Parameter(a, "p" + index++))
#elif NET45 || NETCORE || NET4||PORTABLE
                .Select(Expression.Parameter)
#endif
                .ToList();
            return parameters;
        }
    }
}