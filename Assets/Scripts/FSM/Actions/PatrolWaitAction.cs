using Complete.Tank;
using UnityEngine;

namespace Complete.FSM.Actions
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/PatrolWaitAction")]
    public class PatrolWaitAction : MovementAction
    {
        public float m_MinRange = 10;
        public float m_MaxRange = 25;
        public float m_MinTimeWait = 2;
        public float m_MaxTimeWait = 4;

        private bool m_Waiting = false;
        private float m_WaitTime = 0f;

        private float m_WaitSeconds = 3;

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