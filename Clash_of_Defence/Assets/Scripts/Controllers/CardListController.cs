using UnityEngine;
public class CardListController
{
    public BaseCard[] BaseCardsHand;
    public BaseCard[] BaseCardsMap;
    private BaseCard[] Sort(BaseCard[] baseCards)
    {
        if (baseCards.Length > 1)
        {
            for (int i = 1; i < baseCards.Length;)
            {
                if (baseCards[i].Id < baseCards[i - 1].Id)
                {
                    BaseCard baseCard = baseCards[i - 1];
                    baseCards[i - 1] = baseCards[i];
                    baseCards[i] = baseCard;
                    i = 1;
                }
                else i++;
            }
        }
        return baseCards;
    }
    public void UseTheCard(BaseCard baseCard)//перемещает карту из списка руки в список используемых на игровом поле
    {
        bool use = false;
        BaseCard[] arrHand = new BaseCard[BaseCardsHand.Length-1];
        BaseCard[] arrMap = new BaseCard[BaseCardsMap.Length+1];
        for (int i = 0; i < BaseCardsMap.Length; i++)
        {
            arrMap[i] = BaseCardsMap[i];
        }
        arrMap[BaseCardsMap.Length] = baseCard;
        BaseCardsMap = arrMap;
        for (int i = 0,j=0;i < BaseCardsHand.Length; i++,j++)
        {
            if (BaseCardsHand[i].Id == baseCard.Id&&!use)
            {
                use = true;
                j--;
                continue;
            }
            arrHand[j] = BaseCardsHand[i];
        }
        BaseCardsHand = arrHand;
        GameController.instance.ChangingNumberCards();
    }
    public void Add(BaseCard baseCard)//добавляет карту в список руки
    {
        bool added = false;
        BaseCard[] arr = new BaseCard[BaseCardsHand.Length+1];
        for (int i = 0,j=0;i< BaseCardsHand.Length;i++,j++)
        {
            if(baseCard.Id <= BaseCardsHand[i].Id&&!added)
            {
                arr[j] = baseCard;
                added = true;
                i--;
            }
            else
            {
                arr [j] = BaseCardsHand[i];
            }
        }
        if(!added)
        {
            arr[arr.Length-1] = baseCard;
        }
        BaseCardsHand = arr;
        GameController.instance.ChangingNumberCards();
    }
    public void Add(BaseCard[] baseCards)
    {
        baseCards = Sort(baseCards);
        bool added = false;
        int k = 0;
        BaseCard[] arr = new BaseCard[BaseCardsHand.Length + baseCards.Length];
        for (int i = 0, j = 0; i < BaseCardsHand.Length; i++, j++)
        {
            if (!added&& baseCards[k].Id <= BaseCardsHand[i].Id)
            {
                arr[j] = baseCards[k];
                k++;
                i--;
                if (k == baseCards.Length) { added = true; }
            }
            else
            {
                arr[j] = BaseCardsHand[i];
            }
        }
        if (!added)
        {
            for(int i =BaseCardsHand.Length+k;i<arr.Length; i++)
            {
                arr[i] = baseCards[k];
                k++;
            }
        }
        BaseCardsHand = arr;
        GameController.instance.ChangingNumberCards();
    }
    public void ReturnCardYourHand(int id)
    {
        bool add = false;
        BaseCard[] arr = new BaseCard [BaseCardsMap.Length-1];
        for (int i = 0,j=0; i < BaseCardsMap.Length; i++,j++)
        {
            if (BaseCardsMap[i].Id == id&&!add)
            {
                add = true;
                Add(BaseCardsMap[i]);
                j--;
            }
            else
            {
                arr[j] =BaseCardsMap[i];
            }
        }
        BaseCardsMap=arr;
        GameController.instance.ChangingNumberCards();
    }
    public void ReturnAllCardYourHand()
    {
        Add(BaseCardsMap);
        BaseCardsMap = new BaseCard[0];
    }
    public CardListController()
    {
        this.BaseCardsHand = new BaseCard[0];
        this.BaseCardsMap = new BaseCard[0];
    }
}
