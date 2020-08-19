using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    //public UnityEngine.Animations.Rigging.Rig handIK;
    //public Transform rightHandGrip;
    public Movement movement;
    public Transform weaponParent;
    public Animator rigController;
    public Animator characterController;
    public Weapon weapon;
    public Teste teste;
    bool isHolsterned = false;
    bool isAttacking = false;
    bool endAttack = false;

    private void Start()
    {
        Weapon existingWeapon = gameObject.GetComponentInChildren<Weapon>();

        if(existingWeapon)
        {
            Equip(existingWeapon);
        }
    }

    private void Update()
    {
        if(weapon && !isAttacking)
        {
            if (Input.GetMouseButtonDown(0) && !movement.isRunning && !rigController.GetBool("IsHolsterned"))
            {
                isAttacking = true;
                StartCoroutine(Attack());
            }
            else if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                bool isHolsterned = rigController.GetBool("IsHolsterned");
                if (isHolsterned) { StartCoroutine(Unholstern()); }
                else { StartCoroutine(Holstern()); }
            }
            else if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                StartCoroutine(Holstern());
            }
        }
    }

    public IEnumerator Attack()
    {
        characterController.SetTrigger("Attack");
        rigController.SetBool("IsAttacking", true);

        do
        {
            Debug.Log("Attacking");
            yield return new WaitForEndOfFrame();
        } while (!endAttack);

        endAttack = false;
        rigController.SetBool("IsAttacking", false);
        isAttacking = false;
    }

    public IEnumerator Unholstern()
    {
        if (isHolsterned)
        {
            rigController.SetBool("IsHolsterned", false);
            do
            {
                Debug.Log("Unhosting");
                yield return new WaitForEndOfFrame();
            } while (!teste.endUnhost);

            teste.endUnhost = false;
            isHolsterned = false;
        }
    }

    public IEnumerator Holstern()
    {
        if (!isHolsterned)
        {
            rigController.SetBool("IsHolsterned", true);

            do
            {
                Debug.Log("Unhosting");
                yield return new WaitForEndOfFrame();
            } while (!teste.endHost);
            
            teste.endHost = false;
            isHolsterned = true;
        }
    }
    
    public void Equip(Weapon newWeapon)
    {
        Debug.Log("Equipou");
        weapon = newWeapon;
        weapon.transform.parent = weaponParent;
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;

        rigController.Play("Equip" + weapon.name);
        isHolsterned = false;
    }

    public void Teste3()
    {
        Debug.Log("Sucesso.Attack");
        endAttack = true;
    }
}
