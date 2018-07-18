using UnityEngine;
using UnityEngine.AI;

namespace Complete.Tank
{
    /// <summary>
    ///  TankAI movement types
    /// </summary>
    public class TankAIMovement : TankMovement
    {
        private NavMeshAgent m_NavMeshAgent;   // Reference to Navmesh agent
        private Vector3 m_DesiredPosition;     // Desired position to move

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

        /// <summary>
        /// Patrol around target within range
        /// </summary>
        /// <param name="minRange"> min range to patrol</param>
        /// <param name="maxRange"> max range to patrol </param>
        /// <param name="target"> target to patrol around </param>
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

        /// <summary>
        /// Chase target
        /// </summary>
        /// <param name="target"> Target to chase </param>
        public void Chase(Transform target)
        {
            m_NavMeshAgent.SetDestination(target.position);
        }

        /// <summary>
        /// Run away from target with range
        /// </summary>
        /// <param name="target"> target to run away from </param>
        /// <param name="range"> range to run away  </param>
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


        /// <summary>
        /// Stops the Navmesh agent
        /// </summary>
        /// <param name="stop"> flag to stop the Navmesh agent</param>
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