using UnityEngine;

namespace Complete.FSM.Actions
{
    /// <summary>
    /// State action to chase target
    /// </summary>
    [CreateAssetMenu(menuName = "PluggableAI/Actions/ChaseAction")]
    public class ChaseAction : MovementAction
    {
        public override void Act()
        {           
            m_TankAIMovement.Chase(m_StateController.m_TargetTransform);
        }
    }
}