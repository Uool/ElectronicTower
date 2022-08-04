using UnityEngine;
using UnityEngine.UI;

public class Shop : UI_Scene
{
    public enum EButton { MachineGun, Rocket, Laser, PowerPole }

    [SerializeField] private TurretShopData[] _turretShopData;
    [SerializeField] private Button[] _buyBtn;

    public override void Init()
    {
        base.Init();
        _buyBtn[(int)Define.ETurretType.MachineGun].onClick.AddListener(delegate { PurchaseTurret(Define.ETurretType.MachineGun); });
        _buyBtn[(int)Define.ETurretType.Rocket].onClick.AddListener(delegate { PurchaseTurret(Define.ETurretType.Rocket); });
        _buyBtn[(int)Define.ETurretType.Laser].onClick.AddListener(delegate { PurchaseTurret(Define.ETurretType.Laser); });
        _buyBtn[(int)Define.ETurretType.PowerPole].onClick.AddListener(delegate { PurchaseTurret(Define.ETurretType.PowerPole); });

        // TODO: CSV ���̺�� ShopData �� �����, �װ� �ٷ� �־��ִ� �� ���???
    }

    // �ͷ� ����
    public void PurchaseTurret(Define.ETurretType turretType)
    {
        Managers.Build.SelectTurretToBuild(turretType, _turretShopData[(int)turretType]);
    }
}
