using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : EnemyAIStates
{
    public EnemyAIManager aiManager;

    public ChaseState chaseState;

    public PatrolState patrolState;

    public override EnemyAIStates RunCurrentState()
    {
        AttackPlayer();

        if (!aiManager.playerInAttackRange && aiManager.fov.visibleTarget != null && !PlayerMovement.isHidden)
        {
            return chaseState;
        }
        else if (aiManager.playerInAttackRange && aiManager.fov.visibleTarget != null && !PlayerMovement.isHidden)
        {
            return this;
        }
        else
        {
            return patrolState;
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
            aiManager.anim.Play("Attack");
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
