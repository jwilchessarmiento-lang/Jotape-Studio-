using UnityEngine;

/// <summary>
/// Lets the player inspect a landmark by pressing E nearby.
/// Add a Collider with Is Trigger enabled to the landmark.
/// </summary>
public class InteractableLandmark : MonoBehaviour
{
    [SerializeField] private string placeName = "Plaza de Bolívar";
    [TextArea(2, 5)]
    [SerializeField] private string description =
        "Un lugar emblemático de Bogotá, rodeado de edificios históricos.";
    [SerializeField] private KeyCode interactKey = KeyCode.E;

    private bool playerIsNearby;

    private void Update()
    {
        if (playerIsNearby && Input.GetKeyDown(interactKey) &&
            (GameManager.Instance == null || !GameManager.Instance.IsPaused))
        {
            if (GameManager.Instance != null)
                GameManager.Instance.VisitPlace(placeName, description);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerIsNearby = true;
        if (UIManager.Instance != null)
            UIManager.Instance.ShowInteractionPrompt("Presiona E para explorar: " + placeName);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerIsNearby = false;
        if (UIManager.Instance != null)
            UIManager.Instance.HideInteractionPrompt();
    }
}
