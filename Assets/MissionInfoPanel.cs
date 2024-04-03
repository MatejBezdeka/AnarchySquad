using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class MissionInfoPanel : MonoBehaviour {
    private Random rn;
    [Header("=== Mission panel ===")]
    [SerializeField] TextMeshProUGUI missionName;
    [SerializeField] TextMeshProUGUI missionDifficulty;
    [SerializeField] TextMeshProUGUI missionObjective;
    [SerializeField] TextMeshProUGUI missionMapSize;
    [SerializeField] TextMeshProUGUI missionBuildingDensity;
    [SerializeField] Objective[] objectives;
    [Header("=== Current save info panel ===")]
    [SerializeField] TextMeshProUGUI points;
    [SerializeField] TextMeshProUGUI missionsDone;
    [Header("=== Map ===")] 
    [SerializeField] Transform map;
    [SerializeField] GameObject missionPrefab;
    [Header("=== RandomGeneration Settings ===")] 
    [SerializeField, Range(0,100)] int minObstaclePercentage = 5;
    [SerializeField, Range(0,100)] int maxObstaclePercentage = 70;
    [SerializeField, Tooltip("Biggest difference tile count between sides")] int maxSideDifference = 5;
    [Header("=== Continue Button ===")]
    [SerializeField] GameObject continueButton;
    readonly string[] obstacleDensityTitles = { "Empty", "Sparse", "Moderate", "Dense", "Crowded"};
    readonly string[] sizeTitles = { "Tiny", "Small", "Moderate", "Large", "Huge"};
    List<IdMissionButton> missionPoints = new List<IdMissionButton>();
    private List<Map> missions = new List<Map>();
    private int selectedMapId = -1;

    void Start() {
        Save currentSave = Save.GetSave(0);
        rn = new Random(currentSave.currentSeed);
        //load save
        //new/old seed
        //difficulty
        //objectives
        IdMissionButton.ButtonClicked += SelectMission;
        IdMissionButton.ButtonPointerEntered += ShowNewMissionInfo;
        IdMissionButton.ButtonPointerLeft += HideInfo;
        Rect mapRect = map.GetComponent<RectTransform>().rect;
        Rect missionRect = missionPrefab.GetComponent<RectTransform>().rect;
        bool overlapping;
        int posx;
        int posy;
        for (int i = 0; i < 5; i++) {
            int j = 0;
            do {
                overlapping = false;
                posx = rn.Next((int)(-mapRect.width / 2) + (int)missionRect.width, (int)(mapRect.width / 2) - (int)missionRect.width);
                posy = rn.Next((int)(-mapRect.height / 2) + (int)missionRect.height, (int)(mapRect.height / 2) - (int)missionRect.height);
                foreach (var point in missionPoints) {
                    float distanceX = Mathf.Abs(posx - point.transform.localPosition.x);
                    float distanceY = Mathf.Abs(posy - point.transform.localPosition.y);

                    if (distanceX < missionRect.width && distanceY < missionRect.height)
                    {
                        overlapping = true;
                    }
                }
                j++;
                if (j == 50) {
                    overlapping = false;
                }
            } while (overlapping);
            
            IdMissionButton newPoint = Instantiate(missionPrefab, map).GetComponent<IdMissionButton>();
;            newPoint.id = missionPoints.Count;
            missionPoints.Add(newPoint);
            missions.Add(new Map(rn.Next(int.MinValue+64, int.MaxValue-64), maxSideDifference, minObstaclePercentage, maxObstaclePercentage, 1, objectives[rn.Next(0,objectives.Length)]));
            newPoint.transform.localPosition = new Vector3(posx, posy, 0);
        }  
    }

    void ShowNewMissionInfo(int id) {
        missionName.text = "Mission: " + missions[id].Name[id];
        missionDifficulty.text = "Difficulty: " + missions[id].Diffculty + "/5";
        missionObjective.text = "Objective: " + missions[id].Objective.name;
        missionMapSize.text = "Size: " + GetStringFromValue(missions[id].Size, sizeTitles, MapGenerator.minMapSizeX*MapGenerator.minMapSizeY,MapGenerator.maxMapSizeX * MapGenerator.maxMapSizeY);
        missionBuildingDensity.text = "Density: " +  GetStringFromValue(missions[id].BuildingDensity, obstacleDensityTitles, minObstaclePercentage/100f, maxObstaclePercentage/100f);
    }

    void SelectMission(int id) {
        //
        selectedMapId = id;
        continueButton.SetActive(true);
        //continue button show
    }
    string GetStringFromValue(float value, string[] list, float minValue, float maxValue) {
        return list[(int)Mathf.Round(((value - minValue) / (maxValue - minValue)) * (list.Length - 1))];
    }

    void HideInfo() {
        if (selectedMapId != -1) {
            ShowNewMissionInfo(selectedMapId);
        }
        else {
            missionName.text = "Select Mission";
            missionDifficulty.text = "Difficulty:";
            missionObjective.text = "Objective:";
            missionMapSize.text = "Size:";
            missionBuildingDensity.text = "Density:";
        }
    }

    public void ContinueButtonClicked() {
        Map map = missions[selectedMapId];
        MapParameters.SetMapParameters(map.SizeX, map.SizeY, maxObstaclePercentage, map.Seed);
        IdMissionButton.ButtonClicked -= SelectMission;
        IdMissionButton.ButtonPointerEntered -= ShowNewMissionInfo;
        IdMissionButton.ButtonPointerLeft -= HideInfo;
        SceneManager.LoadScene("Scenes/Hub");
        SceneManager.UnloadSceneAsync("Scenes/MissionSelector");
    }

    public void ExitButtonClicked() {
        IdMissionButton.ButtonClicked -= SelectMission;
        IdMissionButton.ButtonPointerEntered -= ShowNewMissionInfo;
        IdMissionButton.ButtonPointerLeft -= HideInfo;
        SceneManager.LoadScene("Scenes/MainMenu");
        SceneManager.UnloadSceneAsync("Scenes/MissionSelector");
    }
}
