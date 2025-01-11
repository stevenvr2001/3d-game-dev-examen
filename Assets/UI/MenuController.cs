// using UnityEngine;
// using UnityEngine.UIElements;
// using UnityEngine.SceneManagement;

// public class MenuController : MonoBehaviour
// {
//     private VisualElement root;

//     private void OnEnable()
//     {
//         // Haal het UIDocument-component op
//         UIDocument uiDocument = GetComponent<UIDocument>();

//         // Controleer of het UIDocument bestaat
//         if (uiDocument == null)
//         {
//             Debug.LogError("UIDocument is niet gevonden op dit GameObject!");
//             return;
//         }

//         // Haal de rootVisualElement op
//         root = uiDocument.rootVisualElement;

//         // Verbind knoppen met functies
//         BindButton("ContinueButton", OnContinueClicked);
//         BindButton("NewGameButton", OnNewGameClicked);
//         BindButton("LevelSelectionButton", OnLevelSelectionClicked);
//         BindButton("SettingsButton", OnSettingsClicked);
//         BindButton("QuitButton", OnQuitClicked);
//         BindButton("MainMenuButton", OnMainMenuClicked);
//     }

//     private void OnDisable()
//     {
//         // Verwijder eventlisteners (veiligheidshalve)
//         UnbindButton("ContinueButton", OnContinueClicked);
//         UnbindButton("NewGameButton", OnNewGameClicked);
//         UnbindButton("LevelSelectionButton", OnLevelSelectionClicked);
//         UnbindButton("SettingsButton", OnSettingsClicked);
//         UnbindButton("QuitButton", OnQuitClicked);
//         UnbindButton("MainMenuButton", OnMainMenuClicked);
//     }

//     // Algemene functie voor het binden van knoppen
//     private void BindButton(string buttonName, System.Action callback)
//     {
//         var button = root.Q<Button>(buttonName);
//         if (button != null)
//         {
//             button.clicked += callback;
//         }
//         else
//         {
//             Debug.LogWarning($"Button met de naam '{buttonName}' is niet gevonden in de UI!");
//         }
//     }

//     // Algemene functie voor het unbinden van knoppen
//     private void UnbindButton(string buttonName, System.Action callback)
//     {
//         var button = root.Q<Button>(buttonName);
//         if (button != null)
//         {
//             button.clicked -= callback;
//         }
//     }

//     private void OnContinueClicked()
//     {
//         Debug.Log("Continue game clicked!");
//         // Implementatie om het spel voort te zetten
//     }

//     private void OnNewGameClicked()
//     {
//         Debug.Log("New Game clicked!");
//         SceneManager.LoadScene("Level1");
//     }

//     private void OnLevelSelectionClicked()
//     {
//         Debug.Log("Level Selection clicked!");
//         SceneManager.LoadScene("LevelSelection");
//     }

//     private void OnSettingsClicked()
//     {
//         Debug.Log("Settings clicked!");
//         SceneManager.LoadScene("SettingsScene");
//     }

//     private void OnQuitClicked()
//     {
//         Debug.Log("Quit game clicked!");
//         Application.Quit();
//     }

//     private void OnMainMenuClicked()
//     {
//         Debug.Log("Main Menu clicked!");
//         SceneManager.LoadScene("MainMenu");
//     }
// }
