using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Intro : UI_Scene
{
    public enum EButton
    {
        OptionBtn,
    }
    public override void Init()
    {
        base.Init();
        BindUI();
    }

    protected override void BindUI()
    {
        Bind<Button>(typeof(EButton));

        BindEvent(GetButton((int)EButton.OptionBtn).gameObject, (PointerEventData data) => { Option(); });
    }

    void Option()
    {
        Managers.UI.ShowPopupUI<UI_Option>();
    }
}
