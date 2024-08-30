using System;
using System.Collections;
using UnityEngine;

public class SpawnEnemisEnemy:BaseEnemyCharacteristics
{
    [SerializeField] GameObject[] SpawnEnemy;
    [SerializeField] float SpawnReloadingEnemy;
    [SerializeField] Transform SpawnEnemyPosition;
    [SerializeField] GameObject SpawnEnemyEgg;
    protected virtual IEnumerator SpawnEnemyCoroutine()
    {
        while (true)
        {
            if (!GameController.instance.IsPause)
            {
                Transform enemy = Instantiate(SpawnEnemy[UnityEngine.Random.Range(0,SpawnEnemy.Length)]).transform;
                enemy.position = SpawnEnemyPosition.position;
                GameController.instance.EnemiesController.Enemies.Add(enemy.gameObject);
                StartCoroutine(SpawnEnemyEggCoroutine());
                yield return new WaitForSeconds(SpawnReloadingEnemy);                
            }
            yield return null;
        }
    }
    public override void MyStart()
    {
        base.MyStart();
        StartCoroutine(SpawnEnemyCoroutine());
    }
    protected IEnumerator SpawnEnemyEggCoroutine()
    {
        Transform egg = Instantiate(SpawnEnemyEgg).transform;
        egg.position = SpawnEnemyPosition.position;
        yield return new WaitForSeconds(1);
        Destroy(egg.gameObject);
    }
}

