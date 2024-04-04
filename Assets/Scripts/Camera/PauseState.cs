using System;

namespace Camera {
    public class PauseState : PlayerState {
        public static event Action resumeGame; 
        public PauseState(PlayerControl player) : base(player) { }

        protected override void Esc() {
            resumeGame?.Invoke();
            Exit(new NormalState(player));
        }
    }
}