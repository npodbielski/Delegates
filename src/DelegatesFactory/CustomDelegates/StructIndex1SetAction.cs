// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StructIndex1SetAction.cs" company="Natan Podbielski">
//   Copyright (c) 2016 - 2016 Natan Podbielski. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Delegates.CustomDelegates
{
    public delegate void StructIndex1SetAction<T, in TI1, in TProp>(ref T i, TI1 i1, TProp value);
}