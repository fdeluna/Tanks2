using Complete.Utils;
using UnityEngine;
using UnityEngine.AI;

namespace Complete.Tank
{
    public class TankAIMovement : TankMovement
    {
        private NavMeshAgent m_NavMeshAgent;
        private Vector3 m_DesiredPosition;

        protected override void Awake()
        {
            base.Awake();
            m_NavMeshAgent = GetComponent<NavMeshAgent>();
        }

        protected override void Update()
        {
            m_Moving = m_NavMeshAgent.velocity.magnitude > 0 ? true : false;
        }

        public void Patrol(float minRange = 0, float maxRange = 0)
        {
            if (m_NavMeshAgent.remainingDistance <= m_NavMeshAgent.stoppingDistance + 2 && !m_NavMeshAgent.pathPending)
            {
                m_DesiredPosition = m_NavMeshAgent.RandomPointAroundTarget(minRange, maxRange);

                // Clamp movement to viewport
                Vector3 viewPosition = Camera.main.WorldToViewportPoint(m_DesiredPosition);
                viewPosition.x = Mathf.Clamp(viewPosition.x, 0.2f, 0.8f);
                viewPosition.y = Mathf.Clamp(viewPosition.y, 0.2f, 0.8f);
                m_DesiredPosition = Camera.main.ViewportToWorldPoint(viewPosition);
                m_DesiredPosition.y = 0;

                m_NavMeshAgent.SetDestination(m_DesiredPosition);
            }
        }

        public void Chase(Transform target)
        {                       
            m_NavMeshAgent.SetDestination(target.position);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(m_DesiredPosition, 2);
        }
    }
}