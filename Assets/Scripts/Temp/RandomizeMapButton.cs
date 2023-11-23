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
    [SerializeField] int maxSizeDifference = 10;
    int sizeX;
    int sizeY;
    float obstaclePercent;
    int seed;
    Random rn = new Random();
    
    
    void Start() {
        GetComponent<Button>().onClick.AddListener(Randomize);
    }

    void Randomize() {
        seed = rn.Next(int.MinValue, int.MaxValue);
        Random psrn = new Random(seed);
        sizeX = psrn.Next(MapGenerator.minMapSizeX,MapGenerator.maxMapSizeX);
        sizeY = psrn.Next(
            sizeX - maxSizeDifference < MapGenerator.minMapSizeY ? MapGenerator.minMapSizeY : sizeX - maxSizeDifference,
            sizeX + maxSizeDifference > MapGenerator.maxMapSizeY ? MapGenerator.maxMapSizeY : sizeX + maxSizeDifference);
        obstaclePercent = psrn.Next(20, 101) / 100f;
        text.text = "SizeX: " + sizeX + "\nSizeY: " + sizeY + "\nObstacle Percent: " + obstaclePercent+"\nSeed: " + seed;
        MapParameters.SetMapParameters(sizeX, sizeY, obstaclePercent, seed);
    }
}
