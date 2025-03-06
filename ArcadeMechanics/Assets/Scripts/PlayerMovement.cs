using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask GroundLayer;
    bool grounded;

    float horizontalInput;
    float verticalInput;

    public Transform orientation;

    Vector3 moveDirection;

    Rigidbody playerObject;

    private void Start()
    {
        playerObject = GetComponent<Rigidbody>();
        playerObject.freezeRotation = true;
    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight / 2f + 0.2f, GroundLayer);

        if (grounded)
        {
            playerObject.linearDamping = groundDrag;
        }
        else
        {
            playerObject.angularDamping = groundDrag;
        }

        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        playerObject.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }
}
