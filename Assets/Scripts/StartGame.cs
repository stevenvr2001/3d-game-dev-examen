using UnityEngine;
using UnityEngine.SceneManagement; // Required for loading scenes
using UnityEngine.UI; // Required for UI components

public class StartMenu : MonoBehaviour
{
    // Function to start the game when Play is clicked
    public void StartGame()
    {
        // Load the next scene (you should have a scene for the game or level)
        SceneManager.LoadScene(1); // Change "GameScene" to the name of your game scene
    }

    // Function to quit the game when Quit is clicked
    public void QuitGame()
    {
        // Check if running in the Unity editor or in a built game
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // Stop the game in the editor
        #else
            Application.Quit(); // Quit the game in a build
        #endif
    }
}
