using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI {
    public class StartMissionButton : MonoBehaviour, IButton {
        public static event Action startingGame;
        public Settings.ButtonSounds sound;
        public Settings.ButtonSounds Sound { get { return sound; } }
        
        private void Start() {
            
            GetComponent<Button>().onClick.AddListener(Clicked);
        }

        void Clicked() {
            IButton.PlayButtonSound.Invoke(Sound);
            startingGame?.Invoke();
        }
    }
}
