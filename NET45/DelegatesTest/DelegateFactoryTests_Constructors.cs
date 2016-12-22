// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DelegateFactoryTests_Constructors.cs" company="Natan Podbielski">
//   Copyright (c) 2016 - 2016 Natan Podbielski. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

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
    public class DelegateFactoryTests_Constructors
    {
        private static readonly Type TestClassNoDefaultCtorType = typeof(TestClassNoDefaultCtor);
        private static readonly Type TestClassType = typeof(TestClass);
        private static readonly Type TestStructType = typeof(TestStruct);
        
        [TestMethod]
        public void ConstructorByDelegateWithType_BoolParam()
        {
            var c = DelegateFactory.Contructor<Func<bool, TestClass>>();
            Assert.IsNotNull(c);
            var instance = c(false);
            Assert.IsNotNull(instance);
            Assert.IsInstanceOfType(instance, TestClassType);
        }

        [TestMethod]
        public void ConstructorByDelegateWithType_IntParam()
        {
            var c = DelegateFactory.Contructor<Func<int, TestClass>>();
            Assert.IsNotNull(c);
            var instance = c(0);
            Assert.IsNotNull(instance);
            Assert.IsInstanceOfType(instance, TestClassType);
        }

        [TestMethod]
        public void ConstructorByDelegateWithType_IntParam_FromStruct()
        {
            var c = DelegateFactory.Contructor<Func<int, TestStruct>>();
            Assert.IsNotNull(c);
            var instance = c(0);
            Assert.IsInstanceOfType(instance, TestStructType);
        }

        [TestMethod]
        public void ConstructorByDelegateWithType_NoCtor_BoolIntParam()
        {
            var c = DelegateFactory.Contructor<Func<bool, int, TestClassNoDefaultCtor>>();
            Assert.IsNull(c);
        }

        [TestMethod]
        public void ConstructorByDelegateWithType_NoCtor_BoolParam_FromStruct()
        {
            var c = DelegateFactory.Contructor<Func<bool, TestStruct>>();
            Assert.IsNull(c);
        }

        [TestMethod]
        public void ConstructorByDelegateWithType_NoCtor_IntParam()
        {
            var c = DelegateFactory.Contructor<Func<int, TestClassNoDefaultCtor>>();
            Assert.IsNull(c);
        }

        [TestMethod]
        public void ConstructorByDelegateWithType_NoCtor_NoParams()
        {
            var c = DelegateFactory.Contructor<Func<TestClassNoDefaultCtor>>();
            Assert.IsNull(c);
        }

        [TestMethod]
        public void ConstructorByDelegateWithType_NoCtor_NoParams_FromStruct()
        {
            var c = DelegateFactory.Contructor<Func<TestStruct>>();
            Assert.IsNull(c);
        }

        [TestMethod]
        public void ConstructorByDelegateWithType_NoCtor_StringBoolIntParam()
        {
            var c = DelegateFactory.Contructor<Func<string, bool, int, TestClassNoDefaultCtor>>();
            Assert.IsNull(c);
        }

        [TestMethod]
        public void ConstructorByDelegateWithType_NoParams()
        {
            var c = DelegateFactory.Contructor<Func<TestClass>>();
            Assert.IsNotNull(c);
            Assert.IsNotNull(c());
            Assert.IsInstanceOfType(c(), TestClassType);
        }

        [TestMethod]
        public void ConstructorByDelegateWithType_StringParam()
        {
            var c = DelegateFactory.Contructor<Func<string, TestClass>>();
            Assert.IsNotNull(c);
            var instance = c("s");
            Assert.IsNotNull(instance);
            Assert.IsInstanceOfType(instance, TestClassType);
        }

        [TestMethod]
        public void ConstructorByExtensionMethodAndObject_BoolParam()
        {
            var c = TestClassType.Contructor<Func<bool, object>>();
            Assert.IsNotNull(c);
            var instance = c(false);
            Assert.IsNotNull(instance);
            Assert.IsInstanceOfType(instance, TestClassType);
        }

        [TestMethod]
        public void ConstructorByExtensionMethodAndObject_IntParam()
        {
            var c = TestClassType.Contructor<Func<int, object>>();
            Assert.IsNotNull(c);
            var instance = c(0);
            Assert.IsNotNull(instance);
            Assert.IsInstanceOfType(instance, TestClassType);
        }

        [TestMethod]
        public void ConstructorByExtensionMethodAndObject_IntParam_FromStruct()
        {
            var c = TestStructType.Contructor<Func<int, object>>();
            Assert.IsNotNull(c);
            var instance = c(0);
            Assert.IsNotNull(instance);
            Assert.IsInstanceOfType(instance, TestStructType);
        }

        [TestMethod]
        public void ConstructorByExtensionMethodAndObject_StringParam()
        {
            var c = TestClassType.Contructor<Func<string, object>>();
            Assert.IsNotNull(c);
            var instance = c("s");
            Assert.IsNotNull(instance);
            Assert.IsInstanceOfType(instance, TestClassType);
        }

        [TestMethod]
        public void ConstructorByExtensionMethodAndType_BoolParam()
        {
            var c = TestClassType.Contructor<Func<bool, TestClass>>();
            Assert.IsNotNull(c);
            var instance = c(false);
            Assert.IsNotNull(instance);
            Assert.IsInstanceOfType(instance, TestClassType);
        }

        [TestMethod]
        public void ConstructorByExtensionMethodAndType_IntParam()
        {
            var c = TestClassType.Contructor<Func<int, TestClass>>();
            Assert.IsNotNull(c);
            var instance = c(0);
            Assert.IsNotNull(instance);
            Assert.IsInstanceOfType(instance, TestClassType);
        }

        [TestMethod]
        public void ConstructorByExtensionMethodAndType_StringParam()
        {
            var c = TestClassType.Contructor<Func<string, TestClass>>();
            Assert.IsNotNull(c);
            var instance = c("s");
            Assert.IsNotNull(instance);
            Assert.IsInstanceOfType(instance, TestClassType);
        }

        [TestMethod]
        public void ConstructorByObjects_BoolParam()
        {
            var c = TestClassType.Contructor(typeof(bool));
            Assert.IsNotNull(c);
            var instance = c(new object[] {false});
            Assert.IsNotNull(instance);
            Assert.IsInstanceOfType(instance, TestClassType);
        }

        [TestMethod]
        public void ConstructorByObjects_IntParam()
        {
            var c = TestClassType.Contructor(typeof(int));
            Assert.IsNotNull(c);
            var instance = c(new object[] {0});
            Assert.IsNotNull(instance);
            Assert.IsInstanceOfType(instance, TestClassType);
        }

        [TestMethod]
        public void ConstructorByObjects_IntParam_FromStruct()
        {
            var c = TestStructType.Contructor(typeof(int));
            Assert.IsNotNull(c);
            var instance = c(new object[] {0});
            Assert.IsNotNull(instance);
            Assert.IsInstanceOfType(instance, TestStructType);
        }

        [TestMethod]
        public void ConstructorByObjects_NoParam()
        {
            var c = TestClassType.Contructor();
            Assert.IsNotNull(c);
            var instance = c(null);
            Assert.IsNotNull(instance);
            Assert.IsInstanceOfType(instance, TestClassType);
        }

        [TestMethod]
        public void ConstructorByObjects_StringParam()
        {
            var c = TestClassType.Contructor(typeof(string));
            Assert.IsNotNull(c);
            var instance = c(new object[] {"s"});
            Assert.IsNotNull(instance);
            Assert.IsInstanceOfType(instance, TestClassType);
        }

        [TestMethod]
        public void DefaultConstructor_ByDelegate_WithoutType_ByDelegate()
        {
            var cd = TestClassType.Contructor<Func<object>>();
            Assert.IsNotNull(cd);
            Assert.IsNotNull(cd());
            Assert.IsInstanceOfType(cd(), TestClassType);
        }

        [TestMethod]
        public void DefaultConstructor_ByDelegate_WithType_ByDelegate()
        {
            var cd = TestClassType.Contructor<Func<TestClass>>();
            Assert.IsNotNull(cd);
            Assert.IsNotNull(cd());
            Assert.IsInstanceOfType(cd(), TestClassType);
        }

        [TestMethod]
        public void DefaultConstructor_WithoutType()
        {
            var cd = TestClassType.DefaultContructor();
            Assert.IsNotNull(cd);
            Assert.IsNotNull(cd());
            Assert.IsInstanceOfType(cd(), TestClassType);
        }

        [TestMethod]
        public void DefaultConstructor_WithoutType_ByDelegate_NoDefault_FromStruct()
        {
            var cd = TestStructType.Contructor<Func<object>>();
            Assert.IsNull(cd);
        }

        [TestMethod]
        public void DefaultConstructor_WithoutType_ByDelegateWithType_NoDefault_Class()
        {
            var cd = TestClassNoDefaultCtorType.Contructor<Func<TestClassNoDefaultCtor>>();
            Assert.IsNull(cd);
        }

        [TestMethod]
        public void DefaultConstructor_WithoutType_ByDelegateWithType_NoDefault_FromStruct()
        {
            var cd = TestStructType.Contructor<Func<TestStruct>>();
            Assert.IsNull(cd);
        }

        [TestMethod]
        public void DefaultConstructor_WithoutType_NoDefault_Class()
        {
            var cd = TestClassNoDefaultCtorType.DefaultContructor();
            Assert.IsNull(cd);
        }

        [TestMethod]
        public void DefaultConstructor_WithType()
        {
            var cd = DelegateFactory.DefaultContructor<TestClass>();
            Assert.IsNotNull(cd);
            Assert.IsNotNull(cd());
            Assert.IsInstanceOfType(cd(), TestClassType);
        }

        [TestMethod]
        public void DefaultConstructor_WithType_NoDefault_Class()
        {
            var cd = DelegateFactory.DefaultContructor<TestClassNoDefaultCtor>();
            Assert.IsNull(cd);
        }

        [TestMethod]
        public void DefaultConstructor_WithType_NoDefault_FromStruct()
        {
            var cd = DelegateFactory.DefaultContructor<TestStruct>();
            Assert.IsNull(cd);
        }
    }
}