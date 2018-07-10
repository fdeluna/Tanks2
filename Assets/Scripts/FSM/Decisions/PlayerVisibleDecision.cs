using UnityEngine;

namespace Complete.FSM.Decisions
{
    [CreateAssetMenu(menuName = "PluggableAI/Decisions/PlayerVisible")]
    public class PlayerVisibleDecision : Decision
    {
        public float m_Range = 10;

        public override bool Decide(StateController controller)
        {
            bool targetVisible = Look(controller);
            return targetVisible;
        }

        private bool Look(StateController controller)
        {
            RaycastHit hit;

            Debug.DrawRay(controller.eyes.position, controller.eyes.forward.normalized * m_Range, Color.green);

            if (Physics.SphereCast(controller.eyes.position, 1, controller.eyes.forward, out hit, m_Range)
                && hit.collider.CompareTag("Player"))
            {
                controller.m_Player = hit.transform;                
                return true;
            }
            else
            {                
                return false;
            }
            
        }
    }
}
