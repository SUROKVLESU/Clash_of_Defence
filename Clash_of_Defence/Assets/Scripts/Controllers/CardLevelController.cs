
public class CardLevelController
{
    public void LewelUpBuilding(BaseCard baseCard)
    {
        if (baseCard == null) return;
        GameController.instance.ResourcesController.UpdateGameResources
            (GameController.instance.MapController.SelectedCardGameObject.Card.PriceBuilding
            [GameController.instance.MapController.SelectedCardGameObject.Card.GetLevel() + 1],false);
        GameController.instance.MapController.LewelUpBuilding
            (GameController.instance.CollectionController.LevelUpBaseCard(baseCard));
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
        }
        if (onOff && !GameController.instance.MapController.SelectedCardGameObject.Card.IsMaxLevel()
            && GameController.instance.MapController.SelectedCardGameObject.Card.PriceBuilding
            [GameController.instance.MapController.SelectedCardGameObject.Card.GetLevel()+1]
            <= GameController.instance.ResourcesController.GameResources)
        {
            GameController.instance.LevelPanel.transform.GetChild(0).gameObject.SetActive(onOff);
            GameController.instance.LevelPanel.transform.GetChild(3).gameObject.SetActive(onOff);
            GameController.instance.LevelPanel.transform.GetChild(2).gameObject.SetActive(false);
            GameController.instance.ResourcesController.SetTextOnUpdate
                (GameController.instance.MapController.SelectedCardGameObject.Card.PriceBuilding
            [GameController.instance.MapController.SelectedCardGameObject.Card.GetLevel() + 1]);
            return;
        }
        if(onOff && !GameController.instance.MapController.SelectedCardGameObject.Card.IsMaxLevel()
            && !(GameController.instance.MapController.SelectedCardGameObject.Card.PriceBuilding
            [GameController.instance.MapController.SelectedCardGameObject.Card.GetLevel() + 1]
            <= GameController.instance.ResourcesController.GameResources))
        {
            GameController.instance.LevelPanel.transform.GetChild(2).gameObject.SetActive(true);
            GameController.instance.LevelPanel.transform.GetChild(3).gameObject.SetActive(true);
            GameController.instance.ResourcesController.SetTextOnUpdate
                (GameController.instance.MapController.SelectedCardGameObject.Card.PriceBuilding
            [GameController.instance.MapController.SelectedCardGameObject.Card.GetLevel() + 1]);
        }
    }
}

