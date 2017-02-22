using System;
#if NETCORE||STANDARD
using Assert = DelegatesTest.CAssert;
#else
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
#endif

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
