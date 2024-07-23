using UnityEngine.UI;
using UnityEngine;
using System;

public class ListBuildingsHand : MonoBehaviour
{
    private RectTransform[] RectTransformCardsBuilding;
    private const float ScaleHeight = 192;
    //private const float ScaleWidth = 960;
    private const float CardScrolling = 20;
    private const float Sensitivity = 6;
    private float FrontierLCard;
    private float FrontierRCard;
    private Vector2 CreatePosition;
    private RectTransform Hand;
    private BoxCollider Collider;
    private Vector3 OldMousePosition;
    private int IndexSelectedCard;
    private void Awake()
    {
        Hand = gameObject.GetComponent<RectTransform>();
        Collider = gameObject.GetComponent<BoxCollider>();
        CreatePosition = new Vector2
            (-Hand.rect.width/2+Hand.rect.height/2, 0f);
        Collider.size = new Vector3(Hand.rect.width, Hand.rect.height,0);
        RectTransformCardsBuilding = new RectTransform[0];
        FrontierLCard = -Hand.rect.width / 2 + Hand.rect.height / 2;
        FrontierRCard = -FrontierLCard;
    }
    public void AddCard(GameObject obj)
    {
        if (RectTransformCardsBuilding.Length == 0)
        {
            RectTransformCardsBuilding = new RectTransform[]{ Instantiate(obj, Hand).GetComponent<RectTransform>()};
            ScaleImage(RectTransformCardsBuilding[0]);
            RectTransformCardsBuilding[0].localPosition = CreatePosition;
            CreatePosition = new Vector2(CreatePosition.x+ RectTransformCardsBuilding[0].rect.width,0);
        }
        else
        {
            RectTransform[] arrRect = new RectTransform[RectTransformCardsBuilding.Length+1];
            for(int i = 0; i < RectTransformCardsBuilding.Length; i++)
            {
                arrRect[i] = RectTransformCardsBuilding[i];
            }
            arrRect[RectTransformCardsBuilding.Length] = Instantiate(obj, Hand).GetComponent<RectTransform>();
            RectTransformCardsBuilding = arrRect;
            ScaleImage(RectTransformCardsBuilding[RectTransformCardsBuilding.Length - 1]);
            RectTransformCardsBuilding[RectTransformCardsBuilding.Length - 1].localPosition = CreatePosition;
            CreatePosition = new Vector2(CreatePosition.x + RectTransformCardsBuilding[RectTransformCardsBuilding.Length - 1].rect.width, 0);
        }
    }
    private void OnMouseDrag()
    {
        if(Input.mousePosition.y>Hand.rect.height)
        {
            if (RectTransformCardsBuilding.Length == 0)
            { return; }
            RectTransformCardsBuilding[IndexSelectedCard].localPosition = new Vector3
                (Input.mousePosition.x- Screen.currentResolution.width/2,Input.mousePosition.y,0);
            return;
        }
        if (Math.Abs(OldMousePosition.x-Input.mousePosition.x)<= Sensitivity
            || Math.Abs(OldMousePosition.x - Input.mousePosition.x) >= CardScrolling)
        {
            OldMousePosition = Input.mousePosition;
            return;
        }
        float MousePositionX = Input.mousePosition.x-OldMousePosition.x;
        if (MousePositionX > CardScrolling)
        {
            MousePositionX = CardScrolling;
        }
        else if (MousePositionX < -CardScrolling)
        {
            MousePositionX = -CardScrolling;
        }
        if (RectTransformCardsBuilding.Length>=1&& RectTransformCardsBuilding[0].localPosition.x<=FrontierLCard
            && RectTransformCardsBuilding[RectTransformCardsBuilding.Length-1].localPosition.x>=FrontierRCard)
        { 
            for (int i = 0; i < RectTransformCardsBuilding.Length; i++)
            {
                RectTransformCardsBuilding[i].localPosition = new Vector3
                    (RectTransformCardsBuilding[i].localPosition.x + MousePositionX, 0, 0);
            }
            if (RectTransformCardsBuilding[0].localPosition.x > FrontierLCard)
            {
                RectTransformCardsBuilding[0].localPosition = new Vector3(FrontierLCard,0,0);
                for (int i = 1;i < RectTransformCardsBuilding.Length;i++)
                {
                    RectTransformCardsBuilding[i].localPosition = new Vector3(FrontierLCard + Hand.rect.height*i,0,0);
                }
            }
            if (RectTransformCardsBuilding[RectTransformCardsBuilding.Length-1].localPosition.x < FrontierRCard)
            {
                RectTransformCardsBuilding[RectTransformCardsBuilding.Length - 1].localPosition = 
                    new Vector3(FrontierRCard, 0, 0);
                for (int i = RectTransformCardsBuilding.Length-2,j=1; i >=0; i--,j++)
                {
                    RectTransformCardsBuilding[i].localPosition = 
                        new Vector3(FrontierRCard - Hand.rect.height * j, 0, 0);
                }
            }
            CreatePosition = new Vector2
                (RectTransformCardsBuilding[RectTransformCardsBuilding.Length - 1].localPosition.x
                + Hand.rect.height, 0);
        }
        OldMousePosition = Input.mousePosition;
    }
    private void OnMouseDown()
    {
        if (RectTransformCardsBuilding.Length == 0)
        { return; }
        for (int i = 0; i < RectTransformCardsBuilding.Length; i++)
        {
            if (Math.Abs( Input.mousePosition.x- Screen.currentResolution.width / 2
                - RectTransformCardsBuilding[i].localPosition.x) < Hand.rect.height/2)
            {
                IndexSelectedCard = i;
                break;
            }

        }
    }
    private void ScaleImage(RectTransform rect)
    {
        rect.SetSizeWithCurrentAnchors
            (RectTransform.Axis.Horizontal,Hand.rect.height);
        rect.SetSizeWithCurrentAnchors
            (RectTransform.Axis.Vertical, Hand.rect.height);
    }
}
