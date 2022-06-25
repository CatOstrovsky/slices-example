using System.Threading.Tasks;
using Core;

public interface IStorageService : IService
{
    bool Set<T>(string key, T value);
    bool Get<T>(string key, ref T output) where T : new();
}