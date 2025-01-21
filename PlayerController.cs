using UnityEngine;
using System.Collections;
using Unity.Multiplayer.Center.Common.Analytics;

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

    [Space(25)]
    [Header("Player Stamina Settings")]
    [SerializeField] private float Stamina = 100.0f;
    [SerializeField] private float StaminaRegen = 1.0f;
    [SerializeField] private float StaminaDepletion = 1.0f;
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
    }

    void FixedUpdate() //fixed update is called once per physics update
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, PlayerHeight / 2 + 0.1f); //this checks if the player is on the ground
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
            rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse); //forcemode impulse is used to apply the force instantly
            isJumping = true;
        }
    }

    void MoveForward()
    {
        rb.AddForce(playerCamera.transform.forward * WalkingSpeed);
    }

    void MoveBackward()
    {
        rb.AddForce(-playerCamera.transform.forward * WalkingSpeed);
    }

    void MoveLeft()
    {
        rb.AddForce(-playerCamera.transform.right * WalkingSpeed);
    }

    void MoveRight()
    {
        rb.AddForce(playerCamera.transform.right * WalkingSpeed);
    }
    void Sprint()
    {
        rb.AddForce(playerCamera.transform.forward * RunningSpeed);
        isRunning = true;
        //should zoom the camera in some way as well to show the player is running
        //depletes stamina
    }
    void Crouch()
    {
        if(isCrouching)
        {
            transform.localScale = new Vector3(1, 1, 1);
            rb.AddForce(playerCamera.transform.forward * WalkingSpeed);
            isCrouching = false;
        }
        else
        {
            transform.localScale = new Vector3(1, 0.5f, 1);
            rb.AddForce(playerCamera.transform.forward * CrouchingSpeed);
            isCrouching = true;
        }
        
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

        if(Input.GetKey(controls.MoveForward))
        {
            MoveForward();
            isMoving = true;
        }
        if(Input.GetKey(controls.MoveBackward))
        {
            MoveBackward();
            isMoving = true;
        }
        if(Input.GetKey(controls.MoveLeft))
        {
            MoveLeft();
            isMoving = true;
        }
        if(Input.GetKey(controls.MoveRight))
        {
            MoveRight();
            isMoving = true;
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
        }
        if(Input.GetKeyDown(controls.Interact))
        {
            //Interact();
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
