/**
 * Unity Finite State AI with the Delegate Pattern
 * https://unity3d.com/es/learn/tutorials/topics/navigation/finite-state-ai-delegate-pattern
 **/

using Complete.FSM.States;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Complete.FSM
{
    public class StateController : MonoBehaviour
    {
        public State m_InitialState;
        public State m_CurrentState;        
        public Transform eyes;
        [HideInInspector] public float m_StateTimeElapsed;
        [HideInInspector] public Transform m_TargetTransform;

        private Dictionary<String, State> m_States = new Dictionary<String, State>();

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
                }
                else
                {
                    m_CurrentState.StopState();
                }
            }
        }

        public void Awake()
        {
            m_CurrentState = GetState(m_InitialState);
        }

        private void OnDisable()
        {
            m_CurrentState = GetState(m_InitialState);
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
            if (nextState != m_CurrentState)
            {
                m_CurrentState.StopState();
                m_CurrentState = GetState(nextState);

                OnExitState();
            }
        }

        public bool CheckIfCountDownElapsed(float duration)
        {
            bool countDownElapser = false;
            m_StateTimeElapsed += Time.deltaTime;

            countDownElapser = m_StateTimeElapsed >= duration;

            if (countDownElapser)
                m_StateTimeElapsed = 0;

            return countDownElapser;
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

        private State GetState(State stateType)
        {
            State state = null;
            if (m_States.ContainsKey(stateType.name))
            {
                m_States.TryGetValue(stateType.name, out state);
            }
            else
            {

                state = Instantiate(stateType);
                state.name = stateType.name;
                state.Init(this);

                m_States.Add(stateType.name, state);
            }

            return state;
        }
    }
}