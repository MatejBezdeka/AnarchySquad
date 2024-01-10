using System;
using UnityEngine;
using UnityEngine.UI;
namespace UI.Shop {
    [RequireComponent(typeof(Button))]
    public abstract class IUnitButton : MonoBehaviour {
        public int Id => GetId();
        public static event Action<int> clickedUnitButton;
        protected virtual void Start() {
            GetComponent<Button>().onClick.AddListener(Clicked);
        }

        protected abstract int GetId();
        void Clicked() {
            clickedUnitButton!.Invoke(GetId());    
        }

        protected abstract UnitBlueprint ReturnUnit();
    }
}
