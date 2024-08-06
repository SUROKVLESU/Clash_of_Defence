using System;
using System.Collections;
using UnityEngine;
public class BaseEnemyCharacteristics:AttackingBuildingCharacteristics,IMovement
{
    [SerializeField] protected float SpeedMovement;
    protected Coroutine Coroutine;
    protected event Action ActionMovement;

    public virtual void Move(Vector3 vector)
    {
        Coroutine = StartCoroutine(MovementCoroutine(vector));
    }
    public override bool TakingDamage(Attributes damage)
    {
        HP -= damage - Protection;
        if (HP < 0)
        {
            GameController.instance.EnemiesController.Enemies.Remove(gameObject);
            Destroy(gameObject);
            return false;
        }
        else return true;
    }
    protected virtual IEnumerator MovementCoroutine(Vector3 vector)
    {
        while (true)
        {
            if (!TransformAttackTarget.gameObject.activeSelf)
            {
                SearchAttackTarget();
                Move(TransformAttackTarget.position);
                yield break;
            }
            transform.position = Vector3.MoveTowards(transform.position , vector, SpeedMovement*Time.deltaTime);
            if((vector - transform.position).sqrMagnitude<= (AttackRadius * AttackRadius)) break;
            yield return null;
        }
        ActionMovement();
    }
    protected override IEnumerator AttackCoroutine()
    {
        while (true)
        {
            if (!TransformAttackTarget.gameObject.activeSelf)
            {
                SearchAttackTarget();
                Move(TransformAttackTarget.position);
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
    private void Awake()
    {
        ActionMovement += Attack;
    }
}
public interface IMovement
{
    public void Move(Vector3 vector);
}