using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Button))]
public class DeleteSaveButton : MonoBehaviour, IButton
{
    public AudioSettings.ButtonSounds sound;
    public AudioSettings.ButtonSounds Sound { get { return sound; } }
    [SerializeField] private GameObject newGameButton;
    [SerializeField] private GameObject loadSaveButton;
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(Clicked);
    }

    void Clicked()
    {
        IButton.PlayButtonSound.Invoke(Sound);
        Save.DeleteData();
        newGameButton.SetActive(true);
        loadSaveButton.SetActive(false);
    }
}
