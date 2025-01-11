using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private VisualElement root;
    private static string previousScene;

    private void OnEnable()
    {
        // Haal het UIDocument op
        UIDocument uiDocument = GetComponent<UIDocument>();

        if (uiDocument == null)
        {
            Debug.LogError("UIDocument is niet gevonden op dit GameObject!");
            return;
        }

        root = uiDocument.rootVisualElement;

        // Herlaad en reset UI
        ReloadUI();

        // Verbind knoppen met functies
        BindButton("ContinueButton", OnContinueClicked);
        BindButton("NewGameButton", OnNewGameClicked);
        BindButton("LevelSelectionButton", OnLevelSelectionClicked);
        BindButton("SettingsButton", OnSettingsClicked);
        BindButton("QuitButton", OnQuitClicked);
        BindButton("MainMenuButton", OnMainMenuClicked);

        // Luister naar wanneer een scène wordt ontladen
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDisable()
    {
        // Verwijder knopbindingen
        UnbindButton("ContinueButton", OnContinueClicked);
        UnbindButton("NewGameButton", OnNewGameClicked);
        UnbindButton("LevelSelectionButton", OnLevelSelectionClicked);
        UnbindButton("SettingsButton", OnSettingsClicked);
        UnbindButton("QuitButton", OnQuitClicked);
        UnbindButton("MainMenuButton", OnMainMenuClicked);

        // Verwijder root UI-element en eventlistener
        HideAllUIElements();
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    // Functie om UI te resetten en opnieuw te laden
    private void ReloadUI()
    {
        if (root != null)
        {
            root.style.display = DisplayStyle.Flex;
            root.MarkDirtyRepaint();
        }
    }

    // Functie om alle UI-elementen te verbergen
    private void HideAllUIElements()
    {
        if (root != null)
        {
            foreach (var child in root.Children())
            {
                child.style.display = DisplayStyle.None;
            }
        }
    }

    // Algemene functie voor het binden van knoppen
    private void BindButton(string buttonName, System.Action callback)
    {
        var button = root.Q<Button>(buttonName);
        if (button != null)
        {
            button.clicked += callback;
            Debug.Log($"Button {buttonName} gebonden aan callback.");
        }
        else
        {
            Debug.LogWarning($"Button met de naam '{buttonName}' is niet gevonden in de UI!");
        }
    }

    // Algemene functie voor het unbinden van knoppen
    private void UnbindButton(string buttonName, System.Action callback)
    {
        var button = root.Q<Button>(buttonName);
        if (button != null)
        {
            button.clicked -= callback;
        }
    }

    private void OnContinueClicked()
    {
        Debug.Log("Continue game clicked!");
        // Implementatie om het spel voort te zetten
    }

    private void OnNewGameClicked()
    {
        Debug.Log("New Game clicked!");
        LoadScene("Level1");
    }

    private void OnLevelSelectionClicked()
    {
        Debug.Log("Level Selection clicked!");
        LoadScene("LevelSelection");
    }

    private void OnSettingsClicked()
    {
        Debug.Log("Settings clicked!");
        LoadScene("SettingsScene");
    }

    private void OnQuitClicked()
    {
        Debug.Log("Quit game clicked!");
        Application.Quit();
    }

    private void OnMainMenuClicked()
    {
        Debug.Log("Main Menu clicked!");
        LoadScene("MainMenu");
    }

    // Functie om een scène te laden en vorige op te slaan
    public static void LoadScene(string sceneName)
    {
        previousScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }

    // Functie om de vorige scène te laden
    public static void LoadPreviousScene()
    {
        if (!string.IsNullOrEmpty(previousScene))
        {
            SceneManager.LoadScene(previousScene);
        }
        else
        {
            Debug.LogWarning("Er is geen vorige scène opgeslagen!");
        }
    }

    // Event voor wanneer een scène wordt ontladen
    private void OnSceneUnloaded(Scene current)
    {
        HideAllUIElements(); // Zorg dat oude UI-elementen worden verborgen
    }

    // Optionele functie om de naam van de vorige scène te krijgen
    public static string GetPreviousScene()
    {
        return previousScene;
    }
}
