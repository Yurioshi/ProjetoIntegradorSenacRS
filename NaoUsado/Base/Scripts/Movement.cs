using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Vector3 direction;
    public Transform cam;
    public bool isRunning = false;
    private readonly float turnSmoothTime = 0.2f;
    Animator anim;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");

        direction = new Vector3(hor, 0f, ver);

        Rotate();

        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
            Run();
        }
        else
        {
            isRunning = false;
            Walk();
        }
    }

    private void Walk()
    {
        if (direction.normalized.magnitude > 0)
        {
            anim.SetBool("IsMoving", true);
            anim.SetFloat("Speed", 0f, 0.05f, Time.fixedDeltaTime);
        }
        else
        {
            anim.SetBool("IsMoving", false);
        }
    }

    private void Run()
    {
        if (direction.normalized.magnitude > 0)
        {
            anim.SetBool("IsMoving", true);
            anim.SetFloat("Speed", 1f, 0.05f, Time.fixedDeltaTime);
        }
        else
        {
            anim.SetFloat("Speed", 0f, 0.05f, Time.fixedDeltaTime);
            anim.SetBool("IsMoving", false);
        }
    }

    private void Rotate()
    {
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        float angle = Quaternion.Slerp(Quaternion.Euler(0f, transform.eulerAngles.y, 0f), Quaternion.Euler(0f, targetAngle, 0f), turnSmoothTime).eulerAngles.y;
        Vector3 moveDir = new Vector3 (0f, angle, 0f);
        if (direction.magnitude > 0f)
        {
            transform.eulerAngles = moveDir;
        }
    }
}