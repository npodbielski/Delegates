// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StructIndexesSetAction.cs" company="Natan Podbielski">
//   Copyright (c) 2016 - 2016 Natan Podbielski. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Delegates.CustomDelegates
{
    public delegate void StructIndexesSetAction<T, in TProp>(ref T i, object[] indexes, TProp value);
}