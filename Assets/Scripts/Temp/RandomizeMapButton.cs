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
    string[] obstacleIntensityTitles = new[] { "Low", "Med","High" };
    [Header("RandomGeneration Settings")] 
    [SerializeField, Range(0,100)] int minObstaclePercentage = 15;
    [SerializeField, Range(0,100)] int maxObstaclePercentage = 70;
    [SerializeField, Tooltip("Biggest difference tile count between sides")] int maxSideDifference = 10;
    void Start() {
        GetComponent<Button>().onClick.AddListener(Randomize);
        Randomize();
        /*for (int i = minObstaclePercentage; i < maxObstaclePercentage+1; i++) {
            Debug.Log( i + ": " + GetText(  i/100f));
        }*/
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
        float difference = maxObstaclePercentage - minObstaclePercentage;
        difference /= obstacleIntensityTitles.Length;
        for (int i = 1; i < obstacleIntensityTitles.Length+1; i++) {
            if (i * difference >= obstaclePer * 100 - minObstaclePercentage) {
                return obstacleIntensityTitles[i-1];
            }
        }
        return "null";
    }
}
