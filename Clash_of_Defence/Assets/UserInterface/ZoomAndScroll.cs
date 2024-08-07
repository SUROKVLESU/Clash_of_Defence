using UnityEngine;

public class ZoomAndScroll:MonoBehaviour
{
    private Vector3 MousePosition;
    private RectTransform rectTransform;
    private const float RegionZoom = 0.1f;
    private const float MultiplierZoom = 2f;
    private const float MultiplierScroll = 1f;
    private const float MinY = 5;
    private const float MaxY = 45;
    private const float CorrectionCameraPositionTop = 10f;
    private const float CorrectionCameraPositionBottom = 20f;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        gameObject.GetComponent<BoxCollider>().size = new Vector2(rectTransform.rect.width, rectTransform.rect.height);
    }
    private void OnMouseDown()
    {
        MousePosition=Input.mousePosition;
    }
    private void OnMouseDrag()
    {
        if (MousePosition.x >= rectTransform.rect.width * (1 - RegionZoom))
        {
            GameController.instance.Camera.transform.position += 
                new Vector3(0, (MousePosition.y- Input.mousePosition.y)*Time.deltaTime*MultiplierZoom, 0);
            if (GameController.instance.Camera.transform.position.y < MinY )
            {
                GameController.instance.Camera.transform.position = new Vector3
                    (GameController.instance.Camera.transform.position.x, MinY,
                    GameController.instance.Camera.transform.position.z);
            }
            if (GameController.instance.Camera.transform.position.y > MaxY)
            {
                GameController.instance.Camera.transform.position = new Vector3
                    (GameController.instance.Camera.transform.position.x, MaxY,
                    GameController.instance.Camera.transform.position.z);
            }
            MousePosition = Input.mousePosition;
        }
        else
        {
            GameController.instance.Camera.transform.position +=
                new Vector3((MousePosition.x - Input.mousePosition.x) * Time.deltaTime* MultiplierScroll, 0,
                (MousePosition.y - Input.mousePosition.y) * Time.deltaTime* MultiplierScroll);
            if (GameController.instance.Camera.transform.position.x<GameController.instance.MapController.FrontierMap.x)
            {
                GameController.instance.Camera.transform.position = new Vector3
                    (GameController.instance.MapController.FrontierMap.x, GameController.instance.Camera.transform.position.y,
                    GameController.instance.Camera.transform.position.z);
            }
            if (GameController.instance.Camera.transform.position.x > GameController.instance.MapController.FrontierMap.y)
            {
                GameController.instance.Camera.transform.position = new Vector3
                    (GameController.instance.MapController.FrontierMap.y, GameController.instance.Camera.transform.position.y,
                    GameController.instance.Camera.transform.position.z);
            }
            if (GameController.instance.Camera.transform.position.z >
                GameController.instance.MapController.FrontierMap.z- CorrectionCameraPositionTop)
            {
                GameController.instance.Camera.transform.position = new Vector3
                    (GameController.instance.Camera.transform.position.x, GameController.instance.Camera.transform.position.y,
                    GameController.instance.MapController.FrontierMap.z- CorrectionCameraPositionTop);
            }
            if (GameController.instance.Camera.transform.position.z <
                GameController.instance.MapController.FrontierMap.w- CorrectionCameraPositionBottom)
            {
                GameController.instance.Camera.transform.position = new Vector3
                    (GameController.instance.Camera.transform.position.x, GameController.instance.Camera.transform.position.y,
                    GameController.instance.MapController.FrontierMap.w- CorrectionCameraPositionBottom);
            }
            MousePosition = Input.mousePosition;
        }
    }
    private void OnMouseUp()
    {
        
    }
}