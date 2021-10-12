/**
 * Pause Menu Script
 * 
 * Script to handle pausing and game end
 * 
 * Joshua Verhoeff
 * 
 * 2021-03-07
 */
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenu : MonoBehaviour {
    private GameObject[] pauseObjects; // list of objects in pause window
    private GameObject gameOverText; // Game over text
    private GameObject playButton; // Play button game object
    private GameObject resartButton; // resart button game object

    public void showPaused() {
        // Show all pause menu objects
        foreach(GameObject paused in pauseObjects) {
            paused.SetActive(true);
        }
    }

    public void hidePaused() {
        // Hide all pause menu obkects
        foreach (GameObject paused in pauseObjects) {
            paused.SetActive(false);
        }
    }

    public void pauseControl() {
        // Pause the game by setting timescale to 0 or
        //  unpause by setting timescale to 1
        if(Time.timeScale == 1) {
            Time.timeScale = 0;
            showPaused();
        } else if(Time.timeScale == 0) {
            Time.timeScale = 1;
            hidePaused();
        }
    }

    private void Start() {
        // On start up get references to needed game objects and 
        //  Start the game in paused state
        Time.timeScale = 0;
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        gameOverText = GameObject.Find("GameOverText");
        gameOverText.SetActive(false);
        playButton = GameObject.Find("PlayButton");
        resartButton = GameObject.Find("RestartButton");
        resartButton.SetActive(false); // hide restart button
        showPaused();
    }

    private void Update() {
        // Every fram check if p button is pressed
        //  Used to pause and unpause the game
        if(Input.GetKeyDown(KeyCode.P)) {
            pauseControl();
        }
    }

    public void GameWon() {
        pauseControl();
        gameOverText.SetActive(true);
        gameOverText.GetComponent<TextMeshProUGUI>().text = "You Won!";
        playButton.SetActive(false);
        resartButton.SetActive(true);
    }

    public void GameOver() {
        // Pause game and set game over conditions
        pauseControl();
        gameOverText.SetActive(true);
        playButton.SetActive(false);
        resartButton.SetActive(true);
    }

    public void LoadLevel() {
        // UnPause game when start button is clicked
        pauseControl();
    }

    public void RestartLevel() {
        // Restart the scene if the restart button is clicked
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void MainMenu() {
        // Return to main menu if menu button is clicked
        SceneManager.LoadScene("MainMenu");
    }
}
