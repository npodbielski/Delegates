// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StructIndex2SetAction.cs" company="Natan Podbielski">
//   Copyright (c) 2016 - 2016 Natan Podbielski. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Delegates.CustomDelegates
{
    public delegate void StructIndex2SetAction<T, in TI1, in TI2, in TProp>(ref T i, TI1 i1, TI2 i2, TProp value);
}