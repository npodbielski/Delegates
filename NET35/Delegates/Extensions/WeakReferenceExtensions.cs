using System;

namespace Delegates.Extensions
{
    public static class WeakReferenceExtensions
    {
        public static void TryGetTarget(this WeakReference reference, out object target)
        {
            target = reference.Target;
        }
    }
}
