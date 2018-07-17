using UnityEngine;

namespace Complete.FSM.Decisions
{
    [CreateAssetMenu(menuName = "PluggableAI/Decisions/TargetNearDistance")]
    public class TargetNearDistanceDecision : Decision
    {
        public float m_DistanceToTarget = 10;
        private Transform m_Target;

        public override bool Decide(StateController controller)
        {
            return Vector3.Distance(controller.transform.position, controller.m_TargetTransform.position) <= m_DistanceToTarget;
        }
    }
}
