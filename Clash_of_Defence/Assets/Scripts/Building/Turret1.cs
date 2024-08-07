

public class Turret1:AttackingBuildingCharacteristics
{
    private void Start()
    {
        Attack();
    }
    public override void ActivationBuildings()
    {
        base.ActivationBuildings();
        Attack();
    }
}

