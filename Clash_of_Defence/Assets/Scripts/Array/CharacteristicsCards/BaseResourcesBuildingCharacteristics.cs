using UnityEngine;
public class BaseResourcesBuildingCharacteristics : BaseCharacteristics
{
    [SerializeField] protected Resources Resources;
    [SerializeField] protected Resources AddResources = new Resources() {Gold=1,Iron=1,Power=1 };
    public Resources Add_Resources { get { return AddResources; } set { AddResources = value; } }
    public virtual void GetResources()
    {
        if(!gameObject.activeSelf) return;
        GameController.instance.ResourcesController.PlaceResourcesWarehouses(Resources+ Resources *AddResources);
    }
}

