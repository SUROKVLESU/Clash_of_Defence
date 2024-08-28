using UnityEngine;
using System.Collections;
public class AttackingBuildingCharacteristics:BaseCharacteristics, IAttackInterface
{
    [SerializeField] protected Attributes Damage;
    [SerializeField] protected float AttackReloading;
    [SerializeField] protected float AttackRadius;
    [SerializeField] protected Transform TransformAttackRadius;
    protected IBaseInterface AttackTarget;
    protected Transform TransformAttackTarget;
    protected Coroutine Coroutine;
    protected Coroutine CoroutinePause;
    protected Transform TransformTower;
    protected float Angle=-12;
    protected const float RotSpeed = 400f;
    protected AudioSource AudioSource;
    protected Animator Animator;
    [SerializeField] protected string AnimationName;
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
        ActivationBuildings();
    }
    public virtual void SearchAttackTarget()
    {
        if (GameController.instance.EnemiesController.Enemies.Count==0) return;
        GameObject attackTarget = GameController.instance.EnemiesController.Enemies[0].GetComponent<IBaseInterface>().GetAttackTargetPosition().gameObject;
        for (int i = 0; i < GameController.instance.EnemiesController.Enemies.Count; i++)
        {
            if ((GameController.instance.EnemiesController.Enemies[i].transform.position - transform.position).sqrMagnitude
                <= (AttackRadius * AttackRadius) && (TypeAttack == TypeAttack.All||TypeAttack==
                GameController.instance.EnemiesController.Enemies[i].GetComponent<IBaseInterface>().GetTypeTarget()))
            {
                if ((GameController.instance.EnemiesController.Enemies[i].transform.position - transform.position).sqrMagnitude
                <= (attackTarget.transform.position - transform.position).sqrMagnitude)
                {
                    attackTarget = GameController.instance.EnemiesController.Enemies[i].GetComponent<IBaseInterface>().GetAttackTargetPosition().gameObject;
                }
            }
        }
        if ((attackTarget.transform.position - transform.position).sqrMagnitude> (AttackRadius * AttackRadius))
            return;
        TransformAttackTarget = attackTarget.transform;
        AttackTarget = TransformAttackTarget.parent.GetComponent<IBaseInterface>();
    }
    public virtual void Attack(){ }
    protected virtual IEnumerator AttackCoroutine()
    {
        if (TransformAttackTarget == null)
        {
            Coroutine = StartCoroutine(AimingTargetCoroutine());
            yield break;
        }
        if (GameController.instance.IsPause) { yield break; }
        AudioSource.PlayOneShot(AudioSource.clip);
        Animator.Play(AnimationName);
        AttackTarget.TakingDamage(Damage);
        yield return new WaitForSeconds(AttackReloading);
        Coroutine = StartCoroutine(AimingTargetCoroutine());
        yield break;
    }
    protected virtual IEnumerator AimingTargetCoroutine()
    {
        while (true)
        {
            if (GameController.instance.IsPause) { yield break; }
            if (TransformAttackTarget == null)
            {
                SearchAttackTarget();
                yield return null;
                continue;
            }
            TurningTower();
            if (Mathf.Abs(TransformTower.rotation.eulerAngles.y-Angle) <=10) break;
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
public interface IAttackInterface
{
    public void Attack();
    public void SearchAttackTarget();
}