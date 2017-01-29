using System;
using Delegates;
using DelegatesTest.TestObjects;
#if NETCORE||STANDARD
using Assert = DelegatesTest.CAssert;
using TestMethodAttribute = Xunit.FactAttribute;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
#endif

namespace DelegatesTest
{
#if !(NETCORE||STANDARD)
    [TestClass]
#endif
    public class DelegateFactoryTests_StaticMethods
    {
        private const string TestValue = "test";
        private readonly TestClass _testClassInstance = new TestClass();
        private readonly Type _testClassType = typeof(TestClass);
        private readonly Type _testStructType = typeof(TestStruct);
        private TestStruct _testStructInstance = new TestStruct(0);

        //TODO: test static methods with out or ref modifiers
        [TestMethod]
        public void Public_Method_ByTypes_Void_NoParameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Action>("StaticVoidPublic");
            Assert.IsNotNull(m);
            m();
            Assert.AreEqual(true, TestClass.StaticVoidPublicExecuted);
        }

        [TestMethod]
        public void Internal_Method_ByTypes_Void_NoParameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Action>("StaticVoidInternal");
            Assert.IsNotNull(m);
            m();
            Assert.AreEqual(true, TestClass.StaticVoidInternalExecuted);
        }

        [TestMethod]
        public void Private_Method_ByTypes_Void_NoParameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Action>("StaticVoidPrivate");
            Assert.IsNotNull(m);
            m();
            Assert.AreEqual(true, TestClass.StaticVoidPrivateExecuted);
        }

        [TestMethod]
        public void Protected_Method_ByTypes_Void_NoParameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Action>("StaticVoidProtected");
            Assert.IsNotNull(m);
            m();
            Assert.AreEqual(true, TestClass.StaticVoidProtectedExecuted);
        }

        [TestMethod]
        public void Public_Method_ByTypes_Void_SingleParameter()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Action<string>>("StaticVoidPublic");
            Assert.IsNotNull(m);
            m(TestValue);
            Assert.AreEqual(TestValue, TestClass.StaticVoidPublicParam);
        }

        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_NoParameter()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<string>>("StaticPublic");
            Assert.IsNotNull(m);
            var result = m();
            Assert.AreEqual(TestClass.StaticPublicParameterlessReturnValue, result);
        }

        [TestMethod]
        public void Internal_Method_ByTypes_NoVoid_NoParameter()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<string>>("StaticInternal");
            Assert.IsNotNull(m);
            var result = m();
            Assert.AreEqual(TestClass.StaticInternalParameterlessReturnValue, result);
        }


        [TestMethod]
        public void Protected_Method_ByTypes_NoVoid_NoParameter()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<string>>("StaticProtected");
            Assert.IsNotNull(m);
            var result = m();
            Assert.AreEqual(TestClass.StaticProtectedParameterlessReturnValue, result);
        }


        [TestMethod]
        public void Private_Method_ByTypes_NoVoid_NoParameter()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<string>>("StaticPrivate");
            Assert.IsNotNull(m);
            var result = m();
            Assert.AreEqual(TestClass.StaticPrivateParameterlessReturnValue, result);
        }

        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_SingleParameter()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<string, string>>("StaticPublic");
            Assert.IsNotNull(m);
            var result = m(TestValue);
            Assert.AreEqual(TestValue, result);
        }

        [TestMethod]
        public void Internal_Method_ByTypes_NoVoid_SingleParameter()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<string, string>>("StaticInternal");
            Assert.IsNotNull(m);
            var result = m(TestValue);
            Assert.AreEqual(TestValue, result);
        }


        [TestMethod]
        public void Protected_Method_ByTypes_NoVoid_SingleParameter()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<string, string>>("StaticProtected");
            Assert.IsNotNull(m);
            var result = m(TestValue);
            Assert.AreEqual(TestValue, result);
        }


        [TestMethod]
        public void Private_Method_ByTypes_NoVoid_SingleParameter()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<string, string>>("StaticPrivate");
            Assert.IsNotNull(m);
            var result = m(TestValue);
            Assert.AreEqual(TestValue, result);
        }

        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_2Parameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<string, string, string>>("StaticPublic");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_3Parameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<string, string, string, string>>("StaticPublic");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_4Parameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<string, string, string, string, string>>("StaticPublic");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
            }
        }
        
#if !NET35
        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_5Parameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<string, string, string, string, string, string>>
                ("StaticPublic");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_6Parameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<string, string, string, string, string, string, string>>
                ("StaticPublic");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_7Parameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<string, string, string, string, string, string,
                string, string>>
                ("StaticPublic");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_8Parameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<string, string, string, string, string, string,
                string, string, string>>
                ("StaticPublic");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
            }
        }
        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_9Parameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<string, string, string, string, string, string,
                string, string, string, string>>
                ("StaticPublic");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
            }
        }


        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_10Parameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<string, string, string, string, string, string,
                string, string, string, string, string>>
                ("StaticPublic");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_11Parameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<string, string, string, string, string, string,
                string, string, string, string, string, string>>
                ("StaticPublic");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_12Parameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<string, string, string, string, string, string,
                string, string, string, string, string, string, string>>
                ("StaticPublic");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_13Parameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<string, string, string, string, string, string,
                string, string, string, string, string, string, string, string>>
                ("StaticPublic");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_14Parameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<string, string, string, string, string, string,
                string, string, string, string, string, string, string, string, string>>
                ("StaticPublic");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
            }
        }
        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_15Parameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<string, string, string, string, string, string,
                string, string, string, string, string, string, string, string, string, string>>
                ("StaticPublic");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
            }
        }
        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_16Parameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<string, string, string, string, string, string,
                string, string, string, string, string, string, string, string, string, string, string>>
                ("StaticPublic");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
            }
        } 
#endif
        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_17Parameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<string, string, string, string, string, string,
                string, string, string, string, string, string, string, string, string, string, string, string>>
                ("StaticPublic");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
            }
        }
        [TestMethod]
        public void Public_Method_ByTypes_Void_2Parameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Action<string, string>>("StaticVoidPublic");
            Assert.IsNotNull(m);
            var index = 0;
            m(TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByTypes_Void_3Parameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Action<string, string, string>>("StaticVoidPublic");
            Assert.IsNotNull(m);
            var index = 0;
            m(TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByTypes_Void_4Parameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Action<string, string, string, string>>("StaticVoidPublic");
            Assert.IsNotNull(m);
            var index = 0;
            m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
            }
        }

#if !NET35
        [TestMethod]
        public void Public_Method_ByTypes_Void_5Parameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Action<string, string, string, string, string>>
                ("StaticVoidPublic");
            Assert.IsNotNull(m);
            var index = 0;
            m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByTypes_Void_6Parameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Action<string, string, string, string, string, string>>
                ("StaticVoidPublic");
            Assert.IsNotNull(m);
            var index = 0;
            m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++);
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByTypes_Void_7Parameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Action<string, string, string, string, string, string,
                string>>
                ("StaticVoidPublic");
            Assert.IsNotNull(m);
            var index = 0;
            m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByTypes_Void_8Parameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Action<string, string, string, string, string, string,
                string, string>>
                ("StaticVoidPublic");
            Assert.IsNotNull(m);
            var index = 0;
            m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByTypes_Void_9Parameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Action<string, string, string, string, string, string,
                string, string, string>>
                ("StaticVoidPublic");
            Assert.IsNotNull(m);
            var index = 0;
            m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByTypes_Void_10Parameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Action<string, string, string, string, string, string,
                string, string, string, string>>
                ("StaticVoidPublic");
            Assert.IsNotNull(m);
            var index = 0;
            m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByTypes_Void_11Parameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Action<string, string, string, string, string, string,
                string, string, string, string, string>>
                ("StaticVoidPublic");
            Assert.IsNotNull(m);
            var index = 0;
            m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++);
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByTypes_Void_12Parameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Action<string, string, string, string, string, string,
                string, string, string, string, string, string>>
                ("StaticVoidPublic");
            Assert.IsNotNull(m);
            var index = 0;
            m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByTypes_Void_13Parameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Action<string, string, string, string, string, string,
                string, string, string, string, string, string, string>>
                ("StaticVoidPublic");
            Assert.IsNotNull(m);
            var index = 0;
            m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByTypes_Void_14Parameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Action<string, string, string, string, string, string,
                string, string, string, string, string, string, string, string>>
                ("StaticVoidPublic");
            Assert.IsNotNull(m);
            var index = 0;
            m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByTypes_Void_15Parameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Action<string, string, string, string, string, string,
                string, string, string, string, string, string, string, string, string>>
                ("StaticVoidPublic");
            Assert.IsNotNull(m);
            var index = 0;
            m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByTypes_Void_16Parameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Action<string, string, string, string, string, string,
                string, string, string, string, string, string, string, string, string, string>>
                ("StaticVoidPublic");
            Assert.IsNotNull(m);
            var index = 0;
            m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++);
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
            }
        } 
#endif

        [TestMethod]
        public void Public_Method_ByTypes_Void_17Parameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Action<string, string, string, string, string, string,
                string, string, string, string, string, string, string, string, string, string, string>>
                ("StaticVoidPublic");
            Assert.IsNotNull(m);
            var index = 0;
            m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
            }
        }

        [TestMethod]
        public void Internal_Method_ByTypes_Void_SingleParameter()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Action<string>>("StaticVoidInternal");
            Assert.IsNotNull(m);
            m(TestValue);
            Assert.AreEqual(TestValue, TestClass.StaticVoidInternalParam);
        }

        [TestMethod]
        public void Protected_Method_ByTypes_Void_SingleParameter()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Action<string>>("StaticVoidProtected");
            Assert.IsNotNull(m);
            m(TestValue);
            Assert.AreEqual(TestValue, TestClass.StaticVoidProtectedParam);
        }

        [TestMethod]
        public void Private_Method_ByTypes_Void_SingleParameter()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Action<string>>("StaticVoidPrivate");
            Assert.IsNotNull(m);
            m(TestValue);
            Assert.AreEqual(TestValue, TestClass.StaticVoidPrivateParam);
        }

        [TestMethod]
        public void Method_NonExisting_WrongName_ByTypes()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Action>("NonExisting");
            Assert.IsNull(m);
        }

        [TestMethod]
        public void Method_NonExisting_WrongParams_ByTypes()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Action<TestStruct>>("StaticVoidPublic");
            Assert.IsNull(m);
        }

        [TestMethod]
        public void Method_ByTypes_Wrong_TDelegate_Type()
        {
            AssertHelper.ThrowsException<ArgumentException>(
                () => DelegateFactory.StaticMethod<TestClass, string>("NonExisting"));
        }

        [TestMethod]
        public void Public_Method_ByTypes_VoidDelegate_For_NonVoidMethod()
        {
            AssertHelper.ThrowsException<ArgumentException>(() =>
                DelegateFactory.StaticMethod<TestClass, Action<string>>("StaticPublic"));
        }

        [TestMethod]
        public void Public_Method_ByTypes_NoVoidDelegate_For_VoidMethod()
        {
            AssertHelper.ThrowsException<ArgumentException>(() =>
                DelegateFactory.StaticMethod<TestClass, Func<string>>("StaticVoidPublic"));
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_Void_NoParameters()
        {
            var m = _testClassType.StaticMethod<Action>("StaticVoidPublic");
            Assert.IsNotNull(m);
            m();
            Assert.AreEqual(true, TestClass.StaticVoidPublicExecuted);
        }

        [TestMethod]
        public void Internal_Method_ByObjectAndTypes_Void_NoParameters()
        {
            var m = _testClassType.StaticMethod<Action>("StaticVoidInternal");
            Assert.IsNotNull(m);
            m();
            Assert.AreEqual(true, TestClass.StaticVoidInternalExecuted);
        }

        [TestMethod]
        public void Private_Method_ByObjectAndTypes_Void_NoParameters()
        {
            var m = _testClassType.StaticMethod<Action>("StaticVoidPrivate");
            Assert.IsNotNull(m);
            m();
            Assert.AreEqual(true, TestClass.StaticVoidPrivateExecuted);
        }

        [TestMethod]
        public void Protected_Method_ByObjectAndTypes_Void_NoParameters()
        {
            var m = _testClassType.StaticMethod<Action>("StaticVoidProtected");
            Assert.IsNotNull(m);
            m();
            Assert.AreEqual(true, TestClass.StaticVoidProtectedExecuted);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_Void_SingleParameter()
        {
            var m = _testClassType.StaticMethod<Action<string>>("StaticVoidPublic");
            Assert.IsNotNull(m);
            m(TestValue);
            Assert.AreEqual(TestValue, TestClass.StaticVoidPublicParam);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_NoParameter()
        {
            var m = _testClassType.StaticMethod<Func<string>>("StaticPublic");
            Assert.IsNotNull(m);
            var result = m();
            Assert.AreEqual(TestClass.StaticPublicParameterlessReturnValue, result);
        }

        [TestMethod]
        public void Internal_Method_ByObjectAndTypes_NoVoid_NoParameter()
        {
            var m = _testClassType.StaticMethod<Func<string>>("StaticInternal");
            Assert.IsNotNull(m);
            var result = m();
            Assert.AreEqual(TestClass.StaticInternalParameterlessReturnValue, result);
        }


        [TestMethod]
        public void Protected_Method_ByObjectAndTypes_NoVoid_NoParameter()
        {
            var m = _testClassType.StaticMethod<Func<string>>("StaticProtected");
            Assert.IsNotNull(m);
            var result = m();
            Assert.AreEqual(TestClass.StaticProtectedParameterlessReturnValue, result);
        }


        [TestMethod]
        public void Private_Method_ByObjectAndTypes_NoVoid_NoParameter()
        {
            var m = _testClassType.StaticMethod<Func<string>>("StaticPrivate");
            Assert.IsNotNull(m);
            var result = m();
            Assert.AreEqual(TestClass.StaticPrivateParameterlessReturnValue, result);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_SingleParameter()
        {
            var m = _testClassType.StaticMethod<Func<string, string>>("StaticPublic");
            Assert.IsNotNull(m);
            var result = m(TestValue);
            Assert.AreEqual(TestValue, result);
        }

        [TestMethod]
        public void Internal_Method_ByObjectAndTypes_NoVoid_SingleParameter()
        {
            var m = _testClassType.StaticMethod<Func<string, string>>("StaticInternal");
            Assert.IsNotNull(m);
            var result = m(TestValue);
            Assert.AreEqual(TestValue, result);
        }


        [TestMethod]
        public void Protected_Method_ByObjectAndTypes_NoVoid_SingleParameter()
        {
            var m = _testClassType.StaticMethod<Func<string, string>>("StaticProtected");
            Assert.IsNotNull(m);
            var result = m(TestValue);
            Assert.AreEqual(TestValue, result);
        }


        [TestMethod]
        public void Private_Method_ByObjectAndTypes_NoVoid_SingleParameter()
        {
            var m = _testClassType.StaticMethod<Func<string, string>>("StaticPrivate");
            Assert.IsNotNull(m);
            var result = m(TestValue);
            Assert.AreEqual(TestValue, result);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_2Parameters()
        {
            var m = _testClassType.StaticMethod<Func<string, string, string>>("StaticPublic");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_3Parameters()
        {
            var m = _testClassType.StaticMethod<Func<string, string, string, string>>("StaticPublic");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_4Parameters()
        {
            var m = _testClassType.StaticMethod<Func<string, string, string, string, string>>("StaticPublic");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
            }
        }

#if !NET35
        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_5Parameters()
        {
            var m = _testClassType.StaticMethod<Func<string, string, string, string, string, string>>
                ("StaticPublic");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_6Parameters()
        {
            var m = _testClassType.StaticMethod<Func<string, string, string, string, string, string, string>>
                ("StaticPublic");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_7Parameters()
        {
            var m = _testClassType.StaticMethod<Func<string, string, string, string, string, string,
                string, string>>
                ("StaticPublic");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_8Parameters()
        {
            var m = _testClassType.StaticMethod<Func<string, string, string, string, string, string,
                string, string, string>>
                ("StaticPublic");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
            }
        }
        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_9Parameters()
        {
            var m = _testClassType.StaticMethod<Func<string, string, string, string, string, string,
                string, string, string, string>>
                ("StaticPublic");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
            }
        }


        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_10Parameters()
        {
            var m = _testClassType.StaticMethod<Func<string, string, string, string, string, string,
                string, string, string, string, string>>
                ("StaticPublic");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_11Parameters()
        {
            var m = _testClassType.StaticMethod<Func<string, string, string, string, string, string,
                string, string, string, string, string, string>>
                ("StaticPublic");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_12Parameters()
        {
            var m = _testClassType.StaticMethod<Func<string, string, string, string, string, string,
                string, string, string, string, string, string, string>>
                ("StaticPublic");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_13Parameters()
        {
            var m = _testClassType.StaticMethod<Func<string, string, string, string, string, string,
                string, string, string, string, string, string, string, string>>
                ("StaticPublic");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_14Parameters()
        {
            var m = _testClassType.StaticMethod<Func<string, string, string, string, string, string,
                string, string, string, string, string, string, string, string, string>>
                ("StaticPublic");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
            }
        }
        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_15Parameters()
        {
            var m = _testClassType.StaticMethod<Func<string, string, string, string, string, string,
                string, string, string, string, string, string, string, string, string, string>>
                ("StaticPublic");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
            }
        }
        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_16Parameters()
        {
            var m = _testClassType.StaticMethod<Func<string, string, string, string, string, string,
                string, string, string, string, string, string, string, string, string, string, string>>
                ("StaticPublic");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
            }
        } 
#endif

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_17Parameters()
        {
            var m = _testClassType.StaticMethod<Func<string, string, string, string, string, string,
                string, string, string, string, string, string, string, string, string, string, string, string>>
                ("StaticPublic");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
            }
        }
        [TestMethod]
        public void Public_Method_ByObjectAndTypes_Void_2Parameters()
        {
            var m = _testClassType.StaticMethod<Action<string, string>>("StaticVoidPublic");
            Assert.IsNotNull(m);
            var index = 0;
            m(TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_Void_3Parameters()
        {
            var m = _testClassType.StaticMethod<Action<string, string, string>>("StaticVoidPublic");
            Assert.IsNotNull(m);
            var index = 0;
            m(TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_Void_4Parameters()
        {
            var m = _testClassType.StaticMethod<Action<string, string, string, string>>("StaticVoidPublic");
            Assert.IsNotNull(m);
            var index = 0;
            m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
            }
        }

#if !NET35
        [TestMethod]
        public void Public_Method_ByObjectAndTypes_Void_5Parameters()
        {
            var m = _testClassType.StaticMethod<Action<string, string, string, string, string>>
                ("StaticVoidPublic");
            Assert.IsNotNull(m);
            var index = 0;
            m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_Void_6Parameters()
        {
            var m = _testClassType.StaticMethod<Action<string, string, string, string, string, string>>
                ("StaticVoidPublic");
            Assert.IsNotNull(m);
            var index = 0;
            m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++);
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_Void_7Parameters()
        {
            var m = _testClassType.StaticMethod<Action<string, string, string, string, string, string,
                string>>
                ("StaticVoidPublic");
            Assert.IsNotNull(m);
            var index = 0;
            m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_Void_8Parameters()
        {
            var m = _testClassType.StaticMethod<Action<string, string, string, string, string, string,
                string, string>>
                ("StaticVoidPublic");
            Assert.IsNotNull(m);
            var index = 0;
            m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_Void_9Parameters()
        {
            var m = _testClassType.StaticMethod<Action<string, string, string, string, string, string,
                string, string, string>>
                ("StaticVoidPublic");
            Assert.IsNotNull(m);
            var index = 0;
            m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_Void_10Parameters()
        {
            var m = _testClassType.StaticMethod<Action<string, string, string, string, string, string,
                string, string, string, string>>
                ("StaticVoidPublic");
            Assert.IsNotNull(m);
            var index = 0;
            m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_Void_11Parameters()
        {
            var m = _testClassType.StaticMethod<Action<string, string, string, string, string, string,
                string, string, string, string, string>>
                ("StaticVoidPublic");
            Assert.IsNotNull(m);
            var index = 0;
            m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++);
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_Void_12Parameters()
        {
            var m = _testClassType.StaticMethod<Action<string, string, string, string, string, string,
                string, string, string, string, string, string>>
                ("StaticVoidPublic");
            Assert.IsNotNull(m);
            var index = 0;
            m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_Void_13Parameters()
        {
            var m = _testClassType.StaticMethod<Action<string, string, string, string, string, string,
                string, string, string, string, string, string, string>>
                ("StaticVoidPublic");
            Assert.IsNotNull(m);
            var index = 0;
            m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_Void_14Parameters()
        {
            var m = _testClassType.StaticMethod<Action<string, string, string, string, string, string,
                string, string, string, string, string, string, string, string>>
                ("StaticVoidPublic");
            Assert.IsNotNull(m);
            var index = 0;
            m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_Void_15Parameters()
        {
            var m = _testClassType.StaticMethod<Action<string, string, string, string, string, string,
                string, string, string, string, string, string, string, string, string>>
                ("StaticVoidPublic");
            Assert.IsNotNull(m);
            var index = 0;
            m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_Void_16Parameters()
        {
            var m = _testClassType.StaticMethod<Action<string, string, string, string, string, string,
                string, string, string, string, string, string, string, string, string, string>>
                ("StaticVoidPublic");
            Assert.IsNotNull(m);
            var index = 0;
            m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++);
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
            }
        } 
#endif

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_Void_17Parameters()
        {
            var m = _testClassType.StaticMethod<Action<string, string, string, string, string, string,
                string, string, string, string, string, string, string, string, string, string, string>>
                ("StaticVoidPublic");
            Assert.IsNotNull(m);
            var index = 0;
            m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
            }
        }

        [TestMethod]
        public void Internal_Method_ByObjectAndTypes_Void_SingleParameter()
        {
            var m = _testClassType.StaticMethod<Action<string>>("StaticVoidInternal");
            Assert.IsNotNull(m);
            m(TestValue);
            Assert.AreEqual(TestValue, TestClass.StaticVoidInternalParam);
        }

        [TestMethod]
        public void Protected_Method_ByObjectAndTypes_Void_SingleParameter()
        {
            var m = _testClassType.StaticMethod<Action<string>>("StaticVoidProtected");
            Assert.IsNotNull(m);
            m(TestValue);
            Assert.AreEqual(TestValue, TestClass.StaticVoidProtectedParam);
        }

        [TestMethod]
        public void Private_Method_ByObjectAndTypes_Void_SingleParameter()
        {
            var m = _testClassType.StaticMethod<Action<string>>("StaticVoidPrivate");
            Assert.IsNotNull(m);
            m(TestValue);
            Assert.AreEqual(TestValue, TestClass.StaticVoidPrivateParam);
        }

        [TestMethod]
        public void Method_NonExisting_WrongName_ByObjectAndTypes()
        {
            var m = _testClassType.StaticMethod<Action>("NonExisting");
            Assert.IsNull(m);
        }

        [TestMethod]
        public void Method_NonExisting_WrongParams_ByObjectAndTypes()
        {
            var m = _testClassType.StaticMethod<Action<TestStruct>>("StaticVoidPublic");
            Assert.IsNull(m);
        }

        [TestMethod]
        public void Method_ByObjectAndTypes_Wrong_TDelegate_Type()
        {
            AssertHelper.ThrowsException<ArgumentException>(
                () => _testClassType.StaticMethod<TestClass, string>("NonExisting"));
        }
        
        [TestMethod]
        public void Public_Method_ByObjectAndTypes_VoidDelegate_For_NonVoidMethod()
        {
            AssertHelper.ThrowsException<ArgumentException>(() =>
                _testClassType.StaticMethod<Action<string>>("StaticPublic"));
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoidDelegate_For_VoidMethod()
        {
            AssertHelper.ThrowsException<ArgumentException>(() =>
                _testClassType.StaticMethod<Func<string>>("StaticVoidPublic"));
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_NoParameters()
        {
            var m = _testClassType.StaticMethodVoid("StaticVoidPublic");
            Assert.IsNotNull(m);
            m(new object[] { });
            Assert.AreEqual(true, TestClass.StaticVoidPublicExecuted);
        }

        [TestMethod]
        public void Internal_Method_ByObjecs_Void_NoParameters()
        {
            var m = _testClassType.StaticMethodVoid("StaticVoidInternal");
            Assert.IsNotNull(m);
            m(new object[] { });
            Assert.AreEqual(true, TestClass.StaticVoidInternalExecuted);
        }

        [TestMethod]
        public void Private_Method_ByObjecs_Void_NoParameters()
        {
            var m = _testClassType.StaticMethodVoid("StaticVoidPrivate");
            Assert.IsNotNull(m);
            m(new object[] { });
            Assert.AreEqual(true, TestClass.StaticVoidPrivateExecuted);
        }

        [TestMethod]
        public void Protected_Method_ByObjecs_Void_NoParameters()
        {
            var m = _testClassType.StaticMethodVoid("StaticVoidProtected");
            Assert.IsNotNull(m);
            m(new object[] { });
            Assert.AreEqual(true, TestClass.StaticVoidProtectedExecuted);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_SingleParameter()
        {
            var m = _testClassType.StaticMethodVoid("StaticVoidPublic", typeof(string));
            Assert.IsNotNull(m);
            m(new[] { TestValue });
            Assert.AreEqual(TestValue, TestClass.StaticVoidPublicParam);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_SingleParameter_PassedNoParameters()
        {
            var m = _testClassType.StaticMethodVoid("StaticVoidPublic", typeof(string));
            Assert.IsNotNull(m);
            AssertHelper.ThrowsException<IndexOutOfRangeException>(() => m(new object[] { }));
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_SingleParameter_PassedMoreParameters()
        {
            var m = _testClassType.StaticMethodVoid("StaticVoidPublic", typeof(string));
            Assert.IsNotNull(m);
            m(new[] { TestValue, TestValue + 1 });
            Assert.AreEqual(TestValue, TestClass.StaticVoidPublicParam);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_SingleParameter_PassedNull()
        {
            var m = _testClassType.StaticMethodVoid("StaticVoidPublic", typeof(string));
            Assert.IsNotNull(m);
            AssertHelper.ThrowsException<NullReferenceException>(() => m(null));
        }

        [TestMethod]
        public void Public_Method_ByObjecs_WrongParameterType()
        {
            var m = _testClassType.StaticMethodVoid("StaticVoidPublic", typeof(string));
            Assert.IsNotNull(m);
            AssertHelper.ThrowsException<InvalidCastException>(() => m(new object[] { 0 }));
        }

        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_NoParameter()
        {
            var m = _testClassType.StaticMethod("StaticPublic");
            Assert.IsNotNull(m);
            var result = m(new object[0]);
            Assert.AreEqual(TestClass.StaticPublicParameterlessReturnValue, result);
        }

        [TestMethod]
        public void Internal_Method_ByObjecs_NoVoid_NoParameter()
        {
            var m = _testClassType.StaticMethod("StaticInternal");
            Assert.IsNotNull(m);
            var result = m(new object[0]);
            Assert.AreEqual(TestClass.StaticInternalParameterlessReturnValue, result);
        }


        [TestMethod]
        public void Protected_Method_ByObjecs_NoVoid_NoParameter()
        {
            var m = _testClassType.StaticMethod("StaticProtected");
            Assert.IsNotNull(m);
            var result = m(new object[0]);
            Assert.AreEqual(TestClass.StaticProtectedParameterlessReturnValue, result);
        }


        [TestMethod]
        public void Private_Method_ByObjecs_NoVoid_NoParameter()
        {
            var m = _testClassType.StaticMethod("StaticPrivate");
            Assert.IsNotNull(m);
            var result = m(new object[0]);
            Assert.AreEqual(TestClass.StaticPrivateParameterlessReturnValue, result);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_SingleParameter()
        {
            var m = _testClassType.StaticMethod("StaticPublic", typeof(string));
            Assert.IsNotNull(m);
            var result = m(new[] { TestValue });
            Assert.AreEqual(TestValue, result);
        }

        [TestMethod]
        public void Internal_Method_ByObjecs_NoVoid_SingleParameter()
        {
            var m = _testClassType.StaticMethod("StaticInternal", typeof(string));
            Assert.IsNotNull(m);
            var result = m(new[] { TestValue });
            Assert.AreEqual(TestValue, result);
        }


        [TestMethod]
        public void Protected_Method_ByObjecs_NoVoid_SingleParameter()
        {
            var m = _testClassType.StaticMethod("StaticProtected", typeof(string));
            Assert.IsNotNull(m);
            var result = m(new[] { TestValue });
            Assert.AreEqual(TestValue, result);
        }


        [TestMethod]
        public void Private_Method_ByObjecs_NoVoid_SingleParameter()
        {
            var m = _testClassType.StaticMethod("StaticPrivate", typeof(string));
            Assert.IsNotNull(m);
            var result = m(new[] { TestValue });
            Assert.AreEqual(TestValue, result);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_2Parameters()
        {
            var m = _testClassType.StaticMethod("StaticPublic", typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(new object[] { TestValue + index++, TestValue + index++ });
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_3Parameters()
        {
            var m = _testClassType.StaticMethod("StaticPublic", typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(new object[] { TestValue + index++, TestValue + index++, TestValue + index++});
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_4Parameters()
        {
            var m = _testClassType.StaticMethod("StaticPublic", typeof(string), typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(new object[] { TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++});
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
            }
        }


        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_5Parameters()
        {
            var m = _testClassType.StaticMethod("StaticPublic", typeof(string), typeof(string), typeof(string), typeof(string),
                typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(new object[] {TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++});
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_6Parameters()
        {
            var m = _testClassType.StaticMethod("StaticPublic", typeof(string), typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(new object[] {TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++});
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_7Parameters()
        {
            var m = _testClassType.StaticMethod("StaticPublic", typeof(string), typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(new object[] {TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++});
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_8Parameters()
        {
            var m = _testClassType.StaticMethod("StaticPublic", typeof(string), typeof(string), typeof(string), typeof(string),
              typeof(string), typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(new object[] {TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++});
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
            }
        }
        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_9Parameters()
        {
            var m = _testClassType.StaticMethod("StaticPublic", typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(new object[] {TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++});
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
            }
        }


        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_10Parameters()
        {
            var m = _testClassType.StaticMethod("StaticPublic", typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),typeof(string), 
                typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(new object[] {TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++});
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_11Parameters()
        {
            var m = _testClassType.StaticMethod("StaticPublic", typeof(string), typeof(string), typeof(string),
               typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
               typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(new object[] {TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++});
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_12Parameters()
        {
            var m = _testClassType.StaticMethod("StaticPublic", typeof(string), typeof(string), typeof(string),
               typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
               typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(new object[] {TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++});
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_13Parameters()
        {
            var m = _testClassType.StaticMethod("StaticPublic", typeof(string), typeof(string), typeof(string),
             typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
             typeof(string), typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(new object[] {TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++});
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_14Parameters()
        {
            var m = _testClassType.StaticMethod("StaticPublic", typeof(string), typeof(string), typeof(string),
            typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
            typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(new object[] {TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++});
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
            }
        }
        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_15Parameters()
        {
            var m = _testClassType.StaticMethod("StaticPublic", typeof(string), typeof(string), typeof(string),
            typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
            typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(new object[] {TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++});
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
            }
        }
        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_16Parameters()
        {
            var m = _testClassType.StaticMethod("StaticPublic", typeof(string), typeof(string), typeof(string), typeof(string),
            typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
            typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(new object[] {TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++});
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
            }
        }
        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_17Parameters()
        {
            var m = _testClassType.StaticMethod("StaticPublic", typeof(string), typeof(string), typeof(string), typeof(string),
           typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
           typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(new object[] {TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++});
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
            }
        }
        [TestMethod]
        public void Public_Method_ByObjecs_Void_2Parameters()
        {
            var m = _testClassType.StaticMethodVoid("StaticVoidPublic",typeof(string),typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            m(new object[] { TestValue + index++, TestValue + index++});
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_3Parameters()
        {
            var m = _testClassType.StaticMethodVoid("StaticVoidPublic", typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            m(new object[] { TestValue + index++, TestValue + index++, TestValue + index++});
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_4Parameters()
        {
            var m = _testClassType.StaticMethodVoid("StaticVoidPublic", typeof(string), typeof(string), typeof(string),
                typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            m(new object[] { TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++});
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_5Parameters()
        {
            var m = _testClassType.StaticMethodVoid("StaticVoidPublic", typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            m(new object[] { TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++});
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_6Parameters()
        {
            var m = _testClassType.StaticMethodVoid("StaticVoidPublic", typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            m(new object[] { TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++});
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_7Parameters()
        {
            var m = _testClassType.StaticMethodVoid("StaticVoidPublic", typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            m(new object[] { TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++});
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_8Parameters()
        {
            var m = _testClassType.StaticMethodVoid("StaticVoidPublic", typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            m(new object[] { TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++});
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_9Parameters()
        {
            var m = _testClassType.StaticMethodVoid("StaticVoidPublic", typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            m(new object[] { TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++});
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_10Parameters()
        {
            var m = _testClassType.StaticMethodVoid("StaticVoidPublic", typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
                typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            m(new object[] { TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++});
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_11Parameters()
        {
            var m = _testClassType.StaticMethodVoid("StaticVoidPublic", typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            m(new object[] { TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++});
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_12Parameters()
        {
            var m = _testClassType.StaticMethodVoid("StaticVoidPublic", typeof(string), typeof(string), typeof(string),
                    typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
                    typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            m(new object[] { TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++});
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_13Parameters()
        {
            var m = _testClassType.StaticMethodVoid("StaticVoidPublic", typeof(string), typeof(string), typeof(string),
                    typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
                    typeof(string), typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            m(new object[] { TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++});
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_14Parameters()
        {
            var m = _testClassType.StaticMethodVoid("StaticVoidPublic", typeof(string), typeof(string), typeof(string),
                    typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
                    typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            m(new object[] { TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++});
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_15Parameters()
        {
            var m = _testClassType.StaticMethodVoid("StaticVoidPublic", typeof(string), typeof(string), typeof(string),
                    typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
                    typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            m(new object[] { TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++});
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_16Parameters()
        {
            var m = _testClassType.StaticMethodVoid("StaticVoidPublic", typeof(string), typeof(string), typeof(string),
                    typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
                    typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
                    typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            m(new object[] { TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++});
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_17Parameters()
        {
            var m = _testClassType.StaticMethodVoid("StaticVoidPublic", typeof(string), typeof(string), typeof(string),
                    typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
                    typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
                    typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            m(new object[] { TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++});
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
            }
        }

        [TestMethod]
        public void Internal_Method_ByObjecs_Void_SingleParameter()
        {
            var m = _testClassType.StaticMethod<Action<string>>("StaticVoidInternal");
            Assert.IsNotNull(m);
            m(TestValue);
            Assert.AreEqual(TestValue, TestClass.StaticVoidInternalParam);
        }

        [TestMethod]
        public void Protected_Method_ByObjecs_Void_SingleParameter()
        {
            var m = _testClassType.StaticMethod<Action<string>>("StaticVoidProtected");
            Assert.IsNotNull(m);
            m(TestValue);
            Assert.AreEqual(TestValue, TestClass.StaticVoidProtectedParam);
        }

        [TestMethod]
        public void Private_Method_ByObjecs_Void_SingleParameter()
        {
            var m = _testClassType.StaticMethod<Action<string>>("StaticVoidPrivate");
            Assert.IsNotNull(m);
            m(TestValue);
            Assert.AreEqual(TestValue, TestClass.StaticVoidPrivateParam);
        }

        [TestMethod]
        public void Method_NonExisting_WrongName_ByObjects()
        {
            var m = _testClassType.StaticMethod<Action>("NonExisting");
            Assert.IsNull(m);
        }

        [TestMethod]
        public void Method_NonExisting_WrongParams_ByObjects()
        {
            var m = _testClassType.StaticMethod<Action<TestStruct>>("StaticVoidPublic");
            Assert.IsNull(m);
        }

        [TestMethod]
        public void Method_ByObjecs_Wrong_TDelegate_Type()
        {
            AssertHelper.ThrowsException<ArgumentException>(
                () => _testClassType.StaticMethod<TestClass, string>("NonExisting"));
        }

        [TestMethod]
        public void Public_Method_ByObjects_VoidDelegate_For_NonVoidMethod()
        {
            AssertHelper.ThrowsException<ArgumentException>(() =>
            {
                _testClassType.StaticMethodVoid("StaticPublic", typeof(string));
            });
        }

        [TestMethod]
        public void Public_Method_ByObjects_NoVoidDelegate_For_VoidMethod()
        {
            AssertHelper.ThrowsException<ArgumentException>(() =>
                _testClassType.StaticMethod("StaticVoidPublic", typeof(string)));
        }

        [TestMethod]
        public void Incorrect_TDelegate_For_StaticcMethod()
        {
            AssertHelper.ThrowsException<ArgumentException>(() =>
                _testClassType.StaticMethod<Action>("StaticVoidPublic", null, new[] { typeof(string) }));
        }
    }
}
