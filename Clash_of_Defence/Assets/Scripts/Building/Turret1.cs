using System;
using System.Collections;
using UnityEngine;

public class Turret1 : AttackingBuildingCharacteristics
{
    /*protected Transform TransformTower;
    protected float Angle;
    protected const float RotSpeed = 100f;
    protected AudioSource AudioSource;
    protected Animator Animator;
    [SerializeField] protected string AnimationName;
    private void Start()
    {
        MyStart();
    }
    public override void MyStart()
    {
        TransformTower = transform.GetChild(0);
        AudioSource = GetComponent<AudioSource>();
        Animator = GetComponent<Animator>();
        ActivationBuildings();
    }
    public override void ActivationBuildings()
    {
        base.ActivationBuildings();
        Coroutine = StartCoroutine(AimingTargetCoroutine());
    }
    protected virtual void TurningTower()
    {
        if(TransformAttackTarget == null) return;
        Angle = MySpecialClass.GetAngleTarget(transform.position, TransformAttackTarget.position);
        TransformTower.rotation= Quaternion.Euler(new Vector3
            (0,MySpecialClass.MyMoveTowards(TransformTower.rotation.eulerAngles.y, Angle,RotSpeed),0));
    }
    protected virtual IEnumerator AimingTargetCoroutine()
    {
        while (true)
        {
            if (TransformAttackTarget == null)
            {
                SearchAttackTarget();
                yield return null;
                continue;
            }
            TurningTower();
            if ((int)TransformTower.rotation.eulerAngles.y==(int)Angle) break;
            yield return null;
        }
        Coroutine = StartCoroutine(AttackCoroutine());
    }
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
            if (TransformAttackTarget == null) continue;
            AudioSource.PlayOneShot(AudioSource.clip);
            Animator.Play(AnimationName);
            if (!AttackTarget.TakingDamage(Damage))
            {
                TransformAttackTarget = null;
                AttackTarget = null;
                Coroutine = StartCoroutine(AimingTargetCoroutine());
                yield break;
            }
        }
    }*/
}

