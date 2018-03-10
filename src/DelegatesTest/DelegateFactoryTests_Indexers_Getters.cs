// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DelegateFactoryTests_Indexers_Getters.cs" company="Natan Podbielski">
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
    public class DelegateFactoryTests_Indexers_Getters
    {
        private const string FirstStringIndex = "test";
        private const int FirstIntIndex = 0;
        private const long FirstLongIndex = 0;
        private const byte FirstByteIndex = 0;
        private const int SecondIntIndex = 1;

        [TestMethod]
        public void IndexerGet_ByExtensionAndReturnType_NonExisting()
        {
            var ig = DelegateFactory.IndexerGet<TestClass, ulong, ulong>();
            Assert.IsNull(ig);
        }

        [TestMethod]
        public void IndexerGet_ByExtensionAndReturnType_OnlyWrite()
        {
            var ig = typeof(TestClass).IndexerGet<string, string, string>();
            Assert.IsNull(ig);
        }

        [TestMethod]
        public void IndexerGet_ByExtensionAndReturnType_OnlyWrite_FromStruct()
        {
            var ig = typeof(TestStruct).IndexerGet<string, string, string>();
            Assert.IsNull(ig);
        }

        [TestMethod]
        public void IndexerGet_ByExtensionAndReturnType_Internal()
        {
            var ig = typeof(TestClass).IndexerGet<string, string>();
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstStringIndex, ig(new TestClass(), FirstStringIndex));
        }

        [TestMethod]
        public void IndexerGet_ByExtensionAndReturnType_Internal_FromStruct()
        {
            var ig = typeof(TestStruct).IndexerGet<string, string>();
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstStringIndex, ig(new TestStruct(0), FirstStringIndex));
        }

        [TestMethod]
        public void IndexerGet_ByExtensionAndReturnType_Private()
        {
            var ig = typeof(TestClass).IndexerGet<int, int, int>();
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstIntIndex, ig(new TestClass(), FirstIntIndex, SecondIntIndex));
        }

        [TestMethod]
        public void IndexerGet_ByExtensionAndReturnType_Private_FromStruct()
        {
            var ig = typeof(TestStruct).IndexerGet<int, int, int>();
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstIntIndex, ig(new TestStruct(0), FirstIntIndex, SecondIntIndex));
        }

        [TestMethod]
        public void IndexerGet_ByExtensionAndReturnType_Protected()
        {
            var ig = typeof(TestClass).IndexerGet<byte, byte>();
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstByteIndex, ig(new TestClass(), FirstByteIndex));
        }

        [TestMethod]
        public void IndexerGet_ByExtensionAndReturnType_Public()
        {
            var ig = typeof(TestClass).IndexerGet<int, int>();
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstIntIndex, ig(new TestClass(), FirstIntIndex));
        }

        [TestMethod]
        public void IndexerGet_ByExtensionAndReturnType_Public_FromStruct()
        {
            var ig = typeof(TestStruct).IndexerGet<int, int>();
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstIntIndex, ig(new TestStruct(0), FirstIntIndex));
        }

        [TestMethod]
        public void IndexerGet_ByObjects_Internal()
        {
            var ig = typeof(TestClass).IndexerGetNew(typeof(string));
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstStringIndex, ig(new TestClass(), new object[] {FirstStringIndex}));
        }

        [TestMethod]
        public void IndexerGet_ByObjects_Internal_FromStruct()
        {
            var ig = typeof(TestStruct).IndexerGetNew(typeof(string));
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstStringIndex, ig(new TestStruct(0), new object[] {FirstStringIndex}));
        }

        [TestMethod]
        public void IndexerGet_ByObjects_NonExisting()
        {
            var ig = typeof(TestClass).IndexerGetNew(typeof(ulong));
            Assert.IsNull(ig);
        }

        [TestMethod]
        public void IndexerGet_ByObjects_OnlyWrite()
        {
            var ig = typeof(TestClass).IndexerGetNew(typeof(string), typeof(string));
            Assert.IsNull(ig);
        }

        [TestMethod]
        public void IndexerGet_ByObjects_Private()
        {
            var ig = typeof(TestClass).IndexerGetNew(typeof(long));
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstLongIndex, ig(new TestClass(), new object[] {FirstLongIndex}));
        }

        [TestMethod]
        public void IndexerGet_ByObjects_Private_FromStruct()
        {
            var ig = typeof(TestStruct).IndexerGetNew(typeof(long));
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstLongIndex, ig(new TestStruct(0), new object[] {FirstLongIndex}));
        }

        [TestMethod]
        public void IndexerGet_ByObjects_Protected()
        {
            var ig = typeof(TestClass).IndexerGetNew(typeof(byte));
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstByteIndex, ig(new TestClass(), new object[] {FirstByteIndex}));
        }

        [TestMethod]
        public void IndexerGet_ByObjects_Public()
        {
            var ig = typeof(TestClass).IndexerGetNew(typeof(int));
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstIntIndex, ig(new TestClass(), new object[] {FirstIntIndex}));
        }


        [TestMethod]
        public void IndexerGet_ByObjects_Public_FromStruct()
        {
            var ig = typeof(TestStruct).IndexerGetNew(typeof(int));
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstIntIndex, ig(new TestStruct(0), new object[] {FirstIntIndex}));
        }

        [TestMethod]
        public void IndexerGet_ByTypes_Internal()
        {
            var ig = DelegateFactory.IndexerGet<TestClass, string, string>();
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstStringIndex, ig(new TestClass(), FirstStringIndex));
        }

        [TestMethod]
        public void IndexerGet_ByTypes_Internal_FromStruct()
        {
            var ig = DelegateFactory.IndexerGetStruct<TestStruct, string, string>();
            Assert.IsNotNull(ig);
            var testStruct = new TestStruct(0);
            Assert.AreEqual(FirstStringIndex, ig(ref testStruct, FirstStringIndex));
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
            Assert.AreEqual(FirstLongIndex, ig(new TestClass(), FirstLongIndex));
        }

        [TestMethod]
        public void IndexerGet_ByTypes_Private_FromStruct()
        {
            var ig = DelegateFactory.IndexerGetStruct<TestStruct, long, long>();
            Assert.IsNotNull(ig);
            var testStruct = new TestStruct(0);
            Assert.AreEqual(FirstLongIndex, ig(ref testStruct, FirstLongIndex));
        }

        [TestMethod]
        public void IndexerGet_ByTypes_Protected()
        {
            var ig = DelegateFactory.IndexerGet<TestClass, byte, byte>();
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstByteIndex, ig(new TestClass(), FirstByteIndex));
        }

        [TestMethod]
        public void IndexerGet_ByTypes_Public()
        {
            var ig = DelegateFactory.IndexerGet<TestClass, int, int>();
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstIntIndex, ig(new TestClass(), FirstIntIndex));
        }

        [TestMethod]
        public void IndexerGet_ByTypes_Public_FromStruct()
        {
            var ig = DelegateFactory.IndexerGetStruct<TestStruct, int, int>();
            Assert.IsNotNull(ig);
            var testStruct = new TestStruct(0);
            Assert.AreEqual(FirstIntIndex, ig(ref testStruct, FirstIntIndex));
        }

        [TestMethod]
        public void IndexerGet_ByTypes_1Index()
        {
            var ig = DelegateFactory.IndexerGet<TestClass, int, int>();
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstIntIndex, ig(new TestClass(), FirstIntIndex));
        }

        [TestMethod]
        public void IndexerGet_ByTypes_1Index_FromStruct()
        {
            var ig = DelegateFactory.IndexerGetStruct<TestStruct, int, int>();
            Assert.IsNotNull(ig);
            var testStruct = new TestStruct(0);
            Assert.AreEqual(FirstIntIndex, ig(ref testStruct, FirstIntIndex));
        }

        [TestMethod]
        public void IndexerGet_ByTypes_2Index()
        {
            var ig = DelegateFactory.IndexerGet<TestClass, int, int, int>();
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstIntIndex, ig(new TestClass(), FirstIntIndex, SecondIntIndex));
        }

        [TestMethod]
        public void IndexerGet_ByTypes_2Index_FromStruct()
        {
            var ig = DelegateFactory.IndexerGetStruct<TestStruct, int, int, int>();
            Assert.IsNotNull(ig);
            var testStruct = new TestStruct(0);
            Assert.AreEqual(FirstIntIndex, ig(ref testStruct, FirstIntIndex, SecondIntIndex));
        }

        [TestMethod]
        public void IndexerGet_ByTypes_3Index()
        {
            var ig = DelegateFactory.IndexerGet<TestClass, int, int, int, int>();
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstIntIndex, ig(new TestClass(), FirstIntIndex, SecondIntIndex, 0));
        }

        [TestMethod]
        public void IndexerGet_ByTypes_3Index_FromStruct()
        {
            var ig = DelegateFactory.IndexerGetStruct<TestStruct, int, int, int, int>();
            Assert.IsNotNull(ig);
            var testStruct = new TestStruct(0);
            Assert.AreEqual(FirstIntIndex, ig(ref testStruct, FirstIntIndex, SecondIntIndex, 0));
        }


        [TestMethod]
        public void IndexerGet_ByExtensionAndReturnType_1Index()
        {
            var ig = typeof(TestClass).IndexerGet<int, int>();
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstIntIndex, ig(new TestClass(), FirstIntIndex));
        }

        [TestMethod]
        public void IndexerGet_ByExtensionAndReturnType_1Index_FromStruct()
        {
            var ig = typeof(TestStruct).IndexerGet<int, int>();
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstIntIndex, ig(new TestStruct(0), FirstIntIndex));
        }

        [TestMethod]
        public void IndexerGet_ByExtensionAndReturnType_2Index()
        {
            var ig = typeof(TestClass).IndexerGet<int, int, int>();
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstIntIndex, ig(new TestClass(), FirstIntIndex, SecondIntIndex));
        }

        [TestMethod]
        public void IndexerGet_ByExtensionAndReturnType_2Index_FromStruct()
        {
            var ig = typeof(TestStruct).IndexerGet<int, int, int>();
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstIntIndex, ig(new TestStruct(0), FirstIntIndex, SecondIntIndex));
        }

        [TestMethod]
        public void IndexerGet_ByExtensionAndReturnType_3Index()
        {
            var ig = typeof(TestClass).IndexerGet<int, int, int, int>();
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstIntIndex, ig(new TestClass(), FirstIntIndex, SecondIntIndex, 0));
        }

        [TestMethod]
        public void IndexerGet_ByExtensionAndReturnType_3Index_FromStruct()
        {
            var ig = typeof(TestStruct).IndexerGet<int, int, int, int>();
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstIntIndex, ig(new TestStruct(0), FirstIntIndex, SecondIntIndex, 0));
        }

        [TestMethod]
        public void IndexerGet_ByObjects_1Index()
        {
            var ig = typeof(TestClass).IndexerGetNew(typeof(int));
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstIntIndex, ig(new TestClass(), new object[] {FirstIntIndex}));
        }

        [TestMethod]
        public void IndexerGet_ByObjects_1Index_FromStruct()
        {
            var ig = typeof(TestStruct).IndexerGetNew(typeof(int));
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstIntIndex, ig(new TestStruct(0), new object[] {FirstIntIndex}));
        }

        [TestMethod]
        public void IndexerGet_ByObjects_2Index()
        {
            var ig = typeof(TestClass).IndexerGetNew(typeof(int), typeof(int));
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstIntIndex, ig(new TestClass(), new object[] {FirstIntIndex, SecondIntIndex}));
        }

        [TestMethod]
        public void IndexerGet_ByObjects_2Index_FromStruct()
        {
            var ig = typeof(TestStruct).IndexerGetNew(typeof(int), typeof(int));
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstIntIndex, ig(new TestStruct(0), new object[] {FirstIntIndex, SecondIntIndex}));
        }

        [TestMethod]
        public void IndexerGet_ByObjects_3Index()
        {
            var ig = typeof(TestClass).IndexerGetNew(typeof(int), typeof(int), typeof(int));
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstIntIndex, ig(new TestClass(), new object[] {FirstIntIndex, SecondIntIndex, 0}));
        }

        [TestMethod]
        public void IndexerGet_ByObjects_3Index_FromStruct()
        {
            var ig = typeof(TestStruct).IndexerGetNew(typeof(int), typeof(int), typeof(int));
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstIntIndex, ig(new TestStruct(0), new object[] {FirstIntIndex, SecondIntIndex, 0}));
        }

        [TestMethod]
        public void IndexerGet_ByObjects_4Index()
        {
            var ig = typeof(TestClass).IndexerGetNew(typeof(int), typeof(int), typeof(int), typeof(int));
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstIntIndex, ig(new TestClass(), new object[] {FirstIntIndex, SecondIntIndex, 0, 0}));
        }

        [TestMethod]
        public void IndexerGet_ByObjects_4Index_FromStruct()
        {
            var ig = typeof(TestStruct).IndexerGetNew(typeof(int), typeof(int), typeof(int), typeof(int));
            Assert.IsNotNull(ig);
            Assert.AreEqual(FirstIntIndex, ig(new TestStruct(0), new object[] {FirstIntIndex, SecondIntIndex, 0, 0}));
        }

        [TestMethod]
        public void IndexerGet_Interface_ByTypes()
        {
            var ig = DelegateFactory.IndexerGet<IService, int, int>();
            Assert.IsNotNull(ig);
            var interfaceImpl = new Service();
            Assert.AreEqual(interfaceImpl[FirstIntIndex], ig(interfaceImpl, FirstIntIndex));
        }

        [TestMethod]
        public void IndexerGet_Interface_ByObjectAndType()
        {
            var ig = typeof(IService).IndexerGet<int, int>();
            Assert.IsNotNull(ig);
            var interfaceImpl = new Service();
            Assert.AreEqual(interfaceImpl[FirstIntIndex], ig(interfaceImpl, FirstIntIndex));
        }

        [TestMethod]
        public void IndexerGet_Interface_ByObjects()
        {
            var ig = typeof(IService).IndexerGetNew(typeof(int));
            Assert.IsNotNull(ig);
            var interfaceImpl = new Service();
            Assert.AreEqual(interfaceImpl[FirstIntIndex], ig(interfaceImpl, new object[] {FirstIntIndex}));
        }
    }
}