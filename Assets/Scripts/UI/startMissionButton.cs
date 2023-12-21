using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI {
    public class StartMissionButton : MonoBehaviour {
        public static event Action startingGame;
        private void Start() {
            GetComponent<Button>().onClick.AddListener(StartMission);
        }

        void StartMission() {
            startingGame?.Invoke();
            SceneManager.LoadSceneAsync("LoadingScene");
            SceneManager.LoadSceneAsync("Battlefield");
        }
    }
}
