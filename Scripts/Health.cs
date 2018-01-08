using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Handling the health of all breakable objects
/// 
/// Created By: Kpable
/// Date Created: 06-08-17
/// 
/// </summary>
public class Health : MonoBehaviour {

    [Tooltip("Current health of the object (Will auto not exceed max health)")]
    public int currentHealth = 100;
    [Tooltip("Maximum health of the object")]
    public int maxHealth = 100;
    [Tooltip("Damage modifier of the object")]
    public float damageFactor = 1f;

    public delegate void HealthReachedZero ();
    public HealthReachedZero zeroHealthAlert;

    public void AddHealth(int additionalHealth)
    {
        currentHealth = Mathf.Clamp(currentHealth + additionalHealth, 0, maxHealth);
    }

    public void Damage(int damageAmount)
    {
        damageAmount = Mathf.RoundToInt(damageAmount * damageFactor);

        currentHealth = Mathf.Clamp(currentHealth - damageAmount, 0, currentHealth);

        if (currentHealth == 0 && zeroHealthAlert != null) zeroHealthAlert();
    }
}
