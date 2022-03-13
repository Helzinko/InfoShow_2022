using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;

    [SerializeField] private Transform[] spawns;

    [SerializeField] private GameObject enemyPrefab;

    public float spawnTime = 10f;

    private void Awake()
    {
        instance = this;
    }

    public void StartSpawning()
    {
        StartCoroutine(EnemySpawn());
    }

    IEnumerator EnemySpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);

            var enemy = Instantiate(enemyPrefab, spawns[Random.Range(0, spawns.Length)]);
        }
    }

    private float passedTime = 0;
    public float timeBetweenIncrease = 60f;

    private void Update()
    {
        if (spawnTime < 4)
            return;

        passedTime += Time.deltaTime;

        if(passedTime > timeBetweenIncrease)
        {
            passedTime = 0;
            spawnTime--;
        }
    }
}
