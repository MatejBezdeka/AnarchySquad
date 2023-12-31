using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapGenerator : MonoBehaviour {
    [Header("Settings")]
    [SerializeField] Transform tilePrefab;
    [SerializeField] Transform obstaclePrefab;
    [SerializeField] Transform floorNavMesh;
    [SerializeField] Transform floorNavMeshMask;
    [SerializeField] List<Transform> roofsPrefab;
    [SerializeField, Range(10, 22)] int mapSizeX;
    [SerializeField, Range(10, 22)] int mapSizeY;
    [SerializeField, Range(1, 10)] int minBuildingSize;
    [SerializeField, Range(5, 25)] int maxBuildingSize;
    
    [SerializeField, Range(0, 0.15f)] float outlinePercent;
    [SerializeField] float tileSize = 1;
    [SerializeField, Range(0, 1)] float obstaclePercent;
    [SerializeField, Range(int.MinValue+64, int.MaxValue-64)] int seed = 0;
    //When changing max size you have to rebake the navMesh!
    public static int maxMapSizeX => 22;
    public static int maxMapSizeY => 22;
    public static int minMapSizeX => 10;
    public static int minMapSizeY => 10;

    List<Coord> allTileCoords;
    Queue<Coord> shuffledTileCoords;
    Queue<Coord> shuffledOpenTileCoords;
    Coord playerSpawn;
    Coord enemySpawn;
    Coord objectiveCoord;

    Transform[,] tileMap;

    void Awake() {
        SetMapParameters();
    }

    void SetMapParameters() {
        if (MapParameters.sizeX == 0) {
            return;
        }
        mapSizeX = MapParameters.sizeX;
        mapSizeY = MapParameters.sizeY;
        obstaclePercent = MapParameters.obstaclePercent;
        seed = MapParameters.seed;
    }
    public void GenerateMap() {
        transform.position = new Vector3(0, -tileSize, 0);
        tileMap = new Transform[mapSizeX,mapSizeY];
        allTileCoords = new List<Coord>();
        for (int x = 0; x < mapSizeX; x++) {
            for (int y = 0; y < mapSizeY; y++) {
                allTileCoords.Add(new Coord(x, y));
            }
        }

        shuffledTileCoords = new Queue<Coord>(Extensions.ShuffleArray(allTileCoords.ToArray(), seed));
        System.Random rn = new System.Random(seed);
        playerSpawn = new Coord(rn.Next(0,mapSizeX), rn.Next(0,mapSizeY));
        string holderName = "generatedMap";
        if (transform.Find(holderName)) {
            DestroyImmediate(transform.Find(holderName).gameObject);
        }

        Transform mapHolder = new GameObject(holderName).transform;
        mapHolder.parent = transform;
        for (int x = 0; x < mapSizeX; x++) {
            for (int y = 0; y < mapSizeY; y++) {
                
                Vector3 tilePosition = CoordToPosition(x, y);
                Transform newTile = Instantiate(tilePrefab, tilePosition, Quaternion.Euler(0, 0, 0));
                newTile.localScale = Vector3.one /* * ((1 - outlinePercent)*/ * tileSize;
                newTile.parent = mapHolder;
                tileMap[x, y] = newTile;
            }
        }

        bool[,] obstacleMap = new bool[mapSizeX, mapSizeY];
        int obstacleCount = (int)(mapSizeX * mapSizeY * obstaclePercent);
        int currentObstacleCount = 0;
        List<Coord> allOpenCoords = allTileCoords;

        for (int i = 0; i < obstacleCount; i++) {
            
            Coord randomCoord = GetRandomCoord();
            obstacleMap[randomCoord.x, randomCoord.y] = true;
            currentObstacleCount++;
            if (randomCoord != playerSpawn && MapIsFullyAccessible(obstacleMap, currentObstacleCount)) {
                Vector3 obstaclePosition = CoordToPosition(randomCoord.x, randomCoord.y);
                float newScale = (1 - outlinePercent) * tileSize;
                newScale = rn.Next(minBuildingSize, maxBuildingSize);
                //obstaclePosition.y = tileSize + (tileSize - newScale.y/2);
                Transform newObstacle = Instantiate(obstaclePrefab, obstaclePosition + Vector3.up * (tileSize/2 + newScale/2), Quaternion.Euler(0,rn.Next(0,4) * 90,0));
                newObstacle.localScale = new Vector3(tileSize,newScale,tileSize);
                newObstacle.parent = mapHolder;
                allOpenCoords.Remove(randomCoord);
            }
            else {
                obstacleMap[randomCoord.x, randomCoord.y] = false;
                currentObstacleCount--;
            }
        }
        
        shuffledOpenTileCoords = new Queue<Coord>(Extensions.ShuffleArray(allOpenCoords.ToArray(), seed));
        Transform maskLeft = Instantiate(floorNavMeshMask, Vector3.left * ((mapSizeX + maxMapSizeX) / 4f * tileSize), Quaternion.identity);
        maskLeft.parent = mapHolder;
        maskLeft.localScale = new Vector3((maxMapSizeX-mapSizeX)/2f, 2 ,mapSizeY*2) * tileSize;
        Transform maskRight = Instantiate(floorNavMeshMask, Vector3.right * ((mapSizeX + maxMapSizeX) / 4f * tileSize), Quaternion.identity);
        maskRight.parent = mapHolder;
        maskRight.localScale = new Vector3((maxMapSizeX-mapSizeX)/2f, 2 ,mapSizeY*2) * tileSize;
        Transform maskTop = Instantiate(floorNavMeshMask, Vector3.forward * (((mapSizeY + maxMapSizeY) / 4f) * tileSize), Quaternion.identity);
        maskTop.parent = mapHolder;
        maskTop.localScale = new Vector3(mapSizeX, 2 , (maxMapSizeY - mapSizeY)/2f) * tileSize;
        Transform maskBottom = Instantiate(floorNavMeshMask, Vector3.back * (((mapSizeY + maxMapSizeY) / 4f)* tileSize), Quaternion.identity);
        maskBottom.parent = mapHolder;
        maskBottom.localScale = new Vector3(mapSizeX, 2 ,(maxMapSizeY - mapSizeY)/2f) * tileSize;
        
        floorNavMesh.localScale = new Vector3(maxMapSizeX, 1, maxMapSizeY) * tileSize;
    }

    bool MapIsFullyAccessible(bool[,] obstacleMap, int currentObstacleCount) {
        bool[,] mapFlags = new bool[obstacleMap.GetLength(0), obstacleMap.GetLength(1)];
        Queue<Coord> queue = new Queue<Coord>();
        queue.Enqueue(playerSpawn);
        mapFlags[playerSpawn.x, playerSpawn.y] = true;
        int accessibleTileCount = queue.Count;

        while (queue.Count > 0) {
            Coord tile = queue.Dequeue();
            for (int x = -1; x <= 1; x++) {
                for (int y = -1; y <= 1; y++) {
                    int neighborX = tile.x + x;
                    int neighborY = tile.y + y;
                    if (x == 0 ^ y == 0) {
                        if (neighborX >= 0 && neighborX < obstacleMap.GetLength(0) && neighborY >= 0 &&
                            neighborY < obstacleMap.GetLength(1)) {
                            if (!mapFlags[neighborX, neighborY] && !obstacleMap[neighborX, neighborY]) {
                                mapFlags[neighborX, neighborY] = true;
                                queue.Enqueue(new Coord(neighborX, neighborY));
                                accessibleTileCount++;
                            }
                        }
                    }
                }
            }
        }

        int targetAccessibleTileCount = (mapSizeX * mapSizeY - currentObstacleCount);
        return targetAccessibleTileCount == accessibleTileCount;
    }

    Vector3 CoordToPosition(int x, int y) {
        return new Vector3(-mapSizeX / 2f + 0.5f + x, outlinePercent/2, -mapSizeY / 2f + 0.5f + y) * tileSize;
    }
    
    Coord GetRandomCoord() {
        Coord randomCoord = shuffledTileCoords.Dequeue();
        shuffledTileCoords.Enqueue(randomCoord);
        return randomCoord;
    }

    public Transform GetRandomOpenTile() {
        Coord randomCoord = shuffledOpenTileCoords.Dequeue();
        shuffledOpenTileCoords.Enqueue(randomCoord);
        return tileMap[randomCoord.x,randomCoord.y];
    }
    struct Coord {
        public int x;
        public int y;

        public Coord(int x, int y) {
            this.x = x;
            this.y = y;
        }

        public static bool operator == (Coord c1, Coord c2) {
            return c1.x == c2.x && c1.y == c2.y;
        }
        public static bool operator != (Coord c1, Coord c2) {
            return !(c1 == c2);
        }
    }
    public Vector3 SpawnCubePosition() {
        Vector3 point = CoordToPosition(playerSpawn.x, playerSpawn.y);
        point.y += tileSize / 2;
        Debug.Log(point);
        return point;
    }
}
