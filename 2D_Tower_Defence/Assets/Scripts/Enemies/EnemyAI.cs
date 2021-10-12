/**
 * Enamy AI Script
 * 
 * Handles pathfinding and  health for enemy units
 * 
 * Joshua Verhoeff
 * 
 * 2021-03-01
 */

using UnityEngine;

public class EnemyAI : MonoBehaviour {
    [SerializeField] private float movementSpeed = 5f; // Enenmy movement speed
    [SerializeField] private float minDistance = 0.1f; // Mindistance to a waypoint before it will change direction
    [SerializeField] private int goldValue = 10; // The gold value for this enemy dieing
    [SerializeField] private float rotationVarient = 1f; // rotate enemy to match forward direction of sprite
    [SerializeField] GameObject explosionPrefab; // prefab for a explosion animation

    private Waypoints wPoints;
    private int wayPointIndex;
    private WaveManager waveManager;

    private void Start() {
        // On the start load the waypoints and the wave manager
        wPoints = GameObject.FindGameObjectWithTag("Waypoints").GetComponent<Waypoints>();
        waveManager = GameObject.Find("WaveManager").GetComponent<WaveManager>();
    }

    private void Update() {
        // Enemy Movement
        // Use the moveforward function to step enemy forward
        transform.position = Vector2.MoveTowards(
            transform.position,
            wPoints.getWaypoints()[wayPointIndex].position,
            movementSpeed * Time.deltaTime);

        // Enemy Rotation
        Vector3 direction = wPoints.getWaypoints()[wayPointIndex].position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + rotationVarient;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Check if at a waypoint
        if(Vector2.Distance(transform.position, wPoints.getWaypoints()[wayPointIndex].position) < minDistance) {
            if(wayPointIndex < wPoints.getWaypoints().Length - 1) { // check if at last waypoint
                wayPointIndex++;
            } else { // if at the last waypoint destory gameObject and decrement player health
                Destroy(gameObject);
                waveManager.Health--;
            }
        }
    }

    public float DistanceToGoal() {
        // function the find the distance to the next way point
        // used for enemy prioritizing by the turrets
        float distance = 0;
        distance += Vector2.Distance(
            gameObject.transform.position,
            wPoints.getWaypoints()[wayPointIndex].position);
        for(int i = wayPointIndex; i < wPoints.getWaypoints().Length - 1; i++) {
            Vector3 startPosition = wPoints.getWaypoints()[i].position;
            Vector3 endPosition = wPoints.getWaypoints()[i+1].position;
            distance += Vector2.Distance(startPosition, endPosition);
        }
        return distance;
    }

    // Get the gold value of the enemy
    public int getGoldValue() {
        return goldValue;
    }

    // Get the explosion prefab assigned to the enemy
    public GameObject getExplosion() {
        return explosionPrefab;
    }
}
