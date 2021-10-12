/**
 * Waypoints Script:
 * 
 * Class to store the waypoints used for enemy AI
 * 
 * Joshua Verhoeff
 * 
 * 2021-03-01
 */

using UnityEngine;

public class Waypoints : MonoBehaviour {

    [SerializeField] private Transform[] waypoints;

    public Transform[] getWaypoints() {
        return waypoints;
    }
}
