using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Collections;
using Unity.Multiplayer.Center.Common.Analytics;
using UnityEditor.Experimental.GraphView;
using UnityEngine.Rendering;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine.EventSystems;

namespace SafriDesigner
{
    [DefaultExecutionOrder(-1)] //execution order is set to -1, meaning this script will run before all other scripts
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


        #region Player Construction declarations (rigidbody, character type, camera)
        private CharacterType _characterType; //type of character
        private PlayerLocomotionInput _playerLocomotionInput;
        
        
        private StaminaSystem staminaSystem;
        private HealthSystem healthSystem;
        
        
        private Vector3 _velocity; //veclcity will be stored as a vector3 (x, y, z)

        #endregion


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
        [SerializeField] private CharacterController _characterController;


        #region Player health/stam/speed values and floats (walking speed, running speed, health, stamina, etc)
        [Header("Player Movement Settings")]
        [SerializeField] public float walkingSpeed = 10.0f;
        [SerializeField] public float runSpeed = 20.0f;
        [SerializeField] private float crouchingSpeed = 5.0f;
        [SerializeField] private float jumpForce = 10.0f;
        [SerializeField] private float PlayerHeight = 2.0f;
        [SerializeField] private float Gravity = 9.8f;
        

        [Space(25)]
        
        [Header("Player Health Settings")]
        [SerializeField] private float Health = 100.0f;
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
        // private Vector2 _cameraRotation = Vector2.zero;
        // private Vector2 _playerTargetRotation;
        


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
            

            //assign locomotion input
            _playerLocomotionInput = GetComponent<PlayerLocomotionInput>();

            //assign character controller
            _characterController = GetComponent<CharacterController>();

            //if the camera is not assigned, get the main camera
            if (_playerCamera == null) 
            {
                _playerCamera = Camera.main;
            }   


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
        }

        // Update is called once per frame
        void Update() //update is called every frame, use it sparingly for game logic, 
        /// WARNING: offload calculations to outside methods or coroutines
        /// polling inputs and responding in real time, 
        /// handling gameplay systems that need per-frame updates like timers and character AI 
        /// animate or move objects that arent physics based
        {

            isGrounded = _characterController.isGrounded; //isGrounded

            ApplyGravity(); //apply gravity to the player

            //calculations of movement input
            Vector3 cameraForwardXZ = new Vector3(_playerCamera.transform.forward.x, 0f, _playerCamera.transform.forward.z).normalized; 
            Vector3 cameraRightXZ = new Vector3(_playerCamera.transform.right.x, 0f, _playerCamera.transform.right.z).normalized; 

            Vector3 moveDirection = cameraRightXZ * _playerLocomotionInput.MovementInput.x + cameraForwardXZ * _playerLocomotionInput.MovementInput.y; //Vector3 moveDirection

            //normalize the movement in order to keep the player from moving faster diagonally            
            moveDirection = moveDirection.normalized;

            //move the player
            Vector3 move = moveDirection * walkingSpeed;
            _velocity.y += Gravity * Time.deltaTime; //apply gravity to the player
            move.y = _velocity.y; //move the player in the y direction
            // Vector3 movementDelta = moveDirection * runSpeed * Time.deltaTime; //Vector3 movementDelta
            // Vector3 newVelocity = _characterController.velocity + movementDelta; //Vector3 newVelocity

            //only call this once per frame
            _characterController.Move(move * Time.deltaTime); //Move the character controller

            // if(playerHealth <= 0) GameOver();

            // abilityCooldown -= Time.deltaTime;

            // if(isAlive && healthSystem.CurrentHealth > 0)
            // {
            //     healthSystem.RegenerateHealth();
            // }
            
        }

        private void LateUpdate() //always do the camera logic AFTER movement logic, this should be primarily physics logic
        // camera positioning or smoothing, post processing effects, animation syncing
        {
                //i want to set a look limit for the camera so that the player cannot look up or down past a certain point
                //this is done by clamping the y rotation of the camera
            _cameraRotation.x += cameraSensitivityH * _playerLocomotionInput.LookInput.x;
            _cameraRotation.y = Mathf.Clamp(_cameraRotation.y - cameraSensitivityV * _playerLocomotionInput.LookInput.y, -lookLimitV, lookLimitV);

            _playerTargetRotation.x += transform.eulerAngles.x + cameraSensitivityH * _playerLocomotionInput.LookInput.x;
            transform.rotation = Quaternion.Euler(0f, _playerTargetRotation.x, 0f);

            _playerCamera.transform.rotation = Quaternion.Euler(_cameraRotation.y, _cameraRotation.x, 0f);
        }

        void FixedUpdate() //fixed update is called once per physics update
        {
            
            
            // if(!isRunning && isAlive) //if the player is not running and is alive
            // {
            //     staminaSystem.RegenerateStamina(); //regen stamina when you stop running
            // }
        }

        private4 IEnumerator EnemySpawnRoutine()
        {
            //spawn an enemy
            //wait for a few seconds
            //spawn another enemy
            //repeat
            yield return null;
        }

        #region Player Movement Methods (Idle, Jump, MoveForward, MoveBackward, MoveLeft, MoveRight, Sprint, Crouch, Interact)
        void Idle()
        {
            //should stop the player from moving
            //regains stamina
            // transform.localScale = new Vector3(1, 1, 1);
        }
        void Jump()
        {
            
        }

        void Sprint()
        {
            
        }
        void Crouch()
        {
            
        }
        void Interact()
        {
            //should interact with the object in front of the player
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
        #endregion
    }
}