using UnityEngine;

public class ScaleBoxCollider:MonoBehaviour
{
    private void Awake()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        gameObject.GetComponent<BoxCollider>().size = new Vector2(rectTransform.rect.width, rectTransform.rect.height);
    }
}
