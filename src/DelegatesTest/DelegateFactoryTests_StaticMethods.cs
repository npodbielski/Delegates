// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DelegateFactoryTests_StaticMethods.cs" company="Natan Podbielski">
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
#elif NETCOREAPP1_0
    DelegatesTestNETCORE10
#elif NETCOREAPP2_0
        DelegatesTestNETCORE20
#elif NETSTANDARD1_1
        DelegatesTestNETStandard11
#elif NETSTANDARD1_5
        DelegatesTestNETStandard15
#endif
{
    [TestClass]
    public class DelegateFactoryTests_StaticMethods
    {
        private const string TestValue = "test";

        //TODO: test static methods with out or ref modifiers
        [TestMethod]
        public void Public_Method_ByTypes_Void_NoParameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Action>("StaticVoidPublic");
            Assert.IsNotNull(m);
            m();
            Assert.AreEqual(true, TestClass.StaticVoidPublicExecuted);
        }

        [TestMethod]
        public void Internal_Method_ByTypes_Void_NoParameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Action>("StaticVoidInternal");
            Assert.IsNotNull(m);
            m();
            Assert.AreEqual(true, TestClass.StaticVoidInternalExecuted);
        }

        [TestMethod]
        public void Private_Method_ByTypes_Void_NoParameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Action>("StaticVoidPrivate");
            Assert.IsNotNull(m);
            m();
            Assert.AreEqual(true, TestClass.StaticVoidPrivateExecuted);
        }

        [TestMethod]
        public void Protected_Method_ByTypes_Void_NoParameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Action>("StaticVoidProtected");
            Assert.IsNotNull(m);
            m();
            Assert.AreEqual(true, TestClass.StaticVoidProtectedExecuted);
        }

        [TestMethod]
        public void Public_Method_ByTypes_Void_SingleParameter()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Action<string>>("StaticVoidPublic");
            Assert.IsNotNull(m);
            m(TestValue);
            Assert.AreEqual(TestValue, TestClass.StaticVoidPublicParam);
        }

        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_NoParameter()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<string>>("StaticPublic");
            Assert.IsNotNull(m);
            var result = m();
            Assert.AreEqual(TestClass.StaticPublicParameterlessReturnValue, result);
        }

        [TestMethod]
        public void Internal_Method_ByTypes_NoVoid_NoParameter()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<string>>("StaticInternal");
            Assert.IsNotNull(m);
            var result = m();
            Assert.AreEqual(TestClass.StaticInternalParameterlessReturnValue, result);
        }


        [TestMethod]
        public void Protected_Method_ByTypes_NoVoid_NoParameter()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<string>>("StaticProtected");
            Assert.IsNotNull(m);
            var result = m();
            Assert.AreEqual(TestClass.StaticProtectedParameterlessReturnValue, result);
        }


        [TestMethod]
        public void Private_Method_ByTypes_NoVoid_NoParameter()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<string>>("StaticPrivate");
            Assert.IsNotNull(m);
            var result = m();
            Assert.AreEqual(TestClass.StaticPrivateParameterlessReturnValue, result);
        }

        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_SingleParameter()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<string, string>>("StaticPublic");
            Assert.IsNotNull(m);
            var result = m(TestValue);
            Assert.AreEqual(TestValue, result);
        }

        [TestMethod]
        public void Internal_Method_ByTypes_NoVoid_SingleParameter()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<string, string>>("StaticInternal");
            Assert.IsNotNull(m);
            var result = m(TestValue);
            Assert.AreEqual(TestValue, result);
        }


        [TestMethod]
        public void Protected_Method_ByTypes_NoVoid_SingleParameter()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<string, string>>("StaticProtected");
            Assert.IsNotNull(m);
            var result = m(TestValue);
            Assert.AreEqual(TestValue, result);
        }


        [TestMethod]
        public void Private_Method_ByTypes_NoVoid_SingleParameter()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<string, string>>("StaticPrivate");
            Assert.IsNotNull(m);
            var result = m(TestValue);
            Assert.AreEqual(TestValue, result);
        }

        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_2Parameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<string, string, string>>("StaticPublic");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_3Parameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<string, string, string, string>>("StaticPublic");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_4Parameters()
        {
            var m =
                DelegateFactory.StaticMethod<TestClass, Func<string, string, string, string, string>>("StaticPublic");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_17Parameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<string, string, string, string, string, string,
                    string, string, string, string, string, string, string, string, string, string, string, string>>
                ("StaticPublic");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByTypes_Void_2Parameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Action<string, string>>("StaticVoidPublic");
            Assert.IsNotNull(m);
            var index = 0;
            m(TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByTypes_Void_3Parameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Action<string, string, string>>("StaticVoidPublic");
            Assert.IsNotNull(m);
            var index = 0;
            m(TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByTypes_Void_4Parameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Action<string, string, string, string>>("StaticVoidPublic");
            Assert.IsNotNull(m);
            var index = 0;
            m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByTypes_Void_17Parameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Action<string, string, string, string, string, string,
                    string, string, string, string, string, string, string, string, string, string, string>>
                ("StaticVoidPublic");
            Assert.IsNotNull(m);
            var index = 0;
            m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
        }

        [TestMethod]
        public void Internal_Method_ByTypes_Void_SingleParameter()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Action<string>>("StaticVoidInternal");
            Assert.IsNotNull(m);
            m(TestValue);
            Assert.AreEqual(TestValue, TestClass.StaticVoidInternalParam);
        }

        [TestMethod]
        public void Protected_Method_ByTypes_Void_SingleParameter()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Action<string>>("StaticVoidProtected");
            Assert.IsNotNull(m);
            m(TestValue);
            Assert.AreEqual(TestValue, TestClass.StaticVoidProtectedParam);
        }

        [TestMethod]
        public void Private_Method_ByTypes_Void_SingleParameter()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Action<string>>("StaticVoidPrivate");
            Assert.IsNotNull(m);
            m(TestValue);
            Assert.AreEqual(TestValue, TestClass.StaticVoidPrivateParam);
        }

        [TestMethod]
        public void Method_NonExisting_WrongName_ByTypes()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Action>("NonExisting");
            Assert.IsNull(m);
        }

        [TestMethod]
        public void Method_NonExisting_WrongParams_ByTypes()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Action<TestStruct>>("StaticVoidPublic");
            Assert.IsNull(m);
        }

        [TestMethod]
        public void Method_ByTypes_Wrong_TDelegate_Type()
        {
            AssertHelper.ThrowsException<ArgumentException>(
                () => DelegateFactory.StaticMethod<TestClass, string>("NonExisting"));
        }

        [TestMethod]
        public void Public_Method_ByTypes_VoidDelegate_For_NonVoidMethod()
        {
            AssertHelper.ThrowsException<ArgumentException>(() =>
                DelegateFactory.StaticMethod<TestClass, Action<string>>("StaticPublic"));
        }

        [TestMethod]
        public void Public_Method_ByTypes_NoVoidDelegate_For_VoidMethod()
        {
            AssertHelper.ThrowsException<ArgumentException>(() =>
                DelegateFactory.StaticMethod<TestClass, Func<string>>("StaticVoidPublic"));
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_Void_NoParameters()
        {
            var m = typeof(TestClass).StaticMethod<Action>("StaticVoidPublic");
            Assert.IsNotNull(m);
            m();
            Assert.AreEqual(true, TestClass.StaticVoidPublicExecuted);
        }

        [TestMethod]
        public void Internal_Method_ByObjectAndTypes_Void_NoParameters()
        {
            var m = typeof(TestClass).StaticMethod<Action>("StaticVoidInternal");
            Assert.IsNotNull(m);
            m();
            Assert.AreEqual(true, TestClass.StaticVoidInternalExecuted);
        }

        [TestMethod]
        public void Private_Method_ByObjectAndTypes_Void_NoParameters()
        {
            var m = typeof(TestClass).StaticMethod<Action>("StaticVoidPrivate");
            Assert.IsNotNull(m);
            m();
            Assert.AreEqual(true, TestClass.StaticVoidPrivateExecuted);
        }

        [TestMethod]
        public void Protected_Method_ByObjectAndTypes_Void_NoParameters()
        {
            var m = typeof(TestClass).StaticMethod<Action>("StaticVoidProtected");
            Assert.IsNotNull(m);
            m();
            Assert.AreEqual(true, TestClass.StaticVoidProtectedExecuted);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_Void_SingleParameter()
        {
            var m = typeof(TestClass).StaticMethod<Action<string>>("StaticVoidPublic");
            Assert.IsNotNull(m);
            m(TestValue);
            Assert.AreEqual(TestValue, TestClass.StaticVoidPublicParam);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_NoParameter()
        {
            var m = typeof(TestClass).StaticMethod<Func<string>>("StaticPublic");
            Assert.IsNotNull(m);
            var result = m();
            Assert.AreEqual(TestClass.StaticPublicParameterlessReturnValue, result);
        }

        [TestMethod]
        public void Internal_Method_ByObjectAndTypes_NoVoid_NoParameter()
        {
            var m = typeof(TestClass).StaticMethod<Func<string>>("StaticInternal");
            Assert.IsNotNull(m);
            var result = m();
            Assert.AreEqual(TestClass.StaticInternalParameterlessReturnValue, result);
        }


        [TestMethod]
        public void Protected_Method_ByObjectAndTypes_NoVoid_NoParameter()
        {
            var m = typeof(TestClass).StaticMethod<Func<string>>("StaticProtected");
            Assert.IsNotNull(m);
            var result = m();
            Assert.AreEqual(TestClass.StaticProtectedParameterlessReturnValue, result);
        }


        [TestMethod]
        public void Private_Method_ByObjectAndTypes_NoVoid_NoParameter()
        {
            var m = typeof(TestClass).StaticMethod<Func<string>>("StaticPrivate");
            Assert.IsNotNull(m);
            var result = m();
            Assert.AreEqual(TestClass.StaticPrivateParameterlessReturnValue, result);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_SingleParameter()
        {
            var m = typeof(TestClass).StaticMethod<Func<string, string>>("StaticPublic");
            Assert.IsNotNull(m);
            var result = m(TestValue);
            Assert.AreEqual(TestValue, result);
        }

        [TestMethod]
        public void Internal_Method_ByObjectAndTypes_NoVoid_SingleParameter()
        {
            var m = typeof(TestClass).StaticMethod<Func<string, string>>("StaticInternal");
            Assert.IsNotNull(m);
            var result = m(TestValue);
            Assert.AreEqual(TestValue, result);
        }


        [TestMethod]
        public void Protected_Method_ByObjectAndTypes_NoVoid_SingleParameter()
        {
            var m = typeof(TestClass).StaticMethod<Func<string, string>>("StaticProtected");
            Assert.IsNotNull(m);
            var result = m(TestValue);
            Assert.AreEqual(TestValue, result);
        }


        [TestMethod]
        public void Private_Method_ByObjectAndTypes_NoVoid_SingleParameter()
        {
            var m = typeof(TestClass).StaticMethod<Func<string, string>>("StaticPrivate");
            Assert.IsNotNull(m);
            var result = m(TestValue);
            Assert.AreEqual(TestValue, result);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_2Parameters()
        {
            var m = typeof(TestClass).StaticMethod<Func<string, string, string>>("StaticPublic");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_3Parameters()
        {
            var m = typeof(TestClass).StaticMethod<Func<string, string, string, string>>("StaticPublic");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_4Parameters()
        {
            var m = typeof(TestClass).StaticMethod<Func<string, string, string, string, string>>("StaticPublic");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_17Parameters()
        {
            var m = typeof(TestClass).StaticMethod<Func<string, string, string, string, string, string,
                    string, string, string, string, string, string, string, string, string, string, string, string>>
                ("StaticPublic");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_Void_2Parameters()
        {
            var m = typeof(TestClass).StaticMethod<Action<string, string>>("StaticVoidPublic");
            Assert.IsNotNull(m);
            var index = 0;
            m(TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_Void_3Parameters()
        {
            var m = typeof(TestClass).StaticMethod<Action<string, string, string>>("StaticVoidPublic");
            Assert.IsNotNull(m);
            var index = 0;
            m(TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_Void_4Parameters()
        {
            var m = typeof(TestClass).StaticMethod<Action<string, string, string, string>>("StaticVoidPublic");
            Assert.IsNotNull(m);
            var index = 0;
            m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_Void_17Parameters()
        {
            var m = typeof(TestClass).StaticMethod<Action<string, string, string, string, string, string,
                    string, string, string, string, string, string, string, string, string, string, string>>
                ("StaticVoidPublic");
            Assert.IsNotNull(m);
            var index = 0;
            m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
        }

        [TestMethod]
        public void Internal_Method_ByObjectAndTypes_Void_SingleParameter()
        {
            var m = typeof(TestClass).StaticMethod<Action<string>>("StaticVoidInternal");
            Assert.IsNotNull(m);
            m(TestValue);
            Assert.AreEqual(TestValue, TestClass.StaticVoidInternalParam);
        }

        [TestMethod]
        public void Protected_Method_ByObjectAndTypes_Void_SingleParameter()
        {
            var m = typeof(TestClass).StaticMethod<Action<string>>("StaticVoidProtected");
            Assert.IsNotNull(m);
            m(TestValue);
            Assert.AreEqual(TestValue, TestClass.StaticVoidProtectedParam);
        }

        [TestMethod]
        public void Private_Method_ByObjectAndTypes_Void_SingleParameter()
        {
            var m = typeof(TestClass).StaticMethod<Action<string>>("StaticVoidPrivate");
            Assert.IsNotNull(m);
            m(TestValue);
            Assert.AreEqual(TestValue, TestClass.StaticVoidPrivateParam);
        }

        [TestMethod]
        public void Method_NonExisting_WrongName_ByObjectAndTypes()
        {
            var m = typeof(TestClass).StaticMethod<Action>("NonExisting");
            Assert.IsNull(m);
        }

        [TestMethod]
        public void Method_NonExisting_WrongParams_ByObjectAndTypes()
        {
            var m = typeof(TestClass).StaticMethod<Action<TestStruct>>("StaticVoidPublic");
            Assert.IsNull(m);
        }

        [TestMethod]
        public void Method_ByObjectAndTypes_Wrong_TDelegate_Type()
        {
            AssertHelper.ThrowsException<ArgumentException>(
                () => typeof(TestClass).StaticMethod<TestClass, string>("NonExisting"));
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_VoidDelegate_For_NonVoidMethod()
        {
            AssertHelper.ThrowsException<ArgumentException>(() =>
                typeof(TestClass).StaticMethod<Action<string>>("StaticPublic"));
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoidDelegate_For_VoidMethod()
        {
            AssertHelper.ThrowsException<ArgumentException>(() =>
                typeof(TestClass).StaticMethod<Func<string>>("StaticVoidPublic"));
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_NoParameters()
        {
            var m = typeof(TestClass).StaticMethodVoid("StaticVoidPublic");
            Assert.IsNotNull(m);
            m(new object[] { });
            Assert.AreEqual(true, TestClass.StaticVoidPublicExecuted);
        }

        [TestMethod]
        public void Internal_Method_ByObjecs_Void_NoParameters()
        {
            var m = typeof(TestClass).StaticMethodVoid("StaticVoidInternal");
            Assert.IsNotNull(m);
            m(new object[] { });
            Assert.AreEqual(true, TestClass.StaticVoidInternalExecuted);
        }

        [TestMethod]
        public void Private_Method_ByObjecs_Void_NoParameters()
        {
            var m = typeof(TestClass).StaticMethodVoid("StaticVoidPrivate");
            Assert.IsNotNull(m);
            m(new object[] { });
            Assert.AreEqual(true, TestClass.StaticVoidPrivateExecuted);
        }

        [TestMethod]
        public void Protected_Method_ByObjecs_Void_NoParameters()
        {
            var m = typeof(TestClass).StaticMethodVoid("StaticVoidProtected");
            Assert.IsNotNull(m);
            m(new object[] { });
            Assert.AreEqual(true, TestClass.StaticVoidProtectedExecuted);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_SingleParameter()
        {
            var m = typeof(TestClass).StaticMethodVoid("StaticVoidPublic", typeof(string));
            Assert.IsNotNull(m);
            m(new[] {TestValue});
            Assert.AreEqual(TestValue, TestClass.StaticVoidPublicParam);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_SingleParameter_PassedNoParameters()
        {
            var m = typeof(TestClass).StaticMethodVoid("StaticVoidPublic", typeof(string));
            Assert.IsNotNull(m);
            AssertHelper.ThrowsException<IndexOutOfRangeException>(() => m(new object[] { }));
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_SingleParameter_PassedMoreParameters()
        {
            var m = typeof(TestClass).StaticMethodVoid("StaticVoidPublic", typeof(string));
            Assert.IsNotNull(m);
            m(new[] {TestValue, TestValue + 1});
            Assert.AreEqual(TestValue, TestClass.StaticVoidPublicParam);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_SingleParameter_PassedNull()
        {
            var m = typeof(TestClass).StaticMethodVoid("StaticVoidPublic", typeof(string));
            Assert.IsNotNull(m);
            AssertHelper.ThrowsException<NullReferenceException>(() => m(null));
        }

        [TestMethod]
        public void Public_Method_ByObjecs_WrongParameterType()
        {
            var m = typeof(TestClass).StaticMethodVoid("StaticVoidPublic", typeof(string));
            Assert.IsNotNull(m);
            AssertHelper.ThrowsException<InvalidCastException>(() => m(new object[] {0}));
        }

        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_NoParameter()
        {
            var m = typeof(TestClass).StaticMethod("StaticPublic");
            Assert.IsNotNull(m);
            var result = m(new object[0]);
            Assert.AreEqual(TestClass.StaticPublicParameterlessReturnValue, result);
        }

        [TestMethod]
        public void Internal_Method_ByObjecs_NoVoid_NoParameter()
        {
            var m = typeof(TestClass).StaticMethod("StaticInternal");
            Assert.IsNotNull(m);
            var result = m(new object[0]);
            Assert.AreEqual(TestClass.StaticInternalParameterlessReturnValue, result);
        }


        [TestMethod]
        public void Protected_Method_ByObjecs_NoVoid_NoParameter()
        {
            var m = typeof(TestClass).StaticMethod("StaticProtected");
            Assert.IsNotNull(m);
            var result = m(new object[0]);
            Assert.AreEqual(TestClass.StaticProtectedParameterlessReturnValue, result);
        }


        [TestMethod]
        public void Private_Method_ByObjecs_NoVoid_NoParameter()
        {
            var m = typeof(TestClass).StaticMethod("StaticPrivate");
            Assert.IsNotNull(m);
            var result = m(new object[0]);
            Assert.AreEqual(TestClass.StaticPrivateParameterlessReturnValue, result);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_SingleParameter()
        {
            var m = typeof(TestClass).StaticMethod("StaticPublic", typeof(string));
            Assert.IsNotNull(m);
            var result = m(new[] {TestValue});
            Assert.AreEqual(TestValue, result);
        }

        [TestMethod]
        public void Internal_Method_ByObjecs_NoVoid_SingleParameter()
        {
            var m = typeof(TestClass).StaticMethod("StaticInternal", typeof(string));
            Assert.IsNotNull(m);
            var result = m(new[] {TestValue});
            Assert.AreEqual(TestValue, result);
        }


        [TestMethod]
        public void Protected_Method_ByObjecs_NoVoid_SingleParameter()
        {
            var m = typeof(TestClass).StaticMethod("StaticProtected", typeof(string));
            Assert.IsNotNull(m);
            var result = m(new[] {TestValue});
            Assert.AreEqual(TestValue, result);
        }


        [TestMethod]
        public void Private_Method_ByObjecs_NoVoid_SingleParameter()
        {
            var m = typeof(TestClass).StaticMethod("StaticPrivate", typeof(string));
            Assert.IsNotNull(m);
            var result = m(new[] {TestValue});
            Assert.AreEqual(TestValue, result);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_2Parameters()
        {
            var m = typeof(TestClass).StaticMethod("StaticPublic", typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(new object[] {TestValue + index++, TestValue + index++});
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_3Parameters()
        {
            var m = typeof(TestClass).StaticMethod("StaticPublic", typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(new object[] {TestValue + index++, TestValue + index++, TestValue + index++});
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_4Parameters()
        {
            var m = typeof(TestClass).StaticMethod("StaticPublic", typeof(string), typeof(string), typeof(string),
                typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(new object[]
                {TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++});
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
        }


        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_5Parameters()
        {
            var m = typeof(TestClass).StaticMethod("StaticPublic", typeof(string), typeof(string), typeof(string),
                typeof(string),
                typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(new object[]
            {
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
            });
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_6Parameters()
        {
            var m = typeof(TestClass).StaticMethod("StaticPublic", typeof(string), typeof(string), typeof(string),
                typeof(string),
                typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(new object[]
            {
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++
            });
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_7Parameters()
        {
            var m = typeof(TestClass).StaticMethod("StaticPublic", typeof(string), typeof(string), typeof(string),
                typeof(string),
                typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(new object[]
            {
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++
            });
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_8Parameters()
        {
            var m = typeof(TestClass).StaticMethod("StaticPublic", typeof(string), typeof(string), typeof(string),
                typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(new object[]
            {
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
            });
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_9Parameters()
        {
            var m = typeof(TestClass).StaticMethod("StaticPublic", typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(new object[]
            {
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
            });
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
        }


        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_10Parameters()
        {
            var m = typeof(TestClass).StaticMethod("StaticPublic", typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
                typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(new object[]
            {
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
            });
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_11Parameters()
        {
            var m = typeof(TestClass).StaticMethod("StaticPublic", typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(new object[]
            {
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++
            });
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_12Parameters()
        {
            var m = typeof(TestClass).StaticMethod("StaticPublic", typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(new object[]
            {
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++
            });
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_13Parameters()
        {
            var m = typeof(TestClass).StaticMethod("StaticPublic", typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(new object[]
            {
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
            });
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_14Parameters()
        {
            var m = typeof(TestClass).StaticMethod("StaticPublic", typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(new object[]
            {
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
            });
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_15Parameters()
        {
            var m = typeof(TestClass).StaticMethod("StaticPublic", typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(new object[]
            {
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
            });
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_16Parameters()
        {
            var m = typeof(TestClass).StaticMethod("StaticPublic", typeof(string), typeof(string), typeof(string),
                typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
                typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(new object[]
            {
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++
            });
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_NoVoid_17Parameters()
        {
            var m = typeof(TestClass).StaticMethod("StaticPublic", typeof(string), typeof(string), typeof(string),
                typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
                typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(new object[]
            {
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++
            });
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_2Parameters()
        {
            var m = typeof(TestClass).StaticMethodVoid("StaticVoidPublic", typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            m(new object[] {TestValue + index++, TestValue + index++});
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_3Parameters()
        {
            var m = typeof(TestClass).StaticMethodVoid("StaticVoidPublic", typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            m(new object[] {TestValue + index++, TestValue + index++, TestValue + index++});
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_4Parameters()
        {
            var m = typeof(TestClass).StaticMethodVoid("StaticVoidPublic", typeof(string), typeof(string), typeof(string),
                typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            m(new object[] {TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++});
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_5Parameters()
        {
            var m = typeof(TestClass).StaticMethodVoid("StaticVoidPublic", typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            m(new object[]
            {
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
            });
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_6Parameters()
        {
            var m = typeof(TestClass).StaticMethodVoid("StaticVoidPublic", typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            m(new object[]
            {
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
            });
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_7Parameters()
        {
            var m = typeof(TestClass).StaticMethodVoid("StaticVoidPublic", typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            m(new object[]
            {
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++
            });
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_8Parameters()
        {
            var m = typeof(TestClass).StaticMethodVoid("StaticVoidPublic", typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            m(new object[]
            {
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++
            });
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_9Parameters()
        {
            var m = typeof(TestClass).StaticMethodVoid("StaticVoidPublic", typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            m(new object[]
            {
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
            });
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_10Parameters()
        {
            var m = typeof(TestClass).StaticMethodVoid("StaticVoidPublic", typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
                typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            m(new object[]
            {
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
            });
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_11Parameters()
        {
            var m = typeof(TestClass).StaticMethodVoid("StaticVoidPublic", typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            m(new object[]
            {
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
            });
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_12Parameters()
        {
            var m = typeof(TestClass).StaticMethodVoid("StaticVoidPublic", typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            m(new object[]
            {
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++
            });
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_13Parameters()
        {
            var m = typeof(TestClass).StaticMethodVoid("StaticVoidPublic", typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            m(new object[]
            {
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++
            });
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_14Parameters()
        {
            var m = typeof(TestClass).StaticMethodVoid("StaticVoidPublic", typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            m(new object[]
            {
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
            });
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_15Parameters()
        {
            var m = typeof(TestClass).StaticMethodVoid("StaticVoidPublic", typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            m(new object[]
            {
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
            });
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_16Parameters()
        {
            var m = typeof(TestClass).StaticMethodVoid("StaticVoidPublic", typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
                typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            m(new object[]
            {
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
            });
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjecs_Void_17Parameters()
        {
            var m = typeof(TestClass).StaticMethodVoid("StaticVoidPublic", typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
                typeof(string), typeof(string));
            Assert.IsNotNull(m);
            var index = 0;
            m(new object[]
            {
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++
            });
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
        }

        [TestMethod]
        public void Internal_Method_ByObjecs_Void_SingleParameter()
        {
            var m = typeof(TestClass).StaticMethod<Action<string>>("StaticVoidInternal");
            Assert.IsNotNull(m);
            m(TestValue);
            Assert.AreEqual(TestValue, TestClass.StaticVoidInternalParam);
        }

        [TestMethod]
        public void Protected_Method_ByObjecs_Void_SingleParameter()
        {
            var m = typeof(TestClass).StaticMethod<Action<string>>("StaticVoidProtected");
            Assert.IsNotNull(m);
            m(TestValue);
            Assert.AreEqual(TestValue, TestClass.StaticVoidProtectedParam);
        }

        [TestMethod]
        public void Private_Method_ByObjecs_Void_SingleParameter()
        {
            var m = typeof(TestClass).StaticMethod<Action<string>>("StaticVoidPrivate");
            Assert.IsNotNull(m);
            m(TestValue);
            Assert.AreEqual(TestValue, TestClass.StaticVoidPrivateParam);
        }

        [TestMethod]
        public void Method_NonExisting_WrongName_ByObjects()
        {
            var m = typeof(TestClass).StaticMethod<Action>("NonExisting");
            Assert.IsNull(m);
        }

        [TestMethod]
        public void Method_NonExisting_WrongParams_ByObjects()
        {
            var m = typeof(TestClass).StaticMethod<Action<TestStruct>>("StaticVoidPublic");
            Assert.IsNull(m);
        }

        [TestMethod]
        public void Method_ByObjecs_Wrong_TDelegate_Type()
        {
            AssertHelper.ThrowsException<ArgumentException>(
                () => typeof(TestClass).StaticMethod<TestClass, string>("NonExisting"));
        }

        [TestMethod]
        public void Public_Method_ByObjects_VoidDelegate_For_NonVoidMethod()
        {
            AssertHelper.ThrowsException<ArgumentException>(() =>
            {
                typeof(TestClass).StaticMethodVoid("StaticPublic", typeof(string));
            });
        }

        [TestMethod]
        public void Public_Method_ByObjects_NoVoidDelegate_For_VoidMethod()
        {
            AssertHelper.ThrowsException<ArgumentException>(() =>
                typeof(TestClass).StaticMethod("StaticVoidPublic", typeof(string)));
        }

        [TestMethod]
        public void Incorrect_TDelegate_For_StaticcMethod()
        {
            AssertHelper.ThrowsException<ArgumentException>(() =>
                typeof(TestClass).StaticMethod<Action>("StaticVoidPublic", null, new[] {typeof(string)}));
        }

#if !NET35
        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_5Parameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<string, string, string, string, string, string>>
                ("StaticPublic");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_6Parameters()
        {
            var m = DelegateFactory
                .StaticMethod<TestClass, Func<string, string, string, string, string, string, string>>
                    ("StaticPublic");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_7Parameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<string, string, string, string, string, string,
                    string, string>>
                ("StaticPublic");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_8Parameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<string, string, string, string, string, string,
                    string, string, string>>
                ("StaticPublic");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_9Parameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<string, string, string, string, string, string,
                    string, string, string, string>>
                ("StaticPublic");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
        }


        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_10Parameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<string, string, string, string, string, string,
                    string, string, string, string, string>>
                ("StaticPublic");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_11Parameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<string, string, string, string, string, string,
                    string, string, string, string, string, string>>
                ("StaticPublic");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_12Parameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<string, string, string, string, string, string,
                    string, string, string, string, string, string, string>>
                ("StaticPublic");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_13Parameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<string, string, string, string, string, string,
                    string, string, string, string, string, string, string, string>>
                ("StaticPublic");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_14Parameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<string, string, string, string, string, string,
                    string, string, string, string, string, string, string, string, string>>
                ("StaticPublic");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_15Parameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<string, string, string, string, string, string,
                    string, string, string, string, string, string, string, string, string, string>>
                ("StaticPublic");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByTypes_NoVoid_16Parameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<string, string, string, string, string, string,
                    string, string, string, string, string, string, string, string, string, string, string>>
                ("StaticPublic");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
        }
#endif

#if !NET35
        [TestMethod]
        public void Public_Method_ByTypes_Void_5Parameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Action<string, string, string, string, string>>
                ("StaticVoidPublic");
            Assert.IsNotNull(m);
            var index = 0;
            m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByTypes_Void_6Parameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Action<string, string, string, string, string, string>>
                ("StaticVoidPublic");
            Assert.IsNotNull(m);
            var index = 0;
            m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++);
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByTypes_Void_7Parameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Action<string, string, string, string, string, string,
                    string>>
                ("StaticVoidPublic");
            Assert.IsNotNull(m);
            var index = 0;
            m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByTypes_Void_8Parameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Action<string, string, string, string, string, string,
                    string, string>>
                ("StaticVoidPublic");
            Assert.IsNotNull(m);
            var index = 0;
            m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByTypes_Void_9Parameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Action<string, string, string, string, string, string,
                    string, string, string>>
                ("StaticVoidPublic");
            Assert.IsNotNull(m);
            var index = 0;
            m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByTypes_Void_10Parameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Action<string, string, string, string, string, string,
                    string, string, string, string>>
                ("StaticVoidPublic");
            Assert.IsNotNull(m);
            var index = 0;
            m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++);
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByTypes_Void_11Parameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Action<string, string, string, string, string, string,
                    string, string, string, string, string>>
                ("StaticVoidPublic");
            Assert.IsNotNull(m);
            var index = 0;
            m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++);
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByTypes_Void_12Parameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Action<string, string, string, string, string, string,
                    string, string, string, string, string, string>>
                ("StaticVoidPublic");
            Assert.IsNotNull(m);
            var index = 0;
            m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByTypes_Void_13Parameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Action<string, string, string, string, string, string,
                    string, string, string, string, string, string, string>>
                ("StaticVoidPublic");
            Assert.IsNotNull(m);
            var index = 0;
            m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByTypes_Void_14Parameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Action<string, string, string, string, string, string,
                    string, string, string, string, string, string, string, string>>
                ("StaticVoidPublic");
            Assert.IsNotNull(m);
            var index = 0;
            m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByTypes_Void_15Parameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Action<string, string, string, string, string, string,
                    string, string, string, string, string, string, string, string, string>>
                ("StaticVoidPublic");
            Assert.IsNotNull(m);
            var index = 0;
            m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++);
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByTypes_Void_16Parameters()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Action<string, string, string, string, string, string,
                    string, string, string, string, string, string, string, string, string, string>>
                ("StaticVoidPublic");
            Assert.IsNotNull(m);
            var index = 0;
            m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++);
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
        }
#endif

#if !NET35
        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_5Parameters()
        {
            var m = typeof(TestClass).StaticMethod<Func<string, string, string, string, string, string>>
                ("StaticPublic");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_6Parameters()
        {
            var m = typeof(TestClass).StaticMethod<Func<string, string, string, string, string, string, string>>
                ("StaticPublic");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_7Parameters()
        {
            var m = typeof(TestClass).StaticMethod<Func<string, string, string, string, string, string,
                    string, string>>
                ("StaticPublic");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_8Parameters()
        {
            var m = typeof(TestClass).StaticMethod<Func<string, string, string, string, string, string,
                    string, string, string>>
                ("StaticPublic");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_9Parameters()
        {
            var m = typeof(TestClass).StaticMethod<Func<string, string, string, string, string, string,
                    string, string, string, string>>
                ("StaticPublic");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
        }


        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_10Parameters()
        {
            var m = typeof(TestClass).StaticMethod<Func<string, string, string, string, string, string,
                    string, string, string, string, string>>
                ("StaticPublic");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_11Parameters()
        {
            var m = typeof(TestClass).StaticMethod<Func<string, string, string, string, string, string,
                    string, string, string, string, string, string>>
                ("StaticPublic");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_12Parameters()
        {
            var m = typeof(TestClass).StaticMethod<Func<string, string, string, string, string, string,
                    string, string, string, string, string, string, string>>
                ("StaticPublic");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_13Parameters()
        {
            var m = typeof(TestClass).StaticMethod<Func<string, string, string, string, string, string,
                    string, string, string, string, string, string, string, string>>
                ("StaticPublic");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_14Parameters()
        {
            var m = typeof(TestClass).StaticMethod<Func<string, string, string, string, string, string,
                    string, string, string, string, string, string, string, string, string>>
                ("StaticPublic");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_15Parameters()
        {
            var m = typeof(TestClass).StaticMethod<Func<string, string, string, string, string, string,
                    string, string, string, string, string, string, string, string, string, string>>
                ("StaticPublic");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_NoVoid_16Parameters()
        {
            var m = typeof(TestClass).StaticMethod<Func<string, string, string, string, string, string,
                    string, string, string, string, string, string, string, string, string, string, string>>
                ("StaticPublic");
            Assert.IsNotNull(m);
            var index = 0;
            var result = m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", result);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticPublicParams[i]);
        }
#endif

#if !NET35
        [TestMethod]
        public void Public_Method_ByObjectAndTypes_Void_5Parameters()
        {
            var m = typeof(TestClass).StaticMethod<Action<string, string, string, string, string>>
                ("StaticVoidPublic");
            Assert.IsNotNull(m);
            var index = 0;
            m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_Void_6Parameters()
        {
            var m = typeof(TestClass).StaticMethod<Action<string, string, string, string, string, string>>
                ("StaticVoidPublic");
            Assert.IsNotNull(m);
            var index = 0;
            m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++);
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_Void_7Parameters()
        {
            var m = typeof(TestClass).StaticMethod<Action<string, string, string, string, string, string,
                    string>>
                ("StaticVoidPublic");
            Assert.IsNotNull(m);
            var index = 0;
            m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_Void_8Parameters()
        {
            var m = typeof(TestClass).StaticMethod<Action<string, string, string, string, string, string,
                    string, string>>
                ("StaticVoidPublic");
            Assert.IsNotNull(m);
            var index = 0;
            m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_Void_9Parameters()
        {
            var m = typeof(TestClass).StaticMethod<Action<string, string, string, string, string, string,
                    string, string, string>>
                ("StaticVoidPublic");
            Assert.IsNotNull(m);
            var index = 0;
            m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_Void_10Parameters()
        {
            var m = typeof(TestClass).StaticMethod<Action<string, string, string, string, string, string,
                    string, string, string, string>>
                ("StaticVoidPublic");
            Assert.IsNotNull(m);
            var index = 0;
            m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++);
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_Void_11Parameters()
        {
            var m = typeof(TestClass).StaticMethod<Action<string, string, string, string, string, string,
                    string, string, string, string, string>>
                ("StaticVoidPublic");
            Assert.IsNotNull(m);
            var index = 0;
            m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++);
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_Void_12Parameters()
        {
            var m = typeof(TestClass).StaticMethod<Action<string, string, string, string, string, string,
                    string, string, string, string, string, string>>
                ("StaticVoidPublic");
            Assert.IsNotNull(m);
            var index = 0;
            m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_Void_13Parameters()
        {
            var m = typeof(TestClass).StaticMethod<Action<string, string, string, string, string, string,
                    string, string, string, string, string, string, string>>
                ("StaticVoidPublic");
            Assert.IsNotNull(m);
            var index = 0;
            m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_Void_14Parameters()
        {
            var m = typeof(TestClass).StaticMethod<Action<string, string, string, string, string, string,
                    string, string, string, string, string, string, string, string>>
                ("StaticVoidPublic");
            Assert.IsNotNull(m);
            var index = 0;
            m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++);
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_Void_15Parameters()
        {
            var m = typeof(TestClass).StaticMethod<Action<string, string, string, string, string, string,
                    string, string, string, string, string, string, string, string, string>>
                ("StaticVoidPublic");
            Assert.IsNotNull(m);
            var index = 0;
            m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++);
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
        }

        [TestMethod]
        public void Public_Method_ByObjectAndTypes_Void_16Parameters()
        {
            var m = typeof(TestClass).StaticMethod<Action<string, string, string, string, string, string,
                    string, string, string, string, string, string, string, string, string, string>>
                ("StaticVoidPublic");
            Assert.IsNotNull(m);
            var index = 0;
            m(TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++, TestValue + index++, TestValue + index++, TestValue + index++,
                TestValue + index++
                , TestValue + index++);
            Assert.AreEqual(TestValue + "0", TestClass.StaticVoidPublicParam);
            for (var i = 0; i < 1; i++) Assert.AreEqual(TestValue + i, TestClass.StaticVoidPublicParams[i]);
        }
#endif
    }
}