using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Character character;
    public Vector3 direction;
    private readonly float turnSmoothTime = 0.2f;

    private void Awake()
    {
        character = GetComponent<Character>();
    }

    void Update()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");

        direction = new Vector3(hor, 0f, ver);

        Rotate();

        if (Input.GetKey(KeyCode.LeftShift))
        {
            Run();
        }
        else
        {
            Walk();
        }
    }

    private void Walk()
    {
        if (direction.normalized.magnitude > 0)
        {
            character.anim.SetBool("IsMoving", true);
            character.anim.SetFloat("Speed", 0f, 0.05f, Time.fixedDeltaTime);
        }
        else
        {
            character.anim.SetBool("IsMoving", false);
        }
    }

    private void Run()
    {
        if (direction.normalized.magnitude > 0)
        {
            character.anim.SetBool("IsMoving", true);
            character.anim.SetFloat("Speed", 1f, 0.05f, Time.fixedDeltaTime);
        }
        else
        {
            character.anim.SetFloat("Speed", 0f, 0.05f, Time.fixedDeltaTime);
            character.anim.SetBool("IsMoving", false);
        }
    }

    private void Rotate()
    {
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        float angle = Quaternion.Slerp(Quaternion.Euler(0f, transform.eulerAngles.y, 0f), Quaternion.Euler(0f, targetAngle, 0f), turnSmoothTime).eulerAngles.y;

        if (direction.magnitude > 0f && !character.isPicking)
        {
            transform.eulerAngles = new Vector3(0f, angle, 0f);
        }
    }
}