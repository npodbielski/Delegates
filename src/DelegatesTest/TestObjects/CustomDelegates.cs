namespace DelegatesTest.TestObjects
{
    public delegate void CustomAction<in T>(T instance);
    public delegate string CustomFunc<in T>(T instance);
    public delegate void CustomActionSingleParam<in T>(T instance, string s);
    public delegate TestClass CustomCtr();
    public delegate TestClass CustomCtrSingleParam(int i);
}