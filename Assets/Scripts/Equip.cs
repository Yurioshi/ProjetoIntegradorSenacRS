using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equip : MonoBehaviour
{
    public Character character; 
    public Weapon weaponToPick;
    
    private void Awake()
    {
        character = GetComponent<Character>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Weapon"))
        {
            weaponToPick = other.GetComponentInParent<Weapon>();

            Debug.Log(weaponToPick.gameObject.name);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            Weapon weaponUnpicked = other.GetComponentInParent<Weapon>();
            if(weaponToPick == weaponUnpicked)
            {
                weaponToPick = null;
            }
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            PickUpWeapon();
        }
    }

    public void PickUpWeapon()
    {
        if(weaponToPick != null && !character.isHolding && character.currentWeapon == null)
        {
            character.currentWeapon = weaponToPick;
            character.isPicking = true;
            character.anim.SetTrigger("PickUp");
        }
    }

    public void EquipWeapon()
    {
        character.currentWeapon.PickUpCollider(false);
        character.currentWeapon.RemoveRB();
        character.currentWeapon.transform.position = character.hand.position;
        character.currentWeapon.transform.parent = character.hand;
        character.isHolding = true;
        character.anim.SetBool("IsHolding", true);
        character.currentWeapon.ResetRotation();
    }
    public void WeaponEquiped()
    {
        character.isPicking = false;
    }
}
