using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : EnemyAIStates
{
    public EnemyAIManager aiManager;

    public ChaseState chaseState;

    public override EnemyAIStates RunCurrentState()
    {
        //AttackPlayer();

        if (aiManager.playerInSightRange && !aiManager.playerInAttackRange)
        {
            return chaseState;
        }

        else
        {
            Debug.Log("Attack");
            return this;
        }
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        aiManager.agent.SetDestination(transform.position);

        transform.LookAt(aiManager.player);

        if (!aiManager.alreadyAttacked)
        {
            ///Attack code here
            ///End of attack code

            aiManager.alreadyAttacked = true;
            Invoke(nameof(ResetAttack), aiManager.timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        aiManager.alreadyAttacked = false;
    }

}
