namespace Delegates.CustomDelegates
{
    public delegate TProp StructIndex1GetFunc<T, in TI1, out TProp>(ref T i, TI1 i1) where T : struct;
}