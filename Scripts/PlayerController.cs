using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;


namespace SafriDesigner
{
    [DefaultExecutionOrder(-1)] //execution order is set to -1, meaning this script will run before all other scripts
    public class PlayerController : MonoBehaviour
    {

        //components 
        private PlayerControls _playerControls;
        private CharacterController _characterController;
        private Vector2 inputMove;
        private Vector2 inputLook;
        private float yRotation = 0f;


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



        [Space(25)] 
        [Header("Player Controller")]


        #region Player health/stam/speed values and floats (walking speed, running speed, health, stamina, etc)
        [Header("Player Movement Settings")]
        [SerializeField] public float walkingSpeed = 10.0f;
        [SerializeField] public float runSpeed = 20.0f;
        [SerializeField] private float crouchingSpeed = 5.0f;
        [SerializeField] private float jumpForce = 10.0f;
        [SerializeField] private float PlayerHeight = 2.0f;
        [SerializeField] private float Gravity = 9.8f;
        private Vector3 _movementInput;
        private Vector3 _velocity; 
        private PlayerLocomotionInput _playerLocomotionInput;
        

        [Space(25)]
        
        [Header("Player Health Settings")]
        private HealthSystem healthSystem;
        [SerializeField] private float Health = 100.0f;


        [Space(25)]
        [Header("Player Stamina Settings")]
        private StaminaSystem staminaSystem;
        [SerializeField] private float Energy = 100.0f;


        
        #endregion

        [Space(25)]
        [Header("Crosshair Settings")]
        [SerializeField] private Texture2D crosshairTexture;
        [SerializeField] private Rect crosshairPosition;
        [SerializeField] private bool showCrosshair = true;
        

        [Space(50)]

        [Header("Camera Controls")]
        [SerializeField] private Camera _playerCamera;
        [SerializeField] private float cameraSensitivityH = 2.0f;
        [SerializeField] private float cameraSensitivityV = 2.0f;
        [SerializeField] private float cameraSmoothing = 2.0f;
        [SerializeField] private float cameraMaxVerticalAngle = 80.0f;
        


        private void Awake() //awake is called when the script instance is being loaded
        {

            /// <summary>
            /// awake is called when the script instance is being loaded, it is the first method called, so typically within awake we need to write code that sets up references between scripts, and initializes variables
            /// checklist of things to make sure are in awake:
            /// 1. assign locomotion input
            /// 2. assign character controller
            /// 3. assign camera
            /// 4. assign health system
            /// 5. assign stamina system
            /// setup dependencies, initialize variables or states and assign getcomponent references
            /// </summary>
            

            _playerControls = new PlayerControls();
            _characterController = GetComponent<CharacterController>();



        }

        void OnEnable()
        {
            
        }

        void Start() //start is called once before the first execution of Update after the MonoBehaviour is created, use it for caching references, setting up variables, etc
        {
            /// typically within start you will always find a few things happening... 
            /// 1. setting up the player's health system
            /// 2. setting up the player's stamina system
            /// 3. setting up the player's crosshair
            /// 4. setting up the player's camera rotation
            /// 5. setting up the player's camera position
            /// start up the gameplay systems like health, inventory and abilities
            /// initialize anything that depends on other gameobjects
            /// trigger the first gameplay related actions like spawning and starting coroutiines
            
            // healthSystem.Initialize(Health);
            // staminaSystem.Initialize(Energy);
            // StartCoroutine(EnemySpawnRoutine());
            // _playerCamera.Lookat(transform);
        }

        // Update is called once per frame
        void Update() //update is called every frame, use it sparingly for game logic, 
        /// WARNING: offload calculations to outside methods or coroutines
        /// polling inputs and responding in real time, 
        /// handling gameplay systems that need per-frame updates like timers and character AI 
        /// animate or move objects that arent physics based
        {

            //check if the player is grounded
            GroundCheck(); 

            // Apply gravity
            ApplyGravity();

            // Handle movement
            MovePlayer();


            // RegenerateHealth(); //regenerate health
            // RegenerateStamina(); //regenerate stamina
            // if(playerHealth <= 0) GameOver();

            // abilityCooldown -= Time.deltaTime; 

        }


        void FixedUpdate() //fixed update is called once per physics update
        {
            
        }

        
        private void LateUpdate() //always do the camera logic AFTER movement logic, this should be primarily physics logic
        // camera positioning or smoothing, post processing effects, animation syncing
        {
            HandleLookAtMouse();
        }


        private IEnumerator EnemySpawnRoutine()
        {
            //spawn an enemy
            //wait for a few seconds
            //spawn another enemy
            //repeat
            yield return null;
        }





















        #region Player Movement and Camera methods

        void OnJump(InputValue value)
        {
            if(value.isPressed && isGrounded)
            {
                _velocity.y = Mathf.Sqrt(jumpForce * -2f * Gravity);
            }
        }

        void OnMove(InputValue value)
        {
            inputMove = value.Get<Vector2>();
        }

        void OnLook(InputValue value)
        {
            inputLook = value.Get<Vector2>();
        }

        void ApplyGravity()
        {
            //reset vertical velocity if the player is grounded
            if(isGrounded && _velocity.y < 0)
            {
                _velocity.y = -2f; //this is a small downward force to the y velocity to keep the player grounded (couldnt i just do = gravity? the gravity that i serialized?)
            }
            else
            {
                _velocity.y -= Gravity * Time.deltaTime; //apply gravity to the player
            }
        }

        void GroundCheck()
        {
            isGrounded = _characterController.isGrounded;
            if(isGrounded && _velocity.y < 0)
            {
                _velocity.y = -2f;
            }
        }

        void MovePlayer()
        {
            Vector3 moveDirection = new Vector3(inputMove.x, 0, inputMove.y);
            moveDirection = transform.TransformDirection(moveDirection);

            _characterController.Move(moveDirection * walkingSpeed * Time.deltaTime);

            _velocity.y += Gravity * Time.deltaTime;
            _characterController.Move(_velocity * Time.deltaTime);
        }

        // private void RegenerateHealth()
        // {
        //     if (isAlive && healthSystem.CurrentHealth > 0)
        //     {
        //         healthSystem.RegenerateHealth();
        //     }
        // }

        // private void RegenerateStamina()
        // {
        //     if (!isRunning && isAlive) //if the player is not running and is alive
        //     {
        //         staminaSystem.RegenerateStamina(); //regen stamina when you stop running
        //     }
        // }


        void HandleLookAtMouse()
        {
            transform.Rotate(Vector3.up * inputLook.x * cameraSensitivityH);

        }

        // void HandleMovement()
        // {
        //     // Convert input into movement direction
        //     Vector3 moveDirection = new Vector3(_movementInput.x, 0, _movementInput.z);
            
        //     // Normalize movement and apply speed (prevents diagonal movement speed issues)
        //     if (moveDirection.magnitude > 0.1f)
        //     {
        //         moveDirection = moveDirection.normalized * walkingSpeed;
        //     }

        //     // Apply gravity
        //     moveDirection.y = _velocity.y;

        //     // Move the player
        //     _characterController.Move(moveDirection * Time.deltaTime);
        // }
        #endregion
    }
}