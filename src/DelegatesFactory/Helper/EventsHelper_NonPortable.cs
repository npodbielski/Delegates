// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventsHelper_NonPortable.cs" company="Natan Podbielski">
//   Copyright (c) 2016 - 2018 Natan Podbielski. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#if !(NETSTANDARD1_1 || NETSTANDARD1_5)
using System;
#endif
using System.Reflection;
using System.Reflection.Emit;

namespace Delegates.Helper
{
    internal static partial class EventsHelper
    {
        private static
#if !(NETSTANDARD1_1 || NETSTANDARD1_5)
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
#if NET35 || NET4 || NET45 || NET46
            var type = typeBuilder.CreateType();
#elif NETCORE || STANDARD
            var type = typeBuilder.CreateTypeInfo();
#endif
            return type;
        }

#if NET35 || NET4
      private static AssemblyBuilder GetAssemblyBuilder(AssemblyName assemblyName)
        {
            const AssemblyBuilderAccess assemblyAccess =
#if NET4
                AssemblyBuilderAccess.RunAndCollect;
#elif NET35
            AssemblyBuilderAccess.Run;
#endif
            var assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, assemblyAccess);
            return assemblyBuilder;
        }  
#endif

#if NET45 || NET46 || NETCORE || STANDARD
        private static AssemblyBuilder GetAssemblyBuilder(AssemblyName assemblyName)
        {
            const AssemblyBuilderAccess assemblyAccess = AssemblyBuilderAccess.RunAndCollect;
            var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, assemblyAccess);
            return assemblyBuilder;
        }
#endif
    }
}