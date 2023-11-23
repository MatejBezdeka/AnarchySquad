using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class RandomizeMapButton : MonoBehaviour {
    [SerializeField] TextMeshProUGUI text;
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
        sizeY = psrn.Next(MapGenerator.minMapSizeY,MapGenerator.maxMapSizeY);
        obstaclePercent = psrn.Next(20, 101) / 100f;
        text.text = "SizeX: " + sizeX + "\nSizeY: " + sizeY + "\nObstacle Percent: " + obstaclePercent+"\nSeed: " + seed;
        GameManager.instance.SetMap(sizeX, sizeY, obstaclePercent, seed);
    }
}
