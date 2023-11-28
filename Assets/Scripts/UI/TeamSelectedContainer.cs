using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;

public class TeamSelectedContainer : MonoBehaviour {
    [SerializeField] GameObject memberContainer;
    [SerializeField] private TextMeshProUGUI unitCounter;
    private int maxUnitsCount = 6;
    private List<SquadUnit> units = new List<SquadUnit>();
    void Start() {
        ClassComponent.AddUnit += AddUnit;
        SelectedTeamMemberContainer.RemoveUnit += RemoveUnit;
        StartMissionButton.startingGame += SaveUnitsForNextScene;
    }

    void RemoveUnit(SquadUnit unit) {
        units.Remove(unit);
        UpdateCount();
    }

    void AddUnit(SquadUnit unit) {
        if (units.Count == maxUnitsCount) {
            Debug.Log("max num");
            return;
        }
        GameObject container = Instantiate(memberContainer, transform);
        container.GetComponent<SelectedTeamMemberContainer>().Set(unit);
        units.Add(unit);
        UpdateCount();
    }

    void UpdateCount() {
        unitCounter.text = units.Count + "/" + maxUnitsCount;
    }

    void SaveUnitsForNextScene() {
        SquadParameters.units = units;
    }
}
