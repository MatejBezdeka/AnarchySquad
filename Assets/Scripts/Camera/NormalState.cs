using UnityEngine;

namespace Camera {
    public class NormalState : PlayerState {
        private RaycastHit hit;
        public override void Enter() {
            currentState = state.normal;
            base.Enter();
        }

        public override void Update() {
            if(CursorOverUI()) { return; }
            hit = player.RayHit();
            if (player.hitSomething) {
                ChangeCursor();
            }
        }

        public NormalState(PlayerControl player) : base(player) {
        }

        void ChangeCursor() {
            switch (hit.transform.tag) {
                case "Floor":
                    if (player.selectedUnits.Count > 0) {
                        player.UpdateCursor(PlayerControl.cursorTypes.goTo);
                    }
                    else {
                        player.UpdateCursor(PlayerControl.cursorTypes.normal);
                    }
                    break;
                case "Squader":
                    player.UpdateCursor(PlayerControl.cursorTypes.interact);
                    break;
                case "Anarchist":
                    if (player.selectedUnits.Count > 0) {
                        player.UpdateCursor(PlayerControl.cursorTypes.attack);
                    }
                    else {
                        player.UpdateCursor(PlayerControl.cursorTypes.interact);
                    }
                    break;
                case "Building":
                    player.UpdateCursor(PlayerControl.cursorTypes.normal);
                    break;
                case "Obstacle":
                    player.UpdateCursor(PlayerControl.cursorTypes.interact);
                    break;
                default:
                    player.UpdateCursor(PlayerControl.cursorTypes.normal);
                    break;
            }
        }

        protected override void LeftClick() {
            if(CursorOverUI()) { return; }
            if (hit.transform == null) {
                player.DeselectAll();
                return;
            }
            switch (hit.transform.tag) {
                case "Floor":
                    player.DeselectAll();
                    break;
                case "Squader":
                    var unit = hit.collider.GetComponent<Unit>();
                    player.SelectDeselectUnit(unit);
                    break;
                case "Anarchist":
                    break;
                case "Obstacle":
                    break;
            }
        }

        protected override void RightClick() {
            if(CursorOverUI()) { return; }
            if (hit.transform == null) {
                player.DeselectAll();
                return;
            }
            switch (hit.transform.tag) {
                case "Floor":
                    if (player.selectedUnits.Count > 0) {
                        foreach (var unit in player.selectedUnits) {
                            unit.SetDestination(hit.point);
                        }
                        player.MakePointWhereUnitIsMoving(hit.point);
                    }
                    break;
                case "Squader":
                    break;
                case "Anarchist":
                    break;
                case "Obstacle":
                    break;
            }
        }

        protected override void Esc() {
            Exit(new PauseState(player));
        }
    }
}