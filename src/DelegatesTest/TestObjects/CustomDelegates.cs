namespace DelegatesTest.TestObjects
{
    public delegate void CustomAction<in T>(T instance);
    public delegate string CustomFunc<in T>(T instance);
    public delegate void CustomActionSingleParam<in T>(T instance, string s);
}