using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveData> _waveDataList;

    private Transform _spawnPoint;    // starting point
    private int _waveCount = 0;

    private void Start()
    {
        _spawnPoint = WayPoints.points[0];

        _waveDataList = Managers.Game.waveDataList;

        Managers.Game.startWaveAction -= StartWave;
        Managers.Game.startWaveAction += StartWave;

        StopCoroutine(coSpawnWave());
    }

    void StartWave()
    {
        if (_waveCount < _waveDataList.Count)
            StartCoroutine(coSpawnWave());
    }

    IEnumerator coSpawnWave()
    {
        for (int i = 0; i < _waveDataList[_waveCount].EnemyCount; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(_waveDataList[_waveCount].SpawnDelay);
        }

        while (Managers.Game.enemyList.Count > 0)
        {
            yield return null;
        }

        Managers.Game.endWaveAction?.Invoke();
        _waveCount++;
    }

    void SpawnEnemy()
    {
        Enemy enemy = Managers.Game.EnemySpawn(_waveDataList[_waveCount].enemyPrefab);
        enemy.transform.position = _spawnPoint.position;
    }
}
