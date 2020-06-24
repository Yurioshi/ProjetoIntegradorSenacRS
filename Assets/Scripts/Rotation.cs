using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public Character character;
    public Vector3 direction;
    public Transform cam;
    private readonly float turnSmoothTime = 0.3f;
    float turnSmoothVelocity;

    private void Awake()
    {
        character = GetComponent<Character>();
    }

    private void FixedUpdate()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");

        direction = new Vector3(hor, 0f, ver);

        Rotate();
    }

    public void Rotate()
    {
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);//Quaternion.Slerp(Quaternion.Euler(0f, transform.eulerAngles.y, 0f), Quaternion.Euler(0f, targetAngle, 0f), turnSmoothTime).eulerAngles.y;
        Quaternion moveDir = Quaternion.Euler(0f, angle, 0f);

        if (direction.magnitude > 0f && !character.isPicking || character.isAttacking)
        {
            transform.rotation = moveDir;
        }
    }
}
