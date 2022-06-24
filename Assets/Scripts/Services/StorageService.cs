namespace Managers
{
    public interface IStorageService
    {
        public string GetTest();
    }

    public class StorageService : IStorageService
    {
        public string test = "test";
        public string GetTest()
        {
            return test;
        }
    }
}