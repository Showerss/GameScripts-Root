using UnityEngine;
using UnityEngine.InputSystem;

namespace SafriDesigner
{
    [DefaultExecutionOrder(-1)]
    public class PlayerController : MonoBehaviour
    {

        // Components
        private CharacterController _characterController;
        private InputActions _playerControls;

        // Movement settings
        [Header("Player Movement Settings")]
        [SerializeField] private float walkingSpeed = 10f;
        [SerializeField] private float jumpForce = 10f;
        [SerializeField] private float gravity = 9.8f;

        // Input state
        private Vector2 moveInput = Vector2.zero;

        // Internal state
        private Vector3 velocity;
        private bool isGrounded;
        private bool isCrouching;
        private bool interactTriggered;

        private void Awake()
        {
            _playerControls = new InputActions();
            _characterController = GetComponent<CharacterController>();
        }

        private void OnEnable()
        {
            _playerControls.Enable();


            // Hook up input callbacks from the generated input action class
            _playerControls.Player.Move.performed += OnMove;
            _playerControls.Player.Move.canceled += OnMove;

            _playerControls.Player.Jump.performed += OnJump;
            _playerControls.Player.Crouch.performed += OnCrouch;
            _playerControls.Player.Crouch.canceled += OnCrouchCanceled;
            // _playerControls.Player.Interact.performed += OnInteract;
        }

        private void OnDisable()
        {
            _playerControls.Disable();
        }

        private void Update()
        {
            // Ground check based on CharacterController
            isGrounded = _characterController.isGrounded;
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f; // slight downward force to keep the controller grounded
            }

            // Movement (WASD)
            Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y);
            moveDirection = transform.TransformDirection(moveDirection);
            _characterController.Move(moveDirection * walkingSpeed * Time.deltaTime);

            // Apply gravity
            velocity.y -= gravity * Time.deltaTime;
            _characterController.Move(velocity * Time.deltaTime);
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            moveInput = context.ReadValue<Vector2>();
        }

        private void OnJump(InputAction.CallbackContext context)
        {
            if (isGrounded)
            {
                velocity.y = jumpForce;
            }
        }

        private void OnCrouch(InputAction.CallbackContext context)
        {
            isCrouching = true;
            // TODO: Adjust character height or visuals for crouching if needed
        }

        private void OnCrouchCanceled(InputAction.CallbackContext context)
        {
            isCrouching = false;
            // TODO: Revert crouch changes
        }

        private void OnInteract(InputAction.CallbackContext context)
        {
            interactTriggered = true;
            // TODO: Add interact logic (e.g., raycasting to an object) here
        }
    }
}