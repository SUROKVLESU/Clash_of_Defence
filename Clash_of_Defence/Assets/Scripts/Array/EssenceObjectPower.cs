using System;
using UnityEngine;
[Serializable]
public class EssenceObjectPower
{
    public int Power;
    [SerializeField] BaseEssenceObject[] Cards;
    public void SetCards(BaseEssenceObject[] cards)
    {
        Cards = cards;
    }
    public int Length {  get { return Cards.Length; } }
    public bool IsUnlocked()
    {
        if(Cards.Length == 0) return false;
        for (int i = 0; i < Cards.Length; i++)
        {
            if (Cards[i].GetCurrentMaxLevel()>0)
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
            if (Cards[i].GetCurrentMaxLevel()>0)
            {
                count++;
            }
        }
        return count;
    }
    public BaseEssenceObject[] GetArrayUnlockedBaseCard()
    {
        BaseEssenceObject[] arr = new BaseEssenceObject[CountUnlocked()];
        for (int i = 0,j=0; i < Cards.Length; i++)
        {
            if (Cards[i].GetCurrentMaxLevel()>0)
            {
                arr[j] = Cards[i];
                j++;
            }
        }
        return arr;
    }
    public BaseEssenceObject this[int index]
    {
        get
        {
            return Cards[index];
        }
    }

}

