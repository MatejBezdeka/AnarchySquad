using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueButton : UIButton
{
    protected override void Start() {
        base.Start();
        int missionsDone = PlayerPrefs.GetInt("MS");
        if (missionsDone == 6) {
            //ult win
        }
        PlayerPrefs.SetInt("MS", missionsDone++);
    }

    protected override void Functionality() {
        SceneManager.LoadScene("MissionSelector");
        
    }

}
