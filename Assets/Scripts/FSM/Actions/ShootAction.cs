using Complete.Tank;

using UnityEngine;

namespace Complete.FSM.Actions
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/ShootAction")]
    public class ShootAction : StateAction
    {
        public float m_AttackRange;
        public LayerMask m_TargetMask;

        private TankAIShooting m_TankShooting;
        private StateController m_StateController;

        public override void Init(StateController controller)
        {
            m_TankShooting = controller.GetComponent<TankAIShooting>();
        }

        public override void Act(StateController controller)
        {
            RaycastHit hit;
            if (Physics.SphereCast(controller.eyes.position, 2, controller.eyes.forward, out hit, m_AttackRange, m_TargetMask))
            {
                m_TankShooting.Shoot(Vector3.Distance(hit.transform.position, controller.transform.position));
                Debug.DrawRay(controller.eyes.position, controller.eyes.forward.normalized * m_AttackRange, Color.red);
            }

        }

        public override void EndAction(StateController controller)
        {
        }
    }
}
