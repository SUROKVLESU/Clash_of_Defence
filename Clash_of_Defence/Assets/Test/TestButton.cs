using UnityEngine;
using UnityEngine.UI;


public class TestButton : MonoBehaviour
{
    private Button button;
    [SerializeField] ListBuildingsHand ListBuildingsHand;
    [SerializeField] BaseCard BaseCard;
    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => 
        { 
            GameController.instance.CardListController.Add(BaseCard);
        });
    }
}

