namespace Camera {
    public class PlayerState {
        public enum stateStages {
            entry, update, exit
        }

        public enum state {
            normal, grenade, pause
        }

        public state currentState = state.normal;
        public stateStages currentStage = stateStages.entry;
        protected PlayerControl player;
        public PlayerState(PlayerControl player) {
            this.player = player;
        }
        public virtual void Enter() {
            currentStage = stateStages.update;
            player.leftMouseButtonClicked += LeftClick;
            player.rightMouseButtonClicked += RightClick;
            player.escButtonClicked += Esc;
        }
        public virtual void Update() { }
        protected virtual void LeftClick(){}
        protected virtual void RightClick(){}
        protected virtual void Esc(){}

        public void Exit(PlayerState state) {
            player.nextState = state;
            player.leftMouseButtonClicked -= LeftClick;
            player.rightMouseButtonClicked -= RightClick;
            player.escButtonClicked -= Esc;
            currentStage = stateStages.exit;
        }

        protected bool CursorOverUI() {
            return UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
        }
    }
}