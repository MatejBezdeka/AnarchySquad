using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueButton : UIButton {
    int missionsDone;
    protected override void Start() {
        base.Start();
        missionsDone = PlayerPrefs.GetInt("MS");
        missionsDone++;
        PlayerPrefs.SetInt("MS", missionsDone);
    }

    protected override void Functionality() {
        AudioSettings.Music.StopMusic();
        if (missionsDone == 7) {
            Save.DeleteData();
            SceneManager.LoadScene("MainMenu");
                return;
        }
        SceneManager.LoadScene("MissionSelector", LoadSceneMode.Single);
        
    }

}
