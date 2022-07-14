using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [Header("EnemyList")]
    public string[] enemyName;

    [Space(5)]
    public Transform enemyPrefab;
    public Transform spawnPoint;    // starting point
    public float timeBetweenWaves = 5f; // 웨이브와 웨이브 사이의 간격

    private float _countDown = 2f;  // 첫 웨이브 시작할때 카운트 다운
    private int _waveIndex = 1;

    void Update()
    {
        if (_countDown <= 0f)
        {
            StartCoroutine(coSpawnWave());
            _countDown = timeBetweenWaves;
        }

        _countDown -= Time.deltaTime;
    }

    IEnumerator coSpawnWave()
    {
        for (int i = 0; i < _waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
        _waveIndex++;
    }

    void SpawnEnemy()
    {
        GameObject enemy = Managers.Resource.Instantiate($"Enemy/{enemyPrefab.name}");
        enemy.transform.position = spawnPoint.position;
    }
}
