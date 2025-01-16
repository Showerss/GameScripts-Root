using UnityEngine;

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

    // Execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
