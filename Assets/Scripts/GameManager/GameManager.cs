using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private SaveManager _saveManager;
    [SerializeField] private CreationManager _creationManager;
    [SerializeField] private SpawnManager _spawnManager;
    [SerializeField] private SalesSystem _salesSystem;
    [SerializeField] private InventorySystem _inventoryManager;

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

        if (_salesSystem == null)
        {
            _salesSystem = FindObjectOfType<SalesSystem>();
            if (_salesSystem == null)
            {
                Debug.LogError("SalesSystem not found in the scene!");
                return;
            }
        }

        await _salesSystem.Initialize();

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
    }
}
