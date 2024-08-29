using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mortar:Artillery
{
    [SerializeField] protected float MortarRadius;
    [SerializeField] protected GameObject Explosion;
    protected float ExplosionTime = 0.5f;
    protected int CountTick = 5;
    protected override IEnumerator AartilleryAttackCoroutine()
    {
        List<IBaseInterface> baseInterfaces = new();
        Vector3 transformV = TransformAttackTarget.position;
        yield return new WaitForSeconds((TransformAttackTarget.position - transform.position).sqrMagnitude / SqrProjectileVelocity);
        if (TransformAttackTarget!= null)
        {
            transformV = TransformAttackTarget.position;
        }
        StartCoroutine(MortarExplosionCoroutine(transformV));
        for (int i = 0; i < GameController.instance.EnemiesController.Enemies.Count; i++)
        {
            if ((GameController.instance.EnemiesController.Enemies[i].transform.position - transformV).sqrMagnitude
                <= (MortarRadius * MortarRadius) && (TypeAttack == TypeAttack.All || TypeAttack ==
                GameController.instance.EnemiesController.Enemies[i].GetComponent<IBaseInterface>().GetTypeTarget()))
            {
                baseInterfaces.Add(GameController.instance.EnemiesController.Enemies[i].GetComponent<IBaseInterface>());
            }
        }
        foreach (var item in baseInterfaces)
        {
            item.TakingDamage(Damage);
            if (IsSustainedDamage) AttackTarget.TakingSustainedDamage(SustainedDamage, TimeSustainedDamage);
        }
        if (AttackTarget.GetHP() < 0)
        {
            StopCoroutine(Coroutine);
            TransformAttackTarget = null;
            AttackTarget = null;
            ActivationBuildings();
        }
    }
    protected virtual IEnumerator MortarExplosionCoroutine(Vector3 transform)
    {
        GameObject explosion = Instantiate(Explosion);
        explosion.transform.position = transform;
        float delta = (MortarRadius - 1) / CountTick;
        Vector3 vector3 = new Vector3(delta, delta, delta);
        for (int i = 0; i < CountTick; i++)
        {
            explosion.transform.localScale += vector3;
            yield return new WaitForSeconds(ExplosionTime/ CountTick);
        }
        Destroy(explosion);
    }
}
