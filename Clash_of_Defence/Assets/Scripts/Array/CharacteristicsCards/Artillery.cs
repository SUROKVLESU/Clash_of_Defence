using UnityEngine;
using System.Collections;
public class Artillery:AttackingBuildingCharacteristics
{
    [SerializeField] protected float SqrProjectileVelocity;
    protected override IEnumerator AttackCoroutine()
    {
        while (true)
        {
            if (TransformAttackTarget == null)
            {
                Coroutine = StartCoroutine(AimingTargetCoroutine());
                yield break;
            }
            yield return new WaitForSeconds(AttackReloading);
            if (GameController.instance.IsPause) { yield break; }
            if (TransformAttackTarget == null) continue;
            AudioSource.PlayOneShot(AudioSource.clip);
            Animator.Play(AnimationName);
            StartCoroutine(AartilleryAttackCoroutine());
        }
    }
    protected virtual IEnumerator AartilleryAttackCoroutine()
    {
        yield return new WaitForSeconds((TransformAttackTarget.position-transform.position).sqrMagnitude/SqrProjectileVelocity);
        if(TransformAttackTarget == null) yield break;
        AttackTarget.TakingDamage(Damage);
        if (AttackTarget.GetHP()<0)
        {
            StopCoroutine(Coroutine);
            TransformAttackTarget = null;
            AttackTarget = null;
            ActivationBuildings();
        }
    }
}

