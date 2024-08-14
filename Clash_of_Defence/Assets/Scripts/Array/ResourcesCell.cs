using UnityEngine;

public class ResourcesCell
{
    private Resources MaxResources;
    private Resources Resources;
    public ResourcesCell(Resources maxResources)
    {
        MaxResources = maxResources;
        Resources = new();
    }
    ~ResourcesCell()
    {
        GameController.instance.ResourcesController.DeleteResourcesCell(this);
    }
    public Resources Get()
    {
        return Resources;
    }
    public Resources GetMax()
    {
        return MaxResources;
    }
    public Resources AddResources(Resources resources)
    {
        Resources result = new Resources();
        if (Resources.Gold + resources.Gold > MaxResources.Gold)
        {
            result.Gold = resources.Gold - (MaxResources.Gold - Resources.Gold);
            Resources.Gold =MaxResources.Gold;
        }
        else
        {
            Resources.Gold += resources.Gold;
        }
        if (Resources.Iron + resources.Iron > MaxResources.Iron)
        {
            result.Iron = resources.Iron - (MaxResources.Iron - Resources.Iron);
            Resources.Iron = MaxResources.Iron;
        }
        else
        {
            Resources.Iron += resources.Iron;
        }
        if (Resources.Power + resources.Power > MaxResources.Power)
        {
            result.Power = resources.Power - (MaxResources.Power - Resources.Power);
            Resources.Power = MaxResources.Power;
        }
        else
        {
            Resources.Power += resources.Power;
        }
        return result;
    }
    public Resources GetResources(Resources resources)
    {
        Resources result = new Resources();
        if (resources.Gold >= MaxResources.Gold)
        {
            Resources.Gold = 0;
            result.Gold = resources.Gold - MaxResources.Gold;
        }
        else
        {
            Resources.Gold -= resources.Gold;
            result.Gold = 0;
        }
        if (resources.Iron >= MaxResources.Iron)
        {
            Resources.Iron = 0;
            result.Iron = resources.Iron - MaxResources.Iron;
        }
        else
        {
            Resources.Iron -= resources.Iron;
            result.Iron = 0;
        }
        if (resources.Power >= MaxResources.Power)
        {
            Resources.Power = 0;
            result.Power = resources.Power - MaxResources.Power;
        }
        else
        {
            Resources.Power -= resources.Power;
            result.Power = 0;
        }
        return result;
    }
    public void DeleteResources()
    {
        //GameController.instance.ResourcesController.UpdateGameResources(Resources,false);
        Resources = new();
    }
    public void OffWarehouse()
    {
        GameController.instance.ResourcesController.UpdateGameResources(Resources,false);
        Resources = new();
    }
}

