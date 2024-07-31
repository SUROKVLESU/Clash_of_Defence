using System;
using UnityEngine;
[Serializable]
public class CollectionCardsController
{
    [SerializeField] CardsPower[] AllCardsPower;
    [HideInInspector] public CardsPower[] UnlockedCardsPower;
    private int MaxPowerCard;
    public void Initialization()
    {
        int maxPower = 1;
        for (int i = 0; i < UnlockedCardsPower.Length; i++)
        {
            if (UnlockedCardsPower[i].Power > maxPower)
            {
                maxPower = UnlockedCardsPower[i].Power;
            }
        }
        MaxPowerCard = maxPower;
        Reset();
    }
    public CollectionCardsController()
    {

    }
    public BaseCard GetRandomCard(int power)
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
        float MaxProbability(CardsPower cards)
        {
            float maxProbability=0;
            for (int i = 0; i < cards.Length; i++)
            {
                maxProbability += cards[i].ProbabilityCardFalling;
            }
            return maxProbability;
        }
        int GetIndexRandomCard(CardsPower cards, float probability)
        {
            float currentProbability= 0;
            float newProbability = currentProbability + cards[0].ProbabilityCardFalling;
            for (int i = 0; i < cards.Length; i++)
            {
                if (probability >= currentProbability && probability <= newProbability)
                {
                    return i;
                }
                else
                {
                    currentProbability = newProbability;
                    newProbability += cards[i+1].ProbabilityCardFalling;
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
    private int CountUnlocked()
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
    public void InitializationUnlockedCardsPower()
    {
        UnlockedCardsPower = new CardsPower[CountUnlocked()];
        for (int i = 0,j=0; i < AllCardsPower.Length; i++)
        {
            if (AllCardsPower[i].IsUnlocked())
            {
                UnlockedCardsPower[j] = new CardsPower() { Power = AllCardsPower[i].Power};
                UnlockedCardsPower[j].SetCaeds(AllCardsPower[i].GetArrayUnlockedBaseCard());
                j++;
            }
        }
    }
    public BaseCard GetBaseCard(BaseCard card)
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
    public BaseCard LevelUpBaseCard(BaseCard card)
    {
        for (int i = 0; i < UnlockedCardsPower.Length; i++)
        {
            for (int j = 0; j < UnlockedCardsPower[i].Length; j++)
            {
                if (UnlockedCardsPower[i][j].Id == card.Id)
                {
                    UnlockedCardsPower[i][j].SetLevelCard(UnlockedCardsPower[i][j].GetLevelCard()+2);
                    return UnlockedCardsPower[i][j];
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
        InitializationUnlockedCardsPower();
    }
}

