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
    public class TestClass : ITestInterface
    {
        public static readonly string StaticPublicReadOnlyField = "StaticPublicReadOnlyField";

        public static object StaticGenericMethodVoidParameter;
        public static string StaticInternalField = "StaticInternalField";
        public static string StaticPrivateField = "StaticPrivateField";
        public static string StaticProtectedField = "StaticProtectedField";
        public static string StaticPublicField = "StaticPublicField";
        public static string StaticPublicMethodVoidParameter;
        public static int StaticPublicValueField = 0;

        private static string _staticOnlySetPropertyBackend;
        public readonly List<int> IndexerBackend = new List<int>(new int[10]);
        public readonly string PublicReadOnlyField = "PublicReadOnlyField";
        private string _onlySetPropertyBackend;
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private string _privateField;
        public object InstanceGenericMethodVoidParameter;
        internal string InternalField;
        protected string ProtectedField;
        public string PublicField;
        public int PublicFieldInt;
        public string PublicMethodVoidParameter;

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

        public static string StaticOnlyGetProperty { get; } = "StaticOnlyGetProperty";

        public static string StaticOnlySetProperty
        {
            set { _staticOnlySetPropertyBackend = value; }
        }

        public static string StaticPublicProperty { get; set; } = "StaticPublicProperty";

        public static int StaticPublicPropertyValue { get; set; } = 0;

        public string PublicProperty { get; set; }

        public string OnlySetProperty
        {
            set { _onlySetPropertyBackend = value; }
        }

        public string OnlyGetProperty => "OnlyGetProperty";

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


        public int PrivateIndexer;

        [IndexerName("TheItem")]
        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once UnusedParameter.Local
        private int this[int i1, int i2]
        {
            get { return i1; }
            set { PrivateIndexer = value; }
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

        public double InternalIndexer;
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

        public double ProtectedIndexer;
        public int Public3IndexIndexer;
        public int Public4IndexIndexer;

        [IndexerName("TheItem")]
        protected byte this[byte i]
        {
            get { return i; }
            set { ProtectedIndexer = value; }
        }

        [IndexerName("TheItem")]
        // ReSharper disable once UnusedMember.Local
        private long this[long s] => s;

        public event EventHandler<PublicEventArgs> PublicEvent;

        internal event EventHandler<InternalEventArgs> InternalEvent
        {
            add { InternalEventBackend += value; }
            remove { InternalEventBackend -= value; }
        }

        protected event EventHandler<ProtectedEventArgs> ProtectedEvent;

        private event EventHandler<InternalEventArgs> InternalEventBackend;

        private event EventHandler<PrivateEventArgs> PrivateEvent;

        public static T StaticGenericMethod<T>() where T : new()
        {
            return new T();
        }

        public static T StaticGenericMethod<T>(T param)
        {
            return param;
        }

        public static T StaticGenericMethod<T>(T param, int i) where T : ITestInterface
        {
            return param;
        }

        public static T StaticGenericMethod<T>(T param, int i, bool p) where T : struct
        {
            return param;
        }

        public static T1 StaticGenericMethod<T1, T2>() where T1 : new()
        {
            return new T1();
        }

        public static T1 StaticGenericMethod<T1, T2, T3>(int i) where T1 : new()
        {
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

        public T GenericMethod<T>(T s)
        {
            return s;
        }

        public void GenericMethodVoid<T>(T s)
        {
            InstanceGenericMethodVoidParameter = s;
        }

        public string GetPrivateProperty()
        {
            return PrivateProperty;
        }

        public string GetProtectedProperty()
        {
            return ProtectedProperty;
        }

        public static string GetStaticPrivateProperty()
        {
            return StaticPrivateProperty;
        }

        public static string GetStaticProtectedProperty()
        {
            return StaticProtectedProperty;
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

        internal string InternalMethod(string s)
        {
            return s;
        }

        protected static string StaticProtectedMethod(string s)
        {
            return s;
        }

        protected string ProtectedMethod(string s)
        {
            return s;
        }

        private static string StaticPrivateMethod(string s)
        {
            return s;
        }

        private string PrivateMethod(string s)
        {
            return s;
        }

        public string GetPrivateField()
        {
            return _privateField;
        }

        public string GetProtectedField()
        {
            return ProtectedField;
        }

        public static string GetStaticProtectedField()
        {
            return StaticProtectedField;
        }

        public static string GetStaticPrivateField()
        {
            return StaticPrivateField;
        }

        public class PublicEventArgs : EventArgs
        {
        }

        internal class InternalEventArgs : EventArgs
        {
        }

        protected class ProtectedEventArgs
        {
        }

        private class PrivateEventArgs : EventArgs
        {
        }
    }
}