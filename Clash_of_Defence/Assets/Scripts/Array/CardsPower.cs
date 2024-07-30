using System;
using UnityEngine;
[Serializable]
public class CardsPower
{
    public int Power;
    [SerializeField] BaseCard[] Cards;
    public void SetCaeds(BaseCard[] cards)
    {
        Cards = cards;
    }
    public int Length {  get { return Cards.Length; } }
    public bool IsUnlocked()
    {
        if(Cards.Length == 0) return false;
        for (int i = 0; i < Cards.Length; i++)
        {
            if (Cards[i].Unlocked)
            {
                return true;
            }
        }
        return false;
    }
    public int CountUnlocked()
    {
        int count = 0;
        if (Cards.Length == 0) return count;
        for (int i = 0; i < Cards.Length; i++)
        {
            if (Cards[i].Unlocked)
            {
                count++;
            }
        }
        return count;
    }
    public BaseCard[] GetArrayUnlockedBaseCard()
    {
        BaseCard[] arr = new BaseCard[CountUnlocked()];
        for (int i = 0,j=0; i < Cards.Length; i++)
        {
            if (Cards[i].Unlocked)
            {
                arr[j] = Cards[i];
                j++;
            }
        }
        return arr;
    }
    public BaseCard this[int index]
    {
        get
        {
            return Cards[index];
        }
    }

}

