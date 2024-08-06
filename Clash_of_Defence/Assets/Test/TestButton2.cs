using UnityEngine;
using UnityEngine.UI;

public class TestButton2:MonoBehaviour
{
    private Button button;
    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            GameController.instance.SpawnEnemiesController.SpawnEnemies
            (GameController.instance.RandomController.GetRandomEnemies(10));
        });
    }
}

