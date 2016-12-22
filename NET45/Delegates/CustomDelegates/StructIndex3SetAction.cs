namespace Delegates.CustomDelegates
{
    public delegate void StructIndex3SetAction<T, in TI1, in TI2, in TI3, in TProp>(ref T i, TI1 i1, TI2 i2, TI3 i3, TProp value);
}