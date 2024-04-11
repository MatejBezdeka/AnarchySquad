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
    [Header("=== Game Settings ===")]
    [SerializeField, Range(1.1f, 4)] float maxTimeSpeed = 2;
    [SerializeField, Range(0.1f, 1f)] float minTimeSpeed = 0.2f;
    [SerializeField] MapGenerator mapGenerator;
    public Save currentSave;
    public MapGenerator MapGenerator => mapGenerator;
    #region variables
    float timeScale = 1;

    public enum timeState {
        hardPaused, normal
    }

    bool paused = false;
    timeState currentTimeState = timeState.normal;
    CanvasManager canvasManager;
    [SerializeField] List<SquadUnit> units;
    public List<SquadUnit> Units => units;
    public List<EnemyUnit> enemies /*{ get; private <- so I can edit it before I can spawn set; }*/;
    [SerializeField] GameObject unitPrefab;
    #endregion
    
    void Awake() {
        instance = this;
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
        AudioSettings.Music.ChangeAmbientMusic(AudioSettings.AmbientMusic.BatleField);
        AudioSettings.Music.StartMusic();
    }
    public void ChangeTime(float newTime) {
        switch (newTime) {
            //Hard pause/unpause
            case -3:
                if (currentTimeState == timeState.hardPaused) {
                    currentTimeState = timeState.normal;
                    canvasManager.StartTimer();
                    if (!paused) {
                        ChangeTime(-2);
                    }
                    return;
                }
                canvasManager.StopTimer();
                Time.timeScale = 0;
                AudioSettings.Music.PauseMusic();
                currentTimeState = timeState.hardPaused;
                break;
            case -2:
                //Unpause
                if (currentTimeState == timeState.hardPaused) {
                    return;
                }

                paused = false;
                ChangeTime(timeScale);
                AudioSettings.Music.ResumeMusic();
                break;
            case -1:
                //Pause
                if (currentTimeState == timeState.hardPaused) {
                    return;
                }
                if (Time.timeScale != 0) {
                    timeScale = Time.timeScale;
                }
                paused = true;
                Time.timeScale = 0;
                canvasManager.ChangeTimeLabelText("Paused");
                AudioSettings.Music.PauseMusic();
                break;
            default:
                if (currentTimeState == timeState.hardPaused) {
                    return;
                }
                //change time speed
                if (newTime > maxTimeSpeed || newTime < minTimeSpeed) {
                    return;
                }
                if (newTime == 0) {
                    newTime = timeScale;
                }
                Time.timeScale = newTime;
                canvasManager.ChangeTimeLabelText($"{Time.timeScale:0.00}" + " x");
                break;
        }
    }

    void PrepareGameField() {
        
    }

    void SpawnUnits(Vector3 spawnPoint) {
        for (int i = 0; i < SquadParameters.Units.Count; i++) {
            Vector3 rotatedSpawnPoint = spawnPoint.GetRotatedVector3(SquadParameters.Units.Count, i);
            rotatedSpawnPoint.y += 1;
            //GameObject newUnit = Instantiate(unitPrefab, new Vector3(rotatedSpawnPoint.x, rotatedSpawnPoint.y + 1f, rotatedSpawnPoint.z), Quaternion.identity);
            units.Add(UnitFactory.SpawnUnit(unitPrefab, SquadParameters.Units[i], rotatedSpawnPoint) as SquadUnit) ;
            
        }
    }
}
