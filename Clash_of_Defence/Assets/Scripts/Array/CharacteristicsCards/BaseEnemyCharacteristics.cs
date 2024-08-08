﻿using System;
using System.Collections;
using UnityEngine;
public class BaseEnemyCharacteristics:AttackingBuildingCharacteristics,IMovement
{
    [SerializeField] protected float SpeedMovement;
    [SerializeField] protected Resources FallingResources;
    protected event Action MovementEvent;

    public virtual void Move()
    {
        if (GameController.instance.MapController.ActiveCount == 0) return;
        Coroutine = StartCoroutine(MovementCoroutine());
    }
    public override bool TakingDamage(Attributes damage)
    {
        HP -= damage - Protection;
        if (HP < 0)
        {
            GameController.instance.ResourcesController.UpdateGameResources(FallingResources);
            GameController.instance.EnemiesController.Enemies.Remove(gameObject);
            Destroy(gameObject);
            if (GameController.instance.EnemiesController.Enemies.Count == 0
                && GameController.instance.SpawnEnemiesController.IsAllSpawnEnemies())
            {
                GameController.instance.WaveController.EnemiesDefeat();
            }
            return false;
        }
        else return true;
    }
    protected virtual IEnumerator MovementCoroutine()
    {
        if (TransformAttackTarget == null) yield break;
        while (true)
        {
            if (!TransformAttackTarget.gameObject.activeSelf)
            {
                SearchAttackTarget();
                Move();
                yield break;
            }
            transform.position = Vector3.MoveTowards(transform.position , TransformAttackTarget.position, SpeedMovement*Time.deltaTime);
            if((TransformAttackTarget.position - transform.position).sqrMagnitude<= (AttackRadius * AttackRadius)) break;
            yield return null;
        }
        MovementEvent();
    }
    protected override IEnumerator AttackCoroutine()
    {
        if(TransformAttackTarget == null||GameController.instance.MapController.ActiveCount==0) yield break;
        while (true)
        {
            if (!TransformAttackTarget.gameObject.activeSelf)
            {
                SearchAttackTarget();
                Move();
                yield break;
            }
            AttackTarget.TakingDamage(Damage);
            yield return new WaitForSeconds(AttackReloading);
        }
    }
    public override void SearchAttackTarget()
    {   
        GameObject attackTarget = GameController.instance.MapController.Buildings[0];
        for (int i = 0; i < GameController.instance.MapController.Buildings.Length; i++)
        {
            if (GameController.instance.MapController.Buildings[i].activeSelf)
            {
                attackTarget = GameController.instance.MapController.Buildings[i];
                break;
            }
        }
        for (int i = 0; i < GameController.instance.MapController.Buildings.Length; i++)
        {
            if (GameController.instance.MapController.Buildings[i].activeSelf
                && (GameController.instance.MapController.Buildings[i].transform.position - transform.position).sqrMagnitude
                <= (attackTarget.transform.position - transform.position).sqrMagnitude)
            {
                attackTarget = GameController.instance.MapController.Buildings[i];
            }
        }
        TransformAttackTarget = attackTarget.transform;
        AttackTarget = TransformAttackTarget.GetComponent<IBaseInterface>();
    }
    public override void ActivationBuildings()
    {
        TransformAttackTarget = null;
        AttackTarget = null;
    }
    public override void Attack()
    {
        if (GameController.instance.MapController.ActiveCount == 0) return;
        base.Attack();
    }
    private void Awake()
    {
        MovementEvent += Attack;
    }
}
public interface IMovement
{
    public void Move();
}