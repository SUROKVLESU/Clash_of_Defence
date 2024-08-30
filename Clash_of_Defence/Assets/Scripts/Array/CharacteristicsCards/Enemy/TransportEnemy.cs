using System.Collections;
using UnityEngine;

public class TransportEnemy:BaseEnemyCharacteristics
{
    [SerializeField] GameObject[] SpawnEnemy;
    [SerializeField] int CountEnemy;
    [SerializeField] Transform SpawnEnemyPosition;

    protected override void Defeat()
    {
        SpawnEnemies();
        base.Defeat();
    }
    protected virtual void SpawnEnemies()
    {
        for (int i = 0; i < CountEnemy; i++)
        {
            Transform enemy = Instantiate(SpawnEnemy[UnityEngine.Random.Range(0, SpawnEnemy.Length)]).transform;
            enemy.position = SpawnEnemyPosition.position;
            GameController.instance.EnemiesController.Enemies.Add(enemy.gameObject);
        }
    }
}

