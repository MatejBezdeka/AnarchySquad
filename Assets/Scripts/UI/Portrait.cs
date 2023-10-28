using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace World {
    [RequireComponent(typeof(Button), typeof(Outline))]
    public class Portrait : MonoBehaviour{
        public static event Action<SquadUnit> selectedDeselectedUnit;
        //Button button;
        [SerializeField] Button button;
        [SerializeField] Slider hpSlider;
        [SerializeField] Image picture;
        [SerializeField] TextMeshProUGUI nameLabel;
        [SerializeField] Outline outline;
        SquadUnit unit;
        bool clicked = false;

        void Start() {
            button.onClick.AddListener(Clicked);
        }

        public void AssignUnit(SquadUnit unit){
            this.unit = unit;
        }

        void Clicked() {
            selectedDeselectedUnit?.Invoke(unit);
            clicked = !clicked;
            outline.enabled = clicked;
        }
        public void UpdateHpSlider(int value) {
            hpSlider.value = value;
        }
    }
}