// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DelegateFactoryTests_StaticProperties_Setters.cs" company="Natan Podbielski">
//   Copyright (c) 2016 - 2018 Natan Podbielski. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

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
#elif NETCOREAPP1_0
    DelegatesTestNETCORE10
#elif NETCOREAPP2_0
        DelegatesTestNETCORE20
#elif NETSTANDARD1_1
        DelegatesTestNETStandard11
#elif NETSTANDARD1_5
        DelegatesTestNETStandard15
#endif
{
    [TestClass]
    public class DelegateFactoryTests_StaticProperties_Setters
    {
        private const int NewIntValue = 0;
        private const string NewStringValue = "Test";

        //TODO: test with passed incorrect property type
        [TestMethod]
        public void PropertySet_ByExtensionAndReturnType_Internal()
        {
            var sps = typeof(TestClass).StaticPropertySet<string>("StaticInternalProperty");
            Assert.IsNotNull(sps);
            sps(NewStringValue);
            Assert.AreEqual(NewStringValue, TestClass.StaticInternalProperty);
        }

        [TestMethod]
        public void PropertySet_ByExtensionAndReturnType_Internal_FromStruct()
        {
            var sps = typeof(TestStruct).StaticPropertySet<string>("StaticInternalProperty");
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
            var sps = typeof(TestClass).StaticPropertySet<string>("StaticOnlyGetProperty");
            Assert.IsNull(sps);
        }

        [TestMethod]
        public void PropertySet_ByExtensionAndReturnType_Private()
        {
            var sps = typeof(TestClass).StaticPropertySet<string>("StaticPrivateProperty");
            Assert.IsNotNull(sps);
            sps(NewStringValue);
            Assert.AreEqual(NewStringValue, TestClass.GetStaticPrivateProperty());
        }

        [TestMethod]
        public void PropertySet_ByExtensionAndReturnType_Private_FromStruct()
        {
            var sps = typeof(TestStruct).StaticPropertySet<string>("StaticPrivateProperty");
            Assert.IsNotNull(sps);
            sps(NewStringValue);
            Assert.AreEqual(NewStringValue, TestStruct.GetStaticPrivateProperty());
        }

        [TestMethod]
        public void PropertySet_ByExtensionAndReturnType_Protected()
        {
            var sps = typeof(TestClass).StaticPropertySet<string>("StaticProtectedProperty");
            Assert.IsNotNull(sps);
            sps(NewStringValue);
            Assert.AreEqual(NewStringValue, TestClass.GetStaticProtectedProperty());
        }

        [TestMethod]
        public void PropertySet_ByExtensionAndReturnType_Public()
        {
            var sps = typeof(TestClass).StaticPropertySet<string>("StaticPublicProperty");
            Assert.IsNotNull(sps);
            sps(NewStringValue);
            Assert.AreEqual(NewStringValue, TestClass.StaticPublicProperty);
        }

        [TestMethod]
        public void PropertySet_ByExtensionAndReturnType_Public_FromStruct()
        {
            var sps = typeof(TestStruct).StaticPropertySet<string>("StaticPublicProperty");
            Assert.IsNotNull(sps);
            sps(NewStringValue);
            Assert.AreEqual(NewStringValue, TestStruct.StaticPublicProperty);
        }

        [TestMethod]
        public void PropertySet_ByExtensionAndReturnType_Public_Struct()
        {
            var sps = typeof(TestClass).StaticPropertySet<int>("StaticPublicPropertyValue");
            Assert.IsNotNull(sps);
            sps(NewIntValue);
            Assert.AreEqual(NewIntValue, TestClass.StaticPublicPropertyValue);
        }

        [TestMethod]
        public void PropertySet_ByExtensionAndReturnType_Public_Struct_FromStruct()
        {
            var sps = typeof(TestStruct).StaticPropertySet<int>("StaticPublicPropertyValue");
            Assert.IsNotNull(sps);
            sps(NewIntValue);
            Assert.AreEqual(NewIntValue, TestStruct.StaticPublicPropertyValue);
        }

        [TestMethod]
        public void PropertySet_ByObjects_Internal()
        {
            var sps = typeof(TestClass).StaticPropertySet("StaticInternalProperty");
            Assert.IsNotNull(sps);
            sps(NewStringValue);
            Assert.AreEqual(NewStringValue, TestClass.StaticInternalProperty);
        }

        [TestMethod]
        public void PropertySet_ByObjects_Internal_FromStruct()
        {
            var sps = typeof(TestStruct).StaticPropertySet("StaticInternalProperty");
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
            var sps = typeof(TestClass).StaticPropertySet("StaticOnlyGetProperty");
            Assert.IsNull(sps);
        }

        [TestMethod]
        public void PropertySet_ByObjects_Private()
        {
            var sps = typeof(TestClass).StaticPropertySet("StaticPrivateProperty");
            Assert.IsNotNull(sps);
            sps(NewStringValue);
            Assert.AreEqual(NewStringValue, TestClass.GetStaticPrivateProperty());
        }

        [TestMethod]
        public void PropertySet_ByObjects_Private_FromStruct()
        {
            var sps = typeof(TestStruct).StaticPropertySet("StaticPrivateProperty");
            Assert.IsNotNull(sps);
            sps(NewStringValue);
            Assert.AreEqual(NewStringValue, TestStruct.GetStaticPrivateProperty());
        }

        [TestMethod]
        public void PropertySet_ByObjects_Protected()
        {
            var sps = typeof(TestClass).StaticPropertySet("StaticProtectedProperty");
            Assert.IsNotNull(sps);
            sps(NewStringValue);
            Assert.AreEqual(NewStringValue, TestClass.GetStaticProtectedProperty());
        }

        [TestMethod]
        public void PropertySet_ByObjects_Public()
        {
            var sps = typeof(TestClass).StaticPropertySet("StaticPublicProperty");
            Assert.IsNotNull(sps);
            sps(NewStringValue);
            Assert.AreEqual(NewStringValue, TestClass.StaticPublicProperty);
        }

        [TestMethod]
        public void PropertySet_ByObjects_Public_FromStruct()
        {
            var sps = typeof(TestStruct).StaticPropertySet("StaticPublicProperty");
            Assert.IsNotNull(sps);
            sps(NewStringValue);
            Assert.AreEqual(NewStringValue, TestStruct.StaticPublicProperty);
        }

        [TestMethod]
        public void PropertySet_ByObjects_Public_Struct()
        {
            var sps = typeof(TestClass).StaticPropertySet("StaticPublicPropertyValue");
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