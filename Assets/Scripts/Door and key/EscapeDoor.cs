using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DoorInteraction : MonoBehaviour
{
    public static bool PlayerHasKey = false; // Static variable to track key possession

    [Header("Scene Transition")]
    public string nextSceneName = "NextScene"; // Scene to load when the door is unlocked

    [Header("UI Feedback")]
    public TextMeshProUGUI feedbackText; // UI TextMeshPro for player messages

    private bool isNearDoor = false;

    void Start()
    {
        if (feedbackText != null)
            feedbackText.gameObject.SetActive(false);
    }

    void Update()
    {
        // If player is near the door and has the key, allow them to open it
        if (isNearDoor && PlayerHasKey && Input.GetKeyDown(KeyCode.E))
        {
            OpenDoor();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearDoor = true;
            UpdateDoorMessage();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearDoor = false;
            HideMessage();
        }
    }

    void OpenDoor()
    {
        SceneManager.LoadScene(nextSceneName);
    }

    void UpdateDoorMessage()
    {
        if (feedbackText != null)
        {
            if (PlayerHasKey)
                feedbackText.text = "Press 'E' to open the door";
            else
                feedbackText.text = "You need a key to open this door";

            feedbackText.gameObject.SetActive(true);
        }
    }

    void HideMessage()
    {
        if (feedbackText != null)
            feedbackText.gameObject.SetActive(false);
    }
}
