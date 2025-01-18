using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LostScript : MonoBehaviour
{

    public GameObject MainMenu;
    public GameObject LevelsMenu;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        LevelsMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Restart()
    {
        MainMenu.SetActive(false);
        LevelsMenu.SetActive(true);
    }

    public void Back()
    {
        LevelsMenu.SetActive(false);
        MainMenu.SetActive(true);
    }

    public void lvl1()
    {
        SceneManager.LoadScene("Level1"); 
    }
    public void lvl2()
    {
        SceneManager.LoadScene("level2"); 
    }
    public void lvl3()
    {
        SceneManager.LoadScene("Level3"); 
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("StartMenu");   
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
