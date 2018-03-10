// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DelegateFactoryTests_Constructors.cs" company="Natan Podbielski">
//   Copyright (c) 2016 - 2018 Natan Podbielski. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using Delegates;
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
    public class DelegateFactoryTests_Constructors
    {
        //TODO: tests for delegate return type. it cannot be void
        //TODO: tests for delegate with return type compatible with source type i.e. interface of a class
        //TODO: tests custom delegate types
        //TODO: tests more numbers of parameters than 3
        //TODO: tests values and order of constructors parameters
        [TestMethod]
        public void ConstructorByDelegateWithType_BoolParam()
        {
            var c = DelegateFactory.Constructor<Func<bool, TestClass>>();
            Assert.IsNotNull(c);
            var instance = c(false);
            Assert.IsNotNull(instance);
            Assert.IsInstanceOfType(instance, typeof(TestClass));
        }

        [TestMethod]
        public void ConstructorByDelegateWithType_IntParam()
        {
            var c = DelegateFactory.Constructor<Func<int, TestClass>>();
            Assert.IsNotNull(c);
            var instance = c(0);
            Assert.IsNotNull(instance);
            Assert.IsInstanceOfType(instance, typeof(TestClass));
        }

        [TestMethod]
        public void ConstructorByDelegateWithType_IntParam_FromStruct()
        {
            var c = DelegateFactory.Constructor<Func<int, TestStruct>>();
            Assert.IsNotNull(c);
            var instance = c(0);
            Assert.IsInstanceOfType(instance, typeof(TestStruct));
        }

        [TestMethod]
        public void ConstructorByDelegateWithType_NoCtor_BoolIntParam()
        {
            var c = DelegateFactory.Constructor<Func<bool, int, TestClassNoDefaultCtor>>();
            Assert.IsNull(c);
        }

        [TestMethod]
        public void ConstructorByDelegateWithType_NoCtor_BoolParam_FromStruct()
        {
            var c = DelegateFactory.Constructor<Func<bool, TestStruct>>();
            Assert.IsNull(c);
        }

        [TestMethod]
        public void ConstructorByDelegateWithType_NoCtor_IntParam()
        {
            var c = DelegateFactory.Constructor<Func<int, TestClassNoDefaultCtor>>();
            Assert.IsNull(c);
        }

        [TestMethod]
        public void ConstructorByDelegateWithType_NoCtor_NoParams()
        {
            var c = DelegateFactory.Constructor<Func<TestClassNoDefaultCtor>>();
            Assert.IsNull(c);
        }

        [TestMethod]
        public void ConstructorByDelegateWithType_NoCtor_NoParams_FromStruct()
        {
            var c = DelegateFactory.Constructor<Func<TestStruct>>();
            Assert.IsNull(c);
        }

        [TestMethod]
        public void ConstructorByDelegateWithType_NoCtor_StringBoolIntParam()
        {
            var c = DelegateFactory.Constructor<Func<string, bool, int, TestClassNoDefaultCtor>>();
            Assert.IsNull(c);
        }

        [TestMethod]
        public void ConstructorByDelegateWithType_NoParams()
        {
            var c = DelegateFactory.Constructor<Func<TestClass>>();
            Assert.IsNotNull(c);
            Assert.IsNotNull(c());
            Assert.IsInstanceOfType(c(), typeof(TestClass));
        }

        [TestMethod]
        public void ConstructorByDelegateWithType_WithCustomDelegate_NoParams()
        {
            var cd = DelegateFactory.Constructor<CustomCtr>();
            Assert.IsNotNull(cd);
            Assert.IsNotNull(cd());
            Assert.IsInstanceOfType(cd(), typeof(TestClass));
        }

        [TestMethod]
        public void ConstructorByDelegateWithType_WithCustomDelegate_SingleParam()
        {
            var cd = DelegateFactory.Constructor<CustomCtrSingleParam>();
            Assert.IsNotNull(cd);
            var instance = cd(0);
            Assert.IsNotNull(instance);
            Assert.IsInstanceOfType(instance, typeof(TestClass));
        }

        [TestMethod]
        public void ConstructorByDelegateWithType_StringParam()
        {
            var c = DelegateFactory.Constructor<Func<string, TestClass>>();
            Assert.IsNotNull(c);
            var instance = c("s");
            Assert.IsNotNull(instance);
            Assert.IsInstanceOfType(instance, typeof(TestClass));
        }

        [TestMethod]
        public void ConstructorByExtensionMethodAndObject_BoolParam()
        {
            var testClassType = typeof(TestClass);
            var c = testClassType.Constructor<Func<bool, object>>();
            Assert.IsNotNull(c);
            var instance = c(false);
            Assert.IsNotNull(instance);
            Assert.IsInstanceOfType(instance, testClassType);
        }

        [TestMethod]
        public void ConstructorByExtensionMethodAndObject_IntParam()
        {
            var testClassType = typeof(TestClass);
            var c = testClassType.Constructor<Func<int, object>>();
            Assert.IsNotNull(c);
            var instance = c(0);
            Assert.IsNotNull(instance);
            Assert.IsInstanceOfType(instance, testClassType);
        }

        [TestMethod]
        public void ConstructorByExtensionMethodAndObject_IntParam_FromStruct()
        {
            var testStructType = typeof(TestStruct);
            var c = testStructType.Constructor<Func<int, object>>();
            Assert.IsNotNull(c);
            var instance = c(0);
            Assert.IsNotNull(instance);
            Assert.IsInstanceOfType(instance, testStructType);
        }

        [TestMethod]
        public void ConstructorByExtensionMethodAndObject_StringParam()
        {
            var testClassType = typeof(TestClass);
            var c = testClassType.Constructor<Func<string, object>>();
            Assert.IsNotNull(c);
            var instance = c("s");
            Assert.IsNotNull(instance);
            Assert.IsInstanceOfType(instance, testClassType);
        }

        [TestMethod]
        public void ConstructorByExtensionMethodAndType_BoolParam()
        {
            var testClassType = typeof(TestClass);
            var c = testClassType.Constructor<Func<bool, TestClass>>();
            Assert.IsNotNull(c);
            var instance = c(false);
            Assert.IsNotNull(instance);
            Assert.IsInstanceOfType(instance, testClassType);
        }

        [TestMethod]
        public void ConstructorByExtensionMethodAndType_IntParam()
        {
            var testClassType = typeof(TestClass);
            var c = testClassType.Constructor<Func<int, TestClass>>();
            Assert.IsNotNull(c);
            var instance = c(0);
            Assert.IsNotNull(instance);
            Assert.IsInstanceOfType(instance, testClassType);
        }

        [TestMethod]
        public void ConstructorByExtensionMethodAndType_StringParam()
        {
            var testClassType = typeof(TestClass);
            var c = testClassType.Constructor<Func<string, TestClass>>();
            Assert.IsNotNull(c);
            var instance = c("s");
            Assert.IsNotNull(instance);
            Assert.IsInstanceOfType(instance, testClassType);
        }

        [TestMethod]
        public void ConstructorByObjects_BoolParam()
        {
            var testClassType = typeof(TestClass);
            var c = testClassType.Constructor(typeof(bool));
            Assert.IsNotNull(c);
            var instance = c(new object[] {false});
            Assert.IsNotNull(instance);
            Assert.IsInstanceOfType(instance, testClassType);
        }

        [TestMethod]
        public void ConstructorByObjects_IntParam()
        {
            var testClassType = typeof(TestClass);
            var c = testClassType.Constructor(typeof(int));
            Assert.IsNotNull(c);
            var instance = c(new object[] {0});
            Assert.IsNotNull(instance);
            Assert.IsInstanceOfType(instance, testClassType);
        }

        [TestMethod]
        public void ConstructorByObjects_IntParam_FromStruct()
        {
            var testStructType = typeof(TestStruct);
            var c = testStructType.Constructor(typeof(int));
            Assert.IsNotNull(c);
            var instance = c(new object[] {0});
            Assert.IsNotNull(instance);
            Assert.IsInstanceOfType(instance, testStructType);
        }

        [TestMethod]
        public void ConstructorByObjects_NoParam()
        {
            var testClassType = typeof(TestClass);
            var c = testClassType.Constructor();
            Assert.IsNotNull(c);
            var instance = c(null);
            Assert.IsNotNull(instance);
            Assert.IsInstanceOfType(instance, testClassType);
        }

        [TestMethod]
        public void ConstructorByObjects_StringParam()
        {
            var testClassType = typeof(TestClass);
            var c = testClassType.Constructor(typeof(string));
            Assert.IsNotNull(c);
            var instance = c(new object[] {"s"});
            Assert.IsNotNull(instance);
            Assert.IsInstanceOfType(instance, testClassType);
        }

        [TestMethod]
        public void DefaultConstructor_ByDelegate_WithoutType_ByDelegate()
        {
            var testClassType = typeof(TestClass);
            var cd = testClassType.Constructor<Func<object>>();
            Assert.IsNotNull(cd);
            Assert.IsNotNull(cd());
            Assert.IsInstanceOfType(cd(), testClassType);
        }

        [TestMethod]
        public void DefaultConstructor_ByDelegate_WithType_ByDelegate()
        {
            var testClassType = typeof(TestClass);
            var cd = testClassType.Constructor<Func<TestClass>>();
            Assert.IsNotNull(cd);
            Assert.IsNotNull(cd());
            Assert.IsInstanceOfType(cd(), testClassType);
        }

        [TestMethod]
        public void DefaultConstructor_WithoutType()
        {
            var testClassType = typeof(TestClass);
            var cd = testClassType.DefaultConstructor();
            Assert.IsNotNull(cd);
            Assert.IsNotNull(cd());
            Assert.IsInstanceOfType(cd(), testClassType);
        }

        [TestMethod]
        public void DefaultConstructor_WithoutType_ByDelegate_NoDefault_FromStruct()
        {
            var cd = typeof(TestStruct).Constructor<Func<object>>();
            Assert.IsNull(cd);
        }

        [TestMethod]
        public void DefaultConstructor_WithoutType_ByDelegateWithType_NoDefault_Class()
        {
            var cd = typeof(TestClassNoDefaultCtor).Constructor<Func<TestClassNoDefaultCtor>>();
            Assert.IsNull(cd);
        }

        [TestMethod]
        public void DefaultConstructor_WithoutType_ByDelegateWithType_NoDefault_FromStruct()
        {
            var cd = typeof(TestStruct).Constructor<Func<TestStruct>>();
            Assert.IsNull(cd);
        }

        [TestMethod]
        public void DefaultConstructor_WithoutType_NoDefault_Class()
        {
            var cd = typeof(TestClassNoDefaultCtor).DefaultConstructor();
            Assert.IsNull(cd);
        }

        [TestMethod]
        public void DefaultConstructor_WithType()
        {
            var cd = DelegateFactory.DefaultConstructor<TestClass>();
            Assert.IsNotNull(cd);
            Assert.IsNotNull(cd());
            Assert.IsInstanceOfType(cd(), typeof(TestClass));
        }

        [TestMethod]
        public void DefaultConstructor_WithType_NoDefault_Class()
        {
            var cd = DelegateFactory.DefaultConstructor<TestClassNoDefaultCtor>();
            Assert.IsNull(cd);
        }

        [TestMethod]
        public void DefaultConstructor_WithType_NoDefault_FromStruct()
        {
            var cd = DelegateFactory.DefaultConstructor<TestStruct>();
            Assert.IsNull(cd);
        }
    }
}