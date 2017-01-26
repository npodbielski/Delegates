// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StructIndex1GetFunc.cs" company="Natan Podbielski">
//   Copyright (c) 2016 - 2016 Natan Podbielski. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Delegates.CustomDelegates
{
    /// <summary>
    /// Delegates for returning value of indexer with single index parameter from structure type by reference.
    /// </summary>
    /// <typeparam name="T">Structure type</typeparam>
    /// <typeparam name="TI1">Index parameter type</typeparam>
    /// <typeparam name="TReturn">Index return type</typeparam>
    /// <param name="instance">Structure type instance</param>
    /// <param name="index">Index parameter</param>
    /// <returns>Value of indexer at given index</returns>
    public delegate TReturn StructIndex1GetFunc<T, in TI1, out TReturn>(ref T instance, TI1 index) where T : struct;
}