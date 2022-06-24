using Plugins.Common;

namespace Managers
{
    using Log = Log<UserService>;

    public interface IUserService
    {
    }

    public class UserService : IUserService
    {
        public UserService(IStorageService storageService)
        {
            Log.Info($"Register with storage service! {storageService.GetTest()}");
        }
    }
}