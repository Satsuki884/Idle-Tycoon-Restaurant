using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    [SerializeField] private int incomePerOrder = 5;
    [SerializeField] private QueueManager _queueManager;
    [SerializeField] private SpawnManager _spawnManager;
    [SerializeField] private Transform _spawnTransform;
    public QueueManager QueueManager
    {
        get => _queueManager;
        set => _queueManager = value;
    }
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
        // GetTrayDataSO();
        

        _availableSpots = _availableSpots.OrderBy(s => s.position.position.x).ToList();
    }
    private void GetTrayDataSO()
    {
        trayData = SaveManager.Instance.TrayData;
    }

    private void SetCurrentTrayData()
    {
        GetTrayDataSO();
        // Debug.Log(trayData.TrayData.Count);
        foreach (var tray in trayData.TrayData)
        {
            if (tray.TrayData.ProductType == _productType)
            {
                _thisTraySO = tray.TrayData;
            }
        }
        SetAvailableResident();
    }

    public void BuyNewResident()
    {
        // for (int i = 0; i < _thisTraySO.Residents.Count; i++)
        // {
        //     if(!_thisTraySO.Residents[i].IsActive)
        //     {
        //         _thisTraySO.Residents[i].IsActive = true;
        SetAvailableResident();
        //     break;
        // }
        // }
    }

    private void SetAvailableResident()
    {
        if(_thisTraySO.SecondResidents){
            _secondResidentPrefab.SetActive(true);
        } else {
            _secondResidentPrefab.SetActive(false);
        }
        if (_thisTraySO.SecondResidents)
        {
            foreach (var spot in _availableSpots)
            {
                spot.IsLocked = false;
            }
        } else {
            _availableSpots.First().IsLocked = false;
            _availableSpots.Last().IsLocked = true;
        }
    }

    private IEnumerator SpawnCharacters()
    {
        while (true)
        {
            if (!QueueIsFull())
            {
                _queueManager.AddToQueue(_spawnManager.SpawnCharacters(_productType, _spawnZone));
                Debug.Log(_queueManager.WaitingQueue.Count);
            }
            yield return new WaitForSeconds(3f); // Add a delay to prevent infinite rapid spawning
        }
    }

    public void AddCharacterToQueue(Character character)
    {
        character.MoveTo(_queuePoints.position); // Персонаж идёт в очередь
        TryMoveToSpot();
    }

    private void TryMoveToSpot()
    {
        if (_queueManager.WaitingQueue.Count == 0) return;

        var freeSpot = _availableSpots.FirstOrDefault(s => s.isFree && !s.IsLocked);
        if (freeSpot != null)
        {
            var character = _queueManager.WaitingQueue.Dequeue();
            freeSpot.isFree = false;
            character.MoveTo(freeSpot.position.position, () =>
            {
                StartCoroutine(ServeCharacter(character, freeSpot));
            });
        }
    }

    private IEnumerator ServeCharacter(Character character, TraySpot spot)
    {
        yield return new WaitForSeconds(4f);
        spot.isFree = true;
        character.MoveTo(_endPositions.position, () =>
        {
            character.MoveTo(_spawnZone.position, () =>
            {
                Destroy(character.gameObject);
                // spot.isFree = true;
                TryMoveToSpot();
            });
        });
    }


    public bool QueueIsFull()
    {
        // Debug.Log(QueueManager.WaitingQueue.Count + " QueueManager.WaitingQueue.Count");
        // Debug.Log(incomePerOrder + " incomePerOrder");
        if (QueueManager.WaitingQueue.Count == incomePerOrder)
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
    // public void SetSpot()
    // {
    //     Character character = QueueManager.WaitingQueue.Peek();
    //     List<Vector3> newPath = new List<Vector3>();
    //     foreach (var spot in _traySpot)
    //     {
    //         if (spot.Value == true)
    //         {
    //             _traySpot[spot.Key] = false;
    //             newPath.Add(character.transform.position);
    //             newPath.Add(spot.Key);
    //             StartCoroutine(MoveToTray(newPath.ToArray(), character.moveSpeed));
    //             _traySpot[spot.Key] = false;
    //         }
    //     }
    // }

    // IEnumerator MoveToTray(Vector3[] path, float moveSpeed)
    // {
    //     QueueManager.WaitingQueue.Dequeue();
    //     foreach (Vector3 point in path)
    //     {
    //         while (Vector3.Distance(QueueManager.WaitingQueue.Peek().transform.position, point) > 0.1f)
    //         {
    //             QueueManager.WaitingQueue.Peek().transform.position = Vector3.MoveTowards(QueueManager.WaitingQueue.Peek().transform.position, point, moveSpeed * Time.deltaTime);
    //             yield return null;
    //         }
    //     }
    // }


}
