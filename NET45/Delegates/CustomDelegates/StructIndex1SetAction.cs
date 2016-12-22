namespace Delegates.CustomDelegates
{
    public delegate void StructIndex1SetAction<T, in TI1, in TProp>(ref T i, TI1 i1, TProp value);
}