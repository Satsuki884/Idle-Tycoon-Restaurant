using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private SaveManager _saveManager;
    [SerializeField] private CreationManager _creationManager;
    [SerializeField] private SpawnManager _spawnManager;
    // [SerializeField] private QueueManager _queueManager;
    // [SerializeField] private SalesSystem _salesSystem;
    [SerializeField] private InventorySystem _inventoryManager;
    [SerializeField] private PlayerProgressionSystem _playerProgressionSystem;

    private async void Awake()
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

        await _creationManager.Initialize();

        if (_spawnManager == null)
        {
            _spawnManager = FindObjectOfType<SpawnManager>();
            if (_spawnManager == null)
            {
                Debug.LogError("SpawnManager not found in the scene!");
                return;
            }
        }

        await _spawnManager.Initialize();

        // if (_queueManager == null)
        // {
        //     _queueManager = FindObjectOfType<QueueManager>();
        //     if (_queueManager == null)
        //     {
        //         Debug.LogError("QueueManager not found in the scene!");
        //         return;
        //     }
        // }

        // await _queueManager.Initialize();

        // if (_salesSystem == null)
        // {
        //     _salesSystem = FindObjectOfType<SalesSystem>();
        //     if (_salesSystem == null)
        //     {
        //         Debug.LogError("SalesSystem not found in the scene!");
        //         return;
        //     }
        // }

        // await _salesSystem.Initialize();

        if (_inventoryManager == null)
        {
            _inventoryManager = FindObjectOfType<InventorySystem>();
            if (_inventoryManager == null)
            {
                Debug.LogError("InventorySystem not found in the scene!");
                return;
            }
        }

        await _inventoryManager.Initialize();

        if (_playerProgressionSystem == null)
        {
            _playerProgressionSystem = FindObjectOfType<PlayerProgressionSystem>();
            if (_playerProgressionSystem == null)
            {
                Debug.LogError("PlayerProgressionSystem not found in the scene!");
                return;
            }
        }

        await _playerProgressionSystem.Initialize();
    }
}
