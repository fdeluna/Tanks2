using Complete.Tank;

namespace Complete.FSM.Actions
{
    /// <summary>
    ///  Base class for movements actions
    /// </summary>
    public abstract class MovementAction : StateAction
    {
        protected TankAIMovement m_TankAIMovement;

        public override void Init(StateController controller)
        {
            base.Init(controller);
            m_TankAIMovement = m_StateController.gameObject.GetComponent<TankAIMovement>();
        }
    }
}
