using System;
using Delegates;
using DelegatesTest.TestObjects;
#if NETCORE
using Assert = DelegatesTest.CAssert;
using TestMethodAttribute = Xunit.FactAttribute;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
#endif

namespace DelegatesTest
{
#if !NETCORE
    [TestClass]
#endif
    public class DelegateFactoryTests_InstanceMethods
    {
        //TODO: consider closed delegates for instance methods without instance parameters
        //TODO: test creating delegates with custom delegates types
        private const string TestValue = "test";
        private readonly TestClass _testClassInstance = new TestClass();
        private readonly Type _testClassType = typeof(TestClass);
        private readonly Type _testStructType = typeof(TestStruct);
        private TestStruct _testStructInstance = new TestStruct(0);

        [TestMethod]
        public void Public_Method_ByTypes_Void_NoParameters()
        {
            var m = DelegateFactory.InstanceMethod<Action<TestClass>>("PublicMethodVoid");
            Assert.IsNotNull(m);
            m(_testClassInstance);
            Assert.AreEqual(true, _testClassInstance.PublicMethodVoidExecuted);
        }

        [TestMethod]
        public void Internal_Method_ByTypes_Void_NoParameters()
        {
            var m = DelegateFactory.InstanceMethod<Action<TestClass>>("InternalMethodVoid");
            Assert.IsNotNull(m);
            m(_testClassInstance);
            Assert.AreEqual(true, _testClassInstance.InternalMethodVoidExecuted);
        }

        [TestMethod]
        public void Private_Method_ByTypes_Void_NoParameters()
        {
            var m = DelegateFactory.InstanceMethod<Action<TestClass>>("PrivateMethodVoid");
            Assert.IsNotNull(m);
            m(_testClassInstance);
            Assert.AreEqual(true, _testClassInstance.PrivateMethodVoidExecuted);
        }

        [TestMethod]
        public void Protected_Method_ByTypes_Void_NoParameters()
        {
            var m = DelegateFactory.InstanceMethod<Action<TestClass>>("ProtectedMethodVoid");
            Assert.IsNotNull(m);
            m(_testClassInstance);
            Assert.AreEqual(true, _testClassInstance.ProtectedMethodVoidExecuted);
        }

        [TestMethod]
        public void Public_Method_ByTypes_Void_SingleParameter()
        {
            var m = DelegateFactory.InstanceMethod<Action<TestClass, string>>("PublicMethodVoid");
            Assert.IsNotNull(m);
            m(_testClassInstance, TestValue);
            Assert.AreEqual(TestValue, _testClassInstance.PublicMethodVoidParameter);
        }

        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_NoParameter()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, string>>("PublicMethod");
            Assert.IsNotNull(m);
            var result = m(_testClassInstance);
            Assert.AreEqual(_testClassInstance.PublicParameterlessReturnValue, result);
        }

        [TestMethod]
        public void Internal_Method_ByTypes_NoVoid_NoParameter()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, string>>("InternalMethod");
            Assert.IsNotNull(m);
            var result = m(_testClassInstance);
            Assert.AreEqual(_testClassInstance.InternalParameterlessReturnValue, result);
        }

        [TestMethod]
        public void Protected_Method_ByTypes_NoVoid_NoParameter()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, string>>("ProtectedMethod");
            Assert.IsNotNull(m);
            var result = m(_testClassInstance);
            Assert.AreEqual(_testClassInstance.ProtectedParameterlessReturnValue, result);
        }

        [TestMethod]
        public void Private_Method_ByTypes_NoVoid_NoParameter()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, string>>("PrivateMethod");
            Assert.IsNotNull(m);
            var result = m(_testClassInstance);
            Assert.AreEqual(_testClassInstance.PrivateParameterlessReturnValue, result);
        }

        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_SingleParameter()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, string, string>>("PublicMethod");
            Assert.IsNotNull(m);
            var result = m(_testClassInstance, TestValue);
            Assert.AreEqual(TestValue, result);
        }

        [TestMethod]
        public void Internal_Method_ByTypes_NoVoid_SingleParameter()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, string, string>>("InternalMethod");
            Assert.IsNotNull(m);
            var result = m(_testClassInstance, TestValue);
            Assert.AreEqual(TestValue, result);
        }

        [TestMethod]
        public void Protected_Method_ByTypes_NoVoid_SingleParameter()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, string, string>>("ProtectedMethod");
            Assert.IsNotNull(m);
            var result = m(_testClassInstance, TestValue);
            Assert.AreEqual(TestValue, result);
        }

        [TestMethod]
        public void Private_Method_ByTypes_NoVoid_SingleParameter()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, string, string>>("PrivateMethod");
            Assert.IsNotNull(m);
            var result = m(_testClassInstance, TestValue);
            Assert.AreEqual(TestValue, result);
        }

        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_2Parameters()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, string, string, string>>("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(_testClassInstance, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_3Parameters()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, string, string, string, string>>("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

#if !NET35
        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_4Parameters()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, string, string, string, string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_5Parameters()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, string, string, string, string, string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_6Parameters()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, string, string, string, string, string, string,
                string>>("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_7Parameters()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, string, string, string, string, string, string,
                string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_8Parameters()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, string, string, string, string, string, string,
                string, string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }
        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_9Parameters()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, string, string, string, string, string, string,
                string, string, string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }


        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_10Parameters()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, string, string, string, string, string, string,
                string, string, string, string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_11Parameters()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, string, string, string, string, string, string,
                string, string, string, string, string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_12Parameters()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, string, string, string, string, string, string,
                string, string, string, string, string, string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_13Parameters()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, string, string, string, string, string, string,
                string, string, string, string, string, string, string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_14Parameters()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, string, string, string, string, string, string,
                string, string, string, string, string, string, string, string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }
        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_15Parameters()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, string, string, string, string, string, string,
                string, string, string, string, string, string, string, string, string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_16Parameters()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, string, string, string, string, string, string,
                string, string, string, string, string, string, string, string, string, string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }
#endif

        [TestMethod]
        public void Public_Method_ByTypes_Void_2Parameters()
        {
            var m = DelegateFactory.InstanceMethod<Action<TestClass, string, string>>("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            m(_testClassInstance, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", _testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByTypes_Void_3Parameters()
        {
            var m = DelegateFactory.InstanceMethod<Action<TestClass, string, string, string>>("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", _testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

#if !NET35
        [TestMethod]
        public void Public_Method_ByTypes_Void_4Parameters()
        {
            var m = DelegateFactory.InstanceMethod<Action<TestClass, string, string, string, string>>("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", _testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByTypes_Void_5Parameters()
        {
            var m = DelegateFactory.InstanceMethod<Action<TestClass, string, string, string, string, string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", _testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByTypes_Void_6Parameters()
        {
            var m = DelegateFactory.InstanceMethod<Action<TestClass, string, string, string, string, string, string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++);
            Assert.AreEqual(TestValue + "0", _testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByTypes_Void_7Parameters()
        {
            var m = DelegateFactory.InstanceMethod<Action<TestClass, string, string, string, string, string, string,
                string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", _testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByTypes_Void_8Parameters()
        {
            var m = DelegateFactory.InstanceMethod<Action<TestClass, string, string, string, string, string, string,
                string, string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", _testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByTypes_Void_9Parameters()
        {
            var m = DelegateFactory.InstanceMethod<Action<TestClass, string, string, string, string, string, string,
                string, string, string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", _testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByTypes_Void_10Parameters()
        {
            var m = DelegateFactory.InstanceMethod<Action<TestClass, string, string, string, string, string, string,
                string, string, string, string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", _testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByTypes_Void_11Parameters()
        {
            var m = DelegateFactory.InstanceMethod<Action<TestClass, string, string, string, string, string, string,
                string, string, string, string, string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++);
            Assert.AreEqual(TestValue + "0", _testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByTypes_Void_12Parameters()
        {
            var m = DelegateFactory.InstanceMethod<Action<TestClass, string, string, string, string, string, string,
                string, string, string, string, string, string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", _testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByTypes_Void_13Parameters()
        {
            var m = DelegateFactory.InstanceMethod<Action<TestClass, string, string, string, string, string, string,
                string, string, string, string, string, string, string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", _testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByTypes_Void_14Parameters()
        {
            var m = DelegateFactory.InstanceMethod<Action<TestClass, string, string, string, string, string, string,
                string, string, string, string, string, string, string, string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", _testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByTypes_Void_15Parameters()
        {
            var m = DelegateFactory.InstanceMethod<Action<TestClass, string, string, string, string, string, string,
                string, string, string, string, string, string, string, string, string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", _testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByTypes_Void_16Parameters()
        {
            var m = DelegateFactory.InstanceMethod<Action<TestClass, string, string, string, string, string, string,
                string, string, string, string, string, string, string, string, string, string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++);
            Assert.AreEqual(TestValue + "0", _testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }
#endif

        [TestMethod]
        public void Internal_Method_ByTypes_Void_SingleParameter()
        {
            var m = DelegateFactory.InstanceMethod<Action<TestClass, string>>("InternalMethodVoid");
            Assert.IsNotNull(m);
            m(_testClassInstance, TestValue);
            Assert.AreEqual(TestValue, _testClassInstance.InternalMethodVoidParameter);
        }

        [TestMethod]
        public void Protected_Method_ByTypes_Void_SingleParameter()
        {
            var m = DelegateFactory.InstanceMethod<Action<TestClass, string>>("ProtectedMethodVoid");
            Assert.IsNotNull(m);
            m(_testClassInstance, TestValue);
            Assert.AreEqual(TestValue, _testClassInstance.ProtectedMethodVoidParameter);
        }

        [TestMethod]
        public void Private_Method_ByTypes_Void_SingleParameter()
        {
            var m = DelegateFactory.InstanceMethod<Action<TestClass, string>>("PrivateMethodVoid");
            Assert.IsNotNull(m);
            m(_testClassInstance, TestValue);
            Assert.AreEqual(TestValue, _testClassInstance.PrivateMethodVoidParameter);
        }

        [TestMethod]
        public void Method_NonExisting_WrongName_ByTypes()
        {
            var m = DelegateFactory.InstanceMethod<Action<TestClass>>("NonExisting");
            Assert.IsNull(m);
        }

        [TestMethod]
        public void Method_NonExisting_WrongParams_ByTypes()
        {
            var m = DelegateFactory.InstanceMethod<Action<TestClass, TestStruct>>("PublicMethodVoid");
            Assert.IsNull(m);
        }

        [TestMethod]
        public void Method_ByTypes_Wrong_TDelegate_Type()
        {
            AssertHelper.ThrowsException<ArgumentException>(
                () => DelegateFactory.InstanceMethod<string>("PublicMethodVoid"));
        }

        [TestMethod]
        public void Public_Method_ByTypes_VoidDelegate_For_NonVoidMethod()
        {
            AssertHelper.ThrowsException<ArgumentException>(() =>
                DelegateFactory.InstanceMethod<Action<TestClass, string>>("PublicMethod"));
        }

        [TestMethod]
        public void Public_Method_ByTypes_NoVoidDelegate_For_VoidMethod()
        {
            AssertHelper.ThrowsException<ArgumentException>(() =>
                DelegateFactory.InstanceMethod<Func<TestClass, string>>("PublicMethodVoid"));
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_Void_NoParameters()
        {
            var m = _testClassType.InstanceMethod<Action<TestClass>>("PublicMethodVoid");
            Assert.IsNotNull(m);
            m(_testClassInstance);
            Assert.AreEqual(true, _testClassInstance.PublicMethodVoidExecuted);
        }

        [TestMethod]
        public void Internal_Method_ByObjectAndTypes_Void_NoParameters()
        {
            var m = _testClassType.InstanceMethod<Action<TestClass>>("InternalMethodVoid");
            Assert.IsNotNull(m);
            m(_testClassInstance);
            Assert.AreEqual(true, _testClassInstance.InternalMethodVoidExecuted);
        }

        [TestMethod]
        public void Private_Method_ByObjectAndTypes_Void_NoParameters()
        {
            var m = _testClassType.InstanceMethod<Action<TestClass>>("PrivateMethodVoid");
            Assert.IsNotNull(m);
            m(_testClassInstance);
            Assert.AreEqual(true, _testClassInstance.PrivateMethodVoidExecuted);
        }

        [TestMethod]
        public void Protected_Method_ByObjectAndTypes_Void_NoParameters()
        {
            var m = _testClassType.InstanceMethod<Action<TestClass>>("ProtectedMethodVoid");
            Assert.IsNotNull(m);
            m(_testClassInstance);
            Assert.AreEqual(true, _testClassInstance.ProtectedMethodVoidExecuted);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_Void_NoParameters()
        {
            var m = _testClassType.InstanceMethod<Action<object>>("PublicMethodVoid");
            Assert.IsNotNull(m);
            m(_testClassInstance);
            Assert.AreEqual(true, _testClassInstance.PublicMethodVoidExecuted);
        }

        [TestMethod]
        public void Internal_Method_ByObjectAndTypes_WithConversion_Void_NoParameters()
        {
            var m = _testClassType.InstanceMethod<Action<object>>("InternalMethodVoid");
            Assert.IsNotNull(m);
            m(_testClassInstance);
            Assert.AreEqual(true, _testClassInstance.InternalMethodVoidExecuted);
        }

        [TestMethod]
        public void Private_Method_ByObjectAndTypes_WithConversion_Void_NoParameters()
        {
            var m = _testClassType.InstanceMethod<Action<object>>("PrivateMethodVoid");
            Assert.IsNotNull(m);
            m(_testClassInstance);
            Assert.AreEqual(true, _testClassInstance.PrivateMethodVoidExecuted);
        }

        [TestMethod]
        public void Protected_Method_ByObjectAndTypes_WithConversion_Void_NoParameters()
        {
            var m = _testClassType.InstanceMethod<Action<object>>("ProtectedMethodVoid");
            Assert.IsNotNull(m);
            m(_testClassInstance);
            Assert.AreEqual(true, _testClassInstance.ProtectedMethodVoidExecuted);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_Void_SingleParameter()
        {
            var m = _testClassType.InstanceMethod<Action<TestClass, string>>("PublicMethodVoid");
            Assert.IsNotNull(m);
            m(_testClassInstance, TestValue);
            Assert.AreEqual(TestValue, _testClassInstance.PublicMethodVoidParameter);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_Void_SingleParameter()
        {
            var m = _testClassType.InstanceMethod<Action<object, string>>("PublicMethodVoid");
            Assert.IsNotNull(m);
            m(_testClassInstance, TestValue);
            Assert.AreEqual(TestValue, _testClassInstance.PublicMethodVoidParameter);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_NoParameter()
        {
            var m = _testClassType.InstanceMethod<Func<TestClass, string>>("PublicMethod");
            Assert.IsNotNull(m);
            var result = m(_testClassInstance);
            Assert.AreEqual(_testClassInstance.PublicParameterlessReturnValue, result);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_NoVoid_NoParameter()
        {
            var m = _testClassType.InstanceMethod<Func<object, string>>("PublicMethod");
            Assert.IsNotNull(m);
            var result = m(_testClassInstance);
            Assert.AreEqual(_testClassInstance.PublicParameterlessReturnValue, result);
        }

        [TestMethod]
        public void Internal_Method_ByObjectAndTypes_NoVoid_NoParameter()
        {
            var m = _testClassType.InstanceMethod<Func<TestClass, string>>("InternalMethod");
            Assert.IsNotNull(m);
            var result = m(_testClassInstance);
            Assert.AreEqual(_testClassInstance.InternalParameterlessReturnValue, result);
        }

        [TestMethod]
        public void Internal_Method_ByObjectAndTypes_WithConversion_NoVoid_NoParameter()
        {
            var m = _testClassType.InstanceMethod<Func<object, string>>("InternalMethod");
            Assert.IsNotNull(m);
            var result = m(_testClassInstance);
            Assert.AreEqual(_testClassInstance.InternalParameterlessReturnValue, result);
        }

        [TestMethod]
        public void Protected_Method_ByObjectAndTypes_NoVoid_NoParameter()
        {
            var m = _testClassType.InstanceMethod<Func<TestClass, string>>("ProtectedMethod");
            Assert.IsNotNull(m);
            var result = m(_testClassInstance);
            Assert.AreEqual(_testClassInstance.ProtectedParameterlessReturnValue, result);
        }

        [TestMethod]
        public void Protected_Method_ByObjectAndTypes_WithConversion_NoVoid_NoParameter()
        {
            var m = _testClassType.InstanceMethod<Func<object, string>>("ProtectedMethod");
            Assert.IsNotNull(m);
            var result = m(_testClassInstance);
            Assert.AreEqual(_testClassInstance.ProtectedParameterlessReturnValue, result);
        }

        [TestMethod]
        public void Private_Method_ByObjectAndTypes_NoVoid_NoParameter()
        {
            var m = _testClassType.InstanceMethod<Func<TestClass, string>>("PrivateMethod");
            Assert.IsNotNull(m);
            var result = m(_testClassInstance);
            Assert.AreEqual(_testClassInstance.PrivateParameterlessReturnValue, result);
        }

        [TestMethod]
        public void Private_Method_ByObjectAndTypes_WithConversion_NoVoid_NoParameter()
        {
            var m = _testClassType.InstanceMethod<Func<object, string>>("PrivateMethod");
            Assert.IsNotNull(m);
            var result = m(_testClassInstance);
            Assert.AreEqual(_testClassInstance.PrivateParameterlessReturnValue, result);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_SingleParameter()
        {
            var m = _testClassType.InstanceMethod<Func<TestClass, string, string>>("PublicMethod");
            Assert.IsNotNull(m);
            var result = m(_testClassInstance, TestValue);
            Assert.AreEqual(TestValue, result);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_NoVoid_SingleParameter()
        {
            var m = _testClassType.InstanceMethod<Func<object, string, string>>("PublicMethod");
            Assert.IsNotNull(m);
            var result = m(_testClassInstance, TestValue);
            Assert.AreEqual(TestValue, result);
        }

        [TestMethod]
        public void Internal_Method_ByObjectAndTypes_NoVoid_SingleParameter()
        {
            var m = _testClassType.InstanceMethod<Func<TestClass, string, string>>("InternalMethod");
            Assert.IsNotNull(m);
            var result = m(_testClassInstance, TestValue);
            Assert.AreEqual(TestValue, result);
        }

        [TestMethod]
        public void Internal_Method_ByObjectAndTypes_WithConversion_NoVoid_SingleParameter()
        {
            var m = _testClassType.InstanceMethod<Func<object, string, string>>("InternalMethod");
            Assert.IsNotNull(m);
            var result = m(_testClassInstance, TestValue);
            Assert.AreEqual(TestValue, result);
        }

        [TestMethod]
        public void Protected_Method_ByObjectAndTypes_NoVoid_SingleParameter()
        {
            var m = _testClassType.InstanceMethod<Func<TestClass, string, string>>("ProtectedMethod");
            Assert.IsNotNull(m);
            var result = m(_testClassInstance, TestValue);
            Assert.AreEqual(TestValue, result);
        }

        [TestMethod]
        public void Protected_Method_ByObjectAndTypes_WithConversion_NoVoid_SingleParameter()
        {
            var m = _testClassType.InstanceMethod<Func<object, string, string>>("ProtectedMethod");
            Assert.IsNotNull(m);
            var result = m(_testClassInstance, TestValue);
            Assert.AreEqual(TestValue, result);
        }

        [TestMethod]
        public void Private_Method_ByObjectAndTypes_NoVoid_SingleParameter()
        {
            var m = _testClassType.InstanceMethod<Func<TestClass, string, string>>("PrivateMethod");
            Assert.IsNotNull(m);
            var result = m(_testClassInstance, TestValue);
            Assert.AreEqual(TestValue, result);
        }

        [TestMethod]
        public void Private_Method_ByObjectAndTypes_WithConversion_NoVoid_SingleParameter()
        {
            var m = _testClassType.InstanceMethod<Func<object, string, string>>("PrivateMethod");
            Assert.IsNotNull(m);
            var result = m(_testClassInstance, TestValue);
            Assert.AreEqual(TestValue, result);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_2Parameters()
        {
            var m = _testClassType.InstanceMethod<Func<TestClass, string, string, string>>("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(_testClassInstance, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_NoVoid_2Parameters()
        {
            var m = _testClassType.InstanceMethod<Func<object, string, string, string>>("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(_testClassInstance, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_3Parameters()
        {
            var m = _testClassType.InstanceMethod<Func<TestClass, string, string, string, string>>("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_NoVoid_3Parameters()
        {
            var m = _testClassType.InstanceMethod<Func<object, string, string, string, string>>("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

#if !NET35
        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_4Parameters()
        {
            var m = _testClassType.InstanceMethod<Func<TestClass, string, string, string, string, string>>("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_NoVoid_4Parameters()
        {
            var m = _testClassType.InstanceMethod<Func<object, string, string, string, string, string>>("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_5Parameters()
        {
            var m = _testClassType.InstanceMethod<Func<TestClass, string, string, string, string, string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_NoVoid_5Parameters()
        {
            var m = _testClassType.InstanceMethod<Func<object, string, string, string, string, string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_6Parameters()
        {
            var m = _testClassType.InstanceMethod<Func<TestClass, string, string, string, string, string, string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_NoVoid_6Parameters()
        {
            var m = _testClassType.InstanceMethod<Func<object, string, string, string, string, string, string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_7Parameters()
        {
            var m = _testClassType.InstanceMethod<Func<TestClass, string, string, string, string, string, string,
                string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_NoVoid_7Parameters()
        {
            var m = _testClassType.InstanceMethod<Func<object, string, string, string, string, string, string,
                string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_8Parameters()
        {
            var m = _testClassType.InstanceMethod<Func<TestClass, string, string, string, string, string, string,
                string, string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_NoVoid_8Parameters()
        {
            var m = _testClassType.InstanceMethod<Func<object, string, string, string, string, string, string,
                string, string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_9Parameters()
        {
            var m = _testClassType.InstanceMethod<Func<TestClass, string, string, string, string, string, string,
                string, string, string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_NoVoid_9Parameters()
        {
            var m = _testClassType.InstanceMethod<Func<object, string, string, string, string, string, string,
                string, string, string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_10Parameters()
        {
            var m = _testClassType.InstanceMethod<Func<TestClass, string, string, string, string, string, string,
                string, string, string, string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_NoVoid_10Parameters()
        {
            var m = _testClassType.InstanceMethod<Func<object, string, string, string, string, string, string,
                string, string, string, string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_11Parameters()
        {
            var m = _testClassType.InstanceMethod<Func<TestClass, string, string, string, string, string, string,
                string, string, string, string, string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_NoVoid_11Parameters()
        {
            var m = _testClassType.InstanceMethod<Func<object, string, string, string, string, string, string,
                string, string, string, string, string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_12Parameters()
        {
            var m = _testClassType.InstanceMethod<Func<TestClass, string, string, string, string, string, string,
                string, string, string, string, string, string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_NoVoid_12Parameters()
        {
            var m = _testClassType.InstanceMethod<Func<object, string, string, string, string, string, string,
                string, string, string, string, string, string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_13Parameters()
        {
            var m = _testClassType.InstanceMethod<Func<TestClass, string, string, string, string, string, string,
                string, string, string, string, string, string, string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_NoVoid_13Parameters()
        {
            var m = _testClassType.InstanceMethod<Func<object, string, string, string, string, string, string,
                string, string, string, string, string, string, string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_14Parameters()
        {
            var m = _testClassType.InstanceMethod<Func<TestClass, string, string, string, string, string, string,
                string, string, string, string, string, string, string, string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_NoVoid_14Parameters()
        {
            var m = _testClassType.InstanceMethod<Func<object, string, string, string, string, string, string,
                string, string, string, string, string, string, string, string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_15Parameters()
        {
            var m = _testClassType.InstanceMethod<Func<TestClass, string, string, string, string, string, string,
                string, string, string, string, string, string, string, string, string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_NoVoid_15Parameters()
        {
            var m = _testClassType.InstanceMethod<Func<object, string, string, string, string, string, string,
                string, string, string, string, string, string, string, string, string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }
#endif

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_16Parameters()
        {
            var m = _testClassType.InstanceMethod<Func<TestClass, string, string, string, string, string, string,
                string, string, string, string, string, string, string, string, string, string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_NoVoid_16Parameters()
        {
            var m = _testClassType.InstanceMethod<Func<object, string, string, string, string, string, string,
                string, string, string, string, string, string, string, string, string, string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_Void_2Parameters()
        {
            var m = _testClassType.InstanceMethod<Action<TestClass, string, string>>("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            m(_testClassInstance, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", _testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_Void_2Parameters()
        {
            var m = _testClassType.InstanceMethod<Action<object, string, string>>("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            m(_testClassInstance, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", _testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_Void_3Parameters()
        {
            var m = _testClassType.InstanceMethod<Action<TestClass, string, string, string>>("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", _testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_Void_3Parameters()
        {
            var m = _testClassType.InstanceMethod<Action<object, string, string, string>>("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", _testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

#if !NET35
        [TestMethod]
        public void Public_Method_ByObjectAndTypes_Void_4Parameters()
        {
            var m = _testClassType.InstanceMethod<Action<TestClass, string, string, string, string>>("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", _testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_Void_4Parameters()
        {
            var m = _testClassType.InstanceMethod<Action<object, string, string, string, string>>("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", _testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_Void_5Parameters()
        {
            var m = _testClassType.InstanceMethod<Action<TestClass, string, string, string, string, string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", _testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_Void_5Parameters()
        {
            var m = _testClassType.InstanceMethod<Action<object, string, string, string, string, string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", _testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_Void_6Parameters()
        {
            var m = _testClassType.InstanceMethod<Action<TestClass, string, string, string, string, string, string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++);
            Assert.AreEqual(TestValue + "0", _testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_Void_6Parameters()
        {
            var m = _testClassType.InstanceMethod<Action<object, string, string, string, string, string, string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++);
            Assert.AreEqual(TestValue + "0", _testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_Void_7Parameters()
        {
            var m = _testClassType.InstanceMethod<Action<TestClass, string, string, string, string, string, string,
                string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", _testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_Void_7Parameters()
        {
            var m = _testClassType.InstanceMethod<Action<object, string, string, string, string, string, string,
                string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", _testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_Void_8Parameters()
        {
            var m = _testClassType.InstanceMethod<Action<TestClass, string, string, string, string, string, string,
                string, string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", _testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_Void_8Parameters()
        {
            var m = _testClassType.InstanceMethod<Action<object, string, string, string, string, string, string,
                string, string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", _testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_Void_9Parameters()
        {
            var m = _testClassType.InstanceMethod<Action<TestClass, string, string, string, string, string, string,
                string, string, string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", _testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_Void_9Parameters()
        {
            var m = _testClassType.InstanceMethod<Action<object, string, string, string, string, string, string,
                string, string, string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", _testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_Void_10Parameters()
        {
            var m = _testClassType.InstanceMethod<Action<TestClass, string, string, string, string, string, string,
                string, string, string, string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", _testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_Void_10Parameters()
        {
            var m = _testClassType.InstanceMethod<Action<object, string, string, string, string, string, string,
                string, string, string, string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", _testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_Void_11Parameters()
        {
            var m = _testClassType.InstanceMethod<Action<TestClass, string, string, string, string, string, string,
                string, string, string, string, string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++);
            Assert.AreEqual(TestValue + "0", _testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_Void_11Parameters()
        {
            var m = _testClassType.InstanceMethod<Action<object, string, string, string, string, string, string,
                string, string, string, string, string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++);
            Assert.AreEqual(TestValue + "0", _testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_Void_12Parameters()
        {
            var m = _testClassType.InstanceMethod<Action<TestClass, string, string, string, string, string, string,
                string, string, string, string, string, string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", _testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_Void_12Parameters()
        {
            var m = _testClassType.InstanceMethod<Action<object, string, string, string, string, string, string,
                string, string, string, string, string, string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", _testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_Void_13Parameters()
        {
            var m = _testClassType.InstanceMethod<Action<TestClass, string, string, string, string, string, string,
                string, string, string, string, string, string, string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", _testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_Void_13Parameters()
        {
            var m = _testClassType.InstanceMethod<Action<object, string, string, string, string, string, string,
                string, string, string, string, string, string, string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", _testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_Void_14Parameters()
        {
            var m = _testClassType.InstanceMethod<Action<TestClass, string, string, string, string, string, string,
                string, string, string, string, string, string, string, string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", _testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_Void_14Parameters()
        {
            var m = _testClassType.InstanceMethod<Action<object, string, string, string, string, string, string,
                string, string, string, string, string, string, string, string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", _testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_Void_15Parameters()
        {
            var m = _testClassType.InstanceMethod<Action<TestClass, string, string, string, string, string, string,
                string, string, string, string, string, string, string, string, string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", _testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_Void_15Parameters()
        {
            var m = _testClassType.InstanceMethod<Action<object, string, string, string, string, string, string,
                string, string, string, string, string, string, string, string, string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", _testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }
#endif

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_Void_16Parameters()
        {
            var m = _testClassType.InstanceMethod<Action<TestClass, string, string, string, string, string, string,
                string, string, string, string, string, string, string, string, string, string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++);
            Assert.AreEqual(TestValue + "0", _testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_Void_16Parameters()
        {
            var m = _testClassType.InstanceMethod<Action<object, string, string, string, string, string, string,
                string, string, string, string, string, string, string, string, string, string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            m(_testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++);
            Assert.AreEqual(TestValue + "0", _testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Internal_Method_ByObjectAndTypes_Void_SingleParameter()
        {
            var m = _testClassType.InstanceMethod<Action<TestClass, string>>("InternalMethodVoid");
            Assert.IsNotNull(m);
            m(_testClassInstance, TestValue);
            Assert.AreEqual(TestValue, _testClassInstance.InternalMethodVoidParameter);
        }

        [TestMethod]
        public void Internal_Method_ByObjectAndTypes_WithConversion_Void_SingleParameter()
        {
            var m = _testClassType.InstanceMethod<Action<object, string>>("InternalMethodVoid");
            Assert.IsNotNull(m);
            m(_testClassInstance, TestValue);
            Assert.AreEqual(TestValue, _testClassInstance.InternalMethodVoidParameter);
        }

        [TestMethod]
        public void Protected_Method_ByObjectAndTypes_Void_SingleParameter()
        {
            var m = _testClassType.InstanceMethod<Action<TestClass, string>>("ProtectedMethodVoid");
            Assert.IsNotNull(m);
            m(_testClassInstance, TestValue);
            Assert.AreEqual(TestValue, _testClassInstance.ProtectedMethodVoidParameter);
        }

        [TestMethod]
        public void Protected_Method_ByObjectAndTypes_WithConversion_Void_SingleParameter()
        {
            var m = _testClassType.InstanceMethod<Action<object, string>>("ProtectedMethodVoid");
            Assert.IsNotNull(m);
            m(_testClassInstance, TestValue);
            Assert.AreEqual(TestValue, _testClassInstance.ProtectedMethodVoidParameter);
        }

        [TestMethod]
        public void Private_Method_ByObjectAndTypes_Void_SingleParameter()
        {
            var m = _testClassType.InstanceMethod<Action<TestClass, string>>("PrivateMethodVoid");
            Assert.IsNotNull(m);
            m(_testClassInstance, TestValue);
            Assert.AreEqual(TestValue, _testClassInstance.PrivateMethodVoidParameter);
        }

        [TestMethod]
        public void Private_Method_ByObjectAndTypes_WithConversion_Void_SingleParameter()
        {
            var m = _testClassType.InstanceMethod<Action<object, string>>("PrivateMethodVoid");
            Assert.IsNotNull(m);
            m(_testClassInstance, TestValue);
            Assert.AreEqual(TestValue, _testClassInstance.PrivateMethodVoidParameter);
        }

        [TestMethod]
        public void Method_NonExisting_WrongName_ByObjectAndTypes()
        {
            var m = _testClassType.InstanceMethod<Action<TestClass>>("NonExisting");
            Assert.IsNull(m);
        }

        [TestMethod]
        public void Method_NonExisting_WrongParams_ByObjectAndTypes()
        {
            var m = _testClassType.InstanceMethod<Action<TestClass, TestStruct>>("PublicMethodVoid");
            Assert.IsNull(m);
        }

        [TestMethod]
        public void Method_ByObjectAndTypes_Wrong_TDelegate_Type()
        {
            AssertHelper.ThrowsException<ArgumentException>(
                () => _testClassType.InstanceMethod<string>("PublicMethodVoid"));
        }

        [TestMethod]
        public void Method_ByObjectAndTypes_Wrong_Instance_Type()
        {
            AssertHelper.ThrowsException<ArgumentException>(
                () => _testClassType.InstanceMethod<Action<string>>("PublicMethodVoid"));
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_VoidDelegate_For_NonVoidMethod()
        {
            AssertHelper.ThrowsException<ArgumentException>(() =>
                _testClassType.InstanceMethod<Action<TestClass, string>>("PublicMethod"));
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoidDelegate_For_VoidMethod()
        {
            AssertHelper.ThrowsException<ArgumentException>(() =>
                _testClassType.InstanceMethod<Func<TestClass, string>>("PublicMethodVoid"));
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_VoidDelegate_For_NonVoidMethod()
        {
            AssertHelper.ThrowsException<ArgumentException>(() =>
                _testClassType.InstanceMethod<Action<object, string>>("PublicMethod"));
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_NoVoidDelegate_For_VoidMethod()
        {
            AssertHelper.ThrowsException<ArgumentException>(() =>
                _testClassType.InstanceMethod<Func<object, string>>("PublicMethodVoid"));
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_NoParameters()
        {
            var m = _testClassType.InstanceMethodVoid("PublicMethodVoid");
            Assert.IsNotNull(m);
            m(_testClassInstance, new object[] { });
            Assert.AreEqual(true, _testClassInstance.PublicMethodVoidExecuted);
        }

        [TestMethod]
        public void Internal_Method_ByObjecs_Void_NoParameters()
        {
            var m = _testClassType.InstanceMethodVoid("InternalMethodVoid");
            Assert.IsNotNull(m);
            m(_testClassInstance, new object[] { });
            Assert.AreEqual(true, _testClassInstance.InternalMethodVoidExecuted);
        }

        [TestMethod]
        public void Private_Method_ByObjecs_Void_NoParameters()
        {
            var m = _testClassType.InstanceMethodVoid("PrivateMethodVoid");
            Assert.IsNotNull(m);
            m(_testClassInstance, new object[] { });
            Assert.AreEqual(true, _testClassInstance.PrivateMethodVoidExecuted);
        }

        [TestMethod]
        public void Protected_Method_ByObjecs_Void_NoParameters()
        {
            var m = _testClassType.InstanceMethodVoid("ProtectedMethodVoid");
            Assert.IsNotNull(m);
            m(_testClassInstance, new object[] { });
            Assert.AreEqual(true, _testClassInstance.ProtectedMethodVoidExecuted);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_SingleParameter()
        {
            var m = _testClassType.InstanceMethodVoid("PublicMethodVoid", typeof(string));
            Assert.IsNotNull(m);
            m(_testClassInstance, new[] { TestValue });
            Assert.AreEqual(TestValue, _testClassInstance.PublicMethodVoidParameter);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_SingleParameter_PassedNoParameters()
        {
            var m = _testClassType.InstanceMethodVoid("PublicMethodVoid", typeof(string));
            Assert.IsNotNull(m);
            AssertHelper.ThrowsException<IndexOutOfRangeException>(() => m(_testClassInstance, new object[] { }));
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_SingleParameter_PassedMoreParameters()
        {
            var m = _testClassType.InstanceMethodVoid("PublicMethodVoid", typeof(string));
            Assert.IsNotNull(m);
            m(_testClassInstance, new[] { TestValue, TestValue + 1 });
            Assert.AreEqual(TestValue, _testClassInstance.PublicMethodVoidParameter);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_SingleParameter_PassedNull()
        {
            var m = _testClassType.InstanceMethodVoid("PublicMethodVoid", typeof(string));
            Assert.IsNotNull(m);
            AssertHelper.ThrowsException<NullReferenceException>(() => m(_testClassInstance, null));
        }

        [TestMethod]
        public void Public_Method_ByObjecs_WrongParameterType()
        {
            var m = _testClassType.InstanceMethodVoid("PublicMethodVoid", typeof(string));
            Assert.IsNotNull(m);
            AssertHelper.ThrowsException<InvalidCastException>(() => m(_testClassInstance, new object[] { 0 }));
        }

        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_NoParameter()
        {
            var m = _testClassType.InstanceMethod("PublicMethod");
            Assert.IsNotNull(m);
            var result = m(_testClassInstance, new object[0]);
            Assert.AreEqual(_testClassInstance.PublicParameterlessReturnValue, result);
        }

        [TestMethod]
        public void Internal_Method_ByObjecs_NoVoid_NoParameter()
        {
            var m = _testClassType.InstanceMethod("InternalMethod");
            Assert.IsNotNull(m);
            var result = m(_testClassInstance, new object[0]);
            Assert.AreEqual(_testClassInstance.InternalParameterlessReturnValue, result);
        }


        [TestMethod]
        public void Protected_Method_ByObjecs_NoVoid_NoParameter()
        {
            var m = _testClassType.InstanceMethod("ProtectedMethod");
            Assert.IsNotNull(m);
            var result = m(_testClassInstance, new object[0]);
            Assert.AreEqual(_testClassInstance.ProtectedParameterlessReturnValue, result);
        }


        [TestMethod]
        public void Private_Method_ByObjecs_NoVoid_NoParameter()
        {
            var m = _testClassType.InstanceMethod("PrivateMethod");
            Assert.IsNotNull(m);
            var result = m(_testClassInstance, new object[0]);
            Assert.AreEqual(_testClassInstance.PrivateParameterlessReturnValue, result);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_SingleParameter()
        {
            var m = _testClassType.InstanceMethod("PublicMethod", typeof(string));
            Assert.IsNotNull(m);
            var result = m(_testClassInstance, new[] { TestValue });
            Assert.AreEqual(TestValue, result);
        }

        [TestMethod]
        public void Internal_Method_ByObjecs_NoVoid_SingleParameter()
        {
            var m = _testClassType.InstanceMethod("InternalMethod", typeof(string));
            Assert.IsNotNull(m);
            var result = m(_testClassInstance, new[] { TestValue });
            Assert.AreEqual(TestValue, result);
        }


        [TestMethod]
        public void Protected_Method_ByObjecs_NoVoid_SingleParameter()
        {
            var m = _testClassType.InstanceMethod("ProtectedMethod", typeof(string));
            Assert.IsNotNull(m);
            var result = m(_testClassInstance, new[] { TestValue });
            Assert.AreEqual(TestValue, result);
        }


        [TestMethod]
        public void Private_Method_ByObjecs_NoVoid_SingleParameter()
        {
            var m = _testClassType.InstanceMethod("PrivateMethod", typeof(string));
            Assert.IsNotNull(m);
            var result = m(_testClassInstance, new[] { TestValue });
            Assert.AreEqual(TestValue, result);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_2Parameters()
        {
            var m = _testClassType.InstanceMethod("PublicMethod", typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(_testClassInstance, new object[] { TestValue + index++, TestValue + index++ });
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_3Parameters()
        {
            var m = _testClassType.InstanceMethod("PublicMethod", typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(_testClassInstance, new object[] { TestValue + index++, TestValue + index++, TestValue + index++ });
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_4Parameters()
        {
            var m = _testClassType.InstanceMethod("PublicMethod", typeof(string), typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(_testClassInstance, new object[] { TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++ });
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }


        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_5Parameters()
        {
            var m = _testClassType.InstanceMethod("PublicMethod", typeof(string), typeof(string), typeof(string), typeof(string),
                typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(_testClassInstance, new object[] {TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++});
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_6Parameters()
        {
            var m = _testClassType.InstanceMethod("PublicMethod", typeof(string), typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(_testClassInstance, new object[] {TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++});
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_7Parameters()
        {
            var m = _testClassType.InstanceMethod("PublicMethod", typeof(string), typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(_testClassInstance, new object[] {TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++});
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_8Parameters()
        {
            var m = _testClassType.InstanceMethod("PublicMethod", typeof(string), typeof(string), typeof(string), typeof(string),
              typeof(string), typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(_testClassInstance, new object[] {TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++});
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }
        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_9Parameters()
        {
            var m = _testClassType.InstanceMethod("PublicMethod", typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(_testClassInstance, new object[] {TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++});
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }


        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_10Parameters()
        {
            var m = _testClassType.InstanceMethod("PublicMethod", typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
                typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(_testClassInstance, new object[] {TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++});
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_11Parameters()
        {
            var m = _testClassType.InstanceMethod("PublicMethod", typeof(string), typeof(string), typeof(string),
               typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
               typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(_testClassInstance, new object[] {TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++});
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_12Parameters()
        {
            var m = _testClassType.InstanceMethod("PublicMethod", typeof(string), typeof(string), typeof(string),
               typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
               typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(_testClassInstance, new object[] {TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++});
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_13Parameters()
        {
            var m = _testClassType.InstanceMethod("PublicMethod", typeof(string), typeof(string), typeof(string),
             typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
             typeof(string), typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(_testClassInstance, new object[] {TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++});
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_14Parameters()
        {
            var m = _testClassType.InstanceMethod("PublicMethod", typeof(string), typeof(string), typeof(string),
            typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
            typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(_testClassInstance, new object[] {TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++});
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_15Parameters()
        {
            var m = _testClassType.InstanceMethod("PublicMethod", typeof(string), typeof(string), typeof(string),
            typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
            typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(_testClassInstance, new object[] {TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++});
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_16Parameters()
        {
            var m = _testClassType.InstanceMethod("PublicMethod", typeof(string), typeof(string), typeof(string), typeof(string),
            typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
            typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(_testClassInstance, new object[] {TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++});
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_2Parameters()
        {
            var m = _testClassType.InstanceMethodVoid("PublicMethodVoid", typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            m(_testClassInstance, new object[] { TestValue + index++, TestValue + index++ });
            Assert.AreEqual(TestValue + "0", _testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_3Parameters()
        {
            var m = _testClassType.InstanceMethodVoid("PublicMethodVoid", typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            m(_testClassInstance, new object[] { TestValue + index++, TestValue + index++, TestValue + index++ });
            Assert.AreEqual(TestValue + "0", _testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_4Parameters()
        {
            var m = _testClassType.InstanceMethodVoid("PublicMethodVoid", typeof(string), typeof(string), typeof(string),
                typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            m(_testClassInstance, new object[] { TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++ });
            Assert.AreEqual(TestValue + "0", _testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_5Parameters()
        {
            var m = _testClassType.InstanceMethodVoid("PublicMethodVoid", typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            m(_testClassInstance, new object[] { TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++ });
            Assert.AreEqual(TestValue + "0", _testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_6Parameters()
        {
            var m = _testClassType.InstanceMethodVoid("PublicMethodVoid", typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            m(_testClassInstance, new object[] { TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++});
            Assert.AreEqual(TestValue + "0", _testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_7Parameters()
        {
            var m = _testClassType.InstanceMethodVoid("PublicMethodVoid", typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            m(_testClassInstance, new object[] { TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++});
            Assert.AreEqual(TestValue + "0", _testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_8Parameters()
        {
            var m = _testClassType.InstanceMethodVoid("PublicMethodVoid", typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            m(_testClassInstance, new object[] { TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++});
            Assert.AreEqual(TestValue + "0", _testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_9Parameters()
        {
            var m = _testClassType.InstanceMethodVoid("PublicMethodVoid", typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            m(_testClassInstance, new object[] { TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++});
            Assert.AreEqual(TestValue + "0", _testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_10Parameters()
        {
            var m = _testClassType.InstanceMethodVoid("PublicMethodVoid", typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
                typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            m(_testClassInstance, new object[] { TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++});
            Assert.AreEqual(TestValue + "0", _testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_11Parameters()
        {
            var m = _testClassType.InstanceMethodVoid("PublicMethodVoid", typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            m(_testClassInstance, new object[] { TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++});
            Assert.AreEqual(TestValue + "0", _testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_12Parameters()
        {
            var m = _testClassType.InstanceMethodVoid("PublicMethodVoid", typeof(string), typeof(string), typeof(string),
                    typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
                    typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            m(_testClassInstance, new object[] { TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++});
            Assert.AreEqual(TestValue + "0", _testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_13Parameters()
        {
            var m = _testClassType.InstanceMethodVoid("PublicMethodVoid", typeof(string), typeof(string), typeof(string),
                    typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
                    typeof(string), typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            m(_testClassInstance, new object[] { TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++});
            Assert.AreEqual(TestValue + "0", _testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_14Parameters()
        {
            var m = _testClassType.InstanceMethodVoid("PublicMethodVoid", typeof(string), typeof(string), typeof(string),
                    typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
                    typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            m(_testClassInstance, new object[] { TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++});
            Assert.AreEqual(TestValue + "0", _testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_15Parameters()
        {
            var m = _testClassType.InstanceMethodVoid("PublicMethodVoid", typeof(string), typeof(string), typeof(string),
                    typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
                    typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            m(_testClassInstance, new object[] { TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++});
            Assert.AreEqual(TestValue + "0", _testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_16Parameters()
        {
            var m = _testClassType.InstanceMethodVoid("PublicMethodVoid", typeof(string), typeof(string), typeof(string),
                    typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
                    typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
                    typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            m(_testClassInstance, new object[] { TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++});
            Assert.AreEqual(TestValue + "0", _testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, _testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Internal_Method_ByObjecs_Void_SingleParameter()
        {
            var m = _testClassType.InstanceMethod<Action<TestClass, string>>("InternalMethodVoid");
            Assert.IsNotNull(m);
            m(_testClassInstance, TestValue);
            Assert.AreEqual(TestValue, _testClassInstance.InternalMethodVoidParameter);
        }

        [TestMethod]
        public void Protected_Method_ByObjecs_Void_SingleParameter()
        {
            var m = _testClassType.InstanceMethod<Action<TestClass, string>>("ProtectedMethodVoid");
            Assert.IsNotNull(m);
            m(_testClassInstance, TestValue);
            Assert.AreEqual(TestValue, _testClassInstance.ProtectedMethodVoidParameter);
        }

        [TestMethod]
        public void Private_Method_ByObjecs_Void_SingleParameter()
        {
            var m = _testClassType.InstanceMethod<Action<TestClass, string>>("PrivateMethodVoid");
            Assert.IsNotNull(m);
            m(_testClassInstance, TestValue);
            Assert.AreEqual(TestValue, _testClassInstance.PrivateMethodVoidParameter);
        }

        [TestMethod]
        public void Method_NonExisting_WrongName_ByObjects()
        {
            var m = _testClassType.InstanceMethod<Action<TestClass>>("NonExisting");
            Assert.IsNull(m);
        }

        [TestMethod]
        public void Method_NonExisting_WrongName_WithConversion_ByObjects()
        {
            var m = _testClassType.InstanceMethod<Action<object>>("NonExisting");
            Assert.IsNull(m);
        }

        [TestMethod]
        public void Method_NonExisting_WrongParams_ByObjects()
        {
            var m = _testClassType.InstanceMethod<Action<TestClass, TestStruct>>("PublicMethodVoid");
            Assert.IsNull(m);
        }

        [TestMethod]
        public void Method_ByObjecs_Wrong_TDelegate_Type()
        {
            AssertHelper.ThrowsException<ArgumentException>(
                () => _testClassType.InstanceMethod<string>("PublicMethodVoid"));
        }

        [TestMethod]
        public void Public_Method_ByObjects_WithConversion_VoidDelegate_For_NonVoidMethod()
        {
            AssertHelper.ThrowsException<ArgumentException>(() =>
                _testClassType.InstanceMethodVoid("PublicMethod", typeof(string)));
        }

        [TestMethod]
        public void Public_Method_ByObjects_WithConversion_NoVoidDelegate_For_VoidMethod()
        {
            AssertHelper.ThrowsException<ArgumentException>(() =>
                _testClassType.InstanceMethod("PublicMethodVoid", typeof(string)));
        }

        [TestMethod]
        public void Incorrect_TDelegate_For_InstanceGenericMethod()
        {
            AssertHelper.ThrowsException<ArgumentException>(() =>
                _testClassType.InstanceGenericMethod<Action>("PublicMethodVoid", null, new[] { typeof(string) }));
        }
    }
}
