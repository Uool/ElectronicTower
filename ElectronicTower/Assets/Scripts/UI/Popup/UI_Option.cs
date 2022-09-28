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

        Bind<Slider>(typeof(ESlider));
        Get<Slider>((int)ESlider.BGMSlider).onValueChanged.AddListener(delegate { BGMSlider(); });
        Get<Slider>((int)ESlider.EffectSlider).onValueChanged.AddListener(delegate { EffectSlider(); });

        Bind<Button>(typeof(EButton));
        BindEvent(GetButton((int)EButton.CloseBtn).gameObject, (PointerEventData data) => { ClosePopupUI(); });
        BindEvent(GetButton((int)EButton.QuitBtn).gameObject, (PointerEventData data) => { QuitGame(); });

        Bind<Image>(typeof(EImage));
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
