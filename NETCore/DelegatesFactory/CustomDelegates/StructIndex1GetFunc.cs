// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StructIndex1GetFunc.cs" company="Natan Podbielski">
//   Copyright (c) 2016 - 2016 Natan Podbielski. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Delegates.CustomDelegates
{
    public delegate TProp StructIndex1GetFunc<T, in TI1, out TProp>(ref T i, TI1 i1) where T : struct;
}