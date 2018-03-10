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
#if NET45 || NET46 || NETCORE || STANDARD
using WeakReference = System.WeakReference<object>;
#endif

namespace Delegates.Helper
{
    internal static partial class EventsHelper
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
                k.Key.TryGetTarget(out var keyTarget);
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
            var type = CreateConverterType<TSource, TDest>(assemblyName);
            var methodInfo = type.GetMethods()[0];
            return (Func<TSource, TDest>)methodInfo.CreateDelegate(typeof(Func<TSource, TDest>));
        }
    }
}