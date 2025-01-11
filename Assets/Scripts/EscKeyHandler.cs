// using UnityEngine;
// using UnityEngine.SceneManagement; // Voor scene management
// using UnityEngine.UI; // Voor UI elementen

// public class MenuController : MonoBehaviour
// {
//     public GameObject mainMenu; // Het menu dat je wilt tonen (je kunt een public GameObject gebruiken om het menu in te stellen)
//     public GameObject pauseMenu; // Het pauzemenu
//     private bool isPaused = false; // Of het spel gepauzeerd is

//     void Update()
//     {
//         // Controleer of de Escape-toets is ingedrukt
//         if (Input.GetKeyDown(KeyCode.Escape))
//         {
//             // Als het spel gepauzeerd is, ga terug naar het hoofdmenu
//             if (isPaused)
//             {
//                 GoToMainMenu();
//             }
//             else
//             {
//                 // Pauzeer het spel en toon het pauzemenu
//                 PauseGame();
//             }
//         }
//     }

//     public void PauseGame()
//     {
//         Time.timeScale = 0; // Zet de tijd stil, zodat het spel pauzeert
//         pauseMenu.SetActive(true); // Zet het pauzemenu aan
//         isPaused = true; // Markeer het spel als gepauzeerd
//     }
// }
