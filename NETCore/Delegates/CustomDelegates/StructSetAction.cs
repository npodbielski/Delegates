namespace Delegates.CustomDelegates
{
    public delegate T StructSetAction<T, in TProp>(T i, TProp value);
}