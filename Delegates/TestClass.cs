using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Delegates
{
    public interface ITestInterface
    {
    }

    public struct TestStruct
    {
        public TestStruct(int i)
        {
        }
    }

    public class TestClass : ITestInterface
    {
        public static readonly string StaticPublicReadOnlyField = "StaticPublicReadOnlyField";

        public static object StaticGenericMethodVoidParameter;
        public object InstanceGenericMethodVoidParameter;
        public static string StaticInternalField = "StaticInternalField";

        public static string StaticPrivateField = "StaticPrivateField";

        public static string StaticProtectedField = "StaticProtectedField";

        public static string StaticPublicField = "StaticPublicField";

        public static string StaticPublicMethodVoidParameter;
        public static int StaticPublicValueField = 0;

        public readonly string PublicReadOnlyField = "PublicReadOnlyField";
        public string PublicField;
        public string PublicMethodVoidParameter;
        internal string InternalField;
        protected string ProtectedField;
        private readonly List<int> _indexerBackend = new List<int>(new int[10]);
        private string _privateField;

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

        private event EventHandler<InternalEventArgs> InternalEventBackend;

        internal event EventHandler<InternalEventArgs> InternalEvent
        {
            add { InternalEventBackend += value; }
            remove { InternalEventBackend -= value; }
        }

        protected event EventHandler<ProtectedEventArgs> ProtectedEvent;

        private event EventHandler<PrivateEventArgs> PrivateEvent;

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

        public static string StaticOnlyGetProperty { get; private set; } = "StaticOnlyGetOrSetPropertyValue";

        public static string StaticOnlySetProperty
        {
            set { StaticOnlyGetProperty = value; }
        }

        public static string StaticPublicProperty { get; set; } = "StaticPublicProperty";

        public string PublicProperty { get; set; }

        public int PublicPropertyInt { get; set; }
        internal static string StaticInternalProperty { get; set; } = "StaticInternalProperty";

        internal string InternalProperty { get; set; }

        protected static string StaticProtectedProperty { get; set; } = "StaticProtectedProperty";

        protected string ProtectedProperty { get; set; }

        private static string StaticPrivateProperty { get; set; } = "StaticPrivateProperty";

        private string PrivateProperty { get; set; }

        [IndexerName("TheItem")]
        public int this[int i]
        {
            get { return _indexerBackend[i]; }
            set { _indexerBackend[i] = value; }
        }

        [IndexerName("TheItem")]
        public int this[int i1, int i2, int i3]
        {
            get { return i1; }
            set { }
        }

        [IndexerName("TheItem")]
        internal string this[string s] => s;

        [IndexerName("TheItem")]
        private long this[long s] => s;

        [IndexerName("TheItem")]
        private int this[int i1, int i2]
        {
            get { return i1; }
            set { }
        }

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

    //public class TestClass
    //{
    //    public static string StaticPublicProperty { get; set; } = "StaticPublicProperty";

    //    internal static string StaticInternalProperty { get; set; } = "StaticInternalProperty";

    //    protected static string StaticProtectedProperty { get; set; } = "StaticProtectedProperty";

    //    private static string StaticPrivateProperty { get; set; } = "StaticPrivateProperty";
    //}

    //public class TestClass
    //{
    //    public TestClass()
    //    {
    //        PublicProperty = "PublicProperty";
    //        InternalProperty = "InternalProperty";
    //        ProtectedProperty = "ProtectedProperty";
    //        PrivateProperty = "PrivateProperty";
    //    }

    //    public string PublicProperty { get; set; }

    //    internal string InternalProperty { get; set; }

    //    protected string ProtectedProperty { get; set; }

    //    private string PrivateProperty { get; set; }

    //}
}