// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DelegateFactoryTests_StaticField_GetValue.cs" company="Natan Podbielski">
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
    public class DelegateFactoryTests_StaticField_GetValue
    {
        [TestMethod]
        public void FieldGet_ByExtensionAndReturnType_NonExisting()
        {
            var sfg = DelegateFactory.StaticFieldGet<TestClass, string>("NonExisting");
            Assert.IsNull(sfg);
        }

        [TestMethod]
        public void FieldGet_ByExtensionAndReturnType_Internal()
        {
            var sfg = typeof(TestClass).StaticFieldGet<string>("StaticInternalField");
            Assert.IsNotNull(sfg);
            Assert.AreEqual(TestClass.StaticInternalField, sfg());
        }

        [TestMethod]
        public void FieldGet_ByExtensionAndReturnType_Internal_FromStruct()
        {
            var sfg = typeof(TestStruct).StaticFieldGet<string>("StaticInternalField");
            Assert.IsNotNull(sfg);
            Assert.AreEqual(TestStruct.StaticInternalField, sfg());
        }

        [TestMethod]
        public void FieldGet_ByExtensionAndReturnType_Private()
        {
            var sfg = typeof(TestClass).StaticFieldGet<string>("StaticPrivateField");
            Assert.IsNotNull(sfg);
            Assert.AreEqual(TestClass.GetStaticPrivateField(), sfg());
        }

        [TestMethod]
        public void FieldGet_ByExtensionAndReturnType_Private_FromStruct()
        {
            var sfg = typeof(TestStruct).StaticFieldGet<string>("_staticPrivateField");
            Assert.IsNotNull(sfg);
            Assert.AreEqual(TestStruct.GetStaticPrivateField(), sfg());
        }

        [TestMethod]
        public void FieldGet_ByExtensionAndReturnType_Protected()
        {
            var sfg = typeof(TestClass).StaticFieldGet<string>("StaticProtectedField");
            Assert.IsNotNull(sfg);
            Assert.AreEqual(TestClass.GetStaticProtectedField(), sfg());
        }

        [TestMethod]
        public void FieldGet_ByExtensionAndReturnType_Public_FromStruct()
        {
            var sfg = typeof(TestStruct).StaticFieldGet<string>("StaticPublicField");
            Assert.IsNotNull(sfg);
            Assert.AreEqual(TestStruct.StaticPublicField, sfg());
        }

        [TestMethod]
        public void FieldGet_ByExtensionAndReturnType_Public()
        {
            var sfg = typeof(TestClass).StaticFieldGet<string>("StaticPublicField");
            Assert.IsNotNull(sfg);
            Assert.AreEqual(TestClass.StaticPublicField, sfg());
        }

        [TestMethod]
        public void FieldGet_ByExtensionAndReturnType_Public_Struct()
        {
            var sfg = typeof(TestClass).StaticFieldGet<int>("StaticPublicValueField");
            Assert.IsNotNull(sfg);
            Assert.AreEqual(TestClass.StaticPublicValueField, sfg());
        }

        [TestMethod]
        public void FieldGet_ByExtensionAndReturnType_Public_Struct_FromStruct()
        {
            var sfg = typeof(TestStruct).StaticFieldGet<int>("StaticPublicFieldInt");
            Assert.IsNotNull(sfg);
            Assert.AreEqual(TestStruct.StaticPublicFieldInt, sfg());
        }

        [TestMethod]
        public void FieldGet_ByObjects_Internal()
        {
            var sfg = typeof(TestClass).StaticFieldGet("StaticInternalField");
            Assert.IsNotNull(sfg);
            Assert.AreEqual(TestClass.StaticInternalField, sfg());
        }

        [TestMethod]
        public void FieldGet_ByObjects_Internal_FromStruct()
        {
            var sfg = typeof(TestStruct).StaticFieldGet("StaticInternalField");
            Assert.IsNotNull(sfg);
            Assert.AreEqual(TestStruct.StaticInternalField, sfg());
        }

        [TestMethod]
        public void FieldGet_ByObjects_NonExisting()
        {
            var sfg = DelegateFactory.StaticFieldGet<TestClass, string>("NonExisting");
            Assert.IsNull(sfg);
        }

        [TestMethod]
        public void FieldGet_ByObjects_OnlyWrite()
        {
            var sfg = typeof(TestClass).StaticFieldGet("StaticOnlySetField");
            Assert.IsNull(sfg);
        }

        [TestMethod]
        public void FieldGet_ByObjects_Private()
        {
            var sfg = typeof(TestClass).StaticFieldGet("StaticPrivateField");
            Assert.IsNotNull(sfg);
            Assert.AreEqual(TestClass.GetStaticPrivateField(), sfg());
        }

        [TestMethod]
        public void FieldGet_ByObjects_Private_FromStruct()
        {
            var sfg = typeof(TestStruct).StaticFieldGet("_staticPrivateField");
            Assert.IsNotNull(sfg);
            Assert.AreEqual(TestStruct.GetStaticPrivateField(), sfg());
        }

        [TestMethod]
        public void FieldGet_ByObjects_Protected()
        {
            var sfg = typeof(TestClass).StaticFieldGet("StaticProtectedField");
            Assert.IsNotNull(sfg);
            Assert.AreEqual(TestClass.GetStaticProtectedField(), sfg());
        }

        [TestMethod]
        public void FieldGet_ByObjects_Public()
        {
            var sfg = typeof(TestClass).StaticFieldGet("StaticPublicField");
            Assert.IsNotNull(sfg);
            Assert.AreEqual(TestClass.StaticPublicField, sfg());
        }

        [TestMethod]
        public void FieldGet_ByObjects_Public_FromStruct()
        {
            var sfg = typeof(TestStruct).StaticFieldGet("StaticPublicField");
            Assert.IsNotNull(sfg);
            Assert.AreEqual(TestStruct.StaticPublicField, sfg());
        }

        [TestMethod]
        public void FieldGet_ByObjects_Public_Struct_FromStruct()
        {
            var sfg = typeof(TestStruct).StaticFieldGet("StaticPublicFieldInt");
            Assert.IsNotNull(sfg);
            Assert.AreEqual(TestStruct.StaticPublicFieldInt, sfg());
        }

        [TestMethod]
        public void FieldGet_ByTypes_Internal()
        {
            var sfg = DelegateFactory.StaticFieldGet<TestClass, string>("StaticInternalField");
            Assert.IsNotNull(sfg);
            Assert.AreEqual(TestClass.StaticInternalField, sfg());
        }

        [TestMethod]
        public void FieldGet_ByTypes_Internal_FromStruct()
        {
            var sfg = DelegateFactory.StaticFieldGet<TestStruct, string>("StaticInternalField");
            Assert.IsNotNull(sfg);
            Assert.AreEqual(TestStruct.StaticInternalField, sfg());
        }

        [TestMethod]
        public void FieldGet_ByTypes_NonExisting()
        {
            var sfg = DelegateFactory.StaticFieldGet<TestClass, string>("NonExisting");
            Assert.IsNull(sfg);
        }

        [TestMethod]
        public void FieldGet_ByTypes_OnlyWrite()
        {
            var sfg = DelegateFactory.StaticFieldGet<TestClass, string>("StaticOnlySetField");
            Assert.IsNull(sfg);
        }

        [TestMethod]
        public void FieldGet_ByTypes_Private()
        {
            var sfg = DelegateFactory.StaticFieldGet<TestClass, string>("StaticPrivateField");
            Assert.IsNotNull(sfg);
            Assert.AreEqual(TestClass.GetStaticPrivateField(), sfg());
        }


        [TestMethod]
        public void FieldGet_ByTypes_Private_FromStruct()
        {
            var sfg = DelegateFactory.StaticFieldGet<TestStruct, string>("StaticPublicField");
            Assert.IsNotNull(sfg);
            Assert.AreEqual(TestStruct.StaticPublicField, sfg());
        }

        [TestMethod]
        public void FieldGet_ByTypes_Protected()
        {
            var sfg = DelegateFactory.StaticFieldGet<TestClass, string>("StaticProtectedField");
            Assert.IsNotNull(sfg);
            Assert.AreEqual(TestClass.GetStaticProtectedField(), sfg());
        }

        [TestMethod]
        public void FieldGet_ByTypes_Public()
        {
            var sfg = DelegateFactory.StaticFieldGet<TestClass, string>("StaticPublicField");
            Assert.IsNotNull(sfg);
            Assert.AreEqual(TestClass.StaticPublicField, sfg());
        }

        [TestMethod]
        public void FieldGet_ByTypes_Public_FromStruct()
        {
            var sfg = DelegateFactory.StaticFieldGet<TestStruct, string>("StaticPublicField");
            Assert.IsNotNull(sfg);
            Assert.AreEqual(TestStruct.StaticPublicField, sfg());
        }

        [TestMethod]
        public void FieldGet_ByTypes_Public_Struct()
        {
            var sfg = DelegateFactory.StaticFieldGet<TestClass, int>("StaticPublicValueField");
            Assert.IsNotNull(sfg);
            Assert.AreEqual(TestClass.StaticPublicValueField, sfg());
        }

        [TestMethod]
        public void FieldGet_ByTypes_Public_Struct_FromStruct()
        {
            var sfg = DelegateFactory.StaticFieldGet<TestStruct, int>("StaticPublicFieldInt");
            Assert.IsNotNull(sfg);
            Assert.AreEqual(TestStruct.StaticPublicFieldInt, sfg());
        }
    }
}