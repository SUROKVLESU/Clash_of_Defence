using UnityEngine;
using System.Collections;
public class AttackingBuildingCharacteristics:BaseCharacteristics, IAttackInterface
{
    [SerializeField] protected Attributes Damage;
    [SerializeField] protected float AttackReloading;
    [SerializeField] protected float AttackRadius;
    protected IBaseInterface AttackTarget;
    protected Transform TransformAttackTarget;
    protected Coroutine Coroutine;

    public virtual void SearchAttackTarget()
    {
        if (GameController.instance.EnemiesController.Enemies.Count==0) return;
        GameObject attackTarget = null;
        for (int i = 0; i < GameController.instance.EnemiesController.Enemies.Count; i++)
        {
            if ((GameController.instance.EnemiesController.Enemies[i].transform.position - transform.position).sqrMagnitude
                <= (AttackRadius * AttackRadius))
            {
                attackTarget = GameController.instance.EnemiesController.Enemies[i];
                break;
            }
        }
        if (attackTarget == null) return;
        for (int i = 0; i < GameController.instance.EnemiesController.Enemies.Count; i++)
        {
            if ((GameController.instance.EnemiesController.Enemies[i].transform.position - transform.position).sqrMagnitude
                <= (AttackRadius * AttackRadius))
            {
                if ((GameController.instance.EnemiesController.Enemies[i].transform.position - transform.position).sqrMagnitude
                <= (attackTarget.transform.position - transform.position).sqrMagnitude)
                {
                    attackTarget = GameController.instance.EnemiesController.Enemies[i];
                }
            }
        }
        TransformAttackTarget = attackTarget.transform;
        AttackTarget = TransformAttackTarget.GetComponent<IBaseInterface>();
    }
    public virtual void Attack(){ }
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
    public override void ActivationBuildings() 
    {
        TransformAttackTarget = null;
        AttackTarget = null;
    }
    public override void Stop()
    {
        if (Coroutine == null) return;
        StopCoroutine(Coroutine);
    }
}
public interface IAttackInterface
{
    public void Attack();
    public void SearchAttackTarget();
}