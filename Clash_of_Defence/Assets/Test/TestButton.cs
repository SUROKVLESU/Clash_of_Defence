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
            Debug.Log(Application.persistentDataPath);
            GameController.instance.SaveGame.Save();
        });
    }
}

