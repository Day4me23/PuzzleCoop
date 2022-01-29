using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour {
    /*
     * 
     * Mechanics to make: 
     * 1. Anti-Gravity DONE
     * 2. Slow-fall DONE
     * 3. Super Speed DONE
     * 4. Fast Fall
     * 
     */
    [Header("Important Variables")]
    public Transform currentCheckpoint;
    public PlayerMovement oppositePlayer; // <--- the other player in the game
    [SerializeField] private CharacterController controller;
    [SerializeField] private bool canMove; //determines if the player can move or not
    

    #region Speed Variables
        [SerializeField] private float speed; //movement speed
        private float defaultSpeed = 3f;
        private float superSpeed = 9f;
        private float slowSpeed = 1f;
    #endregion
    #region Falling Variables
        [SerializeField] private float gravity = -9.81f;
        [SerializeField] private float fallSpeed; //for slow fall
        private float defaultFallSpeed = 1f;
        private float superFallSpeed = 2f;
        private float slowFallSpeed = 0.4f;
        private bool falling; //when switching gravity
        public bool flipped;
    #endregion

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
    private float defaultJumpHeight;
    private float superJumpHeight;
    
    
    private void Awake() {
        controller = GetComponent<CharacterController>();

    }
    private void Start() {
        DefaultState();
    }

    private void FixedUpdate() {
        if (GameManager.instance.levelHasStarted) {
            //isGrounded = controller.isGrounded;
            isGrounded = groundCheckObject.GetComponent<GroundCheck>().CheckGround(groundCheckObject.position, 0.2f, playerLayer);

            if (isGrounded && velocity.y < 0f && !flipped) { //if player is on the ground
                velocity.y = 0f;
            } else if (isGrounded && velocity.y > 0f && flipped) {
                velocity.y = 0f;
            }

            //normal movement
            Vector3 m = (transform.right * movement.x + transform.forward * movement.z) * speed;
            controller.Move(m * Time.deltaTime);

            //jumping
            if (jumped && isGrounded) {
                if (!flipped) {
                    velocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity);
                } else {
                    velocity.y -= Mathf.Sqrt(jumpHeight * 2f * gravity);
                }

            }
            //gravity
            velocity.y += gravity * fallSpeed * Time.deltaTime;
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

    public void DefaultState() {
        fallSpeed = 1f;
        gravity = -9.81f;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        flipped = false;
        speed = defaultSpeed;
        fallSpeed = defaultFallSpeed;

    }



    #region GRAVITY MECHANIC
    public void ChangeGravity() {
        gravity *= -1f;
        if (!flipped) {
            flipped = true;
            velocity.y = -1f;
        }
        else {
            flipped = false;
        }

        

        //flip player
        //Invoke("StopFalling", 1f);
        transform.Rotate(new Vector3(0f, 0f, 180f));
    }

    #endregion
    #region SPEED MECHANIC
    public void SlowSpeed() {
        speed = slowSpeed;
        
    }

    public void FastSpeed() {
        speed = superSpeed;
        oppositePlayer.SlowSpeed();
    }

    public void NormalSpeed() {
        speed = defaultSpeed;
        //oppositePlayer.NormalSpeed();
    }
    #endregion
    #region FALLING MECHANIC
    public void SlowFall() {
        fallSpeed = slowFallSpeed;
        oppositePlayer.FastFall();
    }

    public void FastFall() {
        fallSpeed = superFallSpeed;
        
    }

    public void NormalFall() {
        fallSpeed = defaultFallSpeed;
        //oppositePlayer.NormalFall();
    }
    #endregion

}
