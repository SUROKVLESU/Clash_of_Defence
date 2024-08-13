﻿
public class WaveController
{
    public int CurentMaxPowerCards;
    private const int MaxBoostCards = 3;
    public int CurentMaxPowerEnemies;
    private const int MaxBoostEnemies = 6;
    private const int MinBoostEnemies = 4;
    public bool IsGame = false;
    public bool IsPlayerDefeat = false;
    public int CountWave = 1;
    public void ResetBuildingsAndEnemies()
    {
        GameController.instance.MapController.ActivationBuildings();
        GameController.instance.EnemiesController.DestroyEnemies();
    }
    public void Defeat()
    {
        GameController.instance.DefeatInterfece.SetActive(true);
        GameController.instance.InterfeceHand.SetActive(false);
        GameController.instance.SaveGame.Save();
    }
    public void EnemiesDefeat()
    {
        GameController.instance.ButtonController.OnStartWaveButton();
        IsGame = false ;
        GameController.instance.MapController.GetResources();
        GameController.instance.WaveController.ResetBuildingsAndEnemies();
        GameController.instance.ButtonController.OnInterfeceHand();
        PowerBoost();
        GameController.instance.InterfeceRandomCardsController.GetRandomCards();
        CountWave++;
        PrintCountWave();
    }
    public void PlayerDefeat()
    {
        GameController.instance.ButtonController.OnStartWaveButton();
        IsGame = false ;
        GameController.instance.EnemiesController.DestroyEnemies();
        Defeat();
        GameController.instance.ResourcesController.SaveGameGold
            (GameController.instance.ResourcesController.GameResources);
        GameController.instance.ResourcesController.PrintDefeatGold();
    }
    public void PowerBoost()
    {

        CurentMaxPowerCards+= UnityEngine.Random.Range(1, MaxBoostCards+1);
        CurentMaxPowerEnemies += UnityEngine.Random.Range(1, MaxBoostEnemies + 1);
    }
    public void StartWave()
    {
        if (GameController.instance.MapController.ActiveCount == 0)
        {
            PlayerDefeat();
            return;
        }
        GameController.instance.ButtonController.OffStartWaveButton();
        IsGame= true;
        GameController.instance.MapController.CancellationSelected();
        GameController.instance.SpawnEnemiesController.SpawnEnemies
            (GameController.instance.RandomController.GetRandomEnemies(GameController.instance.WaveController.CurentMaxPowerEnemies));
        GameController.instance.ButtonController.OffInterfeceHand();
    }
    public void Initialization()
    {
        CurentMaxPowerCards = UnityEngine.Random.Range(2, 18);
        CurentMaxPowerEnemies =UnityEngine.Random.Range(2, 18);
        IsPlayerDefeat = false;
        IsPlayerDefeat = false;
        CountWave = 1;
        PrintCountWave();
    }
    public void OnMenuInterfece()
    {
        GameController.instance.MenuInterfece.SetActive(true);
        GameController.instance.ButtonController.OnMenuInterfece();
    }
    public void PrintCountWave()
    {
        GameController.instance.CountWaveText.text = CountWave.ToString();
    }
}

