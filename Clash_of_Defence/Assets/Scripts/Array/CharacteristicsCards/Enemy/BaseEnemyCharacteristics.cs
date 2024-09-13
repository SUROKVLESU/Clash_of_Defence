using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BaseEnemyCharacteristics:AttackingCharacteristics
{
    [SerializeField] protected float SpeedMovement;
    [SerializeField] protected Resources FallingResources;
    protected bool IsAimingTarget = false;
    protected bool IsMoveEnemy = false;

    public override bool IsMove()
    {
        return IsMoveEnemy;
    }
    private void Start()
    {
        MyStart();
    }
    public override void Activation()
    {
        Animator.SetBool("Attack", false);
        Animator.speed = 1;
        base.Activation();
    }
    public override void MyStart()
    {
        TransformTower = transform;
        Animator = GetComponent<Animator>();
        base.MyStart();
    }
    protected override IEnumerator SearchAttackTargetCoroutine()
    {
        while (true)
        {
            SearchAttackTarget(GameController.instance.MapController.Buildings);
            if (AttackTarget != null)
            {
                break;
            }
            yield return new WaitForSeconds(0.2f);
        }
        Coroutine = StartCoroutine(MovementCoroutine());
    }
    protected override void Defeat()
    {
        GameController.instance.ResourcesController.PlaceResourcesWarehouses(FallingResources);
        GameController.instance.EnemiesController.Enemies.Remove(gameObject);
        GameController.instance.CountEnemyText.text = ":" + --GameController.instance.WaveController.CountEnemis;
        if (GameController.instance.EnemiesController.Enemies.Count == 0
            && GameController.instance.SpawnEnemiesController.IsAllSpawnEnemies())
        {
            GameController.instance.WaveController.EnemiesDefeat();
        }
        StartDead();
        gameObject.SetActive(false);
    }
    protected virtual IEnumerator MovementCoroutine()
    {
        IsMoveEnemy = true;
        StartCoroutine(AimingTargetCoroutine());
        Animator.SetBool("Move",true);
        while (true)
        {
            if (AttackTarget.IsMove())
            {
                Coroutine = StartCoroutine(SearchAttackTargetCoroutine());
                yield break;
            }
            transform.position = Vector3.MoveTowards
                (transform.position , TransformAttackTarget.position, SpeedMovement*Time.deltaTime);
            if (transform.position.x+AttackRadius>AttackTarget.GetForder(true).position.x
                && transform.position.x - AttackRadius < AttackTarget.GetForder(false).position.x
                && transform.position.z - AttackRadius < AttackTarget.GetForder(true).position.z
                && transform.position.z + AttackRadius > AttackTarget.GetForder(false).position.z) break;
            yield return null;
        }
        IsMoveEnemy = false;
        Animator.SetBool("Move", false);
        Coroutine = StartCoroutine(AttackCoroutine());
    }
    protected override IEnumerator AttackCoroutine()
    {
        while (true)
        {
            Animator.StopPlayback();
            Animator.speed = (1/AttackReloading);
            Animator.SetBool("Attack", true);
            AttackTarget.TakingDamage(Damage);
            if (IsSustainedDamage) AttackTarget.TakingSustainedDamage(SustainedDamage, TimeSustainedDamage);
            yield return new WaitForSeconds(AttackReloading);
            if (AttackTarget.IsMove())
            {
                Coroutine = StartCoroutine(SearchAttackTargetCoroutine());
                Animator.speed = 1;
                Animator.SetBool("Attack", false);
                yield break;
            }
        }
    }
    public override void SearchAttackTarget(List<GameObject> target)
    {   
        GameObject attackTarget = 
            GameController.instance.MapController.MainBuilding.GetComponent<IBaseInterface>().GetAttackTargetPosition().gameObject;
        for (int i = 0; i < target.Count; i++)
        {
            if (target[i].activeSelf &&(TypeAttack == TypeAttack.All || TypeAttack ==
                target[i].GetComponent<IBaseInterface>().GetTypeTarget())
                && (target[i].transform.position - transform.position).sqrMagnitude
                < (attackTarget.transform.position- transform.position).sqrMagnitude)
            {
                attackTarget = target[i].GetComponent<IBaseInterface>().GetAttackTargetPosition().gameObject;
            }
        }
        TransformAttackTarget = attackTarget.transform;
        AttackTarget = TransformAttackTarget.parent.GetComponent<IBaseInterface>();
        AttackTarget.AddDead(DeadTarget);
    }
    protected override IEnumerator AimingTargetCoroutine()
    {
        if (IsAimingTarget)
        {
            while (true)
            {
                if(!IsAimingTarget) break;
                yield return null;
            }
        }
        IsAimingTarget = true;
        while (true)
        {
            TurningTower();
            if (Mathf.Abs(TransformTower.rotation.eulerAngles.y - Angle) <= 10) break;
            yield return null;
        }
        IsAimingTarget=false;
    }
}