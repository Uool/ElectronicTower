using UnityEngine;

public class Tile : MonoBehaviour
{
    [HideInInspector] public Coord coord;
    [HideInInspector] public bool isObstcle;

    [HideInInspector] public bool isStart;
    [HideInInspector] public bool isEnd;

    public Tile parentTile;

    /* G: �̵��� �Ÿ�, H: ��ǥ���� �Ÿ�, F: G+H */
    public int G, H;
    public int F { get { return G + H; } }
}
