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

