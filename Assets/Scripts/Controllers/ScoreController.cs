using System;
using Core;

public class ScoreController : ControllerBase
{
    private ScoreModel model = new ScoreModel();
    public ScoreView view;
    public override ViewBase GetView => view;

    private void Start()
    {
        view.UpdateScoreText(model.currentScore);
    }

    public void AddScore(int score)
    {
        model.currentScore += score;
        view.UpdateScoreText(score);
    }
}
