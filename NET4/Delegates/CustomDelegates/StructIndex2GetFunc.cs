namespace Delegates.CustomDelegates
{
    public delegate TProp StructIndex2GetFunc<T, in TI1, in TI2, out TProp>(ref T i, TI1 i1, TI2 i2) where T : struct;
}