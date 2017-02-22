// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StructIndex3SetAction.cs" company="Natan Podbielski">
//   Copyright (c) 2016 - 2016 Natan Podbielski. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Delegates.CustomDelegates
{
    /// <summary>
    /// Delegates for setting value of indexer with three index parameters in structure type by reference.
    /// </summary>
    /// <typeparam name="T">Structure type</typeparam>
    /// <typeparam name="TI1">First index parameter type</typeparam>
    /// <typeparam name="TI2">Second index parameter type</typeparam>
    /// <typeparam name="TI3">Third index parameter type</typeparam>
    /// <typeparam name="TProp">Property type</typeparam>
    /// <param name="i">Structure type instance</param>
    /// <param name="i1">First index parameter</param>
    /// <param name="i2">Second index parameter</param>
    /// <param name="i3">Third index parameter</param>
    /// <param name="value">Value of indexer to set at given index parameters</param>
    public delegate void StructIndex3SetAction<T, in TI1, in TI2, in TI3, in TProp>(
        ref T i, TI1 i1, TI2 i2, TI3 i3, TProp value);
}