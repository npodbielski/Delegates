using System;

namespace DelegatesTest.TestObjects
{
    public interface IService
    {
        event EventHandler<EventArgs> Event;

        int IndexerSetValue { get; set; }
        string Property { get; set; }
        int this[int index] { get; set; }

        string Echo(string text);

        T Echo<T>(T o);

        void InvokeEvent();
    }
}