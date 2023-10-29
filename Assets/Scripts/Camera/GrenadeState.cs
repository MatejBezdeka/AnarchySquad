using UnityEngine;

namespace Camera {
    public class GrenadeState : PlayerState {
        private MeshRenderer indicator;
        private Vector3 startPosition;
        private Vector3 destination;
        private float maxRange;
        
        public GrenadeState(PlayerControl player, MeshRenderer prefab) : base(player) {
            startPosition = player.selectedUnit.transform.position;
            maxRange = player.selectedUnit.stats.MaxEffectiveRange;
            indicator = prefab;
            player.UpdateCursor(PlayerControl.cursorTypes.attack);
        }
        public override void Enter() {
            indicator.enabled = true;
            currentState = state.grenade;
            base.Enter();
        }

        public override void Update() {
            if(CursorOverUI()) {indicator.enabled = false;  return; }
            destination = player.RayHit().point;
            indicator.enabled = true;
            indicator.transform.position = new Vector3(destination.x, destination.y + 0.1f, destination.z);
        }

        protected override void LeftClick() {
            if(CursorOverUI()) { return; }
            player.nextState = new NormalState(player);
            player.selectedUnit.ThrowGrenade(destination);
            indicator.enabled = false; 
            Exit(new NormalState(player));
        }

        protected override void RightClick() {
            indicator.enabled = false; 
            Exit(new NormalState(player));
        }

        protected override void Esc() {
            player.nextState = new NormalState(player);
            indicator.enabled = false; 
            Exit(new NormalState(player));
        }
    }
}