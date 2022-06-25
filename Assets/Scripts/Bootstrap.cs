using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac;
using Core;
using DebugMenu;
using Plugins.Common;
using Service.Profile;
using Service.Scene;
using Service.Storage;
using UnityEngine;

using Log = Plugins.Common.Log<Bootstrap>;
public class Bootstrap : MonoBehaviour
{
    [Header("Scene Service Configuration")]
    public ScenesScriptableObject scenesConfig;

    public static Bootstrap Instance;

    public Observable<float> serviceLoadingProgress = new Observable<float>();

    private void Awake()
    {
        DontDestroyOnLoad(this);

        RegisterServices();
        ConfigureDebugMenu();

        Instance = this;
    }

    private void ConfigureDebugMenu()
    {
        Plugins.Common.DebugMenu.AddPage(new StorageDebugMenuPage());
        Plugins.Common.DebugMenu.AddPage(new ScoreDebugMenuPage());
    }

    private void RegisterServices()
    {
        var builder = new ContainerBuilder();

        builder.RegisterInstance(scenesConfig).As<ScenesScriptableObject>();
        builder.RegisterInstance(new PlayerPrefsStorageService()).As<IStorageService>();

        builder.RegisterType<StorageProfileService>().As<IProfileService>();
        builder.RegisterType<SceneService>().As<ISceneService>();

        var container = builder.Build();

        ServiceProvider.Register(container.Resolve<IProfileService>());
        ServiceProvider.Register(container.Resolve<IStorageService>());
        ServiceProvider.Register(container.Resolve<ISceneService>());

        Initialize().DoNotAwait();
    }

    private async Task Initialize()
    {
        var services = ServiceProvider.GetAll();
        foreach (var serviceKvp in services)
        {
            if (serviceKvp.Value is ServiceBase serviceBase)
            {
                serviceBase.Init().DoNotAwait();
            }
        }

        var index = 0;
        foreach (var serviceKvp in services)
        {
            if (serviceKvp.Value is ServiceBase serviceBase)
            {
                index++;
                serviceLoadingProgress.Value = (float)index / services.Count;

                await serviceBase.IsInitializedTask;
            }
        }

        ServiceProvider.Get<ISceneService>().LoadScene(SceneIds.GameBoard);
    }
}