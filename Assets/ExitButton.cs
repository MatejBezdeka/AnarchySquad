using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ExitButton : MonoBehaviour
{
    void Start() {
        GetComponent<Button>().onClick.AddListener(ExitApp);
    }

    void ExitApp() {
        Application.Quit();
    }
}
