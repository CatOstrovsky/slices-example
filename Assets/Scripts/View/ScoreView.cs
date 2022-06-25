using Core;
using Events;
using Plugins.Common;
using TMPro;

public class ScoreView : ViewBase
{
    public TextMeshProUGUI scoreText;

    private void Start()
    {
        EventSystem.Instance.Subscribe<ScoreUpdatedEvent>(UpdateScoreText);
    }

    private void OnDestroy()
    {
        EventSystem.Instance.Unsubscribe<ScoreUpdatedEvent>(UpdateScoreText);
    }

    private void UpdateScoreText(IEvent obj)
    {
        if (obj is ScoreUpdatedEvent scoreUpdatedEvent)
        {
            UpdateScoreText(scoreUpdatedEvent.score);
        }
    }

    
    public void UpdateScoreText(int score)
    {
        scoreText.text = score.ToString();
    }
}