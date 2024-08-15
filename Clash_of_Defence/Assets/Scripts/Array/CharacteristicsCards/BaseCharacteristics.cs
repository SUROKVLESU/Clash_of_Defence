using UnityEngine;

public class BaseCharacteristics : MonoBehaviour, IBaseInterface
{
    [SerializeField] protected float HP;
    [SerializeField] protected Attributes Protection;
    protected float HPStart;
    [SerializeField] protected Transform AttackTargetPosition;
    [SerializeField] protected Transform LForder;
    [SerializeField] protected Transform RForder;
    [SerializeField] protected GameObject WallGameObject;
    GameObject IBaseInterface.WallGameObject { get { return WallGameObject; } set { WallGameObject = value; } }

    public TypeBuilding TypeBuilding;
    public Transform GetAttackTargetPosition()
    {
        return AttackTargetPosition;
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

    public virtual bool TakingDamage(Attributes damage)
    {
        HP -= damage - Protection;
        if (HP < 0)
        {
            gameObject.SetActive(false);
            return false;
        }
        else return true;
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
    public TypeBuilding GetTypeBuilding()
    {
        return TypeBuilding;
    }
}
public interface IBaseInterface
{
    public bool TakingDamage(Attributes damage);
    public void ResetHP();
    public void ActivationBuildings();
    public void Stop();
    public float GetHP();
    public Transform GetForder(bool lForder);
    public Transform GetAttackTargetPosition();
    public GameObject WallGameObject { get; set; }
    public TypeBuilding GetTypeBuilding();
}