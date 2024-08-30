using System.Collections.Generic;
using UnityEngine;

public class ShopInterfece:MonoBehaviour
{
    private Vector3 OldMousePosition;
    private RectTransform TransformCardsPanel;
    //[SerializeField] RectTransform ParentPanel;
    private List<BaseCard> Cards;
    private List<RectTransform> RectTransformCards=new();
    [SerializeField] GameObject CardShopPrefab;
    private RectTransform PrefabRectTransform;
    private const int CountCardsRow = 7;
    private const float CountCardsHeight = 2;
    private void OnMouseDown()
    {
        OldMousePosition = Input.mousePosition;
    }
    private void OnMouseDrag()
    {
        TransformCardsPanel.localPosition = new Vector3
            (0, TransformCardsPanel.localPosition.y+(Input.mousePosition.y-OldMousePosition.y));
        if (TransformCardsPanel.localPosition.y < 0) TransformCardsPanel.localPosition = new Vector3();
        if (RectTransformCards.Count / (float)CountCardsRow > CountCardsHeight
            && TransformCardsPanel.localPosition.y > -(RectTransformCards[RectTransformCards.Count - 1].localPosition.y
            - PrefabRectTransform.rect.height/2+TransformCardsPanel.rect.height/2))
        {
            TransformCardsPanel.localPosition = new Vector3
                (0, -(RectTransformCards[RectTransformCards.Count - 1].localPosition.y
            - PrefabRectTransform.rect.height / 2 + TransformCardsPanel.rect.height / 2),0);
        }
        //else TransformCardsPanel.localPosition = new Vector3();
        OldMousePosition = Input.mousePosition;
    }
    private void OnMouseUp()
    {

    }
    private void Awake()
    {
        TransformCardsPanel = (RectTransform)transform.GetChild(0);
        Cards = GameController.instance.CollectionController.GetAllCards();
        PrefabRectTransform = CardShopPrefab.GetComponent<RectTransform>();
    }
    private void CreateArrayCard()
    {
        Vector2 newPositionCard = new Vector2
            (-TransformCardsPanel.rect.width/2+ PrefabRectTransform.rect.width/2,
            TransformCardsPanel.rect.height / 2 - PrefabRectTransform.rect.height / 2);
        int countCreateCard =CountCardsRow;
        for (int i = 0; i < Cards.Count; i++)
        {
            RectTransformCards.Add(Instantiate(CardShopPrefab, TransformCardsPanel).GetComponent<RectTransform>());
            RectTransformCards[i].localPosition = newPositionCard;
            RectTransformCards[i].GetChild(1).GetComponent<ShopLevelUpButton>().Initialization(Cards[i]);
            countCreateCard--;
            if (countCreateCard == 0)
            {
                newPositionCard = new Vector2(RectTransformCards[0].localPosition.x, 
                    RectTransformCards[i].localPosition.y-PrefabRectTransform.rect.height);
                countCreateCard = CountCardsRow;
            }
            else
            {
                newPositionCard += new Vector2(PrefabRectTransform.rect.width,0);
            }
        }
        CardShopPrefab.SetActive(false);
    }
    public void ResetShop()
    {
        TransformCardsPanel.localPosition = new Vector2();
        for (int i = 0; i < RectTransformCards.Count; i++)
        {
            Destroy(RectTransformCards[i].gameObject);
        }
        RectTransformCards.Clear();
        CardShopPrefab.SetActive(true);
        CreateArrayCard();
    }
}

