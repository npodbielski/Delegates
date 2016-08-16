using System;
using System.Collections.Generic;

namespace Delegates
{
    public class TestClass
    {
        public static readonly string StaticPublicReadOnlyField = "StaticPublicReadOnlyField";

        public static string StaticInternalField = "StaticInternalField";

        public static string StaticPrivateField = "StaticPrivateField";

        public static string StaticProtectedField = "StaticProtectedField";

        public static string StaticPublicField = "StaticPublicField";

        public static int StaticPublicValueField = 0;

        public readonly string PublicReadOnlyField = "PublicReadOnlyField";

        public string PublicField;
        internal string InternalField;
        protected string ProtectedField;
        private static string _staticOnlyGetOrSetPropertyValue = "StaticOnlyGetOrSetPropertyValue";

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

        internal event EventHandler<InternalEventArgs> InternalEvent;

        protected event EventHandler<ProtectedEventArgs> ProtectedEvent;

        private event EventHandler<PrivateEventArgs> PrivateEvent;

        public static string StaticOnlySetProperty
        {
            set { _staticOnlyGetOrSetPropertyValue = value; }
        }

        public static string StaticPublicProperty { get; set; } = "StaticPublicProperty";

        public string PublicProperty { get; set; }

        public int PublicPropertyInt { get; set; }

        public static string StaticOnlyGetProperty => _staticOnlyGetOrSetPropertyValue;

        internal static string StaticInternalProperty { get; set; } = "StaticInternalProperty";

        internal string InternalProperty { get; set; }

        protected static string StaticProtectedProperty { get; set; } = "StaticProtectedProperty";

        protected string ProtectedProperty { get; set; }

        private static string StaticPrivateProperty { get; set; } = "StaticPrivateProperty";

        private string PrivateProperty { get; set; }

        [System.Runtime.CompilerServices.IndexerName("TheItem")]
        public int this[int i]
        {
            get
            {
                return _indexerBackend[i];
            }
            set { _indexerBackend[i] = value; }
        }

        [System.Runtime.CompilerServices.IndexerName("TheItem")]
        public int this[int i1, int i2, int i3]
        {
            get { return i1; }
            set
            {
            }
        }

        [System.Runtime.CompilerServices.IndexerName("TheItem")]
        internal string this[string s] => s;

        [System.Runtime.CompilerServices.IndexerName("TheItem")]
        private long this[long s] => s;

        [System.Runtime.CompilerServices.IndexerName("TheItem")]
        private int this[int i1, int i2]
        {
            get { return i1; }
            set { }
        }

        public static string StaticPublicMethod(string s)
        {
            return s;
        }

        public void InvokeInternalEvent()
        {
            InternalEvent?.Invoke(this, new InternalEventArgs());
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

        protected class ProtectedEventArgs : EventArgs
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