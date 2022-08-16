using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Option : UI_Popup
{
    public enum ESlider
    {
        BGMSlider,
        EffectSlider
    }

    public enum EButton
    {
        CloseBtn
    }

    [HideInInspector] public float gameSpeed = 1f;

    public override void Init()
    {
        base.Init();

        Bind<Slider>(typeof(ESlider));
        Get<Slider>((int)ESlider.BGMSlider).onValueChanged.AddListener(delegate { BGMSlider(); });
        Get<Slider>((int)ESlider.EffectSlider).onValueChanged.AddListener(delegate { EffectSlider(); });

        Bind<Button>(typeof(EButton));
        BindEvent(GetButton((int)EButton.CloseBtn).gameObject, (PointerEventData data) => { ClosePopupUI(); });
    }

    public override void ClosePopupUI()
    {
        base.ClosePopupUI();
        Time.timeScale = gameSpeed;
    }

    void BGMSlider()
    {
        Managers.Sound.Volume(Define.ESound.Bgm, Get<Slider>((int)ESlider.BGMSlider).value);
    }
    void EffectSlider()
    {
        Managers.Sound.Volume(Define.ESound.Effect, Get<Slider>((int)ESlider.EffectSlider).value);
    }
}
