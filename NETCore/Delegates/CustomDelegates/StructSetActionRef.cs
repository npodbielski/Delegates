// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StructSetActionRef.cs" company="Natan Podbielski">
//   Copyright (c) 2016 - 2016 Natan Podbielski. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Delegates
{
    public delegate void StructSetActionRef<T, in TProp>(ref T i, TProp value);
}