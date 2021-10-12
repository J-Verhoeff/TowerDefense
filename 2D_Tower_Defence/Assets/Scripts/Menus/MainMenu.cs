/**
 * Main Menu Script
 * 
 * Handles on click events for main menu items
 * 
 * Joshua Verhoeff
 * 
 * 2021-03-01
 */

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MainMenu : MonoBehaviour {
    public void Play() { // Loads the level select scene
        SceneManager.LoadScene("LevelSelect");
    }

    public void Instructions() { // Loads the instructions page
        SceneManager.LoadScene("HowToPlay");
    }

    public void Exit() { // Exits the game
        EditorApplication.isPlaying = false;
        Application.Quit();
    }

    public void BackToMenu() { // return to main menu from instructions page
        SceneManager.LoadScene("MainMenu");
    }
}
