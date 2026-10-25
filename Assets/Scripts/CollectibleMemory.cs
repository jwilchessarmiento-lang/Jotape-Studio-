using UnityEngine;

/// <summary>Collectible cultural memory item for the exploration game.</summarynpublic class CollectibleMemory : MonoBehaviour
{
    [SerializeField] private string itemName = "Medallón de la plaza";
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        Debug.Log("Recuerdo recogido: " + itemName);
        gameObject.SetActive(false);
    }
}
