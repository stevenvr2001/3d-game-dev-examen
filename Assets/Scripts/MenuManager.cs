using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuManager : MonoBehaviour
{

    public UIDocument uiDocument; // Verwijzing naar het UIDocument dat de UI laadt
    public VisualTreeAsset gameMenu; // De VisualTreeAsset voor het GameMenu
    private VisualElement currentMenu;
    private VisualElement rootElement;

    void Start()
    {
        // Verkrijg het root element van het UIDocument
        rootElement = uiDocument.rootVisualElement;
    }

    void Update()
    {
        // Controleer of de speler op de Escape-toets drukt
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Als het GameMenu al open is, sluit het; anders open het
            if (currentMenu == gameMenu.CloneTree())
            {
                LoadMenu(null); // Verwijder alle menu's (zet het op None)
            }
            else
            {
                LoadMenu(gameMenu); // Laad het GameMenu als het niet open is
            }
        }
    }

    // Methode om een menu te laden (of te sluiten als menuAsset null is)
    void LoadMenu(VisualTreeAsset menuAsset)
    {
        // Verwijder het huidige menu als er een is
        rootElement.Clear();

        // Als er een menuAsset is, laad het
        if (menuAsset != null)
        {
            currentMenu = menuAsset.CloneTree();
            rootElement.Add(currentMenu);
        }
        else
        {
            currentMenu = null; // Zet currentMenu op null als er geen menu geladen hoeft te worden
        }
    }

}
