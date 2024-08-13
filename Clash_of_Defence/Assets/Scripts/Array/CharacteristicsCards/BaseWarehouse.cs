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
    }
    public override bool TakingDamage(Attributes damage)
    {
        HP -= damage - Protection;
        if (HP < 0)
        {
            ResourcesCell.OffWarehouse();
            GameController.instance.ResourcesController.DeleteResourcesCell(ResourcesCell);
            gameObject.SetActive(false);
            return false;
        }
        else return true;
    }
    public override void ActivationBuildings()
    {
        GameController.instance.ResourcesController.AddResourcesCell(ResourcesCell);
    }

    private void OnDestroy()
    {
        GameController.instance.ResourcesController?.DeleteResourcesCell(ResourcesCell);
    }
}

