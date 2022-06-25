using Plugins.Common;
using Service.Profile;
using UnityEngine;

namespace DebugMenu
{
    public class ScoreDebugMenuPage : DebugMenuPageBase
    {
        private IProfileService profileService => ServiceProvider.Get<IProfileService>();
        public override string Name { get; } = "Score chets";

        public override void OnGUI()
        {
            if (profileService?.IsInitialized != true)
            {
                GUILayout.Label("Initialization in progress...");
                return;
            }

            if (GUILayout.Button("Add 10 points"))
            {
                profileService.SetSore(profileService.GetScore() + 10);
            }
        }
    }
}