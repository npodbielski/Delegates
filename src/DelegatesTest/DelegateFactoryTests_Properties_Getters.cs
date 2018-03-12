// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DelegateFactoryTests_Properties_Getters.cs" company="Natan Podbielski">
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
#elif NETCOREAPP10
        DelegatesTestNETCORE10
#elif NETCOREAPP11
    DelegatesTestNETCORE11
#elif NETCOREAPP20
        DelegatesTestNETCORE20
#elif NETSTANDARD1_1
        DelegatesTestNETStandard11
#elif NETSTANDARD1_5
        DelegatesTestNETStandard15
#endif
{
    [TestClass]
    public class DelegateFactoryTests_Properties_Getters
    {
        [TestMethod]
        public void PropertyGet_ByExtensionAndReturnType_NonExisting()
        {
            var pg = DelegateFactory.PropertyGet<TestClass, string>("NonExisting");
            Assert.IsNull(pg);
        }

        [TestMethod]
        public void PropertyGet_ByExtensionAndReturnType_OnlyWrite()
        {
            var pg = typeof(TestClass).PropertyGet<string>("OnlySetProperty");
            Assert.IsNull(pg);
        }

        [TestMethod]
        public void PropertyGet_ByExtensionAndReturnType_Internal()
        {
            var pg = typeof(TestClass).PropertyGet<string>("InternalProperty");
            Assert.IsNotNull(pg);
            var testClassInstance = new TestClass();
            Assert.AreEqual(testClassInstance.InternalProperty, pg(testClassInstance));
        }

        [TestMethod]
        public void PropertyGet_ByExtensionAndReturnType_Internal_FromStruct()
        {
            var pg = typeof(TestStruct).PropertyGet<string>("InternalProperty");
            Assert.IsNotNull(pg);
            var testStructInstance = new TestStruct(0);
            Assert.AreEqual(testStructInstance.InternalProperty, pg(testStructInstance));
        }

        [TestMethod]
        public void PropertyGet_ByExtensionAndReturnType_Private()
        {
            var pg = typeof(TestClass).PropertyGet<string>("PrivateProperty");
            Assert.IsNotNull(pg);
            var testClassInstance = new TestClass();
            Assert.AreEqual(testClassInstance.GetPrivateProperty(), pg(testClassInstance));
        }

        [TestMethod]
        public void PropertyGet_ByExtensionAndReturnType_Private_FromStruct()
        {
            var pg = typeof(TestStruct).PropertyGet<string>("PrivateProperty");
            Assert.IsNotNull(pg);
            var testStructInstance = new TestStruct(0);
            Assert.AreEqual(testStructInstance.GetPrivateProperty(), pg(testStructInstance));
        }

        [TestMethod]
        public void PropertyGet_ByExtensionAndReturnType_Protected()
        {
            var pg = typeof(TestClass).PropertyGet<string>("ProtectedProperty");
            Assert.IsNotNull(pg);
            var testClassInstance = new TestClass();
            Assert.AreEqual(testClassInstance.GetProtectedProperty(), pg(testClassInstance));
        }

        [TestMethod]
        public void PropertyGet_ByExtensionAndReturnType_Public()
        {
            var pg = typeof(TestClass).PropertyGet<string>("PublicProperty");
            Assert.IsNotNull(pg);
            var testClassInstance = new TestClass();
            Assert.AreEqual(testClassInstance.PublicProperty, pg(testClassInstance));
        }

        [TestMethod]
        public void PropertyGet_ByExtensionAndReturnType_Public_FromStruct()
        {
            var pg = typeof(TestStruct).PropertyGet<string>("PublicProperty");
            Assert.IsNotNull(pg);
            var testStructInstance = new TestStruct(0);
            Assert.AreEqual(testStructInstance.PublicProperty, pg(testStructInstance));
        }

        [TestMethod]
        public void PropertyGet_ByExtensionAndReturnType_Public_Struct()
        {
            var pg = typeof(TestClass).PropertyGet<int>("PublicPropertyInt");
            Assert.IsNotNull(pg);
            var testClassInstance = new TestClass();
            Assert.AreEqual(testClassInstance.PublicPropertyInt, pg(testClassInstance));
        }

        [TestMethod]
        public void PropertyGet_ByExtensionAndReturnType_Public_Struct_FromStruct()
        {
            var pg = typeof(TestStruct).PropertyGet<int>("PublicPropertyInt");
            Assert.IsNotNull(pg);
            var testStructInstance = new TestStruct(0);
            Assert.AreEqual(testStructInstance.PublicPropertyInt, pg(testStructInstance));
        }

        [TestMethod]
        public void PropertyGet_ByObjects_Internal()
        {
            var pg = typeof(TestClass).PropertyGet("InternalProperty");
            Assert.IsNotNull(pg);
            var testClassInstance = new TestClass();
            Assert.AreEqual(testClassInstance.InternalProperty, pg(testClassInstance));
        }

        [TestMethod]
        public void PropertyGet_ByObjects_Internal_FromStruct()
        {
            var pg = typeof(TestStruct).PropertyGet("InternalProperty");
            Assert.IsNotNull(pg);
            var testStructInstance = new TestStruct(0);
            Assert.AreEqual(testStructInstance.InternalProperty, pg(testStructInstance));
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
            var pg = typeof(TestClass).PropertyGet("OnlySetProperty");
            Assert.IsNull(pg);
        }

        [TestMethod]
        public void PropertyGet_ByObjects_Private()
        {
            var pg = typeof(TestClass).PropertyGet("PrivateProperty");
            Assert.IsNotNull(pg);
            var testClassInstance = new TestClass();
            Assert.AreEqual(testClassInstance.GetPrivateProperty(), pg(testClassInstance));
        }

        [TestMethod]
        public void PropertyGet_ByObjects_Private_FromStruct()
        {
            var pg = typeof(TestStruct).PropertyGet("PrivateProperty");
            Assert.IsNotNull(pg);
            var testStructInstance = new TestStruct(0);
            Assert.AreEqual(testStructInstance.GetPrivateProperty(), pg(testStructInstance));
        }

        [TestMethod]
        public void PropertyGet_ByObjects_Protected()
        {
            var pg = typeof(TestClass).PropertyGet("ProtectedProperty");
            Assert.IsNotNull(pg);
            var testClassInstance = new TestClass();
            Assert.AreEqual(testClassInstance.GetProtectedProperty(), pg(testClassInstance));
        }

        [TestMethod]
        public void PropertyGet_ByObjects_Public()
        {
            var pg = typeof(TestClass).PropertyGet("PublicProperty");
            Assert.IsNotNull(pg);
            var testClassInstance = new TestClass();
            Assert.AreEqual(testClassInstance.PublicProperty, pg(testClassInstance));
        }


        [TestMethod]
        public void PropertyGet_ByObjects_Public_FromStruct()
        {
            var pg = typeof(TestStruct).PropertyGet("PublicProperty");
            Assert.IsNotNull(pg);
            var testStructInstance = new TestStruct(0);
            Assert.AreEqual(testStructInstance.PublicProperty, pg(testStructInstance));
        }

        [TestMethod]
        public void PropertyGet_ByObjects_Public_Struct()
        {
            var pg = typeof(TestClass).PropertyGet("PublicPropertyInt");
            Assert.IsNotNull(pg);
            var testClassInstance = new TestClass();
            Assert.AreEqual(testClassInstance.PublicPropertyInt, pg(testClassInstance));
        }

        [TestMethod]
        public void PropertyGet_ByObjects_Public_Struct_FromStruct()
        {
            var pg = typeof(TestStruct).PropertyGet("PublicPropertyInt");
            Assert.IsNotNull(pg);
            var testStructInstance = new TestStruct(0);
            Assert.AreEqual(testStructInstance.PublicPropertyInt, pg(testStructInstance));
        }

        [TestMethod]
        public void PropertyGet_ByTypes_Internal()
        {
            var pg = DelegateFactory.PropertyGet<TestClass, string>("InternalProperty");
            Assert.IsNotNull(pg);
            var testClassInstance = new TestClass();
            Assert.AreEqual(testClassInstance.InternalProperty, pg(testClassInstance));
        }

        [TestMethod]
        public void PropertyGet_ByTypes_Internal_FromStruct()
        {
            var pg = DelegateFactory.PropertyGetStruct<TestStruct, string>("InternalProperty");
            Assert.IsNotNull(pg);
            var testStructInstance = new TestStruct(0);
            Assert.AreEqual(testStructInstance.InternalProperty, pg(ref testStructInstance));
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
            var testClassInstance = new TestClass();
            Assert.AreEqual(testClassInstance.GetPrivateProperty(), pg(testClassInstance));
        }

        [TestMethod]
        public void PropertyGet_ByTypes_Private_FromStruct()
        {
            var pg = DelegateFactory.PropertyGetStruct<TestStruct, string>("PrivateProperty");
            Assert.IsNotNull(pg);
            var testStructInstance = new TestStruct(0);
            Assert.AreEqual(testStructInstance.GetPrivateProperty(), pg(ref testStructInstance));
        }

        [TestMethod]
        public void PropertyGet_ByTypes_Protected()
        {
            var pg = DelegateFactory.PropertyGet<TestClass, string>("ProtectedProperty");
            Assert.IsNotNull(pg);
            var testClassInstance = new TestClass();
            Assert.AreEqual(testClassInstance.GetProtectedProperty(), pg(testClassInstance));
        }

        [TestMethod]
        public void PropertyGet_ByTypes_Public()
        {
            var pg = DelegateFactory.PropertyGet<TestClass, string>("PublicProperty");
            Assert.IsNotNull(pg);
            var testClassInstance = new TestClass();
            Assert.AreEqual(testClassInstance.PublicProperty, pg(testClassInstance));
        }

        [TestMethod]
        public void PropertyGet_ByTypes_Public_Struct()
        {
            var pg = DelegateFactory.PropertyGet<TestClass, int>("PublicPropertyInt");
            Assert.IsNotNull(pg);
            var testClassInstance = new TestClass();
            Assert.AreEqual(testClassInstance.PublicPropertyInt, pg(testClassInstance));
        }


        [TestMethod]
        public void PropertyGet_ByTypes_Public_FromStruct()
        {
            var pg = DelegateFactory.PropertyGetStruct<TestStruct, string>("PublicProperty");
            Assert.IsNotNull(pg);
            var testStructInstance = new TestStruct(0);
            Assert.AreEqual(testStructInstance.PublicProperty, pg(ref testStructInstance));
        }

        [TestMethod]
        public void PropertyGet_ByTypes_Public_Struct_FromStruct()
        {
            var pg = DelegateFactory.PropertyGetStruct<TestStruct, int>("PublicPropertyInt");
            Assert.IsNotNull(pg);
            var testStructInstance = new TestStruct(0);
            Assert.AreEqual(testStructInstance.PublicPropertyInt, pg(ref testStructInstance));
        }

        [TestMethod]
        public void PropertyGet_Interface_ByTypes()
        {
            var pg = DelegateFactory.PropertyGet<IService, string>("Property");
            Assert.IsNotNull(pg);
            var interfaceImpl = new Service();
            Assert.AreEqual(interfaceImpl.Property, pg(interfaceImpl));
        }

        [TestMethod]
        public void PropertyGet_Interface_ByObjectAndType()
        {
            var pg = typeof(IService).PropertyGet<string>("Property");
            Assert.IsNotNull(pg);
            var interfaceImpl = new Service();
            Assert.AreEqual(interfaceImpl.Property, pg(interfaceImpl));
        }

        [TestMethod]
        public void PropertyGet_Interface_ByObjects()
        {
            var pg = typeof(IService).PropertyGet("Property");
            Assert.IsNotNull(pg);
            var interfaceImpl = new Service();
            Assert.AreEqual(interfaceImpl.Property, pg(interfaceImpl));
        }

        [TestMethod]
        public void PropertyGet_NonStaticByStaticName_ByObjects()
        {
            var pg = typeof(TestClass).PropertyGet("StaticPublicProperty");
            Assert.IsNull(pg);
        }
        
        [TestMethod]
        public void PropertyGet_NonStaticByStaticName_ByObjectAndType()
        {
            var pg = typeof(TestClass).PropertyGet<string>("StaticPublicProperty");
            Assert.IsNull(pg);
        }


        [TestMethod]
        public void PropertyGet_NonStaticByStaticName_ByTypes()
        {
            var pg = DelegateFactory.PropertyGet<TestClass,string>("StaticPublicProperty");
            Assert.IsNull(pg);
        }
    }
}