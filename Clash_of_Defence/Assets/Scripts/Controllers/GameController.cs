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
        RandomCardsController = new RandomCardsController();
        CollectionCardsController.Initialization();
        InterfeceRandomCardsController = new InterfeceRandomCardsController();
        //InterfeceRandomCardsController.Initialization();
        StartHeightCamera = Camera.transform.position.y;
        ChangingNumberCards = new Action(() => { });
        NewMapBlockButton.onClick.AddListener(() => { MapController.CreateButtonNewMapBlock(); });
        ReturnCardYourHandButton.onClick.AddListener(() => { MapController.ReturnCardYourHand(); });
        DeleteBuildingsButton.onClick.AddListener(() => { MapController.ReturnAllCardYourHand(); });
        ZoomAndScrollButton.onClick.AddListener(() => { OnClickZoomAndScrollButton(); });
        ReverseRandomCardsButton.onClick.AddListener(() => {InterfeceRandomCardsController.ReceiveRandomCards
                (RandomCardsController.GetRandomCards(UnityEngine.Random.Range(2, 18)));});
        OkRandomCardsButton.onClick.AddListener(() => { InterfeceRandomCardsController.AddCards(); });
    }
    [HideInInspector] public float StartHeightCamera;
    public Action ChangingNumberCards;
    public CardListController CardListController;
    [HideInInspector] public MapController MapController;
    [HideInInspector] public InterfeceRandomCardsController InterfeceRandomCardsController;
    public RandomCardsController RandomCardsController;
    public GameObject ButtonNewMapBlock;
    public GameObject NewMapBlock;
    public GameObject Camera;
    public GameObject ZoomAndScroll;
    public GameObject InterfeceRandomCards;

    [Header("Cards")]
    public CollectionCardsController CollectionCardsController;

    [Header("Button Interfece Hand")]
    [SerializeField] Button NewMapBlockButton;
    [SerializeField] Button ReturnCardYourHandButton;
    [SerializeField] Button DeleteBuildingsButton;
    [SerializeField] Button ZoomAndScrollButton;

    [Header("InterfeceRandomCards")]
    [SerializeField] Button OkRandomCardsButton;
    [SerializeField] Button ReverseRandomCardsButton;

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
