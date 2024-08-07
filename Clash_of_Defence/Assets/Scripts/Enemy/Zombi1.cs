using UnityEngine;

public class Zombi1:BaseEnemyCharacteristics
{
    private void Start()
    {
        SearchAttackTarget();
        Move();
    }
    public override void ActivationBuildings()
    {
        base.ActivationBuildings();
        SearchAttackTarget();
        Move();
    }
}

