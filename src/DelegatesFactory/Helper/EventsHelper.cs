// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventsHelper.cs" company="Natan Podbielski">
//   Copyright (c) 2016 - 2018 Natan Podbielski. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

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
                EventsProxies[kv.Key].TryGetTarget(out var fromCache);
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
                    if (handler is Action<TSource, object> action)
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
    }
}