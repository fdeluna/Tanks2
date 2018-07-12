/**
 * Unity Finite State AI with the Delegate Pattern
 * https://unity3d.com/es/learn/tutorials/topics/navigation/finite-state-ai-delegate-pattern
 **/
using UnityEngine;

namespace Complete.FSM.Actions
{        
    public abstract class Action : ScriptableObject
    {
        public abstract void Init(StateController controller);
        public abstract void Act();        
    }
}