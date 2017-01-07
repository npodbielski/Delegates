namespace DelegatesTest.TestObjects
{
    public class Service : IService
    {
        public string Echo(string text)
        {
            return text;
        }
    }
}
