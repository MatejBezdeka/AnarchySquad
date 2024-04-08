using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BackHubButton : MonoBehaviour, IButton
{
    public AudioSettings.ButtonSounds sound;
    public AudioSettings.ButtonSounds Sound { get { return sound; } }
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(Clicked);
    }

    void Clicked() {
        IButton.PlayButtonSound.Invoke(Sound);
        SceneManager.LoadScene("Scenes/MissionSelector");
        SceneManager.UnloadSceneAsync("Hub");
    }
}
