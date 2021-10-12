/**
 * Level Select Script
 * 
 * Loads the scene for the selected level
 * 
 * Joshua Verhoeff
 * 
 * 2021-03-01
 */

using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour {
    [SerializeField] private int numLevels = 0; // number of currently implemented levels

    public void nextPage() { // load the next page of levels
        Debug.Log("Load Next Page: Not Yet Implemented");
    }

    public void LoadLevel(int levelNumber) { // Loads the level 1 scene
        if (levelNumber <= numLevels) {
            string levelScene = "Level" + levelNumber.ToString(); // create scene name
            SceneManager.LoadScene(levelScene);
        } else {
            Debug.Log("Level not yet implemented");
        }
    }

    public void Back() {
        // Function the return to main menu upon back button click
        SceneManager.LoadScene("MainMenu");
    }
}
