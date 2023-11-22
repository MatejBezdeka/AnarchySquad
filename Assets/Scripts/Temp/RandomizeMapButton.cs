using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RandomizeMapButton : MonoBehaviour {
    [SerializeField] TextMeshProUGUI text;
    int sizeX;
    int sizeY;
    int obstaclePercent;
    int seed;
    
    void Start() {
        GetComponent<Button>().onClick.AddListener(Randomize);
    }

    void Randomize() {
        
    }
}
