using System;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

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
