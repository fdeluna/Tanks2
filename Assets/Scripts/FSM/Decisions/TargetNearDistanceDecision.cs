using UnityEngine;

namespace Complete.FSM.Decisions
{
    [CreateAssetMenu(menuName = "PluggableAI/Decisions/TargetNearDistance")]
    public class TargetNearDistanceDecision : Decision
    {
        public float m_DistanceToTarget = 10;
        public float m_TimeToCheck = 4;
        private Transform m_Target;

        public override bool Decide(StateController controller)
        {
            bool targetNear = false;
            if (controller.CheckIfCountDownElapsed(m_TimeToCheck))
            {                
                targetNear = Vector3.Distance(controller.transform.position, controller.m_TargetTransform.position) <= m_DistanceToTarget;
            }
            return targetNear;
        }
    }
}
