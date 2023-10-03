using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CanvasManager))]
public class GameManager : MonoBehaviour {
    //Singleton
    public static GameManager instance;
    
    [Header("=== Game Settings ===")]
    [SerializeField, Range(1.1f, 4)] float maxTimeSpeed = 2;
    [SerializeField, Range(0.1f, 1f)] float minTimeSpeed = 0.2f;

    #region variables
    float time = 1;
    CanvasManager canvasManager;

    #endregion
    void Awake() {
        instance = this;
        PlayerControl.changedTime += TimeChanged;
    }

    void Start() {
        canvasManager = GetComponent<CanvasManager>();
    }

    void FixedUpdate() {
        
    }

    void Update() {
        //Debug.Log("Updating");
    }

    int debug = 0;
    void TimeChanged(float newTime) {
        // if (deb == 0) {
        //     debug++;
        // }
        // deb++;
        // Debug.Log(newTime + " " + deb + ":" + debug);

        if (newTime == -2) {
            Debug.Log( "aa");

            //Unpause
            TimeChanged(time);
            //TODO
        }else if (newTime < minTimeSpeed && newTime > minTimeSpeed-0.5 || newTime < 0) {
            //Debug.Log( "bb     " + time + "   " + Time.timeScale + "     " + newTime);
            Debug.Log((newTime < minTimeSpeed) + " "  + (newTime > minTimeSpeed-0.5) + " " + (newTime < 0) + " ");
            //Pause
            if (Time.timeScale != 0) {
                time = Time.timeScale;
            }
            Time.timeScale = 0;
            canvasManager.ChangeTimeLabelText("Paused");
        }
        else {
            Debug.Log( "cc");

            if (newTime > maxTimeSpeed) {
                newTime = maxTimeSpeed;
            }
            Time.timeScale = newTime;
            canvasManager.ChangeTimeLabelText($"{Time.timeScale:0.00}" + " x");
        }
    }
}
