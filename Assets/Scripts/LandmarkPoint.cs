using UnityEngine;

/// <summary>Marks a place of cultural interest in the scene.</summary>
public class LandmarkPoint : MonoBehaviour
{
    [SerializeField] private string landmarkName = "Plaza de Bolívar";
    [TextArea, SerializeField] private string description = "Espacio público representativo de la historia y la vida ciudadana de Bogotá.";
    public string LandmarkName => landmarkName;
    public string Description => description;
    private void OnDrawGizmosSelected() { Gizmos.color = new Color32(248, 200, 75, 255); Gizmos.DrawWireSphere(transform.position, 1.5f); }
}
