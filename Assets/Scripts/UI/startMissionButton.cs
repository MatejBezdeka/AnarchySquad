using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI {
    public class StartMissionButton : MonoBehaviour
    {
        private void Start() {
            GetComponent<Button>().onClick.AddListener(StartMission);
        }

        void StartMission() {
            SceneManager.LoadSceneAsync("Battlefield");
        }
    }
}
