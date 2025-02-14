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
    [SerializeField] private Transform _endPositions;
    [SerializeField] private Transform _queuePoint;
    [SerializeField] private List<TrayQueuePoint> _queuePoints;
    [SerializeField] public Transform _spawnZone;
    [SerializeField] private List<TraySpot> _availableSpots;
    private Queue<Character> _waitingQueue = new Queue<Character>();
    [SerializeField] private SpawnManager _spawnManager;
    [SerializeField] private ProductType _productType;
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
                _waitingQueue.Enqueue(_character);
                AddCharacterToQueue(_character);
                _character = null;
            }
            yield return new WaitForSeconds(Random.Range(_thisTraySO.TimeToServe / 1.3f, _thisTraySO.TimeToServe * 2));
        }
    }

    public void AddCharacterToQueue(Character character)
    {
        character.MoveTo(_queuePoint.position, () =>
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
        if (_waitingQueue.Count >= _queuePoints.Count)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


}
