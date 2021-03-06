﻿/**
 * Unity Finite State AI with the Delegate Pattern
 * https://unity3d.com/es/learn/tutorials/topics/navigation/finite-state-ai-delegate-pattern
 **/
using System;
using System.Collections;
using UnityEngine;

namespace Complete.FSM.Actions
{
    /// <summary>
    /// Abstract to define states actions
    /// </summary>
    public abstract class StateAction : ScriptableObject
    {
        protected StateController m_StateController;

        public virtual void Init(StateController controller)
        {
            m_StateController = controller.gameObject.GetComponent<StateController>();
        }

        public abstract void Act();
        

        protected IEnumerator WaitSecondsAction(float seconds, Action callBack = null)
        {
            yield return new WaitForSeconds(seconds);
            if (callBack != null)
                callBack.Invoke();
        }
    }
}