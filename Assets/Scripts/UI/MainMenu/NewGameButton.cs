using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
[RequireComponent(typeof(Button))]
public class NewGameButton : MonoBehaviour, IButton {
    public Settings.ButtonSounds sound;
    public Settings.ButtonSounds Sound { get { return sound; } }
    void Start()
    {
        transform.GetComponent<Button>().onClick.AddListener(Clicked);
    }

    void Clicked() {
        IButton.PlayButtonSound.Invoke(Sound);
        System.Random rn = new System.Random();
        Save.NewSave(0,rn.Next(int.MinValue + 64, int.MaxValue - 64));
        SceneManager.UnloadSceneAsync("Scenes/MainMenu");
        SceneManager.LoadScene("Scenes/MissionSelector");
    }
}
