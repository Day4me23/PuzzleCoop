using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    [Header("Important Variables")]
    [SerializeField] private CharacterController controller;
    [SerializeField] private float speed; //movement speed
    [SerializeField] private bool canMove; //determines if the player can move or not

    private Vector3 movement;
    private Vector3 velocity;
    private bool jumped;

    [Header("Ground Detection")]
    [SerializeField] private bool isGrounded = false; //is on the ground
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheckObject;
    [SerializeField] private float groundDistance = 0.2f;

    [Header("Jumping")]
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float gravity = -9.81f;

    private void Awake() {
        controller = GetComponent<CharacterController>();
    }

    private void FixedUpdate() {
        if (canMove) {
            //isGrounded = Physics.CheckSphere(groundCheckObject.position, groundDistance);
            //isGrounded = groundCheckObject.GetComponent<GroundCheck>().CheckGround(groundCheckObject.position, groundDistance, groundLayer);
            isGrounded = controller.isGrounded;
            if (isGrounded && velocity.y < 0f) { //if player is on the ground
                velocity.y = 0f;
            }

            //normal movement
            Vector3 m = (transform.right * movement.x + transform.forward * movement.z) * speed;
            controller.Move(m * Time.deltaTime);

            //jumping
            if(jumped && isGrounded) {
                velocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
            //gravity
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
    }

    /// <summary>
    /// Reads value into the context to create the movement vector. 
    /// </summary>
    /// <param name="context"></param>
    public void Move(InputAction.CallbackContext context) {
        movement = new Vector3(context.ReadValue<Vector2>().x, 0, context.ReadValue<Vector2>().y);
    }

    /// <summary>
    /// Allows the player to jump!
    /// </summary>
    /// <param name="context"></param>
    public void Jump(InputAction.CallbackContext context) {
        jumped = context.action.triggered;
    }

}
