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
    public MapGenerator MapGenerator => mapGenerator;
    #region variables
    float time = 1;
    CanvasManager canvasManager;
    public List<SquadUnit> Squaders /*{ get; private <- so I can edit it before I can spawn set; }*/ = new List<SquadUnit>();
    public List<EnemyUnit> Enemies /*{ get; private <- so I can edit it before I can spawn set; }*/ = new List<EnemyUnit>();
    [SerializeField] GameObject[] tmpList;
    #endregion
    
    void Awake() {
        instance = this;
        PlayerControl.changedTime += TimeChanged;
    }
    void Start() {
        //generate
        canvasManager = GetComponent<CanvasManager>();
        mapGenerator.GenerateMap();
        //spawnUnits
        SpawnUnits(mapGenerator.SpawnCubePosition());
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
        //Debug.Log(spawnPoint);
        /*for (int i = 0; i < Squaders.Count; i++) {
            Vector3 rotatedSpawnPoint = spawnPoint.GetRotatedVector3(Squaders.Count, i);
            Debug.Log(Squaders[i].transform.position);
            Squaders[i].gameObject.transform.position = new Vector3(rotatedSpawnPoint.x, rotatedSpawnPoint.y + 1f, rotatedSpawnPoint.z);
            Debug.Log(Squaders[i].transform.position);
            
        }*/
        
        for (int i = 0; i < tmpList.Length; i++) {
            Vector3 rotatedSpawnPoint = spawnPoint.GetRotatedVector3(tmpList.Length, i);
            Instantiate(tmpList[i], new Vector3(rotatedSpawnPoint.x, rotatedSpawnPoint.y + 1f, rotatedSpawnPoint.z), Quaternion.identity);
            
        }
    }
}
