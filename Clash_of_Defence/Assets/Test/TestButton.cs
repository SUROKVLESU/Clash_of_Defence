﻿using UnityEngine;
using UnityEngine.UI;


public class TestButton : MonoBehaviour
{
    private Button button;
    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => 
        {
            GameController.instance.InterfeceRandomCards.SetActive(true);
            GameController.instance.InterfeceRandomCardsController.Initialization();
            GameController.instance.InterfeceRandomCardsController.ReceiveRandomCards
                (GameController.instance.RandomController.GetRandomCards(Random.Range(2, 18)));
        });
    }
}

