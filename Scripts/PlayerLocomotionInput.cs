using UnityEngine;
using UnityEngine.InputSystem;

namespace SafriDesigner
{
    [DefaultExecutionOrder(-2)] //execution order is set to -2, meaning this script will run before all other scripts
    public class PlayerLocomotionInput : MonoBehaviour, PlayerControls.IPlayerLocomotionMapActions
    {

        public PlayerControls PlayerControls { get; private set; } //make get public and set private, meaning nobody outside of this class can set anything, but they can get the value
        public Vector2 MovementInput { get; private set; } //make get public and set private, meaning nobody outside of this class can set anything, but they can get the value
        public Vector2 LookInput { get; private set; } //make get public and set private, meaning nobody outside of this class can set anything, but they can get the value

        private void OnEnable() //onEnable is called when the object becomes enabled and active
        {
            PlayerControls = new PlayerControls();
            PlayerControls.Enable();
            PlayerControls.PlayerLocomotionMap.Enable();
            PlayerControls.PlayerLocomotionMap.SetCallbacks(this);
        }

        private void OnDisable() //onDisable is called when the object becomes disabled
        {
            PlayerControls.Disable();
            PlayerControls.PlayerLocomotionMap.Disable();
            PlayerControls.Dispose();
        }
        public void OnMovement(InputAction.CallbackContext context)
        {
            MovementInput = context.ReadValue<Vector2>();
            Debug.Log($"Moving: {MovementInput}");
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            // Implement the OnJump method here
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            LookInput = context.ReadValue<Vector2>();
            Debug.Log($"Looking: {LookInput}");
        }
    }
}
