using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopUI : UI_Base
{
    public enum EButton { MachineGun, Rocket, Laser, PowerPole, ShopBtn }

    [SerializeField] private TurretShopData[] _turretShopData;

    private Animator _animator;
    private int _shopOpenId;
    private bool _isOpen;
    public override void Init()
    {
        _animator = GetComponent<Animator>();
        _shopOpenId = Animator.StringToHash("IsOpen");

        BindUI();

        // TODO: CSV ���̺�� ShopData �� �����, �װ� �ٷ� �־��ִ� �� ���???
    }

    void BindUI()
    {
        Bind<Button>(typeof(EButton));

        BindEvent(GetButton((int)EButton.MachineGun).gameObject, (PointerEventData data) => { PurchaseTurret(Define.ETurretType.MachineGun); });
        BindEvent(GetButton((int)EButton.Rocket).gameObject, (PointerEventData data) => { PurchaseTurret(Define.ETurretType.Rocket); });
        BindEvent(GetButton((int)EButton.Laser).gameObject, (PointerEventData data) => { PurchaseTurret(Define.ETurretType.Laser); });
        BindEvent(GetButton((int)EButton.PowerPole).gameObject, (PointerEventData data) => { PurchaseTurret(Define.ETurretType.PowerPole); });

        BindEvent(GetButton((int)EButton.ShopBtn).gameObject, (PointerEventData data) => { ShopActive(); });
        
    }

    // �ͷ� ����
    void PurchaseTurret(Define.ETurretType turretType)
    {
        Managers.Build.SelectTurretToBuild(turretType, _turretShopData[(int)turretType]);
    }

    void ShopActive()
    {
        _isOpen = !_isOpen;
        _animator.SetBool(_shopOpenId, _isOpen);
    }
}
