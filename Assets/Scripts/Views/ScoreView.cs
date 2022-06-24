using Core;
using TMPro;
using UnityEngine;

public class ScoreView : ViewBase
{
    public TextMeshProUGUI scoreText;

    public void UpdateScoreText(int score)
    {
        scoreText.text = score.ToString();
    }
}