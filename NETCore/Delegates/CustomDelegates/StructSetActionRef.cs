namespace Delegates
{
    public delegate void StructSetActionRef<T, in TProp>(ref T i, TProp value);
}