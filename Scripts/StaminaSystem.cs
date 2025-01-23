using UnityEngine;
using System.Collections;
using Unity.Multiplayer.Center.Common.Analytics;
using UnityEditor.Experimental.GraphView;


public class StaminaSystem : MonoBehaviour
{
    public float CurrentStamina { get; private set; }

    [Header("Player Stamina Settings")]
    [SerializeField] private float maxStamina = 100.0f;
    [SerializeField] private float staminaRegen = 1.0f;
    [SerializeField] private float staminaDepletion = 1.0f;


    private void Start()
    {
        CurrentStamina = maxStamina;
    }

    public void RegenerateStamina()
    {
        CurrentStamina = Mathf.Min(CurrentStamina + staminaRegen * Time.deltaTime, maxStamina);
    }

    public void DepleteStamina()
    {
        CurrentStamina = Mathf.Max(CurrentStamina - staminaDepletion * Time.deltaTime, 0);
    }
}