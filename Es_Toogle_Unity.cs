using UnityEngine;
using UnityEngine.UI;

public class ThemeManager : MonoBehaviour
{
    public Toggle themeToggle;
    public Image background;

    private void Start()
    {
        // Carica la preferenza salvata
        bool isDarkMode = PlayerPrefs.GetInt("DarkMode", 0) == 1;
        themeToggle.isOn = isDarkMode;
        ApplyTheme(isDarkMode);

        // Aggiungi listener al toggle
        themeToggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged(themeToggle);
        });
    }

    void ToggleValueChanged(Toggle change)
    {
        ApplyTheme(change.isOn);
        // Salva la preferenza
        PlayerPrefs.SetInt("DarkMode", change.isOn ? 1 : 0);
    }

    void ApplyTheme(bool isDarkMode)
    {
        if (isDarkMode)
        {
            background.color = Color.black;
            // Puoi aggiungere ulteriori modifiche per il tema scuro
        }
        else
        {
            background.color = Color.white;
            // Puoi aggiungere ulteriori modifiche per il tema chiaro
        }
    }

    private void OnDestroy()
    {
        // Rimuovi il listener quando l'oggetto viene distrutto
        themeToggle.onValueChanged.RemoveAllListeners();
    }
}
