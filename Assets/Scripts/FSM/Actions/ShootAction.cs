using Complete.Tank;

using UnityEngine;

namespace Complete.FSM.Actions
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/ShootAction")]
    public class ShootAction : Action
    {
        public float m_MinRange;
        public float m_MaxRange;

        private TankAIShooting m_TankShooting;

        public override void Init(StateController controller)
        {
            m_TankShooting = controller.GetComponent<TankAIShooting>();
        }

        public override void Act()
        {
            m_TankShooting.Shoot(m_MinRange, m_MaxRange);
        }
    }
}
