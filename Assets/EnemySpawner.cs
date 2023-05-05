using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private Transform parent;
    public GameObject enemy;
    public float spawnRadius;

    // Start is called before the first frame update
    void Start()
    {
        parent = this.transform;
        SpawnEnemy();
    }

    void SpawnEnemy()
    {
        for (int i = 0; i < GameManager.enemiesSpawned; i++)
        {
            GameObject enemySpawned = Instantiate(enemy, new Vector3(parent.transform.position.x + Random.Range(-12f, 12f), 0, parent.transform.position.z + Random.Range(-12f, 12f)), Quaternion.identity);

            enemySpawned.transform.parent = parent.transform;
        }
    }

}
