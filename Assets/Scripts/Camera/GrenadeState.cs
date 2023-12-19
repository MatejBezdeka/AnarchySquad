using System.Collections.Generic;
using UnityEngine;

namespace Camera {
    public class GrenadeState : PlayerState {
        LineRenderer line;
        Vector3 startPosition;
        Vector3 destination;
        float maxRange;
        Vector3[] points = new Vector3[5];
        public GrenadeState(PlayerControl player, LineRenderer prefab) : base(player) {
            startPosition = player.selectedUnit.transform.position;
            maxRange = player.selectedUnit.stats.MaxEffectiveRange;
            line = prefab;
            line.positionCount = points.Length;
            player.UpdateCursor(PlayerControl.cursorTypes.attack);
        }
        public override void Enter() {
            line.enabled = true;
            base.Enter();
        }

        public override void Update() {
            if(CursorOverUI()) {line.enabled = false; line.enabled = true; return; }
            destination = player.RayHit().point;
            ShowTrajectory(destination);
            //line.transform.position = new Vector3(destination.x, destination.y + 0.1f, destination.z);
        }

        protected override void LeftClick() {
            if(CursorOverUI()) { return; }
            player.nextState = new NormalState(player);
            player.selectedUnit.ThrowGrenade(destination);
            line.enabled = false; 
            Exit(new NormalState(player));
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
        
        void ShowTrajectory(Vector3 target) {
            points[^1] = target;
            points[0] = startPosition;
            int launchAngle = 45;
            
            Vector3 direction = target - startPosition;

            float horizontalDistance = Mathf.Sqrt(direction.x * direction.x + direction.z * direction.z);

            float initialVelocity = horizontalDistance / (Mathf.Cos(Mathf.Deg2Rad * launchAngle) * Mathf.Sqrt(2f * Mathf.Abs(Physics.gravity.y)));

            float Vx = initialVelocity * Mathf.Cos(Mathf.Deg2Rad * launchAngle);
            float Vy = initialVelocity * Mathf.Sin(Mathf.Deg2Rad * launchAngle);

            float flightTime = 2f * Vy / Mathf.Abs(Physics.gravity.y);

            for (int i = 0; i < points.Length; i++) {
                points[i] = new Vector3(Vx, Vy, direction.z / flightTime)/(i+1);
                Debug.Log(startPosition + " " + direction);
            }
            line.SetPositions(points);
        }
        
    }
}