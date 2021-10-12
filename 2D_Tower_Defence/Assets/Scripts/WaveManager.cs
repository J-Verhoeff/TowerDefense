/**
 * Wave Mangement Script
 * 
 * Script to handle wave spawning, and time between rounds
 * 
 * Joshua Verhoeff
 */

using UnityEngine;
using TMPro;

public class WaveManager : MonoBehaviour {

    [Header("Health")]
    [SerializeField] private TextMeshProUGUI healthLabel; // Display for player health
    [SerializeField] private int startingHealth = 10; // Starting player health

    [Header("Waves")]
    [SerializeField] private TextMeshProUGUI waveLabel; // Display for wave number
    [SerializeField] private int timeBetweenWaves = 5; // Interval between waves
    [SerializeField] private Transform spawnLocation; // Spawn location of enemies

    private float lastSpawnTime;
    private int enemiesSpawned = 0;
    private GameObject[] waves;

    // Get and set the player health
    private int health;
    public int Health {
        get {
            return health;
        }
        set {
            health = value;
            healthLabel.GetComponent<TextMeshProUGUI>().text = "Health: " + health;

            // If health is less than 0, end the game
            if(health <= 0 && !GameOver) {
                GameObject pause = GameObject.Find("GameOverScreen");
                pause.GetComponent<PauseMenu>().GameOver();
                GameOver = true;
            }
        }
    }

    // get and set the wave number
    private int wave = 0;
    public int Wave {
        get {
            return wave;
        }
        set {
            wave = value;
            waveLabel.GetComponent<TextMeshProUGUI>().text = "Wave: " + (wave+1);
        }
    }

    // get and set the game over bool value
    private bool gameOver;
    public bool GameOver {
        get {
            return gameOver;
        }
        set {
            gameOver = value;
        }
    }

    private void Start() {
        // On start load needed game objects and set starting health
        lastSpawnTime = Time.time;
        waves = GameObject.FindGameObjectsWithTag("Waves");
        Health = startingHealth;
    }

    private void Update() {
        // Spawn enemies for the current wave
        if(wave < waves.Length) {
            float timeInterval = Time.time - lastSpawnTime;
            float spawnInterval = waves[Wave].GetComponent<Wave>().getSpawnInterval();

            if(((enemiesSpawned == 0 && timeInterval > timeBetweenWaves) ||
                timeInterval > spawnInterval) && enemiesSpawned < waves[Wave].GetComponent<Wave>().getMaxEnemies()) {

                lastSpawnTime = Time.time;
                // if multiple enemy prefabs randomly spawn 1
                int numPrefabs = waves[Wave].GetComponent<Wave>().getEnemyPrefabs().Length;
                int choice = Random.Range(0, numPrefabs);
                GameObject newEnemy = waves[Wave].GetComponent<Wave>().getEnemyPrefabs()[choice];
                Instantiate(newEnemy, spawnLocation.position, Quaternion.identity);
                enemiesSpawned++;
            }

            // Move to next wave
            if(enemiesSpawned == waves[Wave].GetComponent<Wave>().getMaxEnemies() &&
                GameObject.FindGameObjectWithTag("Enemies") == null) {
                Wave++;
                enemiesSpawned = 0;
                lastSpawnTime = Time.time;
                // remove all explosion animations
                GameObject[] explosions = GameObject.FindGameObjectsWithTag("Explosion");
                foreach(GameObject explosion in explosions) {
                    Destroy(explosion);
                }
            }
        }
        else {
            // Set winning conditions
            GameObject pause = GameObject.Find("GameOverScreen");
            pause.GetComponent<PauseMenu>().GameWon();
            GameOver = true;
        }
    }
}
