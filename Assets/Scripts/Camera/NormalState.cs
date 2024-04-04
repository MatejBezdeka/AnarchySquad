using System.Collections.Generic;
using UnityEngine;

namespace Camera {
    public class NormalState : PlayerState {
        private RaycastHit hit;
        public NormalState(PlayerControl player) : base(player) { }

        public override void Enter() {
            base.Enter();
        }

        public override void Update() {
            hit = player.RayHit();
            //if (player.hitSomething) {
                ChangeCursor();
            //}
        }
        
        void ChangeCursor() {
            if (CursorOverUI() || !player.hitSomething) {
                player.UpdateCursor(PlayerControl.cursorTypes.normal);
                return;
            }
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
                    SquadUnit unit = hit.collider.GetComponent<SquadUnit>();
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
                        for (int i = 0; i < player.selectedUnits.Count; i++) {
                            if (player.selectedUnits[i].CurrentState.GetType() == typeof(AttackUnitState)) {
                                player.selectedUnits[i].SetNewState(new NormalUnitState(player.selectedUnits[i]));
                                
                            }
                            player.selectedUnits[0].PlayAudioClip(Unit.AudioClips.roger);
                            Vector3 position = hit.point.GetRotatedVector3(player.selectedUnits.Count, i);
                            player.selectedUnits[i].SetDestination(position);
                            player.MakePointWhereUnitIsMoving(position);
                        }
                    }
                    break;
                case "Squader":
                    break;
                case "Anarchist":
                    var anarchist = hit.transform.gameObject.GetComponent<Unit>();
                    foreach (SquadUnit unit in player.selectedUnits) {
                        unit.SetNewState(new AttackUnitState(unit, anarchist));
                    }
                    break;
                case "Obstacle":
                    break;
            }
        }

        
        protected override void Esc() {
            player.PausePanel.SetActive(true);
            GameManager.instance.ChangeTime(-3);
            Exit(new PauseState(player));
        }
    }
}