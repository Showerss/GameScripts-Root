using UnityEngine;

namespace SafriDesigner
{
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _animator = null;

        private PlayerLocomotionInput _playerLocomotionInput;
        private static int inputXHash = Animator.StringToHash("InputX");
        private static int inputYHash = Animator.StringToHash("InputY");   


        private void Awake()
        {
            _playerLocomotionInput = GetComponent<PlayerLocomotionInput>();
        }

        private void Update()
        {
            UpdateAnimationState();
        }





        void UpdateAnimationState()
        {
            Vector2 inputTarget = _playerLocomotionInput.MovementInput;
            _animator.SetFloat(inputXHash, inputTarget.x);
            _animator.SetFloat(inputYHash, inputTarget.y);
        }
    }
}