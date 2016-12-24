// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DelegateFactoryTests_Indexers_Getters.cs" company="Natan Podbielski">
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
    public class DelegateFactoryTests_Indexers_Getters
    {
        private const string FirstStringIndex = "test";
        private const int FirstIntIndex = 0;
        private const long FirstLongIndex = 0;
        private const byte FirstByteIndex = 0;
        private const int SecondIntIndex = 1;
        private readonly TestClass _testClassInstance = new TestClass();
        private readonly Type _testClassType = typeof(TestClass);
        private readonly Type _testStrucType = typeof(TestStruct);
        private string _secondStringIndex = "test2";
        private TestStruct _testStructInstance = new TestStruct(0);

        [TestMethod]
        public void IndexerGet_ByExtensionAndReturnType_NonExisting()
        {
            var ig = DelegateFactory.IndexerGet<TestClass, ulong, ulong>();
            Assert.IsNull(ig);
        }

        [TestMethod]
        public void IndexerGet_ByExtensionAndReturnType_OnlyWrite()
        {
            var ig = _testClassType.IndexerGet<string, string, string>();
            Assert.IsNull(ig);
        }

        [TestMethod]
        public void IndexerGet_ByExtensionAndReturnType_OnlyWrite_FromStruct()
        {
            var ig = _testStrucType.IndexerGet<string, string, string>();
            Assert.IsNull(ig);
        }

        [TestMethod]
        public void IndexerGet_ByExtensionAndReturnType_Internal()
        {
            var ig = _testClassType.IndexerGet<string, string>();
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstStringIndex, ig(_testClassInstance, FirstStringIndex));
        }

        [TestMethod]
        public void IndexerGet_ByExtensionAndReturnType_Internal_FromStruct()
        {
            var ig = _testStrucType.IndexerGet<string, string>();
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstStringIndex, ig(_testStructInstance, FirstStringIndex));
        }

        [TestMethod]
        public void IndexerGet_ByExtensionAndReturnType_Private()
        {
            var ig = _testClassType.IndexerGet<int, int, int>();
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstIntIndex, ig(_testClassInstance, FirstIntIndex, SecondIntIndex));
        }

        [TestMethod]
        public void IndexerGet_ByExtensionAndReturnType_Private_FromStruct()
        {
            var ig = _testStrucType.IndexerGet<int, int, int>();
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstIntIndex, ig(_testStructInstance, FirstIntIndex, SecondIntIndex));
        }

        [TestMethod]
        public void IndexerGet_ByExtensionAndReturnType_Protected()
        {
            var ig = _testClassType.IndexerGet<byte, byte>();
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstByteIndex, ig(_testClassInstance, FirstByteIndex));
        }

        [TestMethod]
        public void IndexerGet_ByExtensionAndReturnType_Public()
        {
            var ig = _testClassType.IndexerGet<int, int>();
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstIntIndex, ig(_testClassInstance, FirstIntIndex));
        }

        [TestMethod]
        public void IndexerGet_ByExtensionAndReturnType_Public_FromStruct()
        {
            var ig = _testStrucType.IndexerGet<int, int>();
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstIntIndex, ig(_testStructInstance, FirstIntIndex));
        }

        [TestMethod]
        public void IndexerGet_ByObjects_Internal()
        {
            var ig = _testClassType.IndexerGet(typeof(string), typeof(string));
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstStringIndex, ig(_testClassInstance, FirstStringIndex));
        }

        [TestMethod]
        public void IndexerGet_ByObjects_Internal_FromStruct()
        {
            var ig = _testStrucType.IndexerGet(typeof(string), typeof(string));
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstStringIndex, ig(_testStructInstance, FirstStringIndex));
        }

        [TestMethod]
        public void IndexerGet_ByObjects_NonExisting()
        {
            var ig = _testClassType.IndexerGet(typeof(ulong), typeof(ulong));
            Assert.IsNull(ig);
        }

        [TestMethod]
        public void IndexerGet_ByObjects_OnlyWrite()
        {
            var ig = _testClassType.IndexerGet(typeof(string), typeof(string), typeof(string));
            Assert.IsNull(ig);
        }

        [TestMethod]
        public void IndexerGet_ByObjects_Private()
        {
            var ig = _testClassType.IndexerGet(typeof(long), typeof(long));
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstLongIndex, ig(_testClassInstance, FirstLongIndex));
        }

        [TestMethod]
        public void IndexerGet_ByObjects_Private_FromStruct()
        {
            var ig = _testStrucType.IndexerGet(typeof(long), typeof(long));
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstLongIndex, ig(_testStructInstance, FirstLongIndex));
        }

        [TestMethod]
        public void IndexerGet_ByObjects_Protected()
        {
            var ig = _testClassType.IndexerGet(typeof(byte), typeof(byte));
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstByteIndex, ig(_testClassInstance, FirstByteIndex));
        }

        [TestMethod]
        public void IndexerGet_ByObjects_Public()
        {
            var ig = _testClassType.IndexerGet(typeof(int), typeof(int));
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstIntIndex, ig(_testClassInstance, FirstIntIndex));
        }


        [TestMethod]
        public void IndexerGet_ByObjects_Public_FromStruct()
        {
            var ig = _testStrucType.IndexerGet(typeof(int), typeof(int));
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstIntIndex, ig(_testStructInstance, FirstIntIndex));
        }

        [TestMethod]
        public void IndexerGet_ByTypes_Internal()
        {
            var ig = DelegateFactory.IndexerGet<TestClass, string, string>();
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstStringIndex, ig(_testClassInstance, FirstStringIndex));
        }

        [TestMethod]
        public void IndexerGet_ByTypes_Internal_FromStruct()
        {
            var ig = DelegateFactory.IndexerGetStruct<TestStruct, string, string>();
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstStringIndex, ig(ref _testStructInstance, FirstStringIndex));
        }

        [TestMethod]
        public void IndexerGet_ByTypes_NonExisting()
        {
            var ig = DelegateFactory.IndexerGet<TestClass, ulong, ulong>();
            Assert.IsNull(ig);
        }

        [TestMethod]
        public void IndexerGet_ByTypes_OnlyWrite()
        {
            var ig = DelegateFactory.IndexerGet<TestClass, string, string, string>();
            Assert.IsNull(ig);
        }

        [TestMethod]
        public void IndexerGet_ByTypes_Private()
        {
            var ig = DelegateFactory.IndexerGet<TestClass, long, long>();
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstLongIndex, ig(_testClassInstance, FirstLongIndex));
        }

        [TestMethod]
        public void IndexerGet_ByTypes_Private_FromStruct()
        {
            var ig = DelegateFactory.IndexerGetStruct<TestStruct, long, long>();
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstLongIndex, ig(ref _testStructInstance, FirstLongIndex));
        }

        [TestMethod]
        public void IndexerGet_ByTypes_Protected()
        {
            var ig = DelegateFactory.IndexerGet<TestClass, byte, byte>();
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstByteIndex, ig(_testClassInstance, FirstByteIndex));
        }

        [TestMethod]
        public void IndexerGet_ByTypes_Public()
        {
            var ig = DelegateFactory.IndexerGet<TestClass, int, int>();
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstIntIndex, ig(_testClassInstance, FirstIntIndex));
        }

        [TestMethod]
        public void IndexerGet_ByTypes_Public_FromStruct()
        {
            var ig = DelegateFactory.IndexerGetStruct<TestStruct, int, int>();
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstIntIndex, ig(ref _testStructInstance, FirstIntIndex));
        }

        [TestMethod]
        public void IndexerGet_ByTypes_1Index()
        {
            var ig = DelegateFactory.IndexerGet<TestClass, int, int>();
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstIntIndex, ig(_testClassInstance, FirstIntIndex));
        }

        [TestMethod]
        public void IndexerGet_ByTypes_1Index_FromStruct()
        {
            var ig = DelegateFactory.IndexerGetStruct<TestStruct, int, int>();
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstIntIndex, ig(ref _testStructInstance, FirstIntIndex));
        }

        [TestMethod]
        public void IndexerGet_ByTypes_2Index()
        {
            var ig = DelegateFactory.IndexerGet<TestClass, int, int, int>();
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstIntIndex, ig(_testClassInstance, FirstIntIndex, SecondIntIndex));
        }

        [TestMethod]
        public void IndexerGet_ByTypes_2Index_FromStruct()
        {
            var ig = DelegateFactory.IndexerGetStruct<TestStruct, int, int, int>();
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstIntIndex, ig(ref _testStructInstance, FirstIntIndex, SecondIntIndex));
        }

        [TestMethod]
        public void IndexerGet_ByTypes_3Index()
        {
            var ig = DelegateFactory.IndexerGet<TestClass, int, int, int, int>();
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstIntIndex, ig(_testClassInstance, FirstIntIndex, SecondIntIndex, 0));
        }

        [TestMethod]
        public void IndexerGet_ByTypes_3Index_FromStruct()
        {
            var ig = DelegateFactory.IndexerGetStruct<TestStruct, int, int, int, int>();
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstIntIndex, ig(ref _testStructInstance, FirstIntIndex, SecondIntIndex, 0));
        }


        [TestMethod]
        public void IndexerGet_ByExtensionAndReturnType_1Index()
        {
            var ig = _testClassType.IndexerGet<int, int>();
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstIntIndex, ig(_testClassInstance, FirstIntIndex));
        }

        [TestMethod]
        public void IndexerGet_ByExtensionAndReturnType_1Index_FromStruct()
        {
            var ig = _testStrucType.IndexerGet<int, int>();
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstIntIndex, ig(_testStructInstance, FirstIntIndex));
        }

        [TestMethod]
        public void IndexerGet_ByExtensionAndReturnType_2Index()
        {
            var ig = _testClassType.IndexerGet<int, int, int>();
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstIntIndex, ig(_testClassInstance, FirstIntIndex, SecondIntIndex));
        }

        [TestMethod]
        public void IndexerGet_ByExtensionAndReturnType_2Index_FromStruct()
        {
            var ig = _testStrucType.IndexerGet<int, int, int>();
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstIntIndex, ig(_testStructInstance, FirstIntIndex, SecondIntIndex));
        }

        [TestMethod]
        public void IndexerGet_ByExtensionAndReturnType_3Index()
        {
            var ig = _testClassType.IndexerGet<int, int, int, int>();
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstIntIndex, ig(_testClassInstance, FirstIntIndex, SecondIntIndex, 0));
        }

        [TestMethod]
        public void IndexerGet_ByExtensionAndReturnType_3Index_FromStruct()
        {
            var ig = _testStrucType.IndexerGet<int, int, int, int>();
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstIntIndex, ig(_testStructInstance, FirstIntIndex, SecondIntIndex, 0));
        }

        [TestMethod]
        public void IndexerGet_ByObjects_1Index()
        {
            var ig = _testClassType.IndexerGet(typeof(int), typeof(int));
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstIntIndex, ig(_testClassInstance, FirstIntIndex));
        }

        [TestMethod]
        public void IndexerGet_ByObjects_1Index_FromStruct()
        {
            var ig = _testStrucType.IndexerGet(typeof(int), typeof(int));
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstIntIndex, ig(_testStructInstance, FirstIntIndex));
        }

        [TestMethod]
        public void IndexerGet_ByObjects_2Index()
        {
            var ig = _testClassType.IndexerGet(typeof(int), typeof(int), typeof(int));
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstIntIndex, ig(_testClassInstance, new object[] {FirstIntIndex, SecondIntIndex}));
        }

        [TestMethod]
        public void IndexerGet_ByObjects_2Index_FromStruct()
        {
            var ig = _testStrucType.IndexerGet(typeof(int), typeof(int), typeof(int));
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstIntIndex, ig(_testStructInstance, new object[] {FirstIntIndex, SecondIntIndex}));
        }

        [TestMethod]
        public void IndexerGet_ByObjects_3Index()
        {
            var ig = _testClassType.IndexerGet(typeof(int), typeof(int), typeof(int), typeof(int));
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstIntIndex, ig(_testClassInstance, new object[] {FirstIntIndex, SecondIntIndex, 0}));
        }

        [TestMethod]
        public void IndexerGet_ByObjects_3Index_FromStruct()
        {
            var ig = _testStrucType.IndexerGet(typeof(int), typeof(int), typeof(int), typeof(int));
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstIntIndex, ig(_testStructInstance, new object[] {FirstIntIndex, SecondIntIndex, 0}));
        }

        [TestMethod]
        public void IndexerGet_ByObjects_4Index()
        {
            var ig = _testClassType.IndexerGet(typeof(int), typeof(int), typeof(int), typeof(int), typeof(int));
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstIntIndex, ig(_testClassInstance, new object[] {FirstIntIndex, SecondIntIndex, 0, 0}));
        }

        [TestMethod]
        public void IndexerGet_ByObjects_4Index_FromStruct()
        {
            var ig = _testStrucType.IndexerGet(typeof(int), typeof(int), typeof(int), typeof(int), typeof(int));
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstIntIndex, ig(_testStructInstance, new object[] {FirstIntIndex, SecondIntIndex, 0, 0}));
        }
    }
}