using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
#if NET35 || NET4 || NETCORE || PORTABLE || STANDARD
using System.Reflection.Emit;
using Delegates.Extensions;
#endif
#if NET45||NETCORE||STANDARD
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
                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(EventHandler<>))
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
            var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndCollect);
            var module = assemblyBuilder.DefineDynamicModule(name);
            var typeBuilder = module.DefineType(name, TypeAttributes.Class | TypeAttributes.Public);
            typeBuilder.DefineDefaultConstructor(MethodAttributes.Public);
            var methodBuilder = typeBuilder.DefineMethod(name, MethodAttributes.Static | MethodAttributes.Public |
                MethodAttributes.Final, CallingConventions.Standard, typeof(TDest), new[] { typeof(TSource) });
            var generator = methodBuilder.GetILGenerator();
            var con = typeof(TDest).GetConstructors()[0];
            generator.Emit(OpCodes.Ldarg_0);
            generator.Emit(OpCodes.Ldftn, typeof(TSource).GetMethod("Invoke"));
            generator.Emit(OpCodes.Newobj, con);
            generator.Emit(OpCodes.Ret);
            var type = typeBuilder.CreateType();
            var methodInfo = type.GetMethods()[0];
            return (Func<TSource, TDest>)methodInfo.CreateDelegate(typeof(Func<TSource, TDest>));
        }
    }
}