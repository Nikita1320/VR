using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Camera vrCamera;
    [SerializeField] private float moveSpeed;
    [SerializeField] private InputActionProperty touchPadValueInputAction;

    [Header("GravitySettings")]
    [SerializeField] private bool useGravity = false;
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private float checkSphereRadius = 0.4f;
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private LayerMask groundMask;
    private Vector3 velocity;
    public bool IsGrounded => Physics.CheckSphere(groundCheckPoint.position, checkSphereRadius, groundMask);
    private void Update()
    {
        Move(touchPadValueInputAction.action.ReadValue<Vector2>());
        if (useGravity == true)
        {
            if (IsGrounded && velocity.y < 0)
            {
                velocity.y = -2;
            }

            velocity.y += gravity * Time.deltaTime;
            characterController.Move(velocity * Time.deltaTime);
        }
    }

    public void Move(Vector2 direction)
    {
        Vector3 moveDirection = vrCamera.transform.right * direction.x + vrCamera.transform.forward * direction.y;

        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
    }
}
