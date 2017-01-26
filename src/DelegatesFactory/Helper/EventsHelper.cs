using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
#if NET35||NET4||NETCORE||PORTABLE||STANDARD
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

        public static EventHandler<TEventArgs> EventHandlerFactory<TEventArgs, TSource>(
            object handler, bool isRemove)
            where TEventArgs :
#if NET35 || NET4 || PORTABLE
            EventArgs
#else
            class
#endif
        {
            EventHandler<TEventArgs> newEventHandler;
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
                newEventHandler = (EventHandler<TEventArgs>)fromCache;
                if (isRemove)
                {
                    EventsProxies.Remove(kv.Key);
                    return newEventHandler;
                }
            }
            if (!isRemove)
            {
                var action = handler as Action<TSource, object>;
                if (action != null)
                {
                    newEventHandler = (s, a) => action((TSource)s, a);
                }
                else
                {
                    newEventHandler = new EventHandler<TEventArgs>((Action<object, object>)handler);
                }
                EventsProxies[new WeakReference(handler)] = new WeakReference(newEventHandler);
                return newEventHandler;
            }
            return null;
        }
    }
}