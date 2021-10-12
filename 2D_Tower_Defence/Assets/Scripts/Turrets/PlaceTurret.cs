/**
 * Place Turret Script
 * 
 * Place the selected turret prefab onto an empty tile
 * 
 * Joshua Verhoeff
 * 
 * 2021-03-02
 */

using UnityEngine;

public class PlaceTurret : MonoBehaviour {

    private static GameObject turretPrefab; // Turrets prefab to be placed

    public static GameObject TurretPrefab {
        // Getter and setter for turret prefab
        get {
            return turretPrefab;
        }
        set {
            turretPrefab = value;
        }
    }

    private GameObject turret;

    private bool CanPlaceTurret() {
        // Check if a turret is selected
        return turret == null;
    }

    private void OnMouseUp() {
        // check gold amount
        int cost = turretPrefab.GetComponent<Turret>().getCost();
        GameObject shop = GameObject.Find("Shop");
        if(CanPlaceTurret() && cost <= shop.GetComponent<Shop>().Gold) {
            // create the turret
            turret = (GameObject)Instantiate(turretPrefab, transform.position, Quaternion.identity);

            // decrement gold
            shop.GetComponent<Shop>().Gold -= cost;
        }
    }
}
