using Newtonsoft.Json;
using Plugins.Common;
using Service.Profile;
using Service.Storage;
using UnityEngine;

namespace DebugMenu
{
    public class StorageDebugMenuPage : DebugMenuPageBase
    {
        private IStorageService storageService => ServiceProvider.Get<IStorageService>();
        private ProfileDataModel profileDataModel;
        public override string Name { get; } = "Storage";

        public override void OnGUI()
        {
            if (storageService?.IsInitialized != true)
            {
                GUILayout.Label("Initialization in progress...");
                return;
            }

            GUILayout.BeginVertical(GUI.skin.box);
            GUILayout.Label("Profile data");
            GUILayout.TextArea(JsonConvert.SerializeObject(profileDataModel));
            if (GUILayout.Button("Refresh") || profileDataModel == null)
            {
                profileDataModel = new ProfileDataModel();
                storageService.Get(StorageProfileService.profileDataStorageKey, ref profileDataModel);
            }

            GUILayout.EndVertical();
        }
    }
}