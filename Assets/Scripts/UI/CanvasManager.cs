using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class CanvasManager : MonoBehaviour {
    
    [SerializeField] TextMeshProUGUI timeText;
    InputAction timeAction;

    [SerializeField] TextMeshProUGUI objectiveText;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ChangeTimeLabelText(string text) {
        timeText.text = text;
    }
}
