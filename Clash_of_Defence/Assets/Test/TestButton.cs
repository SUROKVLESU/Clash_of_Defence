using UnityEngine;
using UnityEngine.UI;


public class TestButton : MonoBehaviour
{
    private Button button;
    [SerializeField] ListBuildingsHand ListBuildingsHand;
    [SerializeField] GameObject Obj;
    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => { ListBuildingsHand.AddCard(Obj); });
    }
}

