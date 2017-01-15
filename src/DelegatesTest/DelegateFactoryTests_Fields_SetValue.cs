// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DelegateFactoryTests_Fields_SetValue.cs" company="Natan Podbielski">
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
#if !NET35
#if !(NETCORE||STANDARD)
    [TestClass]
#endif
    public class DelegateFactoryTests_Fields_SetValue
    {
        private const int NewIntValue = 0;
        private const string NewStringValue = "Test";
        private readonly TestClass _testClassInstance = new TestClass();
        private readonly Type _testClassType = typeof(TestClass);
        private readonly Type _testStructType = typeof(TestStruct);
        private TestStruct _testStructInstance = new TestStruct(0);

        [TestMethod]
        public void FieldSet_ByExtensionAndReturnType_Internal()
        {
            var fs = _testClassType.FieldSet<string>("InternalField");
            Assert.IsNotNull(fs);
            fs(_testClassInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, _testClassInstance.InternalField);
        }

        [TestMethod]
        public void FieldSet_ByExtensionAndReturnType_Internal_FromStructRef()
        {
            var fs = _testStructType.FieldSetStructRef<string>("InternalField");
            Assert.IsNotNull(fs);
            var objectStruct = (object)_testStructInstance;
            fs(ref objectStruct, NewStringValue);
            Assert.AreEqual(NewStringValue, ((TestStruct)objectStruct).InternalField);
        }

        [TestMethod]
        public void FieldSet_ByExtensionAndReturnType_Internal_FromStruct()
        {
            var fs = _testStructType.FieldSetStruct<string>("InternalField");
            Assert.IsNotNull(fs);
            var objectStruct = fs(_testStructInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, ((TestStruct)objectStruct).InternalField);
        }

        [TestMethod]
        public void FieldSet_ByExtensionAndReturnType_NonExisting()
        {
            var fs = _testClassType.FieldSet<string>("NonExisting");
            Assert.IsNull(fs);
        }

        [TestMethod]
        public void FieldSet_ByExtensionAndReturnType_ReadOnly()
        {
            var fs = _testClassType.FieldSet<string>("PublicReadOnlyField");
            Assert.IsNull(fs);
        }

        [TestMethod]
        public void FieldSet_ByExtensionAndReturnType_Private()
        {
            var fs = _testClassType.FieldSet<string>("_privateField");
            Assert.IsNotNull(fs);
            fs(_testClassInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, _testClassInstance.GetPrivateField());
        }

        [TestMethod]
        public void FieldSet_ByExtensionAndReturnType_Private_FromStructRef()
        {
            var fs = _testStructType.FieldSetStructRef<string>("_privateField");
            Assert.IsNotNull(fs);
            var objectStruct = (object)_testStructInstance;
            fs(ref objectStruct, NewStringValue);
            Assert.AreEqual(NewStringValue, ((TestStruct)objectStruct).GetPrivateField());
        }

        [TestMethod]
        public void FieldSet_ByExtensionAndReturnType_Private_FromStruct()
        {
            var fs = _testStructType.FieldSetStruct<string>("_privateField");
            Assert.IsNotNull(fs);
            var objectStruct = fs(_testStructInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, ((TestStruct)objectStruct).GetPrivateField());
        }

        [TestMethod]
        public void FieldSet_ByExtensionAndReturnType_Protected()
        {
            var fs = _testClassType.FieldSet<string>("ProtectedField");
            Assert.IsNotNull(fs);
            fs(_testClassInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, _testClassInstance.GetProtectedField());
        }

        [TestMethod]
        public void FieldSet_ByExtensionAndReturnType_Public()
        {
            var fs = _testClassType.FieldSet<string>("PublicField");
            Assert.IsNotNull(fs);
            fs(_testClassInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, _testClassInstance.PublicField);
        }

        [TestMethod]
        public void FieldSet_ByExtensionAndReturnType_Public_FromStruct()
        {
            var fs = _testStructType.FieldSetStruct<string>("PublicField");
            Assert.IsNotNull(fs);
            var objectStruct = fs(_testStructInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, ((TestStruct)objectStruct).PublicField);
        }

        [TestMethod]
        public void FieldSet_ByExtensionAndReturnType_Public_FromStructRef()
        {
            var fs = _testStructType.FieldSetStructRef<string>("PublicField");
            Assert.IsNotNull(fs);
            var objectStruct = (object)_testStructInstance;
            fs(ref objectStruct, NewStringValue);
            Assert.AreEqual(NewStringValue, ((TestStruct)objectStruct).PublicField);
        }

        [TestMethod]
        public void FieldSet_ByExtensionAndReturnType_Public_Struct()
        {
            var fs = _testClassType.FieldSet<int>("PublicFieldInt");
            Assert.IsNotNull(fs);
            fs(_testClassInstance, NewIntValue);
            Assert.AreEqual(NewIntValue, _testClassInstance.PublicFieldInt);
        }

        [TestMethod]
        public void FieldSet_ByExtensionAndReturnType_Public_Struct_FromStructRef()
        {
            var fs = _testStructType.FieldSetStructRef<int>("PublicFieldInt");
            Assert.IsNotNull(fs);
            var objectStruct = (object)_testStructInstance;
            fs(ref objectStruct, NewIntValue);
            Assert.AreEqual(NewIntValue, ((TestStruct)objectStruct).PublicFieldInt);
        }

        [TestMethod]
        public void FieldSet_ByExtensionAndReturnType_Public_Struct_FromStruct()
        {
            var fs = _testStructType.FieldSetStruct<int>("PublicFieldInt");
            Assert.IsNotNull(fs);
            var objectStruct = fs(_testStructInstance, NewIntValue);
            Assert.AreEqual(NewIntValue, ((TestStruct)objectStruct).PublicFieldInt);
        }

        [TestMethod]
        public void FieldSet_ByObjects_Internal()
        {
            var fs = _testClassType.FieldSet("InternalField");
            Assert.IsNotNull(fs);
            fs(_testClassInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, _testClassInstance.InternalField);
        }

        [TestMethod]
        public void FieldSet_ByObjects_Internal_FromStructRef()
        {
            var fs = _testStructType.FieldSetStructRef("InternalField");
            Assert.IsNotNull(fs);
            var objectStruct = (object)_testStructInstance;
            fs(ref objectStruct, NewStringValue);
            Assert.AreEqual(NewStringValue, ((TestStruct)objectStruct).InternalField);
        }

        [TestMethod]
        public void FieldSet_ByObjects_Internal_FromStruct()
        {
            var fs = _testStructType.FieldSetStruct("InternalField");
            Assert.IsNotNull(fs);
            var objectStruct = fs(_testStructInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, ((TestStruct)objectStruct).InternalField);
        }

        [TestMethod]
        public void FieldSet_ByObjects_NonExisting()
        {
            var fs = _testClassType.FieldSet("NonExisting");
            Assert.IsNull(fs);
        }

        [TestMethod]
        public void FieldSet_ByObjects_ReadOnly()
        {
            var fs = _testClassType.FieldSet("PublicReadOnlyField");
            Assert.IsNull(fs);
        }

        [TestMethod]
        public void FieldSet_ByObjects_Private()
        {
            var fs = _testClassType.FieldSet("_privateField");
            Assert.IsNotNull(fs);
            fs(_testClassInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, _testClassInstance.GetPrivateField());
        }

        [TestMethod]
        public void FieldSet_ByObjects_Private_FromStruct()
        {
            var fs = _testStructType.FieldSetStruct("_privateField");
            Assert.IsNotNull(fs);
            var objectStruct = fs(_testStructInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, ((TestStruct)objectStruct).GetPrivateField());
        }

        [TestMethod]
        public void FieldSet_ByObjects_Private_FromStructRef()
        {
            var fs = _testStructType.FieldSetStructRef("_privateField");
            Assert.IsNotNull(fs);
            var objectStruct = (object)_testStructInstance;
            fs(ref objectStruct, NewStringValue);
            Assert.AreEqual(NewStringValue, ((TestStruct)objectStruct).GetPrivateField());
        }

        [TestMethod]
        public void FieldSet_ByObjects_Protected()
        {
            var fs = _testClassType.FieldSet("ProtectedField");
            Assert.IsNotNull(fs);
            fs(_testClassInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, _testClassInstance.GetProtectedField());
        }

        [TestMethod]
        public void FieldSet_ByObjects_Public()
        {
            var fs = _testClassType.FieldSet("PublicField");
            Assert.IsNotNull(fs);
            fs(_testClassInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, _testClassInstance.PublicField);
        }

        [TestMethod]
        public void FieldSet_ByObjects_Public_FromStructRef()
        {
            var fs = _testStructType.FieldSetStructRef("PublicField");
            Assert.IsNotNull(fs);
            var objectStruct = (object)_testStructInstance;
            fs(ref objectStruct, NewStringValue);
            Assert.AreEqual(NewStringValue, ((TestStruct)objectStruct).PublicField);
        }

        [TestMethod]
        public void FieldSet_ByObjects_Public_FromStruct()
        {
            var fs = _testStructType.FieldSetStruct("PublicField");
            Assert.IsNotNull(fs);
            var objectStruct = fs(_testStructInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, ((TestStruct)objectStruct).PublicField);
        }

        [TestMethod]
        public void FieldSet_ByObjects_Public_Struct()
        {
            var fs = _testClassType.FieldSet("PublicFieldInt");
            Assert.IsNotNull(fs);
            fs(_testClassInstance, NewIntValue);
            Assert.AreEqual(NewIntValue, _testClassInstance.PublicFieldInt);
        }

        [TestMethod]
        public void FieldSet_ByObjects_Public_Struct_FromStructRef()
        {
            var fs = _testStructType.FieldSetStructRef("PublicFieldInt");
            Assert.IsNotNull(fs);
            var objectStruct = (object)_testStructInstance;
            fs(ref objectStruct, NewIntValue);
            Assert.AreEqual(NewIntValue, ((TestStruct)objectStruct).PublicFieldInt);
        }

        [TestMethod]
        public void FieldSet_ByObjects_Public_Struct_FromStruct()
        {
            var fs = _testStructType.FieldSetStruct("PublicFieldInt");
            Assert.IsNotNull(fs);
            var objectStruct = fs(_testStructInstance, NewIntValue);
            Assert.AreEqual(NewIntValue, ((TestStruct)objectStruct).PublicFieldInt);
        }

        [TestMethod]
        public void FieldSet_ByTypes_Internal()
        {
            var fs = DelegateFactory.FieldSet<TestClass, string>("InternalField");
            Assert.IsNotNull(fs);
            fs(_testClassInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, _testClassInstance.InternalField);
        }

        [TestMethod]
        public void FieldSet_ByTypes_Internal_FromStruct()
        {
            var fs = DelegateFactory.FieldSetStruct<TestStruct, string>("InternalField");
            Assert.IsNotNull(fs);
            fs(ref _testStructInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, _testStructInstance.InternalField);
        }

        [TestMethod]
        public void FieldSet_ByTypes_NonExisting()
        {
            var fs = DelegateFactory.FieldSet<TestClass, string>("NonExisting");
            Assert.IsNull(fs);
        }

        [TestMethod]
        public void FieldSet_ByTypes_ReadOnly()
        {
            var fs = DelegateFactory.FieldSet<TestClass, string>("PublicReadOnlyField");
            Assert.IsNull(fs);
        }

        [TestMethod]
        public void FieldSet_ByTypes_Private()
        {
            var fs = DelegateFactory.FieldSet<TestClass, string>("_privateField");
            Assert.IsNotNull(fs);
            fs(_testClassInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, _testClassInstance.GetPrivateField());
        }

        [TestMethod]
        public void FieldSet_ByTypes_Private_FromStruct()
        {
            var fs = DelegateFactory.FieldSetStruct<TestStruct, string>("_privateField");
            Assert.IsNotNull(fs);
            fs(ref _testStructInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, _testStructInstance.GetPrivateField());
        }

        [TestMethod]
        public void FieldSet_ByTypes_Protected()
        {
            var fs = DelegateFactory.FieldSet<TestClass, string>("ProtectedField");
            Assert.IsNotNull(fs);
            fs(_testClassInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, _testClassInstance.GetProtectedField());
        }

        [TestMethod]
        public void FieldSet_ByTypes_Public()
        {
            var fs = DelegateFactory.FieldSet<TestClass, string>("PublicField");
            Assert.IsNotNull(fs);
            fs(_testClassInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, _testClassInstance.PublicField);
        }

        [TestMethod]
        public void FieldSet_ByTypes_Public_FromStruct()
        {
            var fs = DelegateFactory.FieldSetStruct<TestStruct, string>("PublicField");
            Assert.IsNotNull(fs);
            fs(ref _testStructInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, _testStructInstance.PublicField);
        }

        [TestMethod]
        public void FieldSet_ByTypes_Public_Struct()
        {
            var fs = DelegateFactory.FieldSet<TestClass, int>("PublicFieldInt");
            Assert.IsNotNull(fs);
            fs(_testClassInstance, NewIntValue);
            Assert.AreEqual(NewIntValue, _testClassInstance.PublicFieldInt);
        }

        [TestMethod]
        public void FieldSet_ByTypes_Public_Struct_FromStruct()
        {
            var fs = DelegateFactory.FieldSetStruct<TestStruct, int>("PublicFieldInt");
            Assert.IsNotNull(fs);
            fs(ref _testStructInstance, NewIntValue);
            Assert.AreEqual(NewIntValue, _testStructInstance.PublicFieldInt);
        }
    }
#endif
}