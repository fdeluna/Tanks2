using Complete.Tank;
using UnityEngine;

namespace Complete.FSM.Actions
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/ChaseAction")]
    public class ChaseAction : StateAction
    {
        private TankAIMovement m_TankAIMovement;
        private Transform target;

        public override void Init(StateController controller)
        {
            m_TankAIMovement = controller.gameObject.GetComponent<TankAIMovement>();
            target = controller.m_TargetTransform;
        }

        public override void Act()
        {           
            m_TankAIMovement.Chase(target);
        }

        public override void EndAction()
        {
            
        }
    }
}
