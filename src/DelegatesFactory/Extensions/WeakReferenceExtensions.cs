// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WeakReferenceExtensions.cs" company="Natan Podbielski">
//   Copyright (c) 2016 - 2016 Natan Podbielski. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace Delegates.Extensions
{
    internal static class WeakReferenceExtensions
    {
        public static void TryGetTarget(this WeakReference reference, out object target)
        {
            target = reference.Target;
        }
    }
}