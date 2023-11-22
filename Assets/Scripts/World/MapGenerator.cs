using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapGenerator : MonoBehaviour {
    [SerializeField] Transform tilePrefab;
    [SerializeField] Transform obstaclePrefab;
    [SerializeField] Transform floorNavMesh;
    [SerializeField] Transform floorNavMeshMask;
    [SerializeField, Range(1, 100)] int mapSizeX;
    [SerializeField, Range(1, 100)] int mapSizeY;
    static int maxMapSizeX = 100;
    static int maxMapSizeY = 100;
    [SerializeField, Range(0, 0.15f)] float outlinePercent;
    [SerializeField] float tileSize = 1;
    [SerializeField, Range(0, 1)] float obstaclePercent;
    [SerializeField, Range(int.MinValue, int.MaxValue)] int seed;

    public static int MaxMapSizeX => maxMapSizeX;
    public static int MaxMapSizeY => maxMapSizeY;
    List<Coord> allTileCoords;
    Queue<Coord> shuffledTileCoords;
    Coord playerSpawn;
    Coord enemySpawn;
    Coord objectiveCoord;

    void Start() {
        GenerateMap();
    }

    public void SetMapParameters(int sizeX, int sizeY, float obstaclePercent) {
        this.mapSizeX = sizeX;
        this.mapSizeY = sizeY;
        this.obstaclePercent = obstaclePercent;
    }
    public void GenerateMap() {
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
                newTile.localScale = Vector3.one /** ((1 - outlinePercent)*/ * tileSize;
                newTile.parent = mapHolder;
                if (x == playerSpawn.x && y == playerSpawn.y) {
                    //Spawn Units
                }
            }
        }

        bool[,] obstacleMap = new bool[mapSizeX, mapSizeY];
        int obstacleCount = (int)(mapSizeX * mapSizeY * obstaclePercent);
        int currentObstacleCount = 0;
        for (int i = 0; i < obstacleCount; i++) {
            
            Coord randomCoord = GetRandomCoord();
            obstacleMap[randomCoord.x, randomCoord.y] = true;
            currentObstacleCount++;
            if (randomCoord != playerSpawn && MapIsFullyAccessible(obstacleMap, currentObstacleCount)) {
                Vector3 obstaclePosition = CoordToPosition(randomCoord.x, randomCoord.y);
                Transform newObstacle = Instantiate(obstaclePrefab, obstaclePosition + Vector3.up * tileSize, Quaternion.identity);
                newObstacle.localScale = Vector3.one * ((1 - outlinePercent) * tileSize);
                newObstacle.parent = mapHolder;
            }
            else {
                obstacleMap[randomCoord.x, randomCoord.y] = false;
                currentObstacleCount--;
            }
        }

        Transform maskLeft = Instantiate(floorNavMeshMask, Vector3.left * ((mapSizeX + maxMapSizeX) / 4 * tileSize), Quaternion.identity);
        maskLeft.parent = mapHolder;
        maskLeft.localScale = new Vector3((maxMapSizeX-mapSizeX)/2, 2 ,mapSizeY*2) * tileSize;
        Transform maskRight = Instantiate(floorNavMeshMask, Vector3.right * ((mapSizeX + maxMapSizeX) / 4 * tileSize), Quaternion.identity);
        maskRight.parent = mapHolder;
        maskRight.localScale = new Vector3((maxMapSizeX-mapSizeX)/2, 2 ,mapSizeY*2) * tileSize;
        Transform maskTop = Instantiate(floorNavMeshMask, Vector3.forward * (((mapSizeY + maxMapSizeY) / 4 + 0.5f) * tileSize), Quaternion.identity);
        maskTop.parent = mapHolder;
        maskTop.localScale = new Vector3(mapSizeX, 2 , (maxMapSizeY - mapSizeY)/2) * tileSize;
        Transform maskBottom = Instantiate(floorNavMeshMask, Vector3.back * (((mapSizeY + maxMapSizeY) / 4 -0.5f)* tileSize), Quaternion.identity);
        maskBottom.parent = mapHolder;
        maskBottom.localScale = new Vector3(mapSizeX, 2 ,(maxMapSizeY - mapSizeY)/2) * tileSize;
        
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
        return new Vector3(-mapSizeX / 2 + 0.5f + x, outlinePercent/2, -mapSizeY / 2 + 0.5f + y) * tileSize;
    }
    
    Coord GetRandomCoord() {
        Coord randomCoord = shuffledTileCoords.Dequeue();
        shuffledTileCoords.Enqueue(randomCoord);
        return randomCoord;
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
}
