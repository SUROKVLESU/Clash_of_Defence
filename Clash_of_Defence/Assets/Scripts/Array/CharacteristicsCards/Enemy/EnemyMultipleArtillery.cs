using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMultipleArtillery:EnemyMortar
{
    [SerializeField] protected int CountTarget;
    protected override IEnumerator AartilleryAttackCoroutine()
    {
        List<IBaseInterface> baseInterfaces = new();
        yield return new WaitForSeconds((TransformAttackTarget.position - transform.position).sqrMagnitude / SqrProjectileVelocity);
        for (int i = 0; i < GameController.instance.MapController.Buildings.Length; i++)
        {
            if ((GameController.instance.MapController.Buildings[i].transform.position - transform.position).sqrMagnitude
                <= (AttackRadius * AttackRadius) && (TypeAttack == TypeAttack.All || TypeAttack ==
                GameController.instance.MapController.Buildings[i].GetComponent<IBaseInterface>().GetTypeTarget()))
            {
                baseInterfaces.Add(GameController.instance.MapController.Buildings[i].GetComponent<IBaseInterface>());
                StartCoroutine(MortarExplosionCoroutine(GameController.instance.MapController.Buildings[i].transform.position));
            }
            if (baseInterfaces.Count == CountTarget)
            {
                break;
            }
            if (baseInterfaces.Count < CountTarget && i == GameController.instance.MapController.Buildings.Length - 1)
            {
                i = 0;
            }
        }
        if ((GameController.instance.MapController.MainBuilding.transform.position - transform.position).sqrMagnitude
                <= (MortarRadius * MortarRadius) && (TypeAttack == TypeAttack.All || TypeAttack ==
                GameController.instance.MapController.MainBuilding.GetComponent<IBaseInterface>().GetTypeTarget()))
        {
            baseInterfaces.Add(GameController.instance.MapController.MainBuilding.GetComponent<IBaseInterface>());
            StartCoroutine(MortarExplosionCoroutine(GameController.instance.MapController.MainBuilding.transform.position));
        }
        foreach (var item in baseInterfaces)
        {
            item.TakingDamage(Damage);
            if (IsSustainedDamage) AttackTarget.TakingSustainedDamage(SustainedDamage, TimeSustainedDamage);
        }
    }
    public EnemyMultipleArtillery()
    {
        ExplosionTime = ExplosionTime / 3;
    }
}

