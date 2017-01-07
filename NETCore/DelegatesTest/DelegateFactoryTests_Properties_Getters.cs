// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DelegateFactoryTests_Properties_Getters.cs" company="Natan Podbielski">
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
    public class DelegateFactoryTests_Properties_Getters
    {
        //TODO: test interfaces properties getters
        private readonly TestClass _testClassInstance = new TestClass();
        private readonly Type _testClassType = typeof(TestClass);
        private readonly Type _testStrucType = typeof(TestStruct);
        private TestStruct _testStructInstance = new TestStruct(0);

        [TestMethod]
        public void PropertyGet_ByExtensionAndReturnType_NonExisting()
        {
            var pg = DelegateFactory.PropertyGet<TestClass, string>("NonExisting");
            Assert.IsNull(pg);
        }

        [TestMethod]
        public void PropertyGet_ByExtensionAndReturnType_OnlyWrite()
        {
            var pg = _testClassType.PropertyGet<string>("OnlySetProperty");
            Assert.IsNull(pg);
        }

        [TestMethod]
        public void PropertyGet_ByExtensionAndReturnType_Internal()
        {
            var pg = _testClassType.PropertyGet<string>("InternalProperty");
            Assert.IsNotNull(pg);
            Assert.AreEqual(_testClassInstance.InternalProperty, pg(_testClassInstance));
        }

        [TestMethod]
        public void PropertyGet_ByExtensionAndReturnType_Internal_FromStruct()
        {
            var pg = _testStrucType.PropertyGet<string>("InternalProperty");
            Assert.IsNotNull(pg);
            Assert.AreEqual(_testStructInstance.InternalProperty, pg(_testStructInstance));
        }

        [TestMethod]
        public void PropertyGet_ByExtensionAndReturnType_Private()
        {
            var pg = _testClassType.PropertyGet<string>("PrivateProperty");
            Assert.IsNotNull(pg);
            Assert.AreEqual(_testClassInstance.GetPrivateProperty(), pg(_testClassInstance));
        }

        [TestMethod]
        public void PropertyGet_ByExtensionAndReturnType_Private_FromStruct()
        {
            var pg = _testStrucType.PropertyGet<string>("PrivateProperty");
            Assert.IsNotNull(pg);
            Assert.AreEqual(_testStructInstance.GetPrivateProperty(), pg(_testStructInstance));
        }

        [TestMethod]
        public void PropertyGet_ByExtensionAndReturnType_Protected()
        {
            var pg = _testClassType.PropertyGet<string>("ProtectedProperty");
            Assert.IsNotNull(pg);
            Assert.AreEqual(_testClassInstance.GetProtectedProperty(), pg(_testClassInstance));
        }

        [TestMethod]
        public void PropertyGet_ByExtensionAndReturnType_Public()
        {
            var pg = _testClassType.PropertyGet<string>("PublicProperty");
            Assert.IsNotNull(pg);
            Assert.AreEqual(_testClassInstance.PublicProperty, pg(_testClassInstance));
        }

        [TestMethod]
        public void PropertyGet_ByExtensionAndReturnType_Public_FromStruct()
        {
            var pg = _testStrucType.PropertyGet<string>("PublicProperty");
            Assert.IsNotNull(pg);
            Assert.AreEqual(_testStructInstance.PublicProperty, pg(_testStructInstance));
        }

        [TestMethod]
        public void PropertyGet_ByExtensionAndReturnType_Public_Struct()
        {
            var pg = _testClassType.PropertyGet<int>("PublicPropertyInt");
            Assert.IsNotNull(pg);
            Assert.AreEqual(_testClassInstance.PublicPropertyInt, pg(_testClassInstance));
        }

        [TestMethod]
        public void PropertyGet_ByExtensionAndReturnType_Public_Struct_FromStruct()
        {
            var pg = _testStrucType.PropertyGet<int>("PublicPropertyInt");
            Assert.IsNotNull(pg);
            Assert.AreEqual(_testStructInstance.PublicPropertyInt, pg(_testStructInstance));
        }

        [TestMethod]
        public void PropertyGet_ByObjects_Internal()
        {
            var pg = _testClassType.PropertyGet("InternalProperty");
            Assert.IsNotNull(pg);
            Assert.AreEqual(_testClassInstance.InternalProperty, pg(_testClassInstance));
        }

        [TestMethod]
        public void PropertyGet_ByObjects_Internal_FromStruct()
        {
            var pg = _testStrucType.PropertyGet("InternalProperty");
            Assert.IsNotNull(pg);
            Assert.AreEqual(_testStructInstance.InternalProperty, pg(_testStructInstance));
        }

        [TestMethod]
        public void PropertyGet_ByObjects_NonExisting()
        {
            var pg = DelegateFactory.PropertyGet<TestClass, string>("NonExisting");
            Assert.IsNull(pg);
        }

        [TestMethod]
        public void PropertyGet_ByObjects_OnlyWrite()
        {
            var pg = _testClassType.PropertyGet("OnlySetProperty");
            Assert.IsNull(pg);
        }

        [TestMethod]
        public void PropertyGet_ByObjects_Private()
        {
            var pg = _testClassType.PropertyGet("PrivateProperty");
            Assert.IsNotNull(pg);
            Assert.AreEqual(_testClassInstance.GetPrivateProperty(), pg(_testClassInstance));
        }

        [TestMethod]
        public void PropertyGet_ByObjects_Private_FromStruct()
        {
            var pg = _testStrucType.PropertyGet("PrivateProperty");
            Assert.IsNotNull(pg);
            Assert.AreEqual(_testStructInstance.GetPrivateProperty(), pg(_testStructInstance));
        }

        [TestMethod]
        public void PropertyGet_ByObjects_Protected()
        {
            var pg = _testClassType.PropertyGet("ProtectedProperty");
            Assert.IsNotNull(pg);
            Assert.AreEqual(_testClassInstance.GetProtectedProperty(), pg(_testClassInstance));
        }

        [TestMethod]
        public void PropertyGet_ByObjects_Public()
        {
            var pg = _testClassType.PropertyGet("PublicProperty");
            Assert.IsNotNull(pg);
            Assert.AreEqual(_testClassInstance.PublicProperty, pg(_testClassInstance));
        }


        [TestMethod]
        public void PropertyGet_ByObjects_Public_FromStruct()
        {
            var pg = _testStrucType.PropertyGet("PublicProperty");
            Assert.IsNotNull(pg);
            Assert.AreEqual(_testStructInstance.PublicProperty, pg(_testStructInstance));
        }

        [TestMethod]
        public void PropertyGet_ByObjects_Public_Struct()
        {
            var pg = _testClassType.PropertyGet("PublicPropertyInt");
            Assert.IsNotNull(pg);
            Assert.AreEqual(_testClassInstance.PublicPropertyInt, pg(_testClassInstance));
        }

        [TestMethod]
        public void PropertyGet_ByObjects_Public_Struct_FromStruct()
        {
            var pg = _testStrucType.PropertyGet("PublicPropertyInt");
            Assert.IsNotNull(pg);
            Assert.AreEqual(_testStructInstance.PublicPropertyInt, pg(_testStructInstance));
        }

        [TestMethod]
        public void PropertyGet_ByTypes_Internal()
        {
            var pg = DelegateFactory.PropertyGet<TestClass, string>("InternalProperty");
            Assert.IsNotNull(pg);
            Assert.AreEqual(_testClassInstance.InternalProperty, pg(_testClassInstance));
        }


        [TestMethod]
        public void PropertyGet_ByTypes_Internal_FromStruct()
        {
            var pg = DelegateFactory.PropertyGetStruct<TestStruct, string>("InternalProperty");
            Assert.IsNotNull(pg);
            Assert.AreEqual(_testStructInstance.InternalProperty, pg(ref _testStructInstance));
        }

        [TestMethod]
        public void PropertyGet_ByTypes_NonExisting()
        {
            var pg = DelegateFactory.PropertyGet<TestClass, string>("NonExisting");
            Assert.IsNull(pg);
        }

        [TestMethod]
        public void PropertyGet_ByTypes_OnlyWrite()
        {
            var pg = DelegateFactory.PropertyGet<TestClass, string>("OnlySetProperty");
            Assert.IsNull(pg);
        }

        [TestMethod]
        public void PropertyGet_ByTypes_Private()
        {
            var pg = DelegateFactory.PropertyGet<TestClass, string>("PrivateProperty");
            Assert.IsNotNull(pg);
            Assert.AreEqual(_testClassInstance.GetPrivateProperty(), pg(_testClassInstance));
        }

        [TestMethod]
        public void PropertyGet_ByTypes_Private_FromStruct()
        {
            var pg = DelegateFactory.PropertyGetStruct<TestStruct, string>("PrivateProperty");
            Assert.IsNotNull(pg);
            Assert.AreEqual(_testStructInstance.GetPrivateProperty(), pg(ref _testStructInstance));
        }

        [TestMethod]
        public void PropertyGet_ByTypes_Protected()
        {
            var pg = DelegateFactory.PropertyGet<TestClass, string>("ProtectedProperty");
            Assert.IsNotNull(pg);
            Assert.AreEqual(_testClassInstance.GetProtectedProperty(), pg(_testClassInstance));
        }

        [TestMethod]
        public void PropertyGet_ByTypes_Public()
        {
            var pg = DelegateFactory.PropertyGet<TestClass, string>("PublicProperty");
            Assert.IsNotNull(pg);
            Assert.AreEqual(_testClassInstance.PublicProperty, pg(_testClassInstance));
        }

        [TestMethod]
        public void PropertyGet_ByTypes_Public_Struct()
        {
            var pg = DelegateFactory.PropertyGet<TestClass, int>("PublicPropertyInt");
            Assert.IsNotNull(pg);
            Assert.AreEqual(_testClassInstance.PublicPropertyInt, pg(_testClassInstance));
        }


        [TestMethod]
        public void PropertyGet_ByTypes_Public_FromStruct()
        {
            var pg = DelegateFactory.PropertyGetStruct<TestStruct, string>("PublicProperty");
            Assert.IsNotNull(pg);
            Assert.AreEqual(_testStructInstance.PublicProperty, pg(ref _testStructInstance));
        }

        [TestMethod]
        public void PropertyGet_ByTypes_Public_Struct_FromStruct()
        {
            var pg = DelegateFactory.PropertyGetStruct<TestStruct, int>("PublicPropertyInt");
            Assert.IsNotNull(pg);
            Assert.AreEqual(_testStructInstance.PublicPropertyInt, pg(ref _testStructInstance));
        }
    }
}