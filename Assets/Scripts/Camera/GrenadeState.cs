using UnityEngine;

namespace Camera {
    public class GrenadeState : PlayerState {
        private GameObject indicator;
        private Vector3 startPosition;
        private Vector3 destination;
        private float maxRange;
        
        public GrenadeState(PlayerControl player, GameObject prefab) : base(player) {
            startPosition = player.selectedUnit.transform.position;
            maxRange = player.selectedUnit.stats.MaxEffectiveRange;
            indicator = prefab;
            player.UpdateCursor(PlayerControl.cursorTypes.attack);
        }
        public override void Enter() {
            indicator.SetActive(true);
            currentState = state.grenade;
            base.Enter();
        }

        public override void Update() {
            if(CursorOverUI()) {indicator.SetActive(false);  return; }
            destination = player.RayHit().point;
            indicator.SetActive(true);
            indicator.transform.position = destination;
        }

        protected override void LeftClick() {
            if(CursorOverUI()) { return; }
            player.nextState = new NormalState(player);
            player.selectedUnit.ThrowGrenade(destination);
            Exit(new NormalState(player));
        }

        protected override void RightClick() {
            if(CursorOverUI()) { return; }
            Exit(new NormalState(player));
        }

        protected override void Esc() {
            player.nextState = new NormalState(player);
            Exit(new NormalState(player));
        }
    }
}