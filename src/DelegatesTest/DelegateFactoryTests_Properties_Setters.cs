// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DelegateFactoryTests_Properties_Setters.cs" company="Natan Podbielski">
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
#elif NETCORE
        DelegatesTestNETCORE
#elif NETSTANDARD1_1
        DelegatesTestNETStandard11
#elif NETSTANDARD1_5
        DelegatesTestNETStandard15
#endif
{
    [TestClass]
    public class DelegateFactoryTests_Properties_Setters
    {
        private const int NewIntValue = 0;
        private const string NewStringValue = "Test";

        [TestMethod]
        public void PropertySet_ByExtensionAndReturnType_Internal()
        {
            var ps = typeof(TestClass).PropertySet<string>("InternalProperty");
            Assert.IsNotNull(ps);
            var testClassInstance = new TestClass();
            ps(testClassInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, testClassInstance.InternalProperty);
        }

        [TestMethod]
        public void PropertySet_ByExtensionAndReturnType_NonExisting()
        {
            var ps = DelegateFactory.PropertySet<TestClass, string>("NonExisting");
            Assert.IsNull(ps);
        }

        [TestMethod]
        public void PropertySet_ByExtensionAndReturnType_OnlyRead()
        {
            var ps = typeof(TestClass).PropertySet<string>("OnlyGetProperty");
            Assert.IsNull(ps);
        }

        [TestMethod]
        public void PropertySet_ByExtensionAndReturnType_Private()
        {
            var ps = typeof(TestClass).PropertySet<string>("PrivateProperty");
            Assert.IsNotNull(ps);
            var testClassInstance = new TestClass();
            ps(testClassInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, testClassInstance.GetPrivateProperty());
        }

        [TestMethod]
        public void PropertySet_ByExtensionAndReturnType_Protected()
        {
            var ps = typeof(TestClass).PropertySet<string>("ProtectedProperty");
            Assert.IsNotNull(ps);
            var testClassInstance = new TestClass();
            ps(testClassInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, testClassInstance.GetProtectedProperty());
        }

        [TestMethod]
        public void PropertySet_ByExtensionAndReturnType_Public()
        {
            var ps = typeof(TestClass).PropertySet<string>("PublicProperty");
            Assert.IsNotNull(ps);
            var testClassInstance = new TestClass();
            ps(testClassInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, testClassInstance.PublicProperty);
        }

        [TestMethod]
        public void PropertySet_ByExtensionAndReturnType_Public_Struct()
        {
            var ps = typeof(TestClass).PropertySet<int>("PublicPropertyInt");
            Assert.IsNotNull(ps);
            var testClassInstance = new TestClass();
            ps(testClassInstance, NewIntValue);
            Assert.AreEqual(NewIntValue, testClassInstance.PublicPropertyInt);
        }

        [TestMethod]
        public void PropertySet_ByObjects_Internal()
        {
            var ps = typeof(TestClass).PropertySet("InternalProperty");
            Assert.IsNotNull(ps);
            var testClassInstance = new TestClass();
            ps(testClassInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, testClassInstance.InternalProperty);
        }

        [TestMethod]
        public void PropertySet_ByObjects_NonExisting()
        {
            var ps = typeof(TestStruct).PropertySet("NonExisting");
            Assert.IsNull(ps);
        }

        [TestMethod]
        public void PropertySet_ByObjects_OnlyRead()
        {
            var ps = typeof(TestClass).PropertySet("OnlyGetProperty");
            Assert.IsNull(ps);
        }

        [TestMethod]
        public void PropertySet_ByObjects_Private()
        {
            var ps = typeof(TestClass).PropertySet("PrivateProperty");
            Assert.IsNotNull(ps);
            var testClassInstance = new TestClass();
            ps(testClassInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, testClassInstance.GetPrivateProperty());
        }

        [TestMethod]
        public void PropertySet_ByObjects_Protected()
        {
            var ps = typeof(TestClass).PropertySet("ProtectedProperty");
            Assert.IsNotNull(ps);
            var testClassInstance = new TestClass();
            ps(testClassInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, testClassInstance.GetProtectedProperty());
        }

        [TestMethod]
        public void PropertySet_ByObjects_Public()
        {
            var ps = typeof(TestClass).PropertySet("PublicProperty");
            Assert.IsNotNull(ps);
            var testClassInstance = new TestClass();
            ps(testClassInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, testClassInstance.PublicProperty);
        }

        [TestMethod]
        public void PropertySet_ByObjects_Public_Struct()
        {
            var ps = typeof(TestClass).PropertySet("PublicPropertyInt");
            Assert.IsNotNull(ps);
            var testClassInstance = new TestClass();
            ps(testClassInstance, NewIntValue);
            Assert.AreEqual(NewIntValue, testClassInstance.PublicPropertyInt);
        }

        [TestMethod]
        public void PropertySet_ByTypes_Internal()
        {
            var ps = DelegateFactory.PropertySet<TestClass, string>("InternalProperty");
            Assert.IsNotNull(ps);
            var testClassInstance = new TestClass();
            ps(testClassInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, testClassInstance.InternalProperty);
        }

        [TestMethod]
        public void PropertySet_ByTypes_Internal_FromStruct()
        {
            var ps = DelegateFactory.PropertySetStructRef<TestStruct, string>("InternalProperty");
            Assert.IsNotNull(ps);
            var testStructInstance = new TestStruct(0);
            ps(ref testStructInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, testStructInstance.InternalProperty);
        }

        [TestMethod]
        public void PropertySet_ByTypes_NonExisting()
        {
            var ps = DelegateFactory.PropertySet<TestClass, string>("NonExisting");
            Assert.IsNull(ps);
        }

        [TestMethod]
        public void PropertySet_ByTypes_OnlyRead()
        {
            var ps = DelegateFactory.PropertySet<TestClass, string>("OnlyGetProperty");
            Assert.IsNull(ps);
        }

        [TestMethod]
        public void PropertySet_ByTypes_Private()
        {
            var ps = DelegateFactory.PropertySet<TestClass, string>("PrivateProperty");
            Assert.IsNotNull(ps);
            var testClassInstance = new TestClass();
            ps(testClassInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, testClassInstance.GetPrivateProperty());
        }

        [TestMethod]
        public void PropertySet_ByTypes_Private_FromStruct()
        {
            var ps = DelegateFactory.PropertySetStructRef<TestStruct, string>("PrivateProperty");
            Assert.IsNotNull(ps);
            var testStructInstance = new TestStruct(0);
            ps(ref testStructInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, testStructInstance.GetPrivateProperty());
        }

        [TestMethod]
        public void PropertySet_ByTypes_Protected()
        {
            var ps = DelegateFactory.PropertySet<TestClass, string>("ProtectedProperty");
            Assert.IsNotNull(ps);
            var testClassInstance = new TestClass();
            ps(testClassInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, testClassInstance.GetProtectedProperty());
        }

        [TestMethod]
        public void PropertySet_ByTypes_Public()
        {
            var ps = DelegateFactory.PropertySet<TestClass, string>("PublicProperty");
            Assert.IsNotNull(ps);
            var testClassInstance = new TestClass();
            ps(testClassInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, testClassInstance.PublicProperty);
        }

        [TestMethod]
        public void PropertySet_ByTypes_Public_FromStruct()
        {
            var ps = DelegateFactory.PropertySetStructRef<TestStruct, string>("PublicProperty");
            Assert.IsNotNull(ps);
            var testStructInstance = new TestStruct(0);
            ps(ref testStructInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, testStructInstance.PublicProperty);
        }

        [TestMethod]
        public void PropertySet_ByTypes_Public_Struct()
        {
            var ps = DelegateFactory.PropertySet<TestClass, int>("PublicPropertyInt");
            Assert.IsNotNull(ps);
            var testClassInstance = new TestClass();
            ps(testClassInstance, NewIntValue);
            Assert.AreEqual(NewIntValue, testClassInstance.PublicPropertyInt);
        }

        [TestMethod]
        public void PropertySet_ByTypes_Public_Struct_FromStruct()
        {
            var ps = DelegateFactory.PropertySetStructRef<TestStruct, int>("PublicPropertyInt");
            Assert.IsNotNull(ps);
            var testStructInstance = new TestStruct(0);
            ps(ref testStructInstance, NewIntValue);
            Assert.AreEqual(NewIntValue, testStructInstance.PublicPropertyInt);
        }

        [TestMethod]
        public void PropertySet_Interface_ByTypes()
        {
            var ps = DelegateFactory.PropertySet<IService, string>("Property");
            Assert.IsNotNull(ps);
            var interfaceImpl = new Service();
            ps(interfaceImpl, NewStringValue);
            Assert.AreEqual(NewStringValue, interfaceImpl.Property);
        }

        [TestMethod]
        public void PropertySet_Interface_ByObjectAndType()
        {
            var ps = typeof(IService).PropertySet<string>("Property");
            Assert.IsNotNull(ps);
            var interfaceImpl = new Service();
            ps(interfaceImpl, NewStringValue);
            Assert.AreEqual(NewStringValue, interfaceImpl.Property);
        }

        [TestMethod]
        public void PropertySet_Interface_ByObjects()
        {
            var ps = typeof(IService).PropertySet("Property");
            Assert.IsNotNull(ps);
            var interfaceImpl = new Service();
            ps(interfaceImpl, NewStringValue);
            Assert.AreEqual(NewStringValue, interfaceImpl.Property);
        }

#if !NET35
        [TestMethod]
        public void PropertySet_ByExtensionAndReturnType_Internal_FromStructRef()
        {
            var ps = typeof(TestStruct).PropertySetStructRef<string>("InternalProperty");
            Assert.IsNotNull(ps);
            var objectStruct = (object)new TestStruct(0);
            ps(ref objectStruct, NewStringValue);
            Assert.AreEqual(NewStringValue, ((TestStruct)objectStruct).InternalProperty);
        }

        [TestMethod]
        public void PropertySet_ByExtensionAndReturnType_Internal_FromStruct()
        {
            var ps = typeof(TestStruct).PropertySetStruct<string>("InternalProperty");
            Assert.IsNotNull(ps);
            var objectStruct = ps(new TestStruct(0), NewStringValue);
            Assert.AreEqual(NewStringValue, ((TestStruct)objectStruct).InternalProperty);
        }
#endif

#if !NET35
        [TestMethod]
        public void PropertySet_ByExtensionAndReturnType_Private_FromStructRef()
        {
            var ps = typeof(TestStruct).PropertySetStructRef<string>("PrivateProperty");
            Assert.IsNotNull(ps);
            var objectStruct = (object)new TestStruct(0);
            ps(ref objectStruct, NewStringValue);
            Assert.AreEqual(NewStringValue, ((TestStruct)objectStruct).GetPrivateProperty());
        }

        [TestMethod]
        public void PropertySet_ByExtensionAndReturnType_Private_FromStruct()
        {
            var ps = typeof(TestStruct).PropertySetStruct<string>("PrivateProperty");
            Assert.IsNotNull(ps);
            var objectStruct = ps(new TestStruct(0), NewStringValue);
            Assert.AreEqual(NewStringValue, ((TestStruct)objectStruct).GetPrivateProperty());
        }
#endif

#if !NET35
        [TestMethod]
        public void PropertySet_ByExtensionAndReturnType_Public_FromStructRef()
        {
            var ps = typeof(TestStruct).PropertySetStructRef<string>("PublicProperty");
            Assert.IsNotNull(ps);
            var objectStruct = (object)new TestStruct(0);
            ps(ref objectStruct, NewStringValue);
            Assert.AreEqual(NewStringValue, ((TestStruct)objectStruct).PublicProperty);
        }

        [TestMethod]
        public void PropertySet_ByExtensionAndReturnType_Public_FromStruct()
        {
            var ps = typeof(TestStruct).PropertySetStruct<string>("PublicProperty");
            Assert.IsNotNull(ps);
            var objectStruct = ps(new TestStruct(0), NewStringValue);
            Assert.AreEqual(NewStringValue, ((TestStruct)objectStruct).PublicProperty);
        }
#endif

#if !NET35
        [TestMethod]
        public void PropertySet_ByExtensionAndReturnType_Public_Struct_FromStructRef()
        {
            var ps = typeof(TestStruct).PropertySetStructRef<int>("PublicPropertyInt");
            Assert.IsNotNull(ps);
            var objectStruct = (object)new TestStruct(0);
            ps(ref objectStruct, NewIntValue);
            Assert.AreEqual(NewIntValue, ((TestStruct)objectStruct).PublicPropertyInt);
        }

        [TestMethod]
        public void PropertySet_ByExtensionAndReturnType_Public_Struct_FromStruct()
        {
            var ps = typeof(TestStruct).PropertySetStruct<int>("PublicPropertyInt");
            Assert.IsNotNull(ps);
            var objectStruct = ps(new TestStruct(0), NewIntValue);
            Assert.AreEqual(NewIntValue, ((TestStruct)objectStruct).PublicPropertyInt);
        }
#endif

#if !NET35
        [TestMethod]
        public void PropertySet_ByObjects_Internal_FromStructRef()
        {
            var ps = typeof(TestStruct).PropertySetStructRef("InternalProperty");
            Assert.IsNotNull(ps);
            var objectStruct = (object)new TestStruct(0);
            ps(ref objectStruct, NewStringValue);
            Assert.AreEqual(NewStringValue, ((TestStruct)objectStruct).InternalProperty);
        }

        [TestMethod]
        public void PropertySet_ByObjects_Internal_FromStruct()
        {
            var ps = typeof(TestStruct).PropertySetStruct("InternalProperty");
            Assert.IsNotNull(ps);
            var objectStruct = ps(new TestStruct(0), NewStringValue);
            Assert.AreEqual(NewStringValue, ((TestStruct)objectStruct).InternalProperty);
        }
#endif

#if !NET35
        [TestMethod]
        public void PropertySet_ByObjects_Private_FromStructRef()
        {
            var ps = typeof(TestStruct).PropertySetStructRef("PrivateProperty");
            Assert.IsNotNull(ps);
            var objectStruct = (object)new TestStruct(0);
            ps(ref objectStruct, NewStringValue);
            Assert.AreEqual(NewStringValue, ((TestStruct)objectStruct).GetPrivateProperty());
        }

        [TestMethod]
        public void PropertySet_ByObjects_Private_FromStruct()
        {
            var ps = typeof(TestStruct).PropertySetStruct("PrivateProperty");
            Assert.IsNotNull(ps);
            var objectStruct = ps(new TestStruct(0), NewStringValue);
            Assert.AreEqual(NewStringValue, ((TestStruct)objectStruct).GetPrivateProperty());
        }
#endif

#if !NET35
        [TestMethod]
        public void PropertySet_ByObjects_Public_FromStructRef()
        {
            var ps = typeof(TestStruct).PropertySetStructRef("PublicProperty");
            Assert.IsNotNull(ps);
            var objectStruct = (object)new TestStruct(0);
            ps(ref objectStruct, NewStringValue);
            Assert.AreEqual(NewStringValue, ((TestStruct)objectStruct).PublicProperty);
        }

        [TestMethod]
        public void PropertySet_ByObjects_Public_FromStruct()
        {
            var ps = typeof(TestStruct).PropertySetStruct("PublicProperty");
            Assert.IsNotNull(ps);
            var objectStruct = ps(new TestStruct(0), NewStringValue);
            Assert.AreEqual(NewStringValue, ((TestStruct)objectStruct).PublicProperty);
        }
#endif

#if !NET35
        [TestMethod]
        public void PropertySet_ByObjects_Public_Struct_FromStruct()
        {
            var ps = typeof(TestStruct).PropertySetStruct("PublicPropertyInt");
            Assert.IsNotNull(ps);
            var objectStruct = ps(new TestStruct(0), NewIntValue);
            Assert.AreEqual(NewIntValue, ((TestStruct)objectStruct).PublicPropertyInt);
        }

        [TestMethod]
        public void PropertySet_ByObjects_Public_Struct_FromStructRef()
        {
            var ps = typeof(TestStruct).PropertySetStructRef("PublicPropertyInt");
            Assert.IsNotNull(ps);
            var objectStruct = (object)new TestStruct(0);
            ps(ref objectStruct, NewIntValue);
            Assert.AreEqual(NewIntValue, ((TestStruct)objectStruct).PublicPropertyInt);
        }
#endif
    }
}