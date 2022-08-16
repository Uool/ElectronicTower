using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainGameUI : UI_Scene
{
    public enum EButton
    {
        PauseBtn,
        OptionBtn,
        SpeedBtn,
        ShopBtn,
        AreaCheckBtn,
    }

    public enum EText
    {
        SpeedText,
    }

    private GameObject _pauseUIObject;

    private float _gameSpeed = 1f;
    public override void Init()
    {
        base.Init();
        BindUI();
    }

    void BindUI()
    {
        Bind<Button>(typeof(EButton));
        Bind<Text>(typeof(EText));

        BindEvent(GetButton((int)EButton.PauseBtn).gameObject, (PointerEventData data) => { Pause(); });
        BindEvent(GetButton((int)EButton.OptionBtn).gameObject, (PointerEventData data) => { Option(); });
        BindEvent(GetButton((int)EButton.SpeedBtn).gameObject, (PointerEventData data) => { Speed(); });
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

    }

    #endregion
}
