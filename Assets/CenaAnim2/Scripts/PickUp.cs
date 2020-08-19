using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    WeaponManager activeWeapon;
    Weapon weaponToPick;

    private void Awake()
    {
        activeWeapon = GetComponent<WeaponManager>();
    }

    private void Update()
    {
        if(activeWeapon)
        {
            if(weaponToPick)
            {
                if(Input.GetKeyDown(KeyCode.E))
                {
                    activeWeapon.Equip(weaponToPick);
                    weaponToPick = null;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Weapon weapon = other.GetComponent<Weapon>();
        if(weapon && !weaponToPick)
        {
            weaponToPick = weapon;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Weapon weapon = other.GetComponent<Weapon>();
        if(weapon)
        {
            if (weaponToPick == weapon)
            {
                weaponToPick = null;
            }
        }
    }
}
