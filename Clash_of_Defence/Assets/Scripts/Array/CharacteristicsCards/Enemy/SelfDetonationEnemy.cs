using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class SelfDetonationEnemy:BaseEnemyCharacteristics
{
    [SerializeField] float ExplosionRadius;
    [SerializeField] protected GameObject Explosion;

    protected override IEnumerator AttackCoroutine()
    {
        if (!TransformAttackTarget.parent.gameObject.activeSelf) yield break;
        Vector3 oldTransformAttackTarget = TransformAttackTarget.position;
        while (true)
        {
            if (!TransformAttackTarget.parent.gameObject.activeSelf)
            {
                SearchAttackTarget();
                Move();
                Animator.speed = 1;
                Animator.SetBool("Attack", false);
                yield break;
            }
            if (oldTransformAttackTarget != TransformAttackTarget.position)
            {
                Move();
                Animator.speed = 1;
                Animator.SetBool("Attack", false);
                oldTransformAttackTarget = TransformAttackTarget.position;
                yield break;
            }
            Animator.StopPlayback();
            Animator.speed = (1 / AttackReloading);
            Animator.SetBool("Attack", true);
            //if (!TransformAttackTarget.gameObject.activeSelf) continue;
            ExplosionAttackCoroutine();
            yield return new WaitForSeconds(AttackReloading);
        }
    }
    protected virtual void ExplosionAttackCoroutine()
    {
        List<IBaseInterface> baseInterfaces = new();
        StartCoroutine(MortarExplosionCoroutine(transform.position));
        for (int i = 0; i < GameController.instance.MapController.Buildings.Length; i++)
        {
            if ((GameController.instance.MapController.Buildings[i].transform.position - transform.position).sqrMagnitude
                <= (ExplosionRadius * ExplosionRadius) && (TypeAttack == TypeAttack.All || TypeAttack ==
                GameController.instance.MapController.Buildings[i].GetComponent<IBaseInterface>().GetTypeTarget()))
            {
                baseInterfaces.Add(GameController.instance.MapController.Buildings[i].GetComponent<IBaseInterface>());
            }
        }
        if ((GameController.instance.MapController.MainBuilding.transform.position - transform.position).sqrMagnitude
                <= (ExplosionRadius * ExplosionRadius) && (TypeAttack == TypeAttack.All || TypeAttack ==
                GameController.instance.MapController.MainBuilding.GetComponent<IBaseInterface>().GetTypeTarget()))
        {
            baseInterfaces.Add(GameController.instance.MapController.MainBuilding.GetComponent<IBaseInterface>());
        }
        foreach (var item in baseInterfaces)
        {
            item.TakingDamage(Damage);
            if (IsSustainedDamage) AttackTarget.TakingSustainedDamage(SustainedDamage, TimeSustainedDamage);
        }
    }
    protected virtual IEnumerator MortarExplosionCoroutine(Vector3 transform)
    {
        float CountTick = 5;
        float ExplosionTime = 0.5f;
        GameObject explosion = Instantiate(Explosion);
        explosion.transform.position = transform;
        float delta = (ExplosionRadius - 1) / CountTick;
        Vector3 vector3 = new Vector3(delta, delta, delta);
        for (int i = 0; i < CountTick; i++)
        {
            explosion.transform.localScale += vector3;
            yield return new WaitForSeconds(ExplosionTime / CountTick);
        }
        Destroy(explosion);
    }
}

