using System;
using System.Collections;
using UnityEngine;
public class BaseEnemyCharacteristics:AttackingBuildingCharacteristics,IMovement
{
    [SerializeField] protected float SpeedMovement;
    [SerializeField] protected Resources FallingResources;
    //protected event Action MovementEvent;
    //protected float Angle;
    protected bool IsAimingTarget = false;
    //protected const float RotSpeed = 400f;

    public virtual void Move()
    {
        Coroutine = StartCoroutine(MovementCoroutine());
    }
    private void Start()
    {
        MyStart();
    }
    public override void MyStart()
    {
        TransformTower = transform;
        SearchAttackTarget();
        Move();
    }
    public override void ActivationBuildings()
    {
        SearchAttackTarget();
        Move();
    }
    public override void Defeat()
    {
        GameController.instance.ResourcesController.PlaceResourcesWarehouses(FallingResources);
        GameController.instance.EnemiesController.Enemies.Remove(gameObject);
        if (GameController.instance.EnemiesController.Enemies.Count == 0
            && GameController.instance.SpawnEnemiesController.IsAllSpawnEnemies())
        {
            GameController.instance.WaveController.EnemiesDefeat();
        }
        Destroy(gameObject);
    }
    protected virtual IEnumerator MovementCoroutine()
    {
        Vector3 oldTransformAttackTarget = TransformAttackTarget.position;
        StartCoroutine(AimingTargetCoroutine());
        while (true)
        {
            if (oldTransformAttackTarget!= TransformAttackTarget.position)
            {
                StartCoroutine(AimingTargetCoroutine());
                oldTransformAttackTarget = TransformAttackTarget.position;
                SearchAttackTarget();
            }
            if (!TransformAttackTarget.parent.gameObject.activeSelf)
            {
                SearchAttackTarget();
                Move();
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
        Coroutine = StartCoroutine(AttackCoroutine());
    }
    protected override IEnumerator AttackCoroutine()
    {
        if(!TransformAttackTarget.parent.gameObject.activeSelf) yield break;
        Vector3 oldTransformAttackTarget = TransformAttackTarget.position;
        while (true)
        {
            if (!TransformAttackTarget.parent.gameObject.activeSelf)
            {
                SearchAttackTarget();
                Move();
                yield break;
            }
            if (oldTransformAttackTarget != TransformAttackTarget.position)
            {
                Move();
                oldTransformAttackTarget = TransformAttackTarget.position;
                yield break;
            }
            yield return new WaitForSeconds(AttackReloading);
            if (!TransformAttackTarget.gameObject.activeSelf) continue;
            AttackTarget.TakingDamage(Damage);
        }
    }
    public override void SearchAttackTarget()
    {   
        Transform attackTarget = GameController.instance.MapController.MainBuilding.GetComponent<IBaseInterface>().GetAttackTargetPosition();
        for (int i = 0; i < GameController.instance.MapController.Buildings.Length; i++)
        {
            IBaseInterface baseInterface = GameController.instance.MapController.Buildings[i].GetComponent<IBaseInterface>();
            if (GameController.instance.MapController.Buildings[i].activeSelf
                && (baseInterface.GetAttackTargetPosition().position - transform.position).sqrMagnitude
                <= (attackTarget.position - transform.position).sqrMagnitude&&(TypeAttack == TypeAttack.All
                || TypeAttack == baseInterface.GetTypeTarget()))
            {
                if (baseInterface.WallGameObject!=null&& baseInterface.WallGameObject.activeSelf)
                {
                    attackTarget = baseInterface.WallGameObject.GetComponent<IBaseInterface>().GetAttackTargetPosition();
                }
                else attackTarget = GameController.instance.MapController.Buildings[i].GetComponent<IBaseInterface>().GetAttackTargetPosition();
            }
        }
        TransformAttackTarget = attackTarget;
        AttackTarget = TransformAttackTarget.parent.GetComponent<IBaseInterface>();
    }
    protected override IEnumerator AimingTargetCoroutine()
    {
        Angle = MySpecialClass.GetAngleTarget(transform.position, TransformAttackTarget.position);
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
            if (!TransformAttackTarget.parent.gameObject.activeSelf)
            {
                yield break;
            }
            TurningTower();
            if (Mathf.Abs(TransformTower.rotation.eulerAngles.y - Angle) <= 2) break;
            yield return null;
        }
        IsAimingTarget=false;
    }
}
public interface IMovement
{
    public void Move();
}