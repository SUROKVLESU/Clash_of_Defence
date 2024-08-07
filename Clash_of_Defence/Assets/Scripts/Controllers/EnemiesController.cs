using System.Collections.Generic;
using UnityEngine;

public class EnemiesController:MonoBehaviour
{
    public List<GameObject> Enemies = new List<GameObject>();
    public void DestroyEnemies()
    {
        for (int i = 0; i < Enemies.Count; i++)
        {
            Destroy(Enemies[i]);
        }
        Enemies.Clear();
    }
    public void Pause()
    {
        for (int i = 0; i < Enemies.Count; i++)
        {
            Enemies[i].GetComponent<BaseEnemyCharacteristics>().Stop();
        }
    }
    public void OffPause()
    {
        for (int i = 0; i < Enemies.Count; i++)
        {
            IBaseInterface attacking = Enemies[i].GetComponent<IBaseInterface>();
            attacking.ActivationBuildings();
        }
    }
}

