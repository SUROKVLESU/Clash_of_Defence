using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapController:MonoBehaviour
{
    public MapBlock[] ArrayMapBlock;
    public List<GameObject> ArrayMapBlockGameObjects=new List<GameObject>();
    public GameObject[] Buildings;
    public Vector3Int[] ArrayNewPositionMapBlock;
    public Vector3Int[] ArrayOldPositionMapBlock;            //(3)
    public Vector4 FrontierMap;                             //(1)(2)
    private GameObject[] ArrayNewMapBlockButton;           //  (4)   -это порядок границ карты.
    public UserInteractionBuilding SelectedCardGameObject;
    public int ActiveCount {  get;  set; }
    private float Radius = 10;
    private const int SizeMapXxX = 20;
    public MapController()
    {
        ArrayMapBlock = new MapBlock[] { new MapBlock() };
        NewFrontierMap();
        ArrayOldPositionMapBlock = new Vector3Int[] { new Vector3Int() };
        Buildings = new GameObject[0];
        ArrayNewPositionMapBlock = new Vector3Int[4];
        ArrayNewMapBlockButton = new GameObject[4];
        for (int i = 0; i < 4; i++)
        {
            switch (i)
            {
                case 0:
                    ArrayNewPositionMapBlock[i] = new Vector3Int(0, 0, SizeMapXxX);
                    break;
                case 1:
                    ArrayNewPositionMapBlock[i] = new Vector3Int(0, 0, -SizeMapXxX);
                    break;
                case 2:
                    ArrayNewPositionMapBlock[i] = new Vector3Int(-SizeMapXxX, 0, 0);
                    break;
                case 3:
                    ArrayNewPositionMapBlock[i] = new Vector3Int(SizeMapXxX, 0, 0);
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
    public void AddBuilding(BaseCard baseCard)
    {
        GameObject[] arr = new GameObject[Buildings.Length+1];
        for(int i = 0; Buildings.Length > i; i++)
        {
            arr[i]=Buildings[i];
        }
        GameObject gameObject = Instantiate(baseCard.GameObjects[baseCard.GetLevel()]);
        MapCell FreeCell = SearchFreeCell();
        gameObject.transform.position = new Vector3(FreeCell.Position.x,0,FreeCell.Position.y);
        arr[Buildings.Length] = gameObject;
        Buildings = arr;
        Buildings[Buildings.Length-1].GetComponent<UserInteractionBuilding>().Card = baseCard;
        FreeCell.Free=false;
        ActiveCount += 1;
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
                if (Mathf.Abs(ArrayMapBlock[i].Position.x - vector.x) < SizeMapXxX * tryingFind/2
                    && Mathf.Abs(ArrayMapBlock[i].Position.y - vector.y) < SizeMapXxX * tryingFind/2
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
        if (ArrayNewMapBlockButton[0] != null
            ||GameController.instance.ResourcesController.GameResources
                <=GameController.instance.ResourcesController.PriceNewMapBlock) return;
        GameController.instance.ButtonController.OffStartWaveButton();
        GameController.instance.MapController.CancellationSelected();
        GameController.instance.ResourcesController.UbdatePriceNewMapBlockText();
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
                    arr2[i] = new Vector3Int(position.x, 0, position.z+ SizeMapXxX);
                    break;
                case 1:
                    arr2[i] = new Vector3Int(position.x, 0, position.z - SizeMapXxX);
                    break;
                case 2:
                    arr2[i] = new Vector3Int(position.x - SizeMapXxX, 0, position.z);
                    break;
                case 3:
                    arr2[i] = new Vector3Int(position.x+ SizeMapXxX, 0, position.z);
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
        GameController.instance.CardListController.ReturnCardYourHand(SelectedCardGameObject.Card);
        SetFreeCell(new Vector2Int
            ((int)SelectedCardGameObject.transform.position.x, (int)SelectedCardGameObject.transform.position.z), true);
        GameObject[] arr = new GameObject[Buildings.Length-1];
        for (int i = 0,j=0; i < Buildings.Length; i++,j++)
        {
            if (Buildings[i].GetComponent<UserInteractionBuilding>().UniqueNumber == SelectedCardGameObject.UniqueNumber)
            {
                Destroy(Buildings[i].gameObject);
                j--;
            }
            else
            {
                arr [j] = Buildings[i];
            }
        }
        Buildings=arr;
        ActiveCount -= 1;
        SelectedCardGameObject = null;
    }
    public void ReturnAllCardYourHand()
    {
        if (Buildings.Length == 0) { return; }
        GameController.instance.CardListController.ReturnAllCardYourHand();
        for (int i = 0; i < Buildings.Length; i++)
        {
            SetFreeCell(new Vector2Int
            ((int)Buildings[i].transform.position.x, (int)Buildings[i].transform.position.z), true);
            Destroy (Buildings[i].gameObject);
        }
        Buildings = new GameObject [0];
        ActiveCount = 0;
    }
    private Vector2Int GetCenterScreenWorldCoordinate()
    {
        return new Vector2Int((int)GameController.instance.Camera.transform.position.x,
            (int)(GameController.instance.Camera.transform.position.z
            + Mathf.Abs(GameController.instance.Camera.transform.position.y) / Mathf.Sqrt(3)));
    }
    public void NewFrontierMap()
    {
        if (FrontierMap == null) { FrontierMap = new Vector4(); }
        for (int i = 0; i < ArrayMapBlock.Length; i++)
        {
            if (ArrayMapBlock[i].Position.x<=FrontierMap.x) { FrontierMap.x = ArrayMapBlock[i].Position.x- Radius; }
            if (ArrayMapBlock[i].Position.x >= FrontierMap.y) { FrontierMap.y = ArrayMapBlock[i].Position.x+ Radius; }
            if (ArrayMapBlock[i].Position.y >= FrontierMap.z) { FrontierMap.z = ArrayMapBlock[i].Position.y+ Radius; }
            if (ArrayMapBlock[i].Position.y <= FrontierMap.w) { FrontierMap.w = ArrayMapBlock[i].Position.y- Radius; }
        }
    }
    public void CancellationSelected()
    {
        for (int i = 0; i < Buildings.Length; i++)
        {
            Buildings[i].GetComponent<UserInteractionBuilding>().ResetPosition();
        }
        GameController.instance.CardLevelController.OnOffUpdeteMenu(false);
    }
    public void LewelUpBuilding(BaseEssenceObject baseCard)
    {
        for (int i = 0; Buildings.Length > i; i++)
        {
            if (Buildings[i].GetComponent<UserInteractionBuilding>().Card.Id == baseCard.Id)
            {
                GameObject gameObject = Instantiate(baseCard.GameObjects[baseCard.GetLevel()]);
                gameObject.transform.position = Buildings[i].transform.position;
                Destroy(Buildings[i].gameObject);
                Buildings[i] = gameObject;
                Buildings[i].GetComponent<UserInteractionBuilding>().Card = (BaseCard)baseCard;
                Buildings[i].GetComponent<UserInteractionBuilding>().SelectedBuilding(true);
            }
        }
    }
    public void ActivationBuildings()
    {
        for (int i = 0; i < Buildings.Length; i++)
        {
            Buildings[i].SetActive(true);
            IBaseInterface attacking = Buildings[i].GetComponent<IBaseInterface>();
            attacking.ResetHP();
            attacking.ActivationBuildings();
        }
        ActiveCount = Buildings.Length;
    }
    public void Pause()
    {
        for (int i = 0; i < Buildings.Length; i++)
        {
            Buildings[i].GetComponent<IBaseInterface>().Stop();
        }
    }
    public void OffPause()
    {
        for (int i = 0; i < Buildings.Length; i++)
        {
            IBaseInterface attacking = Buildings[i].GetComponent<IBaseInterface>();
            attacking.ActivationBuildings();
        }
        ActiveCount = Buildings.Length;
    }
    public void GetResources()
    {
        for (int i = 0; i < Buildings.Length; i++)
        {
            if (Buildings[i].GetComponent<BaseResourcesBuildingCharacteristics>())
            {
                Buildings[i].GetComponent<BaseResourcesBuildingCharacteristics>().GetResources();   
            }
        }
    }
    public void ResetController()
    {
        for (int i = 0; i < ArrayMapBlockGameObjects.Count; i++)
        {
            Destroy(ArrayMapBlockGameObjects[i]);
        }
        ArrayMapBlockGameObjects.Clear();
        ArrayMapBlock = new MapBlock[] { new MapBlock() };
        for (int i = 0; i < Buildings.Length; i++)
        {
            Destroy(Buildings[i]);
        }
        Buildings = new GameObject[0];
        ArrayOldPositionMapBlock = new Vector3Int[] { new Vector3Int() };
        ArrayNewPositionMapBlock = new Vector3Int[4];
        NewFrontierMap();
        SelectedCardGameObject=null;
        ArrayNewMapBlockButton = new GameObject[4];
        for (int i = 0; i < 4; i++)
        {
            switch (i)
            {
                case 0:
                    ArrayNewPositionMapBlock[i] = new Vector3Int(0, 0, SizeMapXxX);
                    break;
                case 1:
                    ArrayNewPositionMapBlock[i] = new Vector3Int(0, 0, -SizeMapXxX);
                    break;
                case 2:
                    ArrayNewPositionMapBlock[i] = new Vector3Int(-SizeMapXxX, 0, 0);
                    break;
                case 3:
                    ArrayNewPositionMapBlock[i] = new Vector3Int(SizeMapXxX, 0, 0);
                    break;
            }
        }
    }
}

