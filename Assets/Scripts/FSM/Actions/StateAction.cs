/**
 * Unity Finite State AI with the Delegate Pattern
 * https://unity3d.com/es/learn/tutorials/topics/navigation/finite-state-ai-delegate-pattern
 **/
using System;
using System.Collections;
using UnityEngine;

namespace Complete.FSM.Actions
{        
    public abstract class StateAction : ScriptableObject
    {
        public abstract void Init(StateController controller);
        public abstract void Act(StateController controller);
        public abstract void EndAction(StateController controller);


        protected IEnumerator WaitSecondsAction(float seconds, Action callBack = null)
        {
            yield return new WaitForSeconds(seconds);
            if (callBack != null)
                callBack.Invoke();
        }
    }
}