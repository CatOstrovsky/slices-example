using UnityEngine;
using UnityEngine.UIElements;

public class SlicePlace : MonoBehaviour
{
    private Button _trigger;
    
    private void Start()
    {
        if (TryGetComponent(out _trigger))
        {
            _trigger.clicked += OnClicked;
        }
    }

    private void OnDestroy()
    {
        if (_trigger != null)
        {
            _trigger.clicked -= OnClicked;
        }
    }

    private void OnClicked()
    {
        
    }
}
