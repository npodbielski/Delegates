namespace DelegatesTest.TestObjects
{
    public interface IService
    {
        string Echo(string text);
        T Echo<T>(T o);
    }
}