using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class MapController:MonoBehaviour
{
    public MapBlock[] ArrayMapBlock;
    public UserInteractionBuilding[] UserInteractionBuilding;
    public UserInteractionBuilding SelectedCardGameObject;
    public MapController()
    {
        ArrayMapBlock = new MapBlock[] { new MapBlock()};
        UserInteractionBuilding = new UserInteractionBuilding[0];
    }
    public bool IsFreeCell()
    {
        for (int i = 0; i < ArrayMapBlock.Length; i++)
        {
            if(ArrayMapBlock[i].IsFreeCell())
            {
                return true;
            }
        }
        return false;
    }
    private void AddMapBlock(Vector2Int position)
    {
        MapBlock[] arr = new MapBlock[ArrayMapBlock.Length+1];
        for (int i = 0;i < ArrayMapBlock.Length;i++)
        {
            arr[i] = ArrayMapBlock[i];
        }
        arr[ArrayMapBlock.Length] = new MapBlock(position);
        ArrayMapBlock = arr;
    }
    public void AddBuilding(BuildingFromCard building)
    {
        UserInteractionBuilding[] arr = new UserInteractionBuilding[UserInteractionBuilding.Length+1];
        for(int i = 0; UserInteractionBuilding.Length > i; i++)
        {
            arr[i]=UserInteractionBuilding[i];
        }
        GameObject gameObject = Instantiate(building.Building);
        MapCell FreeCell = SearchFreeCell();
        gameObject.transform.position = new Vector3(FreeCell.Position.x,0,FreeCell.Position.y);
        arr[UserInteractionBuilding.Length] = gameObject.GetComponent<UserInteractionBuilding>();
        UserInteractionBuilding = arr;
        UserInteractionBuilding[UserInteractionBuilding.Length-1].CardId = building.CardId;
        FreeCell.Free=false;
    }
    private MapCell SearchFreeCell()
    {
        for(int i = 0;i<ArrayMapBlock.Length;i++)
        {
            if (ArrayMapBlock[i].IsFreeCell())
            {
                return ArrayMapBlock[i].SearchFreeCell();
            }
        }
        return null;
    }
    public bool IsPositionCell(Vector2Int position)
    {
        for (int i = 0; i < ArrayMapBlock.Length; i++)
        {
            if (ArrayMapBlock[i].IsPositionCell(position))
            {
                return true;
            }
        }
        return false;
    }
    public void SetFreeCell(Vector2Int position, bool free)
    {
        for (int i = 0; i < ArrayMapBlock.Length; i++)
        {
            ArrayMapBlock[i].SetFreeCell(position, free);
        }
    }
}

