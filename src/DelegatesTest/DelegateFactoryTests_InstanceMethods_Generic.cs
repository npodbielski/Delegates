using System;
using Delegates;
using DelegatesTest.TestObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace DelegatesTest
{
    [TestClass]
    public class DelegateFactoryTests_InstanceMethods_Generic
    {
        private const string TestValue = "test";
        private readonly TestClass _testClassInstance = new TestClass();
        private readonly Type _testClassType = typeof(TestClass);
        private readonly Type _testStructType = typeof(TestStruct);
        private readonly TestStruct _testStructInstance = new TestStruct(0);
        private readonly GenericClass<TestClass> _genericClassInstance = new GenericClass<TestClass>();
        private readonly IService _interfaceImpl = new Service();
        private readonly Type _interfaceType = typeof(IService);

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
            m(_testClassInstance);
            Assert.AreEqual(null, _testClassInstance.PublicParams);
            Assert.AreEqual(typeof(string), _testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void Internal_GenericMethod_Void_NoParameter_OneTypeParameter_ByTypes()
        {
            var m = DelegateFactory.InstanceMethod<Action<TestClass>, string>("InternalInstanceGenericMethodVoid");
            Assert.IsNotNull(m);
            m(_testClassInstance);
            Assert.AreEqual(null, _testClassInstance.PublicParams);
            Assert.AreEqual(typeof(string), _testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void Protected_GenericMethod_Void_NoParameter_OneTypeParameter_ByTypes()
        {
            var m = DelegateFactory.InstanceMethod<Action<TestClass>, string>("ProtectedInstanceGenericMethodVoid");
            Assert.IsNotNull(m);
            m(_testClassInstance);
            Assert.AreEqual(null, _testClassInstance.PublicParams);
            Assert.AreEqual(typeof(string), _testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void Private_GenericMethod_Void_NoParameter_OneTypeParameter_ByTypes()
        {
            var m = DelegateFactory.InstanceMethod<Action<TestClass>, string>("PrivateInstanceGenericMethodVoid");
            Assert.IsNotNull(m);
            m(_testClassInstance);
            Assert.AreEqual(null, _testClassInstance.PublicParams);
            Assert.AreEqual(typeof(string), _testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void Public_GenericMethod_NoVoid_NoParameter_OneTypeParameter_ByTypes()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, TestClass>, TestClass>("PublicInstanceGenericMethod");
            Assert.IsNotNull(m);
            var result = m(_testClassInstance);
            Assert.AreEqual(null, _testClassInstance.PublicParams);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), _testClassInstance.PublicTypeParams[0]);
        }


        [TestMethod]
        public void Internal_GenericMethod_NoVoid_NoParameter_OneTypeParameter_ByTypes()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, TestClass>, TestClass>("InternalInstanceGenericMethod");
            Assert.IsNotNull(m);
            var result = m(_testClassInstance);
            Assert.AreEqual(null, _testClassInstance.PublicParams);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), _testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void Protected_GenericMethod_NoVoid_NoParameter_OneTypeParameter_ByTypes()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, TestClass>, TestClass>("ProtectedInstanceGenericMethod");
            Assert.IsNotNull(m);
            var result = m(_testClassInstance);
            Assert.AreEqual(null, _testClassInstance.PublicParams);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), _testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void Private_GenericMethod_NoVoid_NoParameter_OneTypeParameter_ByTypes()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, TestClass>, TestClass>("PrivateInstanceGenericMethod");
            Assert.IsNotNull(m);
            var result = m(_testClassInstance);
            Assert.AreEqual(null, _testClassInstance.PublicParams);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), _testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void GenericMethod_NoVoid_SingleParameter_OneTypeParameter_ByTypes()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, TestClass, TestClass>, TestClass>
                ("InstanceGenericMethod");
            Assert.IsNotNull(m);
            var result = m(_testClassInstance, _testClassInstance);
            Assert.AreEqual(1, _testClassInstance.PublicParams.Length);
            Assert.AreEqual(_testClassInstance, _testClassInstance.PublicParams[0]);
            Assert.AreEqual(_testClassInstance, result);
            Assert.AreEqual(typeof(TestClass), _testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void GenericMethod_NoVoid_2Parameter_OneTypeParameter_ByTypes()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, TestClass, int, TestClass>, TestClass>
                ("InstanceGenericMethod");
            Assert.IsNotNull(m);
            var result = m(_testClassInstance, _testClassInstance, 0);
            Assert.AreEqual(2, _testClassInstance.PublicParams.Length);
            Assert.AreEqual(_testClassInstance, _testClassInstance.PublicParams[0]);
            Assert.AreEqual(_testClassInstance, result);
            Assert.AreEqual(typeof(TestClass), _testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void GenericMethod_NoVoid_3Parameter_OneTypeParameter_ByTypes()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, TestStruct, int, bool, TestStruct>, TestStruct>
                ("InstanceGenericMethod");
            Assert.IsNotNull(m);
            var result = m(_testClassInstance, _testStructInstance, 0, true);
            Assert.AreEqual(3, _testClassInstance.PublicParams.Length);
            Assert.AreEqual(_testStructInstance, _testClassInstance.PublicParams[0]);
            Assert.AreEqual(0, _testClassInstance.PublicParams[1]);
            Assert.AreEqual(true, _testClassInstance.PublicParams[2]);
            Assert.AreEqual(_testStructInstance, result);
            Assert.AreEqual(typeof(TestStruct), _testClassInstance.PublicTypeParams[0]);
        }


        [TestMethod]
        public void GenericMethod_NoVoid_NoParameter_2TypeParameters_ByTypes()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, TestClass>, TestClass, TestStruct>
                ("InstanceGenericMethod");
            Assert.IsNotNull(m);
            var result = m(_testClassInstance);
            Assert.AreEqual(null, _testClassInstance.PublicParams);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), _testClassInstance.PublicTypeParams[0]);
            Assert.AreEqual(typeof(TestStruct), _testClassInstance.PublicTypeParams[1]);
        }

        [TestMethod]
        public void GenericMethod_NoVoid_SingleParameter_3TypeParameters_ByTypes()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, int, TestClass>, TestClass, TestStruct, TestClassNoDefaultCtor>
                ("InstanceGenericMethod");
            Assert.IsNotNull(m);
            var result = m(_testClassInstance, 0);
            Assert.AreEqual(1, _testClassInstance.PublicParams.Length);
            Assert.AreEqual(0, _testClassInstance.PublicParams[0]);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), _testClassInstance.PublicTypeParams[0]);
            Assert.AreEqual(typeof(TestStruct), _testClassInstance.PublicTypeParams[1]);
            Assert.AreEqual(typeof(TestClassNoDefaultCtor), _testClassInstance.PublicTypeParams[2]);
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
            var m = DelegateFactory.InstanceMethod<Func<TestClass, TestClass>, TestClass>("PublicInstanceGenericMethod");
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
            var m = DelegateFactory.InstanceMethod<Func<TestClass, Derived, Derived>, Derived>("InstanceGenericMethodWithType");
            Assert.IsNotNull(m);
        }

        [TestMethod]
        public void GenericMethod_TypeParameter_NotAssignebleToType()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, TestClass, TestClass>, TestClass>("InstanceGenericMethodWithType");
            Assert.IsNull(m);
        }

        [TestMethod]
        public void GenericMethod_TypeParameter_AssignebleToClass()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, TestClass, TestClass>, TestClass>("InstanceGenericMethodWithClass");
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
            var m = DelegateFactory.InstanceMethod<Func<TestClass, TestClass, TestClass>, TestClass>("InstanceGenericMethodWithStruc");
            Assert.IsNull(m);
        }

        [TestMethod]
        public void GenericMethod_TypeParameter_WithTypeParamsInheritance()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, Base, Base>, Base, Derived>("InstanceGenericMethodFromOtherParameter");
            Assert.IsNotNull(m);
        }

        [TestMethod]
        public void GenericMethod_TypeParameter_WithoutTypeParamsInheritance()
        {
            var m = DelegateFactory.InstanceMethod<Func<TestClass, TestClass, TestClass>, TestClass, TestClassNoDefaultCtor>
                ("InstanceGenericMethodFromOtherParameter");
            Assert.IsNull(m);
        }

        [TestMethod]
        public void GenericMethod_SingleTypeParameter_FromStruct()
        {
            var m = DelegateFactory.InstanceMethod<Action<TestClass, TestClass>, TestClass>("InstanceGenericMethodVoid");
            Assert.IsNotNull(m);
            m(_testClassInstance, _testClassInstance);
            Assert.AreEqual(_testClassInstance, _testClassInstance.InstanceGenericMethodVoidParameter);
        }

        [TestMethod]
        public void NonGenericMethod_FromGenericClass()
        {
            var m = DelegateFactory.InstanceMethod<Func<GenericClass<TestClass>, TestClass, TestClass>>("InstanceGenericMethod");
            Assert.IsNotNull(m);
            var result = m(_genericClassInstance, _testClassInstance);
            Assert.AreEqual(_testClassInstance, result);
        }

        [TestMethod]
        public void Public_GenericMethod_Void_NoParameter_OneTypeParameter_ByObjectAndTypes()
        {
            var m = _testClassType.InstanceMethod<Action<TestClass>, string>("PublicInstanceGenericMethodVoid");
            Assert.IsNotNull(m);
            m(_testClassInstance);
            Assert.AreEqual(null, _testClassInstance.PublicParams);
            Assert.AreEqual(typeof(string), _testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void Public_GenericMethod_Void_NoParameter_OneTypeParameter_ByObjectAndTypes_WithCast()
        {
            var m = _testClassType.InstanceMethod<Action<object>, string>("PublicInstanceGenericMethodVoid");
            Assert.IsNotNull(m);
            m(_testClassInstance);
            Assert.AreEqual(null, _testClassInstance.PublicParams);
            Assert.AreEqual(typeof(string), _testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void Internal_GenericMethod_Void_NoParameter_OneTypeParameter_ByObjectAndTypes()
        {
            var m = _testClassType.InstanceMethod<Action<TestClass>, string>("InternalInstanceGenericMethodVoid");
            Assert.IsNotNull(m);
            m(_testClassInstance);
            Assert.AreEqual(null, _testClassInstance.PublicParams);
            Assert.AreEqual(typeof(string), _testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void Internal_GenericMethod_Void_NoParameter_OneTypeParameter_ByObjectAndTypes_WithCast()
        {
            var m = _testClassType.InstanceMethod<Action<object>, string>("InternalInstanceGenericMethodVoid");
            Assert.IsNotNull(m);
            m(_testClassInstance);
            Assert.AreEqual(null, _testClassInstance.PublicParams);
            Assert.AreEqual(typeof(string), _testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void Protected_GenericMethod_Void_NoParameter_OneTypeParameter_ByObjectAndTypes()
        {
            var m = _testClassType.InstanceMethod<Action<TestClass>, string>("ProtectedInstanceGenericMethodVoid");
            Assert.IsNotNull(m);
            m(_testClassInstance);
            Assert.AreEqual(null, _testClassInstance.PublicParams);
            Assert.AreEqual(typeof(string), _testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void Protected_GenericMethod_Void_NoParameter_OneTypeParameter_ByObjectAndTypes_WithCast()
        {
            var m = _testClassType.InstanceMethod<Action<object>, string>("ProtectedInstanceGenericMethodVoid");
            Assert.IsNotNull(m);
            m(_testClassInstance);
            Assert.AreEqual(null, _testClassInstance.PublicParams);
            Assert.AreEqual(typeof(string), _testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void Private_GenericMethod_Void_NoParameter_OneTypeParameter_ByObjectAndTypes()
        {
            var m = _testClassType.InstanceMethod<Action<TestClass>, string>("PrivateInstanceGenericMethodVoid");
            Assert.IsNotNull(m);
            m(_testClassInstance);
            Assert.AreEqual(null, _testClassInstance.PublicParams);
            Assert.AreEqual(typeof(string), _testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void Private_GenericMethod_Void_NoParameter_OneTypeParameter_ByObjectAndTypes_WithCast()
        {
            var m = _testClassType.InstanceMethod<Action<object>, string>("PrivateInstanceGenericMethodVoid");
            Assert.IsNotNull(m);
            m(_testClassInstance);
            Assert.AreEqual(null, _testClassInstance.PublicParams);
            Assert.AreEqual(typeof(string), _testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void Public_GenericMethod_NoVoid_NoParameter_OneTypeParameter_ByObjectAndTypes()
        {
            var m = _testClassType.InstanceMethod<Func<TestClass, TestClass>, TestClass>("PublicInstanceGenericMethod");
            Assert.IsNotNull(m);
            var result = m(_testClassInstance);
            Assert.AreEqual(null, _testClassInstance.PublicParams);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), _testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void Public_GenericMethod_NoVoid_NoParameter_OneTypeParameter_ByObjectAndTypes_WithCast()
        {
            var m = _testClassType.InstanceMethod<Func<object, TestClass>, TestClass>("PublicInstanceGenericMethod");
            Assert.IsNotNull(m);
            var result = m(_testClassInstance);
            Assert.AreEqual(null, _testClassInstance.PublicParams);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), _testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void Internal_GenericMethod_NoVoid_NoParameter_OneTypeParameter_ByObjectAndTypes()
        {
            var m = _testClassType.InstanceMethod<Func<TestClass, TestClass>, TestClass>("InternalInstanceGenericMethod");
            Assert.IsNotNull(m);
            var result = m(_testClassInstance);
            Assert.AreEqual(null, _testClassInstance.PublicParams);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), _testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void Internal_GenericMethod_NoVoid_NoParameter_OneTypeParameter_ByObjectAndTypes_WithCast()
        {
            var m = _testClassType.InstanceMethod<Func<object, TestClass>, TestClass>("InternalInstanceGenericMethod");
            Assert.IsNotNull(m);
            var result = m(_testClassInstance);
            Assert.AreEqual(null, _testClassInstance.PublicParams);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), _testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void Protected_GenericMethod_NoVoid_NoParameter_OneTypeParameter_ByObjectAndTypes()
        {
            var m = _testClassType.InstanceMethod<Func<TestClass, TestClass>, TestClass>("ProtectedInstanceGenericMethod");
            Assert.IsNotNull(m);
            var result = m(_testClassInstance);
            Assert.AreEqual(null, _testClassInstance.PublicParams);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), _testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void Protected_GenericMethod_NoVoid_NoParameter_OneTypeParameter_ByObjectAndTypes_WithCast()
        {
            var m = _testClassType.InstanceMethod<Func<object, TestClass>, TestClass>("ProtectedInstanceGenericMethod");
            Assert.IsNotNull(m);
            var result = m(_testClassInstance);
            Assert.AreEqual(null, _testClassInstance.PublicParams);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), _testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void Private_GenericMethod_NoVoid_NoParameter_OneTypeParameter_ByObjectAndTypes()
        {
            var m = _testClassType.InstanceMethod<Func<TestClass, TestClass>, TestClass>("PrivateInstanceGenericMethod");
            Assert.IsNotNull(m);
            var result = m(_testClassInstance);
            Assert.AreEqual(null, _testClassInstance.PublicParams);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), _testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void Private_GenericMethod_NoVoid_NoParameter_OneTypeParameter_ByObjectAndTypes_WithCast()
        {
            var m = _testClassType.InstanceMethod<Func<object, TestClass>, TestClass>("PrivateInstanceGenericMethod");
            Assert.IsNotNull(m);
            var result = m(_testClassInstance);
            Assert.AreEqual(null, _testClassInstance.PublicParams);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), _testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void GenericMethod_NoVoid_SingleParameter_OneTypeParameter_ByObjectAndTypes()
        {
            var m = _testClassType.InstanceMethod<Func<TestClass, TestClass, TestClass>, TestClass>("InstanceGenericMethod");
            Assert.IsNotNull(m);
            var result = m(_testClassInstance, _testClassInstance);
            Assert.AreEqual(1, _testClassInstance.PublicParams.Length);
            Assert.AreEqual(_testClassInstance, _testClassInstance.PublicParams[0]);
            Assert.AreEqual(_testClassInstance, result);
            Assert.AreEqual(typeof(TestClass), _testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void GenericMethod_NoVoid_SingleParameter_OneTypeParameter_ByObjectAndTypes_WithCast()
        {
            var m = _testClassType.InstanceMethod<Func<object, TestClass, TestClass>, TestClass>("InstanceGenericMethod");
            Assert.IsNotNull(m);
            var result = m(_testClassInstance, _testClassInstance);
            Assert.AreEqual(1, _testClassInstance.PublicParams.Length);
            Assert.AreEqual(_testClassInstance, _testClassInstance.PublicParams[0]);
            Assert.AreEqual(_testClassInstance, result);
            Assert.AreEqual(typeof(TestClass), _testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void GenericMethod_NoVoid_2Parameter_OneTypeParameter_ByObjectAndTypes()
        {
            var m = _testClassType.InstanceMethod<Func<TestClass, TestClass, int, TestClass>, TestClass>
                ("InstanceGenericMethod");
            Assert.IsNotNull(m);
            var result = m(_testClassInstance, _testClassInstance, 0);
            Assert.AreEqual(2, _testClassInstance.PublicParams.Length);
            Assert.AreEqual(_testClassInstance, _testClassInstance.PublicParams[0]);
            Assert.AreEqual(0, _testClassInstance.PublicParams[1]);
            Assert.AreEqual(_testClassInstance, result);
            Assert.AreEqual(typeof(TestClass), _testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void GenericMethod_NoVoid_2Parameter_OneTypeParameter_ByObjectAndTypes_WithCast()
        {
            var m = _testClassType.InstanceMethod<Func<object, TestClass, int, TestClass>, TestClass>
                ("InstanceGenericMethod");
            Assert.IsNotNull(m);
            var result = m(_testClassInstance, _testClassInstance, 0);
            Assert.AreEqual(2, _testClassInstance.PublicParams.Length);
            Assert.AreEqual(_testClassInstance, _testClassInstance.PublicParams[0]);
            Assert.AreEqual(0, _testClassInstance.PublicParams[1]);
            Assert.AreEqual(_testClassInstance, result);
            Assert.AreEqual(typeof(TestClass), _testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void GenericMethod_NoVoid_3Parameter_OneTypeParameter_ByObjectAndTypes()
        {
            var m = _testClassType.InstanceMethod<Func<TestClass, TestStruct, int, bool, TestStruct>, TestStruct>
                ("InstanceGenericMethod");
            Assert.IsNotNull(m);
            var result = m(_testClassInstance, _testStructInstance, 0, true);
            Assert.AreEqual(3, _testClassInstance.PublicParams.Length);
            Assert.AreEqual(_testStructInstance, _testClassInstance.PublicParams[0]);
            Assert.AreEqual(0, _testClassInstance.PublicParams[1]);
            Assert.AreEqual(true, _testClassInstance.PublicParams[2]);
            Assert.AreEqual(_testStructInstance, result);
            Assert.AreEqual(typeof(TestStruct), _testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void GenericMethod_NoVoid_3Parameter_OneTypeParameter_ByObjectAndTypes_WithCast()
        {
            var m = _testClassType.InstanceMethod<Func<object, TestStruct, int, bool, TestStruct>, TestStruct>
                ("InstanceGenericMethod");
            Assert.IsNotNull(m);
            var result = m(_testClassInstance, _testStructInstance, 0, true);
            Assert.AreEqual(3, _testClassInstance.PublicParams.Length);
            Assert.AreEqual(_testStructInstance, _testClassInstance.PublicParams[0]);
            Assert.AreEqual(0, _testClassInstance.PublicParams[1]);
            Assert.AreEqual(true, _testClassInstance.PublicParams[2]);
            Assert.AreEqual(_testStructInstance, result);
            Assert.AreEqual(typeof(TestStruct), _testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void GenericMethod_NoVoid_NoParameter_2TypeParameters_ByObjectAndTypes()
        {
            var m = _testClassType.InstanceMethod<Func<TestClass, TestClass>, TestClass, TestStruct>
                ("InstanceGenericMethod");
            Assert.IsNotNull(m);
            var result = m(_testClassInstance);
            Assert.AreEqual(null, _testClassInstance.PublicParams);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), _testClassInstance.PublicTypeParams[0]);
            Assert.AreEqual(typeof(TestStruct), _testClassInstance.PublicTypeParams[1]);
        }

        [TestMethod]
        public void GenericMethod_NoVoid_NoParameter_2TypeParameters_ByObjectAndTypes_WithCast()
        {
            var m = _testClassType.InstanceMethod<Func<object, TestClass>, TestClass, TestStruct>
                ("InstanceGenericMethod");
            Assert.IsNotNull(m);
            var result = m(_testClassInstance);
            Assert.AreEqual(null, _testClassInstance.PublicParams);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), _testClassInstance.PublicTypeParams[0]);
            Assert.AreEqual(typeof(TestStruct), _testClassInstance.PublicTypeParams[1]);
        }

        [TestMethod]
        public void GenericMethod_NoVoid_SingleParameter_3TypeParameters_ByObjectAndTypes()
        {
            var m = _testClassType.InstanceMethod<Func<TestClass, int, TestClass>, TestClass, TestStruct, TestClassNoDefaultCtor>
                ("InstanceGenericMethod");
            Assert.IsNotNull(m);
            var result = m(_testClassInstance, 0);
            Assert.AreEqual(1, _testClassInstance.PublicParams.Length);
            Assert.AreEqual(0, _testClassInstance.PublicParams[0]);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), _testClassInstance.PublicTypeParams[0]);
            Assert.AreEqual(typeof(TestStruct), _testClassInstance.PublicTypeParams[1]);
            Assert.AreEqual(typeof(TestClassNoDefaultCtor), _testClassInstance.PublicTypeParams[2]);
        }

        [TestMethod]
        public void GenericMethod_NoVoid_SingleParameter_3TypeParameters_ByObjectAndTypes_WithCast()
        {
            var m = _testClassType.InstanceMethod<Func<object, int, TestClass>, TestClass, TestStruct, TestClassNoDefaultCtor>
                ("InstanceGenericMethod");
            Assert.IsNotNull(m);
            var result = m(_testClassInstance, 0);
            Assert.AreEqual(1, _testClassInstance.PublicParams.Length);
            Assert.AreEqual(0, _testClassInstance.PublicParams[0]);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), _testClassInstance.PublicTypeParams[0]);
            Assert.AreEqual(typeof(TestStruct), _testClassInstance.PublicTypeParams[1]);
            Assert.AreEqual(typeof(TestClassNoDefaultCtor), _testClassInstance.PublicTypeParams[2]);
        }

        [TestMethod]
        public void Public_GenericMethod_Void_NoParameter_OneTypeParameter_ByObjects()
        {
            var m = _testClassType.InstanceGenericMethodVoid("PublicInstanceGenericMethodVoid", null,
                new[] { typeof(string) });
            Assert.IsNotNull(m);
            m(_testClassInstance, new object[0]);
            Assert.AreEqual(null, _testClassInstance.PublicParams);
            Assert.AreEqual(typeof(string), _testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void Internal_GenericMethod_Void_NoParameter_OneTypeParameter_ByObjects()
        {
            var m = _testClassType.InstanceGenericMethodVoid("InternalInstanceGenericMethodVoid", null,
                new[] { typeof(string) });
            Assert.IsNotNull(m);
            m(_testClassInstance, new object[0]);
            Assert.AreEqual(null, _testClassInstance.PublicParams);
            Assert.AreEqual(typeof(string), _testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void Protected_GenericMethod_Void_NoParameter_OneTypeParameter_ByObjects()
        {
            var m = _testClassType.InstanceGenericMethodVoid("ProtectedInstanceGenericMethodVoid", null,
                new[] { typeof(string) });
            Assert.IsNotNull(m);
            m(_testClassInstance, new object[0]);
            Assert.AreEqual(null, _testClassInstance.PublicParams);
            Assert.AreEqual(typeof(string), _testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void Private_GenericMethod_Void_NoParameter_OneTypeParameter_ByObjects()
        {
            var m = _testClassType.InstanceGenericMethodVoid("PrivateInstanceGenericMethodVoid", null,
                new[] { typeof(string) });
            Assert.IsNotNull(m);
            m(_testClassInstance, new object[0]);
            Assert.AreEqual(null, _testClassInstance.PublicParams);
            Assert.AreEqual(typeof(string), _testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void Public_GenericMethod_NoVoid_NoParameter_OneTypeParameter_ByObjects()
        {
            var m = _testClassType.InstanceGenericMethod("PublicInstanceGenericMethod", null,
                new[] { _testClassType });
            Assert.IsNotNull(m);
            var result = m(_testClassInstance, new object[] { _testClassInstance });
            Assert.AreEqual(null, _testClassInstance.PublicParams);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), _testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void Internal_GenericMethod_NoVoid_NoParameter_OneTypeParameter_ByObjects()
        {
            var m = _testClassType.InstanceGenericMethod("InternalInstanceGenericMethod", null,
                new[] { _testClassType });
            Assert.IsNotNull(m);
            var result = m(_testClassInstance, new object[] { _testClassInstance });
            Assert.AreEqual(null, _testClassInstance.PublicParams);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), _testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void Protected_GenericMethod_NoVoid_NoParameter_OneTypeParameter_ByObjects()
        {
            var m = _testClassType.InstanceGenericMethod("ProtectedInstanceGenericMethod", null,
                new[] { _testClassType });
            Assert.IsNotNull(m);
            var result = m(_testClassInstance, new object[] { _testClassInstance });
            Assert.AreEqual(null, _testClassInstance.PublicParams);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), _testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void Private_GenericMethod_NoVoid_NoParameter_OneTypeParameter_ByObjects()
        {
            var m = _testClassType.InstanceGenericMethod("PrivateInstanceGenericMethod", null,
                new[] { _testClassType });
            Assert.IsNotNull(m);
            var result = m(_testClassInstance, new object[] { _testClassInstance });
            Assert.AreEqual(null, _testClassInstance.PublicParams);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), _testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void GenericMethod_NoVoid_SingleParameter_OneTypeParameter_ByObjects()
        {
            var m = _testClassType.InstanceGenericMethod("InstanceGenericMethod", new[] { _testClassType },
                new[] { _testClassType });
            var result = m(_testClassInstance, new object[] { _testClassInstance });
            Assert.AreEqual(1, _testClassInstance.PublicParams.Length);
            Assert.AreEqual(_testClassInstance, _testClassInstance.PublicParams[0]);
            Assert.AreEqual(_testClassInstance, result);
            Assert.AreEqual(typeof(TestClass), _testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void GenericMethod_NoVoid_2Parameter_OneTypeParameter_ByObjects()
        {
            var m = _testClassType.InstanceGenericMethod("InstanceGenericMethod", new[] { _testClassType, typeof(int) },
                new[] { _testClassType });
            Assert.IsNotNull(m);
            var result = m(_testClassInstance, new object[] { _testClassInstance, 0 });
            Assert.AreEqual(2, _testClassInstance.PublicParams.Length);
            Assert.AreEqual(_testClassInstance, _testClassInstance.PublicParams[0]);
            Assert.AreEqual(0, _testClassInstance.PublicParams[1]);
            Assert.AreEqual(_testClassInstance, result);
            Assert.AreEqual(typeof(TestClass), _testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void GenericMethod_NoVoid_3Parameter_OneTypeParameter_ByObjects()
        {
            var m = _testClassType.InstanceGenericMethod("InstanceGenericMethod",
                new[] { _testStructType, typeof(int), typeof(bool) }, new[] { _testStructType });
            Assert.IsNotNull(m);
            var result = m(_testClassInstance, new object[] { _testStructInstance, 0, true });
            Assert.AreEqual(3, _testClassInstance.PublicParams.Length);
            Assert.AreEqual(_testStructInstance, _testClassInstance.PublicParams[0]);
            Assert.AreEqual(0, _testClassInstance.PublicParams[1]);
            Assert.AreEqual(true, _testClassInstance.PublicParams[2]);
            Assert.AreEqual(_testStructInstance, result);
            Assert.AreEqual(typeof(TestStruct), _testClassInstance.PublicTypeParams[0]);
        }

        [TestMethod]
        public void GenericMethod_NoVoid_NoParameter_2TypeParameters_ByObjects()
        {
            var m = _testClassType.InstanceGenericMethod("InstanceGenericMethod", null,
                new[] { _testClassType, _testStructType });
            Assert.IsNotNull(m);
            var result = m(_testClassInstance, new object[0]);
            Assert.AreEqual(null, _testClassInstance.PublicParams);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), _testClassInstance.PublicTypeParams[0]);
            Assert.AreEqual(typeof(TestStruct), _testClassInstance.PublicTypeParams[1]);
        }

        [TestMethod]
        public void GenericMethod_NoVoid_SingleParameter_3TypeParameters_ByObjects()
        {
            var m = _testClassType.InstanceGenericMethod("InstanceGenericMethod", new[] { typeof(int) },
                new[] { _testClassType, _testStructType, typeof(TestClassNoDefaultCtor) });
            Assert.IsNotNull(m);
            var result = m(_testClassInstance, new object[] { 0 });
            Assert.AreEqual(1, _testClassInstance.PublicParams.Length);
            Assert.AreEqual(0, _testClassInstance.PublicParams[0]);
            Assert.IsInstanceOfType(result, typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), _testClassInstance.PublicTypeParams[0]);
            Assert.AreEqual(typeof(TestStruct), _testClassInstance.PublicTypeParams[1]);
            Assert.AreEqual(typeof(TestClassNoDefaultCtor), _testClassInstance.PublicTypeParams[2]);
        }

        [TestMethod]
        public void InstanceGenericMethod_DoNotThrowExc_When_ParametersArray_IsNull()
        {
            var m = _testClassType.InstanceGenericMethod("InternalInstanceGenericMethod", null,
                new[] { _testClassType });
            Assert.IsNotNull(m);
        }

        [TestMethod]
        public void InstanceGenericMethod_DoNotThrowExc_When_TypeParametersArray_IsNull()
        {
            var m = _testClassType.InstanceGenericMethod("PublicMethod", new Type[0], null);
            Assert.IsNotNull(m);
        }

        [TestMethod]
        public void InterfaceMethod_Generic_ByTypes()
        {
            var m = DelegateFactory.InstanceMethod<Func<IService, string, string>, string>("Echo");
            Assert.IsNotNull(m);
            var result = m(_interfaceImpl, TestValue);
            Assert.AreEqual(TestValue, result);
        }

        [TestMethod]
        public void InterfaceMethod_Generic_ByObjectAndTypes()
        {
            var m = _interfaceType.InstanceMethod<Func<IService, string, string>, string>("Echo");
            Assert.IsNotNull(m);
            var result = m(_interfaceImpl, TestValue);
            Assert.AreEqual(TestValue, result);
        }

        [TestMethod]
        public void InterfaceMethod_Generic_ByObjectAndTypes_WithCast()
        {
            var m = _interfaceType.InstanceMethod<Func<object, string, string>, string>("Echo");
            Assert.IsNotNull(m);
            var result = m(_interfaceImpl, TestValue);
            Assert.AreEqual(TestValue, result);
        }

        [TestMethod]
        public void InterfaceMethod_Generic_ByObjects()
        {
            var m = _interfaceType.InstanceGenericMethod("Echo", new [] { typeof(string) }, new[] { typeof(string) });
            Assert.IsNotNull(m);
            var result = m(_interfaceImpl, new object[] { TestValue });
            Assert.AreEqual(TestValue, result);
        }

        [TestMethod]
        public void Incorrect_TDelegate_For_InstanceGenericMethod()
        {
            AssertHelper.ThrowsException<ArgumentException>(() =>
                _testClassType.InstanceGenericMethod<Action>("PublicMethodVoid", null, new[] { typeof(string) }));
        }
    }
}
