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
            m_NavMeshAgent.nextPosition = m_NavMeshAgent.nextPosition.ClampVector3ToViewPort(Camera.main, m_MinMargin, m_MaxMargin);
        }

        public void Patrol(float minRange = 0, float maxRange = 0, Transform target = null)
        {
            Vector3 targetPosition = target == null ? m_NavMeshAgent.transform.position : target.position;

            if (m_NavMeshAgent.remainingDistance <= m_NavMeshAgent.stoppingDistance || !Utils.IsPointInViewPort(Camera.main, m_DesiredPosition))
            {
                m_DesiredPosition = m_NavMeshAgent.RandomPointAroundTarget(minRange, maxRange, targetPosition).ClampVector3ToViewPort(Camera.main, m_MinMargin, m_MaxMargin);
                m_DesiredPosition.y = 0;
                m_NavMeshAgent.SetDestination(m_DesiredPosition);                
            }            
        }
        public void Chase(Transform target)
        {
            m_NavMeshAgent.SetDestination(target.position);
        }

        public void Flee(Transform target, float range)
        {
            if (m_NavMeshAgent.remainingDistance <= m_NavMeshAgent.stoppingDistance)
            {
                Vector3 direction = transform.position + (target.position - transform.position) * range;
                m_DesiredPosition = m_NavMeshAgent.RandomPointAroundTarget(range, range, direction).ClampVector3ToViewPort(Camera.main, m_MinMargin, m_MaxMargin);
                m_DesiredPosition.y = 0;
                m_NavMeshAgent.SetDestination(m_DesiredPosition);
            }
        }

        public void LookAtTarget(Transform target)
        {
            if (target != null)
            {
                transform.LookAt(target);
            }
        }
           

        public void Stop(bool stop)
        {
            m_NavMeshAgent.isStopped = stop;
            m_NavMeshAgent.ResetPath();
        }

      

        private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(m_DesiredPosition, 2);
        }
    }
}