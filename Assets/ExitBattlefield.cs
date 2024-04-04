using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitBattlefield : UIButton
{
    protected override void Functionality() {
        SceneManager.LoadScene("Scenes/MainMenu");
        SceneManager.UnloadSceneAsync("Scenes/Battlefield");
    }

}
