// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CAssert.cs" company="Natan Podbielski">
//   Copyright (c) 2016 - 2016 Natan Podbielski. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
#if NETCORE
using Xunit;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

namespace DelegatesTest
{
    public static class CAssert
    {
        /// <summary>Verifies that the specified object is not null. The assertion fails if it is null.</summary>
        /// <param name="value">The object to verify is not null.</param>
#if NETCORE
        /// <exception cref="T:Xunit.Sdk.NotNullException">Thrown when the object is not null</exception>
#else
        /// <exception cref="T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException">
        /// <paramref name="value" /> is null.</exception>
#endif
        public static void IsNotNull(object value)
        {
#if NETCORE
            Assert.NotNull(value);
#else
            Assert.IsNotNull(value);
#endif
        }

        /// <summary>Verifies that the specified object is null. The assertion fails if it is not null.</summary>
        /// <param name="value">The object to verify is null.</param>
#if NETCORE
        /// <exception cref="T:Xunit.Sdk.NullException">Thrown when the object reference is not null</exception>
#else
        /// <exception cref="T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException">
        /// <paramref name="value" /> is not null.</exception>
#endif
        public static void IsNull(object value)
        {
#if NETCORE
            Assert.Null(value);
#else
            Assert.IsNull(value);
#endif
        }

        /// <summary>
        ///     Verifies that two specified generic type data are equal by using the equality operator. The assertion fails if
        ///     they are not equal.
        /// </summary>
        /// <param name="expected">The first generic type data to compare. This is the generic type data the unit test expects.</param>
        /// <param name="actual">The second generic type data to compare. This is the generic type data the unit test produced.</param>
        /// <typeparam name="T">The type of the objects to be compared</typeparam>
#if NETCORE
        /// <exception cref="T:Xunit.Sdk.EqualException">Thrown when the objects are not equal</exception>
#else
        /// <exception cref="T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException">
        /// <paramref name="expected" /> is not equal to <paramref name="actual" />.</exception>
#endif
        public static void AreEqual<T>(T expected, T actual)
        {
#if NETCORE
            Assert.Equal(expected, actual);
#else
            Assert.AreEqual(expected, actual);
#endif
        }

        /// <summary>
        ///     Verifies that the specified object is an instance of the specified type. The assertion fails if the type is
        ///     not found in the inheritance hierarchy of the object.
        /// </summary>
        /// <param name="value">The object to verify is of <paramref name="expectedType" />.</param>
        /// <param name="expectedType">The type expected to be found in the inheritance hierarchy of <paramref name="value" />.</param>
#if NETCORE
        /// <exception cref="T:Xunit.Sdk.IsTypeException">Thrown when the object is not the given type</exception>
#else
        /// <exception cref="T:Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException">
        /// <paramref name="value" /> is null or <paramref name="expectedType" /> is not found in the inheritance hierarchy of <paramref name="value" />.</exception>
#endif
        public static void IsInstanceOfType(object value, Type expectedType)
        {
#if NETCORE
            Assert.IsType(expectedType, value);
#else
            Assert.IsInstanceOfType(value, expectedType);
#endif
        }
    }
}