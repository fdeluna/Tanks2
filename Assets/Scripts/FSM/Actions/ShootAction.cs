using Complete.Tank;

using UnityEngine;

namespace Complete.FSM.Actions
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/ShootAction")]
    public class ShootAction : MovementAction
    {
        public float m_AttackRange;
        public LayerMask m_TargetMask;

        private TankAIShooting m_TankShooting;

        public override void Init(StateController controller)
        {
            base.Init(controller);
            m_TankShooting = controller.GetComponent<TankAIShooting>();
        }

        public override void Act()
        {
            RaycastHit hit;
            if (Physics.SphereCast(m_StateController.eyes.position, 2, m_StateController.eyes.forward, out hit, m_AttackRange, m_TargetMask))
            {
                m_TankShooting.Shoot(Vector3.Distance(hit.transform.position, m_StateController.transform.position));                
                Debug.DrawRay(m_StateController.eyes.position, m_StateController.eyes.forward.normalized * m_AttackRange, Color.red);
            }
        }

        public override void EndAction()
        {

        }
    }
}
