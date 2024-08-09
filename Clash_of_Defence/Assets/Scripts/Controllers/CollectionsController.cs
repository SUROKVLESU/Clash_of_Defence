using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class CollectionsController
{
    [Header("Cards")]
    [SerializeField] EssenceObjectPower[] AllCardsPower;
    [HideInInspector] public EssenceObjectPower[] UnlockedCardsPower;
    [Header("Enemy")]
    [SerializeField] EssenceObjectPower[] AllEnemiesPower;
    [HideInInspector] public EssenceObjectPower[] UnlockedEnemiesPower;
    private int MaxPowerCard;
    private int MaxPowerEnemy;
    public void Initialization()
    {
        int maxPowerCard = 1;
        int maxPowerEnemy = 1;
        for (int i = 0; i < UnlockedCardsPower.Length; i++)
        {
            if (UnlockedCardsPower[i].Power > maxPowerCard)
            {
                maxPowerCard = UnlockedCardsPower[i].Power;
            }
        }
        MaxPowerCard = maxPowerCard;
        for (int i = 0; i < UnlockedEnemiesPower.Length; i++)
        {
            if (UnlockedEnemiesPower[i].Power > maxPowerCard)
            {
                maxPowerCard = UnlockedEnemiesPower[i].Power;
            }
        }
        MaxPowerEnemy = maxPowerEnemy;
        Reset();
    }
    public BaseEssenceObject GetRandomCard(int power)
    {
        if (power > MaxPowerCard) { power = MaxPowerCard; }
        power = CheckingAvailabilityCardsSelectedPower(power);
        float maxProbability;
        for (int i = 0; i < UnlockedCardsPower.Length; i++)
        {
            if (UnlockedCardsPower[i].Power == power)
            {
                maxProbability = MaxProbability(UnlockedCardsPower[i]);
                return UnlockedCardsPower[i]
                    [GetIndexRandomCard(UnlockedCardsPower[i], UnityEngine.Random.Range(0f,maxProbability))];
            }
        }
        return null;
        float MaxProbability(EssenceObjectPower cards)
        {
            float maxProbability=0;
            for (int i = 0; i < cards.Length; i++)
            {
                maxProbability += cards[i].ProbabilityFalling;
            }
            return maxProbability;
        }
        int GetIndexRandomCard(EssenceObjectPower cards, float probability)
        {
            float currentProbability= 0;
            float newProbability = currentProbability + cards[0].ProbabilityFalling;
            for (int i = 0; i < cards.Length; i++)
            {
                if (probability >= currentProbability && probability <= newProbability)
                {
                    return i;
                }
                else
                {
                    currentProbability = newProbability;
                    newProbability += cards[i+1].ProbabilityFalling;
                }
            }
            return 0;
        }
        int CheckingAvailabilityCardsSelectedPower(int power)
        {
            while (true)
            {
                for (int i = 0; i < UnlockedCardsPower.Length; i++)
                {
                    if (UnlockedCardsPower[i].Power == power)
                    {
                        return power;
                    }
                }
                power--;
            }
        }
    }
    public BaseEssenceObject GetRandomEnemy(int power)
    {
        if (power > MaxPowerEnemy) { power = MaxPowerEnemy; }
        power = CheckingAvailabilityCardsSelectedPower(power);
        float maxProbability;
        for (int i = 0; i < UnlockedEnemiesPower.Length; i++)
        {
            if (UnlockedEnemiesPower[i].Power == power)
            {
                maxProbability = MaxProbability(UnlockedEnemiesPower[i]);
                return UnlockedEnemiesPower[i]
                    [GetIndexRandomCard(UnlockedEnemiesPower[i], UnityEngine.Random.Range(0f, maxProbability))];
            }
        }
        return null;
        float MaxProbability(EssenceObjectPower cards)
        {
            float maxProbability = 0;
            for (int i = 0; i < cards.Length; i++)
            {
                maxProbability += cards[i].ProbabilityFalling;
            }
            return maxProbability;
        }
        int GetIndexRandomCard(EssenceObjectPower cards, float probability)
        {
            float currentProbability = 0;
            float newProbability = currentProbability + cards[0].ProbabilityFalling;
            for (int i = 0; i < cards.Length; i++)
            {
                if (probability >= currentProbability && probability <= newProbability)
                {
                    return i;
                }
                else
                {
                    currentProbability = newProbability;
                    newProbability += cards[i + 1].ProbabilityFalling;
                }
            }
            return 0;
        }
        int CheckingAvailabilityCardsSelectedPower(int power)
        {
            while (true)
            {
                for (int i = 0; i < UnlockedEnemiesPower.Length; i++)
                {
                    if (UnlockedEnemiesPower[i].Power == power)
                    {
                        return power;
                    }
                }
                power--;
            }
        }
    }
    private int CountUnlockedCard()
    {
        int count = 0;
        if (AllCardsPower.Length == 0) return count;
        for (int i = 0; i < AllCardsPower.Length; i++)
        {
            if (AllCardsPower[i].IsUnlocked())
            {
                count++;
            }
        }
        return count;
    }
    private int CountUnlockedEnemy()
    {
        int count = 0;
        if (AllEnemiesPower.Length == 0) return count;
        for (int i = 0; i < AllEnemiesPower.Length; i++)
        {
            if (AllEnemiesPower[i].IsUnlocked())
            {
                count++;
            }
        }
        return count;
    }
    public void InitializationUnlockedPower()
    {
        UnlockedCardsPower = new EssenceObjectPower[CountUnlockedCard()];
        for (int i = 0,j=0; i < AllCardsPower.Length; i++)
        {
            if (AllCardsPower[i].IsUnlocked())
            {
                UnlockedCardsPower[j] = new EssenceObjectPower() { Power = AllCardsPower[i].Power};
                UnlockedCardsPower[j].SetCards(AllCardsPower[i].GetArrayUnlockedBaseCard());
                j++;
            }
        }
        UnlockedEnemiesPower = new EssenceObjectPower[CountUnlockedEnemy()];
        for (int i = 0, j = 0; i < AllEnemiesPower.Length; i++)
        {
            if (AllEnemiesPower[i].IsUnlocked())
            {
                UnlockedEnemiesPower[j] = new EssenceObjectPower() { Power = AllEnemiesPower[i].Power };
                UnlockedEnemiesPower[j].SetCards(AllEnemiesPower[i].GetArrayUnlockedBaseCard());
                j++;
            }
        }
    }
    public BaseEssenceObject GetBaseCard(BaseEssenceObject card)
    {
        for (int i = 0; i < UnlockedCardsPower.Length; i++)
        {
            for (int j = 0; j < UnlockedCardsPower[i].Length; j++)
            {
                if (UnlockedCardsPower[i][j].Id == card.Id)
                {
                    return UnlockedCardsPower[i][j];
                }
            }
        }
        return null;
    }
    public BaseEssenceObject LevelUpBaseCard(BaseEssenceObject card)
    {
        for (int i = 0; i < UnlockedCardsPower.Length; i++)
        {
            for (int j = 0; j < UnlockedCardsPower[i].Length; j++)
            {
                if (UnlockedCardsPower[i][j].Id == card.Id)
                {
                    UnlockedCardsPower[i][j].LevelUp();
                    return UnlockedCardsPower[i][j];
                }
            }
        }
        return null;
    }
    public BaseEssenceObject LevelUpEnemy(BaseEssenceObject card)
    {
        for (int i = 0; i < UnlockedEnemiesPower.Length; i++)
        {
            for (int j = 0; j < UnlockedEnemiesPower[i].Length; j++)
            {
                if (UnlockedEnemiesPower[i][j].Id == card.Id)
                {
                    UnlockedEnemiesPower[i][j].LevelUp();
                    return UnlockedEnemiesPower[i][j];
                }
            }
        }
        return null;
    }
    public void Reset()
    {
        for (int i = 0; i < AllCardsPower.Length; i++)
        {
            for (int j = 0; j < AllCardsPower[i].Length; j++)
            {
                AllCardsPower[i][j].ResetLevel();
            }
        }
        for (int i = 0; i < AllEnemiesPower.Length; i++)
        {
            for (int j = 0; j < AllEnemiesPower[i].Length; j++)
            {
                AllEnemiesPower[i][j].ResetLevel();
            }
        }
        InitializationUnlockedPower();
    }
    public List<BaseCard> GetAllCards()
    {
        List<BaseCard> arr = new List<BaseCard>();
        for (int i = 0; i < AllCardsPower.Length; i++)
        {
            for (int j = 0; j < AllCardsPower[i].Length; j++)
            {
                arr.Add((BaseCard)AllCardsPower[i][j]);
            }
        }
        return arr;
    }
}

