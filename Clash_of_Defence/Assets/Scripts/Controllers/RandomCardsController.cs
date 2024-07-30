//using System;
using UnityEngine;

public class RandomCardsController
{
    private const int MinCountCards = 2;
    private const int MaxCountCards = 17;
    public RandomCardsController()
    {

    }
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
            baseCards[index] = GameController.instance.CollectionCardsController.GetRandomCard(newPowerCard);
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
                baseCards[j] = GameController.instance.CollectionCardsController.GetRandomCard(1);
            }
            return baseCards;
        }
    }
}


