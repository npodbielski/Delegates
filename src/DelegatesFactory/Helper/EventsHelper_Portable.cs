// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventsHelper.cs" company="Natan Podbielski">
//   Copyright (c) 2016 - 2018 Natan Podbielski. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Reflection;
using Delegates.Extensions;

namespace Delegates.Helper
{
    internal static partial class EventsHelper
    {
        private static Type CreateConverterType<TSource, TDest>(AssemblyName assemblyName)
        {
            var assemblyBuilder = PortableAssemblyBuilder.DefineDynamicAssembly(assemblyName.Name);
            var module = PortableAssemblyBuilder.DefineDynamicModule(assemblyBuilder, assemblyName.Name);
            var typeBuilder = PortableAssemblyBuilder.DefineType(module, assemblyName.Name);
            var methodBuilder = PortableAssemblyBuilder.DefineMethod(typeBuilder, assemblyName.Name, typeof(TDest),
                new[] { typeof(TSource) });
            var ilGenerator = PortableAssemblyBuilder.GetILGenerator(methodBuilder);
            var con = typeof(TDest).GetTypeInfo().GetConstructors()[0];
            var ldarg0 = PortableAssemblyBuilder.GetOpCode("Ldarg_0");
            var ldftn = PortableAssemblyBuilder.GetOpCode("Ldftn");
            var newObj = PortableAssemblyBuilder.GetOpCode("Newobj");
            var ret = PortableAssemblyBuilder.GetOpCode("Ret");
            PortableAssemblyBuilder.Emit(ilGenerator, ldarg0);
            PortableAssemblyBuilder.Emit(ilGenerator, ldftn, typeof(TSource).GetMethod("Invoke"));
            PortableAssemblyBuilder.Emit(ilGenerator, newObj, con);
            PortableAssemblyBuilder.Emit(ilGenerator, ret);
            var type = PortableAssemblyBuilder.CreateType(typeBuilder);
            return type;
        }
    }
}