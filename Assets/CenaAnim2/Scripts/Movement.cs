using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Animator anim;
    public Vector2 input;
    public Transform cam;
    float turnSpeed = 15f;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetFloat("Speed", 1f, 0.1f, Time.deltaTime);
        }
        else
        {
            anim.SetFloat("Speed", 0f, 0.1f, Time.deltaTime);
        }

        anim.SetFloat("InputX", input.x, 0.05f, Time.deltaTime);
        anim.SetFloat("InputY", input.y, 0.05f, Time.deltaTime);
    }

    private void FixedUpdate()
    {
        float camera = cam.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, camera, 0f), turnSpeed * Time.deltaTime);
    }
}
