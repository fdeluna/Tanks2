using UnityEngine;

namespace Complete.FSM.Actions
{
    /// <summary>
    /// State action to flee from target
    /// </summary>
    [CreateAssetMenu(menuName = "PluggableAI/Actions/FleeAction")]
    public class FleeAction : MovementAction
    {
        public float m_Range = 10;      // Range to flee away 

        public override void Act()
        {
            m_TankAIMovement.Flee(m_StateController.m_TargetTransform, m_Range);
        }
    }
}