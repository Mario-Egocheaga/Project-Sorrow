using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IdleState : EnemyAIStates
{
    public EnemyAIManager aiManager;

    public PatrolState patrolState;

    public ChaseState chaseState;

    float m_WaitTime;
    public float startWaitTime = 3;

    public override EnemyAIStates RunCurrentState()
    {
        Idle();

        if (!aiManager.playerInAttackRange && aiManager.fov.visibleTarget == null && PlayerMovement.isHidden)
        {
            return patrolState;
        }
        else if (!aiManager.playerInAttackRange && aiManager.fov.visibleTarget != null && !PlayerMovement.isHidden)
        {
            return chaseState;
        }
        else
        {
            return this;
        }
   
    }


    private void Idle()
    {
        if (m_WaitTime <= 0)
        {
            m_WaitTime = startWaitTime;
        }
        else
        {
            Stop();
            m_WaitTime -= Time.deltaTime;
        }
    }

    void Stop()
    {
        PlayRandomAnim();
    }


    private void PlayRandomAnim()
    {
        int ranNum = Random.Range(1, 2);
        switch (ranNum)
        {

            case 2:
                aiManager.anim.Play("Arm Stretching");
                break;
            case 1:
                aiManager.anim.Play("Look Over Shoulder");
                break;
            default:
                aiManager.anim.Play("Look Around");
                break;
        }
    }

}
