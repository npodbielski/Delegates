using System;
using Delegates;
using DelegatesTest.TestObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace DelegatesTest
{
    [TestClass]
    public class DelegateFactoryTests_StaticMethods_Generic
    {
        private readonly TestClass _testClassInstance = new TestClass();
        private readonly Type _testClassType = typeof(TestClass);
        private readonly Type _testStructType = typeof(TestStruct);
        private readonly TestStruct _testStructInstance = new TestStruct();

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
            var result = m(_testClassInstance);
            Assert.AreEqual(1, TestClass.StaticPublicParams.Length);
            Assert.AreEqual(_testClassInstance, TestClass.StaticPublicParams[0]);
            Assert.AreEqual(_testClassInstance, result);
            Assert.AreEqual(typeof(TestClass), TestClass.StaticPublicTypeParams[0]);
        }

        [TestMethod]
        public void GenericMethod_NoVoid_2Parameter_OneTypeParameter_ByTypes()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<TestClass, int, TestClass>, TestClass>
                ("StaticGenericMethod");
            Assert.IsNotNull(m);
            var result = m(_testClassInstance, 0);
            Assert.AreEqual(2, TestClass.StaticPublicParams.Length);
            Assert.AreEqual(_testClassInstance, TestClass.StaticPublicParams[0]);
            Assert.AreEqual(_testClassInstance, result);
            Assert.AreEqual(typeof(TestClass), TestClass.StaticPublicTypeParams[0]);
        }

        [TestMethod]
        public void GenericMethod_NoVoid_3Parameter_OneTypeParameter_ByTypes()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<TestStruct, int, bool, TestStruct>, TestStruct>
                ("StaticGenericMethod");
            Assert.IsNotNull(m);
            var result = m(_testStructInstance, 0, true);
            Assert.AreEqual(3, TestClass.StaticPublicParams.Length);
            Assert.AreEqual(_testStructInstance, TestClass.StaticPublicParams[0]);
            Assert.AreEqual(0, TestClass.StaticPublicParams[1]);
            Assert.AreEqual(true, TestClass.StaticPublicParams[2]);
            Assert.AreEqual(_testStructInstance, result);
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
            var m = DelegateFactory.StaticMethod<TestClass, Func<int, TestClass>, TestClass, TestStruct, TestClassNoDefaultCtor>
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
            var m = DelegateFactory.StaticMethod<TestClass, Func<Derived, Derived>, Derived>("StaticGenericMethodWithType");
            Assert.IsNotNull(m);
        }

        [TestMethod]
        public void GenericMethod_TypeParameter_NotAssignebleToType()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<TestClass, TestClass>, TestClass>("StaticGenericMethodWithType");
            Assert.IsNull(m);
        }

        [TestMethod]
        public void GenericMethod_TypeParameter_AssignebleToClass()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<TestClass, TestClass>, TestClass>("StaticGenericMethodWithClass");
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
            var m = DelegateFactory.StaticMethod<TestClass, Func<TestClass, TestClass>, TestClass>("StaticGenericMethodWithStruc");
            Assert.IsNull(m);
        }

        [TestMethod]
        public void GenericMethod_TypeParameter_WithTypeParamsInheritance()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<Base, Base>, Base, Derived>("StaticGenericMethodFromOtherParameter");
            Assert.IsNotNull(m);
        }

        [TestMethod]
        public void GenericMethod_TypeParameter_WithoutTypeParamsInheritance()
        {
            var m = DelegateFactory.StaticMethod<TestClass, Func<TestClass, TestClass>, TestClass, TestClassNoDefaultCtor>
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
            var m = DelegateFactory.StaticMethod<GenericClass<TestClass>, Func<TestClass, TestClass>>("StaticGenericMethod");
            Assert.IsNotNull(m);
            var result = m(_testClassInstance);
            Assert.AreEqual(_testClassInstance, result);
        }

        [TestMethod]
        public void Public_GenericMethod_Void_NoParameter_OneTypeParameter_ByObjectAndTypes()
        {
            var m = _testClassType.StaticMethod<Action, string>("PublicStaticGenericMethodVoid");
            Assert.IsNotNull(m);
            m();
            Assert.AreEqual(null, TestClass.StaticPublicParams);
            Assert.AreEqual(typeof(string), TestClass.StaticPublicTypeParams[0]);
        }

        [TestMethod]
        public void Internal_GenericMethod_Void_NoParameter_OneTypeParameter_ByObjectAndTypes()
        {
            var m = _testClassType.StaticMethod<Action, string>("InternalStaticGenericMethodVoid");
            Assert.IsNotNull(m);
            m();
            Assert.AreEqual(null, TestClass.StaticPublicParams);
            Assert.AreEqual(typeof(string), TestClass.StaticPublicTypeParams[0]);
        }

        [TestMethod]
        public void Protected_GenericMethod_Void_NoParameter_OneTypeParameter_ByObjectAndTypes()
        {
            var m = _testClassType.StaticMethod<Action, string>("ProtectedStaticGenericMethodVoid");
            Assert.IsNotNull(m);
            m();
            Assert.AreEqual(null, TestClass.StaticPublicParams);
            Assert.AreEqual(typeof(string), TestClass.StaticPublicTypeParams[0]);
        }

        [TestMethod]
        public void Private_GenericMethod_Void_NoParameter_OneTypeParameter_ByObjectAndTypes()
        {
            var m = _testClassType.StaticMethod<Action, string>("PrivateStaticGenericMethodVoid");
            Assert.IsNotNull(m);
            m();
            Assert.AreEqual(null, TestClass.StaticPublicParams);
            Assert.AreEqual(typeof(string), TestClass.StaticPublicTypeParams[0]);
        }

        [TestMethod]
        public void Public_GenericMethod_NoVoid_NoParameter_OneTypeParameter_ByObjectAndTypes()
        {
            var m = _testClassType.StaticMethod<Func<TestClass>, TestClass>("PublicStaticGenericMethod");
            Assert.IsNotNull(m);
            var result = m();
            Assert.AreEqual(null, TestClass.StaticPublicParams);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), TestClass.StaticPublicTypeParams[0]);
        }

        [TestMethod]
        public void Internal_GenericMethod_NoVoid_NoParameter_OneTypeParameter_ByObjectAndTypes()
        {
            var m = _testClassType.StaticMethod<Func<TestClass>, TestClass>("InternalStaticGenericMethod");
            Assert.IsNotNull(m);
            var result = m();
            Assert.AreEqual(null, TestClass.StaticPublicParams);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), TestClass.StaticPublicTypeParams[0]);
        }

        [TestMethod]
        public void Protected_GenericMethod_NoVoid_NoParameter_OneTypeParameter_ByObjectAndTypes()
        {
            var m = _testClassType.StaticMethod<Func<TestClass>, TestClass>("ProtectedStaticGenericMethod");
            Assert.IsNotNull(m);
            var result = m();
            Assert.AreEqual(null, TestClass.StaticPublicParams);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), TestClass.StaticPublicTypeParams[0]);
        }

        [TestMethod]
        public void Private_GenericMethod_NoVoid_NoParameter_OneTypeParameter_ByObjectAndTypes()
        {
            var m = _testClassType.StaticMethod<Func<TestClass>, TestClass>("PrivateStaticGenericMethod");
            Assert.IsNotNull(m);
            var result = m();
            Assert.AreEqual(null, TestClass.StaticPublicParams);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), TestClass.StaticPublicTypeParams[0]);
        }

        [TestMethod]
        public void GenericMethod_NoVoid_SingleParameter_OneTypeParameter_ByObjectAndTypes()
        {
            var m = _testClassType.StaticMethod<Func<TestClass, TestClass>, TestClass>("StaticGenericMethod");
            Assert.IsNotNull(m);
            var result = m(_testClassInstance);
            Assert.AreEqual(1, TestClass.StaticPublicParams.Length);
            Assert.AreEqual(_testClassInstance, TestClass.StaticPublicParams[0]);
            Assert.AreEqual(_testClassInstance, result);
            Assert.AreEqual(typeof(TestClass), TestClass.StaticPublicTypeParams[0]);
        }

        [TestMethod]
        public void GenericMethod_NoVoid_2Parameter_OneTypeParameter_ByObjectAndTypes()
        {
            var m = _testClassType.StaticMethod<Func<TestClass, int, TestClass>, TestClass>
                ("StaticGenericMethod");
            Assert.IsNotNull(m);
            var result = m(_testClassInstance, 0);
            Assert.AreEqual(2, TestClass.StaticPublicParams.Length);
            Assert.AreEqual(_testClassInstance, TestClass.StaticPublicParams[0]);
            Assert.AreEqual(0, TestClass.StaticPublicParams[1]);
            Assert.AreEqual(_testClassInstance, result);
            Assert.AreEqual(typeof(TestClass), TestClass.StaticPublicTypeParams[0]);
        }

        [TestMethod]
        public void GenericMethod_NoVoid_3Parameter_OneTypeParameter_ByObjectAndTypes()
        {
            var m = _testClassType.StaticMethod<Func<TestStruct, int, bool, TestStruct>, TestStruct>
                ("StaticGenericMethod");
            Assert.IsNotNull(m);
            var result = m(_testStructInstance, 0, true);
            Assert.AreEqual(3, TestClass.StaticPublicParams.Length);
            Assert.AreEqual(_testStructInstance, TestClass.StaticPublicParams[0]);
            Assert.AreEqual(0, TestClass.StaticPublicParams[1]);
            Assert.AreEqual(true, TestClass.StaticPublicParams[2]);
            Assert.AreEqual(_testStructInstance, result);
            Assert.AreEqual(typeof(TestStruct), TestClass.StaticPublicTypeParams[0]);
        }


        [TestMethod]
        public void GenericMethod_NoVoid_NoParameter_2TypeParameters_ByObjectAndTypes()
        {
            var m = _testClassType.StaticMethod<Func<TestClass>, TestClass, TestStruct>
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
            var m = _testClassType.StaticMethod<Func<int, TestClass>, TestClass, TestStruct, TestClassNoDefaultCtor>
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
            var m = _testClassType.StaticGenericMethodVoid("PublicStaticGenericMethodVoid", null,
                new[] { typeof(string) });
            Assert.IsNotNull(m);
            m(new object[0]);
            Assert.AreEqual(null, TestClass.StaticPublicParams);
            Assert.AreEqual(typeof(string), TestClass.StaticPublicTypeParams[0]);
        }

        [TestMethod]
        public void Internal_GenericMethod_Void_NoParameter_OneTypeParameter_ByObjects()
        {
            var m = _testClassType.StaticGenericMethodVoid("InternalStaticGenericMethodVoid", null,
                new[] { typeof(string) });
            Assert.IsNotNull(m);
            m(new object[0]);
            Assert.AreEqual(null, TestClass.StaticPublicParams);
            Assert.AreEqual(typeof(string), TestClass.StaticPublicTypeParams[0]);
        }

        [TestMethod]
        public void Protected_GenericMethod_Void_NoParameter_OneTypeParameter_ByObjects()
        {
            var m = _testClassType.StaticGenericMethodVoid("ProtectedStaticGenericMethodVoid", null,
                new[] { typeof(string) });
            Assert.IsNotNull(m);
            m(new object[0]);
            Assert.AreEqual(null, TestClass.StaticPublicParams);
            Assert.AreEqual(typeof(string), TestClass.StaticPublicTypeParams[0]);
        }

        [TestMethod]
        public void Private_GenericMethod_Void_NoParameter_OneTypeParameter_ByObjects()
        {
            var m = _testClassType.StaticGenericMethodVoid("PrivateStaticGenericMethodVoid", null,
                new[] { typeof(string) });
            Assert.IsNotNull(m);
            m(new object[0]);
            Assert.AreEqual(null, TestClass.StaticPublicParams);
            Assert.AreEqual(typeof(string), TestClass.StaticPublicTypeParams[0]);
        }

        [TestMethod]
        public void Public_GenericMethod_NoVoid_NoParameter_OneTypeParameter_ByObjects()
        {
            var m = _testClassType.StaticGenericMethod("PublicStaticGenericMethod", null,
                new[] { _testClassType });
            Assert.IsNotNull(m);
            var result = m(new object[] { });
            Assert.AreEqual(null, TestClass.StaticPublicParams);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), TestClass.StaticPublicTypeParams[0]);
        }

        [TestMethod]
        public void Internal_GenericMethod_NoVoid_NoParameter_OneTypeParameter_ByObjects()
        {
            var m = _testClassType.StaticGenericMethod("InternalStaticGenericMethod", null,
                new[] { _testClassType });
            Assert.IsNotNull(m);
            var result = m(new object[] { });
            Assert.AreEqual(null, TestClass.StaticPublicParams);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), TestClass.StaticPublicTypeParams[0]);
        }

        [TestMethod]
        public void Protected_GenericMethod_NoVoid_NoParameter_OneTypeParameter_ByObjects()
        {
            var m = _testClassType.StaticGenericMethod("ProtectedStaticGenericMethod", null,
                new[] { _testClassType });
            Assert.IsNotNull(m);
            var result = m(new object[] { });
            Assert.AreEqual(null, TestClass.StaticPublicParams);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), TestClass.StaticPublicTypeParams[0]);
        }

        [TestMethod]
        public void Private_GenericMethod_NoVoid_NoParameter_OneTypeParameter_ByObjects()
        {
            var m = _testClassType.StaticGenericMethod("PrivateStaticGenericMethod", null,
                new[] { _testClassType });
            Assert.IsNotNull(m);
            var result = m(new object[] { });
            Assert.AreEqual(null, TestClass.StaticPublicParams);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), TestClass.StaticPublicTypeParams[0]);
        }

        [TestMethod]
        public void GenericMethod_NoVoid_SingleParameter_OneTypeParameter_ByObjects()
        {
            var m = _testClassType.StaticGenericMethod("StaticGenericMethod", new[] { _testClassType },
                new[] { _testClassType });
            var result = m(new object[] { _testClassInstance });
            Assert.AreEqual(1, TestClass.StaticPublicParams.Length);
            Assert.AreEqual(_testClassInstance, TestClass.StaticPublicParams[0]);
            Assert.AreEqual(_testClassInstance, result);
            Assert.AreEqual(typeof(TestClass), TestClass.StaticPublicTypeParams[0]);
        }

        [TestMethod]
        public void GenericMethod_NoVoid_2Parameter_OneTypeParameter_ByObjects()
        {
            var m = _testClassType.StaticGenericMethod("StaticGenericMethod", new[] { _testClassType, typeof(int) },
                new[] { _testClassType });
            Assert.IsNotNull(m);
            var result = m(new object[] { _testClassInstance, 0 });
            Assert.AreEqual(2, TestClass.StaticPublicParams.Length);
            Assert.AreEqual(_testClassInstance, TestClass.StaticPublicParams[0]);
            Assert.AreEqual(0, TestClass.StaticPublicParams[1]);
            Assert.AreEqual(_testClassInstance, result);
            Assert.AreEqual(typeof(TestClass), TestClass.StaticPublicTypeParams[0]);
        }

        [TestMethod]
        public void GenericMethod_NoVoid_3Parameter_OneTypeParameter_ByObjects()
        {
            var m = _testClassType.StaticGenericMethod("StaticGenericMethod",
                new[] { _testStructType, typeof(int), typeof(bool) }, new[] { _testStructType });
            Assert.IsNotNull(m);
            var result = m(new object[] { _testStructInstance, 0, true });
            Assert.AreEqual(3, TestClass.StaticPublicParams.Length);
            Assert.AreEqual(_testStructInstance, TestClass.StaticPublicParams[0]);
            Assert.AreEqual(0, TestClass.StaticPublicParams[1]);
            Assert.AreEqual(true, TestClass.StaticPublicParams[2]);
            Assert.AreEqual(_testStructInstance, result);
            Assert.AreEqual(typeof(TestStruct), TestClass.StaticPublicTypeParams[0]);
        }

        [TestMethod]
        public void GenericMethod_NoVoid_NoParameter_2TypeParameters_ByObjects()
        {
            var m = _testClassType.StaticGenericMethod("StaticGenericMethod", null,
                new[] { _testClassType, _testStructType });
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
            var m = _testClassType.StaticGenericMethod("StaticGenericMethod", new[] { typeof(int) },
                new[] { _testClassType, _testStructType, typeof(TestClassNoDefaultCtor) });
            Assert.IsNotNull(m);
            var result = m(new object[] { 0 });
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
            var m = _testClassType.StaticGenericMethod("InternalStaticGenericMethod", null,
                new[] { _testClassType });
            Assert.IsNotNull(m);
        }

        [TestMethod]
        public void InstanceGenericMethod_DoNotThrowExc_When_TypeParametersArray_IsNull()
        {
            var m = _testClassType.StaticGenericMethod("StaticPublic", new Type[0], null);
            Assert.IsNotNull(m);
        }
    }
}
