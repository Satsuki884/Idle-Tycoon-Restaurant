using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class TraySpot
{
    public Transform transform;
    public bool isFree = true;
    public bool IsLocked = false;
    public bool IsLeft;
}

public class Tray : MonoBehaviour
{
    [SerializeField] private string _trayName;
    public string TrayName => _trayName;

    [SerializeField] private Transform _leftEndPosition;
    [SerializeField] private Transform _rightEndPosition;
    [SerializeField] private Transform _spawnZone;
    [SerializeField] private List<TraySpot> _availableSpots;
    // public void SetSecondAvailableSpots()
    // {
    //     _secondResidentPrefab.SetActive(_thisTraySO.SecondResidents);
    //     _availableSpots.Find(s => s.IsLocked).IsLocked = false;
    //     _availableSpots.Find(s => s.isFree).isFree = true;
    // }
    [SerializeField] private Transform _spawnListZone;
    [SerializeField] private SpawnManager _spawnManager;
    [SerializeField] private ProductType _productType;
    [SerializeField] private QueueController _queueController;
    [SerializeField] private GameObject _secondResidentPrefab;

    private Queue<Character> _waitingQueue = new Queue<Character>();
    private TrayDataSO trayData;
    private TrayData _thisTraySO;
    private int _countCharactersInSpot;

    private void Start()
    {
        SetCurrentTrayData();
        StartCoroutine(SpawnCharacters());

        _availableSpots = _availableSpots.OrderBy(s => s.transform.position.x).ToList();
    }

    private void GetTrayDataSO()
    {
        trayData = SaveManager.Instance.TrayData;
    }

    public void SetCurrentTrayData()
    {
        GetTrayDataSO();
        foreach (var tray in trayData.TrayData)
        {
            if (tray.TrayData.ProductType == _productType)
            {
                _thisTraySO = tray.TrayData;
            }
        }
        SetAvailableResident();
    }

    public void SetAvailableResident()
    {
        _secondResidentPrefab.SetActive(_thisTraySO.SecondResidents);

        _availableSpots.First().IsLocked = false;
        _availableSpots.Last().IsLocked = !_thisTraySO.SecondResidents;
    }

    private IEnumerator SpawnCharacters()
    {
        while (true)
        {
            if (!_thisTraySO.IsActive)
            {
                yield return null;
                continue;
            }

            Character newCharacter = null;

            // GetTrayDataSO();

            int maxCharacters = _thisTraySO.SecondResidents ? 2 : 1;
            Debug.Log(maxCharacters);

            if (_queueController.IsQueueEmpty && _countCharactersInSpot < maxCharacters)
            {
                newCharacter = _spawnManager.SpawnCharacters(_spawnZone, _spawnListZone);
                
                _countCharactersInSpot++;
                _waitingQueue.Enqueue(newCharacter);
                TryMoveToSpot();
            }
            else if (_queueController.IsQueueSlotAvailableToAdd && !_queueController.IsUpdateQueueInProgress)
            {
                newCharacter = _spawnManager.SpawnCharacters(_spawnZone, _spawnListZone);

                _waitingQueue.Enqueue(newCharacter);
                
                AddCharacterToQueue(newCharacter);
            }

            yield return new WaitForSeconds(_thisTraySO.TimeToServe / 4);
        }
    }

    public void AddCharacterToQueue(Character character)
    {
        Vector3 position = _queueController.AddNewCharacterToQueue(character);
        
        if (position != Vector3.zero)
        {
            character.MoveTo(position, null);
        }
    }

    private void TryMoveToSpot(Character character = null)
    {
        if (_waitingQueue.Count == 0) return;

        TraySpot freeSpot = _availableSpots.FirstOrDefault(s => s.isFree && !s.IsLocked);
        if (freeSpot != null)
        {
            Character currentCharacter = character != null ? 
                character : 
                _waitingQueue.Dequeue();
            
            freeSpot.isFree = false;
            
            currentCharacter.MoveTo(freeSpot.transform.position, () =>
            {
                StartCoroutine(ServeCharacter(currentCharacter, freeSpot));
            });
        }
    }

    private IEnumerator ServeCharacter(Character character, TraySpot spot)
    {
        float exp = _thisTraySO.ExperiencePerTrayOrder + (_thisTraySO.ExperiencePerTrayOrder * _thisTraySO.UpgradeLevel);
        float money = _thisTraySO.ProductUpgradeData.UpgradePrice[_thisTraySO.UpgradeLevel];

        PlayerProgressionSystem.Instance.BuyProduct((int)exp, (int)money, _thisTraySO.ProductType);
        var upgradeTray = GameObject.Find("UpgradeTray");
        if (upgradeTray != null)
        {
            upgradeTray.GetComponent<UpgradeTray>().UpdatePanel(_thisTraySO.TrayName);
        }

        yield return new WaitForSeconds(_thisTraySO.TimeToServe);

        spot.isFree = true;

        StartCoroutine(SendCharacterFromQueueToSpot());

        character.MoveTo(spot.IsLeft ? _leftEndPosition.position : _rightEndPosition.position, () =>
        {
            character.MoveTo(_spawnZone.position, () => Destroy(character.gameObject));
        });
    }

    private IEnumerator SendCharacterFromQueueToSpot()
    {
        yield return new WaitUntil(() => _queueController.IsQueueEmpty == false);
        yield return new WaitUntil(()=>_queueController.IsUpdateQueueInProgress == false);
        
        var newCharacter = _queueController.DequeueCharacter();
        TryMoveToSpot();
    }
}
