using UnityEngine;
public class BaseResourcesBuildingCharacteristics : BaseCharacteristics
{
    public Resources Resources;
    public virtual void GetResources()
    {
        if(!gameObject.activeSelf) return;
        GameController.instance.ResourcesController.PlaceResourcesWarehouses(Resources);
    }
}

