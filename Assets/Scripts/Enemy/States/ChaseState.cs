using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : EnemyAIStates
{
    public EnemyAIManager aiManager;

    public IdleState idleState;

    public AttackState attackState;


    public override EnemyAIStates RunCurrentState()
    {
        ChasePlayer();

        if (!aiManager.playerInSightRange && !aiManager.playerInAttackRange)
        {
            return idleState;
        }
        else if (aiManager.playerInAttackRange && aiManager.playerInSightRange)
        {
            return attackState;
        }
        else
        {
            Debug.Log("Chase");
            return this;
        }

    }

    private void ChasePlayer()
    {
        aiManager.agent.SetDestination(aiManager.player.position);
    }


}
