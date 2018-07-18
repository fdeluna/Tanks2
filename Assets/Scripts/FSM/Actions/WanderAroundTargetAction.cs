using UnityEngine;

namespace Complete.FSM.Actions
{
    /// <summary>
    ///  State action to wander around target
    /// </summary>
    [CreateAssetMenu(menuName = "PluggableAI/Actions/WanderAroundTargetAction")]
    public class WanderAroundTargetAction : MovementAction
    {
        public float m_MinRange = 5;    // Min range to wander around
        public float m_MaxRange = 15;   // Max range to wander around          
                                   
        public override void Act()
        {
            m_TankAIMovement.Patrol(m_MinRange, m_MaxRange, m_StateController.m_TargetTransform);            
        }
    }
}