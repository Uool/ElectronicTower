using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    private MapGenerator _mapGenerator;
    protected override void Init()
    {
        base.Init();
        SceneType = Define.EScene.Game;

        Managers.Build.Init();
        Managers.Game.Init();

        if (null == _mapGenerator)
            _mapGenerator = FindObjectOfType<MapGenerator>();

        _mapGenerator.seed = Random.Range(1, 100);
    }

    public override void Clear()
    {
        Managers.Game.Clear();
    }
}
