using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string name;
    public List<Collider> pickUpCollider = new List<Collider>();
    public Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void UnableColliders()
    {
        for (int i = 0; i < pickUpCollider.Count; i++)
        {
            pickUpCollider[i].enabled = false;
        }
    }
}
