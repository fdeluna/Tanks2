using Complete.Tank;
using UnityEngine;

namespace Complete.FSM.Actions
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/WanderAroundTargetAction")]
    public class WanderAroundTargetAction : MovementAction
    {
        public float m_MinRange = 5;
        public float m_MaxRange = 15;             
                                   
        public override void Act()
        {
            m_TankAIMovement.Patrol(m_MinRange, m_MaxRange, m_StateController.m_TargetTransform);            
        }
    }
}