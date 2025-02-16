using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

[System.Serializable]
public class TrayPoint
{
    // public Vector3 position;
    public Transform position;
    public bool isFree = true;
}


public class Tray : MonoBehaviour
{
    [SerializeField] private string _trayName;
    [SerializeField] public string TrayName => _trayName;
    [SerializeField] private Transform _rightEndPosition;
    [SerializeField] private TrayPoint _queuePoint;
    [SerializeField] public Transform _spawnZone;
    [SerializeField] private TrayPoint _availableSpots;
    [SerializeField] private Transform _spawnListZone;
    private Queue<Character> _waitingQueue = new Queue<Character>();
    [SerializeField] private SpawnManager _spawnManager;
    [SerializeField] private ProductType _productType;
    private TrayDataSO trayData;
    private TrayData _thisTraySO;

    private void Start()
    {
        SetCurrentTrayData();
        StartCoroutine(SpawnCharacters());
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
    }
    private Character _character;

    private IEnumerator SpawnCharacters()
    {
        while (true)
        {
            if (_thisTraySO.IsActive && _queuePoint.isFree)
            {
                _queuePoint.isFree = false;
                _character = _spawnManager.SpawnCharacters(_spawnZone, _spawnListZone);
                _waitingQueue.Enqueue(_character);
                AddCharacterToQueue(_character);
                _character = null;
            }
            yield return new WaitForSeconds(_thisTraySO.TimeToServe / 2);
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

        if (_availableSpots.isFree)
        {
            _queuePoint.isFree = true;
            _currentCharacter = _waitingQueue.Dequeue();
            _availableSpots.isFree = false;
            _currentCharacter.MoveTo(_availableSpots.position.position, () =>
            {
                StartCoroutine(ServeCharacter(_currentCharacter));
            });
        }
    }

    private IEnumerator ServeCharacter(Character character)
    {
        var _tryExp = _thisTraySO.ExperiencePerTrayOrder;
        _tryExp += _thisTraySO.ExperiencePerTrayOrder * _thisTraySO.UpgradeLevel;

        var _tryMoney = _thisTraySO.ProductUpgradeData.UpgradePrice[_thisTraySO.UpgradeLevel];

        PlayerProgressionSystem.Instance.BuyProduct(_tryExp, _tryMoney, _thisTraySO.ProductType);
        yield return new WaitForSeconds(_thisTraySO.TimeToServe);
        _availableSpots.isFree = true;
        TryMoveToSpot();
        character.MoveTo(_rightEndPosition.position, () =>
        {
            character.MoveTo(_spawnZone.position, () =>
            {
                Destroy(character.gameObject);
            });
        });
    }
}
