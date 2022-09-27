using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat
{
    public void Init()
    {
        _gameMoney = 10000;
        _gameHealth = 5;
    }

    private int _gameMoney = 10000;
    private int _gameHealth = 5;
    public int GameMoney { get { return _gameMoney; } }
    public int GameHealth { get { return _gameHealth; } }

    #region Func
    public void GetMoney(int addCash) { _gameMoney += addCash; }
    public void ConsumeMoney(int minCash) { _gameMoney -= minCash; }
    public void GetHealth(int healthValue) { _gameHealth += healthValue; }
    public void DamageHealth(int healthValue) { _gameHealth -= healthValue; }
    #endregion
}
