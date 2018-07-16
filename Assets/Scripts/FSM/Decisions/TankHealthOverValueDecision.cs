using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Complete.FSM.Decisions
{
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
