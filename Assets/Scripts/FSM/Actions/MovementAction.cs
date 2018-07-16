using Complete.Tank;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Complete.FSM.Actions
{    
    public abstract class MovementAction : StateAction
    {
        protected TankAIMovement m_TankAIMovement;        

        public override void Init(StateController controller)
        {
            base.Init(controller);
            m_TankAIMovement = m_StateController.gameObject.GetComponent<TankAIMovement>();
        }       
    }
}
