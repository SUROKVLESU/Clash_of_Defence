using UnityEngine;
using UnityEngine.UI;

public class Shop:MonoBehaviour
{
    private Button button;
    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => {
            GameController.instance.ShopInterfeceObject.SetActive(true);
            GameController.instance.ResourcesController.PrintShopGold();
            GameController.instance.ShopInterfece.ResetShop();
        });
    }
}

