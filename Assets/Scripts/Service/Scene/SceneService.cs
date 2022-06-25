using System.Threading.Tasks;
using Core;
using Plugins.Common;
using UnityEditor;
using UnityEngine.SceneManagement;

namespace Service.Scene
{
    public class SceneService : ServiceBase, ISceneService
    {
        public string CurrentSceneName { get; private set; }
        public ISceneData CurrentSceneData { get; private set; }

        private ScenesScriptableObject scenesConfig;

        public SceneService(ScenesScriptableObject scenesConfig)
        {
            this.scenesConfig = scenesConfig;
        }
    
        public void LoadScene(int sceneId, ISceneData options = null)
        {
            var sceneAsset = GetScene(sceneId);
            if (sceneAsset != null)
            {
                CurrentSceneData = options;
                SceneManager.LoadScene(sceneAsset.name);
            }
        }

        private SceneAsset GetScene(int sceneId)
        {
            if (scenesConfig.scenes.Count - 1 <= sceneId)
            {
                return scenesConfig.scenes[sceneId];
            }
            else
            {
                Log<SceneService>.Error($"Unable to load a scene with ID {sceneId}");
                return null;
            }
        }

        public override async Task Init()
        {
            Log<SceneService>.Info($"Fake {nameof(SceneService)} initialization delay");
            await Task.Delay(1600);
        
            await base.Init();
        }
    }

    public static class SceneIds
    {
        public const int Bootstrap = 0;
        public const int GameBoard = 1;
    }
}