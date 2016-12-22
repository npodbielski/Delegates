namespace Delegates.CustomDelegates
{
    public delegate void StructIndexesSetAction<T,in TProp>(ref T i, object[] indexes, TProp value);
}