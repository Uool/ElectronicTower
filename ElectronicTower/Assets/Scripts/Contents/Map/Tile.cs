using UnityEngine;

public class Tile : MonoBehaviour
{
    [HideInInspector] public Coord coord;
    [HideInInspector] public bool isObstcle;

    [HideInInspector] public bool isStart;
    [HideInInspector] public bool isEnd;

    public Tile parentTile;

    /* G: 이동한 거리, H: 목표까지 거리, F: G+H */
    public int G, H;
    public int F { get { return G + H; } }
}
