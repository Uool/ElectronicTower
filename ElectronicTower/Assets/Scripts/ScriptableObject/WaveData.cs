using UnityEngine;

[CreateAssetMenu(menuName = "Wave/WaveData", fileName = "WaveData")]
public class WaveData : ScriptableObject
{
    public void WaveDataInit(string[] data)
    {
        fileName = data[0];
        enemyPrefab = Resources.Load<GameObject>($"Prefabs/Enemy/{data[1]}");
        _enemyCount = int.Parse(data[2]);
        _spawnDelay = float.Parse(data[3]);
    }
    public string fileName;

    public GameObject enemyPrefab;
    [SerializeField] private int _enemyCount;
    [SerializeField] private float _spawnDelay;

    public int EnemyCount { get { return _enemyCount; } }
    public float SpawnDelay { get { return _spawnDelay; } }
}