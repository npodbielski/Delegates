// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeExtensions_NETStandard11.cs" company="Natan Podbielski">
//   Copyright (c) 2016 - 2018 Natan Podbielski. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Reflection;

namespace Delegates.Extensions
{
    internal static class TypeExtensions_NetStandard11
    {
        public static PropertyInfo GetPropertyInfo(this Type source, string name, bool isStatic)
        {
            var propertyInfo = source.GetTypeInfo().GetDeclaredProperty(name);
            if (propertyInfo != null
                && (!isStatic && propertyInfo.IsPropertyStatic() ||
                    isStatic && !propertyInfo.IsPropertyStatic()))
            {
                propertyInfo = null;
            }
            return propertyInfo;
        }

        private static bool IsPropertyStatic(this PropertyInfo propertyInfo)
        {
            return propertyInfo.GetMethod != null && propertyInfo.GetMethod.IsStatic
                || propertyInfo.SetMethod != null && propertyInfo.SetMethod.IsStatic;
        }

        public static MethodInfo GetMethods(this Type source, string methodName)
        {
            return source.GetTypeInfo().GetDeclaredMethod(methodName);
        }
    }
}