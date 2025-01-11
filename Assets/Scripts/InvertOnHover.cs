using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InvertOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TextMeshProUGUI textComponent;
    private Color originalColor;

    void Start()
    {
        // Haal de TextMeshProUGUI-component op
        textComponent = GetComponent<TextMeshProUGUI>();

        if (textComponent != null)
        {
            originalColor = textComponent.color;
        }
        else
        {
            Debug.LogError("No TextMeshProUGUI component found on this object.");
        }
    }

    // Event als de muis over de Textbox gaat
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (textComponent != null)
        {
            textComponent.color = InvertColor(originalColor);
        }
    }

    // Event als de muis de Textbox verlaat
    public void OnPointerExit(PointerEventData eventData)
    {
        if (textComponent != null)
        {
            textComponent.color = originalColor;
        }
    }

    // Methode om de kleur te inverteren
    private Color InvertColor(Color color)
    {
        return new Color(1 - color.r, 1 - color.g, 1 - color.b, color.a);
    }
}