using System;
using System.Collections;
using System.Reflection.Emit;
using UnityEngine;
[Serializable]
public class BaseCharacteristics : MonoBehaviour, IBaseInterface
{
    [SerializeField] protected float HP;
    protected float HPStart;
    [SerializeField] protected Attributes Protection;
    [SerializeField] protected Attributes AddProtection;
    [SerializeField] protected Transform AttackTargetPosition;
    [SerializeField] protected Transform LForder;
    [SerializeField] protected Transform RForder;
    [SerializeField] protected GameObject WallGameObject;
    GameObject IBaseInterface.WallGameObject { get { return WallGameObject; } set { WallGameObject = value; } }
    Attributes IBaseInterface.AddProtection { get { return AddProtection; } set { AddProtection = value; } }
    [SerializeField] protected TypeAttack TypeBuilding;
    [SerializeField] protected TypeAttack TypeAttack;
    [SerializeField] protected TypeBuildings TypeBuildings;
    [SerializeField] protected GameObject[] SustainedDamagePanel;
    protected int CountAxidDamage;
    protected int CountIceDamage;
    [SerializeField] protected GameObject[] DamageProtectionPanel;
    [SerializeField] protected GameObject TakingDamagenPanel;
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
    public virtual float GetHP()
    {
        return HP;
    }

    public void TakingDamage(Attributes damage)
    {
        Attributes attributes = damage - Protection - AddProtection;
        if (attributes.Physical < 0.105&&damage.Physical>0.1)
        {
            DamageProtectionPanel[0].SetActive(true);
        }
        if (attributes.Acid < 0.105 && damage.Acid > 0.1)
        {
            DamageProtectionPanel[1].SetActive(true);
        }
        if (attributes.Ice < 0.105 && damage.Ice > 0.1)
        {
            DamageProtectionPanel[2].SetActive(true);
        }
        foreach (var item in DamageProtectionPanel)
        {
            StartCoroutine(OffProtectedPanel(item));
        }
        StartCoroutine(HpPanel());
        HP -= attributes;
        if (HP < 0)
        {
            Defeat();
        }
        IEnumerator OffProtectedPanel(GameObject gameObject)
        {
            yield return new WaitForSeconds(0.1f);
            gameObject.SetActive(false);
        }
        IEnumerator HpPanel()
        {
            TakingDamagenPanel.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            TakingDamagenPanel.SetActive(false);
        }
    }
    protected virtual void Defeat() 
    { 
        gameObject.SetActive(false);
    }
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
    public virtual void MyUpdate(IEnumerator enumerator) { }
    public virtual void PrintAttackRadius(bool on) { }
    protected virtual IEnumerator SustainedDamageCoroutine(Attributes attributes, int time)
    {
        if (attributes.Ice>0)
        {
            SustainedDamagePanel[1].SetActive(true);
            CountIceDamage++;
}
        if (attributes.Acid > 0)
        {
            SustainedDamagePanel[0].SetActive(true);
            CountAxidDamage++;
        }
        int countTaking = time;
        while (true)
        {
            if (!GameController.instance.IsPause)
            {
                TakingDamage(attributes / time);
                countTaking--;
                yield return new WaitForSeconds(1);
                if (countTaking == 0)
                {
                    if (attributes.Ice > 0)
                    {
                        if(--CountIceDamage==0) SustainedDamagePanel[1].SetActive(false);
                    }
                    if (attributes.Acid > 0)
                    {
                        if (--CountAxidDamage == 0) SustainedDamagePanel[0].SetActive(false);
                    }
                    yield break;
                }
            }
            yield return null;
        }
    }
    public virtual void TakingSustainedDamage(Attributes attributes,int time)
    {
        StartCoroutine(SustainedDamageCoroutine(attributes,time));
    }
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
    public Attributes AddProtection {  get; set; }
    public TypeAttack GetTypeTarget();
    public TypeAttack GetTypeAttack();
    public TypeBuildings GetTypeBuilder();
    public void MyStart();
    public void MyUpdate(IEnumerator enumerator);
    //public void Defeat();
    public void PrintAttackRadius(bool on);
    public void TakingSustainedDamage(Attributes attributes, int time);
}