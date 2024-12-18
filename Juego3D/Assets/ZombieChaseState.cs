using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieChaseState : StateMachineBehaviour
{

    UnityEngine.AI.NavMeshAgent agent;

    Transform player;

    public float chaseSpeed = 6f;

    public float stopChasingDistance = 21f;

    public float attackingDistance = 3.0f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       player = GameObject.FindGameObjectWithTag("Player").transform;
       agent = animator.GetComponent<UnityEngine.AI.NavMeshAgent>();

       agent.speed = chaseSpeed;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       agent.SetDestination(player.position);
       animator.transform.LookAt(player);

       float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);
       Debug.Log("Distance from Player: " + distanceFromPlayer);

        if(distanceFromPlayer > stopChasingDistance){
            animator.SetBool("isChasing",false);
        }

        if (distanceFromPlayer < attackingDistance + 0.5f) // Aumenta con un margen
        {
            animator.SetBool("isAttacking", true);
            Debug.Log("Started Attacking Player");
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       agent.SetDestination(animator.transform.position);
    }
}
