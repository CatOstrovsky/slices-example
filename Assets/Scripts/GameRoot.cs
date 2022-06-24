using Autofac;
using Managers;
using UnityEngine;

public class GameRoot : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(this);
        
        var builder = new ContainerBuilder();

        builder.RegisterType<UserService>();
        builder.RegisterType<StorageService>().As<IStorageService>();
        var container = builder.Build();
        
        var userInfoManager = container.Resolve<UserService>();
    }
}
