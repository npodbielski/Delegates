// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DelegateFactoryTests_Fields_GetValue.cs" company="Natan Podbielski">
//   Copyright (c) 2016 - 2018 Natan Podbielski. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using Delegates;
using DelegatesTest.TestObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DelegatesTest
{
    [TestClass]
    public class DelegateFactoryTests_Fields_GetValue
    {
        private readonly TestClass _testClassInstance = new TestClass();
        private readonly Type _testClassType = typeof(TestClass);
        private readonly Type _testStructType = typeof(TestStruct);
        private TestStruct _testStructInstance = new TestStruct(0);

        [TestMethod]
        public void FieldGet_ByExtensionAndReturnType_Internal()
        {
            var fg = _testClassType.FieldGet<string>("InternalField");
            Assert.IsNotNull(fg);
            Assert.AreEqual(_testClassInstance.InternalField, fg(_testClassInstance));
        }

        [TestMethod]
        public void FieldGet_ByExtensionAndReturnType_Internal_FromStruct()
        {
            var fg = _testStructType.FieldGet<string>("InternalField");
            Assert.IsNotNull(fg);
            Assert.AreEqual(_testStructInstance.InternalField, fg(_testStructInstance));
        }

        [TestMethod]
        public void FieldGet_ByExtensionAndReturnType_NonExisting()
        {
            var fg = DelegateFactory.FieldGet<TestClass, string>("NonExisting");
            Assert.IsNull(fg);
        }

        [TestMethod]
        public void FieldGet_ByExtensionAndReturnType_Private()
        {
            var fg = _testClassType.FieldGet<string>("_privateField");
            Assert.IsNotNull(fg);
            Assert.AreEqual(_testClassInstance.GetPrivateField(), fg(_testClassInstance));
        }

        [TestMethod]
        public void FieldGet_ByExtensionAndReturnType_Private_FromStruct()
        {
            var fg = _testStructType.FieldGet<string>("_privateField");
            Assert.IsNotNull(fg);
            Assert.AreEqual(_testStructInstance.GetPrivateField(), fg(_testStructInstance));
        }

        [TestMethod]
        public void FieldGet_ByExtensionAndReturnType_Protected()
        {
            var fg = _testClassType.FieldGet<string>("ProtectedField");
            Assert.IsNotNull(fg);
            Assert.AreEqual(_testClassInstance.GetProtectedField(), fg(_testClassInstance));
        }

        [TestMethod]
        public void FieldGet_ByExtensionAndReturnType_Public()
        {
            var fg = _testClassType.FieldGet<string>("PublicField");
            Assert.IsNotNull(fg);
            Assert.AreEqual(_testClassInstance.PublicField, fg(_testClassInstance));
        }

        [TestMethod]
        public void FieldGet_ByExtensionAndReturnType_Public_FromStruct()
        {
            var fg = _testStructType.FieldGet<string>("PublicField");
            Assert.IsNotNull(fg);
            Assert.AreEqual(_testStructInstance.PublicField, fg(_testStructInstance));
        }

        [TestMethod]
        public void FieldGet_ByExtensionAndReturnType_Public_Struct()
        {
            var fg = _testClassType.FieldGet<int>("PublicFieldInt");
            Assert.IsNotNull(fg);
            Assert.AreEqual(_testClassInstance.PublicFieldInt, fg(_testClassInstance));
        }

        [TestMethod]
        public void FieldGet_ByExtensionAndReturnType_Public_Struct_FromStruct()
        {
            var fg = _testStructType.FieldGet<int>("PublicFieldInt");
            Assert.IsNotNull(fg);
            Assert.AreEqual(_testStructInstance.PublicFieldInt, fg(_testStructInstance));
        }

        [TestMethod]
        public void FieldGet_ByObjects_Internal()
        {
            var fg = _testClassType.FieldGet("InternalField");
            Assert.IsNotNull(fg);
            Assert.AreEqual(_testClassInstance.InternalField, fg(_testClassInstance));
        }

        [TestMethod]
        public void FieldGet_ByObjects_Internal_FromStruct()
        {
            var fg = _testStructType.FieldGet("InternalField");
            Assert.IsNotNull(fg);
            Assert.AreEqual(_testStructInstance.InternalField, fg(_testStructInstance));
        }

        [TestMethod]
        public void FieldGet_ByObjects_NonExisting()
        {
            var fg = DelegateFactory.FieldGet<TestClass, string>("NonExisting");
            Assert.IsNull(fg);
        }

        [TestMethod]
        public void FieldGet_ByObjects_Private()
        {
            var fg = _testClassType.FieldGet("_privateField");
            Assert.IsNotNull(fg);
            Assert.AreEqual(_testClassInstance.GetPrivateField(), fg(_testClassInstance));
        }

        [TestMethod]
        public void FieldGet_ByObjects_Private_FromStruct()
        {
            var fg = _testStructType.FieldGet("_privateField");
            Assert.IsNotNull(fg);
            Assert.AreEqual(_testStructInstance.GetPrivateField(), fg(_testStructInstance));
        }

        [TestMethod]
        public void FieldGet_ByObjects_Protected()
        {
            var fg = _testClassType.FieldGet("ProtectedField");
            Assert.IsNotNull(fg);
            Assert.AreEqual(_testClassInstance.GetProtectedField(), fg(_testClassInstance));
        }

        [TestMethod]
        public void FieldGet_ByObjects_Public()
        {
            var fg = _testClassType.FieldGet("PublicField");
            Assert.IsNotNull(fg);
            Assert.AreEqual(_testClassInstance.PublicField, fg(_testClassInstance));
        }

        [TestMethod]
        public void FieldGet_ByObjects_Public_FromStruct()
        {
            var fg = _testStructType.FieldGet("PublicField");
            Assert.IsNotNull(fg);
            Assert.AreEqual(_testStructInstance.PublicField, fg(_testStructInstance));
        }

        [TestMethod]
        public void FieldGet_ByObjects_Public_Struct()
        {
            var fg = _testClassType.FieldGet("PublicFieldInt");
            Assert.IsNotNull(fg);
            Assert.AreEqual(_testClassInstance.PublicFieldInt, fg(_testClassInstance));
        }

        [TestMethod]
        public void FieldGet_ByObjects_Public_Struct_FromStruct()
        {
            var fg = _testStructType.FieldGet("PublicFieldInt");
            Assert.IsNotNull(fg);
            Assert.AreEqual(_testStructInstance.PublicFieldInt, fg(_testStructInstance));
        }

        [TestMethod]
        public void FieldGet_ByTypes_Internal()
        {
            var fg = DelegateFactory.FieldGet<TestClass, string>("InternalField");
            Assert.IsNotNull(fg);
            Assert.AreEqual(_testClassInstance.InternalField, fg(_testClassInstance));
        }

        [TestMethod]
        public void FieldGet_ByTypes_Internal_FromStruct()
        {
            var fg = DelegateFactory.FieldGet<TestStruct, string>("InternalField");
            Assert.IsNotNull(fg);
            Assert.AreEqual(_testStructInstance.InternalField, fg(_testStructInstance));
        }

#if !NET35
        [TestMethod]
        public void FieldGet_ByTypes_Internal_FromStruct_ByRef()
        {
            var fg = DelegateFactory.FieldGetStruct<TestStruct, string>("InternalField");
            Assert.IsNotNull(fg);
            Assert.AreEqual(_testStructInstance.InternalField, fg(ref _testStructInstance));
        }
#endif

        [TestMethod]
        public void FieldGet_ByTypes_NonExisting()
        {
            var fg = DelegateFactory.FieldGet<TestClass, string>("NonExisting");
            Assert.IsNull(fg);
        }

        [TestMethod]
        public void FieldGet_ByTypes_Private()
        {
            var fg = DelegateFactory.FieldGet<TestClass, string>("_privateField");
            Assert.IsNotNull(fg);
            Assert.AreEqual(_testClassInstance.GetPrivateField(), fg(_testClassInstance));
        }

        [TestMethod]
        public void FieldGet_ByTypes_Private_FromStruct()
        {
            var fg = DelegateFactory.FieldGet<TestStruct, string>("_privateField");
            Assert.IsNotNull(fg);
            Assert.AreEqual(_testStructInstance.GetPrivateField(), fg(_testStructInstance));
        }

#if !NET35
        [TestMethod]
        public void FieldGet_ByTypes_Private_FromStruct_ByRef()
        {
            var fg = DelegateFactory.FieldGetStruct<TestStruct, string>("_privateField");
            Assert.IsNotNull(fg);
            Assert.AreEqual(_testStructInstance.GetPrivateField(), fg(ref _testStructInstance));
        }
#endif

        [TestMethod]
        public void FieldGet_ByTypes_Protected()
        {
            var fg = DelegateFactory.FieldGet<TestClass, string>("ProtectedField");
            Assert.IsNotNull(fg);
            Assert.AreEqual(_testClassInstance.GetProtectedField(), fg(_testClassInstance));
        }

        [TestMethod]
        public void FieldGet_ByTypes_Public()
        {
            var fg = DelegateFactory.FieldGet<TestClass, string>("PublicField");
            Assert.IsNotNull(fg);
            Assert.AreEqual(_testClassInstance.PublicField, fg(_testClassInstance));
        }

        [TestMethod]
        public void FieldGet_ByTypes_Public_FromStruct()
        {
            var fg = DelegateFactory.FieldGet<TestStruct, string>("PublicField");
            Assert.IsNotNull(fg);
            Assert.AreEqual(_testStructInstance.PublicField, fg(_testStructInstance));
        }

#if !NET35
        [TestMethod]
        public void FieldGet_ByTypes_Public_FromStruct_ByRef()
        {
            var fg = DelegateFactory.FieldGetStruct<TestStruct, string>("PublicField");
            Assert.IsNotNull(fg);
            Assert.AreEqual(_testStructInstance.PublicField, fg(ref _testStructInstance));
        }
#endif

        [TestMethod]
        public void FieldGet_ByTypes_Public_Struct()
        {
            var fg = DelegateFactory.FieldGet<TestClass, int>("PublicFieldInt");
            Assert.IsNotNull(fg);
            Assert.AreEqual(_testClassInstance.PublicFieldInt, fg(_testClassInstance));
        }

        [TestMethod]
        public void FieldGet_ByTypes_Public_Struct_FromStruct()
        {
            var fg = DelegateFactory.FieldGet<TestStruct, int>("PublicFieldInt");
            Assert.IsNotNull(fg);
            Assert.AreEqual(_testStructInstance.PublicFieldInt, fg(_testStructInstance));
        }

#if !NET35
        [TestMethod]
        public void FieldGet_ByTypes_Public_Struct_FromStruct_ByRef()
        {
            var fg = DelegateFactory.FieldGetStruct<TestStruct, int>("PublicFieldInt");
            Assert.IsNotNull(fg);
            Assert.AreEqual(_testStructInstance.PublicFieldInt, fg(ref _testStructInstance));
        }
#endif
    }
}