using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerEx
{
    public List<Enemy> enemyList = new List<Enemy>();

    public Enemy EnemySpawn(string path, Transform parent = null)
    {
        Enemy enemy = Managers.Resource.Instantiate(path, parent).GetComponent<Enemy>();

        if (enemy == null)
            return null;

        enemyList.Add(enemy);

        return enemy;
    }

    public void EnemyDespawn(Enemy enemy)
    {
        enemyList.Remove(enemy);
        Managers.Resource.Destroy(enemy.gameObject);
    }
}
