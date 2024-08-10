using System.Collections;
using UnityEngine;

public class SpawnEnemiesController:MonoBehaviour
{
    private const float DistanceSpawn = 40;
    private const float DepthSpawn = 5;//пока не используется(глубина спавна)
    private const float SpawnReloading = 6;
    private Coroutine Coroutine;
    private const float MaxStartFraction = 0.5f;
    private BaseEssenceObject[] EnemiesSpawn = new BaseEssenceObject[0];
    private int OldCountSpawnEnemies;
    public void SpawnEnemies(BaseEssenceObject[] enemies)
    {
        EnemiesSpawn = enemies;
        Coroutine = StartCoroutine(SpawnEnemiesCoroutine(enemies));
    }
    private IEnumerator SpawnEnemiesCoroutine(BaseEssenceObject[] enemies,int startIndex=0)
    {
        int countSpawnEnemies = UnityEngine.Random.Range(1, (int)((enemies.Length + 1)* MaxStartFraction));
        int oldCountSpawnEnemies = countSpawnEnemies;
        OldCountSpawnEnemies = oldCountSpawnEnemies;
        for (int i = startIndex; i < enemies.Length; i++)
        {
            GameObject enemy = Instantiate(enemies[i].GameObjects[enemies[i].GetLevel()]);
            enemy.transform.position = GetRandomPosition();
            GameController.instance.EnemiesController.Enemies.Add(enemy);
            countSpawnEnemies--;
            OldCountSpawnEnemies++;
            if (countSpawnEnemies == 0)
            {
                countSpawnEnemies = UnityEngine.Random.Range(1, enemies.Length + 1-oldCountSpawnEnemies);
                if (countSpawnEnemies+oldCountSpawnEnemies > enemies.Length) 
                    countSpawnEnemies= enemies.Length-oldCountSpawnEnemies;
                oldCountSpawnEnemies += countSpawnEnemies;
                yield return new WaitForSeconds(SpawnReloading);
            }
        }
        EnemiesSpawn = new BaseEssenceObject[0];
    }
    private Vector3 GetRandomPosition()
    {
        switch (Random.Range(1,5))
        {
            case 1:
                return new Vector3
                    (GameController.instance.MapController.FrontierMap.x - DistanceSpawn, 0,
                    Random.Range(GameController.instance.MapController.FrontierMap.w- DistanceSpawn,
                    GameController.instance.MapController.FrontierMap.z+ DistanceSpawn));
            case 2:
                return new Vector3
                    (GameController.instance.MapController.FrontierMap.y + DistanceSpawn, 0,
                    Random.Range(GameController.instance.MapController.FrontierMap.w- DistanceSpawn,
                    GameController.instance.MapController.FrontierMap.z+ DistanceSpawn));
            case 3:
                return new Vector3
                    (Random.Range(GameController.instance.MapController.FrontierMap.x- DistanceSpawn,
                    GameController.instance.MapController.FrontierMap.y+ DistanceSpawn),0,
                    GameController.instance.MapController.FrontierMap.z + DistanceSpawn);
            case 4:
                return new Vector3
                    (Random.Range(GameController.instance.MapController.FrontierMap.x- DistanceSpawn,
                    GameController.instance.MapController.FrontierMap.y+ DistanceSpawn), 0,
                    GameController.instance.MapController.FrontierMap.w - DistanceSpawn);
            default:
                return Vector3.zero;
        }
    }
    public void Pause()
    {
        if(EnemiesSpawn.Length == 0)return;
        StopCoroutine(Coroutine);
    }
    public void OffPause()
    {
        if (EnemiesSpawn.Length == 0) return;
        Coroutine = StartCoroutine(SpawnEnemiesCoroutine(EnemiesSpawn,OldCountSpawnEnemies-1));
    }
    public bool IsAllSpawnEnemies()
    {
        if(EnemiesSpawn.Length > 0) return false;
        else return true;
    }
}

