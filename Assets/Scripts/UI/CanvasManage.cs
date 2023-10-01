using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class CanvasManage : MonoBehaviour {
    [SerializeField, Range(1.1f, 4)] float maxTimeSpeed = 2;
    float currentTimeSpeed = 1;
    [SerializeField] TextMeshProUGUI timeText;
    InputAction timeAction;

    [SerializeField] TextMeshProUGUI objectiveText;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void ChangeTime() {
    }
}
