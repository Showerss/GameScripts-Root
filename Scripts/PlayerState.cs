using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace SafriDesigner
{    
    public class PlayerState : MonoBehaviour
    {
        [field: SerializeField] public PlayerMovement CurrentPlayerMovementState { get; private set; } = PlayerMovement.Idle;
        public enum PlayerMovement
        {
            Idle = 0,
            Walking = 1,
            Running = 2,
            Jumping = 3,
            Falling = 4,
            Strafing = 5,
            Crouching = 6,

        }
    }
}
