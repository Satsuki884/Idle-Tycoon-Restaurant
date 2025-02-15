using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _sfxSource;

    [Header("Audio Clips Music")]
    [SerializeField] private AudioClip _backgroundMusic;
    // [SerializeField] private AudioClip _peopleSpeakMusic;

    [Header("Audio Clips SFX")]
    [SerializeField] private AudioClip _buyHomeMusic;
    [SerializeField] private AudioClip _buySecondResidentMusic;
    [SerializeField] private AudioClip _upgradeTrayMusic;
    [SerializeField] private AudioClip _levelUpMusic;

    public static AudioManager _instance;
    public static AudioManager Instance => _instance;

    public async Task Initialize(params object[] param)
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;


        await Task.Delay(100);
    }


    private void Start()
    {
        _musicSource.clip = _backgroundMusic;
        _musicSource.Play();
    }

    public string BuyHomeMusic = "BuyHome";
    public string BuySecondResidentMusic = "BuySecondResident";
    public string UpgradeTrayMusic = "UpgradeTray";
    public string LevelUpMusic = "LevelUp";

    public void PlaySFX(string clipName)
    {
        // Debug.Log("Playing SFX: " + clipName);
        switch (clipName)
        {
            case "BuyHome":
                _sfxSource.clip = _buyHomeMusic;
                break;
            case "BuySecondResident":
                _sfxSource.clip = _buySecondResidentMusic;
                break;
            case "UpgradeTray":
                _sfxSource.clip = _upgradeTrayMusic;
                break;
            case "LevelUp":
                _sfxSource.clip = _levelUpMusic;
                break;
        }
        _sfxSource.Play();
    }
}
