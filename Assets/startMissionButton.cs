using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class startMissionButton : MonoBehaviour
{
    private void Start() {
        GetComponent<Button>().onClick.AddListener(StartMission);
    }

    void StartMission() {
        
        SceneManager.LoadScene("Battlefield");
    }
}
