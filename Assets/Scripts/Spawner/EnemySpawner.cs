using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform enemyPoint;
    public List<string> enemyPoolTags = new List<string>() { "Eagle Enemy", "Zombie Enemy"};
    public float intervalSpawnTime = 4f;

    ObjectPooler objectPooler;

    // Start is called before the first frame update
    void Start()
    {
        objectPooler = ObjectPooler.SharedInstance;
        StartCoroutine("SpawnEnemy");
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(intervalSpawnTime);
        int i = Random.Range(0, enemyPoolTags.Count);
        objectPooler.SpawnFromPool(enemyPoolTags[i], enemyPoint.position, enemyPoint.rotation);
        StartCoroutine("SpawnEnemy");
    }
}
