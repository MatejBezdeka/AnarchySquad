using System.Collections.Generic;
using UnityEngine;

namespace Camera {
    public class GrenadeState : PlayerState {
        LineRenderer line;
        Unit unit;
        Vector3 destination;
        Vector3 direction;
        float horizontalDistance;
        float initialVelocityY;
        public GrenadeState(PlayerControl player, LineRenderer line) : base(player) {
            unit = player.selectedUnit;
            this.line = line;
            player.UpdateCursor(PlayerControl.cursorTypes.attack);
        }
        public override void Update() {
            if (CursorOverUI()) {
                line.enabled = false; return;
            }
            line.enabled = true;
            destination = player.RayHit().point;
            ShowTrajectory(player.selectedUnit.LaunchAngle,player.selectedUnit.MaxGrenadeDistance);
        }

        protected override void LeftClick() {
            if(CursorOverUI()) { return; }
            player.nextState = new NormalState(player);
            player.selectedUnit.ThrowGrenade(destination);
            line.enabled = false; 
            Exit(new NormalState(player));
            player.selectedUnit.PlayAudioClip(Unit.AudioClips.roger);
        }

        protected override void RightClick() {
            line.enabled = false; 
            Exit(new NormalState(player));
        }

        protected override void Esc() {
            player.nextState = new NormalState(player);
            line.enabled = false; 
            Exit(new NormalState(player));
        }
        
        void ShowTrajectory(float angle, float maxRange) {
            direction = destination - unit.transform.position;
            direction.y = 0;
            horizontalDistance = Mathf.Min(Mathf.Sqrt(direction.x * direction.x + direction.z * direction.z), maxRange);
            initialVelocityY = Mathf.Sqrt(Physics.gravity.magnitude * (direction.y + Mathf.Tan(Mathf.Deg2Rad * angle) * horizontalDistance));
            line.positionCount = Mathf.RoundToInt(horizontalDistance)*3;
            for (int i = 0; i < line.positionCount; i++) {
                float time = i / (float)(line.positionCount-1);
                float t = time * Time.fixedDeltaTime * line.positionCount;
                Vector3 position = unit.transform.position + direction.normalized * (horizontalDistance * time) + Vector3.up * (initialVelocityY * t -1 * Physics.gravity.magnitude * t * t);
                line.SetPosition(i,position);
            }
        }
    }
}