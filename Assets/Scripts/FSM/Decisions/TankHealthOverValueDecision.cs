using UnityEngine;

namespace Complete.FSM.Decisions
{
    /// <summary>
    /// Decision to check if tank health is over a given value
    /// </summary>
    [CreateAssetMenu(menuName = "PluggableAI/Decisions/TankHealthOverValue")]
    public class TankHealthOverValueDecision : Decision
    {
        public float m_HealthPercentage = 30;

        public override bool Decide(StateController controller)
        {            
            return controller.m_TankCurrentHealth >= m_HealthPercentage;
        }
    }
}
