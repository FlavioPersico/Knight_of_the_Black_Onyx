using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public int currentHealth;
    public UnityEvent<int> OnHealthChange;

    public Health(int newHealth)
    {
        currentHealth = newHealth;
        OnHealthChange = new UnityEvent<int>();
    }

    public void TakeDamage(int damage)
    {
		currentHealth -= damage;
		Debug.Log("Damage taken remain life: " + currentHealth);
		OnHealthChange.Invoke(currentHealth);
	}

    public void RecoverHealth(int healthRecover)
    {
        currentHealth += healthRecover;
    }
}
