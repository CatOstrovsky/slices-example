using System.Threading.Tasks;

namespace Core
{
    public abstract class ServiceBase : IService
    {
        public readonly TaskCompletionSource<bool> IsInitializedTCS = new TaskCompletionSource<bool>(false);
        public Task IsInitializedTask => IsInitializedTCS.Task;
        public bool IsInitialized => IsInitializedTCS.Task.Result;
        
        public virtual Task Init()
        {
            IsInitializedTCS.SetResult(true);
            return Task.CompletedTask;
        }
    }

    public interface IService
    {
        public Task IsInitializedTask { get; }
        public bool IsInitialized { get; }
    }
}