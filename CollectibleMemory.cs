using UnityEngine;

/// <summary>
/// Adds a collectible cultural memory to the player's journal.
/// Use a trigger collider and a unique memory ID for each collectible.
/// </summary>
public class CollectibleMemory : MonoBehaviour
{
    [SerializeField] private string memoryId = "memory_01";
    [SerializeField] private string memoryTitle = "Memoria de Bogotá";
    [TextArea(2, 5)]
    [SerializeField] private string memoryDescription =
        "Este recuerdo invita a conocer y cuidar el patrimonio cultural de la ciudad.";

    private bool collected;

    private void OnTriggerEnter(Collider other)
    {
        if (collected || !other.CompareTag("Player")) return;

        collected = true;
        if (GameManager.Instance != null)
            GameManager.Instance.CollectMemory(memoryId, memoryTitle, memoryDescription);

        gameObject.SetActive(false);
    }
}
