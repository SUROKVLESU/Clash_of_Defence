using System.Collections;
using UnityEngine;

public class HelicopterUnit: BaseEnemyCharacteristics
{
    public override void SearchAttackTarget()
    {
        if (GameController.instance.EnemiesController.Enemies.Count == 0) return;
        GameObject attackTarget = GameController.instance.EnemiesController.Enemies[0];
        for (int i = 0; i < GameController.instance.EnemiesController.Enemies.Count; i++)
        {
            if ((GameController.instance.EnemiesController.Enemies[i].transform.position - transform.position).sqrMagnitude
                <= (attackTarget.transform.position - transform.position).sqrMagnitude)
            {
                attackTarget = GameController.instance.EnemiesController.Enemies[i];
            }
        }
        TransformAttackTarget = attackTarget.transform;
        AttackTarget = TransformAttackTarget.GetComponent<IBaseInterface>();
        TransformAttackTarget = AttackTarget.GetAttackTargetPosition();
    }
    protected override IEnumerator MovementCoroutine()
    {
        if (TransformAttackTarget == null)
        {
            SearchAttackTarget();
            yield return null;
            Move();
            yield break;
        }
        Vector3 oldTransformAttackTarget = TransformAttackTarget.position;
        StartCoroutine(AimingTargetCoroutine());
        while (true)
        {
            if (oldTransformAttackTarget != TransformAttackTarget.position)
            {
                StartCoroutine(AimingTargetCoroutine());
                oldTransformAttackTarget = TransformAttackTarget.position;
            }
            if (TransformAttackTarget == null)
            {
                SearchAttackTarget();
                yield return null;
                Move();
                yield break;
            }
            transform.position = Vector3.MoveTowards
                (transform.position, TransformAttackTarget.position, SpeedMovement * Time.deltaTime);
            if (transform.position.x + AttackRadius > AttackTarget.GetForder(true).position.x
                && transform.position.x - AttackRadius < AttackTarget.GetForder(false).position.x
                && transform.position.z - AttackRadius < AttackTarget.GetForder(true).position.z
                && transform.position.z + AttackRadius > AttackTarget.GetForder(false).position.z) break;
            yield return null;
        }
        Coroutine = StartCoroutine(AttackCoroutine());
    }
    private void Start()
    {
        MyStart();
    }
    public override void MyStart()
    {
        SearchAttackTarget();
        Move();
    }
    public override void ActivationBuildings()
    {
        base.ActivationBuildings();
        SearchAttackTarget();
        Move();
    }
}

