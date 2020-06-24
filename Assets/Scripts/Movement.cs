using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Character character;
    public Vector3 direction;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        character = GetComponent<Character>();
    }

    private void Update()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");

        direction = new Vector3(hor, 0f, ver);

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

    
}