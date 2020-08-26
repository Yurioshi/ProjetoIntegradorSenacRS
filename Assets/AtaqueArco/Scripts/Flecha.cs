using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flecha : MonoBehaviour
{
    public Rigidbody rb;
    public Transform arrowRaycastPointsParent;
    Transform[] arrowRaycastPoints;
    bool inAir = false;
    RaycastHit hit;

    private void Awake()
    {
        arrowRaycastPoints = GetComponentsOnlyInChildren(arrowRaycastPointsParent);
    }

    public void StartInAirBehaviour()
    {
        inAir = true;
        StartCoroutine(StuckArrow());
        StartCoroutine(InAirBehaviour());
    }

    private IEnumerator InAirBehaviour()
    {
        rb.useGravity = true;
        while (inAir)
        {
            if (rb.velocity.magnitude > 0f) { rb.transform.rotation = Quaternion.LookRotation(rb.velocity); }
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator StuckArrow()
    {
        while(inAir)
        {
            for (int i = 0; i < arrowRaycastPoints.Length; i++)
            {
                if(i == 4) { break; }
                Vector3 direction = (arrowRaycastPoints[(i + 1)].position - arrowRaycastPoints[i].position).normalized;
                float distance = Vector3.Distance(arrowRaycastPoints[i].position, arrowRaycastPoints[(i + 1)].position);

                if (Physics.Raycast(arrowRaycastPoints[i].position, direction, out hit, distance))
                {
                    yield return new WaitForSeconds(0.006f);
                    inAir = false;
                    rb.isKinematic = true;
                    break;
                }
            }

            yield return new WaitForEndOfFrame();
        }
    }

    private Transform[] GetComponentsOnlyInChildren(Transform parent)
    {
        Transform[] children = parent.GetComponentsInChildren<Transform>();
        Transform[] firstChildren = new Transform[parent.childCount];
        int index = 0;
        foreach (Transform child in children)
        {
            if (child.parent == parent)
            {
                firstChildren[index] = child;
                index++;
            }
        }
        return firstChildren;
    }
}
