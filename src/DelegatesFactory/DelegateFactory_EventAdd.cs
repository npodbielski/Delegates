using System;
using System.Linq.Expressions;
using Delegates.Extensions;
using Delegates.Helper;

namespace Delegates
{
    /// <summary>
    /// Creates delegates for types members
    /// </summary>
    public static partial class DelegateFactory
    {
        //TODO:Consider adding EventInvoke
        /// <summary>
        /// Creates delegate for adding event handler with source instance type and event argument type
        /// </summary>
        /// <typeparam name="TSource">Source type with event</typeparam>
        /// <typeparam name="TEventArgs">Event argument type</typeparam>
        /// <param name="eventName">Name of an event</param>
        /// <returns>Delegate for event add accessor</returns>
        public static Action<TSource, EventHandler<TEventArgs>> EventAdd<TSource, TEventArgs>(string eventName)
#if NET35||NET4||PORTABLE
            where TEventArgs : EventArgs
#endif
        {
            return EventAccessor<TSource, TEventArgs>(eventName, TypeExtensions.AddAccessor);
        }

        /// <summary>
        /// Creates delegate for adding event handler with source instance as object and event argument type
        /// </summary>
        /// <typeparam name="TEventArgs">Event argument type</typeparam>
        /// <param name="source">Source type with defined event</param>
        /// <param name="eventName">Name of an event</param>
        /// <returns>Delegate for event add accessor</returns>
        public static Action<object, EventHandler<TEventArgs>> EventAdd<TEventArgs>(
            this Type source, string eventName)
#if NET35||NET4||PORTABLE
            where TEventArgs : EventArgs
#endif
        {
            return EventAccessor<TEventArgs>(source, eventName, TypeExtensions.AddAccessor);
        }

        /// <summary>
        /// Creates delegate for adding event handler with source instance type and event argument as object
        /// </summary>
        /// <typeparam name="TSource">Source type with event</typeparam>
        /// <param name="eventName">Name of an event</param>
        /// <returns>Delegate for event add accessor</returns>
        public static Action<TSource, Action<TSource, object>> EventAdd<TSource>(string eventName)
        {
            return typeof(TSource).EventAddImpl<Action<TSource, Action<TSource, object>>>(eventName);
        }

        /// <summary>
        /// Creates delegate for adding event handler with source instance as object and event argument as object
        /// </summary>
        /// <param name="source">Source type with defined event</param>
        /// <param name="eventName">Name of an event</param>
        /// <returns>Delegate for event add accessor</returns>
        public static Action<object, Action<object, object>> EventAdd(this Type source, string eventName)
        {
            return source.EventAddImpl<Action<object, Action<object, object>>>(eventName);
        }

        //        public static Action<EventHandler<TEvent>> StaticEventAdd<TEvent>(this Type source, string eventName)
        //#if NET35||NET4||PORTABLE
        //            where TEvent : EventArgs
        //#endif
        //        {
        //            var eventInfo = source.GetEventInfo(eventName);
        //            return eventInfo?.AddMethod.CreateDelegate<Action<EventHandler<TEvent>>>();
        //        }
        
        private static Action<TSource, EventHandler<TEventArgs>> EventAccessor<TSource, TEventArgs>
            (string eventName, string accessorName)
#if NET35||NET4||PORTABLE
            where TEventArgs : EventArgs
#endif
        {
            var sourceType = typeof(TSource);
            var accessor = sourceType.GetEventAccessor(eventName, accessorName);
            if (accessor != null)
            {
                accessor.IsEventArgsTypeCorrect<EventHandler<TEventArgs>>();
                return accessor.CreateDelegate<Action<TSource, EventHandler<TEventArgs>>>();
            }
            return null;
        }

        private static TDelegate EventAccessorImpl<TDelegate>(Type source, string eventName, string accessorName)
            where TDelegate : class
        {
            var eventInfo = source.GetEventInfo(eventName);
            if (eventInfo != null)
            {
                var accessor = accessorName == TypeExtensions.AddAccessor ? eventInfo.AddMethod : eventInfo.RemoveMethod;
                var eventArgsType = eventInfo.EventHandlerType.GenericTypeArguments()[0];
                var instanceParameter = Expression.Parameter(typeof(object), "source");
                var delegateTypeParameter = Expression.Parameter(typeof(object), "delegate");
                var methodCallExpression =
                    Expression.Call(EventsHelper.EventHandlerFactoryMethodInfo.MakeGenericMethod(eventArgsType, source),
                        delegateTypeParameter, Expression.Constant(accessorName == TypeExtensions.RemoveAccessor));
                var lambda = Expression.Lambda<TDelegate>(Expression.Call(Expression.Convert(instanceParameter, source),
                        accessor, methodCallExpression),
                    instanceParameter, delegateTypeParameter);
                return lambda.Compile();
            }
            return null;
        }

        private static TDelegate EventAddImpl<TDelegate>(this Type source, string eventName)
            where TDelegate : class
        {
            return EventAccessorImpl<TDelegate>(source, eventName, TypeExtensions.AddAccessor);
        }

        private static Action<object, EventHandler<TEventArgs>> EventAccessor<TEventArgs>
            (Type source, string eventName, string accessorName)
#if NET35||NET4||PORTABLE
            where TEventArgs : EventArgs
#endif
        {
            var accessor = source.GetEventAccessor(eventName, accessorName);
            if (accessor != null)
            {
                var instanceParameter = Expression.Parameter(typeof(object), "source");
                var delegateTypeParameter = Expression.Parameter(typeof(EventHandler<TEventArgs>), "delegate");
                var lambda = Expression.Lambda(Expression.Call(Expression.Convert(instanceParameter, source),
                        accessor, delegateTypeParameter),
                    instanceParameter, delegateTypeParameter);
                return (Action<object, EventHandler<TEventArgs>>)lambda.Compile();
            }
            return null;
        }
    }
}