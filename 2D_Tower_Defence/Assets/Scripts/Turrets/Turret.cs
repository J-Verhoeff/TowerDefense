/**
 * Turret Script:
 * 
 * Script to handle turret placement and AI
 * 
 * Joshua Verhoeff
 * 
 * 2021-03-01
 */

using UnityEngine;
using System.Collections.Generic;

public class Turret : MonoBehaviour {

    [SerializeField] private float shootTimeMAX = 0.05f; // Fire rate of the turret
    [SerializeField] private int cost = 100; // Gold cost of the turret
    [SerializeField] private GameObject missilePrefab; // What projectile the turret fires

    private float shootTimer;
    private Vector3 projectileShootFromPosition; // Where to spawn the projectiles
    private List<GameObject> enemiesInRange; // List of all enemies in range

    private void Awake() {
        // On awake get needed game objects and start shoot timer for fire rate
        projectileShootFromPosition = transform.Find("turret").Find("ProjectileShootFromPosition").position;
        enemiesInRange = new List<GameObject>();
        shootTimer = shootTimeMAX;
    }

    void OnEnemyDestroy(GameObject enemy) {
        // Remove an enemy from the list if destoyed
        enemiesInRange.Remove(enemy);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        // Detect when enemy enter turret range using collider
        enemiesInRange.Add(collision.gameObject);
        EnemyDestructionDelegate del = collision.gameObject.GetComponent<EnemyDestructionDelegate>();
        del.enemyDelegate += OnEnemyDestroy;
    }

    private void OnTriggerExit2D(Collider2D collision) {
        // If an enemy exits the turret range, then remove from enemies in range list
        if (collision.gameObject.tag.Equals("Enemies")) {
            enemiesInRange.Remove(collision.gameObject);
            EnemyDestructionDelegate del = collision.gameObject.GetComponent<EnemyDestructionDelegate>();
            del.enemyDelegate -= OnEnemyDestroy;
        }
    }

    private void Shoot(Collider2D target) {
        // Shoot a projectile towards the enemy
        ProjectileMissile missile = missilePrefab.GetComponent<ProjectileMissile>();
        missile.Create(missilePrefab.transform, projectileShootFromPosition, target.transform.position, target.gameObject);
    }

    private void Update() {
        // Every fram check for enemies to shoot at
        GameObject target = null;
        // Check for which enemy is closest to the goal
        float minimalEnemyDistance = float.MaxValue;
        foreach(GameObject enemy in enemiesInRange) {
            float distanceToGoal = enemy.GetComponent<EnemyAI>().DistanceToGoal();
            if(distanceToGoal < minimalEnemyDistance) {
                target = enemy;
                minimalEnemyDistance = distanceToGoal;
            }
        }
        if (target != null) {
            // simple shoot timer
            shootTimer -= Time.deltaTime;
            if (shootTimer <= 0f) {
                shootTimer = shootTimeMAX;
                Shoot(target.GetComponent<Collider2D>());
            }
            // rotate turret towards target
            Vector3 dir = gameObject.transform.position - target.transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90f;
            transform.Find("turret").rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    public int getCost() {
        // Get the cost of the turret
        return cost;
    }
}
