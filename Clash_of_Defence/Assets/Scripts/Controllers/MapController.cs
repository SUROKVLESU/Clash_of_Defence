using UnityEngine;

public class MapController:MonoBehaviour
{
    public MapBlock[] ArrayMapBlock;
    public UserInteractionBuilding[] UserInteractionBuilding;
    public Vector3Int[] ArrayNewPositionMapBlock;
    public Vector3Int[] ArrayOldPositionMapBlock;
    public Vector4 FrontierMap;
    private GameObject[] ArrayNewMapBlockButton;
    public UserInteractionBuilding SelectedCardGameObject;
    public MapController()
    {
        ArrayMapBlock = new MapBlock[] { new MapBlock() };
        NewFrontierMap();
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
        NewFrontierMap();
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
        Vector2Int vector = GetCenterScreenWorldCoordinate();
        int tryingFind = 1;
        MapCell result = null;
        while (result==null)
        {
            for (int i = 0; i < ArrayMapBlock.Length; i++)
            {
                if (Mathf.Abs(ArrayMapBlock[i].Position.x - vector.x) < 10 * tryingFind
                    && Mathf.Abs(ArrayMapBlock[i].Position.y - vector.y) < 10 * tryingFind
                    && ArrayMapBlock[i].IsFreeCell())
                {
                    result= ArrayMapBlock[i].SearchFreeCell();
                    break;
                }
            }
            tryingFind++;
        }
        return result;
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
    public void ReturnCardYourHand()
    {
        if (SelectedCardGameObject==null)
        {
            return;
        }
        GameController.instance.CardListController.ReturnCardYourHand(SelectedCardGameObject.CardId);
        SetFreeCell(new Vector2Int
            ((int)SelectedCardGameObject.transform.position.x, (int)SelectedCardGameObject.transform.position.z), true);
        UserInteractionBuilding[] arr = new UserInteractionBuilding[UserInteractionBuilding.Length-1];
        for (int i = 0,j=0; i < UserInteractionBuilding.Length; i++,j++)
        {
            if (UserInteractionBuilding[i].UniqueNumber == SelectedCardGameObject.UniqueNumber)
            {
                Destroy(UserInteractionBuilding[i].gameObject);
                j--;
            }
            else
            {
                arr [j] = UserInteractionBuilding[i];
            }
        }
        UserInteractionBuilding=arr;
        SelectedCardGameObject = null;
    }
    public void ReturnAllCardYourHand()
    {
        if (UserInteractionBuilding.Length == 0) { return; }
        GameController.instance.CardListController.ReturnAllCardYourHand();
        for (int i = 0; i < UserInteractionBuilding.Length; i++)
        {
            SetFreeCell(new Vector2Int
            ((int)UserInteractionBuilding[i].transform.position.x, (int)UserInteractionBuilding[i].transform.position.z), true);
            Destroy (UserInteractionBuilding[i].gameObject);
        }
        UserInteractionBuilding = new UserInteractionBuilding [0];
    }
    private Vector2Int GetCenterScreenWorldCoordinate()
    {
        return new Vector2Int((int)GameController.instance.Camera.transform.position.x,
            (int)(GameController.instance.Camera.transform.position.z
            +Mathf.Sqrt
            (3* GameController.instance.Camera.transform.position.y* GameController.instance.Camera.transform.position.y)/2));
    }
    public void NewFrontierMap()
    {
        if (FrontierMap == null) { FrontierMap = new Vector4(); }
        for (int i = 0; i < ArrayMapBlock.Length; i++)
        {
            if (ArrayMapBlock[i].Position.x<=FrontierMap.x) { FrontierMap.x = ArrayMapBlock[i].Position.x-10; }
            if (ArrayMapBlock[i].Position.x >= FrontierMap.y) { FrontierMap.y = ArrayMapBlock[i].Position.x+10; }
            if (ArrayMapBlock[i].Position.y >= FrontierMap.z) { FrontierMap.z = ArrayMapBlock[i].Position.y+10; }
            if (ArrayMapBlock[i].Position.y <= FrontierMap.w) { FrontierMap.w = ArrayMapBlock[i].Position.y-10; }
        }
    }
}

