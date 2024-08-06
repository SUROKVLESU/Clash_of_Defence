using System;
using System.Collections;
using UnityEngine;

public class BaseCharacteristics:MonoBehaviour, IBaseInterface
{
    [SerializeField] protected float HP;
    [SerializeField] protected Attributes Protection;

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
}
public interface IBaseInterface
{
    public bool TakingDamage(Attributes damage);
}