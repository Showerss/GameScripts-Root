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

    public CharacterType characterType;

    [Header("Player Movement Settings")]
    [SerializeField] private float WalkingSpeed = 10.0f;
    [SerializeField] private float RunningSpeed = 20.0f;
    [SerializeField] private float JumpForce = 10.0f;
    [SerializeField] private float PlayerHeight = 2.0f;
    [SerializeField] private float Gravity = 9.8f;

    [Space(10)]
    
    [Header("Player Health Settings")]
    [SerializeField] private float Health = 100.0f;
    [SerializeField] private float Energy = 100.0f;


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

    [SerializeField] private PlayerControls controls;




    // Execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
