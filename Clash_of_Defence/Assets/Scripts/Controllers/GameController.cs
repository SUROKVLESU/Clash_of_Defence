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
        ChangingNumberCards = new Action(() => { });
        NewMapBlockButton.onClick.AddListener(() => { MapController.CreateButtonNewMapBlock(); });
    }
    public Action ChangingNumberCards;
    public CardListController CardListController;
    public MapController MapController;
    public GameObject ButtonNewMapBlock;
    public GameObject NewMapBlock;

    public Button NewMapBlockButton;
}
