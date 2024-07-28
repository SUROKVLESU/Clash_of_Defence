using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class MapController:MonoBehaviour
{
    public MapBlock[] ArrayMapBlock;
    public UserInteractionBuilding[] UserInteractionBuilding;
    public Vector3Int[] ArrayNewPositionMapBlock;
    public Vector3Int[] ArrayOldPositionMapBlock;
    private GameObject[] ArrayNewMapBlockButton;
    public UserInteractionBuilding SelectedCardGameObject;
    public MapController()
    {
        ArrayMapBlock = new MapBlock[] { new MapBlock() };
        ArrayOldPositionMapBlock = new Vector3Int[] { new Vector3Int() };
        UserInteractionBuilding = new UserInteractionBuilding[0];
        ArrayNewPositionMapBlock = new Vector3Int[4];
        ArrayNewMapBlockButton = new GameObject[4];
        for (int i = 0; i < 4; i++)
        {
            switch (i)
            {
                case 0:
                    ArrayNewPositionMapBlock[i] = new Vector3Int(0, 0, 20);
                    break;
                case 1:
                    ArrayNewPositionMapBlock[i] = new Vector3Int(0, 0, -20);
                    break;
                case 2:
                    ArrayNewPositionMapBlock[i] = new Vector3Int(-20, 0, 0);
                    break;
                case 3:
                    ArrayNewPositionMapBlock[i] = new Vector3Int(20, 0, 0);
                    break;
            }
        }
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
    public void AddMapBlock(Vector2Int position)
    {
        MapBlock[] arr = new MapBlock[ArrayMapBlock.Length+1];
        for (int i = 0;i < ArrayMapBlock.Length;i++)
        {
            arr[i] = ArrayMapBlock[i];
        }
        arr[ArrayMapBlock.Length] = new MapBlock(position);
        ArrayMapBlock = arr;
        Vector3Int[] arr2 = new Vector3Int[ArrayOldPositionMapBlock.Length + 1]; 
        for(int i = 0;i< ArrayOldPositionMapBlock.Length; i++)
        {
            arr2[i] = ArrayOldPositionMapBlock[i];
        }
        arr2[ArrayOldPositionMapBlock.Length] = new Vector3Int(position.x,0,position.y);
        ArrayOldPositionMapBlock= arr2;
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
    public void CreateButtonNewMapBlock()
    {
        for (int i = 0; i < ArrayNewPositionMapBlock.Length; ++i)
        {
            ArrayNewMapBlockButton[i] = Instantiate(GameController.instance.ButtonNewMapBlock);
            ArrayNewMapBlockButton[i].transform.position = ArrayNewPositionMapBlock[i];
        }
    }
    public void DestroyButtonNewMapBlock()
    {
        for (int i = 0; i < ArrayNewMapBlockButton.Length; ++i)
        {
            Destroy(ArrayNewMapBlockButton[i]);
        }
    }
    public void NewArrayNewPositionMapBlock(Vector3Int position)
    {
        Vector3Int[] arr = new Vector3Int[ArrayNewPositionMapBlock.Length-1];
        for(int i = 0,j=0;i < ArrayNewPositionMapBlock.Length;i++,j++)
        {
            if (ArrayNewPositionMapBlock[i]==position)
            {
                j--;
            }
            else
            {
                arr[j] = ArrayNewPositionMapBlock[i];
            }
        }
        ArrayNewPositionMapBlock = arr;
        Vector3Int[] arr2 = new Vector3Int[4];
        for (int i = 0; i < 4; i++)
        {
            switch (i)
            {
                case 0:
                    arr2[i] = new Vector3Int(position.x, 0, position.z+ 20);
                    break;
                case 1:
                    arr2[i] = new Vector3Int(position.x, 0, position.z - 20);
                    break;
                case 2:
                    arr2[i] = new Vector3Int(position.x - 20, 0, position.z);
                    break;
                case 3:
                    arr2[i] = new Vector3Int(position.x+20, 0, position.z);
                    break;
            }
        }
        int newCount = 0;
        Vector3Int vector3Int = new Vector3Int(10000, 0, 0);
        for (int i = 0; i < arr2.Length; i++)
        {
            for (int j = 0; j < ArrayNewPositionMapBlock.Length; j++)
            {
                if (arr2[i]== ArrayNewPositionMapBlock[j])
                {
                    arr2[i] = vector3Int;
                }
            }
            for (int k = 0; k < ArrayOldPositionMapBlock.Length; k++)
            {
                if (arr2[i] == ArrayOldPositionMapBlock[k])
                {
                    arr2[i] = vector3Int; 
                }
            }
            if (arr2[i] != vector3Int) {newCount++; }
        }
        arr = new Vector3Int[ArrayNewPositionMapBlock.Length +newCount];
        for (int i = 0; i < ArrayNewPositionMapBlock.Length; i++)
        {

            arr[i] = ArrayNewPositionMapBlock[i];

        }
        for(int i = 0,j= ArrayNewPositionMapBlock.Length; i < arr2.Length;i++)
        {
            if (arr2[i] != vector3Int)
            {
                arr[j]= arr2[i];
                j++;
            }
        }
        ArrayNewPositionMapBlock = arr;
        ArrayNewMapBlockButton = new GameObject[ArrayNewPositionMapBlock.Length];
    }
}

