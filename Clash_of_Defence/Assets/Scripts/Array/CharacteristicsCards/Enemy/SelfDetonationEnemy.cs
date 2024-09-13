using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class SelfDetonationEnemy:BaseEnemyCharacteristics
{
    [SerializeField] float ExplosionRadius;
    [SerializeField] protected GameObject Explosion;

    protected override IEnumerator AttackCoroutine()
    {
        while (true)
        {
            Animator.StopPlayback();
            Animator.speed = (1 / AttackReloading);
            Animator.SetBool("Attack", true);
            ExplosionAttackCoroutine();
            yield return new WaitForSeconds(AttackReloading);
        }
    }
    protected virtual void ExplosionAttackCoroutine()
    {
        List<IBaseInterface> baseInterfaces = new();
        StartCoroutine(MortarExplosionCoroutine(transform.position));
        for (int i = 0; i < GameController.instance.MapController.Buildings.Count; i++)
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
        Destroy(gameObject);
    }
}

