/**
 * Shop Script:
 * 
 * Script to handle money and turret selection from shop buttons
 * 
 */
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Shop : MonoBehaviour {

    [SerializeField] TextMeshProUGUI goldLabel; // The display for the gold value
    [SerializeField] int startingGold = 200; // Amount of gold player starts with

    private GameObject[] buttons;

    // Setter and getter functions for the gold value
    private int gold;
    public int Gold {
        get {
            return gold;
        }
        set {
            gold = value;
            goldLabel.GetComponent<TextMeshProUGUI>().text = "Gold: " + gold;
        }
    }

    private void Awake() {
        // On awake get the shop buttons and set starting gold
        buttons = GameObject.FindGameObjectsWithTag("TurretButtons");
        gold = startingGold;
    }

    // Select the turret based on the button click
    public void SelectTurret() {
        foreach(GameObject button in buttons) {
            if(EventSystem.current.currentSelectedGameObject == button) {
                // Use event system to get turret prefab stored in the shop button
                PlaceTurret.TurretPrefab = button.GetComponent<TurretButton>().getPrefab();
                break;
            }
        }
    }
}
