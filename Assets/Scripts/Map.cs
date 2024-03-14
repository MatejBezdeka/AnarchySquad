using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map {
    int difficulty;
    int seed;
    int sizeX;
    int sizeY;
    int obstaclePrecentage;

    public Map(int difficulty, int seed, int sizeX, int sizeY, int obstaclePrecentage) {
        this.difficulty = difficulty;
        this.seed = seed;
        this.sizeX = sizeX;
        this.sizeY = sizeY;
        this.obstaclePrecentage = obstaclePrecentage;
    }
}
