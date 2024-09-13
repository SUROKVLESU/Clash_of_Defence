using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun:AttackingCharacteristics
{
    public override void SearchAttackTarget(List<GameObject> target)
    {
        base.SearchAttackTarget(target);
        Transform attackTarget = GameController.instance.MapController.MainBuilding.GetComponent<IBaseInterface>().GetAttackTargetPosition();
        if ((attackTarget.position - transform.position).sqrMagnitude
                <= (AttackRadius * AttackRadius) && (TypeAttack == TypeAttack.All
                || TypeAttack == GameController.instance.MapController.MainBuilding.GetComponent<IBaseInterface>().GetTypeTarget()))
        {
            BaseCharacteristics baseCharacteristics = TransformAttackTarget.GetComponent<BaseCharacteristics>();
            AttackTarget.AddDead(DeadTarget);
            TransformAttackTarget = attackTarget.transform;
            AttackTarget = TransformAttackTarget.parent.GetComponent<IBaseInterface>();
        }
    }
}

