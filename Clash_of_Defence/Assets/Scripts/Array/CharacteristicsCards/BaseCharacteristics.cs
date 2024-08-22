using System;
using System.Reflection.Emit;
using UnityEngine;
[Serializable]
public class BaseCharacteristics : MonoBehaviour, IBaseInterface
{
    [SerializeField] protected float HP;
    protected float HPStart;
    [SerializeField] protected Attributes Protection;
    [SerializeField] protected Transform AttackTargetPosition;
    [SerializeField] protected Transform LForder;
    [SerializeField] protected Transform RForder;
    [SerializeField] protected GameObject WallGameObject;
    GameObject IBaseInterface.WallGameObject { get { return WallGameObject; } set { WallGameObject = value; } }

    [SerializeField] protected TypeAttack TypeBuilding;
    [SerializeField] protected TypeAttack TypeAttack;
    [SerializeField] protected TypeBuildings TypeBuildings;
    public Transform GetAttackTargetPosition()
    {
        return AttackTargetPosition;
    }
    public TypeAttack GetTypeAttack()
    {
        return TypeAttack;
    }
    public TypeBuildings GetTypeBuilder()
    {
        return TypeBuildings;
    }
    public Transform GetForder(bool lForder)
    {
        if(lForder)return LForder;
        return RForder;
    }
    public float GetHP()
    {
        return HP;
    }

    public void TakingDamage(Attributes damage)
    {
        HP -= damage - Protection;
        if (HP < 0)
        {
            Defeat();
        }
    }
    public virtual void Defeat() { }
    public virtual void ResetHP()
    {
        HP=HPStart;
    }
    private void Awake()
    {
        HPStart = HP;
    }
    public virtual void ActivationBuildings(){ }
    public virtual void Stop() { }
    public TypeAttack GetTypeTarget()
    {
        return TypeBuilding;
    }
    public virtual void MyStart() { }
}
public interface IBaseInterface
{
    public void TakingDamage(Attributes damage);
    public void ResetHP();
    public void ActivationBuildings();
    public void Stop();
    public float GetHP();
    public Transform GetForder(bool lForder);
    public Transform GetAttackTargetPosition();
    public GameObject WallGameObject { get; set; }
    public TypeAttack GetTypeTarget();
    public TypeAttack GetTypeAttack();
    public TypeBuildings GetTypeBuilder();
    public void MyStart();
    //public void Defeat();
}