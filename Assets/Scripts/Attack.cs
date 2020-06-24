using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Character character;
    public Rotation rotation;

    private void Awake()
    {
        character = GetComponent<Character>();
        rotation = GetComponent<Rotation>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(character.isHolding && !character.isAttacking && !character.isPicking && character.currentWeapon != null)
            {
                character.anim.SetTrigger("Attack");
            }
        }
    }

    private void Attacking()
    {
        character.attackingAnimDone = false;
        character.isAttacking = true;
        character.currentWeapon.attackCollider.enabled = true;
    }

    private void NotAttacking()
    {
        character.isAttacking = false;
        character.currentWeapon.attackCollider.enabled = false;
    }

    private void AnimationEnd()
    {
        character.attackingAnimDone = true;
    }
}
