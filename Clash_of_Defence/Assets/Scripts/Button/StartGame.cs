using UnityEngine;
using UnityEngine.UI;
public class StartGame:MonoBehaviour
{
    private Button button;
    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => {
            GameController.instance.ButtonController.RestartGame();
        });
    }
}
    
