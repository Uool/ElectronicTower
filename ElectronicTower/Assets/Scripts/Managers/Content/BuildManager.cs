
using UnityEngine;
using UnityEngine.Events;

public class BuildManager
{
    private TurretShopData _turretToBuild;
    private Node _selectedNode;
    private Define.ETurretType _turretType;
    private NodeUI _nodeUI;


    public UnityAction buildEndAction;
    public bool CanBuild { get { return _turretToBuild != null; }}
    public Define.ETurretType TurretType { get { return _turretType; } }

    public void Init()
    {
        buildEndAction -= ClearTurretToBuild;
        buildEndAction += ClearTurretToBuild;
    }

    public void SelectNode (Node node)
    {
        if (_selectedNode == node)
        {
            DeselectNode();
            return;
        }

        _selectedNode = node;
        _turretToBuild = null;

        if (_nodeUI == null)
            _nodeUI = Managers.UI.MakeSubItem<NodeUI>();

        _nodeUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        _selectedNode = null;
        if (null != _nodeUI) _nodeUI.Hide();
    }

    //public GameObject BuildTurretOn(Node node)
    //{
    //    Managers.Player.money -= _turretToBuild.cost;
    //    GameObject turretObject = null;
    //    if (_turretType == Define.ETurretType.PowerPole)
    //        turretObject = Managers.Game.PowerPoleSpawn(_turretToBuild.turretPrefab, node.transform);
    //    else
    //        turretObject = Managers.Game.TurretSpawn(_turretToBuild.turretPrefab, node.transform);

    //    turretObject.transform.position = node.GetBuildPosition();
    //    _turretToBuild = null;

    //    // TODO : 터렛을 건설했을 시 변화될 UI를 이벤트에 넣어야 함.
    //    //Managers.Resource.Instantiate(, node.transform);

    //    return turretObject;
    //}

    public void SelectTurretToBuild(Define.ETurretType type, TurretShopData turretData)
    {
        _turretType = type;
        if (Managers.Player.money >= turretData.Cost)
        {
            _turretToBuild = turretData;
            _selectedNode = null;
        }
        else
        {
            // TODO: 돈이 없다는 Toast를 띄워야 함
            _turretToBuild = null;
            return;
        }
        if (_nodeUI != null)
            _nodeUI.Hide();

        DeselectNode();
    }

    public void CancelTurretToBuild()
    {
        _turretToBuild = null;
        if (_nodeUI != null)
            _nodeUI.Hide();
        DeselectNode();
    }

    public TurretShopData GetTurretToBuild() { return _turretToBuild; }
    public void ClearTurretToBuild() { _turretToBuild = null; }
}
