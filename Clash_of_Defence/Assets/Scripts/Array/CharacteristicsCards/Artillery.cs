using UnityEngine;
using System.Collections;
public class Artillery:AttackingCharacteristics
{
    [SerializeField] protected float SqrProjectileVelocity;
    protected override IEnumerator AttackCoroutine()
    {
        if (TransformAttackTarget == null)
        {
            Coroutine = StartCoroutine(AimingTargetCoroutine());
            yield break;
        }
        if (GameController.instance.IsPause) { yield break; }
        AudioSource.PlayOneShot(AudioSource.clip);
        Animator.Play(AnimationName);
        StartCoroutine(AartilleryAttackCoroutine());
        yield return new WaitForSeconds(AttackReloading);
        Coroutine = StartCoroutine(AimingTargetCoroutine());
        yield break;
    }
    protected virtual IEnumerator AartilleryAttackCoroutine()
    {
        yield return new WaitForSeconds((TransformAttackTarget.position-transform.position).sqrMagnitude/SqrProjectileVelocity);
        if(TransformAttackTarget == null) yield break;
        AttackTarget.TakingDamage(Damage);
        if (IsSustainedDamage) AttackTarget.TakingSustainedDamage(SustainedDamage, TimeSustainedDamage);
        if (AttackTarget.GetHP()<0)
        {
            StopCoroutine(Coroutine);
            TransformAttackTarget = null;
            AttackTarget = null;
            Activation();
        }
    }
}

