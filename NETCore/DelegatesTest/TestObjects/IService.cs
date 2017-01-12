using System;

namespace DelegatesTest.TestObjects
{
    public interface IService
    {
        event EventHandler<EventArgs> Event;

        string Echo(string text);

        T Echo<T>(T o);

        void InvokeEvent();
    }
}