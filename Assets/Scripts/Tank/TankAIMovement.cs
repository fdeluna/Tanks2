using UnityEngine;
using UnityEngine.AI;
using Utils;

namespace Complete
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

        private void FixedUpdate()
        {
            // Adjust the rigidbodies position and orientation in FixedUpdate.
            Patrol();
        }

        private void Patrol()
        {
            m_Moving = m_NavMeshAgent.velocity.magnitude > 0 ? true : false;
            // Apply this movement to the rigidbody's position.                        
            if (m_NavMeshAgent.remainingDistance <= m_NavMeshAgent.stoppingDistance + 2 && !m_NavMeshAgent.pathPending)                
            {                
                m_DesiredPosition = m_NavMeshAgent.RandomPoint(20,40);
                
                // Clamp movement to viewport
                Vector3 viewPosition = Camera.main.WorldToViewportPoint(m_DesiredPosition);
                viewPosition.x = Mathf.Clamp(viewPosition.x, 0.2f, 0.8f);
                viewPosition.y = Mathf.Clamp(viewPosition.y, 0.2f, 0.8f);
                m_DesiredPosition = Camera.main.ViewportToWorldPoint(viewPosition);
                m_DesiredPosition.y = 0;                
                
                m_NavMeshAgent.SetDestination(m_DesiredPosition);
            }                                    
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(m_DesiredPosition, 2);
        }
    }
}