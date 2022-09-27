using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class UI_GameOver : UI_Popup
{
    public enum EButton
    {
        ContinueBtn,
        ExitBtn
    }

    public enum EImage
    {
        FadeOutPanel,
    }

    public override void Init()
    {
        base.Init();
        BindUI();

        Time.timeScale = 0f;
    }

    void BindUI()
    {
        Bind<Button>(typeof(EButton));
        Bind<Image>(typeof(EImage));

        BindEvent(GetButton((int)EButton.ContinueBtn).gameObject, (PointerEventData data) => { Continue(); });
        BindEvent(GetButton((int)EButton.ExitBtn).gameObject, (PointerEventData data) => { ExitGame(); });
    }

    #region ButtonFunction

    void Continue()
    {
        Time.timeScale = 1f;
        Managers.Scene.LoadScene(Define.EScene.Game);
    }

    void ExitGame()
    {
        StartCoroutine(coFadeOut());
    }

    #endregion

    #region ETC

    IEnumerator coFadeOut()
    {
        float alpha = 0f;

        while (alpha <= 1f)
        {
            alpha += Time.deltaTime;
            GetImage((int)EImage.FadeOutPanel).color = new Color(0, 0, 0, alpha);
            yield return null;
        }
#if UNITY_ANDROID
        Application.Quit();
#endif

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    #endregion
}
