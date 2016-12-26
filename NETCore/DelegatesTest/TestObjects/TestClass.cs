// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestClass.cs" company="Natan Podbielski">
//   Copyright (c) 2016 - 2016 Natan Podbielski. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace DelegatesTest.TestObjects
{
    public class GenericClass<T>
    {
        public static T StaticGenericMethod(T param)
        {
            return param;
        }
    }

    public class TestClass : ITestInterface
    {
        public const string StaticPublicParameterlessReturnValue = "StaticPublic";
        public const string StaticInternalParameterlessReturnValue = "StaticInternal";
        public const string StaticProtectedParameterlessReturnValue = "StaticProtected";
        public static readonly string StaticPublicReadOnlyField = "StaticPublicReadOnlyField";

        public static object StaticGenericMethodVoidParameter;
        public static string StaticInternalField = "StaticInternalField";
        public static string StaticPrivateField = "StaticPrivateField";
        public static string StaticProtectedField = "StaticProtectedField";
        public static string StaticPublicField = "StaticPublicField";
        public static string StaticPublicMethodVoidParameter;
        public static int StaticPublicValueField = 0;

        public static bool StaticVoidInternalExecuted;
        public static string StaticVoidInternalParam;
        public static bool StaticVoidPrivateExecuted;
        public static string StaticVoidPrivateParam;
        public static bool StaticVoidProtectedExecuted;
        public static string StaticVoidProtectedParam;
        public static bool StaticVoidPublicExecuted;
        public static string StaticVoidPublicParam;
        public readonly List<int> IndexerBackend = new List<int>(new int[10]);
        public readonly string PublicReadOnlyField = "PublicReadOnlyField";
        public object InstanceGenericMethodVoidParameter;
        public double InternalIndexer;
        public int PrivateIndexer;
        public double ProtectedIndexer;
        public int Public3IndexIndexer;
        public int Public4IndexIndexer;
        public string PublicField;
        public int PublicFieldInt;
        public string PublicMethodVoidParameter;
        internal string InternalField;
        protected string ProtectedField;
        private static string _staticOnlySetPropertyBackend;
        private string _onlySetPropertyBackend;
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private string _privateField;
        public static string[] StaticVoidPublicParams;
        public static string[] StaticPublicParams;
        public static string StaticPrivateParameterlessReturnValue = "StaticPrivate";

        public TestClass()
        {
            PublicProperty = "PublicProperty";
            InternalProperty = "InternalProperty";
            ProtectedProperty = "ProtectedProperty";
            PrivateProperty = "PrivateProperty";
            PublicField = "PublicField";
            InternalField = "InternalField";
            ProtectedField = "ProtectedField";
            _privateField = "_privateField";
        }

        internal TestClass(int p)
            : this()
        {
        }

        protected TestClass(bool p)
            : this()
        {
        }

        private TestClass(string p)
            : this()
        {
        }

        public event EventHandler<PublicEventArgs> PublicEvent;

        internal event EventHandler<InternalEventArgs> InternalEvent
        {
            add { InternalEventBackend += value; }
            remove { InternalEventBackend -= value; }
        }

        protected event EventHandler<ProtectedEventArgs> ProtectedEvent;

        private event EventHandler<InternalEventArgs> InternalEventBackend;

        private event EventHandler<PrivateEventArgs> PrivateEvent;

        public static string StaticOnlyGetProperty { get; } = "StaticOnlyGetProperty";

        public static string StaticOnlySetProperty
        {
            set { _staticOnlySetPropertyBackend = value; }
        }

        public static string StaticPublicProperty { get; set; } = "StaticPublicProperty";

        public static int StaticPublicPropertyValue { get; set; } = 0;

        public string OnlyGetProperty => "OnlyGetProperty";
        public string OnlySetProperty
        {
            set { _onlySetPropertyBackend = value; }
        }

        public string PublicProperty { get; set; }
        public int PublicPropertyInt { get; set; }

        internal static string StaticInternalProperty { get; set; } = "StaticInternalProperty";

        internal string InternalProperty { get; set; }

        protected static string StaticProtectedProperty { get; set; } = "StaticProtectedProperty";

        protected string ProtectedProperty { get; set; }

        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
        private static string StaticPrivateProperty { get; set; } = "StaticPrivateProperty";

        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
        private string PrivateProperty { get; set; }

        [IndexerName("TheItem")]
        public int this[int i]
        {
            get { return IndexerBackend[i]; }
            set { IndexerBackend[i] = value; }
        }

        [IndexerName("TheItem")]
        public int this[int i1, int i2, int i3]
        {
            get { return i1; }
            set { Public3IndexIndexer = value; }
        }

        [IndexerName("TheItem")]
        public int this[int i1, int i2, int i3, int i4]
        {
            get { return i1; }
            set { Public4IndexIndexer = value; }
        }

        [IndexerName("TheItem")]
        internal string this[string s] => s;

        [IndexerName("TheItem")]
        internal double this[double s]
        {
            get { return s; }
            set { InternalIndexer = value; }
        }

        [IndexerName("TheItem")]
        protected string this[string s, string s2]
        {
            set { }
        }

        [IndexerName("TheItem")]
        protected byte this[byte i]
        {
            get { return i; }
            set { ProtectedIndexer = value; }
        }

        [IndexerName("TheItem")]
        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once UnusedParameter.Local
        private int this[int i1, int i2]
        {
            get { return i1; }
            set { PrivateIndexer = value; }
        }
        [IndexerName("TheItem")]
        // ReSharper disable once UnusedMember.Local
        private long this[long s] => s;
        public static string GetStaticPrivateField()
        {
            return StaticPrivateField;
        }

        public static string GetStaticPrivateProperty()
        {
            return StaticPrivateProperty;
        }

        public static string GetStaticProtectedField()
        {
            return StaticProtectedField;
        }

        public static string GetStaticProtectedProperty()
        {
            return StaticProtectedProperty;
        }

        public static void PublicStaticGenericMethodVoid<T>()
        {
            StaticPublicTypeParams = new[] { typeof(T) };
        }

        internal static void InternalStaticGenericMethodVoid<T>()
        {
            StaticPublicTypeParams = new[] { typeof(T) };
        }


        internal static void ProtectedStaticGenericMethodVoid<T>()
        {
            StaticPublicTypeParams = new[] { typeof(T) };
        }


        internal static void PrivateStaticGenericMethodVoid<T>()
        {
            StaticPublicTypeParams = new[] { typeof(T) };
        }

        public static T PublicStaticGenericMethod<T>() where T : new()
        {
            StaticPublicTypeParams = new[] { typeof(T) };
            return new T();
        }

        internal static T InternalStaticGenericMethod<T>() where T : new()
        {
            StaticPublicTypeParams = new[] { typeof(T) };
            return new T();
        }

        protected static T ProtectedStaticGenericMethod<T>() where T : new()
        {
            StaticPublicTypeParams = new[] { typeof(T) };
            return new T();
        }

        private static T PrivateStaticGenericMethod<T>() where T : new()
        {
            StaticPublicTypeParams = new[] { typeof(T) };
            return new T();
        }

        public static T StaticGenericMethod<T>(T param)
        {
            StaticPublicTypeParams = new[] { typeof(T) };
            return param;
        }

        public static T StaticGenericMethod<T>(T param, int i) where T : ITestInterface
        {
            StaticPublicTypeParams = new[] { typeof(T) };
            return param;
        }

        public static T StaticGenericMethodWithType<T>(T param) where T : Base
        {
            StaticPublicTypeParams = new[] { typeof(T) };
            return param;
        }

        public static T StaticGenericMethodWithClass<T>(T param) where T : class
        {
            StaticPublicTypeParams = new[] { typeof(T) };
            return param;
        }

        public static T StaticGenericMethodWithStruc<T>(T param) where T : struct
        {
            StaticPublicTypeParams = new[] { typeof(T) };
            return param;
        }

        public static T StaticGenericMethodFromOtherParameter<T, T2>(T param) where T2 : T
        {
            StaticPublicTypeParams = new[] { typeof(T), typeof(T2) };
            return param;
        }

        public static T StaticGenericMethod<T>(T param, int i, bool p) where T : struct
        {
            StaticPublicTypeParams = new[] { typeof(T) };
            return param;
        }

        public static T1 StaticGenericMethod<T1, T2>() where T1 : new()
        {
            StaticPublicTypeParams = new[] { typeof(T1), typeof(T2) };
            return new T1();
        }

        public static T1 StaticGenericMethod<T1, T2, T3>(int i) where T1 : new()
        {
            StaticPublicTypeParams = new[] { typeof(T1), typeof(T2), typeof(T3) };
            return new T1();
        }

        public static void StaticGenericMethodVoid<T>(T s) where T : class
        {
            StaticGenericMethodVoidParameter = s;
        }

        public static string StaticPublicMethod(string s)
        {
            return s;
        }

        public static string StaticPublicMethod(int i)
        {
            return i.ToString();
        }

        public static int StaticPublicMethodValue(int i)
        {
            return i;
        }

        public static void StaticPublicMethodVoid(string s)
        {
            StaticPublicMethodVoidParameter = s;
        }

        public static void StaticVoidPublic()
        {
            StaticVoidPublicExecuted = true;
            return;
        }

        public static void StaticVoidPublic(string s)
        {
            StaticVoidPublicParam = s;
            return;
        }

        public static string StaticPublic(string s)
        {
            StaticPublicParams = new[] { s };
            return s;
        }

        public static string StaticPublic(string s, string s1)
        {
            StaticPublicParams = new[] { s, s1 };
            return s;
        }

        public static string StaticPublic(string s, string s1, string s2)
        {
            StaticPublicParams = new[] { s, s1, s2 };
            return s;
        }

        public static string StaticPublic(string s, string s1, string s2, string s3)
        {
            StaticPublicParams = new[] { s, s1, s2, s3 };
            return s;
        }

        public static string StaticPublic(string s, string s1, string s2, string s3, string s4)
        {
            StaticPublicParams = new[] { s, s1, s2, s3, s4 };
            return s;
        }

        public static string StaticPublic(string s, string s1, string s2, string s3, string s4, string s5)
        {
            StaticPublicParams = new[] { s, s1, s2, s3, s4, s5 };
            return s;
        }

        public static string StaticPublic(string s, string s1, string s2, string s3, string s4, string s5, string s6)
        {
            StaticPublicParams = new[] { s, s1, s2, s3, s4, s5, s6 };
            return s;
        }

        public static string StaticPublic(string s, string s1, string s2, string s3, string s4, string s5, string s6,
            string s7)
        {
            StaticPublicParams = new[] { s, s1, s2, s3, s4, s5, s6, s7 };
            return s;
        }

        public static string StaticPublic(string s, string s1, string s2, string s3, string s4, string s5, string s6,
            string s7, string s8)
        {
            StaticPublicParams = new[] { s, s1, s2, s3, s4, s5, s6, s7, s8 };
            return s;
        }

        public static string StaticPublic(string s, string s1, string s2, string s3, string s4, string s5, string s6,
            string s7, string s8, string s9)
        {
            StaticPublicParams = new[] { s, s1, s2, s3, s4, s5, s6, s7, s8, s9 };
            return s;
        }

        public static string StaticPublic(string s, string s1, string s2, string s3, string s4, string s5, string s6,
            string s7, string s8, string s9, string s10)
        {
            StaticPublicParams = new[] { s, s1, s2, s3, s4, s5, s6, s7, s8, s9, s10 };
            return s;
        }

        public static string StaticPublic(string s, string s1, string s2, string s3, string s4, string s5, string s6,
            string s7, string s8, string s9, string s10, string s11)
        {
            StaticPublicParams = new[] { s, s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11 };
            return s;
        }

        public static string StaticPublic(string s, string s1, string s2, string s3, string s4, string s5, string s6,
            string s7, string s8, string s9, string s10, string s11, string s12)
        {
            StaticPublicParams = new[] { s, s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12 };
            return s;
        }

        public static string StaticPublic(string s, string s1, string s2, string s3, string s4, string s5, string s6,
            string s7, string s8, string s9, string s10, string s11, string s12, string s13)
        {
            StaticPublicParams = new[] { s, s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12, s13 };
            return s;
        }

        public static string StaticPublic(string s, string s1, string s2, string s3, string s4, string s5, string s6,
            string s7, string s8, string s9, string s10, string s11, string s12, string s13, string s14)
        {
            StaticPublicParams = new[] { s, s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12, s13, s14 };
            return s;
        }

        public static string StaticPublic(string s, string s1, string s2, string s3, string s4, string s5, string s6,
            string s7, string s8, string s9, string s10, string s11, string s12, string s13, string s14, string s15)
        {
            StaticPublicParams = new[] { s, s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12, s13, s14, s15 };
            return s;
        }

        public static string StaticPublic(string s, string s1, string s2, string s3, string s4, string s5, string s6,
            string s7, string s8, string s9, string s10, string s11, string s12, string s13, string s14, string s15,
            string s16)
        {
            StaticPublicParams = new[] { s, s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12, s13, s14, s15, s16 };
            return s;
        }

        internal static string StaticInternal(string s)
        {
            return s;
        }

        protected static string StaticProtected(string s)
        {
            return s;
        }

        private static string StaticPrivate(string s)
        {
            return s;
        }

        public static string StaticPublic()
        {
            return StaticPublicParameterlessReturnValue;
        }

        public static Type[] StaticPublicTypeParams;
        public static string StaticPublic<T>()
        {
            StaticPublicTypeParams = new[] { typeof(T) };
            return StaticPublicParameterlessReturnValue;
        }

        internal static string StaticInternal()
        {
            return StaticInternalParameterlessReturnValue;
        }

        protected static string StaticProtected()
        {
            return StaticProtectedParameterlessReturnValue;
        }

        private static string StaticPrivate()
        {
            return StaticPrivateParameterlessReturnValue;
        }

        public static void StaticVoidPublic(string s, string s1)
        {
            StaticVoidPublicParam = s;
            StaticVoidPublicParams = new[] { s, s1 };
            return;
        }

        public static void StaticVoidPublic(string s, string s1, string s2)
        {
            StaticVoidPublicParam = s;
            StaticVoidPublicParams = new[] { s, s1, s2 };
            return;
        }

        public static void StaticVoidPublic(string s, string s1, string s2, string s3)
        {
            StaticVoidPublicParam = s;
            StaticVoidPublicParams = new[] { s, s1, s2, s3 };
            return;
        }

        public static void StaticVoidPublic(string s, string s1, string s2, string s3, string s4)
        {
            StaticVoidPublicParam = s;
            StaticVoidPublicParams = new[] { s, s1, s2, s3, s4 };
            return;
        }

        public static void StaticVoidPublic(string s, string s1, string s2, string s3, string s4, string s5)
        {
            StaticVoidPublicParam = s;
            StaticVoidPublicParams = new[] { s, s1, s2, s3, s4, s5 };
            return;
        }

        public static void StaticVoidPublic(string s, string s1, string s2, string s3, string s4, string s5, string s6)
        {
            StaticVoidPublicParam = s;
            StaticVoidPublicParams = new[] { s, s1, s2, s3, s4, s5, s6 };
            return;
        }

        public static void StaticVoidPublic(string s, string s1, string s2, string s3, string s4, string s5, string s6,
            string s7)
        {
            StaticVoidPublicParam = s;
            StaticVoidPublicParams = new[] { s, s1, s2, s3, s4, s5, s6, s7 };
            return;
        }

        public static void StaticVoidPublic(string s, string s1, string s2, string s3, string s4, string s5, string s6,
            string s7, string s8)
        {
            StaticVoidPublicParam = s;
            StaticVoidPublicParams = new[] { s, s1, s2, s3, s4, s5, s6, s7, s8 };
            return;
        }

        public static void StaticVoidPublic(string s, string s1, string s2, string s3, string s4, string s5, string s6,
            string s7, string s8, string s9)
        {
            StaticVoidPublicParam = s;
            StaticVoidPublicParams = new[] { s, s1, s2, s3, s4, s5, s6, s7, s8, s9 };
            return;
        }

        public static void StaticVoidPublic(string s, string s1, string s2, string s3, string s4, string s5, string s6,
            string s7, string s8, string s9, string s10)
        {
            StaticVoidPublicParam = s;
            StaticVoidPublicParams = new[] { s, s1, s2, s3, s4, s5, s6, s7, s8, s9, s10 };
            return;
        }


        public static void StaticVoidPublic(string s, string s1, string s2, string s3, string s4, string s5, string s6,
            string s7, string s8, string s9, string s10, string s11)
        {
            StaticVoidPublicParam = s;
            StaticVoidPublicParams = new[] { s, s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11 };
            return;
        }

        public static void StaticVoidPublic(string s, string s1, string s2, string s3, string s4, string s5, string s6,
            string s7, string s8, string s9, string s10, string s11, string s12)
        {
            StaticVoidPublicParam = s;
            StaticVoidPublicParams = new[] { s, s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12 };
            return;
        }

        public static void StaticVoidPublic(string s, string s1, string s2, string s3, string s4, string s5, string s6,
            string s7, string s8, string s9, string s10, string s11, string s12, string s13)
        {
            StaticVoidPublicParam = s;
            StaticVoidPublicParams = new[] { s, s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12, s13 };
            return;
        }

        public static void StaticVoidPublic(string s, string s1, string s2, string s3, string s4, string s5, string s6,
            string s7, string s8, string s9, string s10, string s11, string s12, string s13, string s14)
        {
            StaticVoidPublicParam = s;
            StaticVoidPublicParams = new[] { s, s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12, s13, s14 };
            return;
        }

        public static void StaticVoidPublic(string s, string s1, string s2, string s3, string s4, string s5, string s6,
            string s7, string s8, string s9, string s10, string s11, string s12, string s13, string s14, string s15)
        {
            StaticVoidPublicParam = s;
            StaticVoidPublicParams = new[] { s, s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12, s13, s14, s15 };
            return;
        }

        public static void StaticVoidPublic(string s, string s1, string s2, string s3, string s4, string s5, string s6,
            string s7, string s8, string s9, string s10, string s11, string s12, string s13, string s14, string s15,
            string s16)
        {
            StaticVoidPublicParam = s;
            StaticVoidPublicParams = new[] { s, s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12, s13, s14, s16 };
            return;
        }

        public T GenericMethod<T>(T s)
        {
            return s;
        }
        public void GenericMethodVoid<T>(T s)
        {
            InstanceGenericMethodVoidParameter = s;
        }

        public string GetPrivateField()
        {
            return _privateField;
        }

        public string GetPrivateProperty()
        {
            return PrivateProperty;
        }

        public string GetProtectedField()
        {
            return ProtectedField;
        }

        public string GetProtectedProperty()
        {
            return ProtectedProperty;
        }

        public void InvokeInternalEvent()
        {
            InternalEventBackend?.Invoke(this, new InternalEventArgs());
        }

        public void InvokePrivateEvent()
        {
            PrivateEvent?.Invoke(this, new PrivateEventArgs());
        }

        public void InvokeProtectedEvent()
        {
            ProtectedEvent?.Invoke(this, new ProtectedEventArgs());
        }

        public void InvokePublicEvent()
        {
            PublicEvent?.Invoke(this, new PublicEventArgs());
        }

        public string PublicMethod(string s)
        {
            return s;
        }

        public void PublicMethodVoid(string s)
        {
            PublicMethodVoidParameter = s;
        }

        internal static string StaticInternalMethod(string s)
        {
            return s;
        }

        internal static void StaticVoidInternal()
        {
            StaticVoidInternalExecuted = true;
            return;
        }
        internal static void StaticVoidInternal(string s)
        {
            StaticVoidInternalParam = s;
            return;
        }

        internal string InternalMethod(string s)
        {
            return s;
        }

        protected static string StaticProtectedMethod(string s)
        {
            return s;
        }

        protected static void StaticVoidProtected()
        {
            StaticVoidProtectedExecuted = true;
            return;
        }
        protected static void StaticVoidProtected(string s)
        {
            StaticVoidProtectedParam = s;
            return;
        }

        protected string ProtectedMethod(string s)
        {
            return s;
        }

        private static string StaticPrivateMethod(string s)
        {
            return s;
        }

        private static void StaticVoidPrivate()
        {
            StaticVoidPrivateExecuted = true;
            return;
        }
        private static void StaticVoidPrivate(string s)
        {
            StaticVoidPrivateParam = s;
            return;
        }
        private string PrivateMethod(string s)
        {
            return s;
        }
        public class PublicEventArgs : EventArgs
        {
        }

        internal class InternalEventArgs : EventArgs
        {
        }

        protected class ProtectedEventArgs : EventArgs
        {
        }

        private class PrivateEventArgs : EventArgs
        {
        }
    }
}