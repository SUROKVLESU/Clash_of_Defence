using UnityEngine;

public class CardLevelController
{
    public void LewelUpBuilding(BaseCard baseCard)
    {
        if (baseCard == null) return;
        GameController.instance.MapController.LewelUpBuilding
            (GameController.instance.CollectionCardsController.LevelUpBaseCard(baseCard));
        GameController.instance.MapController.CancellationSelected();
    }
    public void OnOffUpdeteMenu(bool onOff)
    {
        GameController.instance.LevelPanel.SetActive(onOff);
    }
}

