// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StructIndexesSetAction.cs" company="Natan Podbielski">
//   Copyright (c) 2016 - 2016 Natan Podbielski. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Delegates.CustomDelegates
{
    /// <summary>
    /// Delegates for setting value of indexer with unspecified index parameters in structure type by reference.
    /// </summary>
    /// <typeparam name="T">Structure type</typeparam>
    /// <typeparam name="TValue">Value type</typeparam>
    /// <param name="instance">Structure type instance</param>
    /// <param name="indexes">Set of indexer index parameters</param>
    /// <param name="value">Value of indexer to set at given index parameters</param>
    public delegate void StructIndexesSetAction<T, in TValue>(ref T instance, object[] indexes, TValue value);
}