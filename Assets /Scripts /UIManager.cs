using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Updates the game's basic UI. Assign UI objects in the Inspector.
/// Optional references can remain empty while building the first prototype.
/// </summary>
public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("HUD")]
    [SerializeField] private Text messageText;
    [SerializeField] private Text progressText;
    [SerializeField] private Text interactionPrompt;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject journalPanel;
    [SerializeField] private Text journalText;

    [Header("Accessibility")]
    [SerializeField] private Text[] allUiTexts;
    [SerializeField] private Color normalTextColor = Color.white;
    [SerializeField] private Color highContrastTextColor = Color.yellow;
    [SerializeField] private int normalFontSize = 18;
    [SerializeField] private int largeFontSize = 26;

    private readonly List<string> journalEntries = new List<string>();
    private bool highContrastEnabled;
    private bool largeTextEnabled;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        if (pausePanel != null) pausePanel.SetActive(false);
        if (journalPanel != null) journalPanel.SetActive(false);
        HideInteractionPrompt();
        ShowMessage("Explora Bogotá. Muévete con WASD o flechas; presiona E para explorar.");
    }

    public void ShowMessage(string message)
    {
        if (messageText == null) return;
        messageText.text = message;
        messageText.gameObject.SetActive(true);
    }

    public void ShowInteractionPrompt(string message)
    {
        if (interactionPrompt == null) return;
        interactionPrompt.text = message;
        interactionPrompt.gameObject.SetActive(true);
    }

    public void HideInteractionPrompt()
    {
        if (interactionPrompt != null) interactionPrompt.gameObject.SetActive(false);
    }

    public void RefreshProgress(int count)
    {
        if (progressText != null) progressText.text = "Recuerdos: " + count;
    }

    public void AddJournalEntry(string title, string description)
    {
        journalEntries.Add("- " + title + "\n" + description);
        RefreshJournal();
    }

    public void ToggleJournal()
    {
        if (journalPanel == null) return;
        bool isOpening = !journalPanel.activeSelf;
        journalPanel.SetActive(isOpening);
        if (isOpening) RefreshJournal();
    }

    private void RefreshJournal()
    {
        if (journalText == null) return;
        journalText.text = journalEntries.Count == 0
            ? "Aún no has encontrado recuerdos."
            : string.Join("\n\n", journalEntries);
    }

    public void ShowPausePanel(bool show)
    {
        if (pausePanel != null) pausePanel.SetActive(show);
    }

    // Connect this method to an accessibility button in the Inspector.
    public void ToggleHighContrast()
    {
        highContrastEnabled = !highContrastEnabled;
        ApplyAccessibilitySettings();
    }

    // Connect this method to an accessibility button in the Inspector.
    public void ToggleLargeText()
    {
        largeTextEnabled = !largeTextEnabled;
        ApplyAccessibilitySettings();
    }

    private void ApplyAccessibilitySettings()
    {
        if (allUiTexts == null) return;

        foreach (Text uiText in allUiTexts)
        {
            if (uiText == null) continue;
            uiText.color = highContrastEnabled ? highContrastTextColor : normalTextColor;
            uiText.fontSize = largeTextEnabled ? largeFontSize : normalFontSize;
        }
    }
}
