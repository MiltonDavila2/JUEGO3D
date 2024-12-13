using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{

    [SerializeField] private int HP = 100;

    private Animator animator;
    private UnityEngine.AI.NavMeshAgent navAgent;

    public bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        navAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }


    public void TakeDamage(int DamageAmount){

        HP -= DamageAmount;

        if(HP<= 0){
            animator.SetTrigger("DIE");

            isDead = true;

        }else{
            animator.SetTrigger("DAMAGE");
        }
    }


}
