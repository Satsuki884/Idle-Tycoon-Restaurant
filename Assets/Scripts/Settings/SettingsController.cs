using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingController : MonoBehaviour
{
    private string Music = "Music";

    [Header("Audio")]
    [SerializeField] private Slider _volumeSlider;
    [SerializeField] private AudioMixer _audioMixer;
    [Header("Quality")]
    [SerializeField] private TMP_Dropdown _qualityDropdown;

    private SettingsData _settingsData;

    private void Start()
    {
        _settingsData = SaveManager.Instance.SettingsData;
        AddEventListeners();
        SetBetweenSession();
    }

    public void AddEventListeners()
    {
        _volumeSlider.onValueChanged.RemoveAllListeners();
        _volumeSlider.onValueChanged.AddListener(SetVolumeMusic);

        _qualityDropdown.onValueChanged.AddListener(SetQuality);
        _qualityDropdown.onValueChanged.AddListener(SetQuality);
    }

    public void SetBetweenSession()
    {
        _volumeSlider.value = _settingsData.MusicVolume;
        _audioMixer.SetFloat(Music, _settingsData.MusicVolume);

        _qualityDropdown.value = _settingsData.QualityLevel;
        QualitySettings.SetQualityLevel(_settingsData.QualityLevel);
    }

    public void SetVolumeMusic(float volume)
    {
        _settingsData.MusicVolume = volume;
        _audioMixer.SetFloat(Music, volume);
        SaveManager.Instance.SaveSettingsData(_settingsData);
    }

    public void SetQuality(int qualityLevel)
    {
        _settingsData.QualityLevel = qualityLevel;
        QualitySettings.SetQualityLevel(qualityLevel);
        SaveManager.Instance.SaveSettingsData(_settingsData);
    }
}