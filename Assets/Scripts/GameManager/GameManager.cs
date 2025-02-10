using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private SaveManager _saveManager;
    [SerializeField] private CreationManager _creationManager;

    private async void Start()
    {
        if (_saveManager == null)
        {
            _saveManager = FindObjectOfType<SaveManager>();
            if (_saveManager == null)
            {
                Debug.LogError("SaveManager not found in the scene!");
                return;
            }
        }

        await _saveManager.Initialize();

        if (_creationManager == null)
        {
            _creationManager = FindObjectOfType<CreationManager>();
            if (_creationManager == null)
            {
                Debug.LogError("CreationManager not found in the scene!");
                return;
            }
        }
    }
    // private static GameManager _instance;
    // public static GameManager Instance
    // {
    //     get
    //     {
    //         if (_instance == null)
    //         {
    //             _instance = FindObjectOfType<GameManager>();
    //             if (_instance == null)
    //             {
    //                 Debug.LogError("GameManager instance not found in the scene!");
    //                 return null;
    //             }
    //             DontDestroyOnLoad(_instance);
    //         }
    //         return _instance;
    //     }
    // }

    // [SerializeField] private SaveManager _saveManager;
    // public SaveManager SaveManager
    // {
    //     get
    //     {
    //         if (_saveManager == null)
    //         {
    //             Debug.LogError("SaveManager is NULL! Make sure it is assigned in the Inspector.");
    //         }
    //         return _saveManager;
    //     }
    // }

    // private void Awake()
    // {
    //     if (_instance == null)
    //     {
    //         _instance = this;
    //         DontDestroyOnLoad(gameObject);

    //         if (_saveManager == null)
    //         {
    //             _saveManager = FindObjectOfType<SaveManager>();
    //             if (_saveManager == null)
    //             {
    //                 Debug.LogError("SaveManager not found! Make sure it is added to the scene.");
    //             }
    //         }
    //     }
    //     else if (_instance != this)
    //     {
    //         Destroy(gameObject);
    //     }
    // }
}
