using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiePatrollingState : StateMachineBehaviour
{
    float timer;
    public float patrollingTime = 10f;

    Transform player;
    UnityEngine.AI.NavMeshAgent agent;

    public float detectionArea = 18f;
    public float patrolSpeed =  2f;

    List<Transform> waypointsList = new List<Transform>();


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       player = GameObject.FindGameObjectWithTag("Player").transform;
       agent = animator.GetComponent<UnityEngine.AI.NavMeshAgent>();

       agent.speed = patrolSpeed;
       timer = 0;

       GameObject waypointCluster = GameObject.FindGameObjectWithTag("Waypoints");
       foreach(Transform t in waypointCluster.transform){
            waypointsList.Add(t);
       }

       Vector3 nextPosition = waypointsList[Random.Range(0,waypointsList.Count)].position;
       agent.SetDestination(nextPosition);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       if(agent.remainingDistance <= agent.stoppingDistance){
            agent.SetDestination(waypointsList[Random.Range(0, waypointsList.Count)].position);

       }

       timer += Time.deltaTime;
       if(timer > patrollingTime){
        animator.SetBool("isMoving", false);
       }


       float distanceFromPlayer = Vector3.Distance(player.position,animator.transform.position);
       if(distanceFromPlayer < detectionArea){
        animator.SetBool("isChasing",true);
       }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       agent.SetDestination(agent.transform.position);
    }
}
