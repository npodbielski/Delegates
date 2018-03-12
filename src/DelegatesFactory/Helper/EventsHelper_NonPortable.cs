// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventsHelper_NonPortable.cs" company="Natan Podbielski">
//   Copyright (c) 2016 - 2018 Natan Podbielski. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#if NET35 || NET4 || NET45 || NET46
#define Supports_Type_Creation
using System;
#endif
using System.Reflection;
using System.Reflection.Emit;

namespace Delegates.Helper
{
    internal static partial class EventsHelper
    {
        private const AssemblyBuilderAccess ConververAssemblyAccess =
#if !NET35
            AssemblyBuilderAccess.RunAndCollect;
#else
            AssemblyBuilderAccess.Run;
#endif

        private static
#if Supports_Type_Creation
            Type
#else
            TypeInfo
#endif
            CreateConverterType<TSource, TDest>(AssemblyName assemblyName)
        {
            var assemblyBuilder = GetAssemblyBuilder(assemblyName);
            var module = assemblyBuilder.DefineDynamicModule(assemblyName.Name);
            var typeBuilder = module.DefineType(assemblyName.Name, TypeAttributes.Class | TypeAttributes.Public);
            typeBuilder.DefineDefaultConstructor(MethodAttributes.Public);
            var methodBuilder = typeBuilder.DefineMethod(assemblyName.Name, MethodAttributes.Static |
                MethodAttributes.Public |
                MethodAttributes.Final, CallingConventions.Standard,
                typeof(TDest), new[] {typeof(TSource)});
            var generator = methodBuilder.GetILGenerator();
            var con = typeof(TDest).GetConstructors()[0];
            generator.Emit(OpCodes.Ldarg_0);
            generator.Emit(OpCodes.Ldftn, typeof(TSource).GetMethod("Invoke"));
            generator.Emit(OpCodes.Newobj, con);
            generator.Emit(OpCodes.Ret);
#if Supports_Type_Creation
            var type = typeBuilder.CreateType();
#else
            var type = typeBuilder.CreateTypeInfo();
#endif
            return type;
        }

        private static AssemblyBuilder GetAssemblyBuilder(AssemblyName assemblyName)
        {
#if NET35 || NET4
            return AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, assemblyAccess);
#else
            return AssemblyBuilder.DefineDynamicAssembly(assemblyName, ConververAssemblyAccess);
#endif
        }
    }
}