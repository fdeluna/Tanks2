using Complete.Tank;
using UnityEngine;

namespace Complete.FSM.Actions
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/FleeAction")]
    public class FleeAction : MovementAction
    {
        public float m_Range = 10;       

        public override void Act()
        {
            m_TankAIMovement.Flee(m_StateController.m_TargetTransform, m_Range);
        }
    }
}