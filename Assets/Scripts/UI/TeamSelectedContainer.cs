using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;

public class TeamSelectedContainer : MonoBehaviour {
    [SerializeField] GameObject memberPrefab;
    [SerializeField] private TextMeshProUGUI unitCounter;
    [SerializeField] GameObject plusMemberPrefab;
    private int maxUnitsCount = 6;
    private List<SquadUnit> units = new List<SquadUnit>();
    private int selected;
    void Start() {
        Shop.addStats += AddUnit;
        SelectedTeamMemberContainer.RemoveUnit += RemoveUnit;
        StartMissionButton.startingGame += SaveUnitsForNextScene;
        plusMemberPrefab = Instantiate(plusMemberPrefab, transform);

    }

    void RemoveUnit(SquadUnit unit) {
        units.Remove(unit);
        UpdateCount();
    }

    void AddUnit(Stats unitStats) {
        SquadUnit unit = new SquadUnit();
        unit.stats = unitStats;
        if (units.Count == maxUnitsCount) {
            return;
        }
        GameObject container = Instantiate(memberPrefab, transform);
        container.GetComponent<SelectedTeamMemberContainer>().Set(unit);
        units.Add(unit);
        UpdateCount();
    }

    void UpdateCount() {
        unitCounter.text = units.Count + "/" + maxUnitsCount;
        plusMemberPrefab.SetActive(true);
        plusMemberPrefab.transform.SetSiblingIndex(transform.childCount);
        if (units.Count == maxUnitsCount) {
            plusMemberPrefab.SetActive(false);
        }
    }

    void SaveUnitsForNextScene() {
        SquadParameters.units = units;
    }
}
