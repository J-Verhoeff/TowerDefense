/**
 * Projectile Missile Script:
 * 
 * class to spawn missiles for missile turrets
 * 
 * Joshua Verhoeff
 * 
 * 2021-03-01
 */

using UnityEngine;

public class ProjectileMissile : MonoBehaviour {

    [SerializeField] private float moveSpeed = 60f; // Travel speed of projectile
    [SerializeField] private float destroyDistance = 1f; // distance from target where hit is registered
    [SerializeField] private int damageAmount = 25; // damage amount for projectile

    private Vector3 targetPosition;
    private GameObject target;

    public void Create(Transform pfProjectileMissile, Vector3 spawnLocation, Vector3 targetPosition, GameObject target) {
        // Creates the projectile and sets the starting and target positions
        Transform missileTransform = Instantiate(pfProjectileMissile, spawnLocation, Quaternion.identity);
        ProjectileMissile projectileMissile =  missileTransform.GetComponent<ProjectileMissile>();
        projectileMissile.Setup(targetPosition, target);
    }

    private void Setup(Vector3 targetPosition, GameObject target) {
        // Sets the target position and the enemy being targeted
        this.targetPosition = targetPosition;
        this.target = target;
    }

    private void Update() {
        // Every frame step the projectile forward towards the target and 
        //  check for collition with the enemy target
        // move projectile
        Vector3 moveDir = (targetPosition - transform.position).normalized;
        transform.position += moveDir * moveSpeed * Time.deltaTime;

        // rotate projectile to face the enemy
        float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg + 90f;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Check for collition with enemy
        if(Vector3.Distance(transform.position, targetPosition) < destroyDistance) {
            if(target != null) {
                // decrease enemy health based on damage amount
                Transform healthBarTransform = target.transform.Find("Healthbar");
                HealthBar healthBar = healthBarTransform.GetComponent<HealthBar>();
                healthBar.CurrentHealth -= Mathf.Max(damageAmount, 0);

                // Check if enemy has zero health
                if(healthBar.CurrentHealth <=0) {
                    // Add gold based on the value of the enmy to the shop
                    Shop shop = GameObject.Find("Shop").GetComponent<Shop>();
                    shop.Gold += target.GetComponent<EnemyAI>().getGoldValue();
                    // spawn an explosion at the destroyed enemies position
                    if(target.GetComponent<EnemyAI>().getExplosion() != null) {
                        Instantiate(target.GetComponent<EnemyAI>().getExplosion(),
                            targetPosition, 
                            Quaternion.identity);
                    }
                    Destroy(target);
                }
            }
            Destroy(gameObject);
        }
    }
}
