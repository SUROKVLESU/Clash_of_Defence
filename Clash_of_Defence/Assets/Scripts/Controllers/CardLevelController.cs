using UnityEngine;

public class CardLevelController
{
    public void LewelUpBuilding(BaseCard baseCard)
    {
        if (baseCard == null) return;
        GameController.instance.MapController.LewelUpBuilding
            (GameController.instance.CollectionCardsController.LevelUpBaseCard(baseCard));
        //GameController.instance.MapController.CancellationSelected();
    }
    public void OnOffUpdeteMenu(bool onOff)
    {
        for (int i = 0; i < GameController.instance.LevelPanel.transform.childCount; i++)
        {
            GameController.instance.LevelPanel.transform.GetChild(i).gameObject.SetActive(false);
        }
        if (onOff && GameController.instance.MapController.SelectedCardGameObject.Card.IsMaxLevel())
        {
            GameController.instance.LevelPanel.transform.GetChild(1).gameObject.SetActive(true);
            return;
            //GameController.instance.LevelPanel.SetActive(false);
        }
        if (onOff && !GameController.instance.MapController.SelectedCardGameObject.Card.IsMaxLevel()
            && GameController.instance.MapController.SelectedCardGameObject.Card.Characteristics.Price
            [GameController.instance.MapController.SelectedCardGameObject.Card.GetLevel()+1]
            <= GameController.instance.ResourcesController.Gold)
        {
            GameController.instance.LevelPanel.transform.GetChild(0).gameObject.SetActive(onOff);
            GameController.instance.LevelPanel.transform.GetChild(2).gameObject.SetActive(false);
            return;
        }
        if(onOff && !GameController.instance.MapController.SelectedCardGameObject.Card.IsMaxLevel()
            && !(GameController.instance.MapController.SelectedCardGameObject.Card.Characteristics.Price
            [GameController.instance.MapController.SelectedCardGameObject.Card.GetLevel() + 1]
            <= GameController.instance.ResourcesController.Gold))
        {
            GameController.instance.LevelPanel.transform.GetChild(2).gameObject.SetActive(true);
        }
    }
}

