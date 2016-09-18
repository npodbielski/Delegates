// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestStruct.cs" company="Natan Podbielski">
//   Copyright (c) 2016 - 2016 Natan Podbielski. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Diagnostics.Contracts;

namespace DelegatesTest
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
        private static string _staticPrivateField = "_staticPrivateField";

        public static string GetStaticPrivateField()
        {
            return _staticPrivateField;
        }

        public TestStruct(int i)
        {
            PublicProperty = "PublicPropertyStruct";
            InternalProperty = "InternalPropertyStruct";
            PrivateProperty = "PrivatePropertyStruct";
            PublicField = "PublicFieldStruct";
            InternalField = "InternalFieldStruct";
            _privateField = "_privateFieldStruct";
            PublicFieldInt = 10;
            PublicPropertyInt = 10;
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

        [Pure]
        public string GetPrivateProperty()
        {
            return PrivateProperty;
        }

        public static string GetStaticPrivateProperty()
        {
            return StaticPrivateProperty;
        }

        [Pure]
        public string GetPrivateField()
        {
            return _privateField;
        }
    }
}