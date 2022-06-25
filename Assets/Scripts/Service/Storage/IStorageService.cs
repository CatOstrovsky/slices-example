using Core;

namespace Service.Storage
{
    public interface IStorageService : IService
    {
        bool Set<T>(string key, T value);
        bool Get<T>(string key, ref T output) where T : new();
    }
}