// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DelegateFactoryTests_Properties_Setters.cs" company="Natan Podbielski">
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
    public class DelegateFactoryTests_Properties_Setters
    {
        private const int NewIntValue = 0;
        private const string NewStringValue = "Test";
        private readonly TestClass _testClassInstance = new TestClass();
        private readonly Type _testClassType = typeof(TestClass);
        private readonly Type _testStrucType = typeof(TestStruct);
        private TestStruct _testStructInstance = new TestStruct(0);
        private readonly Type _interfaceType = typeof(IService);
        private readonly IService _interfaceImpl = new Service();

        [TestMethod]
        public void PropertySet_ByExtensionAndReturnType_Internal()
        {
            var ps = _testClassType.PropertySet<string>("InternalProperty");
            Assert.IsNotNull(ps);
            ps(_testClassInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, _testClassInstance.InternalProperty);
        }

#if !NET35
        [TestMethod]
        public void PropertySet_ByExtensionAndReturnType_Internal_FromStructRef()
        {
            var ps = _testStrucType.PropertySetStructRef<string>("InternalProperty");
            Assert.IsNotNull(ps);
            var objectStruct = (object)_testStructInstance;
            ps(ref objectStruct, NewStringValue);
            Assert.AreEqual(NewStringValue, ((TestStruct)objectStruct).InternalProperty);
        }

        [TestMethod]
        public void PropertySet_ByExtensionAndReturnType_Internal_FromStruct()
        {
            var ps = _testStrucType.PropertySetStruct<string>("InternalProperty");
            Assert.IsNotNull(ps);
            var objectStruct = ps(_testStructInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, ((TestStruct)objectStruct).InternalProperty);
        } 
#endif

        [TestMethod]
        public void PropertySet_ByExtensionAndReturnType_NonExisting()
        {
            var ps = DelegateFactory.PropertySet<TestClass, string>("NonExisting");
            Assert.IsNull(ps);
        }

        [TestMethod]
        public void PropertySet_ByExtensionAndReturnType_OnlyRead()
        {
            var ps = _testClassType.PropertySet<string>("OnlyGetProperty");
            Assert.IsNull(ps);
        }

        [TestMethod]
        public void PropertySet_ByExtensionAndReturnType_Private()
        {
            var ps = _testClassType.PropertySet<string>("PrivateProperty");
            Assert.IsNotNull(ps);
            ps(_testClassInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, _testClassInstance.GetPrivateProperty());
        }

#if !NET35
        [TestMethod]
        public void PropertySet_ByExtensionAndReturnType_Private_FromStructRef()
        {
            var ps = _testStrucType.PropertySetStructRef<string>("PrivateProperty");
            Assert.IsNotNull(ps);
            var objectStruct = (object)_testStructInstance;
            ps(ref objectStruct, NewStringValue);
            Assert.AreEqual(NewStringValue, ((TestStruct)objectStruct).GetPrivateProperty());
        }

        [TestMethod]
        public void PropertySet_ByExtensionAndReturnType_Private_FromStruct()
        {
            var ps = _testStrucType.PropertySetStruct<string>("PrivateProperty");
            Assert.IsNotNull(ps);
            var objectStruct = ps(_testStructInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, ((TestStruct)objectStruct).GetPrivateProperty());
        } 
#endif

        [TestMethod]
        public void PropertySet_ByExtensionAndReturnType_Protected()
        {
            var ps = _testClassType.PropertySet<string>("ProtectedProperty");
            Assert.IsNotNull(ps);
            ps(_testClassInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, _testClassInstance.GetProtectedProperty());
        }

        [TestMethod]
        public void PropertySet_ByExtensionAndReturnType_Public()
        {
            var ps = _testClassType.PropertySet<string>("PublicProperty");
            Assert.IsNotNull(ps);
            ps(_testClassInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, _testClassInstance.PublicProperty);
        }

#if !NET35
        [TestMethod]
        public void PropertySet_ByExtensionAndReturnType_Public_FromStructRef()
        {
            var ps = _testStrucType.PropertySetStructRef<string>("PublicProperty");
            Assert.IsNotNull(ps);
            var objectStruct = (object)_testStructInstance;
            ps(ref objectStruct, NewStringValue);
            Assert.AreEqual(NewStringValue, ((TestStruct)objectStruct).PublicProperty);
        }

        [TestMethod]
        public void PropertySet_ByExtensionAndReturnType_Public_FromStruct()
        {
            var ps = _testStrucType.PropertySetStruct<string>("PublicProperty");
            Assert.IsNotNull(ps);
            var objectStruct = ps(_testStructInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, ((TestStruct)objectStruct).PublicProperty);
        } 
#endif

        [TestMethod]
        public void PropertySet_ByExtensionAndReturnType_Public_Struct()
        {
            var ps = _testClassType.PropertySet<int>("PublicPropertyInt");
            Assert.IsNotNull(ps);
            ps(_testClassInstance, NewIntValue);
            Assert.AreEqual(NewIntValue, _testClassInstance.PublicPropertyInt);
        }

#if !NET35
        [TestMethod]
        public void PropertySet_ByExtensionAndReturnType_Public_Struct_FromStructRef()
        {
            var ps = _testStrucType.PropertySetStructRef<int>("PublicPropertyInt");
            Assert.IsNotNull(ps);
            var objectStruct = (object)_testStructInstance;
            ps(ref objectStruct, NewIntValue);
            Assert.AreEqual(NewIntValue, ((TestStruct)objectStruct).PublicPropertyInt);
        }

        [TestMethod]
        public void PropertySet_ByExtensionAndReturnType_Public_Struct_FromStruct()
        {
            var ps = _testStrucType.PropertySetStruct<int>("PublicPropertyInt");
            Assert.IsNotNull(ps);
            var objectStruct = ps(_testStructInstance, NewIntValue);
            Assert.AreEqual(NewIntValue, ((TestStruct)objectStruct).PublicPropertyInt);
        } 
#endif

        [TestMethod]
        public void PropertySet_ByObjects_Internal()
        {
            var ps = _testClassType.PropertySet("InternalProperty");
            Assert.IsNotNull(ps);
            ps(_testClassInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, _testClassInstance.InternalProperty);
        }

#if !NET35
        [TestMethod]
        public void PropertySet_ByObjects_Internal_FromStructRef()
        {
            var ps = _testStrucType.PropertySetStructRef("InternalProperty");
            Assert.IsNotNull(ps);
            var objectStruct = (object)_testStructInstance;
            ps(ref objectStruct, NewStringValue);
            Assert.AreEqual(NewStringValue, ((TestStruct)objectStruct).InternalProperty);
        }

        [TestMethod]
        public void PropertySet_ByObjects_Internal_FromStruct()
        {
            var ps = _testStrucType.PropertySetStruct("InternalProperty");
            Assert.IsNotNull(ps);
            var objectStruct = ps(_testStructInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, ((TestStruct)objectStruct).InternalProperty);
        } 
#endif

        [TestMethod]
        public void PropertySet_ByObjects_NonExisting()
        {
            var ps = _testStrucType.PropertySet("NonExisting");
            Assert.IsNull(ps);
        }

        [TestMethod]
        public void PropertySet_ByObjects_OnlyRead()
        {
            var ps = _testClassType.PropertySet("OnlyGetProperty");
            Assert.IsNull(ps);
        }

        [TestMethod]
        public void PropertySet_ByObjects_Private()
        {
            var ps = _testClassType.PropertySet("PrivateProperty");
            Assert.IsNotNull(ps);
            ps(_testClassInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, _testClassInstance.GetPrivateProperty());
        }

#if !NET35
        [TestMethod]
        public void PropertySet_ByObjects_Private_FromStructRef()
        {
            var ps = _testStrucType.PropertySetStructRef("PrivateProperty");
            Assert.IsNotNull(ps);
            var objectStruct = (object)_testStructInstance;
            ps(ref objectStruct, NewStringValue);
            Assert.AreEqual(NewStringValue, ((TestStruct)objectStruct).GetPrivateProperty());
        }

        [TestMethod]
        public void PropertySet_ByObjects_Private_FromStruct()
        {
            var ps = _testStrucType.PropertySetStruct("PrivateProperty");
            Assert.IsNotNull(ps);
            var objectStruct = ps(_testStructInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, ((TestStruct)objectStruct).GetPrivateProperty());
        } 
#endif

        [TestMethod]
        public void PropertySet_ByObjects_Protected()
        {
            var ps = _testClassType.PropertySet("ProtectedProperty");
            Assert.IsNotNull(ps);
            ps(_testClassInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, _testClassInstance.GetProtectedProperty());
        }

        [TestMethod]
        public void PropertySet_ByObjects_Public()
        {
            var ps = _testClassType.PropertySet("PublicProperty");
            Assert.IsNotNull(ps);
            ps(_testClassInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, _testClassInstance.PublicProperty);
        }

#if !NET35
        [TestMethod]
        public void PropertySet_ByObjects_Public_FromStructRef()
        {
            var ps = _testStrucType.PropertySetStructRef("PublicProperty");
            Assert.IsNotNull(ps);
            var objectStruct = (object)_testStructInstance;
            ps(ref objectStruct, NewStringValue);
            Assert.AreEqual(NewStringValue, ((TestStruct)objectStruct).PublicProperty);
        }

        [TestMethod]
        public void PropertySet_ByObjects_Public_FromStruct()
        {
            var ps = _testStrucType.PropertySetStruct("PublicProperty");
            Assert.IsNotNull(ps);
            var objectStruct = ps(_testStructInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, ((TestStruct)objectStruct).PublicProperty);
        } 
#endif

        [TestMethod]
        public void PropertySet_ByObjects_Public_Struct()
        {
            var ps = _testClassType.PropertySet("PublicPropertyInt");
            Assert.IsNotNull(ps);
            ps(_testClassInstance, NewIntValue);
            Assert.AreEqual(NewIntValue, _testClassInstance.PublicPropertyInt);
        }

#if !NET35
        [TestMethod]
        public void PropertySet_ByObjects_Public_Struct_FromStruct()
        {
            var ps = _testStrucType.PropertySetStruct("PublicPropertyInt");
            Assert.IsNotNull(ps);
            var objectStruct = ps(_testStructInstance, NewIntValue);
            Assert.AreEqual(NewIntValue, ((TestStruct)objectStruct).PublicPropertyInt);
        }

        [TestMethod]
        public void PropertySet_ByObjects_Public_Struct_FromStructRef()
        {
            var ps = _testStrucType.PropertySetStructRef("PublicPropertyInt");
            Assert.IsNotNull(ps);
            var objectStruct = (object)_testStructInstance;
            ps(ref objectStruct, NewIntValue);
            Assert.AreEqual(NewIntValue, ((TestStruct)objectStruct).PublicPropertyInt);
        } 
#endif

        [TestMethod]
        public void PropertySet_ByTypes_Internal()
        {
            var ps = DelegateFactory.PropertySet<TestClass, string>("InternalProperty");
            Assert.IsNotNull(ps);
            ps(_testClassInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, _testClassInstance.InternalProperty);
        }

        [TestMethod]
        public void PropertySet_ByTypes_Internal_FromStruct()
        {
            var ps = DelegateFactory.PropertySetStructRef<TestStruct, string>("InternalProperty");
            Assert.IsNotNull(ps);
            ps(ref _testStructInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, _testStructInstance.InternalProperty);
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
            ps(_testClassInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, _testClassInstance.GetPrivateProperty());
        }

        [TestMethod]
        public void PropertySet_ByTypes_Private_FromStruct()
        {
            var ps = DelegateFactory.PropertySetStructRef<TestStruct, string>("PrivateProperty");
            Assert.IsNotNull(ps);
            ps(ref _testStructInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, _testStructInstance.GetPrivateProperty());
        }

        [TestMethod]
        public void PropertySet_ByTypes_Protected()
        {
            var ps = DelegateFactory.PropertySet<TestClass, string>("ProtectedProperty");
            Assert.IsNotNull(ps);
            ps(_testClassInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, _testClassInstance.GetProtectedProperty());
        }

        [TestMethod]
        public void PropertySet_ByTypes_Public()
        {
            var ps = DelegateFactory.PropertySet<TestClass, string>("PublicProperty");
            Assert.IsNotNull(ps);
            ps(_testClassInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, _testClassInstance.PublicProperty);
        }

        [TestMethod]
        public void PropertySet_ByTypes_Public_FromStruct()
        {
            var ps = DelegateFactory.PropertySetStructRef<TestStruct, string>("PublicProperty");
            Assert.IsNotNull(ps);
            ps(ref _testStructInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, _testStructInstance.PublicProperty);
        }

        [TestMethod]
        public void PropertySet_ByTypes_Public_Struct()
        {
            var ps = DelegateFactory.PropertySet<TestClass, int>("PublicPropertyInt");
            Assert.IsNotNull(ps);
            ps(_testClassInstance, NewIntValue);
            Assert.AreEqual(NewIntValue, _testClassInstance.PublicPropertyInt);
        }

        [TestMethod]
        public void PropertySet_ByTypes_Public_Struct_FromStruct()
        {
            var ps = DelegateFactory.PropertySetStructRef<TestStruct, int>("PublicPropertyInt");
            Assert.IsNotNull(ps);
            ps(ref _testStructInstance, NewIntValue);
            Assert.AreEqual(NewIntValue, _testStructInstance.PublicPropertyInt);
        }

        [TestMethod]
        public void PropertySet_Interface_ByTypes()
        {
            var ps = DelegateFactory.PropertySet<IService, string>("Property");
            Assert.IsNotNull(ps);
            ps(_interfaceImpl, NewStringValue);
            Assert.AreEqual(NewStringValue, _interfaceImpl.Property);
        }

        [TestMethod]
        public void PropertySet_Interface_ByObjectAndType()
        {
            var ps = _interfaceType.PropertySet<string>("Property");
            Assert.IsNotNull(ps);
            ps(_interfaceImpl, NewStringValue);
            Assert.AreEqual(NewStringValue, _interfaceImpl.Property);
        }

        [TestMethod]
        public void PropertySet_Interface_ByObjects()
        {
            var ps = _interfaceType.PropertySet("Property");
            Assert.IsNotNull(ps);
            ps(_interfaceImpl, NewStringValue);
            Assert.AreEqual(NewStringValue, _interfaceImpl.Property);
        }
    }
}