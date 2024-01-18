using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI {
    public class StartMissionButton : MonoBehaviour {
        public static event Action startingGame;
        private void Start() {
            
            GetComponent<Button>().onClick.AddListener(Clicked);
        }

        void Clicked() {
            startingGame?.Invoke();
        }
    }
}
