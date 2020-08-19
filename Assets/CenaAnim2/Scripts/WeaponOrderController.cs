using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponOrderController : MonoBehaviour
{
    public WeaponManager weaponManager;

    public void IsHolstered()
    {
        if(weaponManager.activeWeaponIndex == 0)
        {
            Debug.Log("PrimaryI");
            weaponManager.rigController.Play("PrimaryWeaponHolster", 1);
        }
        else
        {
            Debug.Log("SecundaryI");
            weaponManager.rigController.Play("SecondaryWeaponHolster", 2);
        }
    }

    public void IsNotHolstered()
    {
        if (weaponManager.activeWeaponIndex == 0)
        {
            Debug.Log("PrimaryN");
            weaponManager.rigController.Play("PrimaryWeaponEquip", 1);
        }
        else
        {
            Debug.Log("SecundaryN");
            weaponManager.rigController.Play("SecondaryWeaponEquip", 2);
        }
    }
}
