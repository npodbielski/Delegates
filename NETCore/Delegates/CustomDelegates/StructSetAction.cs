// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StructSetAction.cs" company="Natan Podbielski">
//   Copyright (c) 2016 - 2016 Natan Podbielski. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Delegates.CustomDelegates
{
    public delegate T StructSetAction<T, in TProp>(T i, TProp value);
}