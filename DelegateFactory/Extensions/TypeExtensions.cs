// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeExtensions.cs" company="Natan Podbielski">
//   Copyright (c) 2016 - 2016 Natan Podbielski. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;

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

            if (destination == source || source.IsSubclassOf(destination))
            {
                return true;
            }

            if (destination.IsInterface)
            {
                return source.ImplementsInterface(destination);
            }

            if (!destination.IsGenericParameter)
            {
                return false;
            }

            var constraints = destination.GetGenericParameterConstraints();
            return constraints.All(t1 => t1.CanBeAssignedFrom(source));
        }

        private static bool ImplementsInterface(this Type source, Type interfaceType)
        {
            while (source != null)
            {
                var interfaces = source.GetInterfaces();
                if (interfaces.Any(i => i == interfaceType
                                        || i.ImplementsInterface(interfaceType)))
                {
                    return true;
                }

                source = source.BaseType;
            }
            return false;
        }
    }
}