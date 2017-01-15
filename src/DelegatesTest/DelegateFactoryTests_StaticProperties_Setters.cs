// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DelegateFactoryTests_StaticProperties_Setters.cs" company="Natan Podbielski">
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
    public class DelegateFactoryTests_StaticProperties_Setters
    {
        private const int NewIntValue = 0;
        private const string NewStringValue = "Test";
        private static readonly Type TestClassType = typeof(TestClass);
        private static readonly Type TestStructType = typeof(TestStruct);

        [TestMethod]
        public void PropertySet_ByExtensionAndReturnType_Internal()
        {
            var sps = TestClassType.StaticPropertySet<string>("StaticInternalProperty");
            Assert.IsNotNull(sps);
            sps(NewStringValue);
            Assert.AreEqual(NewStringValue, TestClass.StaticInternalProperty);
        }

        [TestMethod]
        public void PropertySet_ByExtensionAndReturnType_Internal_FromStruct()
        {
            var sps = TestStructType.StaticPropertySet<string>("StaticInternalProperty");
            Assert.IsNotNull(sps);
            sps(NewStringValue);
            Assert.AreEqual(NewStringValue, TestStruct.StaticInternalProperty);
        }

        [TestMethod]
        public void PropertySet_ByExtensionAndReturnType_NonExisting()
        {
            var sps = DelegateFactory.StaticPropertySet<TestClass, string>("NonExisting");
            Assert.IsNull(sps);
        }

        [TestMethod]
        public void PropertySet_ByExtensionAndReturnType_OnlyRead()
        {
            var sps = TestClassType.StaticPropertySet<string>("StaticOnlyGetProperty");
            Assert.IsNull(sps);
        }

        [TestMethod]
        public void PropertySet_ByExtensionAndReturnType_Private()
        {
            var sps = TestClassType.StaticPropertySet<string>("StaticPrivateProperty");
            Assert.IsNotNull(sps);
            sps(NewStringValue);
            Assert.AreEqual(NewStringValue, TestClass.GetStaticPrivateProperty());
        }

        [TestMethod]
        public void PropertySet_ByExtensionAndReturnType_Private_FromStruct()
        {
            var sps = TestStructType.StaticPropertySet<string>("StaticPrivateProperty");
            Assert.IsNotNull(sps);
            sps(NewStringValue);
            Assert.AreEqual(NewStringValue, TestStruct.GetStaticPrivateProperty());
        }

        [TestMethod]
        public void PropertySet_ByExtensionAndReturnType_Protected()
        {
            var sps = TestClassType.StaticPropertySet<string>("StaticProtectedProperty");
            Assert.IsNotNull(sps);
            sps(NewStringValue);
            Assert.AreEqual(NewStringValue, TestClass.GetStaticProtectedProperty());
        }

        [TestMethod]
        public void PropertySet_ByExtensionAndReturnType_Public()
        {
            var sps = TestClassType.StaticPropertySet<string>("StaticPublicProperty");
            Assert.IsNotNull(sps);
            sps(NewStringValue);
            Assert.AreEqual(NewStringValue, TestClass.StaticPublicProperty);
        }

        [TestMethod]
        public void PropertySet_ByExtensionAndReturnType_Public_FromStruct()
        {
            var sps = TestStructType.StaticPropertySet<string>("StaticPublicProperty");
            Assert.IsNotNull(sps);
            sps(NewStringValue);
            Assert.AreEqual(NewStringValue, TestStruct.StaticPublicProperty);
        }

        [TestMethod]
        public void PropertySet_ByExtensionAndReturnType_Public_Struct()
        {
            var sps = TestClassType.StaticPropertySet<int>("StaticPublicPropertyValue");
            Assert.IsNotNull(sps);
            sps(NewIntValue);
            Assert.AreEqual(NewIntValue, TestClass.StaticPublicPropertyValue);
        }

        [TestMethod]
        public void PropertySet_ByExtensionAndReturnType_Public_Struct_FromStruct()
        {
            var sps = TestStructType.StaticPropertySet<int>("StaticPublicPropertyValue");
            Assert.IsNotNull(sps);
            sps(NewIntValue);
            Assert.AreEqual(NewIntValue, TestStruct.StaticPublicPropertyValue);
        }

        [TestMethod]
        public void PropertySet_ByObjects_Internal()
        {
            var sps = TestClassType.StaticPropertySet("StaticInternalProperty");
            Assert.IsNotNull(sps);
            sps(NewStringValue);
            Assert.AreEqual(NewStringValue, TestClass.StaticInternalProperty);
        }

        [TestMethod]
        public void PropertySet_ByObjects_Internal_FromStruct()
        {
            var sps = TestStructType.StaticPropertySet("StaticInternalProperty");
            Assert.IsNotNull(sps);
            sps(NewStringValue);
            Assert.AreEqual(NewStringValue, TestStruct.StaticInternalProperty);
        }

        [TestMethod]
        public void PropertySet_ByObjects_NonExisting()
        {
            var sps = DelegateFactory.StaticPropertySet<TestClass, string>("NonExisting");
            Assert.IsNull(sps);
        }

        [TestMethod]
        public void PropertySet_ByObjects_OnlyRead()
        {
            var sps = TestClassType.StaticPropertySet("StaticOnlyGetProperty");
            Assert.IsNull(sps);
        }

        [TestMethod]
        public void PropertySet_ByObjects_Private()
        {
            var sps = TestClassType.StaticPropertySet("StaticPrivateProperty");
            Assert.IsNotNull(sps);
            sps(NewStringValue);
            Assert.AreEqual(NewStringValue, TestClass.GetStaticPrivateProperty());
        }

        [TestMethod]
        public void PropertySet_ByObjects_Private_FromStruct()
        {
            var sps = TestStructType.StaticPropertySet("StaticPrivateProperty");
            Assert.IsNotNull(sps);
            sps(NewStringValue);
            Assert.AreEqual(NewStringValue, TestStruct.GetStaticPrivateProperty());
        }

        [TestMethod]
        public void PropertySet_ByObjects_Protected()
        {
            var sps = TestClassType.StaticPropertySet("StaticProtectedProperty");
            Assert.IsNotNull(sps);
            sps(NewStringValue);
            Assert.AreEqual(NewStringValue, TestClass.GetStaticProtectedProperty());
        }

        [TestMethod]
        public void PropertySet_ByObjects_Public()
        {
            var sps = TestClassType.StaticPropertySet("StaticPublicProperty");
            Assert.IsNotNull(sps);
            sps(NewStringValue);
            Assert.AreEqual(NewStringValue, TestClass.StaticPublicProperty);
        }

        [TestMethod]
        public void PropertySet_ByObjects_Public_FromStruct()
        {
            var sps = TestStructType.StaticPropertySet("StaticPublicProperty");
            Assert.IsNotNull(sps);
            sps(NewStringValue);
            Assert.AreEqual(NewStringValue, TestStruct.StaticPublicProperty);
        }

        [TestMethod]
        public void PropertySet_ByObjects_Public_Struct()
        {
            var sps = TestClassType.StaticPropertySet("StaticPublicPropertyValue");
            Assert.IsNotNull(sps);
            sps(NewIntValue);
            Assert.AreEqual(NewIntValue, TestClass.StaticPublicPropertyValue);
        }

        [TestMethod]
        public void PropertySet_ByTypes_Internal()
        {
            var sps = DelegateFactory.StaticPropertySet<TestClass, string>("StaticInternalProperty");
            Assert.IsNotNull(sps);
            sps(NewStringValue);
            Assert.AreEqual(NewStringValue, TestClass.StaticInternalProperty);
        }

        [TestMethod]
        public void PropertySet_ByTypes_Internal_FromStruct()
        {
            var sps = DelegateFactory.StaticPropertySet<TestStruct, string>("StaticInternalProperty");
            Assert.IsNotNull(sps);
            sps(NewStringValue);
            Assert.AreEqual(NewStringValue, TestStruct.StaticInternalProperty);
        }

        [TestMethod]
        public void PropertySet_ByTypes_NonExisting()
        {
            var sps = DelegateFactory.StaticPropertySet<TestClass, string>("NonExisting");
            Assert.IsNull(sps);
        }

        [TestMethod]
        public void PropertySet_ByTypes_OnlyRead()
        {
            var sps = DelegateFactory.StaticPropertySet<TestClass, string>("StaticOnlyGetProperty");
            Assert.IsNull(sps);
        }

        [TestMethod]
        public void PropertySet_ByTypes_Private()
        {
            var sps = DelegateFactory.StaticPropertySet<TestClass, string>("StaticPrivateProperty");
            Assert.IsNotNull(sps);
            sps(NewStringValue);
            Assert.AreEqual(NewStringValue, TestClass.GetStaticPrivateProperty());
        }

        [TestMethod]
        public void PropertySet_ByTypes_Private_FromStruct()
        {
            var sps = DelegateFactory.StaticPropertySet<TestStruct, string>("StaticPrivateProperty");
            Assert.IsNotNull(sps);
            sps(NewStringValue);
            Assert.AreEqual(NewStringValue, TestStruct.GetStaticPrivateProperty());
        }

        [TestMethod]
        public void PropertySet_ByTypes_Protected()
        {
            var sps = DelegateFactory.StaticPropertySet<TestClass, string>("StaticProtectedProperty");
            Assert.IsNotNull(sps);
            sps(NewStringValue);
            Assert.AreEqual(NewStringValue, TestClass.GetStaticProtectedProperty());
        }

        [TestMethod]
        public void PropertySet_ByTypes_Public()
        {
            var sps = DelegateFactory.StaticPropertySet<TestClass, string>("StaticPublicProperty");
            Assert.IsNotNull(sps);
            sps(NewStringValue);
            Assert.AreEqual(NewStringValue, TestClass.StaticPublicProperty);
        }

        [TestMethod]
        public void PropertySet_ByTypes_Public_FromStruct()
        {
            var sps = DelegateFactory.StaticPropertySet<TestStruct, string>("StaticPublicProperty");
            Assert.IsNotNull(sps);
            sps(NewStringValue);
            Assert.AreEqual(NewStringValue, TestStruct.StaticPublicProperty);
        }

        [TestMethod]
        public void PropertySet_ByTypes_Public_Struct()
        {
            var sps = DelegateFactory.StaticPropertySet<TestClass, int>("StaticPublicPropertyValue");
            Assert.IsNotNull(sps);
            sps(NewIntValue);
            Assert.AreEqual(NewIntValue, TestClass.StaticPublicPropertyValue);
        }

        [TestMethod]
        public void PropertySet_ByTypes_Public_StructFromStruct()
        {
            var sps = DelegateFactory.StaticPropertySet<TestStruct, int>("StaticPublicPropertyValue");
            Assert.IsNotNull(sps);
            sps(NewIntValue);
            Assert.AreEqual(NewIntValue, TestStruct.StaticPublicPropertyValue);
        }
    }
}