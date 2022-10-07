using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopUI : UI_Base
{
    public enum EButton { MachineGun, Rocket, Laser, PowerPole, ShopBtn, CancelBtn }
    public enum EText { CancelTurretText }

    [SerializeField] private TurretShopData[] _turretShopData;
    [SerializeField] private GameObject _wariningUI;

    private Animator _animator;
    private Canvas _canvas;
    private int _shopOpenId;
    private bool _isOpen;

    public override void Init()
    {
        _animator = GetComponent<Animator>();
        _shopOpenId = Animator.StringToHash("IsOpen");
        _canvas = GetComponentInParent<Canvas>();

        BindUI();

        GetButton((int)EButton.CancelBtn).gameObject.SetActive(false);

        Managers.Build.buildEndAction -= InactiveCancelBtn;
        Managers.Build.buildEndAction += InactiveCancelBtn;
    }

    protected override void BindUI()
    {
        Bind<Button>(typeof(EButton));
        Bind<Text>(typeof(EText));

        BindEvent(GetButton((int)EButton.MachineGun).gameObject, (PointerEventData data) => { PurchaseTurret(Define.ETurretType.MachineGun); });
        BindEvent(GetButton((int)EButton.Rocket).gameObject, (PointerEventData data) => { PurchaseTurret(Define.ETurretType.Rocket); });
        BindEvent(GetButton((int)EButton.Laser).gameObject, (PointerEventData data) => { PurchaseTurret(Define.ETurretType.Laser); });
        BindEvent(GetButton((int)EButton.PowerPole).gameObject, (PointerEventData data) => { PurchaseTurret(Define.ETurretType.PowerPole); });

        BindEvent(GetButton((int)EButton.ShopBtn).gameObject, (PointerEventData data) => { ShopActive(); });
        BindEvent(GetButton((int)EButton.CancelBtn).gameObject, (PointerEventData data) => { CancelBuild(); });
    }

    #region ButtenEvent
    // 터랫 구매
    void PurchaseTurret(Define.ETurretType turretType)
    {
        if (turretType != Define.ETurretType.PowerPole)
        {
            if (_wariningUI == null && Managers.Game.powerPoleList.Count == 0)
            {
                _wariningUI = Managers.UI.ShowPopupUI<UI_Warning>(null, _canvas.transform).gameObject;
                return;
            }
        }
        Managers.Build.SelectTurretToBuild(turretType, _turretShopData[(int)turretType]);
        GetButton((int)EButton.CancelBtn).gameObject.SetActive(true);
        GetText((int)EText.CancelTurretText).text = SetTurretText(turretType);
    }

    void ShopActive()
    {
        _isOpen = !_isOpen;
        _animator.SetBool(_shopOpenId, _isOpen);
    }
    void CancelBuild()
    {
        Managers.Build.CancelTurretToBuild();
        InactiveCancelBtn();
    }
    #endregion

    #region ETC
    void InactiveCancelBtn() { GetButton((int)EButton.CancelBtn).gameObject.SetActive(false); }
    string SetTurretText(Define.ETurretType turretType)
    {
        string strTurret = "설치 취소\n";

        switch (turretType)
        {
            case Define.ETurretType.MachineGun:
                strTurret += "(머신건)";
                break;
            case Define.ETurretType.Rocket:
                strTurret += "(로켓)";
                break;
            case Define.ETurretType.Laser:
                strTurret += "(레이저)";
                break;
            case Define.ETurretType.PowerPole:
                strTurret += "(전봇대)";
                break;
            default:
                break;
        }
        return strTurret;
    }
    #endregion
}
