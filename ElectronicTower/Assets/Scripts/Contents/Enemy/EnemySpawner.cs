using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public Transform enemyPrefab;
    public Transform spawnPoint;    // starting point
    public float timeBetweenWaves = 5f; // ���̺�� ���̺� ������ ����

    private float _countDown = 2f;  // ù ���̺� �����Ҷ� ī��Ʈ �ٿ�
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
        Managers.Resource.Instantiate($"Enemy/{enemyPrefab.name}", spawnPoint);
    }
}
