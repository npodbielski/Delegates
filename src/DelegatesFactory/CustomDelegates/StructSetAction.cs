// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StructSetAction.cs" company="Natan Podbielski">
//   Copyright (c) 2016 - 2016 Natan Podbielski. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Delegates.CustomDelegates
{
    /// <summary>
    /// Delegates for setting value of indexer with unspecified index parameters in structure type.
    /// </summary>
    /// <typeparam name="T">Structure type</typeparam>
    /// <typeparam name="TProp">Property type</typeparam>
    /// <param name="i">Structure type instance</param>
    /// <param name="value">Value of indexer to set at given index parameters</param>
    /// <returns>Changed structure value</returns>
    public delegate T StructSetAction<T, in TProp>(T i, TProp value);
}