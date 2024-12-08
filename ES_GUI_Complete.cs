using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Toggle soundToggle;
    public Slider volumeSlider;
    public Dropdown qualityDropdown;
    public Button confirmButton;
    public Button cancelButton;

    void Start()
    {
        // Carica le impostazioni salvate
        soundToggle.isOn = PlayerPrefs.GetInt("Sound", 1) == 1;
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1f);
        qualityDropdown.value = PlayerPrefs.GetInt("Quality", 2); // 0: Bassa, 1: Media, 2: Alta

        // Assegna gli eventi
        soundToggle.onValueChanged.AddListener(OnSoundToggle);
        volumeSlider.onValueChanged.AddListener(OnVolumeChange);
        qualityDropdown.onValueChanged.AddListener(OnQualityChange);
        confirmButton.onClick.AddListener(OnConfirm);
        cancelButton.onClick.AddListener(OnCancel);
    }

    void OnSoundToggle(bool isOn)
    {
        // Abilita o disabilita il suono
        AudioListener.volume = isOn ? volumeSlider.value : 0f;
    }

    void OnVolumeChange(float value)
    {
        // Regola il volume
        if (soundToggle.isOn)
        {
            AudioListener.volume = value;
        }
    }

    void OnQualityChange(int qualityIndex)
    {
        // Cambia la qualità grafica
        QualitySettings.SetQualityLevel(qualityIndex, true);
    }

    void OnConfirm()
    {
        // Salva le impostazioni
        PlayerPrefs.SetInt("Sound", soundToggle.isOn ? 1 : 0);
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
        PlayerPrefs.SetInt("Quality", qualityDropdown.value);
        PlayerPrefs.Save();
        // Chiudi il menu delle opzioni
        gameObject.SetActive(false);
    }

    void OnCancel()
    {
        // Ripristina le impostazioni precedenti
        soundToggle.isOn = PlayerPrefs.GetInt("Sound", 1) == 1;
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1f);
        qualityDropdown.value = PlayerPrefs.GetInt("Quality", 2);
        // Chiudi il menu delle opzioni
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        // Rimuovi gli ascoltatori per evitare memory leak
        soundToggle.onValueChanged.RemoveAllListeners();
        volumeSlider.onValueChanged.RemoveAllListeners();
        qualityDropdown.onValueChanged.RemoveAllListeners();
        confirmButton.onClick.RemoveAllListeners();
        cancelButton.onClick.RemoveAllListeners();
    }
}
