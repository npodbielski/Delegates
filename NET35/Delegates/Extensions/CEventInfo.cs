using System;
using System.Reflection;

namespace Delegates.Extensions
{
    public class CEventInfo
    {
        private readonly EventInfo _event;

        public CEventInfo(EventInfo @event)
        {
            _event = @event;
        }

        public MethodInfo AddMethod => _event?.GetAddMethod() ??
                                       _event?.GetAddMethod(true);

        public MethodInfo RemoveMethod => _event?.GetRemoveMethod()??
                                          _event?.GetRemoveMethod(true);

        public Type EventHandlerType => _event?.EventHandlerType;
    }
}