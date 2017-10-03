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
            return EventAccessor<TSource, EventHandler<TEventArgs>>(eventName, TypeExtensions.AddAccessor);
        }

        /// <summary>
        /// Creates delegate for adding event handler with source instance type and event method delegate type
        /// </summary>
        /// <typeparam name="TSource">Source type with event</typeparam>
        /// <typeparam name="TDelegate">Event method delegate type. Can be either custom delegate or 
        /// <see cref="EventHandler{TEventArgs}"/>, but for second case it is recommended to use 
        /// <see cref="EventAdd{TSource, TEventArgs}"/> instead.
        /// </typeparam>
        /// <param name="eventName">Name of an event</param>
        /// <returns>Delegate for event add accessor</returns>
        public static Action<TSource, TDelegate> EventAddCustomDelegate<TSource, TDelegate>
            (string eventName) where TDelegate : class
        {
            DelegateHelper.CheckDelegate<TDelegate>();
            return EventAccessor<TSource, TDelegate>(eventName, TypeExtensions.AddAccessor);
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
            return EventAccessor<EventHandler<TEventArgs>>(source, eventName, TypeExtensions.AddAccessor);
        }

        /// <summary>
        /// Creates delegate for adding event handler with source instance as object and event method delegate type
        /// </summary>
        /// <typeparam name="TDelegate">Event method delegate type. Can be either custom delegate or 
        /// <see cref="EventHandler{TEventArgs}"/>, but for second case it is recommended to use 
        /// <see cref="DelegateFactory.EventAdd{TEventArgs}"/> instead.
        /// </typeparam>
        /// <param name="source">Source type with defined event</param>
        /// <param name="eventName">Name of an event</param>
        /// <returns>Delegate for event add accessor</returns>
        public static Action<object, TDelegate> EventAddCustomDelegate<TDelegate>(
            this Type source, string eventName) where TDelegate : class
        {
            return EventAccessor<TDelegate>(source, eventName, TypeExtensions.AddAccessor);
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

        private static Action<TSource, TDelegate> EventAccessor<TSource, TDelegate>
            (string eventName, string accessorName) where TDelegate : class
        {
            var sourceType = typeof(TSource);
            var accessor = sourceType.GetEventAccessor(eventName, accessorName);
            if (accessor != null)
            {
                var handlerType = accessor.GetParameters()[0].ParameterType;
                if (typeof(TDelegate) == handlerType)
                {
                    var eventArgsType = typeof(TDelegate).GetDelegateSecondParameter();
                    accessor.IsEventArgsTypeCorrect(eventArgsType);
                    return accessor.CreateDelegate<Action<TSource, TDelegate>>();
                }
                DelegateHelper.IsCompatible<TDelegate>(handlerType);
                return EventAccessorImpl<Action<TSource, TDelegate>>(typeof(TSource), eventName, accessorName);
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
                var eventDelegateType = eventInfo.EventHandlerType;
                var instanceParameter = Expression.Parameter(typeof(object), "source");
                var delegateTypeParameter = Expression.Parameter(typeof(TDelegate).GetDelegateSecondParameter(), "delegate");
                var eventFactory = EventsHelper.EventHandlerFactoryMethodInfo.MakeGenericMethod(
                    delegateTypeParameter.Type, eventDelegateType, eventDelegateType.GetDelegateSecondParameter(), 
                    source);
                var methodCallExpression = Expression.Call(eventFactory,
                      delegateTypeParameter, Expression.Constant(accessorName == TypeExtensions.RemoveAccessor));
                var callExpression = Expression.Call(Expression.Convert(instanceParameter, source),
                    accessor, methodCallExpression);
                var lambda = Expression.Lambda<TDelegate>(callExpression,
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

        private static Action<object, TDelegate> EventAccessor<TDelegate>
            (Type source, string eventName, string accessorName) where TDelegate : class
        {
            DelegateHelper.CheckDelegate<TDelegate>();
            var accessor = source.GetEventAccessor(eventName, accessorName);
            if (accessor != null)
            {
                var handlerType = accessor.GetParameters()[0].ParameterType;
                DelegateHelper.IsCompatible<TDelegate>(handlerType);
                if (typeof(TDelegate) == handlerType)
                {
                    var instanceParameter = Expression.Parameter(typeof(object), "source");
                    var delegateTypeParameter = Expression.Parameter(typeof(TDelegate), "delegate");
                    var lambda = Expression.Lambda<Action<object, TDelegate>>(
                        Expression.Call(Expression.Convert(instanceParameter, source), accessor, delegateTypeParameter),
                        instanceParameter, delegateTypeParameter);
                    return lambda.Compile();
                }
                return EventAccessorImpl<Action<object, TDelegate>>(source, eventName, accessorName);
            }
            return null;
        }
    }
}