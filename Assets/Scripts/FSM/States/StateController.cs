/**
 * Unity Finite State AI with the Delegate Pattern
 * https://unity3d.com/es/learn/tutorials/topics/navigation/finite-state-ai-delegate-pattern
 **/

using Complete.FSM.States;
using UnityEngine;

namespace Complete.FSM
{
    public class StateController : MonoBehaviour
    {
        public State m_CurrentState;        
        public State m_RemainState;
        public Transform eyes;
        [HideInInspector] public float m_StateTimeElapsed;
        [HideInInspector] public Transform m_Player;

        public bool Active
        {
            get
            {
                return m_AIActive;
            }
            set
            {
                m_AIActive = value;

                if (m_AIActive)
                {
                    m_CurrentState.StartState(this);
                }
                else
                {
                    m_CurrentState.StopState();
                }
            }
        }

        public void Awake()
        {
            m_CurrentState.StartState(this);
        }

        public bool m_AIActive = false;
        
        void Update()
        {
            if (!m_AIActive)
                return;
            
            m_CurrentState.UpdateState();
        }

        public void TransitionToState(State nextState)
        {
            if (nextState != m_RemainState)
            {
                m_CurrentState.StopState();
                m_CurrentState = nextState;
                m_CurrentState.StartState(this);
                OnExitState();
            }
        }

        public bool CheckIfCountDownElapsed(float duration)
        {
            m_StateTimeElapsed += Time.deltaTime;
            return (m_StateTimeElapsed >= duration);
        }

        private void OnExitState()
        {
            m_StateTimeElapsed = 0;
        }
        
        void OnDrawGizmos()
        {
            if (m_CurrentState != null && eyes != null)
            {
                Gizmos.color = m_CurrentState.m_SceneGizmoColor;
                Gizmos.DrawWireSphere(eyes.position, 1);
            }
        }
    }
}