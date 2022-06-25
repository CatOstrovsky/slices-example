using Core;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

namespace View
{
    public class LoadingView : ViewBase
    {
        public TextMeshProUGUI loadingText;
        public Slider loadingProgress; 
        
        public void UpdateLoadingText(string text)
        {
            loadingText.text = text;
        }

        public void UpdateLoadingProgress(float progress)
        {
            loadingProgress.DOValue(progress, .25f);
        }
    }
}