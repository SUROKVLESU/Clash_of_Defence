using System;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
        CardListController = new CardListController();
        MapController = new MapController();
        StartHeightCamera = Camera.transform.position.y;
        ChangingNumberCards = new Action(() => { });
        NewMapBlockButton.onClick.AddListener(() => { MapController.CreateButtonNewMapBlock(); });
        ReturnCardYourHandButton.onClick.AddListener(() => { MapController.ReturnCardYourHand(); });
        DeleteBuildingsButton.onClick.AddListener(() => { MapController.ReturnAllCardYourHand(); });
        ZoomAndScrollButton.onClick.AddListener(() => { OnClickZoomAndScrollButton(); });
    }
    public float StartHeightCamera;
    public Action ChangingNumberCards;
    public CardListController CardListController;
    public MapController MapController;
    public GameObject ButtonNewMapBlock;
    public GameObject NewMapBlock;
    public GameObject Camera;
    public GameObject ZoomAndScroll;

    [SerializeField] Button NewMapBlockButton;
    [SerializeField] Button ReturnCardYourHandButton;
    [SerializeField] Button DeleteBuildingsButton;
    [SerializeField] Button ZoomAndScrollButton;

    private void OnClickZoomAndScrollButton()
    {
        if (ZoomAndScroll.activeSelf)
        {
            ZoomAndScroll.SetActive(false);
        }
        else
        {
            ZoomAndScroll.SetActive(true);
        }
    }
}
