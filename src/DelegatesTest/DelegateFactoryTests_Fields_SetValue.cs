// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DelegateFactoryTests_Fields_SetValue.cs" company="Natan Podbielski">
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
    public class DelegateFactoryTests_Fields_SetValue
    {
        private const int NewIntValue = 0;
        private const string NewStringValue = "Test";
        
        [TestMethod]
        public void FieldSet_ByExtensionAndReturnType_Internal()
        {
            var fs = typeof(TestClass).FieldSet<string>("InternalField");
            Assert.IsNotNull(fs);
            var testClassInstance = new TestClass();
            fs(testClassInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, testClassInstance.InternalField);
        }

        [TestMethod]
        public void FieldSet_ByExtensionAndReturnType_Internal_FromStructRef()
        {
            var fs = typeof(TestStruct).FieldSetStructRef<string>("InternalField");
            Assert.IsNotNull(fs);
            var objectStruct = (object)new TestStruct(0);
            fs(ref objectStruct, NewStringValue);
            Assert.AreEqual(NewStringValue, ((TestStruct)objectStruct).InternalField);
        }

        [TestMethod]
        public void FieldSet_ByExtensionAndReturnType_Internal_FromStruct()
        {
            var fs = typeof(TestStruct).FieldSetStruct<string>("InternalField");
            Assert.IsNotNull(fs);
            var objectStruct = fs(new TestStruct(0), NewStringValue);
            Assert.AreEqual(NewStringValue, ((TestStruct)objectStruct).InternalField);
        }

        [TestMethod]
        public void FieldSet_ByExtensionAndReturnType_NonExisting()
        {
            var fs = typeof(TestClass).FieldSet<string>("NonExisting");
            Assert.IsNull(fs);
        }

        [TestMethod]
        public void FieldSet_ByExtensionAndReturnType_ReadOnly()
        {
            var fs = typeof(TestClass).FieldSet<string>("PublicReadOnlyField");
            Assert.IsNull(fs);
        }

        [TestMethod]
        public void FieldSet_ByExtensionAndReturnType_Private()
        {
            var fs = typeof(TestClass).FieldSet<string>("_privateField");
            Assert.IsNotNull(fs);
            var testClassInstance = new TestClass();
            fs(testClassInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, testClassInstance.GetPrivateField());
        }

        [TestMethod]
        public void FieldSet_ByExtensionAndReturnType_Private_FromStructRef()
        {
            var fs = typeof(TestStruct).FieldSetStructRef<string>("_privateField");
            Assert.IsNotNull(fs);
            var objectStruct = (object)new TestStruct(0);
            fs(ref objectStruct, NewStringValue);
            Assert.AreEqual(NewStringValue, ((TestStruct)objectStruct).GetPrivateField());
        }

        [TestMethod]
        public void FieldSet_ByExtensionAndReturnType_Private_FromStruct()
        {
            var fs = typeof(TestStruct).FieldSetStruct<string>("_privateField");
            Assert.IsNotNull(fs);
            var objectStruct = fs(new TestStruct(0), NewStringValue);
            Assert.AreEqual(NewStringValue, ((TestStruct)objectStruct).GetPrivateField());
        }

        [TestMethod]
        public void FieldSet_ByExtensionAndReturnType_Protected()
        {
            var fs = typeof(TestClass).FieldSet<string>("ProtectedField");
            Assert.IsNotNull(fs);
            var testClassInstance = new TestClass();
            fs(testClassInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, testClassInstance.GetProtectedField());
        }

        [TestMethod]
        public void FieldSet_ByExtensionAndReturnType_Public()
        {
            var fs = typeof(TestClass).FieldSet<string>("PublicField");
            Assert.IsNotNull(fs);
            var testClassInstance = new TestClass();
            fs(testClassInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, testClassInstance.PublicField);
        }

        [TestMethod]
        public void FieldSet_ByExtensionAndReturnType_Public_FromStruct()
        {
            var fs = typeof(TestStruct).FieldSetStruct<string>("PublicField");
            Assert.IsNotNull(fs);
            var objectStruct = fs(new TestStruct(0), NewStringValue);
            Assert.AreEqual(NewStringValue, ((TestStruct)objectStruct).PublicField);
        }

        [TestMethod]
        public void FieldSet_ByExtensionAndReturnType_Public_FromStructRef()
        {
            var fs = typeof(TestStruct).FieldSetStructRef<string>("PublicField");
            Assert.IsNotNull(fs);
            var objectStruct = (object)new TestStruct(0);
            fs(ref objectStruct, NewStringValue);
            Assert.AreEqual(NewStringValue, ((TestStruct)objectStruct).PublicField);
        }

        [TestMethod]
        public void FieldSet_ByExtensionAndReturnType_Public_Struct()
        {
            var fs = typeof(TestClass).FieldSet<int>("PublicFieldInt");
            Assert.IsNotNull(fs);
            var testClassInstance = new TestClass();
            fs(testClassInstance, NewIntValue);
            Assert.AreEqual(NewIntValue, testClassInstance.PublicFieldInt);
        }

        [TestMethod]
        public void FieldSet_ByExtensionAndReturnType_Public_Struct_FromStructRef()
        {
            var fs = typeof(TestStruct).FieldSetStructRef<int>("PublicFieldInt");
            Assert.IsNotNull(fs);
            var objectStruct = (object)new TestStruct(0);
            fs(ref objectStruct, NewIntValue);
            Assert.AreEqual(NewIntValue, ((TestStruct)objectStruct).PublicFieldInt);
        }

        [TestMethod]
        public void FieldSet_ByExtensionAndReturnType_Public_Struct_FromStruct()
        {
            var fs = typeof(TestStruct).FieldSetStruct<int>("PublicFieldInt");
            Assert.IsNotNull(fs);
            var objectStruct = fs(new TestStruct(0), NewIntValue);
            Assert.AreEqual(NewIntValue, ((TestStruct)objectStruct).PublicFieldInt);
        }

        [TestMethod]
        public void FieldSet_ByObjects_Internal()
        {
            var fs = typeof(TestClass).FieldSet("InternalField");
            Assert.IsNotNull(fs);
            var testClassInstance = new TestClass();
            fs(testClassInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, testClassInstance.InternalField);
        }

        [TestMethod]
        public void FieldSet_ByObjects_Internal_FromStructRef()
        {
            var fs = typeof(TestStruct).FieldSetStructRef("InternalField");
            Assert.IsNotNull(fs);
            var objectStruct = (object)new TestStruct(0);
            fs(ref objectStruct, NewStringValue);
            Assert.AreEqual(NewStringValue, ((TestStruct)objectStruct).InternalField);
        }

        [TestMethod]
        public void FieldSet_ByObjects_Internal_FromStruct()
        {
            var fs = typeof(TestStruct).FieldSetStruct("InternalField");
            Assert.IsNotNull(fs);
            var objectStruct = fs(new TestStruct(0), NewStringValue);
            Assert.AreEqual(NewStringValue, ((TestStruct)objectStruct).InternalField);
        }

        [TestMethod]
        public void FieldSet_ByObjects_NonExisting()
        {
            var fs = typeof(TestClass).FieldSet("NonExisting");
            Assert.IsNull(fs);
        }

        [TestMethod]
        public void FieldSet_ByObjects_ReadOnly()
        {
            var fs = typeof(TestClass).FieldSet("PublicReadOnlyField");
            Assert.IsNull(fs);
        }

        [TestMethod]
        public void FieldSet_ByObjects_Private()
        {
            var fs = typeof(TestClass).FieldSet("_privateField");
            Assert.IsNotNull(fs);
            var testClassInstance = new TestClass();
            fs(testClassInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, testClassInstance.GetPrivateField());
        }

        [TestMethod]
        public void FieldSet_ByObjects_Private_FromStruct()
        {
            var fs = typeof(TestStruct).FieldSetStruct("_privateField");
            Assert.IsNotNull(fs);
            var objectStruct = fs(new TestStruct(0), NewStringValue);
            Assert.AreEqual(NewStringValue, ((TestStruct)objectStruct).GetPrivateField());
        }

        [TestMethod]
        public void FieldSet_ByObjects_Private_FromStructRef()
        {
            var fs = typeof(TestStruct).FieldSetStructRef("_privateField");
            Assert.IsNotNull(fs);
            var objectStruct = (object)new TestStruct(0);
            fs(ref objectStruct, NewStringValue);
            Assert.AreEqual(NewStringValue, ((TestStruct)objectStruct).GetPrivateField());
        }

        [TestMethod]
        public void FieldSet_ByObjects_Protected()
        {
            var fs = typeof(TestClass).FieldSet("ProtectedField");
            Assert.IsNotNull(fs);
            var testClassInstance = new TestClass();
            fs(testClassInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, testClassInstance.GetProtectedField());
        }

        [TestMethod]
        public void FieldSet_ByObjects_Public()
        {
            var fs = typeof(TestClass).FieldSet("PublicField");
            Assert.IsNotNull(fs);
            var testClassInstance = new TestClass();
            fs(testClassInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, testClassInstance.PublicField);
        }

        [TestMethod]
        public void FieldSet_ByObjects_Public_FromStructRef()
        {
            var fs = typeof(TestStruct).FieldSetStructRef("PublicField");
            Assert.IsNotNull(fs);
            var objectStruct = (object)new TestStruct(0);
            fs(ref objectStruct, NewStringValue);
            Assert.AreEqual(NewStringValue, ((TestStruct)objectStruct).PublicField);
        }

        [TestMethod]
        public void FieldSet_ByObjects_Public_FromStruct()
        {
            var fs = typeof(TestStruct).FieldSetStruct("PublicField");
            Assert.IsNotNull(fs);
            var objectStruct = fs(new TestStruct(0), NewStringValue);
            Assert.AreEqual(NewStringValue, ((TestStruct)objectStruct).PublicField);
        }

        [TestMethod]
        public void FieldSet_ByObjects_Public_Struct()
        {
            var fs = typeof(TestClass).FieldSet("PublicFieldInt");
            Assert.IsNotNull(fs);
            var testClassInstance = new TestClass();
            fs(testClassInstance, NewIntValue);
            Assert.AreEqual(NewIntValue, testClassInstance.PublicFieldInt);
        }

        [TestMethod]
        public void FieldSet_ByObjects_Public_Struct_FromStructRef()
        {
            var fs = typeof(TestStruct).FieldSetStructRef("PublicFieldInt");
            Assert.IsNotNull(fs);
            var objectStruct = (object)new TestStruct(0);
            fs(ref objectStruct, NewIntValue);
            Assert.AreEqual(NewIntValue, ((TestStruct)objectStruct).PublicFieldInt);
        }

        [TestMethod]
        public void FieldSet_ByObjects_Public_Struct_FromStruct()
        {
            var fs = typeof(TestStruct).FieldSetStruct("PublicFieldInt");
            Assert.IsNotNull(fs);
            var objectStruct = fs(new TestStruct(0), NewIntValue);
            Assert.AreEqual(NewIntValue, ((TestStruct)objectStruct).PublicFieldInt);
        }

        [TestMethod]
        public void FieldSet_ByTypes_Internal()
        {
            var fs = DelegateFactory.FieldSet<TestClass, string>("InternalField");
            Assert.IsNotNull(fs);
            var testClassInstance = new TestClass();
            fs(testClassInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, testClassInstance.InternalField);
        }

        [TestMethod]
        public void FieldSet_ByTypes_Internal_FromStruct()
        {
            var fs = DelegateFactory.FieldSetStruct<TestStruct, string>("InternalField");
            Assert.IsNotNull(fs);
            var testStructInstance = new TestStruct(0);
            fs(ref testStructInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, testStructInstance.InternalField);
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
            var testClassInstance = new TestClass();
            fs(testClassInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, testClassInstance.GetPrivateField());
        }

        [TestMethod]
        public void FieldSet_ByTypes_Private_FromStruct()
        {
            var fs = DelegateFactory.FieldSetStruct<TestStruct, string>("_privateField");
            Assert.IsNotNull(fs);
            var testStructInstance = new TestStruct(0);
            fs(ref testStructInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, testStructInstance.GetPrivateField());
        }

        [TestMethod]
        public void FieldSet_ByTypes_Protected()
        {
            var fs = DelegateFactory.FieldSet<TestClass, string>("ProtectedField");
            Assert.IsNotNull(fs);
            var testClassInstance = new TestClass();
            fs(testClassInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, testClassInstance.GetProtectedField());
        }

        [TestMethod]
        public void FieldSet_ByTypes_Public()
        {
            var fs = DelegateFactory.FieldSet<TestClass, string>("PublicField");
            Assert.IsNotNull(fs);
            var testClassInstance = new TestClass();
            fs(testClassInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, testClassInstance.PublicField);
        }

        [TestMethod]
        public void FieldSet_ByTypes_Public_FromStruct()
        {
            var fs = DelegateFactory.FieldSetStruct<TestStruct, string>("PublicField");
            Assert.IsNotNull(fs);
            var testStructInstance = new TestStruct(0);
            fs(ref testStructInstance, NewStringValue);
            Assert.AreEqual(NewStringValue, testStructInstance.PublicField);
        }

        [TestMethod]
        public void FieldSet_ByTypes_Public_Struct()
        {
            var fs = DelegateFactory.FieldSet<TestClass, int>("PublicFieldInt");
            Assert.IsNotNull(fs);
            var testClassInstance = new TestClass();
            fs(testClassInstance, NewIntValue);
            Assert.AreEqual(NewIntValue, testClassInstance.PublicFieldInt);
        }

        [TestMethod]
        public void FieldSet_ByTypes_Public_Struct_FromStruct()
        {
            var fs = DelegateFactory.FieldSetStruct<TestStruct, int>("PublicFieldInt");
            Assert.IsNotNull(fs);
            var testStructInstance = new TestStruct(0);
            fs(ref testStructInstance, NewIntValue);
            Assert.AreEqual(NewIntValue, testStructInstance.PublicFieldInt);
        }
    }
}