// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DelegateFactoryTests_StaticMethods_Generic.cs" company="Natan Podbielski">
//   Copyright (c) 2016 - 2018 Natan Podbielski. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
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
    public class DelegateFactoryTests_StaticMethods_Generic
    {
        [TestMethod]
        public void NoGenericMethod_ShouldNot_Return_NonGeneric_With_TheSame_Name()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Action, string>("StaticVoidPublic");
            Assert.IsNull(m);
        }

        [TestMethod]
        public void Public_GenericMethod_Void_NoParameter_OneTypeParameter_ByTypes()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Action, string>("PublicStaticGenericMethodVoid");
            Assert.IsNotNull(m);
            m();
            Assert.AreEqual(null, TestClass.StaticPublicParams);
            Assert.AreEqual(typeof(string), TestClass.StaticPublicTypeParams[0]);
        }

        [TestMethod]
        public void Internal_GenericMethod_Void_NoParameter_OneTypeParameter_ByTypes()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Action, string>("InternalStaticGenericMethodVoid");
            Assert.IsNotNull(m);
            m();
            Assert.AreEqual(null, TestClass.StaticPublicParams);
            Assert.AreEqual(typeof(string), TestClass.StaticPublicTypeParams[0]);
        }

        [TestMethod]
        public void Protected_GenericMethod_Void_NoParameter_OneTypeParameter_ByTypes()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Action, string>("ProtectedStaticGenericMethodVoid");
            Assert.IsNotNull(m);
            m();
            Assert.AreEqual(null, TestClass.StaticPublicParams);
            Assert.AreEqual(typeof(string), TestClass.StaticPublicTypeParams[0]);
        }

        [TestMethod]
        public void Private_GenericMethod_Void_NoParameter_OneTypeParameter_ByTypes()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Action, string>("PrivateStaticGenericMethodVoid");
            Assert.IsNotNull(m);
            m();
            Assert.AreEqual(null, TestClass.StaticPublicParams);
            Assert.AreEqual(typeof(string), TestClass.StaticPublicTypeParams[0]);
        }

        [TestMethod]
        public void Public_GenericMethod_NoVoid_NoParameter_OneTypeParameter_ByTypes()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<TestClass>, TestClass>("PublicStaticGenericMethod");
            Assert.IsNotNull(m);
            var result = m();
            Assert.AreEqual(null, TestClass.StaticPublicParams);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), TestClass.StaticPublicTypeParams[0]);
        }


        [TestMethod]
        public void Internal_GenericMethod_NoVoid_NoParameter_OneTypeParameter_ByTypes()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<TestClass>, TestClass>("InternalStaticGenericMethod");
            Assert.IsNotNull(m);
            var result = m();
            Assert.AreEqual(null, TestClass.StaticPublicParams);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), TestClass.StaticPublicTypeParams[0]);
        }

        [TestMethod]
        public void Protected_GenericMethod_NoVoid_NoParameter_OneTypeParameter_ByTypes()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<TestClass>, TestClass>("ProtectedStaticGenericMethod");
            Assert.IsNotNull(m);
            var result = m();
            Assert.AreEqual(null, TestClass.StaticPublicParams);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), TestClass.StaticPublicTypeParams[0]);
        }

        [TestMethod]
        public void Private_GenericMethod_NoVoid_NoParameter_OneTypeParameter_ByTypes()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<TestClass>, TestClass>("PrivateStaticGenericMethod");
            Assert.IsNotNull(m);
            var result = m();
            Assert.AreEqual(null, TestClass.StaticPublicParams);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), TestClass.StaticPublicTypeParams[0]);
        }

        [TestMethod]
        public void GenericMethod_NoVoid_SingleParameter_OneTypeParameter_ByTypes()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<TestClass, TestClass>, TestClass>
                ("StaticGenericMethod");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            var result = m(testClassInstance);
            Assert.AreEqual(1, TestClass.StaticPublicParams.Length);
            Assert.AreEqual(testClassInstance, TestClass.StaticPublicParams[0]);
            Assert.AreEqual(testClassInstance, result);
            Assert.AreEqual(typeof(TestClass), TestClass.StaticPublicTypeParams[0]);
        }

        [TestMethod]
        public void GenericMethod_NoVoid_2Parameter_OneTypeParameter_ByTypes()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<TestClass, int, TestClass>, TestClass>
                ("StaticGenericMethod");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, 0);
            Assert.AreEqual(2, TestClass.StaticPublicParams.Length);
            Assert.AreEqual(testClassInstance, TestClass.StaticPublicParams[0]);
            Assert.AreEqual(testClassInstance, result);
            Assert.AreEqual(typeof(TestClass), TestClass.StaticPublicTypeParams[0]);
        }

        [TestMethod]
        public void GenericMethod_NoVoid_3Parameter_OneTypeParameter_ByTypes()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<TestStruct, int, bool, TestStruct>, TestStruct>
                ("StaticGenericMethod");
            Assert.IsNotNull(m);
            var testStructInstance = new TestStruct();
            var result = m(testStructInstance, 0, true);
            Assert.AreEqual(3, TestClass.StaticPublicParams.Length);
            Assert.AreEqual(testStructInstance, TestClass.StaticPublicParams[0]);
            Assert.AreEqual(0, TestClass.StaticPublicParams[1]);
            Assert.AreEqual(true, TestClass.StaticPublicParams[2]);
            Assert.AreEqual(testStructInstance, result);
            Assert.AreEqual(typeof(TestStruct), TestClass.StaticPublicTypeParams[0]);
        }


        [TestMethod]
        public void GenericMethod_NoVoid_NoParameter_2TypeParameters_ByTypes()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<TestClass>, TestClass, TestStruct>
                ("StaticGenericMethod");
            Assert.IsNotNull(m);
            var result = m();
            Assert.AreEqual(null, TestClass.StaticPublicParams);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), TestClass.StaticPublicTypeParams[0]);
            Assert.AreEqual(typeof(TestStruct), TestClass.StaticPublicTypeParams[1]);
        }

        [TestMethod]
        public void GenericMethod_NoVoid_SingleParameter_3TypeParameters_ByTypes()
        {
            var m = DelegateFactory
                .StaticMethod<TestClass, Func<int, TestClass>, TestClass, TestStruct, TestClassNoDefaultCtor>
                    ("StaticGenericMethod");
            Assert.IsNotNull(m);
            var result = m(0);
            Assert.AreEqual(1, TestClass.StaticPublicParams.Length);
            Assert.AreEqual(0, TestClass.StaticPublicParams[0]);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), TestClass.StaticPublicTypeParams[0]);
            Assert.AreEqual(typeof(TestStruct), TestClass.StaticPublicTypeParams[1]);
            Assert.AreEqual(typeof(TestClassNoDefaultCtor), TestClass.StaticPublicTypeParams[2]);
        }

        [TestMethod]
        public void GenericMethod_TypeParameter_NotAssignebleToNewConstraint()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<TestClassNoDefaultCtor>, TestClassNoDefaultCtor>
                ("PublicStaticGenericMethod");
            Assert.IsNull(m);
        }

        [TestMethod]
        public void GenericMethod_TypeParameter_AssignebleToNewConstraint()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<TestClass>, TestClass>("PublicStaticGenericMethod");
            Assert.IsNotNull(m);
        }

        [TestMethod]
        public void GenericMethod_TypeParameter_AssignebleToInterface()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<TestClass, int, TestClass>, TestClass>
                ("StaticGenericMethod");
            Assert.IsNotNull(m);
        }

        [TestMethod]
        public void GenericMethod_TypeParameter_NotAssignebleToInterface()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<TestClassNoDefaultCtor, int, TestClassNoDefaultCtor>,
                TestClassNoDefaultCtor>("StaticGenericMethod");
            Assert.IsNull(m);
        }

        [TestMethod]
        public void GenericMethod_TypeParameter_AssignebleToType()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<Derived, Derived>, Derived>(
                "StaticGenericMethodWithType");
            Assert.IsNotNull(m);
        }

        [TestMethod]
        public void GenericMethod_TypeParameter_NotAssignebleToType()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<TestClass, TestClass>, TestClass>(
                "StaticGenericMethodWithType");
            Assert.IsNull(m);
        }

        [TestMethod]
        public void GenericMethod_TypeParameter_AssignebleToClass()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<TestClass, TestClass>, TestClass>(
                "StaticGenericMethodWithClass");
            Assert.IsNotNull(m);
        }

        [TestMethod]
        public void GenericMethod_TypeParameter_NotAssignebleToClass()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<int, int>, int>("StaticGenericMethodWithClass");
            Assert.IsNull(m);
        }

        [TestMethod]
        public void GenericMethod_TypeParameter_AssignebleToStruct()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<int, int>, int>("StaticGenericMethodWithStruc");
            Assert.IsNotNull(m);
        }

        [TestMethod]
        public void GenericMethod_TypeParameter_NotAssignebleToStruct()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<TestClass, TestClass>, TestClass>(
                "StaticGenericMethodWithStruc");
            Assert.IsNull(m);
        }

        [TestMethod]
        public void GenericMethod_TypeParameter_WithTypeParamsInheritance()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<Base, Base>, Base, Derived>(
                "StaticGenericMethodFromOtherParameter");
            Assert.IsNotNull(m);
        }

        [TestMethod]
        public void GenericMethod_TypeParameter_WithoutTypeParamsInheritance()
        {
            var m = DelegateFactory
                .StaticMethod<TestClass, Func<TestClass, TestClass>, TestClass, TestClassNoDefaultCtor>
                    ("StaticGenericMethodFromOtherParameter");
            Assert.IsNull(m);
        }

        [TestMethod]
        public void GenericMethod_SingleTypeParameter_FromStruct()
        {
            var m = DelegateFactory.StaticMethod<TestStruct, Action, TestClass>("StaticGenericMethodVoid");
            Assert.IsNotNull(m);
            m();
            //Assert.IsTrue(TestStruct.StaticGenericMethodVoidExecuted);
        }

        [TestMethod]
        public void NonGenericMethod_FromGenericClass()
        {
            var m = DelegateFactory.StaticMethod<GenericClass<TestClass>, Func<TestClass, TestClass>>(
                "StaticGenericMethod");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            var result = m(testClassInstance);
            Assert.AreEqual(testClassInstance, result);
        }

        [TestMethod]
        public void Public_GenericMethod_Void_NoParameter_OneTypeParameter_ByObjectAndTypes()
        {
            var m = typeof(TestClass).StaticMethod<Action, string>("PublicStaticGenericMethodVoid");
            Assert.IsNotNull(m);
            m();
            Assert.AreEqual(null, TestClass.StaticPublicParams);
            Assert.AreEqual(typeof(string), TestClass.StaticPublicTypeParams[0]);
        }

        [TestMethod]
        public void Internal_GenericMethod_Void_NoParameter_OneTypeParameter_ByObjectAndTypes()
        {
            var m = typeof(TestClass).StaticMethod<Action, string>("InternalStaticGenericMethodVoid");
            Assert.IsNotNull(m);
            m();
            Assert.AreEqual(null, TestClass.StaticPublicParams);
            Assert.AreEqual(typeof(string), TestClass.StaticPublicTypeParams[0]);
        }

        [TestMethod]
        public void Protected_GenericMethod_Void_NoParameter_OneTypeParameter_ByObjectAndTypes()
        {
            var m = typeof(TestClass).StaticMethod<Action, string>("ProtectedStaticGenericMethodVoid");
            Assert.IsNotNull(m);
            m();
            Assert.AreEqual(null, TestClass.StaticPublicParams);
            Assert.AreEqual(typeof(string), TestClass.StaticPublicTypeParams[0]);
        }

        [TestMethod]
        public void Private_GenericMethod_Void_NoParameter_OneTypeParameter_ByObjectAndTypes()
        {
            var m = typeof(TestClass).StaticMethod<Action, string>("PrivateStaticGenericMethodVoid");
            Assert.IsNotNull(m);
            m();
            Assert.AreEqual(null, TestClass.StaticPublicParams);
            Assert.AreEqual(typeof(string), TestClass.StaticPublicTypeParams[0]);
        }

        [TestMethod]
        public void Public_GenericMethod_NoVoid_NoParameter_OneTypeParameter_ByObjectAndTypes()
        {
            var m = typeof(TestClass).StaticMethod<Func<TestClass>, TestClass>("PublicStaticGenericMethod");
            Assert.IsNotNull(m);
            var result = m();
            Assert.AreEqual(null, TestClass.StaticPublicParams);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), TestClass.StaticPublicTypeParams[0]);
        }

        [TestMethod]
        public void Internal_GenericMethod_NoVoid_NoParameter_OneTypeParameter_ByObjectAndTypes()
        {
            var m = typeof(TestClass).StaticMethod<Func<TestClass>, TestClass>("InternalStaticGenericMethod");
            Assert.IsNotNull(m);
            var result = m();
            Assert.AreEqual(null, TestClass.StaticPublicParams);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), TestClass.StaticPublicTypeParams[0]);
        }

        [TestMethod]
        public void Protected_GenericMethod_NoVoid_NoParameter_OneTypeParameter_ByObjectAndTypes()
        {
            var m = typeof(TestClass).StaticMethod<Func<TestClass>, TestClass>("ProtectedStaticGenericMethod");
            Assert.IsNotNull(m);
            var result = m();
            Assert.AreEqual(null, TestClass.StaticPublicParams);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), TestClass.StaticPublicTypeParams[0]);
        }

        [TestMethod]
        public void Private_GenericMethod_NoVoid_NoParameter_OneTypeParameter_ByObjectAndTypes()
        {
            var m = typeof(TestClass).StaticMethod<Func<TestClass>, TestClass>("PrivateStaticGenericMethod");
            Assert.IsNotNull(m);
            var result = m();
            Assert.AreEqual(null, TestClass.StaticPublicParams);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), TestClass.StaticPublicTypeParams[0]);
        }

        [TestMethod]
        public void GenericMethod_NoVoid_SingleParameter_OneTypeParameter_ByObjectAndTypes()
        {
            var m = typeof(TestClass).StaticMethod<Func<TestClass, TestClass>, TestClass>("StaticGenericMethod");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            var result = m(testClassInstance);
            Assert.AreEqual(1, TestClass.StaticPublicParams.Length);
            Assert.AreEqual(testClassInstance, TestClass.StaticPublicParams[0]);
            Assert.AreEqual(testClassInstance, result);
            Assert.AreEqual(typeof(TestClass), TestClass.StaticPublicTypeParams[0]);
        }

        [TestMethod]
        public void GenericMethod_NoVoid_2Parameter_OneTypeParameter_ByObjectAndTypes()
        {
            var m = typeof(TestClass).StaticMethod<Func<TestClass, int, TestClass>, TestClass>
                ("StaticGenericMethod");
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            var result = m(testClassInstance, 0);
            Assert.AreEqual(2, TestClass.StaticPublicParams.Length);
            Assert.AreEqual(testClassInstance, TestClass.StaticPublicParams[0]);
            Assert.AreEqual(0, TestClass.StaticPublicParams[1]);
            Assert.AreEqual(testClassInstance, result);
            Assert.AreEqual(typeof(TestClass), TestClass.StaticPublicTypeParams[0]);
        }

        [TestMethod]
        public void GenericMethod_NoVoid_3Parameter_OneTypeParameter_ByObjectAndTypes()
        {
            var m = typeof(TestClass).StaticMethod<Func<TestStruct, int, bool, TestStruct>, TestStruct>
                ("StaticGenericMethod");
            Assert.IsNotNull(m);
            var testStructInstance = new TestStruct();
            var result = m(testStructInstance, 0, true);
            Assert.AreEqual(3, TestClass.StaticPublicParams.Length);
            Assert.AreEqual(testStructInstance, TestClass.StaticPublicParams[0]);
            Assert.AreEqual(0, TestClass.StaticPublicParams[1]);
            Assert.AreEqual(true, TestClass.StaticPublicParams[2]);
            Assert.AreEqual(testStructInstance, result);
            Assert.AreEqual(typeof(TestStruct), TestClass.StaticPublicTypeParams[0]);
        }


        [TestMethod]
        public void GenericMethod_NoVoid_NoParameter_2TypeParameters_ByObjectAndTypes()
        {
            var m = typeof(TestClass).StaticMethod<Func<TestClass>, TestClass, TestStruct>
                ("StaticGenericMethod");
            Assert.IsNotNull(m);
            var result = m();
            Assert.AreEqual(null, TestClass.StaticPublicParams);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), TestClass.StaticPublicTypeParams[0]);
            Assert.AreEqual(typeof(TestStruct), TestClass.StaticPublicTypeParams[1]);
        }

        [TestMethod]
        public void GenericMethod_NoVoid_SingleParameter_3TypeParameters_ByObjectAndTypes()
        {
            var m = typeof(TestClass).StaticMethod<Func<int, TestClass>, TestClass, TestStruct, TestClassNoDefaultCtor>
                ("StaticGenericMethod");
            Assert.IsNotNull(m);
            var result = m(0);
            Assert.AreEqual(1, TestClass.StaticPublicParams.Length);
            Assert.AreEqual(0, TestClass.StaticPublicParams[0]);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), TestClass.StaticPublicTypeParams[0]);
            Assert.AreEqual(typeof(TestStruct), TestClass.StaticPublicTypeParams[1]);
            Assert.AreEqual(typeof(TestClassNoDefaultCtor), TestClass.StaticPublicTypeParams[2]);
        }

        [TestMethod]
        public void Public_GenericMethod_Void_NoParameter_OneTypeParameter_ByObjects()
        {
            var m = typeof(TestClass).StaticGenericMethodVoid("PublicStaticGenericMethodVoid", null,
                new[] {typeof(string)});
            Assert.IsNotNull(m);
            m(new object[0]);
            Assert.AreEqual(null, TestClass.StaticPublicParams);
            Assert.AreEqual(typeof(string), TestClass.StaticPublicTypeParams[0]);
        }

        [TestMethod]
        public void Internal_GenericMethod_Void_NoParameter_OneTypeParameter_ByObjects()
        {
            var m = typeof(TestClass).StaticGenericMethodVoid("InternalStaticGenericMethodVoid", null,
                new[] {typeof(string)});
            Assert.IsNotNull(m);
            m(new object[0]);
            Assert.AreEqual(null, TestClass.StaticPublicParams);
            Assert.AreEqual(typeof(string), TestClass.StaticPublicTypeParams[0]);
        }

        [TestMethod]
        public void Protected_GenericMethod_Void_NoParameter_OneTypeParameter_ByObjects()
        {
            var m = typeof(TestClass).StaticGenericMethodVoid("ProtectedStaticGenericMethodVoid", null,
                new[] {typeof(string)});
            Assert.IsNotNull(m);
            m(new object[0]);
            Assert.AreEqual(null, TestClass.StaticPublicParams);
            Assert.AreEqual(typeof(string), TestClass.StaticPublicTypeParams[0]);
        }

        [TestMethod]
        public void Private_GenericMethod_Void_NoParameter_OneTypeParameter_ByObjects()
        {
            var m = typeof(TestClass).StaticGenericMethodVoid("PrivateStaticGenericMethodVoid", null,
                new[] {typeof(string)});
            Assert.IsNotNull(m);
            m(new object[0]);
            Assert.AreEqual(null, TestClass.StaticPublicParams);
            Assert.AreEqual(typeof(string), TestClass.StaticPublicTypeParams[0]);
        }

        [TestMethod]
        public void Public_GenericMethod_NoVoid_NoParameter_OneTypeParameter_ByObjects()
        {
            var testClassType = typeof(TestClass);
            var m = testClassType.StaticGenericMethod("PublicStaticGenericMethod", null,
                new[] {testClassType});
            Assert.IsNotNull(m);
            var result = m(new object[] { });
            Assert.AreEqual(null, TestClass.StaticPublicParams);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), TestClass.StaticPublicTypeParams[0]);
        }

        [TestMethod]
        public void Internal_GenericMethod_NoVoid_NoParameter_OneTypeParameter_ByObjects()
        {
            var testClassType = typeof(TestClass);
            var m = testClassType.StaticGenericMethod("InternalStaticGenericMethod", null,
                new[] {testClassType});
            Assert.IsNotNull(m);
            var result = m(new object[] { });
            Assert.AreEqual(null, TestClass.StaticPublicParams);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), TestClass.StaticPublicTypeParams[0]);
        }

        [TestMethod]
        public void Protected_GenericMethod_NoVoid_NoParameter_OneTypeParameter_ByObjects()
        {
            var testClassType = typeof(TestClass);
            var m = testClassType.StaticGenericMethod("ProtectedStaticGenericMethod", null,
                new[] {testClassType});
            Assert.IsNotNull(m);
            var result = m(new object[] { });
            Assert.AreEqual(null, TestClass.StaticPublicParams);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), TestClass.StaticPublicTypeParams[0]);
        }

        [TestMethod]
        public void Private_GenericMethod_NoVoid_NoParameter_OneTypeParameter_ByObjects()
        {
            var testClassType = typeof(TestClass);
            var m = testClassType.StaticGenericMethod("PrivateStaticGenericMethod", null,
                new[] {testClassType});
            Assert.IsNotNull(m);
            var result = m(new object[] { });
            Assert.AreEqual(null, TestClass.StaticPublicParams);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), TestClass.StaticPublicTypeParams[0]);
        }

        [TestMethod]
        public void GenericMethod_NoVoid_SingleParameter_OneTypeParameter_ByObjects()
        {
            var testClassType = typeof(TestClass);
            var m = testClassType.StaticGenericMethod("StaticGenericMethod", new[] {testClassType},
                new[] {testClassType});
            var testClassInstance = new TestClass();
            var result = m(new object[] {testClassInstance});
            Assert.AreEqual(1, TestClass.StaticPublicParams.Length);
            Assert.AreEqual(testClassInstance, TestClass.StaticPublicParams[0]);
            Assert.AreEqual(testClassInstance, result);
            Assert.AreEqual(typeof(TestClass), TestClass.StaticPublicTypeParams[0]);
        }

        [TestMethod]
        public void GenericMethod_NoVoid_2Parameter_OneTypeParameter_ByObjects()
        {
            var testClassType = typeof(TestClass);
            var m = testClassType.StaticGenericMethod("StaticGenericMethod", new[] {testClassType, typeof(int)},
                new[] {testClassType});
            Assert.IsNotNull(m);
            var testClassInstance = new TestClass();
            var result = m(new object[] {testClassInstance, 0});
            Assert.AreEqual(2, TestClass.StaticPublicParams.Length);
            Assert.AreEqual(testClassInstance, TestClass.StaticPublicParams[0]);
            Assert.AreEqual(0, TestClass.StaticPublicParams[1]);
            Assert.AreEqual(testClassInstance, result);
            Assert.AreEqual(typeof(TestClass), TestClass.StaticPublicTypeParams[0]);
        }

        [TestMethod]
        public void GenericMethod_NoVoid_3Parameter_OneTypeParameter_ByObjects()
        {
            var testStructType = typeof(TestStruct);
            var m = typeof(TestClass).StaticGenericMethod("StaticGenericMethod",
                new[] {testStructType, typeof(int), typeof(bool)}, new[] {testStructType});
            Assert.IsNotNull(m);
            var testStructInstance = new TestStruct();
            var result = m(new object[] {testStructInstance, 0, true});
            Assert.AreEqual(3, TestClass.StaticPublicParams.Length);
            Assert.AreEqual(testStructInstance, TestClass.StaticPublicParams[0]);
            Assert.AreEqual(0, TestClass.StaticPublicParams[1]);
            Assert.AreEqual(true, TestClass.StaticPublicParams[2]);
            Assert.AreEqual(testStructInstance, result);
            Assert.AreEqual(typeof(TestStruct), TestClass.StaticPublicTypeParams[0]);
        }

        [TestMethod]
        public void GenericMethod_NoVoid_NoParameter_2TypeParameters_ByObjects()
        {
            var testClassType = typeof(TestClass);
            var m = testClassType.StaticGenericMethod("StaticGenericMethod", null,
                new[] {testClassType, typeof(TestStruct)});
            Assert.IsNotNull(m);
            var result = m(new object[0]);
            Assert.AreEqual(null, TestClass.StaticPublicParams);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), TestClass.StaticPublicTypeParams[0]);
            Assert.AreEqual(typeof(TestStruct), TestClass.StaticPublicTypeParams[1]);
        }

        [TestMethod]
        public void GenericMethod_NoVoid_SingleParameter_3TypeParameters_ByObjects()
        {
            var testClassType = typeof(TestClass);
            var m = testClassType.StaticGenericMethod("StaticGenericMethod", new[] {typeof(int)},
                new[] {testClassType, typeof(TestStruct), typeof(TestClassNoDefaultCtor)});
            Assert.IsNotNull(m);
            var result = m(new object[] {0});
            Assert.AreEqual(1, TestClass.StaticPublicParams.Length);
            Assert.AreEqual(0, TestClass.StaticPublicParams[0]);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), TestClass.StaticPublicTypeParams[0]);
            Assert.AreEqual(typeof(TestStruct), TestClass.StaticPublicTypeParams[1]);
            Assert.AreEqual(typeof(TestClassNoDefaultCtor), TestClass.StaticPublicTypeParams[2]);
        }

        [TestMethod]
        public void InstanceGenericMethod_DoNotThrowExc_When_ParametersArray_IsNull()
        {
            var testClassType = typeof(TestClass);
            var m = testClassType.StaticGenericMethod("InternalStaticGenericMethod", null,
                new[] {testClassType});
            Assert.IsNotNull(m);
        }

        [TestMethod]
        public void InstanceGenericMethod_DoNotThrowExc_When_TypeParametersArray_IsNull()
        {
            var m = typeof(TestClass).StaticGenericMethod("StaticPublic", new Type[0], null);
            Assert.IsNotNull(m);
        }
    }
}