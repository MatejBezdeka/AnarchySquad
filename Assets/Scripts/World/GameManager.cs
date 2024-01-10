using System;
using System.Collections;
using System.Collections.Generic;
using SceneBridges;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(CanvasManager))]
public class GameManager : MonoBehaviour {
    //Singleton
    public static GameManager instance = new GameManager();
    UnitFactory unitFactory = new UnitFactory();
    
    [Header("=== Game Settings ===")]
    [SerializeField, Range(1.1f, 4)] float maxTimeSpeed = 2;
    [SerializeField, Range(0.1f, 1f)] float minTimeSpeed = 0.2f;
    [SerializeField] MapGenerator mapGenerator;
    public MapGenerator MapGenerator => mapGenerator;
    #region variables
    float time = 1;
    CanvasManager canvasManager;
    public List<SquadUnit> units;
    public List<EnemyUnit> Enemies /*{ get; private <- so I can edit it before I can spawn set; }*/ = new List<EnemyUnit>();
    [SerializeField] GameObject[] tmpList;
    [SerializeField] GameObject unitPrefab;
    #endregion
    
    void Awake() {
        instance = this;
        PlayerControl.changedTime += TimeChanged;
        //generate
        canvasManager = GetComponent<CanvasManager>();
        mapGenerator.GenerateMap();
        //spawnUnits
        SpawnUnits(mapGenerator.SpawnCubePosition());
    }
    void Start() {
        //startGame
        //wakeup units
        //Start UI
    }

    void FixedUpdate() {
        
    }

    void Update() {
        //Debug.Log("Updating");
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

    void SpawnUnits(Vector3 spawnPoint) {
        for (int i = 0; i < SquadParameters.Units.Count; i++) {
            Vector3 rotatedSpawnPoint = spawnPoint.GetRotatedVector3(SquadParameters.Units.Count, i);
            rotatedSpawnPoint.y += 1;
            //GameObject newUnit = Instantiate(unitPrefab, new Vector3(rotatedSpawnPoint.x, rotatedSpawnPoint.y + 1f, rotatedSpawnPoint.z), Quaternion.identity);
            //Instantiate(tmpList[i], new Vector3(rotatedSpawnPoint.x, rotatedSpawnPoint.y + 1f, rotatedSpawnPoint.z), Quaternion.identity);
            units.Add(unitFactory.SpawnUnit(unitPrefab, SquadParameters.Units[i], rotatedSpawnPoint));
            
        }
    }
}
