using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionInfoPanel : MonoBehaviour {
    System.Random rn = new System.Random();
    [Header("=== Mission panel ===")]
    [SerializeField] TextMeshProUGUI missionName;
    [SerializeField] TextMeshProUGUI missionDifficulty;
    [SerializeField] TextMeshProUGUI missionObjective;
    [SerializeField] TextMeshProUGUI missionMapSize;
    [SerializeField] TextMeshProUGUI missionBuildingDensity;
    [Header("=== Current save info panel ===")]
    [SerializeField] TextMeshProUGUI points;
    [SerializeField] TextMeshProUGUI missionsDone;
    [Header("=== Map ===")] 
    [SerializeField] Transform map;
    [SerializeField] GameObject missionPrefab;
    List<GameObject> missionPoints = new List<GameObject>();

    void Start() {
        Rect mapRect = map.GetComponent<RectTransform>().rect;
        Rect missionRect = missionPrefab.GetComponent<RectTransform>().rect;
        bool overlapping;
        int posx;
        int posy;
        for (int i = 0; i < 25; i++) {
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
            
            
            GameObject newPoint = Instantiate(missionPrefab, map);
            missionPoints.Add(newPoint);
            newPoint.transform.localPosition = new Vector3(posx, posy, 0);
            //Debug.Log(mapRect.width + " " + mapRect.height);
        }  
    }

}
