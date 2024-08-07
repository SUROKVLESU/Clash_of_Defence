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
        MapController = new MapController();
        RandomController = new RandomController();
        CollectionController.Initialization();
        InterfeceRandomCardsController = new InterfeceRandomCardsController();
        CardLevelController = new CardLevelController();
        ResourcesController = gameObject.GetComponent<ResourcesController>();
        SpawnEnemiesController = gameObject.AddComponent<SpawnEnemiesController>();
        EnemiesController = new();
        WaveController = new();
        StartHeightCamera = Camera.transform.position.y;
        ChangingNumberCards = new Action(() => { });
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
    public RandomController RandomController;
    public GameObject InterfeceHand;
    public GameObject ButtonNewMapBlock;
    public GameObject NewMapBlock;
    public GameObject Camera;
    public GameObject ZoomAndScroll;
    public GameObject InterfeceRandomCards;
    public GameObject LevelPanel;
    public GameObject DefeatInterfece;
    [SerializeField] public ButtonController ButtonController;
    public CollectionsController CollectionController;
}
