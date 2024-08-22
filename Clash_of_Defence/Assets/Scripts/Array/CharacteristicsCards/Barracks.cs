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
    public override void ActivationBuildings()
    {
        Unit.ActivationBuildings();
    }
    public override void ResetHP()
    {
        base.ResetHP();
        Unit.ResetHP();
    }
}

