using UnityEngine;
using System.Collections;

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

    #region Player Status
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



    #region Player Construction
    public Rigidbody rb;  //rigidbody component
    public CharacterType characterType; //type of character
    public Camera playerCamera; //camera that follows the player
    #endregion




    #region Player Movement
    [Header("Player Movement Settings")]
    [SerializeField] private float WalkingSpeed = 10.0f;
    [SerializeField] private float RunningSpeed = 20.0f;
    [SerializeField] private float CrouchingSpeed = 5.0f;
    [SerializeField] private float JumpForce = 10.0f;
    [SerializeField] private float PlayerHeight = 2.0f;
    [SerializeField] private float Gravity = 9.8f;
    #endregion

    [Space(25)]
    
    [Header("Player Health Settings")]
    [SerializeField] private float Health = 100.0f;
    [SerializeField] private float Energy = 100.0f;

    [Space(25)]
    [Header("Player Stamina Settings")]
    [SerializeField] private float Stamina = 100.0f;
    [SerializeField] private float StaminaRegen = 1.0f;
    [SerializeField] private float StaminaDepletion = 1.0f;


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
        
    }

    void FixedUpdate() //fixed update is called once per physics update
    {

    }


    #region Player Movement Methods

    void Jump()
    {
        rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
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

    #endregion



    #region Collapsable Menus

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
