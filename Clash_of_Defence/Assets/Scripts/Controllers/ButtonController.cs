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
    [SerializeField] BoxCollider HandBoxCollider;
    [SerializeField] Button StartWaveButton;

    [Header("InterfeceRandomCards")]
    [SerializeField] Button OkRandomCardsButton;
    [SerializeField] Button ReverseRandomCardsButton;
    [Header("Pause")]
    [SerializeField] Button PauseButton;
    [SerializeField] GameObject OnPauseButton;
    [SerializeField] GameObject OffPauseButton;
    [Header("DefeatInterfeceButton")]
    [SerializeField] Button MenuButton;
    [SerializeField] Button RestartButton;
    [SerializeField] Button NewLifeButton;
    [SerializeField] Button MultiplyGoldButton;
    private void Awake()
    {
        NewMapBlockButton.onClick.AddListener(() => { GameController.instance.MapController.CreateButtonNewMapBlock(); });
        ReturnCardYourHandButton.onClick.AddListener(() => { GameController.instance.MapController.ReturnCardYourHand(); });
        DeleteBuildingsButton.onClick.AddListener(() => { GameController.instance.MapController.ReturnAllCardYourHand(); });
        ZoomAndScrollButton.onClick.AddListener(() => { OnClickZoomAndScrollButton(); });
        ReverseRandomCardsButton.onClick.AddListener(() => {
            GameController.instance.InterfeceRandomCardsController.ReceiveRandomCards
                (GameController.instance.RandomController.GetRandomCards(UnityEngine.Random.Range(2, 18)));
        });
        OkRandomCardsButton.onClick.AddListener(() => { 
            GameController.instance.InterfeceRandomCardsController.AddCards();
            OnInterfeceHand();
        });
        LevelUpBuildingButton.onClick.AddListener(() => {
            GameController.instance.CardLevelController.LewelUpBuilding
                (GameController.instance.MapController.SelectedCardGameObject?.Card);
        });
        PauseButton.onClick.AddListener(() =>
        {
            if(!GameController.instance.WaveController.IsGame) return;
            if (OnPauseButton.activeSelf)
            {
                OnPauseButton.SetActive(false);
                OffPauseButton.SetActive(true);
                GameController.instance.MapController.Pause();
                GameController.instance.EnemiesController.Pause();
                GameController.instance.SpawnEnemiesController.Pause();
            }
            else
            {
                OnPauseButton.SetActive(true);
                OffPauseButton.SetActive(false);
                GameController.instance.MapController.OffPause();
                GameController.instance.EnemiesController.OffPause();
                GameController.instance.SpawnEnemiesController.OffPause();
            }
        });
        StartWaveButton.onClick.AddListener(() => { GameController.instance.WaveController.StartWave(); });
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
    public void OffInterfeceHand()
    {
        HandBoxCollider.isTrigger = false;
        NewMapBlockButton.enabled = false;
        ReturnCardYourHandButton.enabled = false;
        DeleteBuildingsButton.enabled = false;
        ZoomAndScrollButton.enabled = false;
        GameController.instance.MapController.CancellationSelected();
        for (int i = 0; i < GameController.instance.LevelPanel.transform.childCount; i++)
        {
            GameController.instance.LevelPanel.transform.GetChild(i).gameObject.SetActive(false);
        }
        GameController.instance.ZoomAndScroll.SetActive(true);
    }
    public void OnInterfeceHand()
    {
        HandBoxCollider.isTrigger = true;
        NewMapBlockButton.enabled = true;
        ReturnCardYourHandButton.enabled = true;
        DeleteBuildingsButton.enabled = true;
        ZoomAndScrollButton.enabled = true;
        GameController.instance.ZoomAndScroll.SetActive(false);
    }
    public void GetRandomCards()
    {
        OffInterfeceHand();
        GameController.instance.ZoomAndScroll.SetActive(false);
    }
    public void OnStartWaveButton()
    {
        StartWaveButton.enabled = true;
    }
    public void OffStartWaveButton()
    {
        StartWaveButton.enabled = false;
    }
}

