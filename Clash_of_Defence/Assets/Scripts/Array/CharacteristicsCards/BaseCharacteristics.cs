using UnityEngine;

public class BaseCharacteristics:MonoBehaviour, IBaseInterface
{
    [SerializeField] protected float HP;
    [SerializeField] protected Attributes Protection;
    protected float HPStart;

    public virtual bool TakingDamage(Attributes damage)
    {
        HP -= damage - Protection;
        if (HP < 0)
        {
            gameObject.SetActive(false);
            GameController.instance.MapController.ActiveCount--;
            if (GameController.instance.MapController.ActiveCount == 0)
            {
                GameController.instance.WaveController.PlayerDefeat();
            }
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
}
public interface IBaseInterface
{
    public bool TakingDamage(Attributes damage);
    public void ResetHP();
    public void ActivationBuildings();
    public void Stop();
}