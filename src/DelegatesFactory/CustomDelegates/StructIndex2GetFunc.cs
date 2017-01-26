// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StructIndex2GetFunc.cs" company="Natan Podbielski">
//   Copyright (c) 2016 - 2016 Natan Podbielski. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Delegates.CustomDelegates
{
    /// <summary>
    /// Delegates for returning value of indexer with two index parameters from structure type by reference.
    /// </summary>
    /// <typeparam name="T">Structure type</typeparam>
    /// <typeparam name="TI1">First index parameter type</typeparam>
    /// <typeparam name="TI2">Second index parameter type</typeparam>
    /// <typeparam name="TProp">Property type</typeparam>
    /// <param name="i">Structure type instance</param>
    /// <param name="i1">First index parameter</param>
    /// <param name="i2">Second index parameter</param>
    /// <returns>Value of indexer at given index parameters</returns>
    public delegate TProp StructIndex2GetFunc<T, in TI1, in TI2, out TProp>(ref T i, TI1 i1, TI2 i2) where T : struct;
}