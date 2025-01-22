using UnityEngine;
using System.Collections;
using Unity.Multiplayer.Center.Common.Analytics;
using UnityEditor.Experimental.GraphView;


public class HealthSystem : MonoBehaviour
{
    public float CurrentHealth { get; private set; }

    [Header("Player Stamina Settings")]
    [SerializeField] private float maxHealth = 100.0f;
    [SerializeField] private float healthRegen = 1.0f;


    private void Start()
    {
        CurrentHealth = maxHealth;
    }

    public void RegenerateHealth()
    {
        CurrentHealth = Mathf.Min(CurrentHealth + healthRegen * Time.deltaTime, maxHealth);
    }

}