using UnityEngine;
using System.Collections;
using Unity.Multiplayer.Center.Common.Analytics;
using UnityEditor.Experimental.GraphView;

public class PlayerController : MonoBehaviour
{
    public enum CharacterType
    {
        NPC,
        Enemy,
        Player
    }

    public enum CharacterAge
    {
        Child,
        Teen,
        Adult,
        Elder
    }

    #region Player Status bools (blocking, dead, alive, running, etc)
    public bool isGrounded = false; //is the player on the ground
    public bool isRunning = false; //is the player running
    public bool isCrouching = false; //is the player crouching
    public bool isJumping = false; //is the player jumping
    public bool isAttacking = false; //is the player attacking
    public bool isBlocking = false; //is the player blocking
    public bool isDodging = false; //is the player dodging
    public bool isInteracting = false; //is the player interacting
    public bool isDead = false; //is the player dead
    public bool isAlive = true; //is the player alive
    public bool isInvestigating = false; //is the player investigating
    public bool isBusy = false; //is the player busy
    #endregion



    #region Player Construction declarations (rigidbody, character type, camera)
    public Rigidbody rb;  //rigidbody component
    public CharacterType characterType; //type of character
    public Camera playerCamera; //camera that follows the player
    #endregion



    #region Player health/stam/speed values and floats (walking speed, running speed, health, stamina, etc)
    [Header("Player Movement Settings")]
    [SerializeField] private float WalkingSpeed = 10.0f;
    [SerializeField] private float RunningSpeed = 20.0f;
    [SerializeField] private float CrouchingSpeed = 5.0f;
    [SerializeField] private float JumpForce = 10.0f;
    [SerializeField] private float PlayerHeight = 2.0f;
    [SerializeField] private float Gravity = 9.8f;
    

    [Space(25)]
    
    [Header("Player Health Settings")]
    [SerializeField] private float Health = 100.0f;
    [SerializeField] private float Energy = 100.0f;

    

    private StaminaSystem staminaSystem;
    private HealthSystem healthSystem;

    #endregion

    [Space(25)]
    [Header("Crosshair Settings")]
    [SerializeField] private Texture2D crosshairTexture;
    [SerializeField] private Rect crosshairPosition;
    [SerializeField] private bool showCrosshair = true;
    

    [Space(50)]
    [SerializeField] private PlayerControls controls;


    private void Awake() //awake is called when the script instance is being loaded
    {
        if (rb == null) //if the rigidbody is not assigned, get the rigidbody component
        {
            rb = GetComponent<Rigidbody>();
        }
        if (playerCamera == null) //if the camera is not assigned, get the main camera
        {
            playerCamera = Camera.main;
        }
        
    }

    // Execution of Update after the MonoBehaviour is created
    void Start() //start is called once before the first execution of Update after the MonoBehaviour is created
    {
        
    }

    // Update is called once per frame
    void Update() //update is called every frame
    {
        //camera
        //sprint
        //crouch
        //jump

        if(isAlive && healthSystem.CurrentHealth > 0)
        {
            healthSystem.RegenerateHealth();
        }
        
    }

    void FixedUpdate() //fixed update is called once per physics update
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, PlayerHeight / 2 + 0.1f); //this checks if the player is on the ground
        
        if(!isGrounded) //handle gravity
        {
            rb.AddForce(Vector3.down * Gravity * Time.fixedDeltaTime, ForceMode.Acceleration); //this basically adds gravity to the player, we use fixedDeltaTime to make sure the force is applied at a constant rate, and then forceMode acceleration is used to apply the force over time
        }   
        
        if(!isRunning)
        {
            staminaSystem.RegenerateStamina(); //regen stamina when you stop running
        }
    }

    #region Player Movement Methods (Idle, Jump, MoveForward, MoveBackward, MoveLeft, MoveRight, Sprint, Crouch, Interact)
    void Idle()
    {
        //should stop the player from moving
        //regains stamina
        transform.localScale = new Vector3(1, 1, 1);
    }
    void Jump()
    {
        if(isGrounded)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, JumpForce, rb.linearVelocity.z); //forcemode impulse is used to apply the force instantly
        }
    }

    void Sprint()
    {
        if(staminaSystem.CurrentStamina > 0)
        {
            rb.AddForce(playerCamera.transform.forward * RunningSpeed);
            staminaSystem.DepleteStamina();
            isRunning = true;
            
        }
        else
        {
            isRunning = false;
        }
    }
    void Crouch()
    {
        // if(isCrouching)
        // {
        //     playerCollider.height = originalHeight;
            
        //     isCrouching = false;
        // }
        // else
        // {
        //     playerCollider.height = crouchHeight;
            
        //     isCrouching = true;
        // }
        
    }
    void Interact()
    {
        //should interact with the object in front of the player
    }
    #endregion


    #region Handle Movement if statements (HandleMovement)
    void HandleMovement()
    {

        bool isMoving = false;
        Vector3 movementDirection = Vector3.zero;

        if(Input.GetKey(controls.MoveForward))
        {
            movementDirection += playerCamera.transform.forward;
            isMoving = true;
        }
        if(Input.GetKey(controls.MoveBackward))
        {
            movementDirection -= playerCamera.transform.forward;
            isMoving = true;
        }
        if(Input.GetKey(controls.MoveLeft))
        {
            movementDirection -= playerCamera.transform.right;
            isMoving = true;
        }
        if(Input.GetKey(controls.MoveRight))
        {
            movementDirection += playerCamera.transform.right;
            isMoving = true;
        }


        movementDirection.Normalize(); //this makes it so that the player moves at the same speed in all directions, diagonally too

        if(movementDirection != Vector3.zero)
        {
            rb.linearVelocity = new Vector3(movementDirection.x * WalkingSpeed, rb.linearVelocity.y, movementDirection.z * WalkingSpeed);
        }
        else
        {
            Idle();
        }


        if(Input.GetKeyDown(controls.Jump))
        {
            Jump();
            isJumping = true;
        }
        if(Input.GetKey(controls.Run))
        {
            Sprint();
            isRunning = true;
            isMoving = true;
        }

        if(Input.GetKey(controls.Crouch))
        {
            Crouch();
            isCrouching = true;
        }
        if(Input.GetKeyDown(controls.Interact))
        {
            //Interact();
            isBusy = true;
        }

        if(!isMoving)
        {
            Idle();
        }

    }
    #endregion


    #region Collapsable Menus Serializables (PlayerControls)
    [System.Serializable]
    public class PlayerControls
    {
        [Header("Player Controls")]
        public KeyCode MoveForward = KeyCode.W;
        public KeyCode MoveBackward = KeyCode.S;
        public KeyCode MoveLeft = KeyCode.A;
        public KeyCode MoveRight = KeyCode.D;
        public KeyCode Run = KeyCode.LeftShift;
        public KeyCode Jump = KeyCode.Space;
        public KeyCode Crouch = KeyCode.LeftControl;
        public KeyCode Interact = KeyCode.E;
        public KeyCode Attack = KeyCode.Mouse0;
        public KeyCode BlockorAim = KeyCode.Mouse1;
        public KeyCode Dodge = KeyCode.LeftAlt;
        public KeyCode OpenInventory = KeyCode.I;
        public KeyCode OpenMap = KeyCode.M;
        public KeyCode OpenQuests = KeyCode.L;
        public KeyCode OpenSettings = KeyCode.Escape;
        public KeyCode OpenCharacter = KeyCode.C;
    }

    #endregion

}
