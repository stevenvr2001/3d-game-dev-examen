using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{

    public GameObject PauseMenuLabel;

    public bool isPaused;
    // Start is called before the first frame update
    void Start()
    {
        PauseMenuLabel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;       
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused){
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        PauseMenuLabel.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isPaused = true;
    }

    public void ResumeGame()
    {
        PauseMenuLabel.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isPaused = false;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartMenu");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isPaused = false;        
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
