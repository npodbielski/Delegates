// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CEventInfo.cs" company="Natan Podbielski">
//   Copyright (c) 2016 - 2016 Natan Podbielski. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Reflection;

namespace Delegates.Extensions
{
    /// <summary>
    /// Compatibility class (wrapper) if version of .NET do not support v4.5 of <see cref="EventInfo"/> class.
    /// </summary>
    public class CEventInfo
    {
        private readonly EventInfo _event;

        /// <summary>
        /// Wrapper constructor
        /// </summary>
        /// <param name="event">Incompatible <see cref="EventInfo"/> class instance</param>
        public CEventInfo(EventInfo @event)
        {
            _event = @event;
        }

        /// <summary>
        /// Event add accessor.
        /// </summary>
        public MethodInfo AddMethod => _event?.GetAddMethod() ??
                                       _event?.GetAddMethod(true);

        /// <summary>
        /// Proxy property of <see cref="EventInfo.EventHandlerType"/>
        /// </summary>
        public Type EventHandlerType => _event?.EventHandlerType;

        /// <summary>
        /// Event remove accessor.
        /// </summary>
        public MethodInfo RemoveMethod => _event?.GetRemoveMethod() ??
                                          _event?.GetRemoveMethod(true);
    }
}