using UnityEngine;

[CreateAssetMenu(fileName = "AudioSO", menuName = "ScriptableObjects/AudioSO", order = 1)]
public class SettingsSO : ScriptableObject
{
    [SerializeField] private SettingsData _settingsData;
    public SettingsData SettingsData { get => _settingsData; set => _settingsData = value; }
}

[System.Serializable]
public class SettingsData
{
    [SerializeField] private float musicVolume = 1.0f;
    public float MusicVolume { get => musicVolume; set => musicVolume = value; }

    [SerializeField] private int qualityLevel = 5;
    public int QualityLevel { get => qualityLevel; set => qualityLevel = value; }
}