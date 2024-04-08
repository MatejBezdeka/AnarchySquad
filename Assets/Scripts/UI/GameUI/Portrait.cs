using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace World {
    [RequireComponent(typeof(Button), typeof(Outline))]
    public class Portrait : MonoBehaviour, IButton{
        public static event Action<SquadUnit> selectedDeselectedUnit;
        //Button button;
        public AudioSettings.ButtonSounds Sound { get { return AudioSettings.ButtonSounds.normal; } }
        [SerializeField] Button button;
        [SerializeField] Slider hpSlider;
        [SerializeField] Image picture;
        [SerializeField] TextMeshProUGUI nameLabel;
        [SerializeField] Outline outline;
        SquadUnit unit;
        bool clicked = false;

        void Start() {
            button.onClick.AddListener(Clicked);
            PlayerControl.selectedNewUnit += UpdateOutline;
            unit.updateUI += UpdateHpSlider;
        }

        public void AssignUnit(SquadUnit unit){
            this.unit = unit;
            picture.sprite = unit.stats.Icon;
            nameLabel.text = unit.UnitName;
            hpSlider.maxValue = unit.stats.MaxHp;
            UpdateHpSlider();
        }

        //Unit ignnore -> connected with profile with one event
        void UpdateOutline(Unit ignore) {
            outline.enabled = unit.selected;
        }
        void Clicked() {
            IButton.PlayButtonSound.Invoke(Sound);
            selectedDeselectedUnit?.Invoke(unit);
            UpdateOutline(null);
        }
        void UpdateHpSlider() {
            hpSlider.value = unit.CurrentHp;
        }
    }
}