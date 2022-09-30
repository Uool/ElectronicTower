using System.Collections;
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
        CloseBtn,
        QuitBtn,
    }

    public enum EImage
    {
        FadeOutPanel,
    }

    [HideInInspector] public float gameSpeed = 1f;

    public override void Init()
    {
        base.Init();
        Bind();

        // TODO: 설정된 값을 저장하고 있어야 함
        Get<Slider>((int)ESlider.BGMSlider).value = Managers.Sound.bgmVolume;
        Get<Slider>((int)ESlider.EffectSlider).value = Managers.Sound.effectVolume;
    }

    void Bind()
    {
        Bind<Slider>(typeof(ESlider));
        Bind<Button>(typeof(EButton));
        Bind<Image>(typeof(EImage));

        Get<Slider>((int)ESlider.BGMSlider).onValueChanged.AddListener(delegate { BGMSlider(); });
        Get<Slider>((int)ESlider.EffectSlider).onValueChanged.AddListener(delegate { EffectSlider(); });
        BindEvent(GetButton((int)EButton.CloseBtn).gameObject, (PointerEventData data) => { ClosePopupUI(); });
        BindEvent(GetButton((int)EButton.QuitBtn).gameObject, (PointerEventData data) => { QuitGame(); });
    }

    public override void ClosePopupUI()
    {
        base.ClosePopupUI();
        Time.timeScale = gameSpeed;
    }

    void BGMSlider()
    {
        Managers.Sound.bgmVolume = Get<Slider>((int)ESlider.BGMSlider).value;
        Managers.Sound.Volume(Define.ESound.Bgm, Get<Slider>((int)ESlider.BGMSlider).value);
    }
    void EffectSlider()
    {
        Managers.Sound.effectVolume = Get<Slider>((int)ESlider.EffectSlider).value;
        Managers.Sound.Volume(Define.ESound.Effect, Get<Slider>((int)ESlider.EffectSlider).value);
    }

    void QuitGame()
    {
        StartCoroutine(coFadeOut());
    }

    IEnumerator coFadeOut()
    {
        float alpha = 0f;

        while(alpha <= 1f)
        {
            alpha += Time.deltaTime;
            GetImage((int)EImage.FadeOutPanel).color = new Color(0, 0, 0, alpha);
            yield return null;
        }
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
