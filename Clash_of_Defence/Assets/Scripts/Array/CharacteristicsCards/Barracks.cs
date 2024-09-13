using UnityEngine;
public class Barracks:BaseCharacteristics
{
    [SerializeField] GameObject GameObjectUnit;
    protected IBaseInterface Unit;
    public override void MyStart()
    {
        Unit = GameObjectUnit.GetComponent<IBaseInterface>();
    }
    private void Start()
    {
        MyStart();
    }
    public override void Stop()
    {
        Unit.Stop();
    }
    public override void Activation()
    {
        GameObjectUnit.SetActive(true);
        Unit.Activation();
    }
    public override void PrintAttackRadius(bool on)
    {
        Unit.PrintAttackRadius(on);
    }
    public override void ResetHP()
    {
        base.ResetHP();
        Unit.ResetHP();
    }
    public override float GetHP()
    {
        return Unit.GetHP();
    }
}

