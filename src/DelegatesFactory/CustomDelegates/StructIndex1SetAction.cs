// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StructIndex1SetAction.cs" company="Natan Podbielski">
//   Copyright (c) 2016 - 2016 Natan Podbielski. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Delegates.CustomDelegates
{
    /// <summary>
    /// Delegates for setting value of indexer with single index parameter in structure type by reference.
    /// </summary>
    /// <typeparam name="T">Structure type</typeparam>
    /// <typeparam name="TI1">Index parameter type</typeparam>
    /// <typeparam name="TProp">Property type</typeparam>
    /// <param name="i">Structure type instance</param>
    /// <param name="i1">Index parameter</param>
    /// <param name="value">Value of indexer to set at given parameter</param>
    public delegate void StructIndex1SetAction<T, in TI1, in TProp>(ref T i, TI1 i1, TProp value);
}