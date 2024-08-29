

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleArtillery:Mortar
{
    [SerializeField] protected int CountTarget;
    protected override IEnumerator AartilleryAttackCoroutine()
    {
        List<IBaseInterface> baseInterfaces = new();
        yield return new WaitForSeconds((TransformAttackTarget.position - transform.position).sqrMagnitude / SqrProjectileVelocity);
        for (int i = 0; i < GameController.instance.EnemiesController.Enemies.Count; i++)
        {
            if ((GameController.instance.EnemiesController.Enemies[i].transform.position - transform.position).sqrMagnitude
                <= (AttackRadius * AttackRadius) && (TypeAttack == TypeAttack.All || TypeAttack ==
                GameController.instance.EnemiesController.Enemies[i].GetComponent<IBaseInterface>().GetTypeTarget()))
            {
                baseInterfaces.Add(GameController.instance.EnemiesController.Enemies[i].GetComponent<IBaseInterface>());
                StartCoroutine(MortarExplosionCoroutine(GameController.instance.EnemiesController.Enemies[i].transform.position));
            }
            if (baseInterfaces.Count == CountTarget)
            {
                break;
            }
            if (baseInterfaces.Count < CountTarget && i == GameController.instance.EnemiesController.Enemies.Count - 1)
            {
                i = 0;
            }
        }
        foreach (var item in baseInterfaces)
        {
            item.TakingDamage(Damage);
        }
    }
    public MultipleArtillery()
    {
        ExplosionTime = ExplosionTime / 3;
    }
}

