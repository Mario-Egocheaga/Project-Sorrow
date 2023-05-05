using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : EnemyAIStates
{
    public EnemyAIManager aiManager;

    public ChaseState chaseState;

    public IdleState idleState;

    public override EnemyAIStates RunCurrentState()
    {
        Patroling();

        if (!aiManager.playerInAttackRange && aiManager.fov.visibleTarget != null && !PlayerMovement.isHidden)
        {
            return chaseState;
        }
        else if (!aiManager.playerInAttackRange && aiManager.fov.visibleTarget == null && PlayerMovement.isHidden)
        {
            return idleState;
        }
        else
        {
            aiManager.anim.Play("Walking");
            return this;
        }
   
    }


    private void Patroling()
    {
        if (aiManager.agent.remainingDistance <= aiManager.agent.stoppingDistance) 
        {
            Vector3 point;
            if (RandomPoint(transform.position, aiManager.range, out point)) 
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); 
                aiManager.agent.SetDestination(point);
            }
        }

    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) 
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

}
