using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equip : MonoBehaviour
{
    Weapon weaponToPick;
    [SerializeField] Chest activeChest;
    ActiveWeapon activeWeapon;
    
    private void Awake()
    {
        activeWeapon = GetComponent<ActiveWeapon>();
    }

    private void OnTriggerEnter(Collider other)
    {
        weaponToPick = other.GetComponent<Weapon>();
        activeChest = other.GetComponent<Chest>();
    }

    private void OnTriggerExit(Collider other)
    {
        weaponToPick = null;
        activeChest = null;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(!activeWeapon.weapon)
            {
                Weapon weapon = weaponToPick;
                if (activeChest)
                {
                    weapon = activeChest.OpenChest();
                }
                if(weapon)
                {
                    PickUpWeapon(weapon);
                }
            }
        }
    }

    public void PickUpWeapon(Weapon weaponToPick)
    {
        Weapon weapon = weaponToPick;
        weapon.UnableColliders();
        weapon.rb.Sleep();
        weapon.rb.velocity = Vector3.zero;
        Destroy(weapon.rb);
        activeWeapon.Equip(weapon);
    }
}
