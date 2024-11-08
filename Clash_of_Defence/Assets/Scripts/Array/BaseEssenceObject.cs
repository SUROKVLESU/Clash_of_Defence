﻿using UnityEngine;
[CreateAssetMenu(fileName = "BaseEnemy", menuName = "Enemy/BaseEnemy")]
public class BaseEssenceObject: ScriptableObject
{
    public int Id;
    public string Name;
    private int Level = 1;
    public int MaxLevel
    {
        get
        {
            return GameObjects.Length;
        }
    }
    [SerializeField] int CurrentMaxLevel;//Это не трогать
    public int Power;
    public float ProbabilityFalling;
    public GameObject[] GameObjects;
    public void LevelUp()
    {
        Level++;
        if (Level > CurrentMaxLevel)
        {
            Level = CurrentMaxLevel;
        }
    }
    public void MaxLevelUp()
    {
        CurrentMaxLevel++;
        if (CurrentMaxLevel > MaxLevel)
        {
            CurrentMaxLevel = MaxLevel;
        }
    }
    public void SetLevel(int levelCard)
    {
        if (levelCard > 0 && levelCard <= MaxLevel)
        {
            this.Level = levelCard;
        }
    }
    public int GetLevel()
    {
        return Level - 1;
    }
    public int GetCurrentMaxLevel()
    {
        return CurrentMaxLevel;
    }
    public bool SetCurrentMaxLevel(int currentMaxLevelCard)
    {
        if (currentMaxLevelCard >= 0 && currentMaxLevelCard <= MaxLevel)
        {
            this.CurrentMaxLevel = currentMaxLevelCard;
            return true;
        }
        else return false;
    }
    public void LoadCurentMaxLevel(int currentMaxLevelCard)
    {
        if (currentMaxLevelCard >= 0 && currentMaxLevelCard <= MaxLevel)
        {
            this.CurrentMaxLevel = currentMaxLevelCard;
        }
    }
    public void ResetLevel()
    {
        Level = 1;
    }
    public bool IsMaxLevel()
    {
        if (Level + 1 > CurrentMaxLevel) return true;
        return false;
    }
}
