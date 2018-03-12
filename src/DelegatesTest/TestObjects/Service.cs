// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Service.cs" company="Natan Podbielski">
//   Copyright (c) 2016 - 2018 Natan Podbielski. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace DelegatesTest.TestObjects
{
    public class Service : IService
    {
        public Service()
        {
            Property = "Property";
        }

        public int IndexerSetValue { get; set; }
        public string Property { get; set; }

        public int this[int index]
        {
            get => index;
            set => IndexerSetValue = value;
        }

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