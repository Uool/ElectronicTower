using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    protected override void Init()
    {
        base.Init();
        SceneType = Define.EScene.Game;

        // 최초 웨이브 불러온 후 생성
        Managers.Game.Init();
    }

    public override void Clear()
    {
        Managers.Game.Clear();
    }
}
