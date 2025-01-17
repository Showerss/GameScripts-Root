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

    
    private Rigidbody rb;  //rigidbody component
    public CharacterType characterType; //type of character
    public Camera playerCamera; //camera that follows the player






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


    /// <summary>
    /// Movement functions that are called in the different above gamestates
    /// </summary>



    /// <summary>
    /// Collapsable Menus Logic, playercontrols, etc...
    /// </summary>

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


}
