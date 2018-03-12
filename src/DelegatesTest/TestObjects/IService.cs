// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IService.cs" company="Natan Podbielski">
//   Copyright (c) 2016 - 2018 Natan Podbielski. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace DelegatesTest.TestObjects
{
    public interface IService
    {
        int IndexerSetValue { get; set; }
        string Property { get; set; }
        int this[int index] { get; set; }
        event EventHandler<EventArgs> Event;

        string Echo(string text);

        T Echo<T>(T o);

        void InvokeEvent();
    }
}