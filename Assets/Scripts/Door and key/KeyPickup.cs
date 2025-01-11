using UnityEngine;
using TMPro;

public class KeyPickup : MonoBehaviour
{
    public TextMeshProUGUI feedbackText; // Assign in Inspector
    private bool isPlayerNear = false;
    private bool keyPickedUp = false;

    void Start()
    {
        if (feedbackText != null)
            feedbackText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E) && !keyPickedUp)
        {
            PickUpKey();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !keyPickedUp)
        {
            isPlayerNear = true;
            ShowMessage("Press 'E' to pick up the key");
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

    void PickUpKey()
    {
        keyPickedUp = true;
        // Set the static variable on DoorInteraction to indicate player now has the key
        DoorInteraction.PlayerHasKey = true;

        StartCoroutine(ShowTemporaryMessage("You picked up the key!", 3f));
        Destroy(gameObject, 0.5f);
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
