using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MapParameters {
    public static int sizeX { get; private set; }
    public static int sizeY { get; private set; }
    public static float obstaclePercent { get; private set; }
    public static int seed { get; private set; }
    public static void SetMapParameters(int sizeX, int sizeY, float obstaclePercent, int seed) {
        MapParameters.sizeX = sizeX;
        MapParameters.sizeY = sizeY;
        MapParameters.obstaclePercent = obstaclePercent;
        MapParameters.seed = seed;
    }
}
