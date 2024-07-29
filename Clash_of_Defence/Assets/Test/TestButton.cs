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
            GameController.instance.CardListController.Add
                (GameController.instance.RandomCardsController.GetRandomCards(Random.Range(2,6)));
        });
    }
}

