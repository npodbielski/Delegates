// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DelegateFactoryTests_StaticProperties_Getters.cs" company="Natan Podbielski">
//   Copyright (c) 2016 - 2016 Natan Podbielski. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using Delegates;
using DelegatesTest.TestObjects;
#if NETCORE||STANDARD
using Assert = DelegatesTest.CAssert;
using TestMethodAttribute = Xunit.FactAttribute;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
#endif

namespace DelegatesTest
{
#if !(NETCORE||STANDARD)
    [TestClass]
#endif
    public class DelegateFactoryTests_StaticProperties_Getters
    {
        private static readonly Type TestClassType = typeof(TestClass);
        private static readonly Type TestStructType = typeof(TestStruct);

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
            var spg = TestClassType.StaticPropertyGet<string>("StaticOnlySetProperty");
            Assert.IsNull(spg);
        }

        [TestMethod]
        public void PropertyGet_ByExtensionAndReturnType_Internal()
        {
            var spg = TestClassType.StaticPropertyGet<string>("StaticInternalProperty");
            Assert.IsNotNull(spg);
            Assert.AreEqual(TestClass.StaticInternalProperty, spg());
        }

        [TestMethod]
        public void PropertyGet_ByExtensionAndReturnType_Internal_FromStruct()
        {
            var spg = TestStructType.StaticPropertyGet<string>("StaticInternalProperty");
            Assert.IsNotNull(spg);
            Assert.AreEqual(TestStruct.StaticInternalProperty, spg());
        }

        [TestMethod]
        public void PropertyGet_ByExtensionAndReturnType_Private()
        {
            var spg = TestClassType.StaticPropertyGet<string>("StaticPrivateProperty");
            Assert.IsNotNull(spg);
            Assert.AreEqual(TestClass.GetStaticPrivateProperty(), spg());
        }

        [TestMethod]
        public void PropertyGet_ByExtensionAndReturnType_Private_FromStruct()
        {
            var spg = TestStructType.StaticPropertyGet<string>("StaticPrivateProperty");
            Assert.IsNotNull(spg);
            Assert.AreEqual(TestStruct.GetStaticPrivateProperty(), spg());
        }

        [TestMethod]
        public void PropertyGet_ByExtensionAndReturnType_Protected()
        {
            var spg = TestClassType.StaticPropertyGet<string>("StaticProtectedProperty");
            Assert.IsNotNull(spg);
            Assert.AreEqual(TestClass.GetStaticProtectedProperty(), spg());
        }

        [TestMethod]
        public void PropertyGet_ByExtensionAndReturnType_Public_FromStruct()
        {
            var spg = TestStructType.StaticPropertyGet<string>("StaticPublicProperty");
            Assert.IsNotNull(spg);
            Assert.AreEqual(TestStruct.StaticPublicProperty, spg());
        }

        [TestMethod]
        public void PropertyGet_ByExtensionAndReturnType_Public()
        {
            var spg = TestClassType.StaticPropertyGet<string>("StaticPublicProperty");
            Assert.IsNotNull(spg);
            Assert.AreEqual(TestClass.StaticPublicProperty, spg());
        }

        [TestMethod]
        public void PropertyGet_ByExtensionAndReturnType_Public_Struct()
        {
            var spg = TestClassType.StaticPropertyGet<int>("StaticPublicPropertyValue");
            Assert.IsNotNull(spg);
            Assert.AreEqual(TestClass.StaticPublicPropertyValue, spg());
        }

        [TestMethod]
        public void PropertyGet_ByExtensionAndReturnType_Public_Struct_FromStruct()
        {
            var spg = TestStructType.StaticPropertyGet<int>("StaticPublicPropertyValue");
            Assert.IsNotNull(spg);
            Assert.AreEqual(TestStruct.StaticPublicPropertyValue, spg());
        }

        [TestMethod]
        public void PropertyGet_ByObjects_Internal()
        {
            var spg = TestClassType.StaticPropertyGet("StaticInternalProperty");
            Assert.IsNotNull(spg);
            Assert.AreEqual(TestClass.StaticInternalProperty, spg());
        }

        [TestMethod]
        public void PropertyGet_ByObjects_Internal_FromStruct()
        {
            var spg = TestStructType.StaticPropertyGet("StaticInternalProperty");
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
            var spg = TestClassType.StaticPropertyGet("StaticOnlySetProperty");
            Assert.IsNull(spg);
        }

        [TestMethod]
        public void PropertyGet_ByObjects_Private()
        {
            var spg = TestClassType.StaticPropertyGet("StaticPrivateProperty");
            Assert.IsNotNull(spg);
            Assert.AreEqual(TestClass.GetStaticPrivateProperty(), spg());
        }

        [TestMethod]
        public void PropertyGet_ByObjects_Private_FromStruct()
        {
            var spg = TestStructType.StaticPropertyGet("StaticPrivateProperty");
            Assert.IsNotNull(spg);
            Assert.AreEqual(TestStruct.GetStaticPrivateProperty(), spg());
        }

        [TestMethod]
        public void PropertyGet_ByObjects_Protected()
        {
            var spg = TestClassType.StaticPropertyGet("StaticProtectedProperty");
            Assert.IsNotNull(spg);
            Assert.AreEqual(TestClass.GetStaticProtectedProperty(), spg());
        }

        [TestMethod]
        public void PropertyGet_ByObjects_Public()
        {
            var spg = TestClassType.StaticPropertyGet("StaticPublicProperty");
            Assert.IsNotNull(spg);
            Assert.AreEqual(TestClass.StaticPublicProperty, spg());
        }

        [TestMethod]
        public void PropertyGet_ByObjects_Public_FromStruct()
        {
            var spg = TestStructType.StaticPropertyGet("StaticPublicProperty");
            Assert.IsNotNull(spg);
            Assert.AreEqual(TestStruct.StaticPublicProperty, spg());
        }

        [TestMethod]
        public void PropertyGet_ByObjects_Public_Struct_FromStruct()
        {
            var spg = TestStructType.StaticPropertyGet("StaticPublicPropertyValue");
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
    }
}