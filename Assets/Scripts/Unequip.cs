using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unequip : MonoBehaviour
{
    public Character character;
    
    private void Awake()
    {
        character = GetComponent<Character>();
    }

    private void LateUpdate()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            UnequipWeapon();
        }
    }

    public void UnequipWeapon()
    {
        if (character.isHolding && !character.isPicking && character.attackingAnimDone)
        {
            character.currentWeapon.transform.parent = null;
            character.currentWeapon.AddRB();
            character.currentWeapon.PickUpCollider(true);
            character.currentWeapon = null;
            character.isHolding = false;
            character.anim.SetBool("IsHolding", false);
            Debug.Log("Terminou de desequipar");
        }
    }
}
