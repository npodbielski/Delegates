// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DelegateFactoryTests_StaticField_SetValue.cs" company="Natan Podbielski">
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
    public class DelegateFactoryTests_StaticField_SetValue
    {
        private const int NewIntValue = 0;
        private const string NewStringValue = "Test";

        [TestMethod]
        public void FieldSet_ByExtensionAndReturnType_NonExisting()
        {
            var sfs = DelegateFactory.StaticFieldSet<TestClass, string>("NonExisting");
            Assert.IsNull(sfs);
        }

        [TestMethod]
        public void FieldSet_ByExtensionAndReturnType_ReadOnly()
        {
            var sfs = DelegateFactory.StaticFieldSet<TestClass, string>("StaticPublicReadOnlyField");
            Assert.IsNull(sfs);
        }

        [TestMethod]
        public void FieldSet_ByExtensionAndReturnType_Internal()
        {
            var sfs = typeof(TestClass).StaticFieldSet<string>("StaticInternalField");
            Assert.IsNotNull(sfs);
            sfs(NewStringValue);
            Assert.AreEqual(NewStringValue, TestClass.StaticInternalField);
        }

        [TestMethod]
        public void FieldSet_ByExtensionAndReturnType_Internal_FromStruct()
        {
            var sfs = typeof(TestStruct).StaticFieldSet<string>("StaticInternalField");
            Assert.IsNotNull(sfs);
            sfs(NewStringValue);
            Assert.AreEqual(NewStringValue, TestClass.StaticInternalField);
        }

        [TestMethod]
        public void FieldSet_ByExtensionAndReturnType_Private()
        {
            var sfs = typeof(TestClass).StaticFieldSet<string>("StaticPrivateField");
            Assert.IsNotNull(sfs);
            sfs(NewStringValue);
            Assert.AreEqual(NewStringValue, TestClass.GetStaticPrivateField());
        }

        [TestMethod]
        public void FieldSet_ByExtensionAndReturnType_Private_FromStruct()
        {
            var sfs = typeof(TestStruct).StaticFieldSet<string>("_staticPrivateField");
            Assert.IsNotNull(sfs);
            sfs(NewStringValue);
            Assert.AreEqual(NewStringValue, TestStruct.GetStaticPrivateField());
        }

        [TestMethod]
        public void FieldSet_ByExtensionAndReturnType_Protected()
        {
            var sfs = typeof(TestClass).StaticFieldSet<string>("StaticProtectedField");
            Assert.IsNotNull(sfs);
            sfs(NewStringValue);
            Assert.AreEqual(NewStringValue, TestClass.GetStaticProtectedField());
        }

        [TestMethod]
        public void FieldSet_ByExtensionAndReturnType_Public_FromStruct()
        {
            var sfs = typeof(TestStruct).StaticFieldSet<string>("StaticPublicField");
            Assert.IsNotNull(sfs);
            sfs(NewStringValue);
            Assert.AreEqual(NewStringValue, TestStruct.StaticPublicField);
        }

        [TestMethod]
        public void FieldSet_ByExtensionAndReturnType_Public()
        {
            var sfs = typeof(TestClass).StaticFieldSet<string>("StaticPublicField");
            Assert.IsNotNull(sfs);
            sfs(NewStringValue);
            Assert.AreEqual(NewStringValue, TestClass.StaticPublicField);
        }

        [TestMethod]
        public void FieldSet_ByExtensionAndReturnType_Public_Struct()
        {
            var sfs = typeof(TestClass).StaticFieldSet<int>("StaticPublicValueField");
            Assert.IsNotNull(sfs);
            sfs(NewIntValue);
            Assert.AreEqual(NewIntValue, TestClass.StaticPublicValueField);
        }

        [TestMethod]
        public void FieldSet_ByExtensionAndReturnType_Public_Struct_FromStruct()
        {
            var sfs = typeof(TestStruct).StaticFieldSet<int>("StaticPublicFieldInt");
            Assert.IsNotNull(sfs);
            sfs(NewIntValue);
            Assert.AreEqual(NewIntValue, TestStruct.StaticPublicFieldInt);
        }

        [TestMethod]
        public void FieldSet_ByObjects_Internal()
        {
            var sfs = typeof(TestClass).StaticFieldSet("StaticInternalField");
            Assert.IsNotNull(sfs);
            sfs(NewStringValue);
            Assert.AreEqual(NewStringValue, TestClass.StaticInternalField);
        }

        [TestMethod]
        public void FieldSet_ByObjects_Internal_FromStruct()
        {
            var sfs = typeof(TestStruct).StaticFieldSet("StaticInternalField");
            Assert.IsNotNull(sfs);
            sfs(NewStringValue);
            Assert.AreEqual(NewStringValue, TestStruct.StaticInternalField);
        }

        [TestMethod]
        public void FieldSet_ByObjects_NonExisting()
        {
            var sfs = DelegateFactory.StaticFieldSet<TestClass, string>("NonExisting");
            Assert.IsNull(sfs);
        }

        [TestMethod]
        public void FieldSet_ByObjects_Private()
        {
            var sfs = typeof(TestClass).StaticFieldSet("StaticPrivateField");
            Assert.IsNotNull(sfs);
            sfs(NewStringValue);
            Assert.AreEqual(NewStringValue, TestClass.GetStaticPrivateField());
        }

        [TestMethod]
        public void FieldSet_ByObjects_Private_FromStruct()
        {
            var sfs = typeof(TestStruct).StaticFieldSet("_staticPrivateField");
            Assert.IsNotNull(sfs);
            sfs(NewStringValue);
            Assert.AreEqual(NewStringValue, TestStruct.GetStaticPrivateField());
        }

        [TestMethod]
        public void FieldSet_ByObjects_Protected()
        {
            var sfs = typeof(TestClass).StaticFieldSet("StaticProtectedField");
            Assert.IsNotNull(sfs);
            sfs(NewStringValue);
            Assert.AreEqual(NewStringValue, TestClass.GetStaticProtectedField());
        }

        [TestMethod]
        public void FieldSet_ByObjects_Public()
        {
            var sfs = typeof(TestClass).StaticFieldSet("StaticPublicField");
            Assert.IsNotNull(sfs);
            sfs(NewStringValue);
            Assert.AreEqual(NewStringValue, TestClass.StaticPublicField);
        }

        [TestMethod]
        public void FieldSet_ByObjects_Public_FromStruct()
        {
            var sfs = typeof(TestStruct).StaticFieldSet("StaticPublicField");
            Assert.IsNotNull(sfs);
            sfs(NewStringValue);
            Assert.AreEqual(NewStringValue, TestStruct.StaticPublicField);
        }

        [TestMethod]
        public void FieldSet_ByObjects_Public_Struct_FromStruct()
        {
            var sfs = typeof(TestStruct).StaticFieldSet("StaticPublicFieldInt");
            Assert.IsNotNull(sfs);
            sfs(NewIntValue);
            Assert.AreEqual(NewIntValue, TestStruct.StaticPublicFieldInt);
        }

        [TestMethod]
        public void FieldSet_ByTypes_Internal()
        {
            var sfs = DelegateFactory.StaticFieldSet<TestClass, string>("StaticInternalField");
            Assert.IsNotNull(sfs);
            sfs(NewStringValue);
            Assert.AreEqual(NewStringValue, TestClass.StaticInternalField);
        }

        [TestMethod]
        public void FieldSet_ByTypes_Internal_FromStruct()
        {
            var sfs = DelegateFactory.StaticFieldSet<TestStruct, string>("StaticInternalField");
            Assert.IsNotNull(sfs);
            sfs(NewStringValue);
            Assert.AreEqual(NewStringValue, TestStruct.StaticInternalField);
        }

        [TestMethod]
        public void FieldSet_ByTypes_NonExisting()
        {
            var sfs = DelegateFactory.StaticFieldSet<TestClass, string>("NonExisting");
            Assert.IsNull(sfs);
        }

        [TestMethod]
        public void FieldSet_ByTypes_Private()
        {
            var sfs = DelegateFactory.StaticFieldSet<TestClass, string>("StaticPrivateField");
            Assert.IsNotNull(sfs);
            sfs(NewStringValue);
            Assert.AreEqual(NewStringValue, TestClass.GetStaticPrivateField());
        }


        [TestMethod]
        public void FieldSet_ByTypes_Private_FromStruct()
        {
            var sfs = DelegateFactory.StaticFieldSet<TestStruct, string>("StaticPublicField");
            Assert.IsNotNull(sfs);
            sfs(NewStringValue);
            Assert.AreEqual(NewStringValue, TestStruct.StaticPublicField);
        }

        [TestMethod]
        public void FieldSet_ByTypes_Protected()
        {
            var sfs = DelegateFactory.StaticFieldSet<TestClass, string>("StaticProtectedField");
            Assert.IsNotNull(sfs);
            sfs(NewStringValue);
            Assert.AreEqual(NewStringValue, TestClass.GetStaticProtectedField());
        }

        [TestMethod]
        public void FieldSet_ByTypes_Public()
        {
            var sfs = DelegateFactory.StaticFieldSet<TestClass, string>("StaticPublicField");
            Assert.IsNotNull(sfs);
            sfs(NewStringValue);
            Assert.AreEqual(NewStringValue, TestClass.StaticPublicField);
        }

        [TestMethod]
        public void FieldSet_ByTypes_Public_FromStruct()
        {
            var sfs = DelegateFactory.StaticFieldSet<TestStruct, string>("StaticPublicField");
            Assert.IsNotNull(sfs);
            sfs(NewStringValue);
            Assert.AreEqual(NewStringValue, TestStruct.StaticPublicField);
        }

        [TestMethod]
        public void FieldSet_ByTypes_Public_Struct()
        {
            var sfs = DelegateFactory.StaticFieldSet<TestClass, int>("StaticPublicValueField");
            Assert.IsNotNull(sfs);
            sfs(NewIntValue);
            Assert.AreEqual(NewIntValue, TestClass.StaticPublicValueField);
        }

        [TestMethod]
        public void FieldSet_ByTypes_Public_Struct_FromStruct()
        {
            var sfs = DelegateFactory.StaticFieldSet<TestStruct, int>("StaticPublicFieldInt");
            Assert.IsNotNull(sfs);
            sfs(NewIntValue);
            Assert.AreEqual(NewIntValue, TestStruct.StaticPublicFieldInt);
        }
    }
}