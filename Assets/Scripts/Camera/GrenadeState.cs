using System.Collections.Generic;
using UnityEngine;

namespace Camera {
    public class GrenadeState : PlayerState {
        LineRenderer line;
        Unit unit;
        //Vector3 startPosition;
        Vector3 destination;
        float maxRange;
        Vector3[] points = new Vector3[25];
        public GrenadeState(PlayerControl player, LineRenderer prefab) : base(player) {
            unit = player.selectedUnit;
            //startPosition = player.selectedUnit.transform.position;
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
            float launchAngle = angle(target);
            float radianAngle = Mathf.Deg2Rad * launchAngle;
            float initialVelocity = 10;
            for (int i = 0; i < points.Length; i++)
            {
                float time = i * 0.1f;
                float x = unit.transform.position.x + initialVelocity * Mathf.Cos(radianAngle) * time;
                float y = unit.transform.position.y + initialVelocity * Mathf.Sin(radianAngle) * time - 0.5f * Physics.gravity.magnitude * time * time;
                float z = unit.transform.position.z + initialVelocity * Mathf.Cos(radianAngle) * time;

                points[i] = new Vector3(x, y, z);
            }
        }
        float angle(Vector3 target) {
            float initialVelocity = 10;
            float horizontalRange = Vector2.Distance(new Vector2(unit.transform.position.x, unit.transform.transform.position.z), new Vector2(target.x, target.z));
            float launchAngle = Mathf.Asin((horizontalRange * Physics.gravity.magnitude) / (initialVelocity * initialVelocity));
            launchAngle = Mathf.Rad2Deg * launchAngle;
            return launchAngle;
        }

    }
}