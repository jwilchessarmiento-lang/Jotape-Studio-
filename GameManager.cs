using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stores game progress, collected memories, and pause state.
/// Add this script to a GameObject named GameManager.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private UIManager uiManager;
    private readonly HashSet<string> collectedMemoryIds = new HashSet<string>();
    private readonly List<string> visitedPlaces = new List<string>();

    public bool IsPaused { get; private set; }
    public int MemoriesCollected => collectedMemoryIds.Count;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        Time.timeScale = 1f;
    }

    private void Start()
    {
        if (uiManager != null) uiManager.RefreshProgress(MemoriesCollected);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
            TogglePause();

        if (Input.GetKeyDown(KeyCode.J) && uiManager != null)
            uiManager.ToggleJournal();
    }

    public void CollectMemory(string memoryId, string memoryTitle, string memoryDescription)
    {
        if (string.IsNullOrWhiteSpace(memoryId) || collectedMemoryIds.Contains(memoryId))
            return;

        collectedMemoryIds.Add(memoryId);

        if (uiManager != null)
        {
            uiManager.ShowMessage("Recuerdo encontrado: " + memoryTitle);
            uiManager.AddJournalEntry(memoryTitle, memoryDescription);
            uiManager.RefreshProgress(MemoriesCollected);
        }
    }

    public void VisitPlace(string placeName, string description)
    {
        if (!visitedPlaces.Contains(placeName))
        {
            visitedPlaces.Add(placeName);
            if (uiManager != null) uiManager.ShowMessage(placeName + ": " + description);
        }
    }

    public void TogglePause()
    {
        IsPaused = !IsPaused;
        Time.timeScale = IsPaused ? 0f : 1f;
        if (uiManager != null) uiManager.ShowPausePanel(IsPaused);
    }
}
