using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClassComponent : MonoBehaviour {
    public static event Action<SquadUnit> AddUnit;
    [SerializeField] TextMeshProUGUI classText;
    [SerializeField] Button addButton;
    [SerializeField] Button nextButton;
    [SerializeField] Button backButton;
    //tmp seriealze
    [SerializeField] SquadUnit currentUnit;
    void Start()
    {
        addButton.onClick.AddListener(AddButtonPressed);
        nextButton.onClick.AddListener(NextButtonPressed);
        backButton.onClick.AddListener(BackButtonPressed);
    }

    void AddButtonPressed() {
        AddUnit.Invoke(currentUnit);
    }

    void NextButtonPressed() {
        //TODO
    }

    void BackButtonPressed() {
        //TODO
    }

    void InfoButtonPressed() {
        //TODO
    }
}
