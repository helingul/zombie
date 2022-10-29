using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityStandardAssets.CrossPlatformInput;

public class playerController : MonoSingleton<playerController>
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

    //public Animator animator;

    InputAction movement;
    InputAction jump;
    InputAction ride;


    public GameOverScreen gameOverScreen;
    public bool isAlive = true;

    public Transform lootCollector;
    
    
    //public FixedJoystick joystick;
    
    public int HP;
    void Start()
    {
        HP = 100;
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

    void OnEnable()
    {
        if(isAlive)
            return;

        isAlive = true;
        
        HP = 100;
        movement.Enable();
        jump.Enable();
    }

    private void OnDisable()
    {
        movement.Disable();
        jump.Disable();
    }

    public void ResetPlayerValues()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        //float x = joystick.Horizontal;
        //float z = joystick.Vertical;

        //animator.SetFloat("speed", Mathf.Abs(x) + Mathf.Abs(z));

        move = transform.right * x + transform.forward * z;

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

    public void TakeDamage(int amount)
    {
        if(HP <= 0)
            return;
        
        HP -= amount;
        Debug.Log("CANIN : "+ HP);
        
        
        if (HP <= 0)
        {
            isAlive = false;
            Debug.Log("GAME OVER");
            gameOverScreen.SetupScreen();
            HP = 0;
        } 
    }
        

}
