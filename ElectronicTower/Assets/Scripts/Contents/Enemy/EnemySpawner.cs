using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [Header("EnemyList")]
    public string[] enemyName;

    [Space(5)]
    public GameObject enemyPrefab;
    public float timeBetweenWaves = 5f; // 웨이브와 웨이브 사이의 간격

    [HideInInspector] public bool startWave;

    [SerializeField] List<WaveData> waveDataList;

    private Transform _spawnPoint;    // starting point
    private int _waveCount = 0;

    private void Start()
    {
        _spawnPoint = WayPoints.points[0];
        enemyPrefab = Managers.Resource.Load<GameObject>("Prefabs/Enemy/Enemy");

        waveDataList = new List<WaveData>();
        waveDataList.Add(Managers.Resource.Load<WaveData>("ScriptableObject/Wave/EasyEnemyWave"));

        Managers.Game.startWaveAction -= StartWave;
        Managers.Game.startWaveAction += StartWave;
    }

    void StartWave()
    {
        StartCoroutine(coSpawnWave());
    }

    IEnumerator coSpawnWave()
    {
        for (int i = 0; i < waveDataList[_waveCount].EnemyCount; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
    }

    void SpawnEnemy()
    {
        Enemy enemy = Managers.Game.EnemySpawn(waveDataList[_waveCount].enemyPrefab);
        enemy.transform.position = _spawnPoint.position;
    }
}
