﻿using Complete.Tank;
using UnityEngine;

namespace Complete.FSM.Actions
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/ChaseAction")]
    public class ChaseAction : StateAction
    {
        private TankAIMovement m_TankAIMovement;
        
        public override void Init(StateController controller)
        {
            m_TankAIMovement = controller.gameObject.GetComponent<TankAIMovement>();
        }

        public override void Act(StateController controller)
        {           
            m_TankAIMovement.Chase(controller.m_TargetTransform);
        }

        public override void EndAction(StateController controller)
        {
            m_TankAIMovement.Stop(true);
        }
    }
}
