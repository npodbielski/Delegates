// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AssertHelper.cs" company="Natan Podbielski">
//   Copyright (c) 2016 - 2018 Natan Podbielski. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DelegatesTest
{
    public static class AssertHelper
    {
        public static void ThrowsException<TException>(Action action)
        {
            Exception exp = null;
            try
            {
                action();
            }
            catch (Exception e)
            {
                exp = e;
            }

            Assert.IsNotNull(exp);
            Assert.IsInstanceOfType(exp, typeof(TException));
        }
    }
}