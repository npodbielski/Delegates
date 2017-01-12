using System;

namespace DelegatesTest.TestObjects
{
    public class Service : IService
    {
        public event EventHandler<EventArgs> Event;

        public string Echo(string text)
        {
            return text;
        }

        public T Echo<T>(T o)
        {
            return o;
        }

        public void InvokeEvent()
        {
            Event?.Invoke(this, new EventArgs());
        }
    }
}