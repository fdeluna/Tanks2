﻿/**
 * Unity Finite State AI with the Delegate Pattern
 * https://unity3d.com/es/learn/tutorials/topics/navigation/finite-state-ai-delegate-pattern
 **/

using UnityEngine;

namespace Complete.FSM.Decisions
{    
    public abstract class Decision : ScriptableObject
    {       
        public abstract bool Decide(StateController controller);
    }
}