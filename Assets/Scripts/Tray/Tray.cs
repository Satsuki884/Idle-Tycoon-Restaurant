using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

[System.Serializable]
public class TraySpot
{
    // public Vector3 position;
    public Transform position;
    public bool isFree = true;
    public bool IsLocked = false;
}


public class Tray : MonoBehaviour
{
    [SerializeField] private string _trayName;
    public string TrayName => _trayName;
    [SerializeField] private Transform _startPositions;
    public Transform StartPositions => _startPositions;
    [SerializeField] private Transform _endPositions;
    public Transform EndPositions => _endPositions;
    [SerializeField] private Transform _queuePoints;
    public Transform QueuePoints => _queuePoints;
    [SerializeField] public Transform _spawnZone;
    public Transform SpawnZone => _spawnZone;
    public Vector3[] availableSpots;
    [SerializeField] private List<TraySpot> _availableSpots; // Переделали
    public List<TraySpot> AvailableSpots => _availableSpots;
    [SerializeField] private int incomePerOrder = 2;
    [SerializeField] private QueueManager _queueManager;
    [SerializeField] private SpawnManager _spawnManager;
    [SerializeField] private Transform _spawnTransform;
    // public QueueManager QueueManager
    // {
    //     get => _queueManager;
    //     set => _queueManager = value;
    // }
    [SerializeField] private ProductType _productType;
    public ProductType ProductType => _productType;
    [SerializeField] private bool _isQueueFool;
    public bool IsQueueFool => _isQueueFool;

    [SerializeField] private bool _isTrayFool;
    public bool IsTrayFool => _isTrayFool;
    private TrayDataSO trayData;
    private TrayData _thisTraySO;

    [SerializeField] private GameObject _secondResidentPrefab;

    private void Start()
    {
        SetCurrentTrayData();
        StartCoroutine(SpawnCharacters());

        _availableSpots = _availableSpots.OrderBy(s => s.position.position.x).ToList();
    }
    private void GetTrayDataSO()
    {
        trayData = SaveManager.Instance.TrayData;
    }

    private void SetCurrentTrayData()
    {
        GetTrayDataSO();
        foreach (var tray in trayData.TrayData)
        {
            if (tray.TrayData.ProductType == _productType)
            {
                _thisTraySO = tray.TrayData;
                // Debug.Log(_thisTraySO.TrayName + " " + _thisTraySO.IsActive);
            }
        }
        SetAvailableResident();
    }

    public void SetAvailableResident()
    {
        if (_thisTraySO.SecondResidents)
        {
            _secondResidentPrefab.SetActive(true);
        }
        else
        {
            _secondResidentPrefab.SetActive(false);
        }
        if (_thisTraySO.SecondResidents)
        {
            foreach (var spot in _availableSpots)
            {
                spot.IsLocked = false;
            }
        }
        else
        {
            _availableSpots.First().IsLocked = false;
            _availableSpots.Last().IsLocked = true;
        }
    }

    private Character _character;

    private IEnumerator SpawnCharacters()
    {
        while (true)
        {
            if (!QueueIsFull() && _thisTraySO.IsActive)
            {
                _character = _spawnManager.SpawnCharacters(_spawnZone);
                _queueManager.AddToQueue(_character);
                AddCharacterToQueue(_character);
                _character = null;
            }
            yield return new WaitForSeconds(_thisTraySO.TimeToServe);
        }
    }

    public void AddCharacterToQueue(Character character)
    {
        character.MoveTo(_queuePoints.position, () =>
        {
            TryMoveToSpot();
        });
    }

    private Character _currentCharacter;

    private void TryMoveToSpot()
    {
        if (_queueManager.WaitingQueue.Count == 0) return;

        var freeSpot = _availableSpots.FirstOrDefault(s => s.isFree && !s.IsLocked);
        if (freeSpot != null)
        {
            _currentCharacter = _queueManager.WaitingQueue.Dequeue();
            freeSpot.isFree = false;
            _currentCharacter.MoveTo(freeSpot.position.position, () =>
            {
                StartCoroutine(ServeCharacter(_currentCharacter, freeSpot));
            });
        }
    }

    private IEnumerator ServeCharacter(Character character, TraySpot spot)
    {
        var _tryExp = _thisTraySO.ExperiencePerTrayOrder;

        PlayerProgressionSystem.Instance.BuyProduct(_thisTraySO.ExperiencePerTrayOrder + _thisTraySO.ExperiencePerTrayOrder * _thisTraySO.UpgradeLevel,
                                                        _thisTraySO.ProductUpgradeData.UpgradePrice[_thisTraySO.UpgradeLevel],
                                                        _thisTraySO.ProductType);
        yield return new WaitForSeconds(_thisTraySO.TimeToServe);
        spot.isFree = true;
        TryMoveToSpot();
        character.MoveTo(_endPositions.position, () =>
        {
            character.MoveTo(_spawnZone.position, () =>
            {
                Destroy(character.gameObject);
            });
        });
    }


    public bool QueueIsFull()
    {
        // if (QueueManager.WaitingQueue.Count == incomePerOrder)
        if (_queueManager.WaitingQueue.Count == incomePerOrder)
        {
            _isQueueFool = true;
            return true;
        }
        else
        {
            _isQueueFool = false;
            return false;
        }
    }


}
