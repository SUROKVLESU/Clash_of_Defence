using System;
using UnityEngine;
public class BaseWarehouse:BaseCharacteristics
{
    [SerializeField] protected Resources SizeWarehouse;
    protected ResourcesCell ResourcesCell;
    private void Start()
    {
        ResourcesCell = new ResourcesCell(SizeWarehouse);
        GameController.instance.ResourcesController.AddResourcesCell(ResourcesCell);
        GameController.instance.ResourcesController.UpdateMaxGameResources();
    }
    public override bool TakingDamage(Attributes damage)
    {
        HP -= damage - Protection;
        if (HP < 0)
        {
            ResourcesCell.OffWarehouse();
            GameController.instance.ResourcesController.DeleteResourcesCell(ResourcesCell);
            GameController.instance.ResourcesController.UpdateMaxGameResources();
            gameObject.SetActive(false);
            return false;
        }
        else return true;
    }
    public override void ActivationBuildings()
    {
        GameController.instance.ResourcesController.AddResourcesCell(ResourcesCell);
        GameController.instance.ResourcesController.UpdateMaxGameResources();
    }

    private void OnDestroy()
    {
        GameController.instance.ResourcesController?.DeleteResourcesCell(ResourcesCell);
        GameController.instance.ResourcesController.UpdateMaxGameResources();
    }
}

