using UnityEngine;

public class Zombi1:BaseEnemyCharacteristics
{
    private void Start()
    {
        SearchAttackTarget();
        Move(TransformAttackTarget.position);
    }
}

