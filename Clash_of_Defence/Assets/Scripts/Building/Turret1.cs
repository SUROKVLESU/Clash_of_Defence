using System;
using System.Collections;
using UnityEngine;

public class Turret1:AttackingBuildingCharacteristics
{
    protected Transform TransformTower;
    protected float Angle;
    protected const float RotSpeed = 270f;
    private void Start()
    {
        TransformTower = transform.GetChild(0);
        ActivationBuildings();
    }
    public override void ActivationBuildings()
    {
        base.ActivationBuildings();
        Coroutine = Coroutine = StartCoroutine(AimingTargetCoroutine());
    }
    protected virtual void TurningTower()
    {
        if(TransformAttackTarget == null) return;
        Angle = MySpecialClass.GetAngleTarget(transform.position, TransformAttackTarget.position);
        TransformTower.rotation= Quaternion.Euler(Vector3.MoveTowards(TransformTower.rotation.eulerAngles,
            new Vector3(0,Angle,0), RotSpeed * Time.deltaTime));
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
            if (!AttackTarget.TakingDamage(Damage))
            {
                TransformAttackTarget = null;
                AttackTarget = null;
            }
            yield return new WaitForSeconds(AttackReloading);
        }
    }
}

