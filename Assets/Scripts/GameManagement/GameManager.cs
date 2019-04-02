using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnSequence
    {
        [System.Serializable]
        public struct SpawnIndex
        {
            public GameObject enemy;
            public float delay;
        }
        public SpawnIndex[] spawnIndex;
    }

    public SpawnSequence[] waves;
    public GameObject[] curEnemies;
    public int curWave;
    public int enemyCount;


    void Start()
    {
    }

    public virtual void Update()
    {
        curEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemyCount = curEnemies.Length;
    }

    public void EnemySpawn(GameObject enemy, Vector3 pos)
    {
        Instantiate(enemy, pos, Quaternion.identity);
    }
}
