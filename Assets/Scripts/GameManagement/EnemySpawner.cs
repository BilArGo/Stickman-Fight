using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : GameManager
{
    

    void Start()
    {
        StartCoroutine("Spawner");
    }

    public override void Update()
    {
        base.Update();
        if (enemyCount == 0)
        {
            
        }
    }
    
    IEnumerator Spawner ()
    {
        for (int i = 0; i < 5; i++)
        {
            //EnemySpawn(waves[curWave].spawnIndex[i].enemy, Vector3.zero);
            //yield return new WaitForSeconds(waves[curWave].spawnIndex[i].delay);
            yield return new WaitForSeconds(1.0f);
            print(i);
        }
    }
}
