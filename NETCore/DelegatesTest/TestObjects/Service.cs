namespace DelegatesTest.TestObjects
{
    public class Service : IService
    {
        public string Echo(string text)
        {
            return text;
        }

        public T Echo<T>(T o)
        {
            return o;
        }
    }
}
