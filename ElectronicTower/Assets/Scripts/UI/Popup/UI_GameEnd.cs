using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class UI_GameEnd : UI_Popup
{
    private string _titleString;
    

    public enum EButton
    {
        ContinueBtn,
        ExitBtn
    }

    public enum EImage
    {
        FadeOutPanel,
    }
    public enum EText
    {
        GameEndText,
    }

    public override void Init()
    {
        base.Init();
        BindUI();

        GetText((int)EText.GameEndText).text = _titleString;
        Time.timeScale = 0f;
    }

    

    void BindUI()
    {
        Bind<Button>(typeof(EButton));
        Bind<Image>(typeof(EImage));
        Bind<Text>(typeof(EText));

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
        Time.timeScale = 1f;
        StartCoroutine(coFadeOut());
    }

    #endregion

    #region ETC
    public void Setting(bool isGameOver = false)
    {
        if (isGameOver == true)
            _titleString = "게임 오버!";
        else
            _titleString = "게임 클리어!";
    }

    IEnumerator coFadeOut()
    {
        float alpha = 0f;

        while (alpha <= 1f)
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

#endregion
}
