using UnityEngine;
using UnityEngine.SceneManagement;
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
    [Header("ShopInterfece")]
    [SerializeField] Button ExitShopButton;
    private void Awake()
    {
        NewMapBlockButton.onClick.AddListener(() => { GameController.instance.MapController.CreateButtonNewMapBlock(); });
        ReturnCardYourHandButton.onClick.AddListener(() => { GameController.instance.MapController.ReturnCardYourHand(); });
        DeleteBuildingsButton.onClick.AddListener(() => { GameController.instance.MapController.ReturnAllCardYourHand(); });
        ZoomAndScrollButton.onClick.AddListener(() => { OnClickZoomAndScrollButton(); });
        ReverseRandomCardsButton.onClick.AddListener(() => {
            GameController.instance.InterfeceRandomCardsController.ReceiveRandomCards
                (GameController.instance.RandomController.GetRandomCards
                (GameController.instance.WaveController.CurentMaxPowerCards));
        });
        OkRandomCardsButton.onClick.AddListener(() => { 
            GameController.instance.InterfeceRandomCardsController.AddCards();});
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
                GameController.instance.IsPause = true;
            }
            else
            {
                OnPauseButton.SetActive(true);
                OffPauseButton.SetActive(false);
                GameController.instance.MapController.OffPause();
                GameController.instance.EnemiesController.OffPause();
                GameController.instance.SpawnEnemiesController.OffPause();
                GameController.instance.IsPause = false;
            }
        });
        StartWaveButton.onClick.AddListener(() => { GameController.instance.WaveController.StartWave(); });
        MenuButton.onClick.AddListener(() => { GameController.instance.WaveController.OnMenuInterfece(); });
        RestartButton.onClick.AddListener(() => { RestartGame(); });
        MultiplyGoldButton.onClick.AddListener(() => { GameController.instance.ResourcesController.MultiplierResources(); });
        NewLifeButton.onClick.AddListener(() => { NewLife(); });
        ExitShopButton.onClick.AddListener(() => { ExitShop(); });
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
        HandBoxCollider.enabled = false;
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
        HandBoxCollider.enabled = true;
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
    public void OnMenuInterfece()
    {
        GameController.instance.InterfeceHand.SetActive(false);
        GameController.instance.MenuInterfece.SetActive(true);
    }
    public void RestartGame()
    {
        GameController.instance.InterfeceHand.SetActive(true);
        GameController.instance.MenuInterfece.SetActive(false);
        GameController.instance.DefeatInterfece.SetActive(false);
        GameController.instance.WaveController.Initialization();
        GameController.instance.ResourcesController.ResetController();
        GameController.instance.MapController.ResetController();
        GameController.instance.CollectionController.Initialization();
        GameController.instance.CardListController.ResetController();
        GameController.instance.Camera.transform.position = new Vector3(0,15,-12);
        GameController.instance.StartGame();
    }
    private void NewLife()
    {
        if (GameController.instance.WaveController.IsPlayerDefeat) return;
        GameController.instance.WaveController.IsPlayerDefeat = true;
        GameController.instance.InterfeceRandomCardsController.GetRandomCards();
        GameController.instance.ResourcesController.SaveGameGold
            (GameController.instance.ResourcesController.GameResources * (-1));
        GameController.instance.InterfeceHand.SetActive (true);
        GameController.instance.DefeatInterfece.SetActive (false);
        GameController.instance.WaveController.ResetBuildingsAndEnemies();
    }
    private void ExitShop()
    {
        GameController.instance.ShopInterfeceObject.SetActive (false);
        GameController.instance.SaveGame.Save();
    }
}

