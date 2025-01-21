using UnityEngine;
using TMPro;

public class KeypadInteraction : MonoBehaviour
{
    [Header("UI References")]
    public GameObject keypadUI;  // Assign the keypad UI Canvas in the Inspector
    public TextMeshProUGUI codeDisplay;  // Assign TMP Text to display input
    public TextMeshProUGUI feedbackText;  // Assign TMP Text for interaction prompt

    [Header("Game Objects to Control")]
    public GameObject boxToRemove;  // Box that disappears when code is correct
    public GameObject keyObject;    // Key that appears when code is correct
    public GameObject keypadObject; // Assign the keypad object to destroy it when code is correct

    [Header("Keypad Settings")]
    public string correctCode = "1234";  // Set your desired code

    [Header("Player Control")]
    public GameObject player;  // Assign the player GameObject here
    public MonoBehaviour playerMovementScript;  // Assign movement script here
    public MonoBehaviour cameraLookScript;      // Assign camera look script here

    private string enteredCode = "";
    private bool isInteracting = false;
    private bool isPlayerNear = false;

    private void Start()
    {
        keypadUI.SetActive(false);
        keyObject.SetActive(false);  // Hide key until unlocked

        // Ensure feedbackText is disabled initially
        if (feedbackText != null)
        {
            feedbackText.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E) && !isInteracting)
        {
            OpenKeypad();
        }

        if (isInteracting && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseKeypad();
        }
    }

    public void OpenKeypad()
    {
        isInteracting = true;
        keypadUI.SetActive(true);
        enteredCode = "";
        UpdateCodeDisplay();

        // Unlock the mouse cursor and allow movement
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Disable player movement and camera control
        if (playerMovementScript != null) playerMovementScript.enabled = false;
        if (cameraLookScript != null) cameraLookScript.enabled = false;

        // Hide interaction prompt
        HideMessage();
    }

    public void CloseKeypad()
    {
        isInteracting = false;
        keypadUI.SetActive(false);

        // Lock the mouse cursor and resume player movement
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (playerMovementScript != null) playerMovementScript.enabled = true;
        if (cameraLookScript != null) cameraLookScript.enabled = true;
    }

    public void EnterDigit(string digit)
    {
        if (enteredCode.Length < 9)
        {
            enteredCode += digit;
            UpdateCodeDisplay();
        }
    }

    public void ClearCode()
    {
        enteredCode = "";
        UpdateCodeDisplay();
    }

    public void SubmitCode()
    {
        if (enteredCode == correctCode)
        {
            Debug.Log("Correct Code Entered!");
            RemoveBoxAndUnlockKey();
            CloseKeypad();
        }
        else
        {
            Debug.Log("Incorrect Code!");
            ShowTemporaryMessage("Incorrect Code! Try Again.", 2f);
            ClearCode();
        }
    }

    private void RemoveBoxAndUnlockKey()
    {
        if (boxToRemove != null)
        {
            Destroy(boxToRemove);
        }

        if (keyObject != null)
        {
            keyObject.SetActive(true);
        }

        if (keypadObject != null)
        {
            Destroy(keypadObject);
        }
    }

    private void UpdateCodeDisplay()
    {
        if (codeDisplay != null)
        {
            codeDisplay.text = enteredCode;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isInteracting)
        {
            isPlayerNear = true;
            ShowMessage("Press 'E' to interact with the keypad.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            HideMessage();
        }
    }

    void ShowMessage(string message)
    {
        if (feedbackText != null)
        {
            feedbackText.text = message;
            feedbackText.gameObject.SetActive(true);
        }
    }

    void HideMessage()
    {
        if (feedbackText != null)
            feedbackText.gameObject.SetActive(false);
    }

    System.Collections.IEnumerator ShowTemporaryMessage(string message, float duration)
    {
        ShowMessage(message);
        yield return new WaitForSeconds(duration);
        HideMessage();
    }
}
