using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private FPCharacterController controller;

    private float verticalVelocity;
    private float gravity = 14.0f;
    private float jumpForce = 10f;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<FPCharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.isGrounded)
        {
            verticalVelocity = -gravity * Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity = jumpForce;
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        Vector3 moveVector = Vector3.zero;
        moveVector.x = Input.GetAxisRaw("Horizontal") * 5.0f;
        moveVector.y = verticalVelocity + 163.8f;
        moveVector.z = Input.GetAxisRaw("Vertical") * 5.0f;
        controller.transform.position = moveVector;
    }
}
