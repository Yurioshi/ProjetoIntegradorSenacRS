using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Rigidbody rb;
    public Collider attackCollider;
    public GameObject pickUpCollider;

    public void ResetRotation()
    {
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    public void AddRB()
    {
        rb = gameObject.AddComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.angularDrag = 0f;
    }

    public void RemoveRB()
    {
        Destroy(rb);
    }

    public void PickUpCollider(bool state)
    {
        pickUpCollider.SetActive(state);
    }
}
