using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    EnemyController enemyController;

    private void Awake()
    {
        enemyController = GetComponent<EnemyController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
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
        enemyController.enemyAnim.SetTrigger("Attack");
    }

    public void SetAttackState(bool attackState)
    {
        enemyController.enemyAnim.SetBool("IsAttacking", attackState);
    }
}
