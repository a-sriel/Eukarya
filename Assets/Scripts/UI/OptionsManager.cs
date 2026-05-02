using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsManager : MonoBehaviour
{
    public GameObject optionsPanel;

    public Slider musicSlider;
    public Slider sfxSlider;
    public TextMeshProUGUI musicValueText;
    public TextMeshProUGUI sfxValueText;

    private float pendingMusic;
    private float pendingSFX;

    void Start()
    {
        optionsPanel.SetActive(false);

        float savedMusic = PlayerPrefs.GetFloat("MusicVolume", 50f);
        float savedSFX = PlayerPrefs.GetFloat("SFXVolume", 50f);

        musicSlider.value = savedMusic;
        sfxSlider.value = savedSFX;

        UpdateLabels();
        ApplyVolumes(savedMusic, savedSFX);
    }

    public void Open()
    {
        pendingMusic = musicSlider.value;
        pendingSFX = sfxSlider.value;
        optionsPanel.SetActive(true);
    }

    public void Save()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
        PlayerPrefs.Save();
        ApplyVolumes(musicSlider.value, sfxSlider.value);
        optionsPanel.SetActive(false);
    }

    public void Cancel()
    {
        musicSlider.value = pendingMusic;
        sfxSlider.value = pendingSFX;
        UpdateLabels();
        optionsPanel.SetActive(false);
    }

    public void OnMusicSliderChanged(float value)
    {
        musicValueText.text = Mathf.RoundToInt(value).ToString();
    }

    public void OnSFXSliderChanged(float value)
    {
        sfxValueText.text = Mathf.RoundToInt(value).ToString();
    }

    void UpdateLabels()
    {
        musicValueText.text = Mathf.RoundToInt(musicSlider.value).ToString();
        sfxValueText.text = Mathf.RoundToInt(sfxSlider.value).ToString();
    }

    void ApplyVolumes(float music, float sfx)
    {
        AudioListener.volume = music / 100f;
        // SFX mixer hookup goes here once you set up an AudioMixer
    }
}
