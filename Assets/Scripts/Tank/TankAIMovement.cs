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
            m_NavMeshAgent.speed = m_Speed;
            m_NavMeshAgent.angularSpeed = m_TurnSpeed;
        }

        protected override void Update()
        {
            m_Moving = m_NavMeshAgent.velocity.magnitude > 0 ? true : false;
        }

        public void Patrol(float minRange = 0, float maxRange = 0, Transform target = null)
        {
            if (m_NavMeshAgent.remainingDistance <= m_NavMeshAgent.stoppingDistance)
            {
                m_DesiredPosition = m_NavMeshAgent.RandomPointAroundTarget(minRange, maxRange, target);

                m_NavMeshAgent.SetDestination(m_DesiredPosition.ClampVector3ToViewPort(Camera.main, m_MinMargin, m_MaxMargin));
            }
        }

        public void Stop(bool stop)
        {
            m_NavMeshAgent.isStopped = stop;
            m_NavMeshAgent.ResetPath();
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