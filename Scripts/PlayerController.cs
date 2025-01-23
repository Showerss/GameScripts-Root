using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Collections;
using Unity.Multiplayer.Center.Common.Analytics;
using UnityEditor.Experimental.GraphView;

namespace SafriDesigner
{
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
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Camera _playerCamera;
        private StaminaSystem staminaSystem;
        private HealthSystem healthSystem;

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
        [SerializeField] private PlayerControls controls;
        


        private void Awake() //awake is called when the script instance is being loaded
        {
            if (_playerCamera == null) //if the camera is not assigned, get the main camera
            {
                _playerCamera = Camera.main;
            }   
        }

        void Start() //start is called once before the first execution of Update after the MonoBehaviour is created
        {
            
        }

        // Update is called once per frame
        void Update() //update is called every frame
        {

            Vector3 cameraForwardXZ = new Vector3(_playerCamera.transform.forward.x, 0f, _playerCamera.transform.forward.z).normalized; //Vector3 cameraForwardXZ
            Vector3 cameraRightXZ = new Vector3(_playerCamera.transform.right.x, 0f, _playerCamera.transform.right.z).normalized; //Vector3 cameraRightXZ
            Vector3 moveDirection = cameraRightXZ * _playerLocomotionInput.MovementInput.x + cameraForwardXZ * _playerLocomotionInput.MovementInput.y; //Vector3 moveDirection
            Vector3 movementDelta = moveDirection * walkingSpeed * Time.deltaTime; //Vector3 movementDelta
            Vector3 newVelocity = _characterController.velocity + movementDelta; //Vector3 newVelocity

            //only call this once per frame
            _characterController.Move(newVelocity * Time.deltaTime); //Move the character controller


            if(isAlive && healthSystem.CurrentHealth > 0)
            {
                healthSystem.RegenerateHealth();
            }
            
        }

        void FixedUpdate() //fixed update is called once per physics update
        {
            isGrounded = Physics.Raycast(transform.position, Vector3.down, PlayerHeight / 2 + 0.1f); //this checks if the player is on the ground
            
            
            
            if(!isRunning && isAlive) //if the player is not running and is alive
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
        #endregion


        #region Collapsable Menus Serializables (PlayerControls)
        [System.Serializable]
        public class PlayerControls
        {
            [Header("Player Controls")]
            // public KeyCode MoveForward = KeyCode.W;
            // public KeyCode MoveBackward = KeyCode.S;
            // public KeyCode MoveLeft = KeyCode.A;
            // public KeyCode MoveRight = KeyCode.D;
            public KeyCode Sprint = KeyCode.LeftShift;
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
}