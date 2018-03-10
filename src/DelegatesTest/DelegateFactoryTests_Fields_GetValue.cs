// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DelegateFactoryTests_Fields_GetValue.cs" company="Natan Podbielski">
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
    public class DelegateFactoryTests_Fields_GetValue
    {
        [TestMethod]
        public void FieldGet_ByExtensionAndReturnType_Internal()
        {
            var fg = typeof(TestClass).FieldGet<string>("InternalField");
            Assert.IsNotNull(fg);
            var testClassInstance = new TestClass();
            Assert.AreEqual(testClassInstance.InternalField, fg(testClassInstance));
        }

        [TestMethod]
        public void FieldGet_ByExtensionAndReturnType_Internal_FromStruct()
        {
            var fg = typeof(TestStruct).FieldGet<string>("InternalField");
            Assert.IsNotNull(fg);
            var testStructInstance = new TestStruct(0);
            Assert.AreEqual(testStructInstance.InternalField, fg(testStructInstance));
        }

        [TestMethod]
        public void FieldGet_ByExtensionAndReturnType_NonExisting()
        {
            var fg = DelegateFactory.FieldGet<TestClass, string>("NonExisting");
            Assert.IsNull(fg);
        }

        [TestMethod]
        public void FieldGet_ByExtensionAndReturnType_Private()
        {
            var fg = typeof(TestClass).FieldGet<string>("_privateField");
            Assert.IsNotNull(fg);
            var testClassInstance = new TestClass();
            Assert.AreEqual(testClassInstance.GetPrivateField(), fg(testClassInstance));
        }

        [TestMethod]
        public void FieldGet_ByExtensionAndReturnType_Private_FromStruct()
        {
            var fg = typeof(TestStruct).FieldGet<string>("_privateField");
            Assert.IsNotNull(fg);
            var testStructInstance = new TestStruct(0);
            Assert.AreEqual(testStructInstance.GetPrivateField(), fg(testStructInstance));
        }

        [TestMethod]
        public void FieldGet_ByExtensionAndReturnType_Protected()
        {
            var fg = typeof(TestClass).FieldGet<string>("ProtectedField");
            Assert.IsNotNull(fg);
            var testClassInstance = new TestClass();
            Assert.AreEqual(testClassInstance.GetProtectedField(), fg(testClassInstance));
        }

        [TestMethod]
        public void FieldGet_ByExtensionAndReturnType_Public()
        {
            var fg = typeof(TestClass).FieldGet<string>("PublicField");
            Assert.IsNotNull(fg);
            var testClassInstance = new TestClass();
            Assert.AreEqual(testClassInstance.PublicField, fg(testClassInstance));
        }

        [TestMethod]
        public void FieldGet_ByExtensionAndReturnType_Public_FromStruct()
        {
            var fg = typeof(TestStruct).FieldGet<string>("PublicField");
            Assert.IsNotNull(fg);
            var testStructInstance = new TestStruct(0);
            Assert.AreEqual(testStructInstance.PublicField, fg(testStructInstance));
        }

        [TestMethod]
        public void FieldGet_ByExtensionAndReturnType_Public_Struct()
        {
            var fg = typeof(TestClass).FieldGet<int>("PublicFieldInt");
            Assert.IsNotNull(fg);
            var testClassInstance = new TestClass();
            Assert.AreEqual(testClassInstance.PublicFieldInt, fg(testClassInstance));
        }

        [TestMethod]
        public void FieldGet_ByExtensionAndReturnType_Public_Struct_FromStruct()
        {
            var fg = typeof(TestStruct).FieldGet<int>("PublicFieldInt");
            Assert.IsNotNull(fg);
            var testStructInstance = new TestStruct(0);
            Assert.AreEqual(testStructInstance.PublicFieldInt, fg(testStructInstance));
        }

        [TestMethod]
        public void FieldGet_ByObjects_Internal()
        {
            var fg = typeof(TestClass).FieldGet("InternalField");
            Assert.IsNotNull(fg);
            var testClassInstance = new TestClass();
            Assert.AreEqual(testClassInstance.InternalField, fg(testClassInstance));
        }

        [TestMethod]
        public void FieldGet_ByObjects_Internal_FromStruct()
        {
            var fg = typeof(TestStruct).FieldGet("InternalField");
            Assert.IsNotNull(fg);
            var testStructInstance = new TestStruct(0);
            Assert.AreEqual(testStructInstance.InternalField, fg(testStructInstance));
        }

        [TestMethod]
        public void FieldGet_ByObjects_NonExisting()
        {
            var fg = DelegateFactory.FieldGet<TestClass, string>("NonExisting");
            Assert.IsNull(fg);
        }

        [TestMethod]
        public void FieldGet_ByObjects_Private()
        {
            var fg = typeof(TestClass).FieldGet("_privateField");
            Assert.IsNotNull(fg);
            var testClassInstance = new TestClass();
            Assert.AreEqual(testClassInstance.GetPrivateField(), fg(testClassInstance));
        }

        [TestMethod]
        public void FieldGet_ByObjects_Private_FromStruct()
        {
            var fg = typeof(TestStruct).FieldGet("_privateField");
            Assert.IsNotNull(fg);
            var testStructInstance = new TestStruct(0);
            Assert.AreEqual(testStructInstance.GetPrivateField(), fg(testStructInstance));
        }

        [TestMethod]
        public void FieldGet_ByObjects_Protected()
        {
            var fg = typeof(TestClass).FieldGet("ProtectedField");
            Assert.IsNotNull(fg);
            var testClassInstance = new TestClass();
            Assert.AreEqual(testClassInstance.GetProtectedField(), fg(testClassInstance));
        }

        [TestMethod]
        public void FieldGet_ByObjects_Public()
        {
            var fg = typeof(TestClass).FieldGet("PublicField");
            Assert.IsNotNull(fg);
            var testClassInstance = new TestClass();
            Assert.AreEqual(testClassInstance.PublicField, fg(testClassInstance));
        }

        [TestMethod]
        public void FieldGet_ByObjects_Public_FromStruct()
        {
            var fg = typeof(TestStruct).FieldGet("PublicField");
            Assert.IsNotNull(fg);
            var testStructInstance = new TestStruct(0);
            Assert.AreEqual(testStructInstance.PublicField, fg(testStructInstance));
        }

        [TestMethod]
        public void FieldGet_ByObjects_Public_Struct()
        {
            var fg = typeof(TestClass).FieldGet("PublicFieldInt");
            Assert.IsNotNull(fg);
            var testClassInstance = new TestClass();
            Assert.AreEqual(testClassInstance.PublicFieldInt, fg(testClassInstance));
        }

        [TestMethod]
        public void FieldGet_ByObjects_Public_Struct_FromStruct()
        {
            var fg = typeof(TestStruct).FieldGet("PublicFieldInt");
            Assert.IsNotNull(fg);
            var testStructInstance = new TestStruct(0);
            Assert.AreEqual(testStructInstance.PublicFieldInt, fg(testStructInstance));
        }

        [TestMethod]
        public void FieldGet_ByTypes_Internal()
        {
            var fg = DelegateFactory.FieldGet<TestClass, string>("InternalField");
            Assert.IsNotNull(fg);
            var testClassInstance = new TestClass();
            Assert.AreEqual(testClassInstance.InternalField, fg(testClassInstance));
        }

        [TestMethod]
        public void FieldGet_ByTypes_Internal_FromStruct()
        {
            var fg = DelegateFactory.FieldGet<TestStruct, string>("InternalField");
            Assert.IsNotNull(fg);
            var testStructInstance = new TestStruct(0);
            Assert.AreEqual(testStructInstance.InternalField, fg(testStructInstance));
        }

#if !NET35
        [TestMethod]
        public void FieldGet_ByTypes_Internal_FromStruct_ByRef()
        {
            var fg = DelegateFactory.FieldGetStruct<TestStruct, string>("InternalField");
            Assert.IsNotNull(fg);
            var testStructInstance = new TestStruct(0);
            Assert.AreEqual(testStructInstance.InternalField, fg(ref testStructInstance));
        }
#endif

        [TestMethod]
        public void FieldGet_ByTypes_NonExisting()
        {
            var fg = DelegateFactory.FieldGet<TestClass, string>("NonExisting");
            Assert.IsNull(fg);
        }

        [TestMethod]
        public void FieldGet_ByTypes_Private()
        {
            var fg = DelegateFactory.FieldGet<TestClass, string>("_privateField");
            Assert.IsNotNull(fg);
            var testClassInstance = new TestClass();
            Assert.AreEqual(testClassInstance.GetPrivateField(), fg(testClassInstance));
        }

        [TestMethod]
        public void FieldGet_ByTypes_Private_FromStruct()
        {
            var fg = DelegateFactory.FieldGet<TestStruct, string>("_privateField");
            Assert.IsNotNull(fg);
            var testStructInstance = new TestStruct(0);
            Assert.AreEqual(testStructInstance.GetPrivateField(), fg(testStructInstance));
        }

#if !NET35
        [TestMethod]
        public void FieldGet_ByTypes_Private_FromStruct_ByRef()
        {
            var fg = DelegateFactory.FieldGetStruct<TestStruct, string>("_privateField");
            Assert.IsNotNull(fg);
            var testStructInstance = new TestStruct(0);
            Assert.AreEqual(testStructInstance.GetPrivateField(), fg(ref testStructInstance));
        }
#endif

        [TestMethod]
        public void FieldGet_ByTypes_Protected()
        {
            var fg = DelegateFactory.FieldGet<TestClass, string>("ProtectedField");
            Assert.IsNotNull(fg);
            var testClassInstance = new TestClass();
            Assert.AreEqual(testClassInstance.GetProtectedField(), fg(testClassInstance));
        }

        [TestMethod]
        public void FieldGet_ByTypes_Public()
        {
            var fg = DelegateFactory.FieldGet<TestClass, string>("PublicField");
            Assert.IsNotNull(fg);
            var testClassInstance = new TestClass();
            Assert.AreEqual(testClassInstance.PublicField, fg(testClassInstance));
        }

        [TestMethod]
        public void FieldGet_ByTypes_Public_FromStruct()
        {
            var fg = DelegateFactory.FieldGet<TestStruct, string>("PublicField");
            Assert.IsNotNull(fg);
            var testStructInstance = new TestStruct(0);
            Assert.AreEqual(testStructInstance.PublicField, fg(testStructInstance));
        }

#if !NET35
        [TestMethod]
        public void FieldGet_ByTypes_Public_FromStruct_ByRef()
        {
            var fg = DelegateFactory.FieldGetStruct<TestStruct, string>("PublicField");
            Assert.IsNotNull(fg);
            var testStructInstance = new TestStruct(0);
            Assert.AreEqual(testStructInstance.PublicField, fg(ref testStructInstance));
        }
#endif

        [TestMethod]
        public void FieldGet_ByTypes_Public_Struct()
        {
            var fg = DelegateFactory.FieldGet<TestClass, int>("PublicFieldInt");
            Assert.IsNotNull(fg);
            var testClassInstance = new TestClass();
            Assert.AreEqual(testClassInstance.PublicFieldInt, fg(testClassInstance));
        }

        [TestMethod]
        public void FieldGet_ByTypes_Public_Struct_FromStruct()
        {
            var fg = DelegateFactory.FieldGet<TestStruct, int>("PublicFieldInt");
            Assert.IsNotNull(fg);
            var testStructInstance = new TestStruct(0);
            Assert.AreEqual(testStructInstance.PublicFieldInt, fg(testStructInstance));
        }

#if !NET35
        [TestMethod]
        public void FieldGet_ByTypes_Public_Struct_FromStruct_ByRef()
        {
            var fg = DelegateFactory.FieldGetStruct<TestStruct, int>("PublicFieldInt");
            Assert.IsNotNull(fg);
            var testStructInstance = new TestStruct(0);
            Assert.AreEqual(testStructInstance.PublicFieldInt, fg(ref testStructInstance));
        }
#endif
    }
}