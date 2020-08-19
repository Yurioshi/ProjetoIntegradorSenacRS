using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackColliderController : MonoBehaviour
{
    public WeaponManager weaponManager;
    public bool canDamage;
    Weapon weapon;
    RaycastHit hit;

    private void Update()
    {
        if (canDamage)
        {
            Vector3 dirToTarget1 = (weapon.weaponRaycastTransform[1].position - weapon.weaponRaycastTransform[0].position).normalized;
            Vector3 dirToTarget2 = (weapon.weaponRaycastTransform[2].position - weapon.weaponRaycastTransform[1].position).normalized;
            Vector3 dirToTarget3 = (weapon.weaponRaycastTransform[0].position - weapon.weaponRaycastTransform[2].position).normalized;

            float dstToTarget1 = Vector3.Distance(weapon.weaponRaycastTransform[0].position, weapon.weaponRaycastTransform[1].position);
            float dstToTarget2 = Vector3.Distance(weapon.weaponRaycastTransform[1].position, weapon.weaponRaycastTransform[2].position);
            float dstToTarget3 = Vector3.Distance(weapon.weaponRaycastTransform[2].position, weapon.weaponRaycastTransform[0].position);

            if (Physics.Raycast(weapon.weaponRaycastTransform[0].position, dirToTarget1, out hit, dstToTarget1))
            {
                DealDamage();
            }
            else if(Physics.Raycast(weapon.weaponRaycastTransform[1].position, dirToTarget2, out hit, dstToTarget2))
            {
                DealDamage();
            }
            else if(Physics.Raycast(weapon.weaponRaycastTransform[2].position, dirToTarget3, out hit, dstToTarget3))
            {
                DealDamage();
            }
        }
    }

    public void DealDamage()
    {
        Health enemyHealth = hit.collider.GetComponentInParent<Health>();
        if (enemyHealth)
        {
            enemyHealth.LoseHealth(weapon.damage);
            canDamage = false;

        }
    }

    public void IsAttacking()
    {
        weapon = weaponManager.GetActualWeapon();
        canDamage = true;
    }

    public void IsNotAttacking()
    {
        canDamage = false;
    }
}
