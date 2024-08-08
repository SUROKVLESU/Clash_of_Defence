using System;
using UnityEngine;

public class InterfeceRandomCardsController:MonoBehaviour
{
    private RectTransform RectTransform;
    private const int CountCardsRow = 5;
    private float Height=0;
    private GameObject[] gameObjects = new GameObject[0];
    private BaseCard[] baseCards;
    public void Initialization()
    {
        if (RectTransform == null)
        {
            RectTransform = GameController.instance.InterfeceRandomCards.transform.GetChild(0).GetComponent<RectTransform>();
            Height = RectTransform.rect.height;
        }
        //GameController.instance.InterfeceRandomCards.SetActive(false);
    }
    public void ReceiveRandomCards(BaseCard[] cards)
    {
        DestroyCards();
        baseCards = cards;
        RectTransform.parent.gameObject.SetActive(true);
        gameObjects = new GameObject[cards.Length];
        ScaleImage();
        int ystart;
        float xstart;
        int countString = (int)Math.Ceiling((float)cards.Length / CountCardsRow);
        if (countString==1) ystart = 0;
        else if(countString % 2 == 0) ystart = countString / 2;
        else ystart = countString / 2+1;
        xstart = (CountCardsRow - (cards.Length - CountCardsRow * (countString - 1)))/2f;
        for (int i = 0,xi=0,yi=ystart; i < cards.Length;i++)
        {
            gameObjects[i] = Instantiate(cards[i].CardCover,RectTransform);
            this.ScaleImage(gameObjects[i]);
            if (i < CountCardsRow*(cards.Length / CountCardsRow))
            {
                gameObjects[i].transform.localPosition = new Vector2
                    (Height * (xi - (CountCardsRow / 2)), (Height / 2) * yi);
            }
            else
            {
                gameObjects[i].transform.localPosition = new Vector2
                    (Height * (xi - (CountCardsRow / 2)+xstart), (Height / 2) * yi);
            }
            xi++;
            if ((i+1) % CountCardsRow ==0)
            {
                xi = 0;
                yi-=2;
            }
        }
        void ScaleImage()
        {
            RectTransform.SetSizeWithCurrentAnchors
                (RectTransform.Axis.Horizontal, Height * CountCardsRow);
            int row;
            if (cards.Length <= CountCardsRow) row = 1;
            else if(cards.Length % CountCardsRow==0) row = (cards.Length / CountCardsRow);
            else row = (cards.Length / CountCardsRow) + 1;
            RectTransform.SetSizeWithCurrentAnchors
                (RectTransform.Axis.Vertical, Height * row);
        }
    }
    private void DestroyCards()
    {
        if (gameObjects != null)
        {
            for (int i = 0; i < gameObjects.Length; i++)
            {
                Destroy(gameObjects[i]);
            }
            gameObjects = null;
        }
    }
    private void ScaleImage(GameObject gameObject)
    {
        RectTransform rect = gameObject.GetComponent<RectTransform>();
        rect.SetSizeWithCurrentAnchors
            (RectTransform.Axis.Horizontal, Height);
        rect.SetSizeWithCurrentAnchors
            (RectTransform.Axis.Vertical, Height);
    }
    public void AddCards()
    {
        RectTransform.parent.gameObject.SetActive(false);
        GameController.instance.CardListController.Add(baseCards);
        DestroyCards();
        GameController.instance.ButtonController.OnInterfeceHand();
        GameController.instance.ButtonController.OnStartWaveButton();
    }
    public void GetRandomCards()
    {
        GameController.instance.InterfeceRandomCards.SetActive(true);
        Initialization();
        ReceiveRandomCards(GameController.instance.RandomController.GetRandomCards
            (GameController.instance.WaveController.CurentPowerCards));
        GameController.instance.ButtonController.GetRandomCards();
        GameController.instance.ButtonController.OffStartWaveButton();
    }
}
