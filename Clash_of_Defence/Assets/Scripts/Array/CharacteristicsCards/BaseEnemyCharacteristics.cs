using System;
using System.Collections;
using UnityEngine;
public class BaseEnemyCharacteristics:AttackingBuildingCharacteristics,IMovement
{
    [SerializeField] protected float SpeedMovement;
    [SerializeField] protected Resources FallingResources;
    protected event Action MovementEvent;
    protected float Angle;
    protected bool IsAimingTarget = false;
    protected const float RotSpeed = 400f;

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
        Angle = MySpecialClass.GetAngleTarget(transform.position, TransformAttackTarget.position);
        StartCoroutine(AimingTargetCoroutine());
        while (true)
        {
            if (!TransformAttackTarget.gameObject.activeSelf)
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
        GameObject attackTarget = GameController.instance.MapController.MainBuilding;
        /*for (int i = 0; i < GameController.instance.MapController.Buildings.Length; i++)
        {
            if (GameController.instance.MapController.Buildings[i].activeSelf)
            {
                attackTarget = GameController.instance.MapController.Buildings[i];
                break;
            }
        }*/
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
        TransformAttackTarget = AttackTarget.GetAttackTargetPosition();
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
        MovementEvent += ()=> { Coroutine = StartCoroutine(AttackCoroutine()); };
    }
    protected virtual IEnumerator AimingTargetCoroutine()
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
            if(!IsAimingTarget) continue;
            if (TransformAttackTarget == null)
            {
                yield break;
            }
            transform.rotation = Quaternion.Euler(Vector3.MoveTowards(transform.rotation.eulerAngles,
            new Vector3(0, Angle, 0), RotSpeed * Time.deltaTime));
            if ((int)transform.rotation.eulerAngles.y == (int)Angle) break;
            yield return null;
        }
        IsAimingTarget=false;
    }
}
public interface IMovement
{
    public void Move();
}