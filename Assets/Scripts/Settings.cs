using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
[RequireComponent(typeof(AudioSource))]
public class Settings : MonoBehaviour {
    public static Settings Music;
    public enum AudioGroups {
        Master, Music, Effects, Ambient
    }
    public enum AmbientMusic {
        MainMenu, Hub, BatleField
    }
    public enum ButtonSounds {
        normal, error, succesful
    }
    [Header("Audio Mixer")]
    [SerializeField] AudioMixer audioMixer;
    [Header("------------")]
    [SerializeField] AudioClip[] soundtrackClips;
    int currentSong = 0;
    [Header("Ambient Loops")]
    [SerializeField] AudioClip AmbientClipLoop;
    [SerializeField] AudioClip MainMenuClipLoop;
    [SerializeField] AudioClip HubMenuClipLoop;
    [Header("Button Sounds")]
    [SerializeField] AudioClip NormalButtonClip;
    [SerializeField] AudioClip ErrorButtonClip;
    [SerializeField] AudioClip SuccesfulButtonClip;
    [Header("Audio sources")]
    [SerializeField] AudioSource musicAudioSource;
    [SerializeField] AudioSource effectsAudioSource;
    [SerializeField] AudioSource ambientAudioSource;
    float delay = 0;
    void Start() {
        if (Music == null) {
            Music = this;
            IButton.PlayButtonSound += PlayButtonSound;
            DontDestroyOnLoad(transform.gameObject);
        }
        ChangeAmbientMusic(AmbientMusic.MainMenu);
    }

    public void ChangeVolume(AudioGroups group, float value) {
        if (value == 0) audioMixer.SetFloat(group.ToString(), 0);
        else audioMixer.SetFloat(group.ToString(), Mathf.Log10(value / 120f) * 20);
    }

    public void StartMusic() {
        Extensions.ShuffleArray(soundtrackClips);
        musicAudioSource.clip = soundtrackClips[0];
        delay = soundtrackClips[0].length;
        currentSong = 0;
        musicAudioSource.Play();
        StartCoroutine(MusicChecker());
    }

    IEnumerator MusicChecker() {
        while (true) {
            delay--;
            if (delay >= 0) {
                yield return new WaitForSeconds(1);
            }
            else {
                currentSong++;
                if (currentSong >= soundtrackClips.Length) {
                    Extensions.ShuffleArray(soundtrackClips);
                    currentSong = 0;
                }

                musicAudioSource.clip = soundtrackClips[currentSong];
                delay = soundtrackClips[currentSong].length;
                musicAudioSource.Play();
                yield return new WaitForSeconds(1);
            }
        }
    }
    
    public void ResumeMusic() {
        
    }

    public void PauseMusic() {
        
    }
    public void StopMusic() {
        
    }

    public void ChangeAmbientMusic(AmbientMusic group) {
        switch (group) {
            case AmbientMusic.MainMenu:
                ambientAudioSource.clip = MainMenuClipLoop;
                ambientAudioSource.Play();
                break;
            case AmbientMusic.Hub:
                ambientAudioSource.clip = HubMenuClipLoop;
                ambientAudioSource.Play();
                break;
            case AmbientMusic.BatleField:
                ambientAudioSource.clip = AmbientClipLoop;
                ambientAudioSource.Play();
                break;
        }
    }

    public void PlayEffectClip(AudioClip clip) {
        effectsAudioSource.PlayOneShot(clip);
    }

    void PlayButtonSound(ButtonSounds buttonGroup) {
        switch (buttonGroup) {
            case ButtonSounds.error:
                effectsAudioSource.PlayOneShot(ErrorButtonClip);
                break;
            case ButtonSounds.succesful:
                effectsAudioSource.PlayOneShot(SuccesfulButtonClip);
                break;
            case ButtonSounds.normal:
            default:
                effectsAudioSource.PlayOneShot(NormalButtonClip);
                break;
        }
    }
}
