using System.Collections;
using UnityEngine;

public class EnemyGun:BaseCharacteristics,IAttackInterface
{
    [SerializeField] protected Attributes Damage;
    [SerializeField] protected Attributes SustainedDamage;
    [SerializeField] protected int TimeSustainedDamage;
    protected bool IsSustainedDamage = false;
    [SerializeField] protected float AttackReloading;
    [SerializeField] protected float AttackRadius;
    protected IBaseInterface AttackTarget;
    protected Transform TransformAttackTarget;
    protected Coroutine Coroutine;
    protected Coroutine CoroutinePause;
    protected Transform TransformTower;
    protected float Angle = -12;
    protected const float RotSpeed = 400f;
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
        if (SustainedDamage > 0)
        {
            IsSustainedDamage = true;
        }
        ActivationBuildings();
    }
    public virtual void SearchAttackTarget()
    {
        GameObject attackTarget = GameController.instance.MapController.MainBuilding.GetComponent<IBaseInterface>().GetAttackTargetPosition().gameObject;
        for (int i = 0; i < GameController.instance.MapController.Buildings.Length; i++)
        {
            if ((GameController.instance.MapController.Buildings[i].transform.position - transform.position).sqrMagnitude
                <= (AttackRadius * AttackRadius) && (TypeAttack == TypeAttack.All || TypeAttack ==
                GameController.instance.MapController.Buildings[i].GetComponent<IBaseInterface>().GetTypeTarget()))
            {
                if ((GameController.instance.MapController.Buildings[i].transform.position - transform.position).sqrMagnitude
                <= (attackTarget.transform.position - transform.position).sqrMagnitude)
                {
                    attackTarget = GameController.instance.MapController.Buildings[i].GetComponent<IBaseInterface>().GetAttackTargetPosition().gameObject;
                }
            }
        }
        if ((attackTarget.transform.position - transform.position).sqrMagnitude > (AttackRadius * AttackRadius))
            return;
        TransformAttackTarget = attackTarget.transform;
        AttackTarget = TransformAttackTarget.parent.GetComponent<IBaseInterface>();
    }
    public virtual void Attack() { }
    protected virtual IEnumerator AttackCoroutine()
    {
        if (!TransformAttackTarget.parent.gameObject.activeSelf)
        {
            Coroutine = StartCoroutine(AimingTargetCoroutine());
            yield break;
        }
        if (GameController.instance.IsPause) { yield break; }
        AudioSource.PlayOneShot(AudioSource.clip);
        Animator.Play(AnimationName);
        AttackTarget.TakingDamage(Damage);
        if (IsSustainedDamage) AttackTarget.TakingSustainedDamage(SustainedDamage, TimeSustainedDamage);
        yield return new WaitForSeconds(AttackReloading);
        Coroutine = StartCoroutine(AimingTargetCoroutine());
        yield break;
    }
    protected virtual IEnumerator AimingTargetCoroutine()
    {
        while (true)
        {
            if (GameController.instance.IsPause) { yield break; }
            if (!TransformAttackTarget.parent.gameObject.activeSelf)
            {
                SearchAttackTarget();
                yield return null;
                continue;
            }
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
    public override void ActivationBuildings()
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
        //TransformAttackTarget = null;
        //AttackTarget = null;
        if (Coroutine == null) return;
        StopCoroutine(Coroutine);
        Coroutine = null;
    }
    protected virtual IEnumerator OffPause()
    {
        yield return new WaitForSeconds(AttackReloading);
        if (GameController.instance.IsPause) yield break;
        Coroutine = StartCoroutine(AimingTargetCoroutine());
        CoroutinePause = null;
    }
}

