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
            PlayerControl.selectedNewUnit += Updated;
        }

        public void AssignUnit(SquadUnit unit){
            this.unit = unit;
            picture.sprite = unit.stats.Icon;
            nameLabel.text = unit.stats.UnitName;
            hpSlider.maxValue = unit.stats.MaxHp;
            UpdateHpSlider();
        }

        void Updated(Unit ignore) {
            if (unit.selected) {
                outline.enabled = true;
            }
            else {
                outline.enabled = false;
            }
        }
        void Clicked() {
            selectedDeselectedUnit?.Invoke(unit);
            Updated(null);
        }
        void UpdateHpSlider() {
            hpSlider.value = unit.stats.Hp;
        }
    }
}