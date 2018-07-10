/**
 * Unity Finite State AI with the Delegate Pattern
 * https://unity3d.com/es/learn/tutorials/topics/navigation/finite-state-ai-delegate-pattern
 **/
using Complete.FSM.Actions;
using UnityEngine;

namespace Complete.FSM.States
{
    [CreateAssetMenu(menuName = "PluggableAI/State")]
    public class State : ScriptableObject
    {
        public Action[] m_Actions;
        public Transition[] m_Transitions;
        public Color m_SceneGizmoColor = Color.grey;

        private StateController m_StateController;

        public void StartState(StateController controller)
        {
            m_StateController = controller;
            for (int i = 0; i < m_Actions.Length; i++)
            {
                m_Actions[i].Init(m_StateController);
            }
        }

        public void StopState()
        {

        }

        public void UpdateState()
        {
            DoActions();
            CheckTransitions(m_StateController);
        }

        public void ExitState()
        {

        }

        private void DoActions()
        {
            for (int i = 0; i < m_Actions.Length; i++)
            {
                m_Actions[i].Act();
            }
        }

        private void CheckTransitions(StateController controller)
        {
            for (int i = 0; i < m_Transitions.Length; i++)
            {
                bool decisionSucceeded = m_Transitions[i].decision.Decide(controller);

                if (decisionSucceeded)
                {
                    controller.TransitionToState(m_Transitions[i].trueState);
                }
                else
                {
                    controller.TransitionToState(m_Transitions[i].falseState);
                }
            }
        }
    }
}
