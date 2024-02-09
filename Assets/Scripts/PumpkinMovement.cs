using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PumpkinMovement : MonoBehaviour
{
    private GameObject player;
    private GameManager gameManager;
    private GameObject fireEffects;

    NavMeshAgent navMeshAgent;
    Animator animator;

    private float mesafe;
    public float health;
    private bool isDead;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        SearchInChildren(gameObject.transform);
    }

    void Update()
    {
        navMeshAgent.SetDestination(player.transform.position);
        mesafe = Vector3.Distance(transform.position, player.transform.position);

        if (health <= 0)
        {
            if(!isDead)
            {
                gameManager.deadPumpkinCounter++;
                isDead = true;
            }

            SetAnimationStates(isDead: true, isStop: true);

            fireEffects.SetActive(true);
            Destroy(this.gameObject, 4f);
        }
        else
        {
            if (mesafe < 30)
            {
                SetAnimationStates(isRun: true, isStop: false);

                if (mesafe < 5)
                {
                    SetAnimationStates(isPunch: true, isStop: true);
                }
            }
            else
            {
                SetAnimationStates(isWalk: true, isStop: false);
            }
        }

    }

    void SetAnimationStates(bool isDead = false, bool isWalk = false, bool isPunch = false, bool isRun = false, bool isStop = false)
    {
        navMeshAgent.isStopped = isStop;

        animator.SetBool("isDead", isDead);
        animator.SetBool("isWalk", isWalk);
        animator.SetBool("isPunch", isPunch);
        animator.SetBool("isRun", isRun);
    }

    void SearchInChildren(Transform parent)
    {
        foreach (Transform child in parent)
        {
            if (child.CompareTag("FireEffects"))
            {
                fireEffects = child.GetChild(0).gameObject;
            }

            SearchInChildren(child);
        }
    }

}
