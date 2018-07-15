using UnityEngine;

namespace Complete.FSM.Decisions
{
    [CreateAssetMenu(menuName = "PluggableAI/Decisions/PlayerVisible")]
    public class PlayerDetectedDecision : Decision
    {
        public float m_Range = 10;
        public float m_ViewAngle = 45;
        public LayerMask m_TargetMask;
        public LayerMask m_ObstacleMask;

        protected bool m_PlayerDetected = false;

        public override bool Decide(StateController controller)
        {
            return PlayerDetected(controller);
        }

        protected bool PlayerDetected(StateController controller)
        {
            m_PlayerDetected = false;
            Collider[] targetsInViewRadius = Physics.OverlapSphere(controller.eyes.position, m_Range, m_TargetMask);

            for (int i = 0; i < targetsInViewRadius.Length; i++)
            {
                Transform target = targetsInViewRadius[i].transform.parent;
                if (controller.transform != target)
                {
                    Vector3 dirToTarget = (target.position - controller.eyes.position).normalized;
                    dirToTarget.y = 0;
                    if (Vector3.Angle(controller.eyes.forward, dirToTarget) < m_ViewAngle / 2)
                    {
                        float dstToTarget = Vector3.Distance(controller.eyes.position, target.position);                        
                        m_PlayerDetected = !Physics.Raycast(controller.eyes.position, dirToTarget, dstToTarget, m_ObstacleMask);
                        Debug.DrawRay(controller.eyes.position, dirToTarget * dstToTarget, Color.red);
                        if (m_PlayerDetected)
                        {
                            Debug.DrawRay(controller.eyes.position, dirToTarget * dstToTarget, Color.green);
                            controller.m_TargetTransform = target;
                            break;
                        }
                    }
                }
            }

            return m_PlayerDetected;
        }
    }
}
