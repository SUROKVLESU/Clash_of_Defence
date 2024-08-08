using UnityEngine;
using UnityEngine.UI;
public class ResourcesController:MonoBehaviour
{
    [HideInInspector] public int Gold;
    [HideInInspector] public Resources GameResources;
    [HideInInspector] public Resources PriceNewMapBlock = new Resources() { Gold=500};
    private const float MultiplierPriceNewMapBlock = 1.4f;
    [Header("GameResources")]
    [SerializeField] Text TextGold;
    [SerializeField] Text TextIron;
    [SerializeField] Text TextPower;
    [Header("GameLevelUpBuilding")]
    [SerializeField] Text LevelUpTextGold;
    [SerializeField] Text LevelUpTextIron;
    [SerializeField] Text LevelUpTextPower;
    [Header("PriceNewMapBlock")]
    [SerializeField] Text PriceNewMapBlockText;
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
        TextGold.text = ":" + GameResources.Gold;
        TextIron.text = ":" + GameResources.Iron;
        TextPower.text = ":" + GameResources.Power;
    }
    public void SetTextOnUpdate(Resources resources)
    {
        LevelUpTextGold.text = ":"+resources.Gold;
        LevelUpTextIron.text = ":" + resources.Iron;
        LevelUpTextPower.text = ":" + resources.Power;
    }
    private void Start()
    {
        UpdateGameResources(new Resources());
        PriceNewMapBlockText.text = ":"+ PriceNewMapBlock.Gold;
    }
    public void UbdatePriceNewMapBlockText()
    {
        UpdateGameResources(PriceNewMapBlock,false);
        PriceNewMapBlock *= MultiplierPriceNewMapBlock;
        PriceNewMapBlockText.text = ":" + PriceNewMapBlock.Gold;
    }
    public void SaveGameGold()
    {
        Gold += GameResources.Gold;
    }
    public void ResetController()
    {
        GameResources = new();
        PriceNewMapBlock = new Resources() { Gold = 500 };
        Start();
    }
}
