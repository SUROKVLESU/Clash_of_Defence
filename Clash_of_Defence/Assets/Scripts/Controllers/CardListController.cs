using UnityEngine;
public class CardListController
{
    public BaseCard[] BaseCardsHand;
    public BaseCard[] BaseCardsMap;
    private void Sort()
    {
        if (BaseCardsHand.Length > 1)
        {
            for (int i = 1; i < BaseCardsHand.Length; i++)
            {
                if (BaseCardsHand[i].Id < BaseCardsHand[i-1].Id)
                {
                    BaseCard baseCard = BaseCardsHand[i-1];
                    BaseCardsHand[i-1] = BaseCardsHand[i];
                    BaseCardsHand[i] = baseCard;
                    i = 1;
                }
            }
        }
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
    public CardListController()
    {
        this.BaseCardsHand = new BaseCard[0];
        this.BaseCardsMap = new BaseCard[0];
    }
}
