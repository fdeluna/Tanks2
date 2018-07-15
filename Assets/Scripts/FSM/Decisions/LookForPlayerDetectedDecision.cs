using UnityEngine;

namespace Complete.FSM.Decisions
{
    [CreateAssetMenu(menuName = "PluggableAI/Decisions/LookForPlayerDetected")]
    public class LookForPlayerDetectedDecision : PlayerDetectedDecision
    {
        public float m_Time = 10;


        public override bool Decide(StateController controller)
        {
            bool playerLost = false;
            if (!PlayerDetected(controller))
            {
                playerLost = controller.CheckIfCountDownElapsed(m_Time);
            }
            else
            {
                controller.ResetStateTimeElapsed();
            }

            return playerLost;
        }
    }
}
