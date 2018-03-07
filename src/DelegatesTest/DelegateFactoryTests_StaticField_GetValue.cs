// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DelegateFactoryTests_StaticField_GetValue.cs" company="Natan Podbielski">
//   Copyright (c) 2016 - 2016 Natan Podbielski. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using Delegates;
using DelegatesTest.TestObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace DelegatesTest
{
    [TestClass]
    public class DelegateFactoryTests_StaticField_GetValue
    {
        private static readonly Type TestClassType = typeof(TestClass);
        private static readonly Type TestStructType = typeof(TestStruct);

        [TestMethod]
        public void FieldGet_ByExtensionAndReturnType_NonExisting()
        {
            var sfg = DelegateFactory.StaticFieldGet<TestClass, string>("NonExisting");
            Assert.IsNull(sfg);
        }

        [TestMethod]
        public void FieldGet_ByExtensionAndReturnType_Internal()
        {
            var sfg = TestClassType.StaticFieldGet<string>("StaticInternalField");
            Assert.IsNotNull(sfg);
            Assert.AreEqual(TestClass.StaticInternalField, sfg());
        }

        [TestMethod]
        public void FieldGet_ByExtensionAndReturnType_Internal_FromStruct()
        {
            var sfg = TestStructType.StaticFieldGet<string>("StaticInternalField");
            Assert.IsNotNull(sfg);
            Assert.AreEqual(TestStruct.StaticInternalField, sfg());
        }

        [TestMethod]
        public void FieldGet_ByExtensionAndReturnType_Private()
        {
            var sfg = TestClassType.StaticFieldGet<string>("StaticPrivateField");
            Assert.IsNotNull(sfg);
            Assert.AreEqual(TestClass.GetStaticPrivateField(), sfg());
        }

        [TestMethod]
        public void FieldGet_ByExtensionAndReturnType_Private_FromStruct()
        {
            var sfg = TestStructType.StaticFieldGet<string>("_staticPrivateField");
            Assert.IsNotNull(sfg);
            Assert.AreEqual(TestStruct.GetStaticPrivateField(), sfg());
        }

        [TestMethod]
        public void FieldGet_ByExtensionAndReturnType_Protected()
        {
            var sfg = TestClassType.StaticFieldGet<string>("StaticProtectedField");
            Assert.IsNotNull(sfg);
            Assert.AreEqual(TestClass.GetStaticProtectedField(), sfg());
        }

        [TestMethod]
        public void FieldGet_ByExtensionAndReturnType_Public_FromStruct()
        {
            var sfg = TestStructType.StaticFieldGet<string>("StaticPublicField");
            Assert.IsNotNull(sfg);
            Assert.AreEqual(TestStruct.StaticPublicField, sfg());
        }

        [TestMethod]
        public void FieldGet_ByExtensionAndReturnType_Public()
        {
            var sfg = TestClassType.StaticFieldGet<string>("StaticPublicField");
            Assert.IsNotNull(sfg);
            Assert.AreEqual(TestClass.StaticPublicField, sfg());
        }

        [TestMethod]
        public void FieldGet_ByExtensionAndReturnType_Public_Struct()
        {
            var sfg = TestClassType.StaticFieldGet<int>("StaticPublicValueField");
            Assert.IsNotNull(sfg);
            Assert.AreEqual(TestClass.StaticPublicValueField, sfg());
        }

        [TestMethod]
        public void FieldGet_ByExtensionAndReturnType_Public_Struct_FromStruct()
        {
            var sfg = TestStructType.StaticFieldGet<int>("StaticPublicFieldInt");
            Assert.IsNotNull(sfg);
            Assert.AreEqual(TestStruct.StaticPublicFieldInt, sfg());
        }

        [TestMethod]
        public void FieldGet_ByObjects_Internal()
        {
            var sfg = TestClassType.StaticFieldGet("StaticInternalField");
            Assert.IsNotNull(sfg);
            Assert.AreEqual(TestClass.StaticInternalField, sfg());
        }

        [TestMethod]
        public void FieldGet_ByObjects_Internal_FromStruct()
        {
            var sfg = TestStructType.StaticFieldGet("StaticInternalField");
            Assert.IsNotNull(sfg);
            Assert.AreEqual(TestStruct.StaticInternalField, sfg());
        }

        [TestMethod]
        public void FieldGet_ByObjects_NonExisting()
        {
            var sfg = DelegateFactory.StaticFieldGet<TestClass, string>("NonExisting");
            Assert.IsNull(sfg);
        }

        [TestMethod]
        public void FieldGet_ByObjects_OnlyWrite()
        {
            var sfg = TestClassType.StaticFieldGet("StaticOnlySetField");
            Assert.IsNull(sfg);
        }

        [TestMethod]
        public void FieldGet_ByObjects_Private()
        {
            var sfg = TestClassType.StaticFieldGet("StaticPrivateField");
            Assert.IsNotNull(sfg);
            Assert.AreEqual(TestClass.GetStaticPrivateField(), sfg());
        }

        [TestMethod]
        public void FieldGet_ByObjects_Private_FromStruct()
        {
            var sfg = TestStructType.StaticFieldGet("_staticPrivateField");
            Assert.IsNotNull(sfg);
            Assert.AreEqual(TestStruct.GetStaticPrivateField(), sfg());
        }

        [TestMethod]
        public void FieldGet_ByObjects_Protected()
        {
            var sfg = TestClassType.StaticFieldGet("StaticProtectedField");
            Assert.IsNotNull(sfg);
            Assert.AreEqual(TestClass.GetStaticProtectedField(), sfg());
        }

        [TestMethod]
        public void FieldGet_ByObjects_Public()
        {
            var sfg = TestClassType.StaticFieldGet("StaticPublicField");
            Assert.IsNotNull(sfg);
            Assert.AreEqual(TestClass.StaticPublicField, sfg());
        }

        [TestMethod]
        public void FieldGet_ByObjects_Public_FromStruct()
        {
            var sfg = TestStructType.StaticFieldGet("StaticPublicField");
            Assert.IsNotNull(sfg);
            Assert.AreEqual(TestStruct.StaticPublicField, sfg());
        }

        [TestMethod]
        public void FieldGet_ByObjects_Public_Struct_FromStruct()
        {
            var sfg = TestStructType.StaticFieldGet("StaticPublicFieldInt");
            Assert.IsNotNull(sfg);
            Assert.AreEqual(TestStruct.StaticPublicFieldInt, sfg());
        }

        [TestMethod]
        public void FieldGet_ByTypes_Internal()
        {
            var sfg = DelegateFactory.StaticFieldGet<TestClass, string>("StaticInternalField");
            Assert.IsNotNull(sfg);
            Assert.AreEqual(TestClass.StaticInternalField, sfg());
        }

        [TestMethod]
        public void FieldGet_ByTypes_Internal_FromStruct()
        {
            var sfg = DelegateFactory.StaticFieldGet<TestStruct, string>("StaticInternalField");
            Assert.IsNotNull(sfg);
            Assert.AreEqual(TestStruct.StaticInternalField, sfg());
        }

        [TestMethod]
        public void FieldGet_ByTypes_NonExisting()
        {
            var sfg = DelegateFactory.StaticFieldGet<TestClass, string>("NonExisting");
            Assert.IsNull(sfg);
        }

        [TestMethod]
        public void FieldGet_ByTypes_OnlyWrite()
        {
            var sfg = DelegateFactory.StaticFieldGet<TestClass, string>("StaticOnlySetField");
            Assert.IsNull(sfg);
        }

        [TestMethod]
        public void FieldGet_ByTypes_Private()
        {
            var sfg = DelegateFactory.StaticFieldGet<TestClass, string>("StaticPrivateField");
            Assert.IsNotNull(sfg);
            Assert.AreEqual(TestClass.GetStaticPrivateField(), sfg());
        }


        [TestMethod]
        public void FieldGet_ByTypes_Private_FromStruct()
        {
            var sfg = DelegateFactory.StaticFieldGet<TestStruct, string>("StaticPublicField");
            Assert.IsNotNull(sfg);
            Assert.AreEqual(TestStruct.StaticPublicField, sfg());
        }

        [TestMethod]
        public void FieldGet_ByTypes_Protected()
        {
            var sfg = DelegateFactory.StaticFieldGet<TestClass, string>("StaticProtectedField");
            Assert.IsNotNull(sfg);
            Assert.AreEqual(TestClass.GetStaticProtectedField(), sfg());
        }

        [TestMethod]
        public void FieldGet_ByTypes_Public()
        {
            var sfg = DelegateFactory.StaticFieldGet<TestClass, string>("StaticPublicField");
            Assert.IsNotNull(sfg);
            Assert.AreEqual(TestClass.StaticPublicField, sfg());
        }

        [TestMethod]
        public void FieldGet_ByTypes_Public_FromStruct()
        {
            var sfg = DelegateFactory.StaticFieldGet<TestStruct, string>("StaticPublicField");
            Assert.IsNotNull(sfg);
            Assert.AreEqual(TestStruct.StaticPublicField, sfg());
        }

        [TestMethod]
        public void FieldGet_ByTypes_Public_Struct()
        {
            var sfg = DelegateFactory.StaticFieldGet<TestClass, int>("StaticPublicValueField");
            Assert.IsNotNull(sfg);
            Assert.AreEqual(TestClass.StaticPublicValueField, sfg());
        }

        [TestMethod]
        public void FieldGet_ByTypes_Public_Struct_FromStruct()
        {
            var sfg = DelegateFactory.StaticFieldGet<TestStruct, int>("StaticPublicFieldInt");
            Assert.IsNotNull(sfg);
            Assert.AreEqual(TestStruct.StaticPublicFieldInt, sfg());
        }
    }
}