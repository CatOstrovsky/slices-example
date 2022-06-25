using System.Threading.Tasks;
using Core;
using Events;
using Plugins.Common;
using Service.Storage;

namespace Service.Profile
{
    using Log = Log<StorageProfileService>;

    public class StorageProfileService : 
        ServiceBase,
        IProfileService
    {
        public const string profileDataStorageKey = "Profile";
        private ProfileDataModel model = new ProfileDataModel();
        private IStorageService storageService;

        public StorageProfileService(IStorageService storageService)
        {
            this.storageService = storageService;
        }

        public override async Task Init()
        {
            Log.Info($"Fake {nameof(StorageProfileService)} initialization delay");
            
            await storageService.IsInitializedTask;
            
            await Load();
            AddDataListeners();
            await Task.Delay(3000);
            
            await base.Init();

        }

        private void AddDataListeners()
        {
            model.totalScore.onValueChanged += score =>
            {
                EventSystem.Instance.Emmit(new ScoreUpdatedEvent(score));
            };
        }

        public void SetSore(int score)
        {
            model.totalScore.Value = score;
            Save().DoNotAwait();
        }

        public int GetScore()
        {
            return model.totalScore.Value;
        }

        public Task<bool> Save()
        {
            Log.Info("Profile sync...");
            return Task.FromResult(storageService.Set(profileDataStorageKey, model));
        }

        public Task<bool> Load()
        {
            Log.Info("Profile loading...");
            return Task.FromResult(storageService.Get(profileDataStorageKey, ref model));
        }
    }
}