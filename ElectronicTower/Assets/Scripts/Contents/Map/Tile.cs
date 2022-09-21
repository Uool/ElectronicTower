using UnityEngine;

public class Tile : MonoBehaviour
{
    [HideInInspector] public Coord coord;
    [HideInInspector] public bool isObstcle;

    [HideInInspector] public bool isStart;
    [HideInInspector] public bool isEnd;

    [HideInInspector] public Tile parentTile;

    private Material tileMat;

    /* G: 이동한 거리, H: 목표까지 거리, F: G+H */
    public int G, H;
    public int F { get { return G + H; } }
    public void Init()
    {
        tileMat = Managers.Resource.Load<Material>("Materials/Tile/Tile");
        GetComponent<MeshRenderer>().material = tileMat;
    }

    public void SetPointing(string pointing)
    {
        tileMat = Managers.Resource.Load<Material>($"Materials/Tile/{pointing}");
        GetComponent<MeshRenderer>().material = tileMat;
    }
}
