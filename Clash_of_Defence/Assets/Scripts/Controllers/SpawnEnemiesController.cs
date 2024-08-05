using System.Collections;
using UnityEngine;

public class SpawnEnemiesController:MonoBehaviour
{
    private const float DistanceSpawn = 40;
    private const float DepthSpawn = 5;//пока не используется(глубина спавна)
    private bool Spawning;
    private Coroutine Coroutine;
    private const float MaxStartFraction = 0.5f;
    public void SpawnEnemies(BaseEssenceObject[] enemies)
    {
        Coroutine = StartCoroutine(SpawnEnemiesCoroutine(enemies));
    }
    private IEnumerator SpawnEnemiesCoroutine(BaseEssenceObject[] enemies)
    {
        int countSpawnEnemies = UnityEngine.Random.Range(1, (int)((enemies.Length + 1)* MaxStartFraction));
        int oldCountSpawnEnemies = countSpawnEnemies;
        if (countSpawnEnemies > enemies.Length) countSpawnEnemies = enemies.Length;
        for (int i = 0; i < enemies.Length; i++)
        {
            GameObject enemy = Instantiate(enemies[i].GameObjects[enemies[i].GetLevel()]);
            enemy.transform.position = GetRandomPosition();
            countSpawnEnemies--;
            if (countSpawnEnemies == 0)
            {
                countSpawnEnemies = UnityEngine.Random.Range(1, enemies.Length + 1-oldCountSpawnEnemies);
                if (countSpawnEnemies+oldCountSpawnEnemies > enemies.Length) 
                    countSpawnEnemies= enemies.Length-oldCountSpawnEnemies;
                oldCountSpawnEnemies += countSpawnEnemies;
                yield return new WaitForSeconds(1);
            }
        }
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
    private IEnumerator Print()
    {
        for (int i = 0; i < 5; i++)
        {
            Debug.Log(i);
            yield return new WaitForSeconds(1);
        }
    }
}

