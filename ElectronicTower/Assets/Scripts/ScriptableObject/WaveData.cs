using UnityEngine;

[CreateAssetMenu(menuName = "Wave/WaveData", fileName = "WaveData")]
public class WaveData : ScriptableObject
{
    public GameObject enemyPrefab;
    [SerializeField] private int _enemyCount;

    public int EnemyCount { get { return _enemyCount; } }
}

