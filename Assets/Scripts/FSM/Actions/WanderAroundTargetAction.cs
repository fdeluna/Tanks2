using Complete.Tank;
using UnityEngine;

namespace Complete.FSM.Actions
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/WanderAroundTargetAction")]
    public class WanderAroundTargetAction : StateAction
    {
        public float m_MinRange = 5;
        public float m_MaxRange = 15;             

        private TankAIMovement m_TankAIMovement;
        
        private bool m_Waiting = false;
        private float m_WaitTime = 0f;

        public override void Init(StateController controller)
        {            
            m_TankAIMovement = controller.gameObject.GetComponent<TankAIMovement>();
        }

        public override void Act(StateController controller)
        {
            m_TankAIMovement.Patrol(m_MinRange, m_MaxRange, controller.m_TargetTransform);            
        }

        public override void EndAction(StateController controller)
        {
            m_TankAIMovement.Stop(true);
        }
    }
}