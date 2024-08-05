using UnityEngine;
using UnityEngine.UI;


public class TestButton : MonoBehaviour
{
    private Button button;
    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => 
        {
            //GameController.instance.InterfeceRandomCards.SetActive(true);
            GameController.instance.InterfeceRandomCardsController.Initialization();
            /*GameController.instance.InterfeceRandomCardsController.ReceiveRandomCards
                (GameController.instance.RandomCardsController.GetRandomCards(Random.Range(2, 18)));*/
            GameController.instance.SpawnEnemiesController.SpawnEnemies
            (GameController.instance.RandomController.GetRandomEnemies(10));
        });
    }
}

