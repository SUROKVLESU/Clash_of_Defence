using System;
using UnityEngine;
public class MainBuilding: BaseWarehouse
{
    [SerializeField] GameObject GameObjectTurret;
    private IBaseInterface Turret;
    private void Start()
    {
        MyStart();
    }
    protected override void Defeat()
    {
        gameObject.SetActive(false);
        GameController.instance.WaveController.PlayerDefeat();
    }
    public override void MyStart()
    {
        base.MyStart();
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

