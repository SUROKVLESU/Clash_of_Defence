using System.Collections.Generic;
using UnityEngine;

public class MapController:MonoBehaviour
{
    public MapBlock[] ArrayMapBlock;
    public List<GameObject> ArrayMapBlockGameObjects=new List<GameObject>();
    public List<GameObject> Buildings;
    public GameObject MainBuilding;
    public Vector3Int[] ArrayNewPositionMapBlock;
    public Vector3Int[] ArrayOldPositionMapBlock;            //(3)
    public Vector4 FrontierMap;                             //(1)(2)
    private GameObject[] ArrayNewMapBlockButton;           //  (4)   -это порядок границ карты.
    public UserInteractionBuilding SelectedCardGameObject;
    public int ActiveCount {  get;  set; }
    private float Radius = 10;
    private const int SizeMapXxX = 20;
    private const int SizeCell = 4;
    public MapController()
    {
        ArrayMapBlock = new MapBlock[] { new MapBlock() };
        NewFrontierMap();
        ArrayOldPositionMapBlock = new Vector3Int[] { new Vector3Int() };
        Buildings = new();
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
    public void SetWall()
    {
        IBaseInterface baseMainBuilding = MainBuilding.GetComponent<IBaseInterface>();
        baseMainBuilding.WallGameObject = null;
        IBaseInterface[] basesBuilding = new IBaseInterface[Buildings.Count];
        for (int i = 0; i < Buildings.Count; i++)
        {
            basesBuilding[i] = Buildings[i].GetComponent<IBaseInterface>();
            basesBuilding[i].WallGameObject=null;
        }
        for (int i = 0; i < Buildings.Count; i++)
        {
            if (basesBuilding[i].GetTypeBuilder() == TypeBuildings.Wall)
            {
                if (baseMainBuilding.GetAttackTargetPosition().position.x
                        > basesBuilding[i].GetForder(true).position.x
                        && baseMainBuilding.GetAttackTargetPosition().position.x
                        < basesBuilding[i].GetForder(false).position.x
                        && baseMainBuilding.GetAttackTargetPosition().position.z
                        < basesBuilding[i].GetForder(true).position.z
                        && baseMainBuilding.GetAttackTargetPosition().position.z
                        > basesBuilding[i].GetForder(false).position.z)
                {
                    baseMainBuilding.WallGameObject = Buildings[i];
                }
                for (int j = 0; j < Buildings.Count; j++)
                {
                    if (basesBuilding[j].GetTypeTarget()==TypeAttack.Air)
                    {
                        continue;
                    }
                    if (j == i) continue;
                    if (basesBuilding[j].GetAttackTargetPosition().position.x 
                        > basesBuilding[i].GetForder(true).position.x
                        && basesBuilding[j].GetAttackTargetPosition().position.x 
                        < basesBuilding[i].GetForder(false).position.x
                        && basesBuilding[j].GetAttackTargetPosition().position.z 
                        < basesBuilding[i].GetForder(true).position.z
                        && basesBuilding[j].GetAttackTargetPosition().position.z 
                        > basesBuilding[i].GetForder(false).position.z)
                    {
                        basesBuilding[j].WallGameObject = Buildings[i];
                    }
                }
            }
        }

    }
    public void ProtectionAmplifier()
    {
        IBaseInterface baseMainBuilding = MainBuilding.GetComponent<IBaseInterface>();
        baseMainBuilding.AddProtection = new();
        IBaseInterface[] basesBuilding = new IBaseInterface[Buildings.Count];
        for (int i = 0; i < Buildings.Count; i++)
        {
            basesBuilding[i] = Buildings[i].GetComponent<IBaseInterface>();
            basesBuilding[i].AddProtection=new();
        }
        for (int i = 0; i < Buildings.Count; i++)
        {
            if (basesBuilding[i].GetTypeBuilder() == TypeBuildings.ProtectionAmplifier)
            {
                ProtectionAmplifier prtAmp=Buildings[i].GetComponent< ProtectionAmplifier>();
                if (baseMainBuilding.GetAttackTargetPosition().position.x
                        > prtAmp.GetTransformForder(true).position.x
                        && baseMainBuilding.GetAttackTargetPosition().position.x
                        < prtAmp.GetTransformForder(false).position.x
                        && baseMainBuilding.GetAttackTargetPosition().position.z
                        < prtAmp.GetTransformForder(true).position.z
                        && baseMainBuilding.GetAttackTargetPosition().position.z
                        > prtAmp.GetTransformForder(false).position.z)
                {
                    baseMainBuilding.AddProtection += prtAmp.GetProtectionAdd;
                }
                for (int j = 0; j < Buildings.Count; j++)
                {
                    if (j == i) continue;
                    if (basesBuilding[j].GetAttackTargetPosition().position.x
                        > prtAmp.GetTransformForder(true).position.x
                        && basesBuilding[j].GetAttackTargetPosition().position.x
                        < prtAmp.GetTransformForder(false).position.x
                        && basesBuilding[j].GetAttackTargetPosition().position.z
                        < prtAmp.GetTransformForder(true).position.z
                        && basesBuilding[j].GetAttackTargetPosition().position.z
                        > prtAmp.GetTransformForder(false).position.z)
                    {
                        basesBuilding[j].AddProtection += prtAmp.GetProtectionAdd;
                    }
                }
            }
        }
    }
    public void ResourcesAmplifier()
    {
        IBaseInterface[] basesBuilding = new IBaseInterface[Buildings.Count];
        for (int i = 0; i < Buildings.Count; i++)
        {
            basesBuilding[i] = Buildings[i].GetComponent<IBaseInterface>();
            if (basesBuilding[i].GetTypeBuilder() == TypeBuildings.Resources)
            {
                Buildings[i].GetComponent<BaseResourcesBuildingCharacteristics>().Add_Resources = new ();
            }
        }
        for (int i = 0; i < Buildings.Count; i++)
        {
            if (basesBuilding[i].GetTypeBuilder() == TypeBuildings.ResourcesAmplifier)
            {
                ResourcesAmplifier resAmp =Buildings[i].GetComponent<ResourcesAmplifier>();
                for (int j = 0; j < Buildings.Count; j++)
                {
                    if (j == i) continue;
                    if (basesBuilding[j].GetTypeBuilder() == TypeBuildings.Resources
                        &&basesBuilding[j].GetAttackTargetPosition().position.x
                        > resAmp.GetTransformForder(true).position.x
                        && basesBuilding[j].GetAttackTargetPosition().position.x
                        < resAmp.GetTransformForder(false).position.x
                        && basesBuilding[j].GetAttackTargetPosition().position.z
                        < resAmp.GetTransformForder(true).position.z
                        && basesBuilding[j].GetAttackTargetPosition().position.z
                        > resAmp.GetTransformForder(false).position.z)
                    {
                        Buildings[j].GetComponent<BaseResourcesBuildingCharacteristics>().Add_Resources += resAmp.GetResourcesAdd;
                    }
                }
            }
        }
    }
    public bool IsFreeCell(SizeMapCell sizeMapCell)
    {
        for (int i = 0; i < ArrayMapBlock.Length; i++)
        {
            if(ArrayMapBlock[i].IsFreeCell(sizeMapCell))
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
    public void AddMainBuilding()
    {
        BaseCard baseCard = (BaseCard)GameController.instance.CollectionController.MainBuilding;
        MainBuilding = Instantiate(baseCard.GameObjects[baseCard.GetLevel()]);
        MapCell FreeCell = SearchFreeCell(baseCard.SizeMapCell);
        MainBuilding.transform.position = new Vector3(FreeCell.Position.x, 0, FreeCell.Position.y);
        MainBuilding.GetComponent<UserInteractionBuilding>().Card = baseCard;
        SetFreeCell(FreeCell.Position, baseCard.SizeMapCell, false);
    }
    public void AddBuilding(BaseCard baseCard)
    {
        GameObject gameObject = Instantiate(baseCard.GameObjects[baseCard.GetLevel()]);
        MapCell FreeCell = SearchFreeCell(baseCard.SizeMapCell);
        gameObject.transform.position = new Vector3(FreeCell.Position.x,0,FreeCell.Position.y);
        Buildings.Add(gameObject);
        Buildings[Buildings.Count -1].GetComponent<UserInteractionBuilding>().Card = baseCard;
        SetFreeCell(FreeCell.Position,baseCard.SizeMapCell,false);
        ActiveCount += 1;
    }
    private MapCell SearchFreeCell(SizeMapCell sizeMapCell)
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
                    && ArrayMapBlock[i].IsFreeCell(sizeMapCell))
                {
                    result= ArrayMapBlock[i].SearchFreeCell(sizeMapCell);
                    break;
                }
            }
            tryingFind++;
        }
        return result;
    }
    private MapCell SearchFreeCell(Vector2Int position)
    {
        for (int i = 0; i < ArrayMapBlock.Length; i++)
        {
            MapCell result = ArrayMapBlock[i].SearchFreeCell(position);
            if (result != null)
            {
                return result;
            }
        }
        return null;
    }
    public bool IsPositionCell(Vector2Int position,SizeMapCell sizeMapCell)
    {
        for (int i = 0; i < ArrayMapBlock.Length; i++)
        {
            if (ArrayMapBlock[i].IsPositionSizeMapCell(position,sizeMapCell))
            {
                return true;
            }
        }
        return false;
    }
    public bool IsPositionCellSizeMapCell(Vector2Int position,SizeMapCell sizeMapCell)
    {
        for (int k = 0; k < sizeMapCell.Sizes.Length; k++)
        {
            for (int l = 0; l < sizeMapCell.Sizes[k].line.Length; l++)
            {
                if (sizeMapCell.Sizes[k].line[l])
                {
                    if (IsPositionCell(new Vector2Int(position.x + SizeCell * l, position.y + SizeCell * k)))
                    {
                        continue;
                    }
                    else return false;
                }
                else
                {
                    continue;
                }
            }
        }
        return true;
    }
    public bool IsPositionCell(Vector2Int position)
    {
        for (int i = 0; i < ArrayMapBlock.Length; i++)
        {
            if (ArrayMapBlock[i].IsPositionCell(position))
            {
                return true ;
            }
        }
        return false;
    }
    public void SetFreeCell(Vector2Int position,SizeMapCell sizeMapCell, bool free)
    {
        for (int k = 0; k < sizeMapCell.Sizes.Length; k++)
        {
            for (int l = 0; l < sizeMapCell.Sizes[k].line.Length; l++)
            {
                if (sizeMapCell.Sizes[k].line[l])
                {
                    SearchFreeCell(new Vector2Int(position.x + SizeCell * l, position.y + SizeCell * k)).Free=free;
                }
            }
        }
    }
    public void CreateButtonNewMapBlock()
    {
        if (ArrayNewMapBlockButton[0] != null
            ||GameController.instance.ResourcesController.GameResources
                <GameController.instance.ResourcesController.PriceNewMapBlock) return;
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
        if (SelectedCardGameObject==null|| SelectedCardGameObject.Card.Id==0)
        {
            return;
        }
        GameController.instance.CardListController.ReturnCardYourHand(SelectedCardGameObject.Card);
        SetFreeCell(new Vector2Int
            ((int)SelectedCardGameObject.transform.position.x, (int)SelectedCardGameObject.transform.position.z)
            , SelectedCardGameObject.Card.SizeMapCell, true);
        for (int i = 0,j=0; i < Buildings.Count; i++,j++)
        {
            if (Buildings[i].GetComponent<UserInteractionBuilding>().UniqueNumber == SelectedCardGameObject.UniqueNumber)
            {
                GameObject building = Buildings[i];
                Buildings.Remove(Buildings[i]);
                Destroy(building);
                j--;
            }
        }
        ActiveCount -= 1;
        CancellationSelected();
        SelectedCardGameObject = null;
    }
    public void ReturnAllCardYourHand()
    {
        if (Buildings.Count == 0) { return; }
        GameController.instance.CardListController.ReturnAllCardYourHand();
        ResetFreeMapCell();
        SetFreeCell(new Vector2Int((int)MainBuilding.transform.position.x, (int)MainBuilding.transform.position.z),
            ((BaseCard)GameController.instance.CollectionController.MainBuilding).SizeMapCell, false);
        for (int i = 0; i < Buildings.Count; i++)
        {
            Destroy (Buildings[i].gameObject);
        }
        Buildings = new();
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
        for (int i = 0; i < Buildings.Count; i++)
        {
            Buildings[i].GetComponent<UserInteractionBuilding>().ResetPosition();
            Buildings[i].GetComponent<IBaseInterface>().PrintAttackRadius(false);
        }
        MainBuilding.GetComponent<UserInteractionBuilding>().ResetPosition();
        GameController.instance.CardLevelController.OnOffUpdeteMenu(false);
    }
    public void LewelUpBuilding(BaseEssenceObject baseCard)
    {
        for (int i = 0; Buildings.Count > i; i++)
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
        IBaseInterface attacking;
        for (int i = 0; i < Buildings.Count; i++)
        {
            Buildings[i].SetActive(true);
            attacking = Buildings[i].GetComponent<IBaseInterface>();
            if (attacking.GetHP()<0)
            {
                attacking.Activation();
            }
            attacking.ResetHP();
        }
        //MainBuilding.SetActive(true);
        IBaseInterface mainBuilding = MainBuilding.GetComponent<IBaseInterface>();
        mainBuilding.ResetHP();
        //mainBuilding.Stop();
        if (!MainBuilding.activeSelf)
        {
            MainBuilding.SetActive(true);
            mainBuilding.Activation();
        }
        ActiveCount = Buildings.Count;
    }
    public void Pause()
    {
        for (int i = 0; i < Buildings.Count; i++)
        {
            Buildings[i].GetComponent<IBaseInterface>().Stop();
        }
        MainBuilding.GetComponent<IBaseInterface>().Stop();
    }
    public void OffPause()
    {
        for (int i = 0; i < Buildings.Count; i++)
        {
            Buildings[i].GetComponent<IBaseInterface>().Activation();
        }
        MainBuilding.GetComponent<IBaseInterface>().Activation();
        ActiveCount = Buildings.Count;
    }
    public void GetResources()
    {
        for (int i = 0; i < Buildings.Count; i++)
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
        for (int i = 0; i < Buildings.Count; i++)
        {
            Destroy(Buildings[i]);
        }
        Destroy(MainBuilding);
        //MainBuilding = null;
        Buildings = new();
        ArrayOldPositionMapBlock = new Vector3Int[] { new Vector3Int() };
        ArrayNewPositionMapBlock = new Vector3Int[4];
        AddMainBuilding();
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
    private void ResetFreeMapCell()
    {
        foreach (var item in ArrayMapBlock)
        {
            item.ResetFreeMapCell();
        }
    }
}

