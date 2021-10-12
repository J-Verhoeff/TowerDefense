/**
 * Wave Script:
 * 
 * Class to store wave information
 * 
 * Joshua Verhoeff
 * 
 * 2021-03-05
 */

using UnityEngine;

public class Wave : MonoBehaviour {
    [SerializeField] private GameObject[] enemyPrefabs; // prefabs for enemy units
    [SerializeField] private float spawnInterval = 2; // time between spawns in seconds
    [SerializeField] private int maxEnemies = 20; // max number enmies in the wave

    public GameObject[] getEnemyPrefabs() {
        return enemyPrefabs;
    }

    public float getSpawnInterval() {
        return spawnInterval;
    }

    public int getMaxEnemies() {
        return maxEnemies;
    }
}
