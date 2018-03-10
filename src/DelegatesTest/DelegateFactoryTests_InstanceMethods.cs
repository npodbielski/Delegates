// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DelegateFactoryTests_InstanceMethods.cs" company="Natan Podbielski">
//   Copyright (c) 2016 - 2018 Natan Podbielski. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using Delegates;
using DelegatesTest;
using DelegatesTest.TestObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace
#if NET35
        DelegatesTestNET35
#elif NET4
        DelegatesTestNET4
#elif NET45
        DelegatesTestNET45
#elif NET46
    DelegatesTestNET46
#elif PORTABLE
        DelegatesTestNETPortable
#elif NETCORE
        DelegatesTestNETCORE
#elif NETSTANDARD1_1
        DelegatesTestNETStandard11
#elif NETSTANDARD1_5
        DelegatesTestNETStandard15
#endif
{
    [TestClass]
    public class DelegateFactoryTests_InstanceMethods
    {
        //TODO: consider support for creating delegates from derived type (method defined in base)
        //TODO: consider closed delegates for instance methods without instance parameters
        private const string TestValue = "test";

        //TODO: test instance methods with out or ref modifiers
        //TODO: test InstanceMethod extension method with interface that is implemented in source type but do not have method defined
        [TestMethod]
        public void Public_Method_ByTypes_Void_NoParameters()
        {
            var m = DelegateFactory.InstanceMethod<Action<TestClass>>("PublicMethodVoid");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            m(testClassInstance);
            Assert.AreEqual(true, testClassInstance.PublicMethodVoidExecuted);
        }

        [TestMethod]
        public void Internal_Method_ByTypes_Void_NoParameters()
        {
            var m = DelegateFactory.InstanceMethod<Action<TestClass>>("InternalMethodVoid");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            m(testClassInstance);
            Assert.AreEqual(true, testClassInstance.InternalMethodVoidExecuted);
        }

        [TestMethod]
        public void Private_Method_ByTypes_Void_NoParameters()
        {
            var m = DelegateFactory.InstanceMethod<Action<TestClass>>("PrivateMethodVoid");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            m(testClassInstance);
            Assert.AreEqual(true, testClassInstance.PrivateMethodVoidExecuted);
        }

        [TestMethod]
        public void Protected_Method_ByTypes_Void_NoParameters()
        {
            var m = DelegateFactory.InstanceMethod<Action<TestClass>>("ProtectedMethodVoid");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            m(testClassInstance);
            Assert.AreEqual(true, testClassInstance.ProtectedMethodVoidExecuted);
        }

        [TestMethod]
        public void Public_Method_ByTypes_Void_SingleParameter()
        {
            var m = DelegateFactory.InstanceMethod<Action<TestClass, string>>("PublicMethodVoid");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            m(testClassInstance, TestValue);
            Assert.AreEqual(TestValue, testClassInstance.PublicMethodVoidParameter);
        }

        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_NoParameter()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, string>>("PublicMethod");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            var result = m(testClassInstance);
            Assert.AreEqual(testClassInstance.PublicParameterlessReturnValue, result);
        }

        [TestMethod]
        public void Internal_Method_ByTypes_NoVoid_NoParameter()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, string>>("InternalMethod");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            var result = m(testClassInstance);
            Assert.AreEqual(testClassInstance.InternalParameterlessReturnValue, result);
        }

        [TestMethod]
        public void Protected_Method_ByTypes_NoVoid_NoParameter()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, string>>("ProtectedMethod");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            var result = m(testClassInstance);
            Assert.AreEqual(testClassInstance.ProtectedParameterlessReturnValue, result);
        }

        [TestMethod]
        public void Private_Method_ByTypes_NoVoid_NoParameter()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, string>>("PrivateMethod");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            var result = m(testClassInstance);
            Assert.AreEqual(testClassInstance.PrivateParameterlessReturnValue, result);
        }

        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_SingleParameter()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, string, string>>("PublicMethod");
            Assert.IsNotNull(m);
            var result = m(new TestClass(), TestValue);
            Assert.AreEqual(TestValue, result);
        }

        [TestMethod]
        public void Internal_Method_ByTypes_NoVoid_SingleParameter()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, string, string>>("InternalMethod");
            Assert.IsNotNull(m);
            var result = m(new TestClass(), TestValue);
            Assert.AreEqual(TestValue, result);
        }

        [TestMethod]
        public void Protected_Method_ByTypes_NoVoid_SingleParameter()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, string, string>>("ProtectedMethod");
            Assert.IsNotNull(m);
            var result = m(new TestClass(), TestValue);
            Assert.AreEqual(TestValue, result);
        }

        [TestMethod]
        public void Private_Method_ByTypes_NoVoid_SingleParameter()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, string, string>>("PrivateMethod");
            Assert.IsNotNull(m);
            var result = m(new TestClass(), TestValue);
            Assert.AreEqual(TestValue, result);
        }

        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_2Parameters()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, string, string, string>>("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_3Parameters()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, string, string, string, string>>("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByTypes_Void_2Parameters()
        {
            var m = DelegateFactory.InstanceMethod<Action<TestClass, string, string>>("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            m(testClassInstance, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Public_Method_ByTypes_Void_3Parameters()
        {
            var m = DelegateFactory.InstanceMethod<Action<TestClass, string, string, string>>("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++)
            {
                Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
            }
        }

        [TestMethod]
        public void Internal_Method_ByTypes_Void_SingleParameter()
        {
            var m = DelegateFactory.InstanceMethod<Action<TestClass, string>>("InternalMethodVoid");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            m(testClassInstance, TestValue);
            Assert.AreEqual(TestValue, testClassInstance.InternalMethodVoidParameter);
        }

        [TestMethod]
        public void Protected_Method_ByTypes_Void_SingleParameter()
        {
            var m = DelegateFactory.InstanceMethod<Action<TestClass, string>>("ProtectedMethodVoid");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            m(testClassInstance, TestValue);
            Assert.AreEqual(TestValue, testClassInstance.ProtectedMethodVoidParameter);
        }

        [TestMethod]
        public void Private_Method_ByTypes_Void_SingleParameter()
        {
            var m = DelegateFactory.InstanceMethod<Action<TestClass, string>>("PrivateMethodVoid");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            m(testClassInstance, TestValue);
            Assert.AreEqual(TestValue, testClassInstance.PrivateMethodVoidParameter);
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
            var m = typeof(TestClass).InstanceMethod<Action<TestClass>>("PublicMethodVoid");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            m(testClassInstance);
            Assert.AreEqual(true, testClassInstance.PublicMethodVoidExecuted);
        }

        [TestMethod]
        public void Internal_Method_ByObjectAndTypes_Void_NoParameters()
        {
            var m = typeof(TestClass).InstanceMethod<Action<TestClass>>("InternalMethodVoid");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            m(testClassInstance);
            Assert.AreEqual(true, testClassInstance.InternalMethodVoidExecuted);
        }

        [TestMethod]
        public void Private_Method_ByObjectAndTypes_Void_NoParameters()
        {
            var m = typeof(TestClass).InstanceMethod<Action<TestClass>>("PrivateMethodVoid");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            m(testClassInstance);
            Assert.AreEqual(true, testClassInstance.PrivateMethodVoidExecuted);
        }

        [TestMethod]
        public void Protected_Method_ByObjectAndTypes_Void_NoParameters()
        {
            var m = typeof(TestClass).InstanceMethod<Action<TestClass>>("ProtectedMethodVoid");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            m(testClassInstance);
            Assert.AreEqual(true, testClassInstance.ProtectedMethodVoidExecuted);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_Void_NoParameters()
        {
            var m = typeof(TestClass).InstanceMethod<Action<object>>("PublicMethodVoid");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            m(testClassInstance);
            Assert.AreEqual(true, testClassInstance.PublicMethodVoidExecuted);
        }

        [TestMethod]
        public void Internal_Method_ByObjectAndTypes_WithConversion_Void_NoParameters()
        {
            var m = typeof(TestClass).InstanceMethod<Action<object>>("InternalMethodVoid");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            m(testClassInstance);
            Assert.AreEqual(true, testClassInstance.InternalMethodVoidExecuted);
        }

        [TestMethod]
        public void Private_Method_ByObjectAndTypes_WithConversion_Void_NoParameters()
        {
            var m = typeof(TestClass).InstanceMethod<Action<object>>("PrivateMethodVoid");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            m(testClassInstance);
            Assert.AreEqual(true, testClassInstance.PrivateMethodVoidExecuted);
        }

        [TestMethod]
        public void Protected_Method_ByObjectAndTypes_WithConversion_Void_NoParameters()
        {
            var m = typeof(TestClass).InstanceMethod<Action<object>>("ProtectedMethodVoid");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            m(testClassInstance);
            Assert.AreEqual(true, testClassInstance.ProtectedMethodVoidExecuted);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_Void_SingleParameter()
        {
            var m = typeof(TestClass).InstanceMethod<Action<TestClass, string>>("PublicMethodVoid");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            m(testClassInstance, TestValue);
            Assert.AreEqual(TestValue, testClassInstance.PublicMethodVoidParameter);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_Void_SingleParameter()
        {
            var m = typeof(TestClass).InstanceMethod<Action<object, string>>("PublicMethodVoid");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            m(testClassInstance, TestValue);
            Assert.AreEqual(TestValue, testClassInstance.PublicMethodVoidParameter);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_NoParameter()
        {
            var m = typeof(TestClass).InstanceMethod<Func<TestClass, string>>("PublicMethod");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            var result = m(testClassInstance);
            Assert.AreEqual(testClassInstance.PublicParameterlessReturnValue, result);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_NoVoid_NoParameter()
        {
            var m = typeof(TestClass).InstanceMethod<Func<object, string>>("PublicMethod");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            var result = m(testClassInstance);
            Assert.AreEqual(testClassInstance.PublicParameterlessReturnValue, result);
        }

        [TestMethod]
        public void Internal_Method_ByObjectAndTypes_NoVoid_NoParameter()
        {
            var m = typeof(TestClass).InstanceMethod<Func<TestClass, string>>("InternalMethod");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            var result = m(testClassInstance);
            Assert.AreEqual(testClassInstance.InternalParameterlessReturnValue, result);
        }

        [TestMethod]
        public void Internal_Method_ByObjectAndTypes_WithConversion_NoVoid_NoParameter()
        {
            var m = typeof(TestClass).InstanceMethod<Func<object, string>>("InternalMethod");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            var result = m(testClassInstance);
            Assert.AreEqual(testClassInstance.InternalParameterlessReturnValue, result);
        }

        [TestMethod]
        public void Protected_Method_ByObjectAndTypes_NoVoid_NoParameter()
        {
            var m = typeof(TestClass).InstanceMethod<Func<TestClass, string>>("ProtectedMethod");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            var result = m(testClassInstance);
            Assert.AreEqual(testClassInstance.ProtectedParameterlessReturnValue, result);
        }

        [TestMethod]
        public void Protected_Method_ByObjectAndTypes_WithConversion_NoVoid_NoParameter()
        {
            var m = typeof(TestClass).InstanceMethod<Func<object, string>>("ProtectedMethod");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            var result = m(testClassInstance);
            Assert.AreEqual(testClassInstance.ProtectedParameterlessReturnValue, result);
        }

        [TestMethod]
        public void Private_Method_ByObjectAndTypes_NoVoid_NoParameter()
        {
            var m = typeof(TestClass).InstanceMethod<Func<TestClass, string>>("PrivateMethod");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            var result = m(testClassInstance);
            Assert.AreEqual(testClassInstance.PrivateParameterlessReturnValue, result);
        }

        [TestMethod]
        public void Private_Method_ByObjectAndTypes_WithConversion_NoVoid_NoParameter()
        {
            var m = typeof(TestClass).InstanceMethod<Func<object, string>>("PrivateMethod");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            var result = m(testClassInstance);
            Assert.AreEqual(testClassInstance.PrivateParameterlessReturnValue, result);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_SingleParameter()
        {
            var m = typeof(TestClass).InstanceMethod<Func<TestClass, string, string>>("PublicMethod");
            Assert.IsNotNull(m);
            var result = m(new TestClass(), TestValue);
            Assert.AreEqual(TestValue, result);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_NoVoid_SingleParameter()
        {
            var m = typeof(TestClass).InstanceMethod<Func<object, string, string>>("PublicMethod");
            Assert.IsNotNull(m);
            var result = m(new TestClass(), TestValue);
            Assert.AreEqual(TestValue, result);
        }

        [TestMethod]
        public void Internal_Method_ByObjectAndTypes_NoVoid_SingleParameter()
        {
            var m = typeof(TestClass).InstanceMethod<Func<TestClass, string, string>>("InternalMethod");
            Assert.IsNotNull(m);
            var result = m(new TestClass(), TestValue);
            Assert.AreEqual(TestValue, result);
        }

        [TestMethod]
        public void Internal_Method_ByObjectAndTypes_WithConversion_NoVoid_SingleParameter()
        {
            var m = typeof(TestClass).InstanceMethod<Func<object, string, string>>("InternalMethod");
            Assert.IsNotNull(m);
            var result = m(new TestClass(), TestValue);
            Assert.AreEqual(TestValue, result);
        }

        [TestMethod]
        public void Protected_Method_ByObjectAndTypes_NoVoid_SingleParameter()
        {
            var m = typeof(TestClass).InstanceMethod<Func<TestClass, string, string>>("ProtectedMethod");
            Assert.IsNotNull(m);
            var result = m(new TestClass(), TestValue);
            Assert.AreEqual(TestValue, result);
        }

        [TestMethod]
        public void Protected_Method_ByObjectAndTypes_WithConversion_NoVoid_SingleParameter()
        {
            var m = typeof(TestClass).InstanceMethod<Func<object, string, string>>("ProtectedMethod");
            Assert.IsNotNull(m);
            var result = m(new TestClass(), TestValue);
            Assert.AreEqual(TestValue, result);
        }

        [TestMethod]
        public void Private_Method_ByObjectAndTypes_NoVoid_SingleParameter()
        {
            var m = typeof(TestClass).InstanceMethod<Func<TestClass, string, string>>("PrivateMethod");
            Assert.IsNotNull(m);
            var result = m(new TestClass(), TestValue);
            Assert.AreEqual(TestValue, result);
        }

        [TestMethod]
        public void Private_Method_ByObjectAndTypes_WithConversion_NoVoid_SingleParameter()
        {
            var m = typeof(TestClass).InstanceMethod<Func<object, string, string>>("PrivateMethod");
            Assert.IsNotNull(m);
            var result = m(new TestClass(), TestValue);
            Assert.AreEqual(TestValue, result);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_2Parameters()
        {
            var m = typeof(TestClass).InstanceMethod<Func<TestClass, string, string, string>>("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_NoVoid_2Parameters()
        {
            var m = typeof(TestClass).InstanceMethod<Func<object, string, string, string>>("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_3Parameters()
        {
            var m = typeof(TestClass).InstanceMethod<Func<TestClass, string, string, string, string>>("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_NoVoid_3Parameters()
        {
            var m = typeof(TestClass).InstanceMethod<Func<object, string, string, string, string>>("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_16Parameters()
        {
            var m = typeof(TestClass).InstanceMethod<Func<TestClass, string, string, string, string, string, string,
                    string, string, string, string, string, string, string, string, string, string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_NoVoid_16Parameters()
        {
            var m = typeof(TestClass).InstanceMethod<Func<object, string, string, string, string, string, string,
                    string, string, string, string, string, string, string, string, string, string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_Void_2Parameters()
        {
            var m = typeof(TestClass).InstanceMethod<Action<TestClass, string, string>>("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            m(testClassInstance, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_Void_2Parameters()
        {
            var m = typeof(TestClass).InstanceMethod<Action<object, string, string>>("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            m(testClassInstance, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_Void_3Parameters()
        {
            var m = typeof(TestClass).InstanceMethod<Action<TestClass, string, string, string>>("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_Void_3Parameters()
        {
            var m = typeof(TestClass).InstanceMethod<Action<object, string, string, string>>("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_Void_16Parameters()
        {
            var m = typeof(TestClass).InstanceMethod<Action<TestClass, string, string, string, string, string, string,
                    string, string, string, string, string, string, string, string, string, string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++);
            Assert.AreEqual(TestValue + "0", testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_Void_16Parameters()
        {
            var m = typeof(TestClass).InstanceMethod<Action<object, string, string, string, string, string, string,
                    string, string, string, string, string, string, string, string, string, string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++);
            Assert.AreEqual(TestValue + "0", testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Internal_Method_ByObjectAndTypes_Void_SingleParameter()
        {
            var m = typeof(TestClass).InstanceMethod<Action<TestClass, string>>("InternalMethodVoid");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            m(testClassInstance, TestValue);
            Assert.AreEqual(TestValue, testClassInstance.InternalMethodVoidParameter);
        }

        [TestMethod]
        public void Internal_Method_ByObjectAndTypes_WithConversion_Void_SingleParameter()
        {
            var m = typeof(TestClass).InstanceMethod<Action<object, string>>("InternalMethodVoid");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            m(testClassInstance, TestValue);
            Assert.AreEqual(TestValue, testClassInstance.InternalMethodVoidParameter);
        }

        [TestMethod]
        public void Protected_Method_ByObjectAndTypes_Void_SingleParameter()
        {
            var m = typeof(TestClass).InstanceMethod<Action<TestClass, string>>("ProtectedMethodVoid");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            m(testClassInstance, TestValue);
            Assert.AreEqual(TestValue, testClassInstance.ProtectedMethodVoidParameter);
        }

        [TestMethod]
        public void Protected_Method_ByObjectAndTypes_WithConversion_Void_SingleParameter()
        {
            var m = typeof(TestClass).InstanceMethod<Action<object, string>>("ProtectedMethodVoid");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            m(testClassInstance, TestValue);
            Assert.AreEqual(TestValue, testClassInstance.ProtectedMethodVoidParameter);
        }

        [TestMethod]
        public void Private_Method_ByObjectAndTypes_Void_SingleParameter()
        {
            var m = typeof(TestClass).InstanceMethod<Action<TestClass, string>>("PrivateMethodVoid");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            m(testClassInstance, TestValue);
            Assert.AreEqual(TestValue, testClassInstance.PrivateMethodVoidParameter);
        }

        [TestMethod]
        public void Private_Method_ByObjectAndTypes_WithConversion_Void_SingleParameter()
        {
            var m = typeof(TestClass).InstanceMethod<Action<object, string>>("PrivateMethodVoid");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            m(testClassInstance, TestValue);
            Assert.AreEqual(TestValue, testClassInstance.PrivateMethodVoidParameter);
        }

        [TestMethod]
        public void Method_NonExisting_WrongName_ByObjectAndTypes()
        {
            var m = typeof(TestClass).InstanceMethod<Action<TestClass>>("NonExisting");
            Assert.IsNull(m);
        }

        [TestMethod]
        public void Method_NonExisting_WrongParams_ByObjectAndTypes()
        {
            var m = typeof(TestClass).InstanceMethod<Action<TestClass, TestStruct>>("PublicMethodVoid");
            Assert.IsNull(m);
        }

        [TestMethod]
        public void Method_ByObjectAndTypes_Wrong_TDelegate_Type()
        {
            AssertHelper.ThrowsException<ArgumentException>(
                () => typeof(TestClass).InstanceMethod<string>("PublicMethodVoid"));
        }

        [TestMethod]
        public void Method_ByObjectAndTypes_Wrong_Instance_Type()
        {
            AssertHelper.ThrowsException<ArgumentException>(
                () => typeof(TestClass).InstanceMethod<Action<string>>("PublicMethodVoid"));
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_VoidDelegate_For_NonVoidMethod()
        {
            AssertHelper.ThrowsException<ArgumentException>(() =>
                typeof(TestClass).InstanceMethod<Action<TestClass, string>>("PublicMethod"));
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoidDelegate_For_VoidMethod()
        {
            AssertHelper.ThrowsException<ArgumentException>(() =>
                typeof(TestClass).InstanceMethod<Func<TestClass, string>>("PublicMethodVoid"));
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_VoidDelegate_For_NonVoidMethod()
        {
            AssertHelper.ThrowsException<ArgumentException>(() =>
                typeof(TestClass).InstanceMethod<Action<object, string>>("PublicMethod"));
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_NoVoidDelegate_For_VoidMethod()
        {
            AssertHelper.ThrowsException<ArgumentException>(() =>
                typeof(TestClass).InstanceMethod<Func<object, string>>("PublicMethodVoid"));
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_NoParameters()
        {
            var m = typeof(TestClass).InstanceMethodVoid("PublicMethodVoid");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            m(testClassInstance, new object[] { });
            Assert.AreEqual(true, testClassInstance.PublicMethodVoidExecuted);
        }

        [TestMethod]
        public void Internal_Method_ByObjecs_Void_NoParameters()
        {
            var m = typeof(TestClass).InstanceMethodVoid("InternalMethodVoid");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            m(testClassInstance, new object[] { });
            Assert.AreEqual(true, testClassInstance.InternalMethodVoidExecuted);
        }

        [TestMethod]
        public void Private_Method_ByObjecs_Void_NoParameters()
        {
            var m = typeof(TestClass).InstanceMethodVoid("PrivateMethodVoid");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            m(testClassInstance, new object[] { });
            Assert.AreEqual(true, testClassInstance.PrivateMethodVoidExecuted);
        }

        [TestMethod]
        public void Protected_Method_ByObjecs_Void_NoParameters()
        {
            var m = typeof(TestClass).InstanceMethodVoid("ProtectedMethodVoid");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            m(testClassInstance, new object[] { });
            Assert.AreEqual(true, testClassInstance.ProtectedMethodVoidExecuted);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_SingleParameter()
        {
            var m = typeof(TestClass).InstanceMethodVoid("PublicMethodVoid", typeof(string));
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            m(testClassInstance, new[] {TestValue});
            Assert.AreEqual(TestValue, testClassInstance.PublicMethodVoidParameter);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_SingleParameter_PassedNoParameters()
        {
            var m = typeof(TestClass).InstanceMethodVoid("PublicMethodVoid", typeof(string));
            Assert.IsNotNull(m);
            AssertHelper.ThrowsException<IndexOutOfRangeException>(() => m(new TestClass(), new object[] { }));
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_SingleParameter_PassedMoreParameters()
        {
            var m = typeof(TestClass).InstanceMethodVoid("PublicMethodVoid", typeof(string));
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            m(testClassInstance, new[] {TestValue, TestValue + 1});
            Assert.AreEqual(TestValue, testClassInstance.PublicMethodVoidParameter);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_SingleParameter_PassedNull()
        {
            var m = typeof(TestClass).InstanceMethodVoid("PublicMethodVoid", typeof(string));
            Assert.IsNotNull(m);
            AssertHelper.ThrowsException<NullReferenceException>(() => m(new TestClass(), null));
        }

        [TestMethod]
        public void Public_Method_ByObjecs_WrongParameterType()
        {
            var m = typeof(TestClass).InstanceMethodVoid("PublicMethodVoid", typeof(string));
            Assert.IsNotNull(m);
            AssertHelper.ThrowsException<InvalidCastException>(() => m(new TestClass(), new object[] {0}));
        }

        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_NoParameter()
        {
            var m = typeof(TestClass).InstanceMethod("PublicMethod");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, new object[0]);
            Assert.AreEqual(testClassInstance.PublicParameterlessReturnValue, result);
        }

        [TestMethod]
        public void Internal_Method_ByObjecs_NoVoid_NoParameter()
        {
            var m = typeof(TestClass).InstanceMethod("InternalMethod");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, new object[0]);
            Assert.AreEqual(testClassInstance.InternalParameterlessReturnValue, result);
        }


        [TestMethod]
        public void Protected_Method_ByObjecs_NoVoid_NoParameter()
        {
            var m = typeof(TestClass).InstanceMethod("ProtectedMethod");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, new object[0]);
            Assert.AreEqual(testClassInstance.ProtectedParameterlessReturnValue, result);
        }


        [TestMethod]
        public void Private_Method_ByObjecs_NoVoid_NoParameter()
        {
            var m = typeof(TestClass).InstanceMethod("PrivateMethod");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, new object[0]);
            Assert.AreEqual(testClassInstance.PrivateParameterlessReturnValue, result);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_SingleParameter()
        {
            var m = typeof(TestClass).InstanceMethod("PublicMethod", typeof(string));
            Assert.IsNotNull(m);
            var result = m(new TestClass(), new[] {TestValue});
            Assert.AreEqual(TestValue, result);
        }

        [TestMethod]
        public void Internal_Method_ByObjecs_NoVoid_SingleParameter()
        {
            var m = typeof(TestClass).InstanceMethod("InternalMethod", typeof(string));
            Assert.IsNotNull(m);
            var result = m(new TestClass(), new[] {TestValue});
            Assert.AreEqual(TestValue, result);
        }


        [TestMethod]
        public void Protected_Method_ByObjecs_NoVoid_SingleParameter()
        {
            var m = typeof(TestClass).InstanceMethod("ProtectedMethod", typeof(string));
            Assert.IsNotNull(m);
            var result = m(new TestClass(), new[] {TestValue});
            Assert.AreEqual(TestValue, result);
        }


        [TestMethod]
        public void Private_Method_ByObjecs_NoVoid_SingleParameter()
        {
            var m = typeof(TestClass).InstanceMethod("PrivateMethod", typeof(string));
            Assert.IsNotNull(m);
            var result = m(new TestClass(), new[] {TestValue});
            Assert.AreEqual(TestValue, result);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_2Parameters()
        {
            var m = typeof(TestClass).InstanceMethod("PublicMethod", typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, new object[] {TestValue + index++, TestValue + index++});
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_3Parameters()
        {
            var m = typeof(TestClass).InstanceMethod("PublicMethod", typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            var result = m(testClassInstance,
                new object[] {TestValue + index++, TestValue + index++, TestValue + index++});
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_4Parameters()
        {
            var m = typeof(TestClass).InstanceMethod("PublicMethod", typeof(string), typeof(string), typeof(string),
                typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            var result = m(testClassInstance,
                new object[] {TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++});
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }


        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_5Parameters()
        {
            var m = typeof(TestClass).InstanceMethod("PublicMethod", typeof(string), typeof(string), typeof(string),
                typeof(string),
                typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, new object[]
            {
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
            });
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_6Parameters()
        {
            var m = typeof(TestClass).InstanceMethod("PublicMethod", typeof(string), typeof(string), typeof(string),
                typeof(string),
                typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, new object[]
            {
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++
            });
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_7Parameters()
        {
            var m = typeof(TestClass).InstanceMethod("PublicMethod", typeof(string), typeof(string), typeof(string),
                typeof(string),
                typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, new object[]
            {
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++
            });
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_8Parameters()
        {
            var m = typeof(TestClass).InstanceMethod("PublicMethod", typeof(string), typeof(string), typeof(string),
                typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, new object[]
            {
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
            });
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_9Parameters()
        {
            var m = typeof(TestClass).InstanceMethod("PublicMethod", typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, new object[]
            {
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
            });
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }


        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_10Parameters()
        {
            var m = typeof(TestClass).InstanceMethod("PublicMethod", typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
                typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, new object[]
            {
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
            });
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_11Parameters()
        {
            var m = typeof(TestClass).InstanceMethod("PublicMethod", typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, new object[]
            {
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++
            });
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_12Parameters()
        {
            var m = typeof(TestClass).InstanceMethod("PublicMethod", typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, new object[]
            {
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++
            });
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_13Parameters()
        {
            var m = typeof(TestClass).InstanceMethod("PublicMethod", typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, new object[]
            {
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
            });
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_14Parameters()
        {
            var m = typeof(TestClass).InstanceMethod("PublicMethod", typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, new object[]
            {
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
            });
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_15Parameters()
        {
            var m = typeof(TestClass).InstanceMethod("PublicMethod", typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, new object[]
            {
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
            });
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_16Parameters()
        {
            var m = typeof(TestClass).InstanceMethod("PublicMethod", typeof(string), typeof(string), typeof(string),
                typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
                typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, new object[]
            {
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++
            });
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_2Parameters()
        {
            var m = typeof(TestClass).InstanceMethodVoid("PublicMethodVoid", typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            m(testClassInstance, new object[] {TestValue + index++, TestValue + index++});
            Assert.AreEqual(TestValue + "0", testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_3Parameters()
        {
            var m = typeof(TestClass).InstanceMethodVoid("PublicMethodVoid", typeof(string), typeof(string),
                typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            m(testClassInstance, new object[] {TestValue + index++, TestValue + index++, TestValue + index++});
            Assert.AreEqual(TestValue + "0", testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_4Parameters()
        {
            var m = typeof(TestClass).InstanceMethodVoid("PublicMethodVoid", typeof(string), typeof(string),
                typeof(string),
                typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            m(testClassInstance,
                new object[] {TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++});
            Assert.AreEqual(TestValue + "0", testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_5Parameters()
        {
            var m = typeof(TestClass).InstanceMethodVoid("PublicMethodVoid", typeof(string), typeof(string),
                typeof(string),
                typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            m(testClassInstance,
                new object[]
                {
                    TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                    TestValue + index++
                });
            Assert.AreEqual(TestValue + "0", testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_6Parameters()
        {
            var m = typeof(TestClass).InstanceMethodVoid("PublicMethodVoid", typeof(string), typeof(string),
                typeof(string),
                typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            m(testClassInstance,
                new object[]
                {
                    TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                    TestValue + index++, TestValue + index++
                });
            Assert.AreEqual(TestValue + "0", testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_7Parameters()
        {
            var m = typeof(TestClass).InstanceMethodVoid("PublicMethodVoid", typeof(string), typeof(string),
                typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            m(testClassInstance,
                new object[]
                {
                    TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                    TestValue + index++, TestValue + index++, TestValue + index++
                });
            Assert.AreEqual(TestValue + "0", testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_8Parameters()
        {
            var m = typeof(TestClass).InstanceMethodVoid("PublicMethodVoid", typeof(string), typeof(string),
                typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            m(testClassInstance,
                new object[]
                {
                    TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                    TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                });
            Assert.AreEqual(TestValue + "0", testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_9Parameters()
        {
            var m = typeof(TestClass).InstanceMethodVoid("PublicMethodVoid", typeof(string), typeof(string),
                typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            m(testClassInstance,
                new object[]
                {
                    TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                    TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                    TestValue + index++
                });
            Assert.AreEqual(TestValue + "0", testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_10Parameters()
        {
            var m = typeof(TestClass).InstanceMethodVoid("PublicMethodVoid", typeof(string), typeof(string),
                typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
                typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            m(testClassInstance,
                new object[]
                {
                    TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                    TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                    TestValue + index++, TestValue + index++
                });
            Assert.AreEqual(TestValue + "0", testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_11Parameters()
        {
            var m = typeof(TestClass).InstanceMethodVoid("PublicMethodVoid", typeof(string), typeof(string),
                typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            m(testClassInstance,
                new object[]
                {
                    TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                    TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                    TestValue + index++, TestValue + index++, TestValue + index++
                });
            Assert.AreEqual(TestValue + "0", testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_12Parameters()
        {
            var m = typeof(TestClass).InstanceMethodVoid("PublicMethodVoid", typeof(string), typeof(string),
                typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            m(testClassInstance,
                new object[]
                {
                    TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                    TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                    TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                });
            Assert.AreEqual(TestValue + "0", testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_13Parameters()
        {
            var m = typeof(TestClass).InstanceMethodVoid("PublicMethodVoid", typeof(string), typeof(string),
                typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            m(testClassInstance,
                new object[]
                {
                    TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                    TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                    TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                    TestValue + index++
                });
            Assert.AreEqual(TestValue + "0", testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_14Parameters()
        {
            var m = typeof(TestClass).InstanceMethodVoid("PublicMethodVoid", typeof(string), typeof(string),
                typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            m(testClassInstance,
                new object[]
                {
                    TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                    TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                    TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                    TestValue + index++, TestValue + index++
                });
            Assert.AreEqual(TestValue + "0", testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_15Parameters()
        {
            var m = typeof(TestClass).InstanceMethodVoid("PublicMethodVoid", typeof(string), typeof(string),
                typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            m(testClassInstance,
                new object[]
                {
                    TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                    TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                    TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                    TestValue + index++, TestValue + index++, TestValue + index++
                });
            Assert.AreEqual(TestValue + "0", testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_16Parameters()
        {
            var m = typeof(TestClass).InstanceMethodVoid("PublicMethodVoid", typeof(string), typeof(string),
                typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
                typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            m(testClassInstance,
                new object[]
                {
                    TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                    TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                    TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                    TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                });
            Assert.AreEqual(TestValue + "0", testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Internal_Method_ByObjecs_Void_SingleParameter()
        {
            var m = typeof(TestClass).InstanceMethod<Action<TestClass, string>>("InternalMethodVoid");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            m(testClassInstance, TestValue);
            Assert.AreEqual(TestValue, testClassInstance.InternalMethodVoidParameter);
        }

        [TestMethod]
        public void Protected_Method_ByObjecs_Void_SingleParameter()
        {
            var m = typeof(TestClass).InstanceMethod<Action<TestClass, string>>("ProtectedMethodVoid");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            m(testClassInstance, TestValue);
            Assert.AreEqual(TestValue, testClassInstance.ProtectedMethodVoidParameter);
        }

        [TestMethod]
        public void Private_Method_ByObjecs_Void_SingleParameter()
        {
            var m = typeof(TestClass).InstanceMethod<Action<TestClass, string>>("PrivateMethodVoid");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            m(testClassInstance, TestValue);
            Assert.AreEqual(TestValue, testClassInstance.PrivateMethodVoidParameter);
        }

        [TestMethod]
        public void Method_NonExisting_WrongName_ByObjects()
        {
            var m = typeof(TestClass).InstanceMethod<Action<TestClass>>("NonExisting");
            Assert.IsNull(m);
        }

        [TestMethod]
        public void Method_NonExisting_WrongName_WithConversion_ByObjects()
        {
            var m = typeof(TestClass).InstanceMethod<Action<object>>("NonExisting");
            Assert.IsNull(m);
        }

        [TestMethod]
        public void Method_NonExisting_WrongParams_ByObjects()
        {
            var m = typeof(TestClass).InstanceMethod<Action<TestClass, TestStruct>>("PublicMethodVoid");
            Assert.IsNull(m);
        }

        [TestMethod]
        public void Method_ByObjecs_Wrong_TDelegate_Type()
        {
            AssertHelper.ThrowsException<ArgumentException>(
                () => typeof(TestClass).InstanceMethod<string>("PublicMethodVoid"));
        }

        [TestMethod]
        public void Public_Method_ByObjects_WithConversion_VoidDelegate_For_NonVoidMethod()
        {
            AssertHelper.ThrowsException<ArgumentException>(() =>
                typeof(TestClass).InstanceMethodVoid("PublicMethod", typeof(string)));
        }

        [TestMethod]
        public void Public_Method_ByObjects_WithConversion_NoVoidDelegate_For_VoidMethod()
        {
            AssertHelper.ThrowsException<ArgumentException>(() =>
                typeof(TestClass).InstanceMethod("PublicMethodVoid", typeof(string)));
        }

        [TestMethod]
        public void CustomDelegate_ByTypes_Void_NoParams()
        {
            var m = DelegateFactory.InstanceMethod<CustomAction<TestClass>>("PublicMethodVoid");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            m(testClassInstance);
            Assert.AreEqual(true, testClassInstance.PublicMethodVoidExecuted);
        }

        [TestMethod]
        public void CustomDelegate_ByTypes_Void_SingleParams()
        {
            var m = DelegateFactory.InstanceMethod<CustomActionSingleParam<TestClass>>("PublicMethodVoid");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            m(testClassInstance, TestValue);
            Assert.AreEqual(TestValue, testClassInstance.PublicMethodVoidParameter);
        }

        [TestMethod]
        public void CustomDelegate_ByTypes_NoVoid_Param()
        {
            var m = DelegateFactory.InstanceMethod<CustomFunc<TestClass>>("PublicMethod");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            var result = m(testClassInstance);
            Assert.AreEqual(result, testClassInstance.PublicParameterlessReturnValue);
        }

        [TestMethod]
        public void CustomDelegate_ByObjectAndTypes_Void_NoParams()
        {
            var m = typeof(TestClass).InstanceMethod<CustomAction<TestClass>>("PublicMethodVoid");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            m(testClassInstance);
            Assert.AreEqual(true, testClassInstance.PublicMethodVoidExecuted);
        }

        [TestMethod]
        public void CustomDelegate_ByObjectAndTypes_Void_SingleParams()
        {
            var m = typeof(TestClass).InstanceMethod<CustomActionSingleParam<TestClass>>("PublicMethodVoid");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            m(testClassInstance, TestValue);
            Assert.AreEqual(TestValue, testClassInstance.PublicMethodVoidParameter);
        }

        [TestMethod]
        public void CustomDelegate_ByObjectAndTypes_NoVoid_Param()
        {
            var m = typeof(TestClass).InstanceMethod<CustomFunc<TestClass>>("PublicMethod");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            var result = m(testClassInstance);
            Assert.AreEqual(result, testClassInstance.PublicParameterlessReturnValue);
        }

        [TestMethod]
        public void InterfaceMethod_ByTypes()
        {
            var m = DelegateFactory.InstanceMethod<Func<IService, string, string>>("Echo");
            Assert.IsNotNull(m);
            var result = m(new Service(), TestValue);
            Assert.AreEqual(TestValue, result);
        }

        [TestMethod]
        public void InterfaceMethod_ByObjectAndTypes()
        {
            var m = typeof(IService).InstanceMethod<Func<IService, string, string>>("Echo");
            Assert.IsNotNull(m);
            var result = m(new Service(), TestValue);
            Assert.AreEqual(TestValue, result);
        }

        [TestMethod]
        public void InterfaceMethod_ByObjectAndTypes_WithCast()
        {
            var m = typeof(IService).InstanceMethod<Func<object, string, string>>("Echo");
            Assert.IsNotNull(m);
            var result = m(new Service(), TestValue);
            Assert.AreEqual(TestValue, result);
        }

        [TestMethod]
        public void InterfaceMethod_ByObjects()
        {
            var m = typeof(IService).InstanceMethod("Echo", typeof(string));
            Assert.IsNotNull(m);
            var result = m(new Service(), new object[] {TestValue});
            Assert.AreEqual(TestValue, result);
        }

#if !NET35
        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_4Parameters()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, string, string, string, string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_5Parameters()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, string, string, string, string, string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++,
                TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_6Parameters()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, string, string, string, string, string, string,
                string>>("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_7Parameters()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, string, string, string, string, string, string,
                    string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_8Parameters()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, string, string, string, string, string, string,
                    string, string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_9Parameters()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, string, string, string, string, string, string,
                    string, string, string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }


        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_10Parameters()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, string, string, string, string, string, string,
                    string, string, string, string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_11Parameters()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, string, string, string, string, string, string,
                    string, string, string, string, string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_12Parameters()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, string, string, string, string, string, string,
                    string, string, string, string, string, string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_13Parameters()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, string, string, string, string, string, string,
                    string, string, string, string, string, string, string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_14Parameters()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, string, string, string, string, string, string,
                    string, string, string, string, string, string, string, string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_15Parameters()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, string, string, string, string, string, string,
                    string, string, string, string, string, string, string, string, string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_16Parameters()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, string, string, string, string, string, string,
                    string, string, string, string, string, string, string, string, string, string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }
#endif

#if !NET35
        [TestMethod]
        public void Public_Method_ByTypes_Void_4Parameters()
        {
            var m =
                DelegateFactory.InstanceMethod<Action<TestClass, string, string, string, string>>("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByTypes_Void_5Parameters()
        {
            var m = DelegateFactory.InstanceMethod<Action<TestClass, string, string, string, string, string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++);
            Assert.AreEqual(TestValue + "0", testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByTypes_Void_6Parameters()
        {
            var m = DelegateFactory.InstanceMethod<Action<TestClass, string, string, string, string, string, string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++);
            Assert.AreEqual(TestValue + "0", testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByTypes_Void_7Parameters()
        {
            var m = DelegateFactory.InstanceMethod<Action<TestClass, string, string, string, string, string, string,
                    string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByTypes_Void_8Parameters()
        {
            var m = DelegateFactory.InstanceMethod<Action<TestClass, string, string, string, string, string, string,
                    string, string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByTypes_Void_9Parameters()
        {
            var m = DelegateFactory.InstanceMethod<Action<TestClass, string, string, string, string, string, string,
                    string, string, string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByTypes_Void_10Parameters()
        {
            var m = DelegateFactory.InstanceMethod<Action<TestClass, string, string, string, string, string, string,
                    string, string, string, string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++);
            Assert.AreEqual(TestValue + "0", testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByTypes_Void_11Parameters()
        {
            var m = DelegateFactory.InstanceMethod<Action<TestClass, string, string, string, string, string, string,
                    string, string, string, string, string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++);
            Assert.AreEqual(TestValue + "0", testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByTypes_Void_12Parameters()
        {
            var m = DelegateFactory.InstanceMethod<Action<TestClass, string, string, string, string, string, string,
                    string, string, string, string, string, string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByTypes_Void_13Parameters()
        {
            var m = DelegateFactory.InstanceMethod<Action<TestClass, string, string, string, string, string, string,
                    string, string, string, string, string, string, string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByTypes_Void_14Parameters()
        {
            var m = DelegateFactory.InstanceMethod<Action<TestClass, string, string, string, string, string, string,
                    string, string, string, string, string, string, string, string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByTypes_Void_15Parameters()
        {
            var m = DelegateFactory.InstanceMethod<Action<TestClass, string, string, string, string, string, string,
                    string, string, string, string, string, string, string, string, string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++);
            Assert.AreEqual(TestValue + "0", testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByTypes_Void_16Parameters()
        {
            var m = DelegateFactory.InstanceMethod<Action<TestClass, string, string, string, string, string, string,
                    string, string, string, string, string, string, string, string, string, string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++);
            Assert.AreEqual(TestValue + "0", testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }
#endif

#if !NET35
        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_4Parameters()
        {
            var m =
                typeof(TestClass).InstanceMethod<Func<TestClass, string, string, string, string, string>>("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_NoVoid_4Parameters()
        {
            var m = typeof(TestClass).InstanceMethod<Func<object, string, string, string, string, string>>("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_5Parameters()
        {
            var m = typeof(TestClass).InstanceMethod<Func<TestClass, string, string, string, string, string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++,
                TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_NoVoid_5Parameters()
        {
            var m = typeof(TestClass).InstanceMethod<Func<object, string, string, string, string, string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++,
                TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_6Parameters()
        {
            var m = typeof(TestClass)
                .InstanceMethod<Func<TestClass, string, string, string, string, string, string, string>>
                    ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++,
                TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_NoVoid_6Parameters()
        {
            var m = typeof(TestClass).InstanceMethod<Func<object, string, string, string, string, string, string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++,
                TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_7Parameters()
        {
            var m = typeof(TestClass).InstanceMethod<Func<TestClass, string, string, string, string, string, string,
                    string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_NoVoid_7Parameters()
        {
            var m = typeof(TestClass).InstanceMethod<Func<object, string, string, string, string, string, string,
                    string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_8Parameters()
        {
            var m = typeof(TestClass).InstanceMethod<Func<TestClass, string, string, string, string, string, string,
                    string, string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_NoVoid_8Parameters()
        {
            var m = typeof(TestClass).InstanceMethod<Func<object, string, string, string, string, string, string,
                    string, string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_9Parameters()
        {
            var m = typeof(TestClass).InstanceMethod<Func<TestClass, string, string, string, string, string, string,
                    string, string, string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_NoVoid_9Parameters()
        {
            var m = typeof(TestClass).InstanceMethod<Func<object, string, string, string, string, string, string,
                    string, string, string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_10Parameters()
        {
            var m = typeof(TestClass).InstanceMethod<Func<TestClass, string, string, string, string, string, string,
                    string, string, string, string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_NoVoid_10Parameters()
        {
            var m = typeof(TestClass).InstanceMethod<Func<object, string, string, string, string, string, string,
                    string, string, string, string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_11Parameters()
        {
            var m = typeof(TestClass).InstanceMethod<Func<TestClass, string, string, string, string, string, string,
                    string, string, string, string, string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_NoVoid_11Parameters()
        {
            var m = typeof(TestClass).InstanceMethod<Func<object, string, string, string, string, string, string,
                    string, string, string, string, string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_12Parameters()
        {
            var m = typeof(TestClass).InstanceMethod<Func<TestClass, string, string, string, string, string, string,
                    string, string, string, string, string, string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_NoVoid_12Parameters()
        {
            var m = typeof(TestClass).InstanceMethod<Func<object, string, string, string, string, string, string,
                    string, string, string, string, string, string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_13Parameters()
        {
            var m = typeof(TestClass).InstanceMethod<Func<TestClass, string, string, string, string, string, string,
                    string, string, string, string, string, string, string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_NoVoid_13Parameters()
        {
            var m = typeof(TestClass).InstanceMethod<Func<object, string, string, string, string, string, string,
                    string, string, string, string, string, string, string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_14Parameters()
        {
            var m = typeof(TestClass).InstanceMethod<Func<TestClass, string, string, string, string, string, string,
                    string, string, string, string, string, string, string, string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_NoVoid_14Parameters()
        {
            var m = typeof(TestClass).InstanceMethod<Func<object, string, string, string, string, string, string,
                    string, string, string, string, string, string, string, string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_15Parameters()
        {
            var m = typeof(TestClass).InstanceMethod<Func<TestClass, string, string, string, string, string, string,
                    string, string, string, string, string, string, string, string, string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_NoVoid_15Parameters()
        {
            var m = typeof(TestClass).InstanceMethod<Func<object, string, string, string, string, string, string,
                    string, string, string, string, string, string, string, string, string, string>>
                ("PublicMethod");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }
#endif

#if !NET35
        [TestMethod]
        public void Public_Method_ByObjectAndTypes_Void_4Parameters()
        {
            var m = typeof(TestClass)
                .InstanceMethod<Action<TestClass, string, string, string, string>>("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_Void_4Parameters()
        {
            var m = typeof(TestClass).InstanceMethod<Action<object, string, string, string, string>>("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_Void_5Parameters()
        {
            var m = typeof(TestClass).InstanceMethod<Action<TestClass, string, string, string, string, string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++);
            Assert.AreEqual(TestValue + "0", testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_Void_5Parameters()
        {
            var m = typeof(TestClass).InstanceMethod<Action<object, string, string, string, string, string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++);
            Assert.AreEqual(TestValue + "0", testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_Void_6Parameters()
        {
            var m = typeof(TestClass).InstanceMethod<Action<TestClass, string, string, string, string, string, string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++);
            Assert.AreEqual(TestValue + "0", testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_Void_6Parameters()
        {
            var m = typeof(TestClass).InstanceMethod<Action<object, string, string, string, string, string, string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++);
            Assert.AreEqual(TestValue + "0", testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_Void_7Parameters()
        {
            var m = typeof(TestClass).InstanceMethod<Action<TestClass, string, string, string, string, string, string,
                    string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_Void_7Parameters()
        {
            var m = typeof(TestClass).InstanceMethod<Action<object, string, string, string, string, string, string,
                    string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_Void_8Parameters()
        {
            var m = typeof(TestClass).InstanceMethod<Action<TestClass, string, string, string, string, string, string,
                    string, string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_Void_8Parameters()
        {
            var m = typeof(TestClass).InstanceMethod<Action<object, string, string, string, string, string, string,
                    string, string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_Void_9Parameters()
        {
            var m = typeof(TestClass).InstanceMethod<Action<TestClass, string, string, string, string, string, string,
                    string, string, string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_Void_9Parameters()
        {
            var m = typeof(TestClass).InstanceMethod<Action<object, string, string, string, string, string, string,
                    string, string, string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_Void_10Parameters()
        {
            var m = typeof(TestClass).InstanceMethod<Action<TestClass, string, string, string, string, string, string,
                    string, string, string, string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++);
            Assert.AreEqual(TestValue + "0", testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_Void_10Parameters()
        {
            var m = typeof(TestClass).InstanceMethod<Action<object, string, string, string, string, string, string,
                    string, string, string, string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++);
            Assert.AreEqual(TestValue + "0", testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_Void_11Parameters()
        {
            var m = typeof(TestClass).InstanceMethod<Action<TestClass, string, string, string, string, string, string,
                    string, string, string, string, string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++);
            Assert.AreEqual(TestValue + "0", testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_Void_11Parameters()
        {
            var m = typeof(TestClass).InstanceMethod<Action<object, string, string, string, string, string, string,
                    string, string, string, string, string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++);
            Assert.AreEqual(TestValue + "0", testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_Void_12Parameters()
        {
            var m = typeof(TestClass).InstanceMethod<Action<TestClass, string, string, string, string, string, string,
                    string, string, string, string, string, string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_Void_12Parameters()
        {
            var m = typeof(TestClass).InstanceMethod<Action<object, string, string, string, string, string, string,
                    string, string, string, string, string, string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_Void_13Parameters()
        {
            var m = typeof(TestClass).InstanceMethod<Action<TestClass, string, string, string, string, string, string,
                    string, string, string, string, string, string, string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_Void_13Parameters()
        {
            var m = typeof(TestClass).InstanceMethod<Action<object, string, string, string, string, string, string,
                    string, string, string, string, string, string, string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_Void_14Parameters()
        {
            var m = typeof(TestClass).InstanceMethod<Action<TestClass, string, string, string, string, string, string,
                    string, string, string, string, string, string, string, string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_Void_14Parameters()
        {
            var m = typeof(TestClass).InstanceMethod<Action<object, string, string, string, string, string, string,
                    string, string, string, string, string, string, string, string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_Void_15Parameters()
        {
            var m = typeof(TestClass).InstanceMethod<Action<TestClass, string, string, string, string, string, string,
                    string, string, string, string, string, string, string, string, string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++);
            Assert.AreEqual(TestValue + "0", testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_WithConversion_Void_15Parameters()
        {
            var m = typeof(TestClass).InstanceMethod<Action<object, string, string, string, string, string, string,
                    string, string, string, string, string, string, string, string, string>>
                ("PublicMethodVoid");
            Assert.IsNotNull(m);
            var index = 0;
            var testClassInstance = new TestClass();
            m(testClassInstance, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++);
            Assert.AreEqual(TestValue + "0", testClassInstance.PublicMethodVoidParameter);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, testClassInstance.PublicParams[i]);
        }
#endif
    }
}