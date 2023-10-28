using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace World {
    public class Portrait : MonoBehaviour{
        public static event Action<SquadUnit> selectedDeselectedUnit;
        //Button button;
        [SerializeField] Button button;
        [SerializeField] Slider hpSlider;
        [SerializeField] Image picture;
        [SerializeField] TextMeshProUGUI nameLabel;
        SquadUnit unit;

        void Start() {
            button.onClick.AddListener(Clicked);
        }

        public void AssignUnit(SquadUnit unit){
            this.unit = unit;
        }

        void Clicked() {
            selectedDeselectedUnit?.Invoke(unit);
        }
        public void UpdateHpSlider(int value) {
            hpSlider.value = value;
        }
    }
}