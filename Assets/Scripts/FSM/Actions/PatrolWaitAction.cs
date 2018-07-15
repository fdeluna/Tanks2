using Complete.Tank;
using UnityEngine;

namespace Complete.FSM.Actions
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/PatrolWaitAction")]
    public class PatrolWaitAction : StateAction
    {
        public float m_MinRange = 10;
        public float m_MaxRange = 25;
        public float m_MinTimeWait = 2;
        public float m_MaxTimeWait = 4;

        private TankAIMovement m_TankAIMovement;
        private bool m_Waiting = false;
        private float m_WaitTime = 0f;

        public override void Init(StateController controller)
        {
            m_TankAIMovement = controller.gameObject.GetComponent<TankAIMovement>();
            m_WaitTime = Random.Range(m_MinTimeWait, m_MaxTimeWait);
        }

        public override void Act(StateController controller)
        {
            if (!m_Waiting)
            {
                if (controller.CheckIfCountDownElapsed(m_WaitTime))
                {                    
                    m_TankAIMovement.Stop(true);
                    m_Waiting = true;
                    controller.StartCoroutine(WaitSecondsAction(Random.Range(1, 2), () =>
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

        public override void EndAction(StateController controller)
        {
            m_TankAIMovement.Stop(true);
        }
    }
}