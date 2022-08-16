using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Pause : UI_Popup
{
    public enum EButton { ReleaseBtn }

    [HideInInspector] public float gameSpeed = 1f;

    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(EButton));
        BindEvent(GetButton((int)EButton.ReleaseBtn).gameObject, (PointerEventData data) => { ClosePopupUI(); });
    }

    public override void ClosePopupUI()
    {
        base.ClosePopupUI();
        Time.timeScale = gameSpeed;
    }
}
