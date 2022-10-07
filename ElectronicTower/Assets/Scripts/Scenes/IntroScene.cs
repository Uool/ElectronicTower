using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScene : BaseScene
{
    private GameObject _UI_SceneFader;

    protected override void Init()
    {
        base.Init();
        SceneType = Define.EScene.Intro;

        Managers.UI.ShowSceneUI<UI_Intro>();
        Managers.Sound.Play("BGM/BGM_Intro", Define.ESound.Bgm);
        _UI_SceneFader = Managers.Resource.Instantiate($"UI/SubItem/UI_SceneFader");
    }

    public override void Clear()
    {
        
    }

    #region ButtonFunc

    public void GameStart()
    {
        _UI_SceneFader.GetComponent<UI_SceneFader>().FadeTo(Define.EScene.Game);
    }
    public void GameEnd()
    {
        _UI_SceneFader.GetComponent<UI_SceneFader>().FadeTo(Define.EScene.Unknown);
    }

    #endregion
}
