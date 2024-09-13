using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class AttackingCharacteristics:BaseCharacteristics
{
    [SerializeField] protected Attributes Damage;
    [SerializeField] protected Attributes SustainedDamage;
    [SerializeField] protected int TimeSustainedDamage;
    protected bool IsSustainedDamage=false;
    [SerializeField] protected float AttackReloading;
    [SerializeField] protected float AttackRadius;
    [SerializeField] protected Transform TransformAttackRadius;
    protected IBaseInterface AttackTarget;
    protected Transform TransformAttackTarget;
    protected Coroutine Coroutine;
    protected Coroutine CoroutinePause;
    protected Transform TransformTower;
    protected float Angle;
    protected const float RotSpeed = 400f;
    protected AudioSource AudioSource;
    protected Animator Animator;
    [SerializeField] protected string AnimationName;
    private void Update()
    {
        Debug.Log(Coroutine);
        if (Coroutine!=null)
        Debug.Log(Coroutine.GetHashCode());
        Debug.Log("=========================================");
    }
    public override void PrintAttackRadius(bool on)
    {
        TransformAttackRadius.gameObject.SetActive(on);
    }
    private void Start()
    {
        MyStart();
    }
    public override void MyStart()
    {
        TransformTower = transform.GetChild(0);
        if (TransformAttackRadius!=null)
        {
            TransformAttackRadius.localScale = new Vector3(AttackRadius*2, 1, AttackRadius*2);
        }
        AudioSource = GetComponent<AudioSource>();
        Animator = GetComponent<Animator>();
        if (SustainedDamage>0)
        {
            IsSustainedDamage = true;
        }
        Coroutine = StartCoroutine(SearchAttackTargetCoroutine());
    }
    protected virtual void DeadTarget()
    {
        TransformAttackTarget = null;
        AttackTarget = null;
        StopCoroutine(Coroutine);
        Coroutine = StartCoroutine(SearchAttackTargetCoroutine());
    }
    public virtual void SearchAttackTarget(List<GameObject> target)
    {
        if (target.Count==0) return;
        GameObject attackTarget = target[0].GetComponent<IBaseInterface>().GetAttackTargetPosition().gameObject;
        for (int i = 0; i < target.Count; i++)
        {
            if ((target[i].transform.position - transform.position).sqrMagnitude
                <= (AttackRadius * AttackRadius) && (TypeAttack == TypeAttack.All||TypeAttack==
                target[i].GetComponent<IBaseInterface>().GetTypeTarget()))
            {
                if ((target[i].transform.position - transform.position).sqrMagnitude
                <= (attackTarget.transform.position - transform.position).sqrMagnitude)
                {
                    attackTarget = target[i].GetComponent<IBaseInterface>().GetAttackTargetPosition().gameObject;
                }
            }
        }
        if ((attackTarget.transform.position - transform.position).sqrMagnitude> (AttackRadius * AttackRadius))
            return;
        TransformAttackTarget = attackTarget.transform;
        AttackTarget = TransformAttackTarget.parent.GetComponent<IBaseInterface>();
        AttackTarget.AddDead(DeadTarget);
    }
    protected virtual IEnumerator SearchAttackTargetCoroutine()
    {
        while (true)
        {
            SearchAttackTarget(GameController.instance.EnemiesController.Enemies);
            if (AttackTarget != null)
            {
                break;
            }
            yield return new WaitForSeconds(0.2f);
        }
        Coroutine = StartCoroutine(AimingTargetCoroutine());
    }
    protected virtual IEnumerator AttackCoroutine()
    {
        while (true)
        {
            AudioSource.PlayOneShot(AudioSource.clip);
            Animator.Play(AnimationName);
            AttackTarget.TakingDamage(Damage);
            if (IsSustainedDamage) AttackTarget.TakingSustainedDamage(SustainedDamage, TimeSustainedDamage);
            yield return new WaitForSeconds(AttackReloading);
            if (AttackTarget.IsMove())
            {
                Coroutine = StartCoroutine(SearchAttackTargetCoroutine());
                yield break;
            }
        }
    }
    protected virtual IEnumerator AimingTargetCoroutine()
    {
        while (true)
        {
            TurningTower();
            if (Mathf.Abs(TransformTower.rotation.eulerAngles.y - Angle) <= 10) break;
            yield return null;
        }
        Coroutine = StartCoroutine(AttackCoroutine());
    }
    protected virtual void TurningTower()
    {
        if (TransformAttackTarget == null) return;
        Angle = MySpecialClass.GetAngleTarget(transform.position, TransformAttackTarget.position);
        TransformTower.rotation = Quaternion.Euler(new Vector3
            (0, MySpecialClass.MyMoveTowards(TransformTower.rotation.eulerAngles.y, Angle, RotSpeed), 0));
    }
    public override void Activation() 
    {
        if (CoroutinePause != null)
        {
            StopCoroutine(CoroutinePause);
            CoroutinePause = StartCoroutine(OffPause());
        }
        else
        {
            CoroutinePause = StartCoroutine(OffPause());
        }
    }
    public override void Stop()
    {
        if (Coroutine == null) return;
        StopCoroutine(Coroutine);
        Coroutine = null;
    }
    protected virtual IEnumerator OffPause()
    {
        yield return new WaitForSeconds(AttackReloading);
        if (GameController.instance.IsPause) yield break;
        Coroutine = StartCoroutine(SearchAttackTargetCoroutine());
        CoroutinePause = null;
    }
}
