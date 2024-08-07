using UnityEngine;
using UnityEngine.UI;
public class ResourcesController:MonoBehaviour
{
    [HideInInspector] public int Gold;
    [HideInInspector] public Resources GameResources;
    [SerializeField] Text TextGold;
    [SerializeField] Text TextIron;
    [SerializeField] Text TextPower;
    public ResourcesController()
    {
        GameResources = new() {Gold=200,Iron=0,Power=0 };
    }
    public void UpdateGameResources(Resources resources,bool add =true)
    {
        if (add)
        {
            GameResources += resources;
        }
        else
        {
            GameResources -= resources;
        }
        TextGold.text = ":" + GameResources.Gold;
        TextIron.text = ":" + GameResources.Iron;
        TextPower.text = ":" + GameResources.Power;
    }
    private void Start()
    {
        UpdateGameResources(new Resources());
    }
}
