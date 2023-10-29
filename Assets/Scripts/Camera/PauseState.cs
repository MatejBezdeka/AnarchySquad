namespace Camera {
    public class PauseState : PlayerState {
        public override void Enter() {
            base.Enter();
        }

        public PauseState(PlayerControl player) : base(player) { }

        protected override void Esc() {
            Exit(new NormalState(player));
        }
    }
}