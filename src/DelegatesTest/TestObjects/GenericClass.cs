// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GenericClass.cs" company="Natan Podbielski">
//   Copyright (c) 2016 - 2018 Natan Podbielski. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace DelegatesTest.TestObjects
{
    public class GenericClass<T>
    {
        public static T StaticGenericMethod(T param)
        {
            return param;
        }

        public T InstanceGenericMethod(T param)
        {
            return param;
        }
    }
}