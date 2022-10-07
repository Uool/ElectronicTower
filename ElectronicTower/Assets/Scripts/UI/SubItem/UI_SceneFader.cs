using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UI_SceneFader : UI_Base
{
    public enum EImage
    {
        BlackBG,
    }

    private Image _blackBG;

    public AnimationCurve curve;

    public override void Init()
    {
        BindUI();

        StartCoroutine(FadeIn());
    }

    protected override void BindUI()
    {
        Bind<Image>(typeof(EImage));
        _blackBG = GetImage((int)EImage.BlackBG);
    }

    public void FadeTo(Define.EScene eScene)
    {
        StartCoroutine(FadeOut(eScene));
    }

    // ∞À¡§ -> π‡¿Ω
    IEnumerator FadeIn()
    {
        float t = 1f;

        while (t > 0f)
        {
            t -= Time.deltaTime;
            float a = curve.Evaluate(t);
            _blackBG.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }
    }
    // π‡¿Ω -> ∞À¡§
    IEnumerator FadeOut(Define.EScene eScene)
    {
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime;
            float a = curve.Evaluate(t);
            _blackBG.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }

        if (eScene == Define.EScene.Unknown)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }
        else
        {
            Managers.Scene.LoadScene(eScene);
        }
    }
}
