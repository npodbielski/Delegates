// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DelegateFactoryTests_Indexers_Setters.cs" company="Natan Podbielski">
//   Copyright (c) 2016 - 2018 Natan Podbielski. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using Delegates;
using DelegatesTest;
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
    public class DelegateFactoryTests_Indexers_Setters
    {
        private const int FirstIntIndex = 0;
        private const byte NewByteValue = 0;
        private const double FirstDoubleIndex = 0.0;
        private const byte FirstByteIndex = 0;
        private const int SecondIntIndex = 1;
        private const int NewIntValue = 100;
        private const double NewDoubleValue = 11.0;

        //TODO: test correctness of returnType parameter of DelegateIndexerSet method
        [TestMethod]
        public void IndexerSet_ByExtensionAndReturnType_NonExisting()
        {
            var @is = DelegateFactory.IndexerSet<TestClass, ulong, ulong>();
            Assert.IsNull(@is);
        }

        [TestMethod]
        public void IndexerSet_ByExtensionAndReturnType_OnlyRead()
        {
            var @is = typeof(TestClass).IndexerSet<string, string>();
            Assert.IsNull(@is);
        }

        [TestMethod]
        public void IndexerSet_ByExtensionAndReturnType_OnlyWrite_FromStruct()
        {
            var @is = typeof(TestStruct).IndexerSet<string, string>();
            Assert.IsNull(@is);
        }

        [TestMethod]
        public void IndexerSet_ByExtensionAndReturnType_Internal()
        {
            var @is = typeof(TestClass).IndexerSet<double, double>();
            Assert.IsNotNull(@is);
            var testClassInstance = new TestClass();
            @is(testClassInstance, FirstDoubleIndex, NewDoubleValue);
            Assert.AreEqual(NewDoubleValue, testClassInstance.InternalIndexer);
        }

#if !NET35
        [TestMethod]
        public void IndexerSet_ByExtensionAndReturnType_Internal_FromStruct()
        {
            var @is = typeof(TestStruct).IndexerSetStruct<double, double>();
            Assert.IsNotNull(@is);
            var objectStruct = (object)new TestStruct(0);
            @is(ref objectStruct, FirstDoubleIndex, NewDoubleValue);
            Assert.AreEqual(NewDoubleValue, ((TestStruct)objectStruct).InternalIndexer);
        }
#endif

        [TestMethod]
        public void IndexerSet_ByExtensionAndReturnType_Private()
        {
            var @is = typeof(TestClass).IndexerSet<int, int, int>();
            Assert.IsNotNull(@is);
            var testClassInstance = new TestClass();
            @is(testClassInstance, FirstIntIndex, SecondIntIndex, NewIntValue);
            Assert.AreEqual(NewIntValue, testClassInstance.PrivateIndexer);
        }

#if !NET35
        [TestMethod]
        public void IndexerSet_ByExtensionAndReturnType_Private_FromStruct()
        {
            var @is = typeof(TestStruct).IndexerSetStruct<int, int, int>();
            Assert.IsNotNull(@is);
            var objectStruct = (object)new TestStruct(0);
            @is(ref objectStruct, FirstIntIndex, SecondIntIndex, NewIntValue);
            Assert.AreEqual(NewIntValue, ((TestStruct)objectStruct).PrivateIndexer);
        }
#endif

        [TestMethod]
        public void IndexerSet_ByExtensionAndReturnType_Protected()
        {
            var @is = typeof(TestClass).IndexerSet<byte, byte>();
            Assert.IsNotNull(@is);
            var testClassInstance = new TestClass();
            @is(testClassInstance, FirstByteIndex, NewByteValue);
            Assert.AreEqual(NewByteValue, testClassInstance.ProtectedIndexer);
        }

        [TestMethod]
        public void IndexerSet_ByExtensionAndReturnType_Public()
        {
            var @is = typeof(TestClass).IndexerSet<int, int>();
            Assert.IsNotNull(@is);
            var testClassInstance = new TestClass();
            @is(testClassInstance, FirstIntIndex, NewIntValue);
            Assert.AreEqual(NewIntValue, testClassInstance.IndexerBackend[FirstIntIndex]);
        }

        [TestMethod]
        public void IndexerSet_ByTypes_Internal()
        {
            var @is = DelegateFactory.IndexerSet<TestClass, double, double>();
            Assert.IsNotNull(@is);
            var testClassInstance = new TestClass();
            @is(testClassInstance, FirstDoubleIndex, NewDoubleValue);
            Assert.AreEqual(NewDoubleValue, testClassInstance.InternalIndexer);
        }

        [TestMethod]
        public void IndexerSet_ByTypes_Internal_FromStruct()
        {
            var @is = DelegateFactory.IndexerSetStruct<TestStruct, double, double>();
            Assert.IsNotNull(@is);
            var testStructInstance = new TestStruct(0);
            @is(ref testStructInstance, FirstDoubleIndex, NewDoubleValue);
            Assert.AreEqual(NewDoubleValue, testStructInstance.InternalIndexer);
        }

        [TestMethod]
        public void IndexerSet_ByTypes_NonExisting()
        {
            var @is = DelegateFactory.IndexerSet<TestClass, ulong, ulong>();
            Assert.IsNull(@is);
        }

        [TestMethod]
        public void IndexerSet_ByTypes_OnlyRead()
        {
            var @is = DelegateFactory.IndexerSet<TestClass, string, string>();
            Assert.IsNull(@is);
        }

        [TestMethod]
        public void IndexerSet_ByTypes_Private()
        {
            var @is = DelegateFactory.IndexerSet<TestClass, int, int, int>();
            Assert.IsNotNull(@is);
            var testClassInstance = new TestClass();
            @is(testClassInstance, FirstIntIndex, SecondIntIndex, NewIntValue);
            Assert.AreEqual(NewIntValue, testClassInstance.PrivateIndexer);
        }

        [TestMethod]
        public void IndexerSet_ByTypes_Private_FromStruct()
        {
            var @is = DelegateFactory.IndexerSetStruct<TestStruct, int, int, int>();
            Assert.IsNotNull(@is);
            var testStructInstance = new TestStruct(0);
            @is(ref testStructInstance, FirstIntIndex, SecondIntIndex, NewIntValue);
            Assert.AreEqual(NewIntValue, testStructInstance.PrivateIndexer);
        }

        [TestMethod]
        public void IndexerSet_ByTypes_Protected()
        {
            var @is = DelegateFactory.IndexerSet<TestClass, byte, byte>();
            Assert.IsNotNull(@is);
            var testClassInstance = new TestClass();
            @is(testClassInstance, FirstByteIndex, NewByteValue);
            Assert.AreEqual(NewByteValue, testClassInstance.ProtectedIndexer);
        }

        [TestMethod]
        public void IndexerSet_ByTypes_Public()
        {
            var @is = DelegateFactory.IndexerSet<TestClass, int, int>();
            Assert.IsNotNull(@is);
            var testClassInstance = new TestClass();
            @is(testClassInstance, FirstIntIndex, NewIntValue);
            Assert.AreEqual(NewIntValue, testClassInstance.IndexerBackend[FirstIntIndex]);
        }

        [TestMethod]
        public void IndexerSet_ByTypes_Public_FromStruct()
        {
            var @is = DelegateFactory.IndexerSetStruct<TestStruct, int, int>();
            Assert.IsNotNull(@is);
            var testStructInstance = new TestStruct(0);
            @is(ref testStructInstance, FirstIntIndex, NewIntValue);
            Assert.AreEqual(NewIntValue, testStructInstance.IndexerBackend[FirstIntIndex]);
        }

        [TestMethod]
        public void IndexerSet_ByTypes_1Index()
        {
            var @is = DelegateFactory.IndexerSet<TestClass, int, int>();
            Assert.IsNotNull(@is);
            var testClassInstance = new TestClass();
            @is(testClassInstance, FirstIntIndex, NewIntValue);
            Assert.AreEqual(NewIntValue, testClassInstance.IndexerBackend[FirstIntIndex]);
        }

        [TestMethod]
        public void IndexerSet_ByTypes_IncorrectReturnType_Incompatible()
        {
            AssertHelper.ThrowsException<ArgumentException>(() =>
            {
                DelegateFactory.IndexerSet<TestClass, string, int>();
            });
        }

        [TestMethod]
        public void IndexerSet_ByTypes_IncorrectReturnType_Object()
        {
            var @is = DelegateFactory.IndexerSet<TestClass, object, int>();
            Assert.IsNotNull(@is);
            var testClassInstance = new TestClass();
            @is(testClassInstance, FirstIntIndex, NewIntValue);
            Assert.AreEqual(NewIntValue, testClassInstance.IndexerBackend[FirstIntIndex]);
        }

        [TestMethod]
        public void IndexerSet_ByTypes_IncorrectReturnType_Compatible()
        {
            var @is = DelegateFactory.IndexerSet<TestClass, long, int>();
            Assert.IsNotNull(@is);
            var testClassInstance = new TestClass();
            @is(testClassInstance, FirstIntIndex, NewIntValue);
            Assert.AreEqual(NewIntValue, testClassInstance.IndexerBackend[FirstIntIndex]);
        }

        [TestMethod]
        public void IndexerSet_ByTypes_1Index_FromStruct()
        {
            var @is = DelegateFactory.IndexerSetStruct<TestStruct, int, int>();
            Assert.IsNotNull(@is);
            var testStructInstance = new TestStruct(0);
            @is(ref testStructInstance, FirstIntIndex, NewIntValue);
            Assert.AreEqual(NewIntValue, testStructInstance.IndexerBackend[FirstIntIndex]);
        }

        [TestMethod]
        public void IndexerSet_ByTypes_2Index()
        {
            var @is = DelegateFactory.IndexerSet<TestClass, int, int, int>();
            Assert.IsNotNull(@is);
            var testClassInstance = new TestClass();
            @is(testClassInstance, FirstIntIndex, SecondIntIndex, NewIntValue);
            Assert.AreEqual(NewIntValue, testClassInstance.PrivateIndexer);
        }

        [TestMethod]
        public void IndexerSet_ByTypes_2Index_FromStruct()
        {
            var @is = DelegateFactory.IndexerSetStruct<TestStruct, int, int, int>();
            Assert.IsNotNull(@is);
            var testStructInstance = new TestStruct(0);
            @is(ref testStructInstance, FirstIntIndex, SecondIntIndex, NewIntValue);
            Assert.AreEqual(NewIntValue, testStructInstance.PrivateIndexer);
        }

        [TestMethod]
        public void IndexerSet_ByTypes_3Index()
        {
            var @is = DelegateFactory.IndexerSet<TestClass, int, int, int, int>();
            Assert.IsNotNull(@is);
            var testClassInstance = new TestClass();
            @is(testClassInstance, FirstIntIndex, SecondIntIndex, 0, NewIntValue);
            Assert.AreEqual(NewIntValue, testClassInstance.Public3IndexIndexer);
        }

        [TestMethod]
        public void IndexerSet_ByTypes_3Index_FromStruct()
        {
            var @is = DelegateFactory.IndexerSetStruct<TestStruct, int, int, int, int>();
            Assert.IsNotNull(@is);
            var testStructInstance = new TestStruct(0);
            @is(ref testStructInstance, FirstIntIndex, SecondIntIndex, 0, NewIntValue);
            Assert.AreEqual(NewIntValue, testStructInstance.Public3IndexIndexer);
        }


        [TestMethod]
        public void IndexerSet_ByExtensionAndReturnType_1Index()
        {
            var @is = typeof(TestClass).IndexerSet<int, int>();
            Assert.IsNotNull(@is);
            var testClassInstance = new TestClass();
            @is(testClassInstance, FirstIntIndex, NewIntValue);
            Assert.AreEqual(NewIntValue, testClassInstance.IndexerBackend[FirstIntIndex]);
        }

        [TestMethod]
        public void IndexerSet_ByExtensionAndReturnType_2Index()
        {
            var @is = typeof(TestClass).IndexerSet<int, int, int>();
            Assert.IsNotNull(@is);
            var testClassInstance = new TestClass();
            @is(testClassInstance, FirstIntIndex, SecondIntIndex, NewIntValue);
            Assert.AreEqual(NewIntValue, testClassInstance.PrivateIndexer);
        }

#if !NET35
        [TestMethod]
        public void IndexerSet_ByExtensionAndReturnType_2Index_FromStruct()
        {
            var @is = typeof(TestStruct).IndexerSetStruct<int, int, int>();
            Assert.IsNotNull(@is);
            var objectStruct = (object)new TestStruct(0);
            @is(ref objectStruct, FirstIntIndex, SecondIntIndex, NewIntValue);
            Assert.AreEqual(NewIntValue, ((TestStruct)objectStruct).PrivateIndexer);
        }
#endif

        [TestMethod]
        public void IndexerSet_ByExtensionAndReturnType_3Index()
        {
            var @is = typeof(TestClass).IndexerSet<int, int, int, int>();
            Assert.IsNotNull(@is);
            var testClassInstance = new TestClass();
            @is(testClassInstance, FirstIntIndex, SecondIntIndex, 0, NewIntValue);
            Assert.AreEqual(NewIntValue, testClassInstance.Public3IndexIndexer);
        }

        [TestMethod]
        public void IndexerSet_Interface_ByTypes()
        {
            var @is = DelegateFactory.IndexerSet<IService, int, int>();
            Assert.IsNotNull(@is);
            var interfaceImpl = new Service();
            @is(interfaceImpl, FirstIntIndex, NewIntValue);
            Assert.AreEqual(NewIntValue, interfaceImpl.IndexerSetValue);
        }

        [TestMethod]
        public void IndexerSet_Interface_ByObjectAndType()
        {
            var @is = typeof(IService).IndexerSet<int, int>();
            Assert.IsNotNull(@is);
            var interfaceImpl = new Service();
            @is(interfaceImpl, FirstIntIndex, NewIntValue);
            Assert.AreEqual(NewIntValue, interfaceImpl.IndexerSetValue);
        }

        [TestMethod]
        public void IndexerSet_ByExtensionAndReturnType_IncorrectReturnType_Incompatible()
        {
            AssertHelper.ThrowsException<ArgumentException>(() => { typeof(TestClass).IndexerSet<string, int>(); });
        }

        [TestMethod]
        public void IndexerSet_ByExtensionAndReturnType_IncorrectReturnType_Object()
        {
            var @is = typeof(TestClass).IndexerSet<object, int>();
            Assert.IsNotNull(@is);
            var testClassInstance = new TestClass();
            @is(testClassInstance, FirstIntIndex, NewIntValue);
            Assert.AreEqual(NewIntValue, testClassInstance.IndexerBackend[FirstIntIndex]);
        }

        [TestMethod]
        public void IndexerSet_ByExtensionAndReturnType_IncorrectReturnType_Compatible()
        {
            var @is = typeof(TestClass).IndexerSet<long, int>();
            Assert.IsNotNull(@is);
            var testClassInstance = new TestClass();
            @is(testClassInstance, FirstIntIndex, NewIntValue);
            Assert.AreEqual(NewIntValue, testClassInstance.IndexerBackend[FirstIntIndex]);
        }

#if !NET35
        [TestMethod]
        public void IndexerSet_ByExtensionAndReturnType_Public_FromStruct()
        {
            var @is = typeof(TestStruct).IndexerSetStruct<int, int>();
            Assert.IsNotNull(@is);
            var objectStruct = (object)new TestStruct(0);
            @is(ref objectStruct, FirstIntIndex, NewIntValue);
            Assert.AreEqual(NewIntValue, ((TestStruct)objectStruct).IndexerBackend[FirstIntIndex]);
        }

        [TestMethod]
        public void IndexerSet_ByObjects_Internal()
        {
#pragma warning disable 618
            var @is = typeof(TestClass).IndexerSet(typeof(double), typeof(double));
#pragma warning restore 618
            Assert.IsNotNull(@is);
            var testClassInstance = new TestClass();
            @is(testClassInstance, new object[] {FirstDoubleIndex}, NewDoubleValue);
            Assert.AreEqual(NewDoubleValue, testClassInstance.InternalIndexer);
        }

        [TestMethod]
        public void IndexerSetNew_ByObjects_Internal()
        {
            var @is = typeof(TestClass).IndexerSetNew(typeof(double));
            Assert.IsNotNull(@is);
            var testClassInstance = new TestClass();
            @is(testClassInstance, new object[] {FirstDoubleIndex}, NewDoubleValue);
            Assert.AreEqual(NewDoubleValue, testClassInstance.InternalIndexer);
        }

        [TestMethod]
        public void IndexerSet_ByObjects_Internal_FromStruct()
        {
#pragma warning disable 618
            var @is = typeof(TestStruct).IndexerSetStruct(typeof(double), typeof(double));
#pragma warning restore 618
            Assert.IsNotNull(@is);
            var objectStruct = (object)new TestStruct(0);
            @is(ref objectStruct, new object[] {FirstDoubleIndex}, NewDoubleValue);
            Assert.AreEqual(NewDoubleValue, ((TestStruct)objectStruct).InternalIndexer);
        }

        [TestMethod]
        public void IndexerSetNew_ByObjects_Internal_FromStruct()
        {
            var @is = typeof(TestStruct).IndexerSetStructNew(typeof(double));
            Assert.IsNotNull(@is);
            var objectStruct = (object)new TestStruct(0);
            @is(ref objectStruct, new object[] {FirstDoubleIndex}, NewDoubleValue);
            Assert.AreEqual(NewDoubleValue, ((TestStruct)objectStruct).InternalIndexer);
        }

        [TestMethod]
        public void IndexerSet_ByObjects_NonExisting()
        {
#pragma warning disable 618
            var @is = typeof(TestClass).IndexerSet(typeof(ulong), typeof(ulong));
#pragma warning restore 618
            Assert.IsNull(@is);
        }

        [TestMethod]
        public void IndexerSetNew_ByObjects_NonExisting()
        {
            var @is = typeof(TestClass).IndexerSetNew(typeof(ulong));
            Assert.IsNull(@is);
        }

        [TestMethod]
        public void IndexerSet_ByObjects_OnlyRead()
        {
#pragma warning disable 618
            var @is = typeof(TestClass).IndexerSet(typeof(string), typeof(string));
#pragma warning restore 618
            Assert.IsNull(@is);
        }

        [TestMethod]
        public void IndexerSetNew_ByObjects_OnlyRead()
        {
            var @is = typeof(TestClass).IndexerSetNew(typeof(string));
            Assert.IsNull(@is);
        }

        [TestMethod]
        public void IndexerSet_ByObjects_Private()
        {
#pragma warning disable 618
            var @is = typeof(TestClass).IndexerSet(typeof(int), typeof(int), typeof(int));
#pragma warning restore 618
            Assert.IsNotNull(@is);
            var testClassInstance = new TestClass();
            @is(testClassInstance, new object[] {FirstIntIndex, SecondIntIndex}, NewIntValue);
            Assert.AreEqual(NewIntValue, testClassInstance.PrivateIndexer);
        }

        [TestMethod]
        public void IndexerSetNew_ByObjects_Private()
        {
            var @is = typeof(TestClass).IndexerSetNew(typeof(int), typeof(int));
            Assert.IsNotNull(@is);
            var testClassInstance = new TestClass();
            @is(testClassInstance, new object[] {FirstIntIndex, SecondIntIndex}, NewIntValue);
            Assert.AreEqual(NewIntValue, testClassInstance.PrivateIndexer);
        }

        [TestMethod]
        public void IndexerSet_ByObjects_Private_FromStruct()
        {
#pragma warning disable 618
            var @is = typeof(TestStruct).IndexerSetStruct(typeof(int), typeof(int), typeof(int));
#pragma warning restore 618
            Assert.IsNotNull(@is);
            var objectStruct = (object)new TestStruct(0);
            @is(ref objectStruct, new object[] {FirstIntIndex, SecondIntIndex}, NewIntValue);
            Assert.AreEqual(NewIntValue, ((TestStruct)objectStruct).PrivateIndexer);
        }

        [TestMethod]
        public void IndexerSetNew_ByObjects_Private_FromStruct()
        {
            var @is = typeof(TestStruct).IndexerSetStructNew(typeof(int), typeof(int));
            Assert.IsNotNull(@is);
            var objectStruct = (object)new TestStruct(0);
            @is(ref objectStruct, new object[] {FirstIntIndex, SecondIntIndex}, NewIntValue);
            Assert.AreEqual(NewIntValue, ((TestStruct)objectStruct).PrivateIndexer);
        }

        [TestMethod]
        public void IndexerSet_ByObjects_Protected()
        {
#pragma warning disable 618
            var @is = typeof(TestClass).IndexerSet(typeof(byte), typeof(byte));
#pragma warning restore 618
            Assert.IsNotNull(@is);
            var testClassInstance = new TestClass();
            @is(testClassInstance, new object[] {FirstByteIndex}, NewByteValue);
            Assert.AreEqual(NewByteValue, testClassInstance.ProtectedIndexer);
        }

        [TestMethod]
        public void IndexerSetNew_ByObjects_Protected()
        {
            var @is = typeof(TestClass).IndexerSetNew(typeof(byte));
            Assert.IsNotNull(@is);
            var testClassInstance = new TestClass();
            @is(testClassInstance, new object[] {FirstByteIndex}, NewByteValue);
            Assert.AreEqual(NewByteValue, testClassInstance.ProtectedIndexer);
        }

        [TestMethod]
        public void IndexerSet_ByObjects_Public()
        {
#pragma warning disable 618
            var @is = typeof(TestClass).IndexerSet(typeof(int), typeof(int));
#pragma warning restore 618
            Assert.IsNotNull(@is);
            var testClassInstance = new TestClass();
            @is(testClassInstance, new object[] {FirstIntIndex}, NewIntValue);
            Assert.AreEqual(NewIntValue, testClassInstance.IndexerBackend[FirstIntIndex]);
        }

        [TestMethod]
        public void IndexerSetNew_ByObjects_Public()
        {
            var @is = typeof(TestClass).IndexerSetNew(typeof(int));
            Assert.IsNotNull(@is);
            var testClassInstance = new TestClass();
            @is(testClassInstance, new object[] {FirstIntIndex}, NewIntValue);
            Assert.AreEqual(NewIntValue, testClassInstance.IndexerBackend[FirstIntIndex]);
        }

        [TestMethod]
        public void IndexerSet_ByObjects_Public_FromStruct()
        {
#pragma warning disable 618
            var @is = typeof(TestStruct).IndexerSetStruct(typeof(int), typeof(int));
#pragma warning restore 618
            Assert.IsNotNull(@is);
            var objectStruct = (object)new TestStruct(0);
            @is(ref objectStruct, new object[] {FirstIntIndex}, NewIntValue);
            Assert.AreEqual(NewIntValue, ((TestStruct)objectStruct).IndexerBackend[FirstIntIndex]);
        }

        [TestMethod]
        public void IndexerSetNew_ByObjects_Public_FromStruct()
        {
            var @is = typeof(TestStruct).IndexerSetStructNew(typeof(int));
            Assert.IsNotNull(@is);
            var objectStruct = (object)new TestStruct(0);
            @is(ref objectStruct, new object[] {FirstIntIndex}, NewIntValue);
            Assert.AreEqual(NewIntValue, ((TestStruct)objectStruct).IndexerBackend[FirstIntIndex]);
        }
#endif

#if !NET35
        [TestMethod]
        public void IndexerSet_ByExtensionAndReturnType_1Index_FromStruct()
        {
            var @is = typeof(TestStruct).IndexerSetStruct<int, int>();
            Assert.IsNotNull(@is);
            var objectStruct = (object)new TestStruct(0);
            @is(ref objectStruct, FirstIntIndex, NewIntValue);
            Assert.AreEqual(NewIntValue, ((TestStruct)objectStruct).IndexerBackend[FirstIntIndex]);
        }

        [TestMethod]
        public void IndexerSetStruct_ByExtensionAndReturnType_IncorrectReturnType_Incompatible()
        {
            AssertHelper.ThrowsException<ArgumentException>(() => { typeof(TestStruct).IndexerSetStruct<string, int>(); });
        }

        [TestMethod]
        public void IndexerSetStruct_ByExtensionAndReturnType_IncorrectReturnType_Object()
        {
            var @is = typeof(TestStruct).IndexerSetStruct<object, int>();
            Assert.IsNotNull(@is);
            var objectStruct = (object)new TestStruct(0);
            @is(ref objectStruct, FirstIntIndex, NewIntValue);
            Assert.AreEqual(NewIntValue, ((TestStruct)objectStruct).IndexerBackend[FirstIntIndex]);
        }

        [TestMethod]
        public void IndexerSetStruct_ByExtensionAndReturnType_IncorrectReturnType_Compatible()
        {
            var @is = typeof(TestStruct).IndexerSetStruct<long, int>();
            Assert.IsNotNull(@is);
            var objectStruct = (object)new TestStruct(0);
            @is(ref objectStruct, FirstIntIndex, NewIntValue);
            Assert.AreEqual(NewIntValue, ((TestStruct)objectStruct).IndexerBackend[FirstIntIndex]);
        }
#endif

#if !NET35
        [TestMethod]
        public void IndexerSet_ByExtensionAndReturnType_3Index_FromStruct()
        {
            var @is = typeof(TestStruct).IndexerSetStruct<int, int, int, int>();
            Assert.IsNotNull(@is);
            var objectStruct = (object)new TestStruct(0);
            @is(ref objectStruct, FirstIntIndex, SecondIntIndex, 0, NewIntValue);
            Assert.AreEqual(NewIntValue, ((TestStruct)objectStruct).Public3IndexIndexer);
        }

        [TestMethod]
        public void IndexerSet_ByObjects_1Index()
        {
#pragma warning disable 618
            var @is = typeof(TestClass).IndexerSet(typeof(int), typeof(int));
#pragma warning restore 618
            Assert.IsNotNull(@is);
            var testClassInstance = new TestClass();
            @is(testClassInstance, new object[] {FirstIntIndex}, NewIntValue);
            Assert.AreEqual(NewIntValue, testClassInstance.IndexerBackend[FirstIntIndex]);
        }

        [TestMethod]
        public void IndexerSetNew_ByObjects_1Index()
        {
            var @is = typeof(TestClass).IndexerSetNew(typeof(int));
            Assert.IsNotNull(@is);
            var testClassInstance = new TestClass();
            @is(testClassInstance, new object[] {FirstIntIndex}, NewIntValue);
            Assert.AreEqual(NewIntValue, testClassInstance.IndexerBackend[FirstIntIndex]);
        }

        [TestMethod]
        public void IndexerSet_ByObjects_1Index_FromStruct()
        {
#pragma warning disable 618
            var @is = typeof(TestStruct).IndexerSetStruct(typeof(int), typeof(int));
#pragma warning restore 618
            Assert.IsNotNull(@is);
            var objectStruct = (object)new TestStruct(0);
            @is(ref objectStruct, new object[] {FirstIntIndex}, NewIntValue);
            Assert.AreEqual(NewIntValue, ((TestStruct)objectStruct).IndexerBackend[FirstIntIndex]);
        }

        [TestMethod]
        public void IndexerSetNew_ByObjects_1Index_FromStruct()
        {
            var @is = typeof(TestStruct).IndexerSetStructNew(typeof(int));
            Assert.IsNotNull(@is);
            var objectStruct = (object)new TestStruct(0);
            @is(ref objectStruct, new object[] {FirstIntIndex}, NewIntValue);
            Assert.AreEqual(NewIntValue, ((TestStruct)objectStruct).IndexerBackend[FirstIntIndex]);
        }

        [TestMethod]
        public void IndexerSet_ByObjects_2Index()
        {
#pragma warning disable 618
            var @is = typeof(TestClass).IndexerSet(typeof(int), typeof(int), typeof(int));
#pragma warning restore 618
            Assert.IsNotNull(@is);
            var testClassInstance = new TestClass();
            @is(testClassInstance, new object[] {FirstIntIndex, SecondIntIndex}, NewIntValue);
            Assert.AreEqual(NewIntValue, testClassInstance.PrivateIndexer);
        }

        [TestMethod]
        public void IndexerSetNew_ByObjects_2Index()
        {
            var @is = typeof(TestClass).IndexerSetNew(typeof(int), typeof(int));
            Assert.IsNotNull(@is);
            var testClassInstance = new TestClass();
            @is(testClassInstance, new object[] {FirstIntIndex, SecondIntIndex}, NewIntValue);
            Assert.AreEqual(NewIntValue, testClassInstance.PrivateIndexer);
        }

        [TestMethod]
        public void IndexerSet_ByObjects_2Index_FromStruct()
        {
#pragma warning disable 618
            var @is = typeof(TestStruct).IndexerSetStruct(typeof(int), typeof(int), typeof(int));
#pragma warning restore 618
            Assert.IsNotNull(@is);
            var objectStruct = (object)new TestStruct(0);
            @is(ref objectStruct, new object[] {FirstIntIndex, SecondIntIndex}, NewIntValue);
            Assert.AreEqual(NewIntValue, ((TestStruct)objectStruct).PrivateIndexer);
        }

        [TestMethod]
        public void IndexerSetNew_ByObjects_2Index_FromStruct()
        {
            var @is = typeof(TestStruct).IndexerSetStructNew(typeof(int), typeof(int));
            Assert.IsNotNull(@is);
            var objectStruct = (object)new TestStruct(0);
            @is(ref objectStruct, new object[] {FirstIntIndex, SecondIntIndex}, NewIntValue);
            Assert.AreEqual(NewIntValue, ((TestStruct)objectStruct).PrivateIndexer);
        }

        [TestMethod]
        public void IndexerSet_ByObjects_3Index()
        {
#pragma warning disable 618
            var @is = typeof(TestClass).IndexerSet(typeof(int), typeof(int), typeof(int), typeof(int));
#pragma warning restore 618
            Assert.IsNotNull(@is);
            var testClassInstance = new TestClass();
            @is(testClassInstance, new object[] {FirstIntIndex, SecondIntIndex, 0}, NewIntValue);
            Assert.AreEqual(NewIntValue, testClassInstance.Public3IndexIndexer);
        }

        [TestMethod]
        public void IndexerSetNew_ByObjects_3Index()
        {
            var @is = typeof(TestClass).IndexerSetNew(typeof(int), typeof(int), typeof(int));
            Assert.IsNotNull(@is);
            var testClassInstance = new TestClass();
            @is(testClassInstance, new object[] {FirstIntIndex, SecondIntIndex, 0}, NewIntValue);
            Assert.AreEqual(NewIntValue, testClassInstance.Public3IndexIndexer);
        }

        [TestMethod]
        public void IndexerSet_ByObjects_3Index_FromStruct()
        {
#pragma warning disable 618
            var @is = typeof(TestStruct).IndexerSetStruct(typeof(int), typeof(int), typeof(int), typeof(int));
#pragma warning restore 618
            Assert.IsNotNull(@is);
            var objectStruct = (object)new TestStruct(0);
            @is(ref objectStruct, new object[] {FirstIntIndex, SecondIntIndex, 0}, NewIntValue);
            Assert.AreEqual(NewIntValue, ((TestStruct)objectStruct).Public3IndexIndexer);
        }

        [TestMethod]
        public void IndexerSetNew_ByObjects_3Index_FromStruct()
        {
            var @is = typeof(TestStruct).IndexerSetStructNew(typeof(int), typeof(int), typeof(int));
            Assert.IsNotNull(@is);
            var objectStruct = (object)new TestStruct(0);
            @is(ref objectStruct, new object[] {FirstIntIndex, SecondIntIndex, 0}, NewIntValue);
            Assert.AreEqual(NewIntValue, ((TestStruct)objectStruct).Public3IndexIndexer);
        }

        [TestMethod]
        public void IndexerSet_ByObjects_4Index()
        {
#pragma warning disable 618
            var @is = typeof(TestClass).IndexerSet(typeof(int), typeof(int), typeof(int), typeof(int), typeof(int));
#pragma warning restore 618
            Assert.IsNotNull(@is);
            var testClassInstance = new TestClass();
            @is(testClassInstance, new object[] {FirstIntIndex, SecondIntIndex, 0, 0}, NewIntValue);
            Assert.AreEqual(NewIntValue, testClassInstance.Public4IndexIndexer);
        }

        [TestMethod]
        public void IndexerSetNew_ByObjects_4Index()
        {
            var @is = typeof(TestClass).IndexerSetNew(typeof(int), typeof(int), typeof(int), typeof(int));
            Assert.IsNotNull(@is);
            var testClassInstance = new TestClass();
            @is(testClassInstance, new object[] {FirstIntIndex, SecondIntIndex, 0, 0}, NewIntValue);
            Assert.AreEqual(NewIntValue, testClassInstance.Public4IndexIndexer);
        }

        [TestMethod]
        public void IndexerSet_ByObjects_4Index_FromStruct()
        {
#pragma warning disable 618
            var @is = typeof(TestStruct).IndexerSetStruct(typeof(int), typeof(int), typeof(int), typeof(int), typeof(int));
#pragma warning restore 618
            Assert.IsNotNull(@is);
            var objectStruct = (object)new TestStruct(0);
            @is(ref objectStruct, new object[] {FirstIntIndex, SecondIntIndex, 0, 0}, NewIntValue);
            Assert.AreEqual(NewIntValue, ((TestStruct)objectStruct).Public4IndexIndexer);
        }

        [TestMethod]
        public void IndexerSetNew_ByObjects_4Index_FromStruct()
        {
            var @is = typeof(TestStruct).IndexerSetStructNew(typeof(int), typeof(int), typeof(int), typeof(int));
            Assert.IsNotNull(@is);
            var objectStruct = (object)new TestStruct(0);
            @is(ref objectStruct, new object[] {FirstIntIndex, SecondIntIndex, 0, 0}, NewIntValue);
            Assert.AreEqual(NewIntValue, ((TestStruct)objectStruct).Public4IndexIndexer);
        }
#endif

#if !NET35
        [TestMethod]
        public void IndexerSet_Interface_ByObjects()
        {
#pragma warning disable 618
            var @is = typeof(IService).IndexerSet(typeof(int), typeof(int));
#pragma warning restore 618
            Assert.IsNotNull(@is);
            var interfaceImpl = new Service();
            @is(interfaceImpl, new object[] {FirstIntIndex}, NewIntValue);
            Assert.AreEqual(NewIntValue, interfaceImpl.IndexerSetValue);
        }

        [TestMethod]
        public void IndexerSetNew_Interface_ByObjects()
        {
            var @is = typeof(IService).IndexerSetNew(typeof(int));
            Assert.IsNotNull(@is);
            var interfaceImpl = new Service();
            @is(interfaceImpl, new object[] {FirstIntIndex}, NewIntValue);
            Assert.AreEqual(NewIntValue, interfaceImpl.IndexerSetValue);
        }

        [TestMethod]
        public void IndexerSet_Interface_ByObjects_New()
        {
            var @is = typeof(IService).IndexerSetNew(typeof(int));
            Assert.IsNotNull(@is);
            var interfaceImpl = new Service();
            @is(interfaceImpl, new object[] {FirstIntIndex}, NewIntValue);
            Assert.AreEqual(NewIntValue, interfaceImpl.IndexerSetValue);
        }
#endif
    }
}