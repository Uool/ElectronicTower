using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainGameUI : UI_Scene
{
    #region Enum
    public enum EButton
    {
        StartBtn,
        PauseBtn,
        OptionBtn,
        SpeedBtn,
        ShopBtn,
        AreaCheckBtn,
    }

    public enum EGameObject
    {
        GameState,
    }

    public enum EText
    {
        SpeedText,
        WaveText,
        EnemyText,
    }

    public enum EToggle
    {
        AreaCheckToggle,
    }

    #endregion

    private float _gameSpeed = 1f;
    public override void Init()
    {
        base.Init();
        BindUI();

        Get<Toggle>((int)EToggle.AreaCheckToggle).isOn = true;
    }

    void BindUI()
    {
        Bind<Button>(typeof(EButton));
        Bind<Text>(typeof(EText));
        Bind<Toggle>(typeof(EToggle));

        BindEvent(GetButton((int)EButton.PauseBtn).gameObject, (PointerEventData data) => { Pause(); });
        BindEvent(GetButton((int)EButton.OptionBtn).gameObject, (PointerEventData data) => { Option(); });
        BindEvent(GetButton((int)EButton.SpeedBtn).gameObject, (PointerEventData data) => { Speed(); });
        Get<Toggle>((int)EToggle.AreaCheckToggle).onValueChanged.AddListener(delegate { AreaCheck(); });
    }

    #region ButtonFunction
    void Pause()
    {
        UI_Pause pause = Managers.UI.ShowPopupUI<UI_Pause>();
        pause.gameSpeed = _gameSpeed;

        Time.timeScale = 0f;
    }

    void Option()
    {
        UI_Option option = Managers.UI.ShowPopupUI<UI_Option>();
        option.gameSpeed = _gameSpeed;

        Time.timeScale = 0f;
    }

    void Speed()
    {
        _gameSpeed = (_gameSpeed + 0.5f > 2f) ? 1f : _gameSpeed + 0.5f;
        GetText((int)EText.SpeedText).text = string.Format("x {0:F1}", _gameSpeed);
        Time.timeScale = _gameSpeed;
    }

    void AreaCheck()
    {
        Managers.Game.PowerPoleArea(Get<Toggle>((int)EToggle.AreaCheckToggle).isOn);
    }

    #endregion
}
