using UnityEngine;

public class Zombi1:BaseEnemyCharacteristics
{
    private void Start()
    {
        MyStart();
    }
    public override void MyStart()
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

