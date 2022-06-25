using Core;
using Model;
using Plugins.Common;
using Service.Profile;
using View;

namespace Controller
{
    public class ScoreController : ControllerBase
    {
        private static IProfileService profileService => ServiceProvider.Get<IProfileService>();
        private ScoreModel model = new ScoreModel();
        public ScoreView view;
        public override ViewBase GetView => view;

        private void Start()
        {
            model.currentScore = profileService.GetScore();
            view.UpdateScoreText(model.currentScore);
        }

        public void AddScore(int score)
        {
            model.currentScore += score;
            profileService.SetSore(model.currentScore);
            view.UpdateScoreText(model.currentScore);
        }
    }
}