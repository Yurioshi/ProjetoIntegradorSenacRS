using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum WeaponOrder
    {
        Primary, Secondary, Null
    }

    public string weaponName;
    public WeaponOrder weaponOrder = WeaponOrder.Null;
    public Rigidbody rb;
    public List<Collider> weaponColliders = new List<Collider>();
    public int damage;
    public AttackColliderController attackCollider;
    public List<Transform> weaponRaycastTransform = new List<Transform>();

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void ColliderController(bool state)
    {
        if(weaponColliders[0].enabled == state) { return; }

        for (int i = 0; i < weaponColliders.Count; i++)
        {
            weaponColliders[i].enabled = state;
        }
    }
}
