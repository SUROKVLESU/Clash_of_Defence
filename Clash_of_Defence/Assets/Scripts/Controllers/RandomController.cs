//using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomController
{
    private const int MinCountCards = 2;
    private const int MaxCountCards = 7;
    public BaseCard[] GetRandomCards(int power)
    {
        int newPower = power;
        int countCards;
        int index = 0;
        if (power >= MinCountCards&&power<=MaxCountCards)
        {
            countCards = Random.Range(MinCountCards, power + 1);
        }
        else if (power >= MaxCountCards)
        {
            countCards = Random.Range(MinCountCards, MaxCountCards + 1);
        }
        else 
        { 
            countCards = power;
        }
        BaseCard[] baseCards = new BaseCard[countCards];
        if (power -countCards==0)
        {
            return FillOutEnd(0);
        }
        while (true)
        {
            int newPowerCard = Random.Range(1, newPower - countCards + 2);
            baseCards[index] = (BaseCard)GameController.instance.CollectionCardsController.GetRandomCard(newPowerCard);
            index++;
            countCards--;
            newPower -= newPowerCard;

            if (newPower == countCards) { return FillOutEnd(index); }
            if (countCards == 0) { return baseCards; }
        }
        BaseCard[] FillOutEnd(int indexFree)
        {
            for (int j = indexFree; j < baseCards.Length; j++)
            {
                baseCards[j] = (BaseCard)GameController.instance.CollectionCardsController.GetRandomCard(1);
            }
            return baseCards;
        }
    }
    public BaseEssenceObject[] GetRandomEnemies(int power)
    {
        List<BaseEssenceObject> enemies = new();
        int newPower = power;
        while (true)
        {
            int newPowerCard = Random.Range(1, newPower + 1);
            BaseEssenceObject enemy = GameController.instance.CollectionCardsController.GetRandomEnemy(newPowerCard);
            enemies.Add(enemy);
            newPower -= enemy.Power;
            if (newPower <= 0) { break; }
        }
        BaseEssenceObject[] arr = new BaseEssenceObject[enemies.Count];
        for (int i = 0; i < enemies.Count; i++)
        {
            arr[i]= enemies[i];
        }
        return arr;
    }
}


