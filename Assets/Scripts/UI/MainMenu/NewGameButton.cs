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
        GameManager.instance.currentSave = new Save(Random.Range(int.MinValue + 64, int.MaxValue - 64));
        IButton.PlayButtonSound.Invoke(Sound);
        SceneManager.LoadScene("Hub");
    }
}
