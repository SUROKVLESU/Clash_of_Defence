using UnityEngine.UI;
using UnityEngine;
using System;

public class ListBuildingsHand : MonoBehaviour
{
    private RectTransform[] RectTransformCards;
    private const float ScaleHeight = 192;
    //private const float ScaleWidth = 960;
    private float CardScrolling = 20;
    private float Sensitivity = 6;
    private int NumberVisibleCards;
    private float FrontierLCard;
    private float FrontierRCard;
    private Vector2 CreatePosition;
    private RectTransform Hand;
    private BoxCollider Collider;
    private Vector3 OldMousePosition;
    private int IndexSelectedCard;
    private bool IsCardUsed = true;
    private void Awake()
    {
        Hand = gameObject.GetComponent<RectTransform>();
        Collider = gameObject.GetComponent<BoxCollider>();
        CreatePosition = new Vector2
            (-Hand.rect.width/2+Hand.rect.height/2, 0f);
        Collider.size = new Vector3(Hand.rect.width, Hand.rect.height,0);
        RectTransformCards = new RectTransform[0];
        FrontierLCard = -Hand.rect.width / 2 + Hand.rect.height / 2;
        FrontierRCard = -FrontierLCard;
        NumberVisibleCards = (int)Math.Ceiling(Hand.rect.width/Hand.rect.height-1);
        CardScrolling = CardScrolling * (Screen.currentResolution.width/1920);
        Sensitivity = Sensitivity * ((Screen.currentResolution.width / 1920));
    }
    private void Start()
    {
        GameController.instance.ChangingNumberCards += ChangingNumberCards;
    }
    private RectTransform AddCard(GameObject obj)
    {
        RectTransform rect = Instantiate(obj, Hand).GetComponent<RectTransform>();
        ScaleImage(rect);
        rect.localPosition = CreatePosition;
        CreatePosition = new Vector2(CreatePosition.x + rect.rect.width, 0);
        return rect;
    }
    private void OnMouseDrag()
    {
        if(Input.mousePosition.y>Hand.rect.height)
        {
            if (RectTransformCards.Length == 0)
            { return; }
            if(GameController.instance.MapController.IsFreeCell()&& IsCardUsed)
            {
                IsCardUsed = false;
                GameController.instance.MapController.AddBuilding
                    (GameController.instance.CardListController.BaseCardsHand[IndexSelectedCard].BuildingFromCard);
                GameController.instance.CardListController.UseTheCard
                    (GameController.instance.CardListController.BaseCardsHand[IndexSelectedCard]);
            }
            return;
        }
        /*if (Math.Abs(OldMousePosition.x-Input.mousePosition.x)<= Sensitivity
            || Math.Abs(OldMousePosition.x - Input.mousePosition.x) >= CardScrolling)
        {
            OldMousePosition = Input.mousePosition;
            return;
        }*/
        float MousePositionX = Input.mousePosition.x-OldMousePosition.x;
        /*if (MousePositionX > CardScrolling)
        {
            MousePositionX = CardScrolling;
        }
        else if (MousePositionX < -CardScrolling)
        {
            MousePositionX = -CardScrolling;
        }*/
        if (RectTransformCards.Length>=1&& RectTransformCards[0].localPosition.x<=FrontierLCard
            && RectTransformCards[RectTransformCards.Length-1].localPosition.x>=FrontierRCard)
        { 
            for (int i = 0; i < RectTransformCards.Length; i++)
            {
                RectTransformCards[i].localPosition = new Vector3
                    (RectTransformCards[i].localPosition.x + MousePositionX, 0, 0);
            }
            if (RectTransformCards[0].localPosition.x > FrontierLCard)
            {
                RectTransformCards[0].localPosition = new Vector3(FrontierLCard,0,0);
                for (int i = 1;i < RectTransformCards.Length;i++)
                {
                    RectTransformCards[i].localPosition = new Vector3(FrontierLCard + Hand.rect.height*i,0,0);
                }
            }
            if (RectTransformCards[RectTransformCards.Length-1].localPosition.x < FrontierRCard)
            {
                RectTransformCards[RectTransformCards.Length - 1].localPosition = 
                    new Vector3(FrontierRCard, 0, 0);
                for (int i = RectTransformCards.Length-2,j=1; i >=0; i--,j++)
                {
                    RectTransformCards[i].localPosition = 
                        new Vector3(FrontierRCard - Hand.rect.height * j, 0, 0);
                }
            }
            CreatePosition = new Vector2
                (RectTransformCards[RectTransformCards.Length - 1].localPosition.x
                + Hand.rect.height, 0);
        }
        OldMousePosition = Input.mousePosition;
    }
    private void OnMouseDown()
    {
        OldMousePosition = Input.mousePosition;
        if (RectTransformCards.Length == 0)
        { return; }
        for (int i = 0; i < RectTransformCards.Length; i++)
        {
            if (Math.Abs( Input.mousePosition.x- Screen.currentResolution.width / 2
                - RectTransformCards[i].localPosition.x) < Hand.rect.height/2)
            {
                IndexSelectedCard = i;
                break;
            }

        }
    }
    private void OnMouseUp()
    {
        IsCardUsed = true;
    }
    private void ScaleImage(RectTransform rect)
    {
        rect.SetSizeWithCurrentAnchors
            (RectTransform.Axis.Horizontal,Hand.rect.height);
        rect.SetSizeWithCurrentAnchors
            (RectTransform.Axis.Vertical, Hand.rect.height);
    }
    private void ChangingNumberCards()
    {
        if(GameController.instance.CardListController.BaseCardsHand.Length > NumberVisibleCards)
        {
            if (RectTransformCards .Length>0&& RectTransformCards[RectTransformCards.Length - 1].localPosition.x - Hand.rect.height < FrontierRCard)
            {
                CreatePosition = new Vector2
                    (FrontierRCard-Hand.rect.height* (GameController.instance.CardListController.BaseCardsHand.Length-1), 0);
            }
            else if(RectTransformCards.Length > 0)
            {
                CreatePosition = new Vector2(RectTransformCards[0].localPosition.x, 0);
            }
            else
            {
                CreatePosition = new Vector2
            (-Hand.rect.width / 2 + Hand.rect.height / 2, 0f);
            }
            for (int i = 0;i < RectTransformCards.Length;i++)
            {
                Destroy(RectTransformCards[i].gameObject);
            }
            RectTransformCards = new RectTransform[GameController.instance.CardListController.BaseCardsHand.Length];
            for (int i = 0;i<RectTransformCards.Length;i++)
            {
                RectTransformCards[i] = AddCard(GameController.instance.CardListController.BaseCardsHand[i].CardCover);
            }
        }
        else
        {
            CreatePosition = new Vector2(-Hand.rect.width / 2 + Hand.rect.height / 2, 0f);
            if (RectTransformCards.Length > 0)
            {
                for (int i = 0; i < RectTransformCards.Length; i++)
                {
                    Destroy(RectTransformCards[i].gameObject);
                }
            }
            RectTransformCards = new RectTransform[GameController.instance.CardListController.BaseCardsHand.Length];
            for (int i = 0; i < RectTransformCards.Length; i++)
            {
                RectTransformCards[i] = AddCard(GameController.instance.CardListController.BaseCardsHand[i].CardCover);
            }
        }
    }
}
