namespace Delegates.CustomDelegates
{
    public delegate TProp StructGetFunc<T, out TProp>(ref T i) where T : struct;
}