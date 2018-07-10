using Complete.Tank;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Complete.FSM.Actions
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/PatrolAction")]
    public class PatrolAction : Action
    {
        public float m_MinRange;
        public float m_MaxRange;

        private TankAIMovement m_TankAIMovement;

        public override void Init(StateController controller)
        {
            Debug.Log("init");
            m_TankAIMovement = controller.gameObject.GetComponent<TankAIMovement>();
        }

        public override void Act()
        {
            m_TankAIMovement.Patrol(m_MinRange, m_MaxRange);
        }                
    }
}
