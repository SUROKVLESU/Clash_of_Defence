//using System;
using UnityEngine;

public class RandomCardsController
{
    private const int MinCountCards = 2;
    private const int MaxCountCards = 5;
    private readonly int MaxPowerCard;
    public RandomCardsController()
    {
        int maxPower = 1;
        for (int i = 0; i < GameController.instance.CollectionCardsController.CardsPower.Length; i++)
        {
            if (GameController.instance.CollectionCardsController.CardsPower[i].Power > maxPower)
            {
                maxPower = GameController.instance.CollectionCardsController.CardsPower[i].Power;
            }
        }
        MaxPowerCard = maxPower;
    }
    public BaseCard[] GetRandomCards(int power)
    {
        int newPower = power;
        int countCards;
        int index = 0;
        if (power > MaxCountCards)
        {
            countCards = Random.Range(MaxCountCards, MaxCountCards + 1);
        }
        else if (power >= MinCountCards)
        {
            countCards = power;
        }
        else
        {
            countCards = MinCountCards;
        }
        BaseCard[] baseCards = new BaseCard[countCards];
        if (power -countCards==0)
        {
            return FillOutEnd(0);
        }
        while (true)
        {
            int newPowerCard = Random.Range(1,newPower-countCards+2);
            if (newPowerCard > MaxPowerCard) { newPowerCard = MaxPowerCard; }
            for (int i = 0; i < GameController.instance.CollectionCardsController.CardsPower.Length; i++)
            {
                if (GameController.instance.CollectionCardsController.CardsPower[i].Power == newPowerCard)
                {
                    baseCards[index] = GameController.instance.CollectionCardsController.CardsPower[i].Cards
                            [Random.Range(0, GameController.instance.CollectionCardsController.CardsPower[i].Cards.Length)];
                    index++;
                    countCards--;
                    newPower -= newPowerCard; 
                    break;
                }
            }
            if (newPower == countCards) { return FillOutEnd(index); }
            if(countCards == 0) { return baseCards; }
        }
        BaseCard[] FillOutEnd(int indexFree)
        {
            for (int i = 0; i < GameController.instance.CollectionCardsController.CardsPower.Length; i++)
            {
                if (GameController.instance.CollectionCardsController.CardsPower[i].Power == 1)
                {
                    for (int j = indexFree; j < baseCards.Length; j++)
                    {
                        baseCards[j] = GameController.instance.CollectionCardsController.CardsPower[i].Cards
                            [Random.Range(0, GameController.instance.CollectionCardsController.CardsPower[i].Cards.Length)];
                    }
                }
            }
            return baseCards;
        }
    }
}


