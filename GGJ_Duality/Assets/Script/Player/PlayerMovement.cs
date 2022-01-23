using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private CharacterController controller;
    [SerializeField] private float speed; //movement speed
    [SerializeField] private bool canMove;

    private Vector3 movement;
    private void Awake() {
        controller = GetComponent<CharacterController>();
    }

    private void FixedUpdate() {
        if (canMove) {
            Vector3 m = (transform.right * movement.x + transform.forward * movement.z) * speed;
            controller.Move(m * Time.deltaTime);
        }
    }

    /// <summary>
    /// Reads value into the context to create the movement vector. 
    /// </summary>
    /// <param name="context"></param>
    public void Move(InputAction.CallbackContext context) {
        movement = new Vector3(context.ReadValue<Vector2>().x, 0, context.ReadValue<Vector2>().y);
    }
}
