using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public EnemyAIStates currentState;

    void Update()
    {
        RunStateMachine();
    }

    private void RunStateMachine()
    {
        EnemyAIStates nextState = currentState?.RunCurrentState();

        if (nextState != null)
        {
            SwitchToNextState(nextState);
        }
    }

    private void SwitchToNextState(EnemyAIStates nextState)
    {
        currentState = nextState;  
    }

}
