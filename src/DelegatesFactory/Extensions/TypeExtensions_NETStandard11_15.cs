// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeExtensions_NETStandard11.cs" company="Natan Podbielski">
//   Copyright (c) 2016 - 2018 Natan Podbielski. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Reflection;

internal static class TypeExtensions_NetStandard11_15
{
    public static MethodInfo GetMethod(this Type type, string name)
    {
        return type.GetTypeInfo().GetDeclaredMethod(name);
    }

    public static ConstructorInfo[] GetConstructors(this Type type)
    {
        return type.GetTypeInfo().DeclaredConstructors.ToArray();
    }

    public static MethodInfo[] GetMethods(this TypeInfo type)
    {
        return type.DeclaredMethods.ToArray();
    }
}