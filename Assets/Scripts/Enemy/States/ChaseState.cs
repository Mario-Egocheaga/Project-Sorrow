using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : EnemyAIStates
{
    public EnemyAIManager aiManager;

    public SearchState searchState;

    public AttackState attackState;


    public override EnemyAIStates RunCurrentState()
    {
        ChasePlayer();

        if (!aiManager.playerInAttackRange && aiManager.fov.visibleTarget == null && PlayerMovement.isHidden)
        {
            return searchState;
        }
        else if (aiManager.playerInAttackRange && aiManager.fov.visibleTarget != null && !PlayerMovement.isHidden)
        {
            return attackState;
        }
        else
        {
            aiManager.anim.Play("Running");
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
