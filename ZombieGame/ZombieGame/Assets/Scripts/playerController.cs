using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityStandardAssets.CrossPlatformInput;

public class playerController : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 15;
    private Vector3 move;

    public float gravity = -10f;
    public float jumpHeight = 2;
    private Vector3 velocity;

    public Transform groundCheck;
    public LayerMask groundLayer;
    private bool isGrounded;

   // public Animator animator;

    InputAction movement;
    InputAction jump;
    InputAction ride;

   // public FixedJoystick joystick;



    float rotationX = 0;
    public Camera playerCamera;
    public Vector2 LookAxis;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    void Start()
    {
        jump = new InputAction("Jump", binding: "<Gamepad>/a");
        jump.AddBinding("<keyboard>/space");

        movement = new InputAction("PlayerMovement", binding: "<Gamepad>/leftStick");
        movement.AddCompositeBinding("Dpad")
            .With("Up", "<keyboard>/w")
            .With("Up", "<keyboard>/upArrow")
            .With("Down", "<keyboard>/s")
            .With("Down", "<keyboard>/downArrow")
            .With("Left", "<keyboard>/a")
            .With("Left", "<keyboard>/letfArrow")
            .With("Right", "<keyboard>/d")
            .With("Right", "<keyboard>/rightArrow");

        movement.Enable();
        jump.Enable();

    }

    // Update is called once per frame
    void Update()
    {

        rotationX += -LookAxis.y * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, LookAxis.x * lookSpeed, 0);


        //float x = Input.GetAxis("Horizontal");
        //float z = Input.GetAxis("Vertical");
       
        //float x = joystick.Horizontal;
        //float z = joystick.Vertical;

        //animator.SetFloat("speed", Mathf.Abs(x) + Mathf.Abs(z));

       // move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        isGrounded = Physics.CheckSphere(groundCheck.position, 0.3f, groundLayer);
  
        if (isGrounded && velocity.y < 0)
            velocity.y = -1f;

        if (isGrounded)
        {
            if (CrossPlatformInputManager.GetButtonDown("Jump"))
            {
                Jump();
            }
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        controller.Move(velocity * Time.deltaTime);
    }

    private void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * 2 * -gravity);
    }



}
