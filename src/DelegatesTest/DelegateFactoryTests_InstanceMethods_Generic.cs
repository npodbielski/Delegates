// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DelegateFactoryTests_InstanceMethods_Generic.cs" company="Natan Podbielski">
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
    public class DelegateFactoryTests_InstanceMethods_Generic
    {
        private const string TestValue = "test";

        [TestMethod]
        public void NoGenericMethod_ShouldNot_Return_NonGeneric_With_TheSame_Name()
        {
            var m = DelegateFactory.InstanceMethod<Action<TestClass>, string>("StaticVoidPublic");
            Assert.IsNull(m);
        }

        [TestMethod]
        public void Public_GenericMethod_Void_NoParameter_OneTypeParameter_ByTypes()
        {
            var m = DelegateFactory.InstanceMethod<Action<TestClass>, string>("PublicInstanceGenericMethodVoid");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            m(testClassInstance);
            Assert.AreEqual(null, testClassInstance.PublicParams);
            Assert.AreEqual(typeof(string), testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void Internal_GenericMethod_Void_NoParameter_OneTypeParameter_ByTypes()
        {
            var m = DelegateFactory.InstanceMethod<Action<TestClass>, string>("InternalInstanceGenericMethodVoid");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            m(testClassInstance);
            Assert.AreEqual(null, testClassInstance.PublicParams);
            Assert.AreEqual(typeof(string), testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void Protected_GenericMethod_Void_NoParameter_OneTypeParameter_ByTypes()
        {
            var m = DelegateFactory.InstanceMethod<Action<TestClass>, string>("ProtectedInstanceGenericMethodVoid");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            m(testClassInstance);
            Assert.AreEqual(null, testClassInstance.PublicParams);
            Assert.AreEqual(typeof(string), testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void Private_GenericMethod_Void_NoParameter_OneTypeParameter_ByTypes()
        {
            var m = DelegateFactory.InstanceMethod<Action<TestClass>, string>("PrivateInstanceGenericMethodVoid");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            m(testClassInstance);
            Assert.AreEqual(null, testClassInstance.PublicParams);
            Assert.AreEqual(typeof(string), testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void Public_GenericMethod_NoVoid_NoParameter_OneTypeParameter_ByTypes()
        {
            var m = DelegateFactory
                .InstanceMethod<Func<TestClass, TestClass>, TestClass>("PublicInstanceGenericMethod");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            var result = m(testClassInstance);
            Assert.AreEqual(null, testClassInstance.PublicParams);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), testClassInstance.PublicTypeParams[0]);
        }


        [TestMethod]
        public void Internal_GenericMethod_NoVoid_NoParameter_OneTypeParameter_ByTypes()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, TestClass>, TestClass>(
                "InternalInstanceGenericMethod");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            var result = m(testClassInstance);
            Assert.AreEqual(null, testClassInstance.PublicParams);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void Protected_GenericMethod_NoVoid_NoParameter_OneTypeParameter_ByTypes()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, TestClass>, TestClass>(
                "ProtectedInstanceGenericMethod");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            var result = m(testClassInstance);
            Assert.AreEqual(null, testClassInstance.PublicParams);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void Private_GenericMethod_NoVoid_NoParameter_OneTypeParameter_ByTypes()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, TestClass>, TestClass>(
                "PrivateInstanceGenericMethod");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            var result = m(testClassInstance);
            Assert.AreEqual(null, testClassInstance.PublicParams);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void GenericMethod_NoVoid_SingleParameter_OneTypeParameter_ByTypes()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, TestClass, TestClass>, TestClass>
                ("InstanceGenericMethod");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, testClassInstance);
            Assert.AreEqual(1, testClassInstance.PublicParams.Length);
            Assert.AreEqual(testClassInstance, testClassInstance.PublicParams[0]);
            Assert.AreEqual(testClassInstance, result);
            Assert.AreEqual(typeof(TestClass), testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void GenericMethod_NoVoid_2Parameter_OneTypeParameter_ByTypes()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, TestClass, int, TestClass>, TestClass>
                ("InstanceGenericMethod");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, testClassInstance, 0);
            Assert.AreEqual(2, testClassInstance.PublicParams.Length);
            Assert.AreEqual(testClassInstance, testClassInstance.PublicParams[0]);
            Assert.AreEqual(testClassInstance, result);
            Assert.AreEqual(typeof(TestClass), testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void GenericMethod_NoVoid_3Parameter_OneTypeParameter_ByTypes()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, TestStruct, int, bool, TestStruct>, TestStruct>
                ("InstanceGenericMethod");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            var testStructInstance = new TestStruct(0);
            var result = m(testClassInstance, testStructInstance, 0, true);
            Assert.AreEqual(3, testClassInstance.PublicParams.Length);
            Assert.AreEqual(testStructInstance, testClassInstance.PublicParams[0]);
            Assert.AreEqual(0, testClassInstance.PublicParams[1]);
            Assert.AreEqual(true, testClassInstance.PublicParams[2]);
            Assert.AreEqual(testStructInstance, result);
            Assert.AreEqual(typeof(TestStruct), testClassInstance.PublicTypeParams[0]);
        }


        [TestMethod]
        public void GenericMethod_NoVoid_NoParameter_2TypeParameters_ByTypes()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, TestClass>, TestClass, TestStruct>
                ("InstanceGenericMethod");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            var result = m(testClassInstance);
            Assert.AreEqual(null, testClassInstance.PublicParams);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), testClassInstance.PublicTypeParams[0]);
            Assert.AreEqual(typeof(TestStruct), testClassInstance.PublicTypeParams[1]);
        }

        [TestMethod]
        public void GenericMethod_NoVoid_SingleParameter_3TypeParameters_ByTypes()
        {
            var m = DelegateFactory
                .InstanceMethod<Func<TestClass, int, TestClass>, TestClass, TestStruct, TestClassNoDefaultCtor>
                    ("InstanceGenericMethod");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, 0);
            Assert.AreEqual(1, testClassInstance.PublicParams.Length);
            Assert.AreEqual(0, testClassInstance.PublicParams[0]);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), testClassInstance.PublicTypeParams[0]);
            Assert.AreEqual(typeof(TestStruct), testClassInstance.PublicTypeParams[1]);
            Assert.AreEqual(typeof(TestClassNoDefaultCtor), testClassInstance.PublicTypeParams[2]);
        }

        [TestMethod]
        public void GenericMethod_TypeParameter_NotAssignebleToNewConstraint()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, TestClassNoDefaultCtor>, TestClassNoDefaultCtor>
                ("PublicInstanceGenericMethod");
            Assert.IsNull(m);
        }

        [TestMethod]
        public void GenericMethod_TypeParameter_AssignebleToNewConstraint()
        {
            var m = DelegateFactory
                .InstanceMethod<Func<TestClass, TestClass>, TestClass>("PublicInstanceGenericMethod");
            Assert.IsNotNull(m);
        }

        [TestMethod]
        public void GenericMethod_TypeParameter_AssignebleToInterface()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, TestClass, int, TestClass>, TestClass>
                ("InstanceGenericMethod");
            Assert.IsNotNull(m);
        }

        [TestMethod]
        public void GenericMethod_TypeParameter_NotAssignebleToInterface()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, TestClassNoDefaultCtor, int, TestClassNoDefaultCtor>,
                TestClassNoDefaultCtor>("InstanceGenericMethod");
            Assert.IsNull(m);
        }

        [TestMethod]
        public void GenericMethod_TypeParameter_AssignebleToType()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, Derived, Derived>, Derived>(
                "InstanceGenericMethodWithType");
            Assert.IsNotNull(m);
        }

        [TestMethod]
        public void GenericMethod_TypeParameter_NotAssignebleToType()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, TestClass, TestClass>, TestClass>(
                "InstanceGenericMethodWithType");
            Assert.IsNull(m);
        }

        [TestMethod]
        public void GenericMethod_TypeParameter_AssignebleToClass()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, TestClass, TestClass>, TestClass>(
                "InstanceGenericMethodWithClass");
            Assert.IsNotNull(m);
        }

        [TestMethod]
        public void GenericMethod_TypeParameter_NotAssignebleToClass()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, int, int>, int>("InstanceGenericMethodWithClass");
            Assert.IsNull(m);
        }

        [TestMethod]
        public void GenericMethod_TypeParameter_AssignebleToStruct()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, int, int>, int>("InstanceGenericMethodWithStruc");
            Assert.IsNotNull(m);
        }

        [TestMethod]
        public void GenericMethod_TypeParameter_NotAssignebleToStruct()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, TestClass, TestClass>, TestClass>(
                "InstanceGenericMethodWithStruc");
            Assert.IsNull(m);
        }

        [TestMethod]
        public void GenericMethod_TypeParameter_WithTypeParamsInheritance()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, Base, Base>, Base, Derived>(
                "InstanceGenericMethodFromOtherParameter");
            Assert.IsNotNull(m);
        }

        [TestMethod]
        public void GenericMethod_TypeParameter_WithoutTypeParamsInheritance()
        {
            var m = DelegateFactory
                .InstanceMethod<Func<TestClass, TestClass, TestClass>, TestClass, TestClassNoDefaultCtor>
                    ("InstanceGenericMethodFromOtherParameter");
            Assert.IsNull(m);
        }

        [TestMethod]
        public void GenericMethod_SingleTypeParameter_FromStruct()
        {
            var m = DelegateFactory
                .InstanceMethod<Action<TestClass, TestClass>, TestClass>("InstanceGenericMethodVoid");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            m(testClassInstance, testClassInstance);
            Assert.AreEqual(testClassInstance, testClassInstance.InstanceGenericMethodVoidParameter);
        }

        [TestMethod]
        public void NonGenericMethod_FromGenericClass()
        {
            var m = DelegateFactory.InstanceMethod<Func<GenericClass<TestClass>, TestClass, TestClass>>(
                "InstanceGenericMethod");
            Assert.IsNotNull(m);
            var genericClassInstance = new GenericClass<TestClass>();
            var testClassInstance = new TestClass();
            var result = m(genericClassInstance, testClassInstance);
            Assert.AreEqual(testClassInstance, result);
        }

        [TestMethod]
        public void Public_GenericMethod_Void_NoParameter_OneTypeParameter_ByObjectAndTypes()
        {
            var m = typeof(TestClass).InstanceMethod<Action<TestClass>, string>("PublicInstanceGenericMethodVoid");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            m(testClassInstance);
            Assert.AreEqual(null, testClassInstance.PublicParams);
            Assert.AreEqual(typeof(string), testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void Public_GenericMethod_Void_NoParameter_OneTypeParameter_ByObjectAndTypes_WithCast()
        {
            var m = typeof(TestClass).InstanceMethod<Action<object>, string>("PublicInstanceGenericMethodVoid");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            m(testClassInstance);
            Assert.AreEqual(null, testClassInstance.PublicParams);
            Assert.AreEqual(typeof(string), testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void Internal_GenericMethod_Void_NoParameter_OneTypeParameter_ByObjectAndTypes()
        {
            var m = typeof(TestClass).InstanceMethod<Action<TestClass>, string>("InternalInstanceGenericMethodVoid");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            m(testClassInstance);
            Assert.AreEqual(null, testClassInstance.PublicParams);
            Assert.AreEqual(typeof(string), testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void Internal_GenericMethod_Void_NoParameter_OneTypeParameter_ByObjectAndTypes_WithCast()
        {
            var m = typeof(TestClass).InstanceMethod<Action<object>, string>("InternalInstanceGenericMethodVoid");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            m(testClassInstance);
            Assert.AreEqual(null, testClassInstance.PublicParams);
            Assert.AreEqual(typeof(string), testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void Protected_GenericMethod_Void_NoParameter_OneTypeParameter_ByObjectAndTypes()
        {
            var m = typeof(TestClass).InstanceMethod<Action<TestClass>, string>("ProtectedInstanceGenericMethodVoid");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            m(testClassInstance);
            Assert.AreEqual(null, testClassInstance.PublicParams);
            Assert.AreEqual(typeof(string), testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void Protected_GenericMethod_Void_NoParameter_OneTypeParameter_ByObjectAndTypes_WithCast()
        {
            var m = typeof(TestClass).InstanceMethod<Action<object>, string>("ProtectedInstanceGenericMethodVoid");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            m(testClassInstance);
            Assert.AreEqual(null, testClassInstance.PublicParams);
            Assert.AreEqual(typeof(string), testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void Private_GenericMethod_Void_NoParameter_OneTypeParameter_ByObjectAndTypes()
        {
            var m = typeof(TestClass).InstanceMethod<Action<TestClass>, string>("PrivateInstanceGenericMethodVoid");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            m(testClassInstance);
            Assert.AreEqual(null, testClassInstance.PublicParams);
            Assert.AreEqual(typeof(string), testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void Private_GenericMethod_Void_NoParameter_OneTypeParameter_ByObjectAndTypes_WithCast()
        {
            var m = typeof(TestClass).InstanceMethod<Action<object>, string>("PrivateInstanceGenericMethodVoid");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            m(testClassInstance);
            Assert.AreEqual(null, testClassInstance.PublicParams);
            Assert.AreEqual(typeof(string), testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void Public_GenericMethod_NoVoid_NoParameter_OneTypeParameter_ByObjectAndTypes()
        {
            var m = typeof(TestClass).InstanceMethod<Func<TestClass, TestClass>, TestClass>("PublicInstanceGenericMethod");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            var result = m(testClassInstance);
            Assert.AreEqual(null, testClassInstance.PublicParams);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void Public_GenericMethod_NoVoid_NoParameter_OneTypeParameter_ByObjectAndTypes_WithCast()
        {
            var m = typeof(TestClass).InstanceMethod<Func<object, TestClass>, TestClass>("PublicInstanceGenericMethod");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            var result = m(testClassInstance);
            Assert.AreEqual(null, testClassInstance.PublicParams);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void Internal_GenericMethod_NoVoid_NoParameter_OneTypeParameter_ByObjectAndTypes()
        {
            var m = typeof(TestClass).InstanceMethod<Func<TestClass, TestClass>, TestClass>(
                "InternalInstanceGenericMethod");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            var result = m(testClassInstance);
            Assert.AreEqual(null, testClassInstance.PublicParams);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void Internal_GenericMethod_NoVoid_NoParameter_OneTypeParameter_ByObjectAndTypes_WithCast()
        {
            var m = typeof(TestClass).InstanceMethod<Func<object, TestClass>, TestClass>("InternalInstanceGenericMethod");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            var result = m(testClassInstance);
            Assert.AreEqual(null, testClassInstance.PublicParams);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void Protected_GenericMethod_NoVoid_NoParameter_OneTypeParameter_ByObjectAndTypes()
        {
            var m = typeof(TestClass).InstanceMethod<Func<TestClass, TestClass>, TestClass>(
                "ProtectedInstanceGenericMethod");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            var result = m(testClassInstance);
            Assert.AreEqual(null, testClassInstance.PublicParams);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void Protected_GenericMethod_NoVoid_NoParameter_OneTypeParameter_ByObjectAndTypes_WithCast()
        {
            var m = typeof(TestClass).InstanceMethod<Func<object, TestClass>, TestClass>("ProtectedInstanceGenericMethod");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            var result = m(testClassInstance);
            Assert.AreEqual(null, testClassInstance.PublicParams);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void Private_GenericMethod_NoVoid_NoParameter_OneTypeParameter_ByObjectAndTypes()
        {
            var m = typeof(TestClass)
                .InstanceMethod<Func<TestClass, TestClass>, TestClass>("PrivateInstanceGenericMethod");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            var result = m(testClassInstance);
            Assert.AreEqual(null, testClassInstance.PublicParams);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void Private_GenericMethod_NoVoid_NoParameter_OneTypeParameter_ByObjectAndTypes_WithCast()
        {
            var m = typeof(TestClass).InstanceMethod<Func<object, TestClass>, TestClass>("PrivateInstanceGenericMethod");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            var result = m(testClassInstance);
            Assert.AreEqual(null, testClassInstance.PublicParams);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void GenericMethod_NoVoid_SingleParameter_OneTypeParameter_ByObjectAndTypes()
        {
            var m = typeof(TestClass).InstanceMethod<Func<TestClass, TestClass, TestClass>, TestClass>(
                "InstanceGenericMethod");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, testClassInstance);
            Assert.AreEqual(1, testClassInstance.PublicParams.Length);
            Assert.AreEqual(testClassInstance, testClassInstance.PublicParams[0]);
            Assert.AreEqual(testClassInstance, result);
            Assert.AreEqual(typeof(TestClass), testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void GenericMethod_NoVoid_SingleParameter_OneTypeParameter_ByObjectAndTypes_WithCast()
        {
            var m = typeof(TestClass).InstanceMethod<Func<object, TestClass, TestClass>, TestClass>(
                "InstanceGenericMethod");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, testClassInstance);
            Assert.AreEqual(1, testClassInstance.PublicParams.Length);
            Assert.AreEqual(testClassInstance, testClassInstance.PublicParams[0]);
            Assert.AreEqual(testClassInstance, result);
            Assert.AreEqual(typeof(TestClass), testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void GenericMethod_NoVoid_2Parameter_OneTypeParameter_ByObjectAndTypes()
        {
            var m = typeof(TestClass).InstanceMethod<Func<TestClass, TestClass, int, TestClass>, TestClass>
                ("InstanceGenericMethod");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, testClassInstance, 0);
            Assert.AreEqual(2, testClassInstance.PublicParams.Length);
            Assert.AreEqual(testClassInstance, testClassInstance.PublicParams[0]);
            Assert.AreEqual(0, testClassInstance.PublicParams[1]);
            Assert.AreEqual(testClassInstance, result);
            Assert.AreEqual(typeof(TestClass), testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void GenericMethod_NoVoid_2Parameter_OneTypeParameter_ByObjectAndTypes_WithCast()
        {
            var m = typeof(TestClass).InstanceMethod<Func<object, TestClass, int, TestClass>, TestClass>
                ("InstanceGenericMethod");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, testClassInstance, 0);
            Assert.AreEqual(2, testClassInstance.PublicParams.Length);
            Assert.AreEqual(testClassInstance, testClassInstance.PublicParams[0]);
            Assert.AreEqual(0, testClassInstance.PublicParams[1]);
            Assert.AreEqual(testClassInstance, result);
            Assert.AreEqual(typeof(TestClass), testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void GenericMethod_NoVoid_3Parameter_OneTypeParameter_ByObjectAndTypes()
        {
            var m = typeof(TestClass).InstanceMethod<Func<TestClass, TestStruct, int, bool, TestStruct>, TestStruct>
                ("InstanceGenericMethod");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            var testStructInstance = new TestStruct(0);
            var result = m(testClassInstance, testStructInstance, 0, true);
            Assert.AreEqual(3, testClassInstance.PublicParams.Length);
            Assert.AreEqual(testStructInstance, testClassInstance.PublicParams[0]);
            Assert.AreEqual(0, testClassInstance.PublicParams[1]);
            Assert.AreEqual(true, testClassInstance.PublicParams[2]);
            Assert.AreEqual(testStructInstance, result);
            Assert.AreEqual(typeof(TestStruct), testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void GenericMethod_NoVoid_3Parameter_OneTypeParameter_ByObjectAndTypes_WithCast()
        {
            var m = typeof(TestClass).InstanceMethod<Func<object, TestStruct, int, bool, TestStruct>, TestStruct>
                ("InstanceGenericMethod");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            var testStructInstance = new TestStruct(0);
            var result = m(testClassInstance, testStructInstance, 0, true);
            Assert.AreEqual(3, testClassInstance.PublicParams.Length);
            Assert.AreEqual(testStructInstance, testClassInstance.PublicParams[0]);
            Assert.AreEqual(0, testClassInstance.PublicParams[1]);
            Assert.AreEqual(true, testClassInstance.PublicParams[2]);
            Assert.AreEqual(testStructInstance, result);
            Assert.AreEqual(typeof(TestStruct), testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void GenericMethod_NoVoid_NoParameter_2TypeParameters_ByObjectAndTypes()
        {
            var m = typeof(TestClass).InstanceMethod<Func<TestClass, TestClass>, TestClass, TestStruct>
                ("InstanceGenericMethod");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            var result = m(testClassInstance);
            Assert.AreEqual(null, testClassInstance.PublicParams);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), testClassInstance.PublicTypeParams[0]);
            Assert.AreEqual(typeof(TestStruct), testClassInstance.PublicTypeParams[1]);
        }

        [TestMethod]
        public void GenericMethod_NoVoid_NoParameter_2TypeParameters_ByObjectAndTypes_WithCast()
        {
            var m = typeof(TestClass).InstanceMethod<Func<object, TestClass>, TestClass, TestStruct>
                ("InstanceGenericMethod");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            var result = m(testClassInstance);
            Assert.AreEqual(null, testClassInstance.PublicParams);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), testClassInstance.PublicTypeParams[0]);
            Assert.AreEqual(typeof(TestStruct), testClassInstance.PublicTypeParams[1]);
        }

        [TestMethod]
        public void GenericMethod_NoVoid_SingleParameter_3TypeParameters_ByObjectAndTypes()
        {
            var m = typeof(TestClass)
                .InstanceMethod<Func<TestClass, int, TestClass>, TestClass, TestStruct, TestClassNoDefaultCtor>
                    ("InstanceGenericMethod");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, 0);
            Assert.AreEqual(1, testClassInstance.PublicParams.Length);
            Assert.AreEqual(0, testClassInstance.PublicParams[0]);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), testClassInstance.PublicTypeParams[0]);
            Assert.AreEqual(typeof(TestStruct), testClassInstance.PublicTypeParams[1]);
            Assert.AreEqual(typeof(TestClassNoDefaultCtor), testClassInstance.PublicTypeParams[2]);
        }

        [TestMethod]
        public void GenericMethod_NoVoid_SingleParameter_3TypeParameters_ByObjectAndTypes_WithCast()
        {
            var m = typeof(TestClass)
                .InstanceMethod<Func<object, int, TestClass>, TestClass, TestStruct, TestClassNoDefaultCtor>
                    ("InstanceGenericMethod");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, 0);
            Assert.AreEqual(1, testClassInstance.PublicParams.Length);
            Assert.AreEqual(0, testClassInstance.PublicParams[0]);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), testClassInstance.PublicTypeParams[0]);
            Assert.AreEqual(typeof(TestStruct), testClassInstance.PublicTypeParams[1]);
            Assert.AreEqual(typeof(TestClassNoDefaultCtor), testClassInstance.PublicTypeParams[2]);
        }

        [TestMethod]
        public void Public_GenericMethod_Void_NoParameter_OneTypeParameter_ByObjects()
        {
            var m = typeof(TestClass).InstanceGenericMethodVoid("PublicInstanceGenericMethodVoid", null,
                new[] {typeof(string)});
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            m(testClassInstance, new object[0]);
            Assert.AreEqual(null, testClassInstance.PublicParams);
            Assert.AreEqual(typeof(string), testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void Internal_GenericMethod_Void_NoParameter_OneTypeParameter_ByObjects()
        {
            var m = typeof(TestClass).InstanceGenericMethodVoid("InternalInstanceGenericMethodVoid", null,
                new[] {typeof(string)});
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            m(testClassInstance, new object[0]);
            Assert.AreEqual(null, testClassInstance.PublicParams);
            Assert.AreEqual(typeof(string), testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void Protected_GenericMethod_Void_NoParameter_OneTypeParameter_ByObjects()
        {
            var m = typeof(TestClass).InstanceGenericMethodVoid("ProtectedInstanceGenericMethodVoid", null,
                new[] {typeof(string)});
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            m(testClassInstance, new object[0]);
            Assert.AreEqual(null, testClassInstance.PublicParams);
            Assert.AreEqual(typeof(string), testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void Private_GenericMethod_Void_NoParameter_OneTypeParameter_ByObjects()
        {
            var m = typeof(TestClass).InstanceGenericMethodVoid("PrivateInstanceGenericMethodVoid", null,
                new[] {typeof(string)});
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            m(testClassInstance, new object[0]);
            Assert.AreEqual(null, testClassInstance.PublicParams);
            Assert.AreEqual(typeof(string), testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void Public_GenericMethod_NoVoid_NoParameter_OneTypeParameter_ByObjects()
        {
            var testClassType = typeof(TestClass);
            var m = testClassType.InstanceGenericMethod("PublicInstanceGenericMethod", null,
                new[] {testClassType});
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, new object[] {testClassInstance});
            Assert.AreEqual(null, testClassInstance.PublicParams);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void Internal_GenericMethod_NoVoid_NoParameter_OneTypeParameter_ByObjects()
        {
            var testClassType = typeof(TestClass);
            var m = testClassType.InstanceGenericMethod("InternalInstanceGenericMethod", null,
                new[] {testClassType});
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, new object[] {testClassInstance});
            Assert.AreEqual(null, testClassInstance.PublicParams);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void Protected_GenericMethod_NoVoid_NoParameter_OneTypeParameter_ByObjects()
        {
            var testClassType = typeof(TestClass);
            var m = testClassType.InstanceGenericMethod("ProtectedInstanceGenericMethod", null,
                new[] {testClassType});
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, new object[] {testClassInstance});
            Assert.AreEqual(null, testClassInstance.PublicParams);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void Private_GenericMethod_NoVoid_NoParameter_OneTypeParameter_ByObjects()
        {
            var testClassType = typeof(TestClass);
            var m = testClassType.InstanceGenericMethod("PrivateInstanceGenericMethod", null,
                new[] {testClassType});
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, new object[] {testClassInstance});
            Assert.AreEqual(null, testClassInstance.PublicParams);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void GenericMethod_NoVoid_SingleParameter_OneTypeParameter_ByObjects()
        {
            var testClassType = typeof(TestClass);
            var m = testClassType.InstanceGenericMethod("InstanceGenericMethod", new[] {testClassType},
                new[] {testClassType});
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, new object[] {testClassInstance});
            Assert.AreEqual(1, testClassInstance.PublicParams.Length);
            Assert.AreEqual(testClassInstance, testClassInstance.PublicParams[0]);
            Assert.AreEqual(testClassInstance, result);
            Assert.AreEqual(typeof(TestClass), testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void GenericMethod_NoVoid_2Parameter_OneTypeParameter_ByObjects()
        {
            var testClassType = typeof(TestClass);
            var m = testClassType.InstanceGenericMethod("InstanceGenericMethod", new[] {testClassType, typeof(int)},
                new[] {testClassType});
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, new object[] {testClassInstance, 0});
            Assert.AreEqual(2, testClassInstance.PublicParams.Length);
            Assert.AreEqual(testClassInstance, testClassInstance.PublicParams[0]);
            Assert.AreEqual(0, testClassInstance.PublicParams[1]);
            Assert.AreEqual(testClassInstance, result);
            Assert.AreEqual(typeof(TestClass), testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void GenericMethod_NoVoid_3Parameter_OneTypeParameter_ByObjects()
        {
            var testStructType = typeof(TestStruct);
            var m = typeof(TestClass).InstanceGenericMethod("InstanceGenericMethod",
                new[] {testStructType, typeof(int), typeof(bool)}, new[] {testStructType});
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            var testStructInstance = new TestStruct(0);
            var result = m(testClassInstance, new object[] {testStructInstance, 0, true});
            Assert.AreEqual(3, testClassInstance.PublicParams.Length);
            Assert.AreEqual(testStructInstance, testClassInstance.PublicParams[0]);
            Assert.AreEqual(0, testClassInstance.PublicParams[1]);
            Assert.AreEqual(true, testClassInstance.PublicParams[2]);
            Assert.AreEqual(testStructInstance, result);
            Assert.AreEqual(typeof(TestStruct), testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void GenericMethod_NoVoid_NoParameter_2TypeParameters_ByObjects()
        {
            var testClassType = typeof(TestClass);
            var m = testClassType.InstanceGenericMethod("InstanceGenericMethod", null,
                new[] {testClassType, typeof(TestStruct)});
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, new object[0]);
            Assert.AreEqual(null, testClassInstance.PublicParams);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), testClassInstance.PublicTypeParams[0]);
            Assert.AreEqual(typeof(TestStruct), testClassInstance.PublicTypeParams[1]);
        }

        [TestMethod]
        public void GenericMethod_NoVoid_SingleParameter_3TypeParameters_ByObjects()
        {
            var testClassType = typeof(TestClass);
            var m = testClassType.InstanceGenericMethod("InstanceGenericMethod", new[] {typeof(int)},
                new[] {testClassType, typeof(TestStruct), typeof(TestClassNoDefaultCtor)});
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, new object[] {0});
            Assert.AreEqual(1, testClassInstance.PublicParams.Length);
            Assert.AreEqual(0, testClassInstance.PublicParams[0]);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), testClassInstance.PublicTypeParams[0]);
            Assert.AreEqual(typeof(TestStruct), testClassInstance.PublicTypeParams[1]);
            Assert.AreEqual(typeof(TestClassNoDefaultCtor), testClassInstance.PublicTypeParams[2]);
        }

        [TestMethod]
        public void InstanceGenericMethod_DoNotThrowExc_When_ParametersArray_IsNull()
        {
            var testClassType = typeof(TestClass);
            var m = testClassType.InstanceGenericMethod("InternalInstanceGenericMethod", null,
                new[] {testClassType});
            Assert.IsNotNull(m);
        }

        [TestMethod]
        public void InstanceGenericMethod_DoNotThrowExc_When_TypeParametersArray_IsNull()
        {
            var m = typeof(TestClass).InstanceGenericMethod("PublicMethod", new Type[0], null);
            Assert.IsNotNull(m);
        }

        [TestMethod]
        public void InterfaceMethod_Generic_ByTypes()
        {
            var m = DelegateFactory.InstanceMethod<Func<IService, string, string>, string>("Echo");
            Assert.IsNotNull(m);
            var result = m(new Service(), TestValue);
            Assert.AreEqual(TestValue, result);
        }

        [TestMethod]
        public void InterfaceMethod_Generic_ByObjectAndTypes()
        {
            var m = typeof(IService).InstanceMethod<Func<IService, string, string>, string>("Echo");
            Assert.IsNotNull(m);
            var result = m(new Service(), TestValue);
            Assert.AreEqual(TestValue, result);
        }

        [TestMethod]
        public void InterfaceMethod_Generic_ByObjectAndTypes_WithCast()
        {
            var m = typeof(IService).InstanceMethod<Func<object, string, string>, string>("Echo");
            Assert.IsNotNull(m);
            var result = m(new Service(), TestValue);
            Assert.AreEqual(TestValue, result);
        }

        [TestMethod]
        public void InterfaceMethod_Generic_ByObjects()
        {
            var m = typeof(IService).InstanceGenericMethod("Echo", new[] {typeof(string)}, new[] {typeof(string)});
            Assert.IsNotNull(m);
            var result = m(new Service(), new object[] {TestValue});
            Assert.AreEqual(TestValue, result);
        }

        [TestMethod]
        public void Incorrect_TDelegate_For_InstanceGenericMethod()
        {
            AssertHelper.ThrowsException<ArgumentException>(() =>
                typeof(TestClass).InstanceGenericMethod<Action>("PublicMethodVoid", null, new[] {typeof(string)}));
        }
    }
}