using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Coord
{
    public int x;
    public int y;
    public Coord(int _x, int _y)
    {
        x = _x;
        y = _y;
    }

    public static bool operator ==(Coord c1, Coord c2) { return c1.x == c2.x && c1.y == c2.y; }
    public static bool operator !=(Coord c1, Coord c2) { return !(c1 == c2); }
}

public class MapGenerator : MonoBehaviour
{
    private Dictionary<Coord, Tile> tiles;
    private List<Coord> allTileCoords;
    private Queue<Coord> shuffledTileCoords;

    private List<Tile> openList;
    private List<Tile> closeList;
    private List<Tile> finalTileList;

    private Coord centerCoord;
    private Coord startCoord;
    private Coord endCoord;
    private Tile curTile;

    public Transform tilePrefab;
    public Transform obstaclePrefab;    // TurretNode
    public Transform[] waypoints;       // EnemyRoute
    public Vector2 mapSize;

    [Range(0, 1)]
    public float outlinePercent;
    [Range(0, 1)]
    public float obstaclePercent;

    public int seed = 10;

    private void Start()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {
        tiles = new Dictionary<Coord, Tile>();
        allTileCoords = new List<Coord>();
        //if (allTileCoords.Count > 0)
        //    allTileCoords.Clear();

        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                allTileCoords.Add(new Coord(x, y));
            }
        }
        shuffledTileCoords = new Queue<Coord>(Util.SuffleArray(allTileCoords.ToArray(), seed));

        startCoord = new Coord(-1, -1);
        endCoord = new Coord(-1, -1);
        centerCoord = new Coord((int)mapSize.x / 2, (int)mapSize.y / 2);

        string holderName = "Generated Map";
        if (transform.Find(holderName))
            DestroyImmediate(transform.Find(holderName).gameObject);

        Transform mapHolder = new GameObject(holderName).transform;
        mapHolder.parent = transform;

        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                Vector3 tilePosition = CoordToPosition(x, y);
                Coord tileCoord = new Coord(x, y);
                //GameObject newTile = Managers.Resource.Instantiate(tilePrefab.gameObject, tilePosition, Quaternion.Euler(Vector3.right * 90), mapHolder);
                Tile newTile = Instantiate(tilePrefab, tilePosition, Quaternion.Euler(Vector3.right * 90), mapHolder).GetComponent<Tile>();
                newTile.transform.localScale = Vector3.one * (1 - outlinePercent);
                newTile.coord = tileCoord;
                tiles.Add(tileCoord, newTile);
            }
        }

        bool[,] obstacleMap = new bool[(int)mapSize.x, (int)mapSize.y];

        int obstacleCount = (int)(mapSize.x * mapSize.y * obstaclePercent);
        int currentObstacleCount = 0;

        for (int i = 0; i < obstacleCount; i++)
        {
            Coord randomCoord = GetRandomCoord();
            obstacleMap[randomCoord.x, randomCoord.y] = true;
            currentObstacleCount++;
            if (randomCoord != centerCoord && MapIsFullyAccessable(obstacleMap, currentObstacleCount))
            {
                Vector3 obstaclePosition = CoordToPosition(randomCoord.x, randomCoord.y);
                //GameObject newObstacle = Managers.Resource.Instantiate(obstaclePrefab.gameObject, obstaclePosition + Vector3.up * 0.5f, Quaternion.identity, mapHolder);
                Transform newObstacle = Instantiate(obstaclePrefab, obstaclePosition + Vector3.up * 0.5f, Quaternion.identity, mapHolder) as Transform;

                if (tiles.ContainsKey(randomCoord))
                    tiles[randomCoord].isObstcle = true;
            }
            else
            {
                obstacleMap[randomCoord.x, randomCoord.y] = false;
                currentObstacleCount--;
            }
        }

        // TODO: 시작지점 AND 종료지점을 알아야 함(startCoord / endCoord)
        FindStartToEndCoord(obstacleMap);
    }

    private bool MapIsFullyAccessable(bool[,] obstacleMap, int currentObstacleCount)
    {
        // Flood Fill (이미 살펴봤던 타일들을 표시해서, 같은 타일을 또 보지 않도록 하게끔 주의)
        bool[,] mapFlags = new bool[obstacleMap.GetLength(0), obstacleMap.GetLength(1)];
        Queue<Coord> queue = new Queue<Coord>();
        queue.Enqueue(centerCoord);
        // 중앙 부분은 무조껀 비어있게끔
        mapFlags[centerCoord.x, centerCoord.y] = true;

        // 접근 가능한 타일의 수 (1인 이유 : 처음에 가운데는 무조껀 가능한 위치라서)
        int accessibleTileCount = 1;

        while (queue.Count > 0)
        {
            Coord tile = queue.Dequeue();

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    int neighbourX = tile.x + x;
                    int neighbourY = tile.y + y;
                    if (x == 0 || y == 0)
                    {
                        if (neighbourX >= 0 && neighbourX < obstacleMap.GetLength(0)
                            && neighbourY >= 0 && neighbourY < obstacleMap.GetLength(1))
                        {
                            if (mapFlags[neighbourX, neighbourY] == false && obstacleMap[neighbourX, neighbourY] == false)
                            {
                                mapFlags[neighbourX, neighbourY] = true;
                                queue.Enqueue(new Coord(neighbourX, neighbourY));
                                accessibleTileCount++;
                            }
                        }
                    }
                }
            }
        }

        int targetAccessibleTileCount = (int)(mapSize.x * mapSize.y - currentObstacleCount);
        return targetAccessibleTileCount == accessibleTileCount;
    }

    private Vector3 CoordToPosition(int x, int y)
    {
        return new Vector3(-mapSize.x / 2f + 0.5f + x, 0f, -mapSize.y / 2f + 0.5f + y);
    }

    public Coord GetRandomCoord()
    {
        // 가장 앞에 있는 좌표를 빼내고 맨 뒤로 보내준다.
        Coord randomCoord = shuffledTileCoords.Dequeue();
        shuffledTileCoords.Enqueue(randomCoord);
        return randomCoord;
    }

    void FindStartToEndCoord(bool[,] obstacleMap)
    {
        List<Coord> openCoordList = new List<Coord>();
        for (int i = 0; i < shuffledTileCoords.Count; i++)
        {
            Coord randomCoord;
            Coord[] arrayCoord = new Coord[shuffledTileCoords.Count];
            
            shuffledTileCoords.CopyTo(arrayCoord, 0);
            randomCoord = arrayCoord[i];

            if (obstacleMap[randomCoord.x, randomCoord.y] == false &&
                ((randomCoord.x == 0 || randomCoord.x == obstacleMap.GetLength(0) - 1) ||
                (randomCoord.y == 0 || randomCoord.y == obstacleMap.GetLength(1) - 1)))
            {
                if (startCoord.x == -1 || startCoord.y == -1)
                {
                    startCoord = randomCoord;
                    tiles[randomCoord].isStart = true;
                }
            }
            else if (obstacleMap[randomCoord.x, randomCoord.y] == false)
                openCoordList.Add(randomCoord);
        }

        int maxLength = 0;
        foreach (Coord edge in openCoordList)
        {
            int length = Mathf.Abs(startCoord.x - edge.x) + Mathf.Abs(startCoord.y - edge.y);
            if (length > maxLength)
            {
                maxLength = length;
                endCoord = edge;
            }
        }
        tiles[endCoord].isEnd = true;

        PathFinding();
    }

    void PathFinding()
    {
        openList = new List<Tile>() { tiles[startCoord] };
        closeList = new List<Tile>();
        finalTileList = new List<Tile>();

        while (openList.Count > 0)
        {
            curTile = openList[0];

            for (int i = 1; i < openList.Count; i++)
            {
                if (openList[i].F <= curTile.F && openList[i].H < curTile.H)
                    curTile = openList[i];
            }

            openList.Remove(curTile);
            closeList.Add(curTile);

            // 마지막으로 결정된 길의 Tile들을 넣어준다.
            if (curTile == tiles[endCoord])
            {
                Tile targetCurTile = tiles[endCoord];
                while (targetCurTile != tiles[startCoord])
                {
                    finalTileList.Add(targetCurTile);
                    targetCurTile = targetCurTile.parentTile;
                }
                finalTileList.Add(tiles[startCoord]);
                finalTileList.Reverse();

                waypoints = new Transform[finalTileList.Count];
                for (int i = 0; i < finalTileList.Count; i++)
                {
                    waypoints[i] = finalTileList[i].transform;
                }
                return;
            }

            // 상,좌,하,우
            OpenListAdd(curTile.coord.x, curTile.coord.y + 1);
            OpenListAdd(curTile.coord.x + 1, curTile.coord.y);
            OpenListAdd(curTile.coord.x, curTile.coord.y - 1);
            OpenListAdd(curTile.coord.x - 1, curTile.coord.y);
        }
    }

    void OpenListAdd(int x, int y)
    {
        Coord currentCoord = new Coord(x, y);

        // x,y 좌표가 맵 끝을 넘지 않으며, 벽이 아니어야 하고, closeList에 있으면 안됌.
        if (x >= 0 && x < mapSize.x && y >= 0 && y < mapSize.y &&
            !tiles[currentCoord].isObstcle && !closeList.Contains(tiles[currentCoord]))
        {
            Tile neighborTile = tiles[currentCoord];
            int moveCost = curTile.G + 10;  // 대각선은 사용하지 않을 예정

            if (moveCost < neighborTile.G || openList.Contains(neighborTile) == false)
            {
                neighborTile.G = moveCost;
                neighborTile.H = Mathf.Abs(neighborTile.coord.x - tiles[endCoord].coord.x) + Mathf.Abs(neighborTile.coord.y - tiles[endCoord].coord.y) * 10;
                neighborTile.parentTile = curTile;

                openList.Add(neighborTile);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (waypoints != null && waypoints.Length != 0)
        {
            for (int i = 0; i < waypoints.Length - 1; i++)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
            }
        }            
    }
}
