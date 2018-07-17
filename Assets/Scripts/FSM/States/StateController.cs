/**
 * Unity Finite State AI with the Delegate Pattern
 * https://unity3d.com/es/learn/tutorials/topics/navigation/finite-state-ai-delegate-pattern
 **/

using Complete.FSM.States;
using Complete.Tank;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Complete.FSM
{
    public class StateController : MonoBehaviour
    {
        [Header("AI Inputs")]
        public Transform eyes;
        [Header("States Setup")]
        public State m_InitialState;
        public State m_CurrentState;
        public State m_DamageState;
        public bool m_AIActive = false;

        [HideInInspector] public float m_StateTimeElapsed;
        [HideInInspector] public Transform m_TargetTransform;
        [HideInInspector] public float m_TankCurrentHealth;


        private Dictionary<String, State> m_States = new Dictionary<String, State>();
        
        private void OnEnable()
        {
            m_AIActive = true;
            TankHealth.OnTankDamaged -= OnTankDamaged;
            TankHealth.OnTankDamaged += OnTankDamaged;
        }

        private void OnDisable()
        {
            m_AIActive = false;
            m_CurrentState = GetState(m_InitialState);
            TankHealth.OnTankDamaged -= OnTankDamaged;
        }

        void Update()
        {
            if (!m_AIActive)
                return;
            m_CurrentState.UpdateState();
        }

        public void TransitionToState(State nextState)
        {
            if (nextState != null && nextState != m_CurrentState)
            {
                m_CurrentState = GetState(nextState);
                ResetStateTimeElapsed();
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

        public void ResetStateTimeElapsed()
        {
            m_StateTimeElapsed = 0;
        }

        public void OnTankDamaged(float percentage, Transform damagedBy)
        {
            m_TankCurrentHealth = percentage;
            m_TargetTransform = damagedBy;

            if (m_DamageState != null)
            {
                TransitionToState(m_DamageState);
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

        void OnDrawGizmos()
        {
            if (m_CurrentState != null && eyes != null)
            {
                Gizmos.color = m_CurrentState.m_SceneGizmoColor;
                Gizmos.DrawWireSphere(eyes.position, 1);
                if (m_TargetTransform != null)
                    Gizmos.DrawWireSphere(m_TargetTransform.position, 1);
            }
        }
    }
}