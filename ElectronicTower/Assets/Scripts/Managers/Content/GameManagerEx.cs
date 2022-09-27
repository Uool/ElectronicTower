using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManagerEx
{
    public List<Enemy> enemyList = new List<Enemy>();
    public List<Turret> turretList = new List<Turret>();
    public List<PowerPole> powerPoleList = new List<PowerPole>();
    public List<WaveData> waveDataList = new List<WaveData>();

    public bool isPowerPoleArea = true;

    public UnityAction startWaveAction;
    public UnityAction endWaveAction;

    public void Init()
    {
        Managers.Player.Init();

        waveDataList.Add(Managers.Resource.Load<WaveData>("ScriptableObject/Wave/EasyEnemyWave"));
        waveDataList.Add(Managers.Resource.Load<WaveData>("ScriptableObject/Wave/NormalEnemyWave"));
        waveDataList.Add(Managers.Resource.Load<WaveData>("ScriptableObject/Wave/BossEnemyWave"));
    }

    public Enemy EnemySpawn(GameObject enemyPrefab, Transform parent = null)
    {
        Enemy enemy = Managers.Resource.Instantiate(enemyPrefab, parent).GetComponent<Enemy>();

        if (enemy == null)
            return null;

        enemyList.Add(enemy);

        return enemy;
    }

    public GameObject TurretSpawn(GameObject turretPrefab, Transform parent)
    {
        GameObject go = Managers.Resource.Instantiate(turretPrefab, parent);
        Turret turret = go.GetComponent<Turret>();
        turret.myNode = parent;
        turret.Init();
        turretList.Add(turret);

        CheckLink();

        return go;
    }

    public GameObject PowerPoleSpawn(GameObject PowerPolePrefab, Transform parent)
    {
        GameObject go = Managers.Resource.Instantiate(PowerPolePrefab, parent);
        PowerPole powerPole = go.GetComponent<PowerPole>();
        powerPole.myNode = parent;
        powerPole.Init();
        powerPoleList.Add(powerPole);

        CheckLink();

        return go;
    }

    public void EnemyDespawn(Enemy enemy)
    {
        enemyList.Remove(enemy);
        Managers.Resource.Destroy(enemy.gameObject);
    }

    public void TurretDespawn(Turret turret)
    {
        turretList.Remove(turret);
        Managers.Resource.Destroy(turret.gameObject);
    }

    public void PowerPoleArea(bool isOn)
    {
        isPowerPoleArea = isOn;
        foreach (PowerPole powerPole in powerPoleList)
        {
            powerPole.ActiveElectroArea(isOn);
        }
    }

    public void GameOver()
    {
        if (Managers.Player.GameHealth <= 0)
            Managers.UI.ShowPopupUI<UI_GameOver>();
    }

    public void Clear()
    {
        enemyList.Clear();
        turretList.Clear();
        powerPoleList.Clear();
        waveDataList.Clear();

        startWaveAction = null;
        endWaveAction = null;
    }

    void CheckLink()
    {
        foreach (PowerPole pole in powerPoleList)
        {
            if (pole.linkedAction != null)
                pole.linkedAction.Invoke();
        }
    }
}
