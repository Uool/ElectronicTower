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

        // TODO: CSV 테이블로 ShopData 를 만들고, 그걸 바로 넣어주는 건 어떨까???
    }

    // 터랫 구매
    public void PurchaseTurret(Define.ETurretType turretType)
    {
        Managers.Build.SelectTurretToBuild(turretType, _turretShopData[(int)turretType]);
    }
}
