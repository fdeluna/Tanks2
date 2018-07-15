using Complete.Tank;

using UnityEngine;

namespace Complete.FSM.Actions
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/ShootAction")]
    public class ShootAction : StateAction
    {
        public float m_AttackRange;        

        private TankAIShooting m_TankShooting;
        private StateController m_StateController;

        public override void Init(StateController controller)
        {
            m_TankShooting = controller.GetComponent<TankAIShooting>();
        }

        public override void Act(StateController controller)
        {
            //RaycastHit hit;

            //Debug.DrawRay(m_StateController.eyes.position, m_StateController.eyes.forward.normalized * m_AttackRange, Color.red);

            //if (Physics.SphereCast(m_StateController.eyes.position,2, m_StateController.eyes.forward, out hit, m_AttackRange)
            //    && hit.collider.CompareTag("Player"))
            //{
            //    if (controller.CheckIfCountDownElapsed(controller.enemyStats.attackRate))
            //    {
            //        controller.tankShooting.Fire(controller.enemyStats.attackForce, controller.enemyStats.attackRate);
            //    }
            //}

        }

        public override void EndAction(StateController controller)
        {            
        }
    }
}
