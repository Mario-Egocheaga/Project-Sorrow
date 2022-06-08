using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IdleState : EnemyAIStates
{
    public EnemyAIManager aiManager;

    public ChaseState chaseState;

    public override EnemyAIStates RunCurrentState()
    {
        Patroling();

        if (!aiManager.playerInAttackRange && aiManager.fov.visibleTarget != null && !PlayerMovement.isHidden)
        {
            return chaseState;
        }
        else
        {
            aiManager.anim.Play("Walking");
            return this;
        }
   
    }

    private void Patroling()
    {
        //Does Not Have Predictable Pattern
        if (!aiManager.walkPointSet) SearchWalkPoint();

        if (aiManager.walkPointSet)
            aiManager.agent.SetDestination(aiManager.walkPoint);

        Vector3 distanceToWalkPoint = transform.position - aiManager.walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            aiManager.walkPointSet = false;

        //Has Predictable Pattern
        if (aiManager.hasPatrolPattern)
        {
            aiManager.agent.destination = aiManager.waypoints[aiManager.nextWaypoint].position;
            //aiManager.agent.Resume();
            if (aiManager.agent.remainingDistance <= aiManager.agent.stoppingDistance && !aiManager.agent.pathPending)
            {
                aiManager.nextWaypoint = (aiManager.nextWaypoint + 1) % aiManager.waypoints.Count;
            }
        }
    }

    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-aiManager.walkPointRange, aiManager.walkPointRange);
        float randomX = Random.Range(-aiManager.walkPointRange, aiManager.walkPointRange);

        aiManager.walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(aiManager.walkPoint, -transform.up, 2f, aiManager.whatIsGround))
            aiManager.walkPointSet = true;
    }
}
