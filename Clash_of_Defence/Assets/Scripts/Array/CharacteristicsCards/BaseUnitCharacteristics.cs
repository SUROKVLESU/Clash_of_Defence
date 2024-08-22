using UnityEngine;
using System.Collections;
public class BaseUnitCharacteristics:AttackingBuildingCharacteristics
{
    [SerializeField] protected float SpeedMovement;
    [SerializeField] protected Resources FallingResources;
    protected bool IsAimingTarget = false;
    protected float StartHeight;

    public override void ResetHP()
    {
        base.ResetHP();
        transform.position = transform.parent.position+new Vector3(0,StartHeight,0);
    }
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
        StartHeight = transform.position.y;
        SearchAttackTarget();
        Move();
    }
    public override void ActivationBuildings()
    {

        SearchAttackTarget();
        Move();
    }
    public override void Defeat(){ }
    protected virtual IEnumerator MovementCoroutine()
    {
        if (TransformAttackTarget == null||GameController.instance.IsPause)
        {
            yield return null;
            SearchAttackTarget();
            Move();
            yield break;
        }
        Vector3 oldTransformAttackTarget = TransformAttackTarget.position;
        StartCoroutine(AimingTargetCoroutine());
        while (true)
        {
            if (TransformAttackTarget==null)
            {
                SearchAttackTarget();
                Move();
                yield break;
            }
            if (oldTransformAttackTarget != TransformAttackTarget.position)
            {
                StartCoroutine(AimingTargetCoroutine());
                oldTransformAttackTarget = TransformAttackTarget.position;
            }
            transform.position = Vector3.MoveTowards
                (transform.position, TransformAttackTarget.position, SpeedMovement * Time.deltaTime);
            transform.position = new Vector3
                (transform.position.x,StartHeight,transform.position.z);
            if (transform.position.x + AttackRadius > AttackTarget.GetForder(true).position.x
                && transform.position.x - AttackRadius < AttackTarget.GetForder(false).position.x
                && transform.position.z - AttackRadius < AttackTarget.GetForder(true).position.z
                && transform.position.z + AttackRadius > AttackTarget.GetForder(false).position.z) break;
            yield return null;
        }
        Coroutine = StartCoroutine(AttackCoroutine());
    }
    protected override IEnumerator AttackCoroutine()
    {
        Vector3 oldTransformAttackTarget = TransformAttackTarget.position;
        if (TransformAttackTarget==null) yield break;
        while (true)
        {
            if (TransformAttackTarget == null)
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
            if (TransformAttackTarget==null) continue;
            AttackTarget.TakingDamage(Damage);
        }
    }
    public override void SearchAttackTarget()
    {
        if (GameController.instance.EnemiesController.Enemies.Count == 0) return;
        GameObject attackTarget = GameController.instance.EnemiesController.Enemies[0];
        for (int i = 0; i < GameController.instance.EnemiesController.Enemies.Count; i++)
        {
            IBaseInterface baseInterface = GameController.instance.EnemiesController.Enemies[i].GetComponent<IBaseInterface>();
            if ((baseInterface.GetAttackTargetPosition().position - transform.position).sqrMagnitude
                <= (attackTarget.transform.position - transform.position).sqrMagnitude && (TypeAttack == TypeAttack.All
                || TypeAttack == baseInterface.GetTypeTarget()))
            {
                attackTarget = GameController.instance.EnemiesController.Enemies[i];
            }
        }
        TransformAttackTarget = attackTarget.transform;
        AttackTarget = TransformAttackTarget.GetComponent<IBaseInterface>();
}
    protected override IEnumerator AimingTargetCoroutine()
    {
        Angle = MySpecialClass.GetAngleTarget(transform.position, TransformAttackTarget.position);
        if (IsAimingTarget)
        {
            while (true)
            {
                if (!IsAimingTarget) break;
                yield return null;
            }
        }
        IsAimingTarget = true;
        while (true)
        {
            if (TransformAttackTarget == null)
            {
                yield break;
            }
            TurningTower();
            if (Mathf.Abs(TransformTower.rotation.eulerAngles.y - Angle) <= 2) break;
            yield return null;
        }
        IsAimingTarget = false;
    }
}