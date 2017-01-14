// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StructIndex3GetFunc.cs" company="Natan Podbielski">
//   Copyright (c) 2016 - 2016 Natan Podbielski. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Delegates.CustomDelegates
{
    public delegate TProp StructIndex3GetFunc<T, in TI1, in TI2, in TI3, out TProp>(ref T i, TI1 i1, TI2 i2, TI3 i3)
        where T : struct;
}