using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
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

    private List<Coord> allTileCoords;
    private Queue<Coord> shuffledTileCoords;

    Coord centerCoord;

    public Transform tilePrefab;    // Node
    public Transform obstaclePrefab;
    public Vector2 mapSize;

    [Range(0,1)]
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
        allTileCoords = new List<Coord>();

        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                allTileCoords.Add(new Coord(x, y));
            }
        }
        shuffledTileCoords = new Queue<Coord>(Util.SuffleArray(allTileCoords.ToArray(), seed));

        // TODO: 시작지점 AND 종료지점을 알아야 함(startCoord / endCoord)

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
                //GameObject newTime = Managers.Resource.Instantiate(tilePrefab.gameObject, tilePosition, Quaternion.Euler(Vector3.right * 90), mapHolder);
                Transform newTime = Instantiate(tilePrefab, tilePosition, Quaternion.Euler(Vector3.right * 90), mapHolder) as Transform;
                newTime.transform.localScale = Vector3.one * (1 - outlinePercent);
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
            }
            else
            {
                obstacleMap[randomCoord.x, randomCoord.y] = false;
                currentObstacleCount--;
            }
        }
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
}
