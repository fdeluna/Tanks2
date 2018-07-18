using UnityEngine;

namespace Complete.FSM.Actions
{
    /// <summary>
    /// State action to patrol and wait
    /// </summary>
    [CreateAssetMenu(menuName = "PluggableAI/Actions/PatrolWaitAction")]
    public class PatrolWaitAction : MovementAction
    {
        public float m_MinRange = 10;       // Min Range ro patrol
        public float m_MaxRange = 25;       // Max Range ro patrol
        public float m_MinTimeWait = 2;     // Min time to make a stop
        public float m_MaxTimeWait = 4;     // Max time to make a stop

        private bool m_Waiting = false;     // Waiting flag
        private float m_WaitTime = 0f;      // Get random value between range

        private float m_WaitSeconds = 3;    // Waiting time

        public override void Init(StateController controller)
        {
            base.Init(controller);
            m_WaitTime = Random.Range(m_MinTimeWait, m_MaxTimeWait);
        }

        public override void Act()
        {
            if (!m_Waiting)
            {
                if (m_StateController.CheckIfCountDownElapsed(m_WaitTime))
                {
                    m_TankAIMovement.Stop(true);
                    m_Waiting = true;
                    m_StateController.StartCoroutine(WaitSecondsAction(Random.Range(m_WaitSeconds / 2, m_WaitSeconds), () =>
                      {
                          m_Waiting = false;
                          m_TankAIMovement.Stop(false);
                          m_WaitTime = Random.Range(m_MinTimeWait, m_MaxTimeWait);
                      }));
                }
                else
                {
                    m_TankAIMovement.Patrol(m_MinRange, m_MaxRange);
                }

            }
        }
    }
}