using System;
using UnityEngine;
public class MainBuilding: BaseWarehouse
{
    [SerializeField] GameObject GameObjectTurret;
    private IBaseInterface Turret;
    public override void Stop()
    {
        Turret = GameObjectTurret.GetComponent<IBaseInterface>();
        Turret.Stop();
    }
    public override void ActivationBuildings()
    {
        Turret = GameObjectTurret.GetComponent<IBaseInterface>();
        Turret.ActivationBuildings();
    }
    public override bool TakingDamage(Attributes damage)
    {
        HP -= damage - Protection;
        if (HP < 0)
        {
            gameObject.SetActive(false);
            GameController.instance.WaveController.PlayerDefeat();
            return false;
        }
        else return true;
    }
}

