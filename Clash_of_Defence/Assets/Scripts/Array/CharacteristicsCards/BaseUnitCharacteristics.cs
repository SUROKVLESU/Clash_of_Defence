using UnityEngine;
using System.Collections;
public class BaseUnitCharacteristics:AttackingBuildingCharacteristics
{
    [SerializeField] protected float SpeedMovement;
    [SerializeField] protected Resources FallingResources;
    //protected bool IsAimingTarget = false;
    protected float StartHeight;

    public override void MyUpdate(IEnumerator enumerator)
    {
        if (!GameController.instance.IsPause)
        {
            Coroutine = StartCoroutine(enumerator);
        }
    }
    public override void Defeat()
    {
        gameObject.transform.parent.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
    public override void ResetHP()
    {
        base.ResetHP();
        transform.position = transform.parent.position+new Vector3(0,StartHeight,0);
    }
    public virtual void Move()
    {
        MyUpdate(MovementCoroutine());
    }
    private void Start()
    {
        MyStart();
    }
    public override void MyStart()
    {
        TransformTower = transform.GetChild(0);
        StartHeight = transform.position.y;
        SearchAttackTarget();
        Move();
    }
    public override void ActivationBuildings()
    {
        SearchAttackTarget();
        Move();
    }
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
        //StartCoroutine(AimingTargetCoroutine());
        TurningTower();
        while (true)
        {
            if (GameController.instance.IsPause) { yield break; }
            if (TransformAttackTarget==null)
            {
                SearchAttackTarget();
                Move();
                yield break;
            }
            if (oldTransformAttackTarget != TransformAttackTarget.position)
            {
                TurningTower();
                //StartCoroutine(AimingTargetCoroutine());
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
        MyUpdate(AttackCoroutine());
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
            if (GameController.instance.IsPause) { yield break; }
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
        /*if (Angle < 0)
        {
            Angle = MySpecialClass.GetAngleTarget(transform.position, TransformAttackTarget.position);
        }
        else
        {
            Angle = MySpecialClass.GetAngleTarget(transform.position, TransformAttackTarget.position);
            yield break;
        }
        //Angle = MySpecialClass.GetAngleTarget(transform.position, TransformAttackTarget.position);
        if (IsAimingTarget)
        {
            while (true)
            {
                Debug.Log("Fail");
                if (!IsAimingTarget) break;
                yield return null;
            }
        }
        IsAimingTarget = true;*/
        while (true)
        {
            if (TransformAttackTarget == null)
            {
                yield break;
            }
            TurningTower();
            if (Mathf.Abs(TransformTower.rotation.eulerAngles.y - Angle) <= 2) break;
            yield return null;
            //Debug.Log(Angle);
        }
        Angle = -12;
        //Debug.Log(Angle);
        //IsAimingTarget = false;
    }
    protected override void TurningTower()
    {
        if (TransformAttackTarget == null) return;
        Angle = MySpecialClass.GetAngleTarget(transform.position, TransformAttackTarget.position);
        TransformTower.rotation = Quaternion.Euler(new Vector3
            //(0, MySpecialClass.MyMoveTowards(TransformTower.rotation.eulerAngles.y, Angle, RotSpeed), 0));
            (0, Angle, 0));
    }
}