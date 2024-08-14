using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
public class ResourcesController:MonoBehaviour
{
    [HideInInspector] public int Gold;
    [HideInInspector] public Resources GameResources;
    private List<ResourcesCell> ArrayResourcesCell=new();
    [HideInInspector] public Resources PriceNewMapBlock = new Resources() { Gold=500};
    [HideInInspector] public bool IsMultiplier = false;
    private const float MultiplierPriceNewMapBlock = 1.4f;
    private const float MultiplierGameResources = 1.5f;
    [Header("GameResources")]
    [SerializeField] Text TextGold;
    [SerializeField] Text TextIron;
    [SerializeField] Text TextPower;
    [Header("MaxGameResources")]
    [SerializeField] Text MaxTextGold;
    [SerializeField] Text MaxTextIron;
    [SerializeField] Text MaxTextPower;
    [Header("GameLevelUpBuilding")]
    [SerializeField] Text LevelUpTextGold;
    [SerializeField] Text LevelUpTextIron;
    [SerializeField] Text LevelUpTextPower;
    [Header("PriceNewMapBlock")]
    [SerializeField] Text PriceNewMapBlockText;
    [Header("DefeatInterfece")]
    [SerializeField] Text DefeatText;
    [Header("ShopInterfece")]
    [SerializeField] Text TextGoldShop;
    public void UpdateMaxGameResources()
    {
        Resources resources = new();
        for (int i = 0; i < ArrayResourcesCell.Count; i++)
        {
            resources += ArrayResourcesCell[i].GetMax();
        }
        MaxTextGold.text = CircumcisionNumber(resources.Gold);
        MaxTextIron.text = CircumcisionNumber(resources.Iron);
        MaxTextPower.text = CircumcisionNumber(resources.Power);
    }
    public void SetGameResources(Resources gameResources)
    {
        GameResources=gameResources;
    }
    public void AddResourcesCell(ResourcesCell resourcesCell)
    {
        ArrayResourcesCell.Add(resourcesCell);
    }
    public void DeleteResourcesCell(ResourcesCell resourcesCell)
    {
        ArrayResourcesCell.Remove(resourcesCell);
    }
    public void PlaceResourcesWarehouses(Resources resources)
    {   
        Resources addResources = resources;
        for (int i = 0; i < ArrayResourcesCell.Count; i++)
        {
            resources=ArrayResourcesCell[i].AddResources(resources);
            if (resources == new Resources()) break;
        }
        UpdateGameResources(addResources-resources);
    }
    public void GetResourcesWarehouses(Resources resources)
    {
        UpdateGameResources(resources,false);
        for (int i = 0; i < ArrayResourcesCell.Count; i++)
        {
            resources = ArrayResourcesCell[i].GetResources(resources);
            if (resources == new Resources()) break;
        }
    }
    public void ResetResourcesWarehouses()
    {
        Resources gameResources = GameResources;
        SetGameResources(new Resources());
        for (int i = 0; i < ArrayResourcesCell.Count; i++)
        {
            ArrayResourcesCell[i].DeleteResources();
        }
        PlaceResourcesWarehouses(gameResources);
    }
    public ResourcesController()
    {
        GameResources = new() {Gold=200,Iron=0,Power=0 };
    }
    public void UpdateGameResources(Resources resources,bool add =true)
    {
        if (add)
        {
            GameResources += resources;
        }
        else
        {
            GameResources -= resources;
        }
        TextGold.text = ":" + CircumcisionNumber(GameResources.Gold);
        TextIron.text = ":" + CircumcisionNumber(GameResources.Iron);
        TextPower.text = ":" + CircumcisionNumber(GameResources.Power);
    }
    public void SetTextOnUpdate(Resources resources)
    {
        LevelUpTextGold.text = ":"+ CircumcisionNumber(resources.Gold);
        LevelUpTextIron.text = ":" + CircumcisionNumber(resources.Iron);
        LevelUpTextPower.text = ":" + CircumcisionNumber(resources.Power);
    }
    private void Start()
    {
        UpdateGameResources(new Resources());
        PriceNewMapBlockText.text = ":"+ CircumcisionNumber(PriceNewMapBlock.Gold);
    }
    public void UbdatePriceNewMapBlockText()
    {
        GetResourcesWarehouses(PriceNewMapBlock);
        PriceNewMapBlock *= MultiplierPriceNewMapBlock;
        PriceNewMapBlockText.text = ":" + CircumcisionNumber(PriceNewMapBlock.Gold);
    }
    public void SaveGameGold(Resources resources)
    {
        Gold += resources.Gold;
    }
    public void ResetController()
    {
        IsMultiplier=false;
        GameResources = new();
        PriceNewMapBlock = new Resources() { Gold = 500 };
        ArrayResourcesCell = new();
        Start();
    }
    public void MultiplierResources()
    {
        if(IsMultiplier) return;
        IsMultiplier = true;
        GameController.instance.WaveController.IsPlayerDefeat = true;
        SaveGameGold(GameResources * MultiplierGameResources - GameResources);
        GameResources*=MultiplierGameResources;
        PrintDefeatGold();
        GameController.instance.SaveGame.Save();
    }
    public void PrintDefeatGold()
    {
        DefeatText.text = ":+"+ CircumcisionNumber(GameResources.Gold);
    }
    public string CircumcisionNumber(int number)
    {
        if (number >= 1000000000)
        {
            number /= 1000000000;
            return number.ToString() + "MM";
        }
        if (number >=1000000)
        {
            number /= 1000000;
            return number.ToString() + "M";
        }
        if (number >= 10000)
        {
            number /= 10000;
            return number.ToString() + "K";
        }
        return number.ToString();
    }
    public void PrintShopGold()
    {
        TextGoldShop.text = ":" + CircumcisionNumber(Gold);
    }
}
