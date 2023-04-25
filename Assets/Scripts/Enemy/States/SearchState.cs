using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchState : EnemyAIStates
{
    public EnemyAIManager aiManager;

    public StateManager stateManager;

    public IdleState idleState;

    public ChaseState chaseState;

    private bool searched = false;

    private float stateTimeElasped;

    public override EnemyAIStates RunCurrentState()
    {
        Searching();

        if (searched && !aiManager.playerInAttackRange && aiManager.fov.visibleTarget == null && PlayerMovement.isHidden)
        {
            return idleState;
        }
        else if(aiManager.fov.visibleTarget != null && !PlayerMovement.isHidden)
        {
            return chaseState;
        }
        else
        {
            return this;
        }
    }

    void Searching()
    {
        HasTimeElapsed(10);
        if (HasTimeElapsed(10))
        {
            searched = true;
            aiManager.anim.Play("Win");
        }
        else
        {
            searched = false;
            aiManager.anim.Play("Look Around");
        }
    }

    public bool HasTimeElapsed(float duration)
    {
        stateTimeElasped += Time.deltaTime;
        if (stateTimeElasped >= duration)
        {
            stateTimeElasped = 0;
            return true;
        }
        else
        {
            return false;
        }
    }

}
