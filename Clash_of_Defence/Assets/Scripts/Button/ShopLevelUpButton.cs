using UnityEngine;
using UnityEngine.UI;

public class ShopLevelUpButton:MonoBehaviour
{
    [HideInInspector] public BaseCard BaseCard;
    public Text Price;
    public Text LVL;
    private void OnMouseUp()
    {
        if (BaseCard.GetCurrentMaxLevel() < BaseCard.MaxLevel
            && BaseCard.PriceCard[BaseCard.GetCurrentMaxLevel()]<=GameController.instance.ResourcesController.Gold
            && BaseCard.SetCurrentMaxLevel(BaseCard.GetCurrentMaxLevel() + 1))
        {
            GameController.instance.ResourcesController.Gold -= BaseCard.PriceCard[BaseCard.GetCurrentMaxLevel()-1];
            GameController.instance.ResourcesController.PrintShopGold();
            if (BaseCard.GetCurrentMaxLevel() < BaseCard.MaxLevel)
            {
                Price.text = GameController.instance.ResourcesController.CircumcisionNumber
                    (BaseCard.PriceCard[BaseCard.GetCurrentMaxLevel()]);
                LVL.text = BaseCard.GetCurrentMaxLevel().ToString();
            }
            else
            {
                Price.text = "-";
                LVL.text = "-";
            }
        }
    }
    public void Initialization(BaseCard baseCard)
    {
        BaseCard = baseCard;
        transform.parent.GetChild(0).GetComponent<Image>().sprite = BaseCard.CardCover.GetComponent<Image>().sprite;
        if (BaseCard.GetCurrentMaxLevel() < BaseCard.MaxLevel)
        {
            Price.text = GameController.instance.ResourcesController.CircumcisionNumber
                (BaseCard.PriceCard[BaseCard.GetCurrentMaxLevel()]);
            LVL.text = BaseCard.GetCurrentMaxLevel().ToString();
        }
        else
        {
            Price.text = "-";
            LVL.text = "-";
        }
    }
}

