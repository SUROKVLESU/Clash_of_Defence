using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class UserInteractionBuilding:MonoBehaviour
{
    public int CardId;
    public int UniqueNumber;
    private static int NextUniqueNumber = 0;
    private Vector3 MousePositionDown;
    private Vector2Int OldPosition;
    private float ScrollStep;
    private Vector2Int newPosition;
    private void Awake()
    {
        ScrollStep = 180 * (Screen.currentResolution.height/1080);
        UniqueNumber = NextUniqueNumber++;
    }
    private void OnMouseDown()
    {
        GameController.instance.MapController.SelectedCardGameObject = this;
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
            transform.position = new Vector3(newPosition.x, 0, newPosition.y);
        }
    }
    private void OnMouseUp()
    {
        GameController.instance.MapController.SetFreeCell(newPosition, false);
    }
}

