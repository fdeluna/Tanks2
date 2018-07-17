using Complete.Tank;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Complete.FSM.Actions
{    
    public abstract class MovementAction : StateAction
    {
        public float m_avoidObstacleRadius = 3;
        protected TankAIMovement m_TankAIMovement;        

        public override void Init(StateController controller)
        {
            base.Init(controller);
            m_TankAIMovement = m_StateController.gameObject.GetComponent<TankAIMovement>();
            m_TankAIMovement.SetObstacleRadius(m_avoidObstacleRadius);
        }       
    }
}
