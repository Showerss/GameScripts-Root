using UnityEngine;

public class EnemyData : MonoBehaviour
{

    public enum EnemyType
    {
        Melee,
        Ranged,
        Flying,
        Boss,
        MiniBoss,
        Rare

    }

    public enum CharacterStatus
    {
        Idle,
        Walking,
        Running,
        Attacking,
        Dead, 
        Alive, 
        Investigating,
        Busy
    }

    [Space(30)]
    [Header("Enemy Movement Settings")]
    [SerializeField] private float WalkingSpeed = 10.0f;

    [Space(30)]
    [Header("Enemy Health Settings")]
    [SerializeField] private float Health = 100.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
