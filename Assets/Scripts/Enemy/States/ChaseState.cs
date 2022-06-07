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

        if (!aiManager.playerInSightRange && !aiManager.playerInAttackRange && aiManager.fov == null)
        {
            return idleState;
        }
        else if (aiManager.playerInAttackRange && aiManager.playerInSightRange && aiManager.fov.visibleTarget != null)
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
        if (aiManager.fov == null) return;
        if (aiManager.fov.visibleTarget != null)
        {
            aiManager.agent.destination = aiManager.player.position;
            aiManager.lastKnownTargetPosition = aiManager.player.position;
            aiManager.agent.Resume();
        }
        else
        {
            aiManager.agent.destination = aiManager.lastKnownTargetPosition;
        }

        //aiManager.agent.SetDestination(aiManager.player.position);
    }


}
