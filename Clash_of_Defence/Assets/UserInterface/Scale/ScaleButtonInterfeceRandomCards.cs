using UnityEngine;

public class ScaleButtonInterfeceRandomCards:MonoBehaviour
{
    private RectTransform rectTransform;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 
                rectTransform.rect.height * (1080/ Screen.currentResolution.height));
    }
}

