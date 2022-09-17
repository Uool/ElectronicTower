using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat
{
    private int _gameMoney = 10000;

    public int GameMoney { get { return _gameMoney; } }
    public void GetMoney(int addCash) { _gameMoney += addCash; }
    public void ConsumeMoney(int minCash) { _gameMoney -= minCash; }
}
