using System;
using UnityEngine;
public class MainBuilding:BaseCharacteristics
{
    [SerializeField] GameObject GameObjectTurret;
    private IBaseInterface Turret;
    private void Start()
    {
        Turret = GameObjectTurret.GetComponent<IBaseInterface>();
    }
    public override void Stop()
    {
        Turret.Stop();
    }
    public override void ActivationBuildings()
    {
        Turret.ActivationBuildings();
    }
}

