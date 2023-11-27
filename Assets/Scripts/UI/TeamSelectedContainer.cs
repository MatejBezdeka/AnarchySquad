using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamSelectedContainer : MonoBehaviour {
    [SerializeField] GameObject memberContainer;
    private int maxUnitsCount = 6;
    private List<SquadUnit> units = new List<SquadUnit>();
    void Start() {
        ClassComponent.AddUnit += AddUnit;
        SelectedTeamMemberContainer.RemoveUnit += RemoveUnit;
    }

    void RemoveUnit(SquadUnit unit) {
        units.Remove(unit);
        
    }

    void AddUnit(SquadUnit unit) {
        if (units.Count == maxUnitsCount) {
            Debug.Log("max num");
            return;
        }
        GameObject container = Instantiate(memberContainer, transform);
        container.GetComponent<SelectedTeamMemberContainer>().Set(unit);
        units.Add(unit);
    }
}
