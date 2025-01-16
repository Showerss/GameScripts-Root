using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public enum CharacterType
    {
        NPC,
        Enemy,
        Player
    }

    public CharacterType characterType;

    [SerializeField] private float WalkingSpeed = 10.0f;
    [SerializeField] private float RunningSpeed = 20.0f;
    [SerializeField] private float JumpForce = 10.0f;
    [SerializeField] private float PlayerHeight = 2.0f;
    [SerializeField] private float Gravity = 9.8f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
