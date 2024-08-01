using System;
using UnityEngine;
using UnityEngine.UI;
public class ButtonController:MonoBehaviour
{
    [Header("Button Interfece Hand")]
    [SerializeField] Button NewMapBlockButton;
    [SerializeField] Button ReturnCardYourHandButton;
    [SerializeField] Button DeleteBuildingsButton;
    [SerializeField] Button ZoomAndScrollButton;
    [SerializeField] Button LevelUpBuildingButton;

    [Header("InterfeceRandomCards")]
    [SerializeField] Button OkRandomCardsButton;
    [SerializeField] Button ReverseRandomCardsButton;
    private void Awake()
    {
        NewMapBlockButton.onClick.AddListener(() => { GameController.instance.MapController.CreateButtonNewMapBlock(); });
        ReturnCardYourHandButton.onClick.AddListener(() => { GameController.instance.MapController.ReturnCardYourHand(); });
        DeleteBuildingsButton.onClick.AddListener(() => { GameController.instance.MapController.ReturnAllCardYourHand(); });
        ZoomAndScrollButton.onClick.AddListener(() => { OnClickZoomAndScrollButton(); });
        ReverseRandomCardsButton.onClick.AddListener(() => {
            GameController.instance.InterfeceRandomCardsController.ReceiveRandomCards
                (GameController.instance.RandomCardsController.GetRandomCards(UnityEngine.Random.Range(2, 18)));
        });
        OkRandomCardsButton.onClick.AddListener(() => { GameController.instance.InterfeceRandomCardsController.AddCards(); });
        LevelUpBuildingButton.onClick.AddListener(() => {
            GameController.instance.CardLevelController.LewelUpBuilding
                (GameController.instance.MapController.SelectedCardGameObject?.Card);
        });
    }
    private void OnClickZoomAndScrollButton()
    {
        if (GameController.instance.ZoomAndScroll.activeSelf)
        {
            GameController.instance.ZoomAndScroll.SetActive(false);
        }
        else
        {
            GameController.instance.MapController.CancellationSelected();
            GameController.instance.ZoomAndScroll.SetActive(true);
        }
    }
    //public void 
}

