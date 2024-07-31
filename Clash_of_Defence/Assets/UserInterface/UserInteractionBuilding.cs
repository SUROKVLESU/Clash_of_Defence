using UnityEngine;

public class UserInteractionBuilding : MonoBehaviour
{
    public BaseCard Card;
    public int UniqueNumber;
    private static int NextUniqueNumber = 0;
    private Vector3 MousePositionDown;
    private Vector2Int OldPosition;
    private float ScrollStep;
    private Vector2Int newPosition;
    private bool Selected = false;
    private bool Moved = false;
    private const float LiftingHeight = 1;
    private void Awake()
    {
        ScrollStep = 180 * (Screen.currentResolution.height / 1080);
        UniqueNumber = NextUniqueNumber++;
    }
    private void OnMouseDown()
    {
        MousePositionDown = Input.mousePosition;
        OldPosition = new Vector2Int((int)transform.position.x, (int)transform.position.z);
    }
    private void OnMouseDrag()
    {
        Vector3 vector = Input.mousePosition-MousePositionDown;
        int CountX = (int)((vector.x / ScrollStep)
            *(GameController.instance.Camera.transform.position.y/GameController.instance.StartHeightCamera));
        int CountY = (int)((vector.y / ScrollStep) 
            * (GameController.instance.Camera.transform.position.y / GameController.instance.StartHeightCamera));
        newPosition = new Vector2Int(OldPosition.x+4*CountX, OldPosition.y+4*CountY);
        if(GameController.instance.MapController.IsPositionCell(newPosition))
        {
            GameController.instance.MapController.SetFreeCell(OldPosition, true);
            transform.position = new Vector3(newPosition.x, LiftingHeight, newPosition.y);
            Moved = true;
        }
    }
    private void OnMouseUp()
    {
        GameController.instance.MapController.SetFreeCell(newPosition, false);
        if (Moved)
        {
            SelectedBuilding(false);
            Moved = false;
        }
        else
        {
            SelectedBuilding(!Selected);
        }
        }
    private void SelectedBuilding(bool selected)
    {
        GameController.instance.MapController.CancellationSelected();
        GameController.instance.CardLevelController.OnOffUpdeteMenu(selected);
        if (selected)
        {
            GameController.instance.MapController.SelectedCardGameObject = this;
            Selected = true;
            transform.position = new Vector3(transform.position.x, LiftingHeight, transform.position.z);
        }
        else
        {
            GameController.instance.MapController.SelectedCardGameObject = null;
            Selected = false;
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
    }
    public void ResetPosition()
    {
        if (Selected)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            Selected = false;
            GameController.instance.MapController.SelectedCardGameObject = null;
        }
    }
}

