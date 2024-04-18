using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitBattlefield : UIButton
{
    protected override void Functionality() {
        AudioSettings.Music.StopMusic();
        SceneManager.LoadScene("Scenes/MainMenu", LoadSceneMode.Single);
        SceneManager.UnloadSceneAsync("Scenes/Battlefield");
    }
}