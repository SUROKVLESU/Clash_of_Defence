using System;
using UnityEngine;

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
        MapController = gameObject.AddComponent<MapController>();
        RandomController = new RandomController();
        CollectionController.Initialization();
        InterfeceRandomCardsController = gameObject.AddComponent<InterfeceRandomCardsController>();
        CardLevelController = new CardLevelController();
        ResourcesController = gameObject.GetComponent<ResourcesController>();
        SpawnEnemiesController = gameObject.AddComponent<SpawnEnemiesController>();
        ButtonController = gameObject.GetComponent<ButtonController>();
        EnemiesController = gameObject.AddComponent<EnemiesController> ();
        WaveController = new();
        StartHeightCamera = Camera.transform.position.y;
        ChangingNumberCards = new Action(() => { });
    }
    private void Start()
    {
        ButtonController.OnMenuInterfece();
    }
    [HideInInspector] public float StartHeightCamera;
    public Action ChangingNumberCards;
    public CardListController CardListController;
    [HideInInspector] public MapController MapController;
    [HideInInspector] public InterfeceRandomCardsController InterfeceRandomCardsController;
    [HideInInspector] public CardLevelController CardLevelController;
    [HideInInspector] public ResourcesController ResourcesController;
    [HideInInspector] public SpawnEnemiesController SpawnEnemiesController;
    [HideInInspector] public EnemiesController EnemiesController;
    [HideInInspector] public WaveController WaveController;
    [HideInInspector] public ButtonController ButtonController;
    public RandomController RandomController;
    public GameObject InterfeceHand;
    public GameObject ButtonNewMapBlock;
    public GameObject NewMapBlock;
    public GameObject Camera;
    public GameObject ZoomAndScroll;
    public GameObject InterfeceRandomCards;
    public GameObject LevelPanel;
    public GameObject DefeatInterfece;
    public GameObject MenuInterfece;
    public CollectionsController CollectionController;
    public void StartGame()
    {
        WaveController.Initialization();
        InterfeceRandomCardsController.GetRandomCards();
    }
}
