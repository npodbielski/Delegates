// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DelegateFactoryTests_Indexers_Setters.cs" company="Natan Podbielski">
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
    public class DelegateFactoryTests_Indexers_Setters
    {
        private const string FirstStringIndex = "index1";
        private const string SecondStringIndex = "index2";
        private const int FirstIntIndex = 0;
        private const byte NewByteValue = 0;
        private const long FirstLongIndex = 0;
        private const long NewLongValue = 12;
        private const double FirstDoubleIndex = 0.0;
        private const byte FirstByteIndex = 0;
        private const int SecondIntIndex = 1;
        private const string NewStringValue = "TEST";
        private const int NewIntValue = 100;
        private const double NewDoubleValue = 11.0;
        private readonly TestClass _testClassInstance = new TestClass();
        private readonly Type _testClassType = typeof(TestClass);
        private readonly Type _testStrucType = typeof(TestStruct);
        private string _secondStringIndex = "test2";
        private TestStruct _testStructInstance = new TestStruct(0);


        [TestMethod]
        public void IndexerSet_ByExtensionAndReturnType_NonExisting()
        {
            var @is = DelegateFactory.IndexerSet<TestClass, ulong, ulong>();
            Assert.IsNull(@is);
        }

        [TestMethod]
        public void IndexerSet_ByExtensionAndReturnType_OnlyRead()
        {
            var @is = _testClassType.IndexerSet<string, string>();
            Assert.IsNull(@is);
        }

        [TestMethod]
        public void IndexerSet_ByExtensionAndReturnType_OnlyWrite_FromStruct()
        {
            var @is = _testStrucType.IndexerSet<string, string>();
            Assert.IsNull(@is);
        }

        [TestMethod]
        public void IndexerSet_ByExtensionAndReturnType_Internal()
        {
            var @is = _testClassType.IndexerSet<double, double>();
            Assert.IsNotNull(@is);
            @is(_testClassInstance, FirstDoubleIndex, NewDoubleValue);
            Assert.AreEqual(NewDoubleValue, _testClassInstance.InternalIndexer);
        }

#if !NET35
        [TestMethod]
        public void IndexerSet_ByExtensionAndReturnType_Internal_FromStruct()
        {
            var @is = _testStrucType.IndexerSetStruct<double, double>();
            Assert.IsNotNull(@is);
            var objectStruct = (object)_testStructInstance;
            @is(ref objectStruct, FirstDoubleIndex, NewDoubleValue);
            Assert.AreEqual(NewDoubleValue, ((TestStruct)objectStruct).InternalIndexer);
        }
#endif

        [TestMethod]
        public void IndexerSet_ByExtensionAndReturnType_Private()
        {
            var @is = _testClassType.IndexerSet<int, int, int>();
            Assert.IsNotNull(@is);
            @is(_testClassInstance, FirstIntIndex, SecondIntIndex, NewIntValue);
            Assert.AreEqual(NewIntValue, _testClassInstance.PrivateIndexer);
        }

#if !NET35
        [TestMethod]
        public void IndexerSet_ByExtensionAndReturnType_Private_FromStruct()
        {
            var @is = _testStrucType.IndexerSetStruct<int, int, int>();
            Assert.IsNotNull(@is);
            var objectStruct = (object)_testStructInstance;
            @is(ref objectStruct, FirstIntIndex, SecondIntIndex, NewIntValue);
            Assert.AreEqual(NewIntValue, ((TestStruct)objectStruct).PrivateIndexer);
        }
#endif

        [TestMethod]
        public void IndexerSet_ByExtensionAndReturnType_Protected()
        {
            var @is = _testClassType.IndexerSet<byte, byte>();
            Assert.IsNotNull(@is);
            @is(_testClassInstance, FirstByteIndex, NewByteValue);
            Assert.AreEqual(NewByteValue, _testClassInstance.ProtectedIndexer);
        }

        [TestMethod]
        public void IndexerSet_ByExtensionAndReturnType_Public()
        {
            var @is = _testClassType.IndexerSet<int, int>();
            Assert.IsNotNull(@is);
            @is(_testClassInstance, FirstIntIndex, NewIntValue);
            Assert.AreEqual(NewIntValue, _testClassInstance.IndexerBackend[FirstIntIndex]);
        }

#if !NET35
        [TestMethod]
        public void IndexerSet_ByExtensionAndReturnType_Public_FromStruct()
        {
            var @is = _testStrucType.IndexerSetStruct<int, int>();
            Assert.IsNotNull(@is);
            var objectStruct = (object)_testStructInstance;
            @is(ref objectStruct, FirstIntIndex, NewIntValue);
            Assert.AreEqual(NewIntValue, ((TestStruct)objectStruct).IndexerBackend[FirstIntIndex]);
        }

        [TestMethod]
        public void IndexerSet_ByObjects_Internal()
        {
            var @is = _testClassType.IndexerSet(typeof(double), typeof(double));
            Assert.IsNotNull(@is);
            @is(_testClassInstance, new object[] { FirstDoubleIndex }, NewDoubleValue);
            Assert.AreEqual(NewDoubleValue, _testClassInstance.InternalIndexer);
        }

        [TestMethod]
        public void IndexerSet_ByObjects_Internal_FromStruct()
        {
            var @is = _testStrucType.IndexerSetStruct(typeof(double), typeof(double));
            Assert.IsNotNull(@is);
            var objectStruct = (object)_testStructInstance;
            @is(ref objectStruct, new object[] { FirstDoubleIndex }, NewDoubleValue);
            Assert.AreEqual(NewDoubleValue, ((TestStruct)objectStruct).InternalIndexer);
        }

        [TestMethod]
        public void IndexerSet_ByObjects_NonExisting()
        {
            var @is = _testClassType.IndexerSet(typeof(ulong), typeof(ulong));
            Assert.IsNull(@is);
        }

        [TestMethod]
        public void IndexerSet_ByObjects_OnlyRead()
        {
            var @is = _testClassType.IndexerSet(typeof(string), typeof(string));
            Assert.IsNull(@is);
        }

        [TestMethod]
        public void IndexerSet_ByObjects_Private()
        {
            var @is = _testClassType.IndexerSet(typeof(int), typeof(int), typeof(int));
            Assert.IsNotNull(@is);
            @is(_testClassInstance, new object[] { FirstIntIndex, SecondIntIndex }, NewIntValue);
            Assert.AreEqual(NewIntValue, _testClassInstance.PrivateIndexer);
        }

        [TestMethod]
        public void IndexerSet_ByObjects_Private_FromStruct()
        {
            var @is = _testStrucType.IndexerSetStruct(typeof(int), typeof(int), typeof(int));
            Assert.IsNotNull(@is);
            var objectStruct = (object)_testStructInstance;
            @is(ref objectStruct, new object[] { FirstIntIndex, SecondIntIndex }, NewIntValue);
            Assert.AreEqual(NewIntValue, ((TestStruct)objectStruct).PrivateIndexer);
        }

        [TestMethod]
        public void IndexerSet_ByObjects_Protected()
        {
            var @is = _testClassType.IndexerSet(typeof(byte), typeof(byte));
            Assert.IsNotNull(@is);
            @is(_testClassInstance, new object[] { FirstByteIndex }, NewByteValue);
            Assert.AreEqual(NewByteValue, _testClassInstance.ProtectedIndexer);
        }

        [TestMethod]
        public void IndexerSet_ByObjects_Public()
        {
            var @is = _testClassType.IndexerSet(typeof(int), typeof(int));
            Assert.IsNotNull(@is);
            @is(_testClassInstance, new object[] { FirstIntIndex }, NewIntValue);
            Assert.AreEqual(NewIntValue, _testClassInstance.IndexerBackend[FirstIntIndex]);
        }


        [TestMethod]
        public void IndexerSet_ByObjects_Public_FromStruct()
        {
            var @is = _testStrucType.IndexerSetStruct(typeof(int), typeof(int));
            Assert.IsNotNull(@is);
            var objectStruct = (object)_testStructInstance;
            @is(ref objectStruct, new object[] { FirstIntIndex }, NewIntValue);
            Assert.AreEqual(NewIntValue, ((TestStruct)objectStruct).IndexerBackend[FirstIntIndex]);
        } 
#endif

        [TestMethod]
        public void IndexerSet_ByTypes_Internal()
        {
            var @is = DelegateFactory.IndexerSet<TestClass, double, double>();
            Assert.IsNotNull(@is);
            @is(_testClassInstance, FirstDoubleIndex, NewDoubleValue);
            Assert.AreEqual(NewDoubleValue, _testClassInstance.InternalIndexer);
        }

        [TestMethod]
        public void IndexerSet_ByTypes_Internal_FromStruct()
        {
            var @is = DelegateFactory.IndexerSetStruct<TestStruct, double, double>();
            Assert.IsNotNull(@is);
            @is(ref _testStructInstance, FirstDoubleIndex, NewDoubleValue);
            Assert.AreEqual(NewDoubleValue, _testStructInstance.InternalIndexer);
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
            @is(_testClassInstance, FirstIntIndex, SecondIntIndex, NewIntValue);
            Assert.AreEqual(NewIntValue, _testClassInstance.PrivateIndexer);
        }

        [TestMethod]
        public void IndexerSet_ByTypes_Private_FromStruct()
        {
            var @is = DelegateFactory.IndexerSetStruct<TestStruct, int, int, int>();
            Assert.IsNotNull(@is);
            @is(ref _testStructInstance, FirstIntIndex, SecondIntIndex, NewIntValue);
            Assert.AreEqual(NewIntValue, _testStructInstance.PrivateIndexer);
        }

        [TestMethod]
        public void IndexerSet_ByTypes_Protected()
        {
            var @is = DelegateFactory.IndexerSet<TestClass, byte, byte>();
            Assert.IsNotNull(@is);
            @is(_testClassInstance, FirstByteIndex, NewByteValue);
            Assert.AreEqual(NewByteValue, _testClassInstance.ProtectedIndexer);
        }

        [TestMethod]
        public void IndexerSet_ByTypes_Public()
        {
            var @is = DelegateFactory.IndexerSet<TestClass, int, int>();
            Assert.IsNotNull(@is);
            @is(_testClassInstance, FirstIntIndex, NewIntValue);
            Assert.AreEqual(NewIntValue, _testClassInstance.IndexerBackend[FirstIntIndex]);
        }

        [TestMethod]
        public void IndexerSet_ByTypes_Public_FromStruct()
        {
            var @is = DelegateFactory.IndexerSetStruct<TestStruct, int, int>();
            Assert.IsNotNull(@is);
            @is(ref _testStructInstance, FirstIntIndex, NewIntValue);
            Assert.AreEqual(NewIntValue, _testStructInstance.IndexerBackend[FirstIntIndex]);
        }

        [TestMethod]
        public void IndexerSet_ByTypes_1Index()
        {
            var @is = DelegateFactory.IndexerSet<TestClass, int, int>();
            Assert.IsNotNull(@is);
            @is(_testClassInstance, FirstIntIndex, NewIntValue);
            Assert.AreEqual(NewIntValue, _testClassInstance.IndexerBackend[FirstIntIndex]);
        }

        [TestMethod]
        public void IndexerSet_ByTypes_1Index_FromStruct()
        {
            var @is = DelegateFactory.IndexerSetStruct<TestStruct, int, int>();
            Assert.IsNotNull(@is);
            @is(ref _testStructInstance, FirstIntIndex, NewIntValue);
            Assert.AreEqual(NewIntValue, _testStructInstance.IndexerBackend[FirstIntIndex]);
        }

        [TestMethod]
        public void IndexerSet_ByTypes_2Index()
        {
            var @is = DelegateFactory.IndexerSet<TestClass, int, int, int>();
            Assert.IsNotNull(@is);
            @is(_testClassInstance, FirstIntIndex, SecondIntIndex, NewIntValue);
            Assert.AreEqual(NewIntValue, _testClassInstance.PrivateIndexer);
        }

        [TestMethod]
        public void IndexerSet_ByTypes_2Index_FromStruct()
        {
            var @is = DelegateFactory.IndexerSetStruct<TestStruct, int, int, int>();
            Assert.IsNotNull(@is);
            @is(ref _testStructInstance, FirstIntIndex, SecondIntIndex, NewIntValue);
            Assert.AreEqual(NewIntValue, _testStructInstance.PrivateIndexer);
        }

        [TestMethod]
        public void IndexerSet_ByTypes_3Index()
        {
            var @is = DelegateFactory.IndexerSet<TestClass, int, int, int, int>();
            Assert.IsNotNull(@is);
            @is(_testClassInstance, FirstIntIndex, SecondIntIndex, 0, NewIntValue);
            Assert.AreEqual(NewIntValue, _testClassInstance.Public3IndexIndexer);
        }

        [TestMethod]
        public void IndexerSet_ByTypes_3Index_FromStruct()
        {
            var @is = DelegateFactory.IndexerSetStruct<TestStruct, int, int, int, int>();
            Assert.IsNotNull(@is);
            @is(ref _testStructInstance, FirstIntIndex, SecondIntIndex, 0, NewIntValue);
            Assert.AreEqual(NewIntValue, _testStructInstance.Public3IndexIndexer);
        }


        [TestMethod]
        public void IndexerSet_ByExtensionAndReturnType_1Index()
        {
            var @is = _testClassType.IndexerSet<int, int>();
            Assert.IsNotNull(@is);
            @is(_testClassInstance, FirstIntIndex, NewIntValue);
            Assert.AreEqual(NewIntValue, _testClassInstance.IndexerBackend[FirstIntIndex]);
        }

#if !NET35
        [TestMethod]
        public void IndexerSet_ByExtensionAndReturnType_1Index_FromStruct()
        {
            var @is = _testStrucType.IndexerSetStruct<int, int>();
            Assert.IsNotNull(@is);
            var objectStruct = (object)_testStructInstance;
            @is(ref objectStruct, FirstIntIndex, NewIntValue);
            Assert.AreEqual(NewIntValue, ((TestStruct)objectStruct).IndexerBackend[FirstIntIndex]);
        }
#endif

        [TestMethod]
        public void IndexerSet_ByExtensionAndReturnType_2Index()
        {
            var @is = _testClassType.IndexerSet<int, int, int>();
            Assert.IsNotNull(@is);
            @is(_testClassInstance, FirstIntIndex, SecondIntIndex, NewIntValue);
            Assert.AreEqual(NewIntValue, _testClassInstance.PrivateIndexer);
        }

#if !NET35
        [TestMethod]
        public void IndexerSet_ByExtensionAndReturnType_2Index_FromStruct()
        {
            var @is = _testStrucType.IndexerSetStruct<int, int, int>();
            Assert.IsNotNull(@is);
            var objectStruct = (object)_testStructInstance;
            @is(ref objectStruct, FirstIntIndex, SecondIntIndex, NewIntValue);
            Assert.AreEqual(NewIntValue, ((TestStruct)objectStruct).PrivateIndexer);
        }
#endif

        [TestMethod]
        public void IndexerSet_ByExtensionAndReturnType_3Index()
        {
            var @is = _testClassType.IndexerSet<int, int, int, int>();
            Assert.IsNotNull(@is);
            @is(_testClassInstance, FirstIntIndex, SecondIntIndex, 0, NewIntValue);
            Assert.AreEqual(NewIntValue, _testClassInstance.Public3IndexIndexer);
        }

#if !NET35
        [TestMethod]
        public void IndexerSet_ByExtensionAndReturnType_3Index_FromStruct()
        {
            var @is = _testStrucType.IndexerSetStruct<int, int, int, int>();
            Assert.IsNotNull(@is);
            var objectStruct = (object)_testStructInstance;
            @is(ref objectStruct, FirstIntIndex, SecondIntIndex, 0, NewIntValue);
            Assert.AreEqual(NewIntValue, ((TestStruct)objectStruct).Public3IndexIndexer);
        }

        [TestMethod]
        public void IndexerSet_ByObjects_1Index()
        {
            var @is = _testClassType.IndexerSet(typeof(int), typeof(int));
            Assert.IsNotNull(@is);
            @is(_testClassInstance, new object[] {FirstIntIndex}, NewIntValue);
            Assert.AreEqual(NewIntValue, _testClassInstance.IndexerBackend[FirstIntIndex]);
        }

        [TestMethod]
        public void IndexerSet_ByObjects_1Index_FromStruct()
        {
            var @is = _testStrucType.IndexerSetStruct(typeof(int), typeof(int));
            Assert.IsNotNull(@is);
            var objectStruct = (object)_testStructInstance;
            @is(ref objectStruct, new object[] {FirstIntIndex}, NewIntValue);
            Assert.AreEqual(NewIntValue, ((TestStruct)objectStruct).IndexerBackend[FirstIntIndex]);
        }

        [TestMethod]
        public void IndexerSet_ByObjects_2Index()
        {
            var @is = _testClassType.IndexerSet(typeof(int), typeof(int), typeof(int));
            Assert.IsNotNull(@is);
            @is(_testClassInstance, new object[] {FirstIntIndex, SecondIntIndex}, NewIntValue);
            Assert.AreEqual(NewIntValue, _testClassInstance.PrivateIndexer);
        }

        [TestMethod]
        public void IndexerSet_ByObjects_2Index_FromStruct()
        {
            var @is = _testStrucType.IndexerSetStruct(typeof(int), typeof(int), typeof(int));
            Assert.IsNotNull(@is);
            var objectStruct = (object)_testStructInstance;
            @is(ref objectStruct, new object[] {FirstIntIndex, SecondIntIndex}, NewIntValue);
            Assert.AreEqual(NewIntValue, ((TestStruct)objectStruct).PrivateIndexer);
        }

        [TestMethod]
        public void IndexerSet_ByObjects_3Index()
        {
            var @is = _testClassType.IndexerSet(typeof(int), typeof(int), typeof(int), typeof(int));
            Assert.IsNotNull(@is);
            @is(_testClassInstance, new object[] {FirstIntIndex, SecondIntIndex, 0}, NewIntValue);
            Assert.AreEqual(NewIntValue, _testClassInstance.Public3IndexIndexer);
        }

        [TestMethod]
        public void IndexerSet_ByObjects_3Index_FromStruct()
        {
            var @is = _testStrucType.IndexerSetStruct(typeof(int), typeof(int), typeof(int), typeof(int));
            Assert.IsNotNull(@is);
            var objectStruct = (object)_testStructInstance;
            @is(ref objectStruct, new object[] {FirstIntIndex, SecondIntIndex, 0}, NewIntValue);
            Assert.AreEqual(NewIntValue, ((TestStruct)objectStruct).Public3IndexIndexer);
        }

        [TestMethod]
        public void IndexerSet_ByObjects_4Index()
        {
            var @is = _testClassType.IndexerSet(typeof(int), typeof(int), typeof(int), typeof(int), typeof(int));
            Assert.IsNotNull(@is);
            @is(_testClassInstance, new object[] {FirstIntIndex, SecondIntIndex, 0, 0}, NewIntValue);
            Assert.AreEqual(NewIntValue, _testClassInstance.Public4IndexIndexer);
        }

        [TestMethod]
        public void IndexerSet_ByObjects_4Index_FromStruct()
        {
            var @is = _testStrucType.IndexerSetStruct(typeof(int), typeof(int), typeof(int), typeof(int), typeof(int));
            Assert.IsNotNull(@is);
            var objectStruct = (object)_testStructInstance;
            @is(ref objectStruct, new object[] {FirstIntIndex, SecondIntIndex, 0, 0}, NewIntValue);
            Assert.AreEqual(NewIntValue, ((TestStruct)objectStruct).Public4IndexIndexer);
        }
#endif
    }
}