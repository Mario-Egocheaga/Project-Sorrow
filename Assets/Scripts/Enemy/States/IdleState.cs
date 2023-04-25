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
        if (aiManager.agent.remainingDistance <= aiManager.agent.stoppingDistance) //done with path
        {
            Vector3 point;
            if (RandomPoint(transform.position, aiManager.range, out point)) //pass in our centre point and radius of area
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
                aiManager.agent.SetDestination(point);
            }
        }
        /*
        //Walkpoint reached
        if (aiManager.distanceToWalkPoint.magnitude < 1f)
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
        */

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

    /*
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-aiManager.walkPointRange, aiManager.walkPointRange);
        float randomX = Random.Range(-aiManager.walkPointRange, aiManager.walkPointRange);

        aiManager.walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(aiManager.walkPoint, -transform.up, 2f, aiManager.whatIsGround))
            aiManager.walkPointSet = true;
    }
    */
}
