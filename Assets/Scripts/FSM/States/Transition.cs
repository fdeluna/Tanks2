/**
* Unity Finite State AI with the Delegate Pattern
* https://unity3d.com/es/learn/tutorials/topics/navigation/finite-state-ai-delegate-pattern
**/

using Complete.FSM.Decisions;
using Complete.FSM.States;

namespace Complete.FSM
{
    [System.Serializable]
    public class Transition
    {
        public Decision decision;
        public State trueState;
        public State falseState;
    }
}