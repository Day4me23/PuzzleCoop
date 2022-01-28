using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    /*
     * 
     * Mechanics to make: 
     * 1. Anti-Gravity
     * 2. Slow-fall
     * 3. Super Speed
     * 4. Super Jump
     * 
     */
    [Header("Important Variables")]
    [SerializeField] private CharacterController controller;
    [SerializeField] private float speed; //movement speed
    [SerializeField] private bool canMove; //determines if the player can move or not
    public Transform currentCheckpoint;

    [Header("OrbManager")]
    [SerializeField] private OrbHolderManager orbHolder;

    private Vector3 movement;
    [SerializeField] private Vector3 velocity;
    [SerializeField] private bool jumped;

    [Header("Ground Detection")]
    [SerializeField] private bool isGrounded = false; //is on the ground
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Transform groundCheckObject;
    [SerializeField] private float groundDistance = 0.2f;

    [Header("Jumping")]
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float gravity = -9.81f;
    private bool falling; //when switching gravity
    [SerializeField] private bool flipped;
    private void Awake() {
        controller = GetComponent<CharacterController>();
    }

    private void FixedUpdate() {
        if (canMove) {
               // isGrounded = controller.isGrounded;
                isGrounded = groundCheckObject.GetComponent<GroundCheck>().CheckGround(groundCheckObject.position, 0.4f, playerLayer);

            if (isGrounded && velocity.y < 0f) { //if player is on the ground
                velocity.y = 0f;
            }

            //normal movement
            Vector3 m = (transform.right * movement.x + transform.forward * movement.z) * speed;
            controller.Move(m * Time.deltaTime);

            //jumping
            if(jumped && isGrounded) {
                if (!flipped) {
                    velocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity);
                } else {
                    velocity.y -= Mathf.Sqrt(jumpHeight * 2f * gravity);
                }
                
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

    public void ChangeGravity() {
        gravity *= -1f;
        if (!flipped) {
            flipped = true;
            velocity.y = -1f;
        } else {
            flipped = false;
        }
        
        //flip player
        //Invoke("StopFalling", 1f);
        transform.Rotate(new Vector3(0f, 0f, 180f));
    }

    private void StopFalling() {
        falling = false;
    }
}
