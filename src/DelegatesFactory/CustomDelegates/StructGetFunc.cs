// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StructGetFunc.cs" company="Natan Podbielski">
//   Copyright (c) 2016 - 2016 Natan Podbielski. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Delegates.CustomDelegates
{
    public delegate TProp StructGetFunc<T, out TProp>(ref T i) where T : struct;
}