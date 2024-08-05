using System;
using UnityEditor.SceneManagement;
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
        RandomController = new RandomController();
        CollectionCardsController.Initialization();
        InterfeceRandomCardsController = new InterfeceRandomCardsController();
        CardLevelController = new CardLevelController();
        ResourcesController = new();
        SpawnEnemiesController = gameObject.AddComponent<SpawnEnemiesController>();
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
    public RandomController RandomController;
    public GameObject ButtonNewMapBlock;
    public GameObject NewMapBlock;
    public GameObject Camera;
    public GameObject ZoomAndScroll;
    public GameObject InterfeceRandomCards;
    public GameObject LevelPanel;
    [SerializeField] public ButtonController ButtonController;
    public CollectionCardsController CollectionCardsController;
}
