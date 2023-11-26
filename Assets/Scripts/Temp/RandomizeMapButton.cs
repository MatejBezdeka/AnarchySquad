using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class RandomizeMapButton : MonoBehaviour {
    [SerializeField] TextMeshProUGUI text;
    Random rn = new Random();
    private string[] o = new[] { "Low", "Med","High" };
    [Header("RandomGeneration Settings")] 
    [SerializeField, Range(0,100)] int minObstaclePercentage = 10;
    [SerializeField, Range(0,100)] int maxObstaclePercentage = 80;
    [SerializeField, Tooltip("Biggest difference tile count between sides")] int maxSideDifference = 10;
    void Start() {
        GetComponent<Button>().onClick.AddListener(Randomize);
        Randomize();
    }

    void Randomize() {
        int seed = rn.Next(int.MinValue, int.MaxValue);
        Random psrn = new Random(seed);
        int sizeX = psrn.Next(MapGenerator.minMapSizeX,MapGenerator.maxMapSizeX);
        int sizeY = psrn.Next(
            sizeX - maxSideDifference < MapGenerator.minMapSizeY ? MapGenerator.minMapSizeY : sizeX - maxSideDifference,
            sizeX + maxSideDifference > MapGenerator.maxMapSizeY ? MapGenerator.maxMapSizeY : sizeX + maxSideDifference);
        float obstaclePercent = psrn.Next(minObstaclePercentage, maxObstaclePercentage) / 100f;
        text.text = "SizeX: " + sizeX + "\nSizeY: " + sizeY + "\nObstacles: " + GetText(obstaclePercent) + "(" + obstaclePercent + ")" +"\nSeed: " + seed;
        MapParameters.SetMapParameters(sizeX, sizeY, obstaclePercent, seed);
    }

    string GetText(float obstaclePer) {
        int difference = maxObstaclePercentage - minObstaclePercentage;
        difference /= o.Length;
        for (int i = 0; i < o.Length; i++) {
            if (i * difference >= obstaclePer * 100) {
                return o[i];
            }
        }
        return "null";
    }
}
