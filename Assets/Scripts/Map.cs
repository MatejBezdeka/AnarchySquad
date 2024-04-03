using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
[Serializable]
public class Map {
    private string[] mapNames = new[] { "Alpha", "Beta", "Gamma", "Delta", "Epsilon", "Zeta", "Eta", "Theta", "Iota", "Kappa", "Lambda", "Mu", "Nu", "Xi", "Omicron", "Pi", "Rho", "Sigma", "Tau", "Upsilon", "Phi", "Chi", "Psi", "Omega" };
    public string[] Name => mapNames;
    int difficulty;
    public int Diffculty => difficulty;
    int seed;
    public int Seed => seed;
    int sizeX;
    int sizeY;
    public int Size => sizeX * sizeY;
    public int SizeX => sizeX;
    public int SizeY => sizeY;

    float obstaclePrecentage;
    public float BuildingDensity => obstaclePrecentage;
    private Objective objective;
    public Objective Objective => objective;

    /*public Map(int difficulty, int seed, int sizeX, int sizeY, int obstaclePrecentage) {
        this.difficulty = difficulty;
        this.seed = seed;
        this.sizeX = sizeX;
        this.sizeY = sizeY;
        this.obstaclePrecentage = obstaclePrecentage;
    }*/

    public Map(int seed, int maxSideDifference, int minObstaclePercentage, int maxObstaclePercentage, int difficulty, Objective objective) {
        this.seed = seed;
        this.difficulty = difficulty;
        this.objective = objective;
        Random rn = new Random(seed);
        sizeX = rn.Next(MapGenerator.minMapSizeX,MapGenerator.maxMapSizeX);
        sizeY = rn.Next(
            sizeX - maxSideDifference < MapGenerator.minMapSizeY ? MapGenerator.minMapSizeY : sizeX - maxSideDifference,
            sizeX + maxSideDifference > MapGenerator.maxMapSizeY ? MapGenerator.maxMapSizeY : sizeX + maxSideDifference);
        obstaclePrecentage = rn.Next(minObstaclePercentage, maxObstaclePercentage) / 100f;
    }
}
