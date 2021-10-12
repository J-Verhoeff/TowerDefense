/**
 * Turret Button Script
 * 
 * Class to store prefab for the selected button
 * 
 * Joshua Verhoeff
 * 
 * 2021-03-04
 */

using UnityEngine;

public class TurretButton : MonoBehaviour {

    [SerializeField] private GameObject turretPrefab;

    public GameObject getPrefab() {
        return turretPrefab;
    }
}
