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
    public bool IsLeft;
}

[System.Serializable]
public class TrayQueuePoint
{
    public Transform position;
    public bool isFree = true;
}


public class Tray : MonoBehaviour
{
    [SerializeField] private string _trayName;
    [SerializeField] public string TrayName => _trayName;
    [SerializeField] private Transform _leftEndPosition;
    [SerializeField] private Transform _rightEndPosition;
    [SerializeField] private TrayQueuePoint _queuePoint;
    [SerializeField] private List<TrayQueuePoint> _queuePoints;
    [SerializeField] public Transform _spawnZone;
    [SerializeField] private List<TraySpot> _availableSpots;
    [SerializeField] private Transform _spawnListZone;
    public List<TraySpot> AvailableSpots
    {
        get
        {
            return _availableSpots;
        }
        set
        {
            _availableSpots = value;
        }
    }
    private Queue<Character> _waitingQueue = new Queue<Character>();
    [SerializeField] private SpawnManager _spawnManager;
    [SerializeField] private ProductType _productType;
    private TrayDataSO trayData;
    private TrayData _thisTraySO;

    [SerializeField] private GameObject _secondResidentPrefab;
    public GameObject SecondResidentPrefab => _secondResidentPrefab;

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

    public void SetCurrentTrayData()
    {
        // Debug.Log("SetCurrentTrayData");
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
        // Debug.Log("SetAvailableResident");
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
            _availableSpots.First().IsLocked = false;
            _availableSpots.Last().IsLocked = false;
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
            if (/*!QueueIsFull() && */_thisTraySO.IsActive && _queuePoint.isFree)
            {
                _queuePoint.isFree = false;
                _character = _spawnManager.SpawnCharacters(_spawnZone, _spawnListZone);
                _waitingQueue.Enqueue(_character);
                AddCharacterToQueue(_character);
                _character = null;
            }
            yield return new WaitForSeconds(_thisTraySO.TimeToServe/2);
            // yield return null;
        }
    }

    public void AddCharacterToQueue(Character character)
    {
        
        character.MoveTo(_queuePoint.position.position, () =>
        {
            TryMoveToSpot();
        });
    }

    private Character _currentCharacter;

    private void TryMoveToSpot()
    {
        if (_waitingQueue.Count == 0) return;

        var freeSpot = _availableSpots.FirstOrDefault(s => s.isFree && !s.IsLocked);
        if (freeSpot != null)
        {
            _queuePoint.isFree = true;
            _currentCharacter = _waitingQueue.Dequeue();
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
        _tryExp += _thisTraySO.ExperiencePerTrayOrder * _thisTraySO.UpgradeLevel;

        var _tryMoney = _thisTraySO.ProductUpgradeData.UpgradePrice[_thisTraySO.UpgradeLevel];

        PlayerProgressionSystem.Instance.BuyProduct(_tryExp, _tryMoney, _thisTraySO.ProductType);
        yield return new WaitForSeconds(_thisTraySO.TimeToServe);
        spot.isFree = true;
        TryMoveToSpot();
        if (spot.IsLeft)
        {
            character.MoveTo(_leftEndPosition.position, () =>
            {
                character.MoveTo(_spawnZone.position, () =>
                {
                    Destroy(character.gameObject);
                });
            });
        }
        else
        {
            character.MoveTo(_rightEndPosition.position, () =>
            {
                character.MoveTo(_spawnZone.position, () =>
                {
                    Destroy(character.gameObject);
                });
            });
        }
    }

    public bool QueueIsFull()
    {
        if (_waitingQueue.Count >= _queuePoints.Count || _spawnListZone.childCount >= _queuePoints.Count)
        {
            Debug.Log("true");
            return true;
        }
        else
        {
            return false;
        }
    }


}
