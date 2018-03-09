// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventsHelper_Portable.cs" company="Natan Podbielski">
//   Copyright (c) 2016 - 2018 Natan Podbielski. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Delegates.Helper
{
    public class PortableAssemblyBuilder
    {
        private const string NotSupported = "On this platform delegates types conversion is not supported!";

        private static Type _assemblyBuilderType;

        private static Type _assemblyAccessType;

        private static object _assemblyAccess;

        private static Func<object[], object> _defineDynamicAssemblyDelegate;

        private static Func<object, object[], object> _defineDynamicModuleDelegate;
        private static Func<object, string, TypeAttributes, object> _defineTypeDelegate;

        private static Func<object, string, MethodAttributes, CallingConventions, Type, Type[], object>
            _defineMethodDelegate;

        private static Func<object, object> _getGeneratorDelegate;

        private static Type _opCodesType;

        private static readonly Dictionary<string, object> OpCodesValues = new Dictionary<string, object>();
        private static Action<object, object[]> _emitDelegate1;
        private static Action<object, object[]> _emitDelegateMethod;
        private static Action<object, object[]> _emitDelegateConstr;
        private static Func<object, Type> _createTypeDeleg;
        private static Type _opCodeType;

        public static Type AssemblyBuilderType
        {
            get
            {
                if (_assemblyBuilderType != null)
                {
                    return _assemblyBuilderType;
                }

                try
                {
                    return _assemblyBuilderType = Type.GetType("System.Reflection.Emit.AssemblyBuilder");
                }
                catch (TypeLoadException)
                {
                    throw new NotSupportedException(NotSupported);
                }
            }
        }

        public static Type AssemblyAccessType
        {
            get
            {
                if (_assemblyAccessType != null)
                {
                    return _assemblyAccessType;
                }

                try
                {
                    _assemblyAccessType = Type.GetType("System.Reflection.Emit.AssemblyBuilderAccess");
                    return _assemblyAccessType;
                }
                catch (TypeLoadException)
                {
                    throw new NotSupportedException(NotSupported);
                }
            }
        }

        public static object AssemblyAccess
        {
            get
            {
                if (_assemblyAccess != null)
                {
                    return _assemblyAccess;
                }

                try
                {
                    var values = Enum.GetValues(AssemblyAccessType).Cast<object>().ToArray();
                    var runAndCollect = values.FirstOrDefault(v => v.ToString() == "RunAndCollect");
                    if (runAndCollect == null)
                    {
                        var run = values.FirstOrDefault(v => v.ToString() == "Run");
                        _assemblyAccess = run ?? throw new NotSupportedException(NotSupported);
                    }
                    else
                    {
                        _assemblyAccess = runAndCollect;
                    }

                    return _assemblyAccess;
                }
                catch (TypeLoadException)
                {
                    throw new NotSupportedException(NotSupported);
                }
            }
        }

        public static Type OpCodesType
        {
            get
            {
                if (_opCodesType != null)
                {
                    return _opCodesType;
                }

                try
                {
                    _opCodesType = Type.GetType("System.Reflection.Emit.OpCodes");
                    return _opCodesType;
                }
                catch (TypeLoadException)
                {
                    throw new NotSupportedException(NotSupported);
                }
            }
        }

        public static Type OpCodeType
        {
            get
            {
                if (_opCodeType != null)
                {
                    return _opCodeType;
                }

                try
                {
                    _opCodeType = Type.GetType("System.Reflection.Emit.OpCode");
                    return _opCodeType;
                }
                catch (TypeLoadException)
                {
                    throw new NotSupportedException(NotSupported);
                }
            }
        }

        public static object DefineDynamicAssembly(string name)
        {
            if (_defineDynamicAssemblyDelegate == null)
            {
                _defineDynamicAssemblyDelegate = AssemblyBuilderType.StaticMethod("DefineDynamicAssembly",
                    typeof(AssemblyName), AssemblyAccessType);
            }

            return _defineDynamicAssemblyDelegate(new[] {new AssemblyName(name), AssemblyAccess});
        }

        public static object DefineDynamicModule(object assemblyBuilder, string name)
        {
            if (_defineDynamicModuleDelegate == null)
            {
                _defineDynamicModuleDelegate =
                    AssemblyBuilderType.InstanceMethod("DefineDynamicModule", typeof(string));
            }

            return _defineDynamicModuleDelegate(assemblyBuilder, new object[] {name});
        }

        public static object DefineType(object module, string name)
        {
            if (_defineTypeDelegate == null)
            {
                _defineTypeDelegate = module.GetType().InstanceMethod<
                    Func<object, string, TypeAttributes, object>>("DefineType");
            }

            return _defineTypeDelegate(module, name, TypeAttributes.Class | TypeAttributes.Public);
        }

        public static object DefineMethod(object typeBuilder, string name, Type returnType, Type[] parameterTypes)
        {
            if (_defineMethodDelegate == null)
            {
                _defineMethodDelegate = typeBuilder.GetType().InstanceMethod
                    <Func<object, string, MethodAttributes, CallingConventions, Type, Type[], object>>(
                        "DefineMethod");
            }

            return _defineMethodDelegate(typeBuilder, name, MethodAttributes.Static | MethodAttributes.Public |
                MethodAttributes.Final, CallingConventions.Standard, returnType, parameterTypes);
        }

        public static object GetILGenerator(object methodBuilder)
        {
            if (_getGeneratorDelegate == null)
            {
                _getGeneratorDelegate = methodBuilder.GetType().InstanceMethod<Func<object, object>>("GetILGenerator");
            }

            return _getGeneratorDelegate(methodBuilder);
        }

        public static Type CreateType(object typeBuilder)
        {
            if (_createTypeDeleg == null)
            {
                _createTypeDeleg = typeBuilder.GetType().InstanceMethod<Func<object, Type>>("CreateType");
            }

            return _createTypeDeleg(typeBuilder);
        }

        public static object GetOpCode(string opCodeName)
        {
            if (!OpCodesValues.ContainsKey(opCodeName))
            {
                OpCodesValues[opCodeName] = OpCodesType.GetField(opCodeName).GetValue(null);
            }

            return OpCodesValues[opCodeName];
        }

        public static void Emit(object ilGenerator, object opCode)
        {
            if (_emitDelegate1 == null)
            {
                _emitDelegate1 = ilGenerator.GetType().InstanceMethodVoid("Emit", OpCodeType);
            }

            _emitDelegate1(ilGenerator, new[] {opCode});
        }

        public static void Emit(object ilGenerator, object opCode, ConstructorInfo conInfo)
        {
            if (_emitDelegateConstr == null)
            {
                _emitDelegateConstr =
                    ilGenerator.GetType().InstanceMethodVoid("Emit", OpCodeType, typeof(ConstructorInfo));
            }

            _emitDelegateConstr(ilGenerator, new[] {opCode, conInfo});
        }

        public static void Emit(object ilGenerator, object opCode, MethodInfo methodInfo)
        {
            if (_emitDelegateMethod == null)
            {
                _emitDelegateMethod =
                    ilGenerator.GetType().InstanceMethodVoid("Emit", OpCodeType, typeof(MethodInfo));
            }

            _emitDelegateMethod(ilGenerator, new[] {opCode, methodInfo});
        }
    }
}