using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCotroller : MonoBehaviour
{
    public Animator enemyAnim;
    public Transform playerPosition;
    public NavMeshAgent enemyNavMeshAgent;
    public bool isChasing;

    private void Awake()
    {
        enemyAnim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (playerPosition)
        {
            isChasing = true;
        }
        else
        {
            isChasing = false;
        }

        enemyAnim.SetBool("IsIdle", !isChasing);
        enemyAnim.SetBool("IsChasing", isChasing);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            StartAttackPlayer();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetAttackState(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetAttackState(false);
        }
    }

    public void StartAttackPlayer()
    {
        enemyAnim.SetTrigger("Attack");
    }

    public void SetAttackState(bool attackState)
    {
        enemyAnim.SetBool("IsAttacking", attackState);
    }

    public void ChasePlayer()
    {
        if(playerPosition)
        {
            SetEnemyDestination(playerPosition.position);
            transform.LookAt(new Vector3(playerPosition.position.x, 0f, playerPosition.position.z));
        }
    }

    public void SetEnemyDestination(Vector3 destination)
    {
        enemyNavMeshAgent.SetDestination(destination);
    }
}
