/**
 * Health Bar Script
 * 
 * Script to adjust the health bar based on the enemies missing health
 * 
 * Joshua Verhoeff
 * 
 * 2021-03-05
 */

using UnityEngine;

public class HealthBar : MonoBehaviour {

    [SerializeField] private float maxHealth = 100f; // Set the max health of an enemy
    [SerializeField] private float currentHealth = 100f; // stores the current health
   
    public float CurrentHealth {
        // Getter and setter for the current health
        get {
            return currentHealth;
        }
        set {
            currentHealth = value;
        }
    }

    private float originalScale;

    private void Start() {
        // On start store the original size of the health bar
        originalScale = gameObject.transform.localScale.x;
    }

    private void Update() {
        // Each frame adjust the length of the health bar based on missing health
        Vector3 tempScale = gameObject.transform.localScale;
        tempScale.x = currentHealth / maxHealth * originalScale;
        gameObject.transform.localScale = tempScale;
    }
}
