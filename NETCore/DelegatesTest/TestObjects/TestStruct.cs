// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestStruct.cs" company="Natan Podbielski">
//   Copyright (c) 2016 - 2016 Natan Podbielski. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
#if NET45
using System.Diagnostics.Contracts;
#endif

namespace DelegatesTest.TestObjects
{
    public struct TestStruct
    {
        public string PublicField;

        public int PublicFieldInt;

        internal string InternalField;

        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private string _privateField;

        public static string StaticPublicField = "StaticPublicField ";

        public static readonly string StaticPublicReadOnlyField = "StaticPublicReadOnlyField";

        public static int StaticPublicFieldInt = 20;

        internal static string StaticInternalField = "StaticInternalField";

        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        // ReSharper disable once ConvertToConstant.Local
        private static string _staticPrivateField = "_staticPrivateField";

        public static string GetStaticPrivateField()
        {
            return _staticPrivateField;
        }

        public readonly List<int> IndexerBackend;

        public int this[int i]
        {
            get { return IndexerBackend[i]; }
            set { IndexerBackend[i] = value; }
        }

        public int this[int i1, int i2, int i3]
        {
            get { return i1; }
            set { Public3IndexIndexer = value; }
        }

        public int this[int i1, int i2, int i3, int i4]
        {
            get { return i1; }
            set { Public4IndexIndexer = value; }
        }


        public double InternalIndexer;

        internal double this[double s]
        {
            get { return s; }
            set { InternalIndexer = value; }
        }

        public int Public3IndexIndexer;
        public int Public4IndexIndexer;

        internal string this[string s] => s;

        internal string this[string s, string s2]
        {
            set { }
        }

        public byte this[byte i]
        {
            get { return i; }
            set { }
        }

        // ReSharper disable once UnusedMember.Local
        private long this[long s] => s;

        public int PrivateIndexer;
        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once UnusedParameter.Local
        private int this[int i1, int i2]
        {
            get { return i1; }
            set { PrivateIndexer = value; }
        }

        public TestStruct(int i)
        {
            InternalIndexer = 0;
            PrivateIndexer = 0;
            IndexerBackend = new List<int>(new int[10]);
            PublicProperty = "PublicPropertyStruct";
            InternalProperty = "InternalPropertyStruct";
            PrivateProperty = "PrivatePropertyStruct";
            PublicField = "PublicFieldStruct";
            InternalField = "InternalFieldStruct";
            _privateField = "_privateFieldStruct";
            PublicFieldInt = 10;
            PublicPropertyInt = 10;
            Public3IndexIndexer = 0;
            Public4IndexIndexer = 0;
        }

        public static string StaticPublicProperty { get; set; }

        internal static string StaticInternalProperty { get; set; }

        public string PublicProperty { get; set; }

        public int PublicPropertyInt { get; set; }

        internal string InternalProperty { get; set; }

        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
        private string PrivateProperty { get; set; }

        private static string StaticPrivateProperty { get; set; }

        public static int StaticPublicPropertyValue { get; set; }

#if NET45
        [Pure]
#endif

        public string GetPrivateProperty()
        {
            return PrivateProperty;
        }

        public static string GetStaticPrivateProperty()
        {
            return StaticPrivateProperty;
        }

#if NET45
        [Pure]
#endif

        public string GetPrivateField()
        {
            return _privateField;
        }
    }
}