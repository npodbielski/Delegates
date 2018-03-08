// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomDelegates.cs" company="Natan Podbielski">
//   Copyright (c) 2016 - 2018 Natan Podbielski. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace DelegatesTest.TestObjects
{
    public delegate void CustomAction<in T>(T instance);

    public delegate string CustomFunc<in T>(T instance);

    public delegate void CustomActionSingleParam<in T>(T instance, string s);

    public delegate TestClass CustomCtr();

    public delegate TestClass CustomCtrSingleParam(int i);
}