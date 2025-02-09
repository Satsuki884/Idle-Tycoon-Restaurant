using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            // _instance = this;
            _instance = FindObjectOfType<GameManager>();
            DontDestroyOnLoad(_instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [SerializeField] private SaveManager _saveManager;
    public SaveManager SaveManager
    {
        get => _saveManager;
        set => _saveManager = value;
    }

    // [SerializeField] private AudioManager _audioManager;
    // public AudioManager AudioManager
    // {
    //     get => _audioManager;
    //     set => _audioManager = value;
    // }
}
