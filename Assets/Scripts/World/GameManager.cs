using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CanvasManager))]
public class GameManager : MonoBehaviour {
    //Singleton
    public static GameManager instance = new GameManager();
    
    [Header("=== Game Settings ===")]
    [SerializeField, Range(1.1f, 4)] float maxTimeSpeed = 2;
    [SerializeField, Range(0.1f, 1f)] float minTimeSpeed = 0.2f;
    [SerializeField] MapGenerator mapGenerator;
    #region variables
    float time = 1;
    CanvasManager canvasManager;
    public List<SquadUnit> Squaders /*{ get; private <- so I can edit it before I can spawn set; }*/ = new List<SquadUnit>();
    public List<EnemyUnit> Enemies /*{ get; private <- so I can edit it before I can spawn set; }*/ = new List<EnemyUnit>();
    #endregion
    
    //
    private int sizeX;
    private int sizeY;
    private float obstaclePercent;
    private int seed;
    void Awake() {
        instance = this;
        PlayerControl.changedTime += TimeChanged;
        
    }
    void Start() {
        canvasManager = GetComponent<CanvasManager>();
        mapGenerator.SetMapParameters(sizeX, sizeY, obstaclePercent, seed);  
        mapGenerator.GenerateMap();
        //generate
        //spawnpoints
        //spawnUnits
        //startGame
        //wakeup units
        //Start UI
    }

    void FixedUpdate() {
        
    }

    void Update() {
        //Debug.Log("Updating");
    }
    
    public void SetMap(int sizeX, int sizeY, float obstaclePercent, int seed) {
        this.sizeX = sizeX;
        this.sizeY = sizeY;
        this.obstaclePercent = obstaclePercent;
        this.seed = seed;
    }
    void TimeChanged(float newTime) {
        if ((int)newTime == -2) {
            //Unpause
            TimeChanged(time);
        }else if ((int)newTime == -1) {
            //Pause
            if (Time.timeScale != 0) {
                time = Time.timeScale;
            }
            Time.timeScale = 0;
            canvasManager.ChangeTimeLabelText("Paused");
        }else {
            //change time speed
            if (newTime > maxTimeSpeed || newTime < minTimeSpeed) {
                return;
            }
            if (newTime == 0) {
                newTime = time;
            }

            Time.timeScale = newTime;
            canvasManager.ChangeTimeLabelText($"{Time.timeScale:0.00}" + " x");
        }
    }

    void PrepareGameField() {
        
    }
}
