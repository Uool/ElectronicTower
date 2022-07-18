using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerEx
{
    public List<Enemy> enemyList = new List<Enemy>();
    public List<Turret> turretList = new List<Turret>();

    public Enemy EnemySpawn(string path, Transform parent = null)
    {
        Enemy enemy = Managers.Resource.Instantiate(path, parent).GetComponent<Enemy>();

        if (enemy == null)
            return null;

        enemyList.Add(enemy);

        return enemy;
    }

    public GameObject TurretSpawn(GameObject turretPrefab, Transform parent)
    {
        GameObject go = Managers.Resource.Instantiate(turretPrefab, parent);
        Turret turret = go.GetComponent<Turret>();
        turret.Init();

        turretList.Add(turret);

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

    public void Clear()
    {
        enemyList.Clear();
        turretList.Clear();
    }
}
