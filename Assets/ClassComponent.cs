using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClassComponent : MonoBehaviour {
    [SerializeField] TextMeshProUGUI classText;
    [SerializeField] Button addButton;
    [SerializeField] Button nextButton;
    [SerializeField] Button backButton;
    [SerializeField] Button infoButton;
    //tmp seriealze
    [SerializeField] SquadUnit currentUnit;
    void Start()
    {
        addButton.onClick.AddListener(AddButtonPressed);
        nextButton.onClick.AddListener(NextButtonPressed);
        backButton.onClick.AddListener(BackButtonPressed);
        infoButton.onClick.AddListener(InfoButtonPressed);
    }

    void AddButtonPressed() {
        
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
