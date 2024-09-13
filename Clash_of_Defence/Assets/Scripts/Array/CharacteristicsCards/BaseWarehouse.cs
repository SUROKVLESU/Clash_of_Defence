using System;
using UnityEngine;
public class BaseWarehouse:BaseCharacteristics
{
    [SerializeField] protected Resources SizeWarehouse;
    protected ResourcesCell ResourcesCell;
    public override void MyStart()
    {
        ResourcesCell = new ResourcesCell(SizeWarehouse);
        GameController.instance.ResourcesController.AddResourcesCell(ResourcesCell);
        GameController.instance.ResourcesController.UpdateMaxGameResources();
    }
    private void Start()
    {
        MyStart();
    }
    protected override void Defeat()
    {
        ResourcesCell.OffWarehouse();
        GameController.instance.ResourcesController.DeleteResourcesCell(ResourcesCell);
        GameController.instance.ResourcesController.UpdateMaxGameResources();
        gameObject.SetActive(false);
    }
    public override void Activation()
    {
        GameController.instance.ResourcesController.AddResourcesCell(ResourcesCell);
        GameController.instance.ResourcesController.UpdateMaxGameResources();
    }

    private void OnDestroy()
    {
        GameController.instance.ResourcesController?.DeleteResourcesCell(ResourcesCell);
        GameController.instance.ResourcesController?.UpdateMaxGameResources();
    }
}

