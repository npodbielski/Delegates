using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
#if NET35 || NET4 || NET45 || NET46 || NETCORE || STANDARD
using System.Reflection.Emit;
#endif
#if NET35 || NET4 || NETCORE || PORTABLE || STANDARD
using Delegates.Extensions;
#endif
#if NET45 || NETCORE || STANDARD
using WeakReference = System.WeakReference<object>;
#endif

namespace Delegates.Helper
{
    internal static class EventsHelper
    {
        public static readonly MethodInfo EventHandlerFactoryMethodInfo =
            typeof(EventsHelper).GetMethod("EventHandlerFactory");

        private static readonly Dictionary<WeakReference, WeakReference> EventsProxies =
            new Dictionary<WeakReference, WeakReference>();

        //TODO: optimize by creating more methods that suppose to do least work its possible - heave duty should be done in Reflection during creation of delegate in EventAdd/Remove
        public static TEventDelegate EventHandlerFactory<TSourceDelegate, TEventDelegate, TEventArgs, TSource>(
            Delegate handler, bool isRemove)
            where TSourceDelegate : class
            where TEventDelegate : class
#if NET35 || NET4 || PORTABLE
            where TEventArgs : EventArgs
#endif
        {
            Delegate newEventHandler;
            var haveKey = false;
            var kv = EventsProxies.FirstOrDefault(k =>
            {
                object keyTarget;
                k.Key.TryGetTarget(out keyTarget);
                if (Equals(keyTarget, handler))
                {
                    haveKey = true;
                    return true;
                }
                return false;
            });
            if (haveKey)
            {
                object fromCache;
                EventsProxies[kv.Key].TryGetTarget(out fromCache);
                newEventHandler = (Delegate)fromCache;
                if (isRemove)
                {
                    EventsProxies.Remove(kv.Key);
                    return newEventHandler as TEventDelegate;
                }
            }
            if (!isRemove)
            {
                var type = typeof(TEventDelegate);
                if (type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof(EventHandler<>))
                {
                    var action = handler as Action<TSource, object>;
                    if (action != null)
                    {
                        newEventHandler = (EventHandler<TEventArgs>)((s, a) => action((TSource)s, a));
                    }
                    else
                    {
                        var objectAction = (Action<object, object>)handler;
                        newEventHandler = new EventHandler<TEventArgs>((s, a) => objectAction(s, a));
                    }
                }
                else
                {
                    newEventHandler = GetConverter<TSourceDelegate, TEventDelegate>()(handler as TSourceDelegate)
                        as Delegate;
                }
                EventsProxies[new WeakReference(handler)] = new WeakReference(newEventHandler);
                return newEventHandler as TEventDelegate;
            }
            return null;
        }

        private static readonly Dictionary<string, Delegate> Converters = new Dictionary<string, Delegate>();

        private static Func<TSource, TDest> GetConverter<TSource, TDest>()
        {
            var key = typeof(TSource).FullName + typeof(TDest).FullName;
            if (!Converters.ContainsKey(key))
            {
                Converters[key] = CreateDelegateConverter<TSource, TDest>();
            }
            return (Func<TSource, TDest>)Converters[key];
        }

        private static Func<TSource, TDest> CreateDelegateConverter<TSource, TDest>()
        {
            var name = $"Converter_{typeof(TSource).Name}_to_{typeof(TDest).Name}";
            var assemblyName = new AssemblyName(name);
#if !PORTABLE
            const AssemblyBuilderAccess assemblyAccess =
#if NET4 || NET45 || NET46 || NETCORE || STANDARD
                AssemblyBuilderAccess.RunAndCollect;
#elif NET35
                AssemblyBuilderAccess.Run;
#endif
#if NET45 || NET46 || NETCORE || STANDARD
            var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, assemblyAccess);
#elif NET35 || NET4
            var assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, assemblyAccess);
#endif
            var module = assemblyBuilder.DefineDynamicModule(name);
            var typeBuilder = module.DefineType(name, TypeAttributes.Class | TypeAttributes.Public);
            typeBuilder.DefineDefaultConstructor(MethodAttributes.Public);
            var methodBuilder = typeBuilder.DefineMethod(name, MethodAttributes.Static | MethodAttributes.Public |
                MethodAttributes.Final, CallingConventions.Standard, typeof(TDest), new[] { typeof(TSource) });
            var generator = methodBuilder.GetILGenerator();
            var con = typeof(TDest).GetTypeInfo().GetConstructors()[0];
            generator.Emit(OpCodes.Ldarg_0);
            generator.Emit(OpCodes.Ldftn, typeof(TSource).GetMethod("Invoke"));
            generator.Emit(OpCodes.Newobj, con);
            generator.Emit(OpCodes.Ret);
#if NET35 || NET4 || NET45 || NET46
            var type = typeBuilder.CreateType();
#elif NETCORE || STANDARD
            var type = typeBuilder.CreateTypeInfo();
#endif
#else
            var assemblyBuilder = PortableAssemblyBuilder.DefineDynamicAssembly(name);
            var module = PortableAssemblyBuilder.DefineDynamicModule(assemblyBuilder, name);
            var typeBuilder = PortableAssemblyBuilder.DefineType(module, name);
            var methodBuilder = PortableAssemblyBuilder.DefineMethod(typeBuilder, name, typeof(TDest),
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
#endif
            var methodInfo = type.GetMethods()[0];
            return (Func<TSource, TDest>)methodInfo.CreateDelegate(typeof(Func<TSource, TDest>));
        }

#if PORTABLE
        public class PortableAssemblyBuilder
        {
            private const string NotSupported = "On this platform delegates types conversion is not supported!";

            private static Type _assemblyBuilderType;

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

            private static Type _assemblyAccessType;

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

            private static object _assemblyAccess;

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

            private static Func<object[], object> _defineDynamicAssemblyDelegate;

            public static object DefineDynamicAssembly(string name)
            {
                if (_defineDynamicAssemblyDelegate == null)
                {
                    _defineDynamicAssemblyDelegate = AssemblyBuilderType.StaticMethod("DefineDynamicAssembly",
                        typeof(AssemblyName), AssemblyAccessType);
                }

                return _defineDynamicAssemblyDelegate(new[] { new AssemblyName(name), AssemblyAccess });
            }

            private static Func<object, object[], object> _defineDynamicModuleDelegate;
            private static Func<object, string, TypeAttributes, object> _defineTypeDelegate;
            private static Func<object, string, MethodAttributes, CallingConventions, Type, Type[], object> _defineMethodDelegate;
            private static Func<object, object> _getGeneratorDelegate;

            public static object DefineDynamicModule(object assemblyBuilder, string name)
            {
                if (_defineDynamicModuleDelegate == null)
                {
                    _defineDynamicModuleDelegate = AssemblyBuilderType.InstanceMethod("DefineDynamicModule", typeof(string));
                }

                return _defineDynamicModuleDelegate(assemblyBuilder, new object[] { name });
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
                    _getGeneratorDelegate = methodBuilder.GetType().
                        InstanceMethod<Func<object, object>>("GetILGenerator");
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

            private static Type _opCodesType;

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

            private static readonly Dictionary<string, object> OpCodesValues = new Dictionary<string, object>();
            private static Action<object, object[]> _emitDelegate1;
            private static Action<object, object[]> _emitDelegateMethod;
            private static Action<object, object[]> _emitDelegateConstr;
            private static Func<object, Type> _createTypeDeleg;
            private static Type _opCodeType;

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
                _emitDelegate1(ilGenerator, new[] { opCode });
            }

            public static void Emit(object ilGenerator, object opCode, ConstructorInfo conInfo)
            {
                if (_emitDelegateConstr == null)
                {
                    _emitDelegateConstr = ilGenerator.GetType().InstanceMethodVoid("Emit", OpCodeType, typeof(ConstructorInfo));
                }
                _emitDelegateConstr(ilGenerator, new[] { opCode, conInfo });
            }

            public static void Emit(object ilGenerator, object opCode, MethodInfo methodInfo)
            {
                if (_emitDelegateMethod == null)
                {
                    _emitDelegateMethod = ilGenerator.GetType().InstanceMethodVoid("Emit", OpCodeType, typeof(MethodInfo));
                }
                _emitDelegateMethod(ilGenerator, new[] { opCode, methodInfo });
            }
        }
#endif
    }
}