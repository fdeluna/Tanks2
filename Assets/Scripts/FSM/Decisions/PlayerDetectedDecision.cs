using UnityEngine;

namespace Complete.FSM.Decisions
{
    [CreateAssetMenu(menuName = "PluggableAI/Decisions/PlayerVisible")]
    public class PlayerDetectedDecision : Decision
    {
        public float m_Range = 10;
        public float m_ViewAngle = 45;
        public LayerMask targetMask;
        public LayerMask obstacleMask;
        bool playerDetected = false;

        public override bool Decide(StateController controller)
        {
            Collider[] targetsInViewRadius = Physics.OverlapSphere(controller.eyes.position, m_Range, targetMask);

            for (int i = 0; i < targetsInViewRadius.Length; i++)
            {
                Transform target = targetsInViewRadius[i].transform;
                Vector3 dirToTarget = (target.position - controller.eyes.position).normalized;
                dirToTarget.y = 0;
                if (Vector3.Angle(controller.eyes.forward, dirToTarget) < m_ViewAngle / 2)
                {
                    float dstToTarget = Vector3.Distance(controller.eyes.position, target.position);

                    playerDetected = !Physics.Raycast(controller.eyes.position, dirToTarget, dstToTarget, obstacleMask);
                    Debug.DrawRay(controller.eyes.position, dirToTarget * dstToTarget, Color.red);
                    if (playerDetected)
                    {
                        Debug.DrawRay(controller.eyes.position, dirToTarget * dstToTarget, Color.green);
                        controller.m_TargetTransform = controller.transform;
                    }
                }
            }

            return playerDetected;
        }
    }
}
