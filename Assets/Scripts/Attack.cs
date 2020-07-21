using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Character character;
    
    private void Awake()
    {
        character = GetComponent<Character>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(character.isHolding && !character.isAttacking && !character.isPicking)
            {
                character.anim.SetTrigger("Attack");
                character.anim.SetBool("IsAttacking", true);
            }
        }
    }

    public void Attacking()
    {
        character.isAttacking = true;
        character.currentWeapon.attackCollider.enabled = true;
    }

    public void NotAttacking()
    {
        character.isAttacking = false;
        character.anim.SetBool("IsAttacking", false);
        character.currentWeapon.attackCollider.enabled = false;
    }
}
