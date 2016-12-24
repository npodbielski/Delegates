// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DelegateFactoryTests_StaticField_SetValue.cs" company="Natan Podbielski">
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
#if !NET35
#if !NETCORE
    [TestClass]
#endif
    public class DelegateFactoryTests_StaticField_SetValue
    {
        private const int NewIntValue = 0;
        private const string NewStringValue = "Test";
        private static readonly Type TestClassType = typeof(TestClass);
        private static readonly Type TestStructType = typeof(TestStruct);

        [TestMethod]
        public void FieldSet_ByExtensionAndReturnType_NonExisting()
        {
            var sfs = DelegateFactory.StaticFieldSet<TestClass, string>("NonExisting");
            Assert.IsNull(sfs);
        }

        [TestMethod]
        public void FieldSet_ByExtensionAndReturnType_ReadOnly()
        {
            var sfs = DelegateFactory.StaticFieldSet<TestClass, string>("StaticPublicReadOnlyField");
            Assert.IsNull(sfs);
        }

        [TestMethod]
        public void FieldSet_ByExtensionAndReturnType_Internal()
        {
            var sfs = TestClassType.StaticFieldSet<string>("StaticInternalField");
            Assert.IsNotNull(sfs);
            sfs(NewStringValue);
            Assert.AreEqual(NewStringValue, TestClass.StaticInternalField);
        }

        [TestMethod]
        public void FieldSet_ByExtensionAndReturnType_Internal_FromStruct()
        {
            var sfs = TestStructType.StaticFieldSet<string>("StaticInternalField");
            Assert.IsNotNull(sfs);
            sfs(NewStringValue);
            Assert.AreEqual(NewStringValue, TestClass.StaticInternalField);
        }

        [TestMethod]
        public void FieldSet_ByExtensionAndReturnType_Private()
        {
            var sfs = TestClassType.StaticFieldSet<string>("StaticPrivateField");
            Assert.IsNotNull(sfs);
            sfs(NewStringValue);
            Assert.AreEqual(NewStringValue, TestClass.GetStaticPrivateField());
        }

        [TestMethod]
        public void FieldSet_ByExtensionAndReturnType_Private_FromStruct()
        {
            var sfs = TestStructType.StaticFieldSet<string>("_staticPrivateField");
            Assert.IsNotNull(sfs);
            sfs(NewStringValue);
            Assert.AreEqual(NewStringValue, TestStruct.GetStaticPrivateField());
        }

        [TestMethod]
        public void FieldSet_ByExtensionAndReturnType_Protected()
        {
            var sfs = TestClassType.StaticFieldSet<string>("StaticProtectedField");
            Assert.IsNotNull(sfs);
            sfs(NewStringValue);
            Assert.AreEqual(NewStringValue, TestClass.GetStaticProtectedField());
        }

        [TestMethod]
        public void FieldSet_ByExtensionAndReturnType_Public_FromStruct()
        {
            var sfs = TestStructType.StaticFieldSet<string>("StaticPublicField");
            Assert.IsNotNull(sfs);
            sfs(NewStringValue);
            Assert.AreEqual(NewStringValue, TestStruct.StaticPublicField);
        }

        [TestMethod]
        public void FieldSet_ByExtensionAndReturnType_Public()
        {
            var sfs = TestClassType.StaticFieldSet<string>("StaticPublicField");
            Assert.IsNotNull(sfs);
            sfs(NewStringValue);
            Assert.AreEqual(NewStringValue, TestClass.StaticPublicField);
        }

        [TestMethod]
        public void FieldSet_ByExtensionAndReturnType_Public_Struct()
        {
            var sfs = TestClassType.StaticFieldSet<int>("StaticPublicValueField");
            Assert.IsNotNull(sfs);
            sfs(NewIntValue);
            Assert.AreEqual(NewIntValue, TestClass.StaticPublicValueField);
        }

        [TestMethod]
        public void FieldSet_ByExtensionAndReturnType_Public_Struct_FromStruct()
        {
            var sfs = TestStructType.StaticFieldSet<int>("StaticPublicFieldInt");
            Assert.IsNotNull(sfs);
            sfs(NewIntValue);
            Assert.AreEqual(NewIntValue, TestStruct.StaticPublicFieldInt);
        }

        [TestMethod]
        public void FieldSet_ByObjects_Internal()
        {
            var sfs = TestClassType.StaticFieldSet("StaticInternalField");
            Assert.IsNotNull(sfs);
            sfs(NewStringValue);
            Assert.AreEqual(NewStringValue, TestClass.StaticInternalField);
        }

        [TestMethod]
        public void FieldSet_ByObjects_Internal_FromStruct()
        {
            var sfs = TestStructType.StaticFieldSet("StaticInternalField");
            Assert.IsNotNull(sfs);
            sfs(NewStringValue);
            Assert.AreEqual(NewStringValue, TestStruct.StaticInternalField);
        }

        [TestMethod]
        public void FieldSet_ByObjects_NonExisting()
        {
            var sfs = DelegateFactory.StaticFieldSet<TestClass, string>("NonExisting");
            Assert.IsNull(sfs);
        }

        [TestMethod]
        public void FieldSet_ByObjects_Private()
        {
            var sfs = TestClassType.StaticFieldSet("StaticPrivateField");
            Assert.IsNotNull(sfs);
            sfs(NewStringValue);
            Assert.AreEqual(NewStringValue, TestClass.GetStaticPrivateField());
        }

        [TestMethod]
        public void FieldSet_ByObjects_Private_FromStruct()
        {
            var sfs = TestStructType.StaticFieldSet("_staticPrivateField");
            Assert.IsNotNull(sfs);
            sfs(NewStringValue);
            Assert.AreEqual(NewStringValue, TestStruct.GetStaticPrivateField());
        }

        [TestMethod]
        public void FieldSet_ByObjects_Protected()
        {
            var sfs = TestClassType.StaticFieldSet("StaticProtectedField");
            Assert.IsNotNull(sfs);
            sfs(NewStringValue);
            Assert.AreEqual(NewStringValue, TestClass.GetStaticProtectedField());
        }

        [TestMethod]
        public void FieldSet_ByObjects_Public()
        {
            var sfs = TestClassType.StaticFieldSet("StaticPublicField");
            Assert.IsNotNull(sfs);
            sfs(NewStringValue);
            Assert.AreEqual(NewStringValue, TestClass.StaticPublicField);
        }

        [TestMethod]
        public void FieldSet_ByObjects_Public_FromStruct()
        {
            var sfs = TestStructType.StaticFieldSet("StaticPublicField");
            Assert.IsNotNull(sfs);
            sfs(NewStringValue);
            Assert.AreEqual(NewStringValue, TestStruct.StaticPublicField);
        }

        [TestMethod]
        public void FieldSet_ByObjects_Public_Struct_FromStruct()
        {
            var sfs = TestStructType.StaticFieldSet("StaticPublicFieldInt");
            Assert.IsNotNull(sfs);
            sfs(NewIntValue);
            Assert.AreEqual(NewIntValue, TestStruct.StaticPublicFieldInt);
        }

        [TestMethod]
        public void FieldSet_ByTypes_Internal()
        {
            var sfs = DelegateFactory.StaticFieldSet<TestClass, string>("StaticInternalField");
            Assert.IsNotNull(sfs);
            sfs(NewStringValue);
            Assert.AreEqual(NewStringValue, TestClass.StaticInternalField);
        }

        [TestMethod]
        public void FieldSet_ByTypes_Internal_FromStruct()
        {
            var sfs = DelegateFactory.StaticFieldSet<TestStruct, string>("StaticInternalField");
            Assert.IsNotNull(sfs);
            sfs(NewStringValue);
            Assert.AreEqual(NewStringValue, TestStruct.StaticInternalField);
        }

        [TestMethod]
        public void FieldSet_ByTypes_NonExisting()
        {
            var sfs = DelegateFactory.StaticFieldSet<TestClass, string>("NonExisting");
            Assert.IsNull(sfs);
        }

        [TestMethod]
        public void FieldSet_ByTypes_Private()
        {
            var sfs = DelegateFactory.StaticFieldSet<TestClass, string>("StaticPrivateField");
            Assert.IsNotNull(sfs);
            sfs(NewStringValue);
            Assert.AreEqual(NewStringValue, TestClass.GetStaticPrivateField());
        }


        [TestMethod]
        public void FieldSet_ByTypes_Private_FromStruct()
        {
            var sfs = DelegateFactory.StaticFieldSet<TestStruct, string>("StaticPublicField");
            Assert.IsNotNull(sfs);
            sfs(NewStringValue);
            Assert.AreEqual(NewStringValue, TestStruct.StaticPublicField);
        }

        [TestMethod]
        public void FieldSet_ByTypes_Protected()
        {
            var sfs = DelegateFactory.StaticFieldSet<TestClass, string>("StaticProtectedField");
            Assert.IsNotNull(sfs);
            sfs(NewStringValue);
            Assert.AreEqual(NewStringValue, TestClass.GetStaticProtectedField());
        }

        [TestMethod]
        public void FieldSet_ByTypes_Public()
        {
            var sfs = DelegateFactory.StaticFieldSet<TestClass, string>("StaticPublicField");
            Assert.IsNotNull(sfs);
            sfs(NewStringValue);
            Assert.AreEqual(NewStringValue, TestClass.StaticPublicField);
        }

        [TestMethod]
        public void FieldSet_ByTypes_Public_FromStruct()
        {
            var sfs = DelegateFactory.StaticFieldSet<TestStruct, string>("StaticPublicField");
            Assert.IsNotNull(sfs);
            sfs(NewStringValue);
            Assert.AreEqual(NewStringValue, TestStruct.StaticPublicField);
        }

        [TestMethod]
        public void FieldSet_ByTypes_Public_Struct()
        {
            var sfs = DelegateFactory.StaticFieldSet<TestClass, int>("StaticPublicValueField");
            Assert.IsNotNull(sfs);
            sfs(NewIntValue);
            Assert.AreEqual(NewIntValue, TestClass.StaticPublicValueField);
        }

        [TestMethod]
        public void FieldSet_ByTypes_Public_Struct_FromStruct()
        {
            var sfs = DelegateFactory.StaticFieldSet<TestStruct, int>("StaticPublicFieldInt");
            Assert.IsNotNull(sfs);
            sfs(NewIntValue);
            Assert.AreEqual(NewIntValue, TestStruct.StaticPublicFieldInt);
        }
    }
#endif
}