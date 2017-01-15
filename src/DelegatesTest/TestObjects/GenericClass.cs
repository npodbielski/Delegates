namespace DelegatesTest.TestObjects
{
    public class GenericClass<T>
    {
        public static T StaticGenericMethod(T param)
        {
            return param;
        }

        public T InstanceGenericMethod(T param)
        {
            return param;
        }
    }
}