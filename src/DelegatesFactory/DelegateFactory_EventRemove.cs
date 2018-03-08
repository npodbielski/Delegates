// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DelegateFactory_EventRemove.cs" company="Natan Podbielski">
//   Copyright (c) 2016 - 2018 Natan Podbielski. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using Delegates.Extensions;
using Delegates.Helper;

namespace Delegates
{
    /// <summary>
    ///     Creates delegates for types members
    /// </summary>
    public static partial class DelegateFactory
    {
        /// <summary>
        ///     Creates delegate for removing event handler with source instance type and event argument type
        /// </summary>
        /// <typeparam name="TSource">Source type with event</typeparam>
        /// <typeparam name="TEventArgs">Event argument type</typeparam>
        /// <param name="eventName">Name of an event</param>
        /// <returns>Delegate for event remove accessor</returns>
        public static Action<TSource, EventHandler<TEventArgs>> EventRemove<TSource, TEventArgs>(string eventName)
#if NET35||NET4||PORTABLE
            where TEventArgs : EventArgs
#endif
        {
            return EventAccessor<TSource, EventHandler<TEventArgs>>(eventName, TypeExtensions.RemoveAccessor);
        }

        /// <summary>
        ///     Creates delegate for removing event handler with source instance type and event method delegate type
        /// </summary>
        /// <typeparam name="TSource">Source type with event</typeparam>
        /// <typeparam name="TDelegate">
        ///     Event method delegate type. Can be either custom delegate or
        ///     <see cref="EventHandler{TEventArgs}" />, but for second case it is recommended to use
        ///     <see cref="EventRemove{TSource, TEventArgs}" /> instead.
        /// </typeparam>
        /// <param name="eventName">Name of an event</param>
        /// <returns>Delegate for event remove accessor</returns>
        public static Action<TSource, TDelegate> EventRemoveCustomDelegate<TSource, TDelegate>
            (string eventName) where TDelegate : class
        {
            DelegateHelper.CheckDelegate<TDelegate>();
            return EventAccessor<TSource, TDelegate>(eventName, TypeExtensions.RemoveAccessor);
        }

        /// <summary>
        ///     Creates delegate for removing event handler with source instance as object and event argument type
        /// </summary>
        /// <typeparam name="TEventArgs">Event argument type</typeparam>
        /// <param name="source">Source type with defined event</param>
        /// <param name="eventName">Name of an event</param>
        /// <returns>Delegate for event remove accessor</returns>
        public static Action<object, EventHandler<TEventArgs>> EventRemove<TEventArgs>(
            this Type source, string eventName)
#if NET35||NET4||PORTABLE
            where TEventArgs : EventArgs
#endif
        {
            return EventAccessor<EventHandler<TEventArgs>>(source, eventName, TypeExtensions.RemoveAccessor);
        }

        /// <summary>
        ///     Creates delegate for removing event handler with source instance as object and event method delegate type
        /// </summary>
        /// <typeparam name="TDelegate">
        ///     Event method delegate type. Can be either custom delegate or
        ///     <see cref="EventHandler{TEventArgs}" />, but for second case it is recommended to use
        ///     <see cref="DelegateFactory.EventRemove{TEventArgs}" /> instead.
        /// </typeparam>
        /// <param name="source">Source type with defined event</param>
        /// <param name="eventName">Name of an event</param>
        /// <returns>Delegate for event remove accessor</returns>
        public static Action<object, TDelegate> EventRemoveCustomDelegate<TDelegate>(
            this Type source, string eventName) where TDelegate : class
        {
            return EventAccessor<TDelegate>(source, eventName, TypeExtensions.RemoveAccessor);
        }

        /// <summary>
        ///     Creates delegate for removing event handler with source instance type and event argument as object
        /// </summary>
        /// <typeparam name="TSource">Source type with event</typeparam>
        /// <param name="eventName">Name of an event</param>
        /// <returns>Delegate for event remove accessor</returns>
        public static Action<TSource, Action<TSource, object>> EventRemove<TSource>(string eventName)
        {
            return typeof(TSource).EventRemoveImpl<Action<TSource, Action<TSource, object>>>(eventName);
        }

        /// <summary>
        ///     Creates delegate for removing event handler with source instance as object and event argument as object
        /// </summary>
        /// <param name="source">Source type with defined event</param>
        /// <param name="eventName">Name of an event</param>
        /// <returns>Delegate for event remove accessor</returns>
        public static Action<object, Action<object, object>> EventRemove(this Type source, string eventName)
        {
            return source.EventRemoveImpl<Action<object, Action<object, object>>>(eventName);
        }

        private static TDelegate EventRemoveImpl<TDelegate>(this Type source, string eventName)
            where TDelegate : class
        {
            return EventAccessorImpl<TDelegate>(source, eventName, TypeExtensions.RemoveAccessor);
        }
    }
}