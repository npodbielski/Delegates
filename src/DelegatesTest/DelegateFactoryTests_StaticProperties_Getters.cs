// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DelegateFactoryTests_StaticProperties_Getters.cs" company="Natan Podbielski">
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
    public class DelegateFactoryTests_StaticProperties_Getters
    {
        //TODO: test what happen if passed incorrect property type (maybe allow this?)
        //TODO: test if interface or abstract class is passed as source
        //TODO: test static properties in generic classes
        [TestMethod]
        public void PropertyGet_ByExtensionAndReturnType_NonExisting()
        {
            var spg = DelegateFactory.StaticPropertyGet<TestClass, string>("NonExisting");
            Assert.IsNull(spg);
        }

        [TestMethod]
        public void PropertyGet_ByExtensionAndReturnType_OnlyWrite()
        {
            var spg = typeof(TestClass).StaticPropertyGet<string>("StaticOnlySetProperty");
            Assert.IsNull(spg);
        }

        [TestMethod]
        public void PropertyGet_ByExtensionAndReturnType_Internal()
        {
            var spg = typeof(TestClass).StaticPropertyGet<string>("StaticInternalProperty");
            Assert.IsNotNull(spg);
            Assert.AreEqual(TestClass.StaticInternalProperty, spg());
        }

        [TestMethod]
        public void PropertyGet_ByExtensionAndReturnType_Internal_FromStruct()
        {
            var spg = typeof(TestStruct).StaticPropertyGet<string>("StaticInternalProperty");
            Assert.IsNotNull(spg);
            Assert.AreEqual(TestStruct.StaticInternalProperty, spg());
        }

        [TestMethod]
        public void PropertyGet_ByExtensionAndReturnType_Private()
        {
            var spg = typeof(TestClass).StaticPropertyGet<string>("StaticPrivateProperty");
            Assert.IsNotNull(spg);
            Assert.AreEqual(TestClass.GetStaticPrivateProperty(), spg());
        }

        [TestMethod]
        public void PropertyGet_ByExtensionAndReturnType_Private_FromStruct()
        {
            var spg = typeof(TestStruct).StaticPropertyGet<string>("StaticPrivateProperty");
            Assert.IsNotNull(spg);
            Assert.AreEqual(TestStruct.GetStaticPrivateProperty(), spg());
        }

        [TestMethod]
        public void PropertyGet_ByExtensionAndReturnType_Protected()
        {
            var spg = typeof(TestClass).StaticPropertyGet<string>("StaticProtectedProperty");
            Assert.IsNotNull(spg);
            Assert.AreEqual(TestClass.GetStaticProtectedProperty(), spg());
        }

        [TestMethod]
        public void PropertyGet_ByExtensionAndReturnType_Public_FromStruct()
        {
            var spg = typeof(TestStruct).StaticPropertyGet<string>("StaticPublicProperty");
            Assert.IsNotNull(spg);
            Assert.AreEqual(TestStruct.StaticPublicProperty, spg());
        }

        [TestMethod]
        public void PropertyGet_ByExtensionAndReturnType_Public()
        {
            var spg = typeof(TestClass).StaticPropertyGet<string>("StaticPublicProperty");
            Assert.IsNotNull(spg);
            Assert.AreEqual(TestClass.StaticPublicProperty, spg());
        }

        [TestMethod]
        public void PropertyGet_ByExtensionAndReturnType_Public_Struct()
        {
            var spg = typeof(TestClass).StaticPropertyGet<int>("StaticPublicPropertyValue");
            Assert.IsNotNull(spg);
            Assert.AreEqual(TestClass.StaticPublicPropertyValue, spg());
        }

        [TestMethod]
        public void PropertyGet_ByExtensionAndReturnType_Public_Struct_FromStruct()
        {
            var spg = typeof(TestStruct).StaticPropertyGet<int>("StaticPublicPropertyValue");
            Assert.IsNotNull(spg);
            Assert.AreEqual(TestStruct.StaticPublicPropertyValue, spg());
        }

        [TestMethod]
        public void PropertyGet_ByObjects_Internal()
        {
            var spg = typeof(TestClass).StaticPropertyGet("StaticInternalProperty");
            Assert.IsNotNull(spg);
            Assert.AreEqual(TestClass.StaticInternalProperty, spg());
        }

        [TestMethod]
        public void PropertyGet_ByObjects_Internal_FromStruct()
        {
            var spg = typeof(TestStruct).StaticPropertyGet("StaticInternalProperty");
            Assert.IsNotNull(spg);
            Assert.AreEqual(TestStruct.StaticInternalProperty, spg());
        }

        [TestMethod]
        public void PropertyGet_ByObjects_NonExisting()
        {
            var spg = DelegateFactory.StaticPropertyGet<TestClass, string>("NonExisting");
            Assert.IsNull(spg);
        }

        [TestMethod]
        public void PropertyGet_ByObjects_OnlyWrite()
        {
            var spg = typeof(TestClass).StaticPropertyGet("StaticOnlySetProperty");
            Assert.IsNull(spg);
        }

        [TestMethod]
        public void PropertyGet_ByObjects_Private()
        {
            var spg = typeof(TestClass).StaticPropertyGet("StaticPrivateProperty");
            Assert.IsNotNull(spg);
            Assert.AreEqual(TestClass.GetStaticPrivateProperty(), spg());
        }

        [TestMethod]
        public void PropertyGet_ByObjects_Private_FromStruct()
        {
            var spg = typeof(TestStruct).StaticPropertyGet("StaticPrivateProperty");
            Assert.IsNotNull(spg);
            Assert.AreEqual(TestStruct.GetStaticPrivateProperty(), spg());
        }

        [TestMethod]
        public void PropertyGet_ByObjects_Protected()
        {
            var spg = typeof(TestClass).StaticPropertyGet("StaticProtectedProperty");
            Assert.IsNotNull(spg);
            Assert.AreEqual(TestClass.GetStaticProtectedProperty(), spg());
        }

        [TestMethod]
        public void PropertyGet_ByObjects_Public()
        {
            var spg = typeof(TestClass).StaticPropertyGet("StaticPublicProperty");
            Assert.IsNotNull(spg);
            Assert.AreEqual(TestClass.StaticPublicProperty, spg());
        }

        [TestMethod]
        public void PropertyGet_ByObjects_Public_FromStruct()
        {
            var spg = typeof(TestStruct).StaticPropertyGet("StaticPublicProperty");
            Assert.IsNotNull(spg);
            Assert.AreEqual(TestStruct.StaticPublicProperty, spg());
        }

        [TestMethod]
        public void PropertyGet_ByObjects_Public_Struct_FromStruct()
        {
            var spg = typeof(TestStruct).StaticPropertyGet("StaticPublicPropertyValue");
            Assert.IsNotNull(spg);
            Assert.AreEqual(TestStruct.StaticPublicPropertyValue, spg());
        }

        [TestMethod]
        public void PropertyGet_ByTypes_Internal()
        {
            var spg = DelegateFactory.StaticPropertyGet<TestClass, string>("StaticInternalProperty");
            Assert.IsNotNull(spg);
            Assert.AreEqual(TestClass.StaticInternalProperty, spg());
        }

        [TestMethod]
        public void PropertyGet_ByTypes_Internal_FromStruct()
        {
            var spg = DelegateFactory.StaticPropertyGet<TestStruct, string>("StaticInternalProperty");
            Assert.IsNotNull(spg);
            Assert.AreEqual(TestStruct.StaticInternalProperty, spg());
        }

        [TestMethod]
        public void PropertyGet_ByTypes_NonExisting()
        {
            var spg = DelegateFactory.StaticPropertyGet<TestClass, string>("NonExisting");
            Assert.IsNull(spg);
        }

        [TestMethod]
        public void PropertyGet_ByTypes_OnlyWrite()
        {
            var spg = DelegateFactory.StaticPropertyGet<TestClass, string>("StaticOnlySetProperty");
            Assert.IsNull(spg);
        }

        [TestMethod]
        public void PropertyGet_ByTypes_Private()
        {
            var spg = DelegateFactory.StaticPropertyGet<TestClass, string>("StaticPrivateProperty");
            Assert.IsNotNull(spg);
            Assert.AreEqual(TestClass.GetStaticPrivateProperty(), spg());
        }


        [TestMethod]
        public void PropertyGet_ByTypes_Private_FromStruct()
        {
            var spg = DelegateFactory.StaticPropertyGet<TestStruct, string>("StaticPrivateProperty");
            Assert.IsNotNull(spg);
            Assert.AreEqual(TestStruct.GetStaticPrivateProperty(), spg());
        }

        [TestMethod]
        public void PropertyGet_ByTypes_Protected()
        {
            var spg = DelegateFactory.StaticPropertyGet<TestClass, string>("StaticProtectedProperty");
            Assert.IsNotNull(spg);
            Assert.AreEqual(TestClass.GetStaticProtectedProperty(), spg());
        }

        [TestMethod]
        public void PropertyGet_ByTypes_Public()
        {
            var spg = DelegateFactory.StaticPropertyGet<TestClass, string>("StaticPublicProperty");
            Assert.IsNotNull(spg);
            Assert.AreEqual(TestClass.StaticPublicProperty, spg());
        }

        [TestMethod]
        public void PropertyGet_ByTypes_Public_FromStruct()
        {
            var spg = DelegateFactory.StaticPropertyGet<TestStruct, string>("StaticPublicProperty");
            Assert.IsNotNull(spg);
            Assert.AreEqual(TestStruct.StaticPublicProperty, spg());
        }

        [TestMethod]
        public void PropertyGet_ByTypes_Public_Struct()
        {
            var spg = DelegateFactory.StaticPropertyGet<TestClass, int>("StaticPublicPropertyValue");
            Assert.IsNotNull(spg);
            Assert.AreEqual(TestClass.StaticPublicPropertyValue, spg());
        }

        [TestMethod]
        public void PropertyGet_ByTypes_Public_Struct_FromStruct()
        {
            var spg = DelegateFactory.StaticPropertyGet<TestStruct, int>("StaticPublicPropertyValue");
            Assert.IsNotNull(spg);
            Assert.AreEqual(TestStruct.StaticPublicPropertyValue, spg());
        }

        [TestMethod]
        public void PropertyGet_StaticByNonStaticName_ByTypes()
        {
            var pg = DelegateFactory.StaticPropertyGet<TestClass, string>("PublicProperty");
            Assert.IsNull(pg);
        }

        [TestMethod]
        public void PropertyGet_StaticByNonStaticName_ByExtensionAndReturnType()
        {
            var pg = typeof(TestClass).StaticPropertyGet<string>("PublicProperty");
            Assert.IsNull(pg);
        }

        [TestMethod]
        public void PropertyGet_StaticByNonStaticName_ByObjects()
        {
            var pg = typeof(TestClass).StaticPropertyGet("PublicProperty");
            Assert.IsNull(pg);
        }
    }
}