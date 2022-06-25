using Core;

namespace Service.Scene
{
    public interface ISceneService : IService
    {
        string CurrentSceneName { get; }
        ISceneData CurrentSceneData { get; }
        void LoadScene(int sceneId, ISceneData options = null);
    }
}
