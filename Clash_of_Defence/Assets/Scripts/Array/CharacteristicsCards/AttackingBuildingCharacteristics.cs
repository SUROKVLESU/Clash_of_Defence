﻿using UnityEngine;
using System.Collections;
public class AttackingBuildingCharacteristics:BaseCharacteristics, IAttackInterface
{
    [SerializeField] protected Attributes Damage;
    [SerializeField] protected int AttackReloading;
    [SerializeField] protected float AttackRadius;
    protected IBaseInterface AttackTarget;
    protected Transform TransformAttackTarget;

    public virtual void SearchAttackTarget()
    {
        for (int i = 0; i < GameController.instance.EnemiesController.Enemies.Count; i++)
        {
            if (GameController.instance.EnemiesController.Enemies[i].activeSelf 
                && (GameController.instance.EnemiesController.Enemies[i].transform.position - transform.position).sqrMagnitude
                <= (AttackRadius * AttackRadius))
            {
                TransformAttackTarget = GameController.instance.EnemiesController.Enemies[i].transform;
                AttackTarget = TransformAttackTarget.GetComponent<IBaseInterface>();
                return;
            }
        }

    }
    public virtual void Attack()
    {
        StartCoroutine(AttackCoroutine());
    }
    protected virtual IEnumerator AttackCoroutine()
    {
        while (true)
        {
            if (TransformAttackTarget == null)
            {
                SearchAttackTarget();
                yield return null;
                continue;
            }
            if (!AttackTarget.TakingDamage(Damage))
            {
                TransformAttackTarget = null;
                AttackTarget = null;
            }
            yield return new WaitForSeconds(AttackReloading);
        }
    }
}
public interface IAttackInterface
{
    public void Attack();
    public void SearchAttackTarget();
}