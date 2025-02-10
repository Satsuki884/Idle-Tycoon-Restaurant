using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class TraySpot
{
    public Vector3 position;
    public bool isFree;
}

public class Tray : MonoBehaviour
{
    [SerializeField] private string _trayName;
    public string TrayName => _trayName;
    [SerializeField] private Vector3[] _startPositions;
    public Vector3[] StartPositions => _startPositions;
    [SerializeField] private Vector3[] _endPositions;
    public Vector3[] EndPositions => _endPositions;
    [SerializeField] private Vector3 _queuePoints;
    public Vector3 QueuePoints => _queuePoints;
    public Vector3[] availableSpots;
    [SerializeField] private TraySpot[] _traySpot;
    public TraySpot[] TraySpot => _traySpot;
    [SerializeField] private int incomePerOrder = 5;
    [SerializeField] private QueueManager _queueManager;
    [SerializeField] private int _levelForUnlock;
    public bool IsActiveToPurchase()
    {
        if (_levelForUnlock <= SaveManager.Instance.PlayerData.PlayerLevel)
            {
                return true;
            }
            else
            {
                return false;
            }
    }// => _isActiveToPurchase;
    [SerializeField] private bool _isActive;
    public bool IsActive
    {
        get => _isActive;
        set
        {
            _isActive = value;
            foreach (TraySO tray in trayData.TrayData)
            {
                if (tray.TrayData.TrayName == _trayName)
                {
                    tray.TrayData.IsActive = value;
                }
            }
            SaveManager.Instance.SaveTrayData(trayData);
        }
    }
    [SerializeField] private int _cost;
    public int Cost => _cost;
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

    private void Start()
    {
        trayData = SaveManager.Instance.TrayData;
        foreach (TraySO tray in trayData.TrayData)
        {
            if (tray.TrayData.TrayName == _trayName)
            {
                _isActive = tray.TrayData.IsActive;
                _cost = tray.TrayData.Cost;
                _productType = tray.TrayData.ProductType;
                _levelForUnlock = tray.TrayData.LevelForUnlock;
            }
        }
    }
    public bool QueueIsFull()
    {
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
    public void SetSpot()
    {
        Character character = QueueManager.WaitingQueue.Peek();
        List<Vector3> newPath = new List<Vector3>();
        foreach (TraySpot spot in _traySpot)
        {
            if (spot.isFree)
            {
                spot.isFree = false;
                newPath.Add(character.transform.position);
                newPath.Add(spot.position);
                StartCoroutine(MoveToTray(newPath.ToArray(), character.moveSpeed, spot));
            }
        }
    }

    IEnumerator MoveToTray(Vector3[] path, float moveSpeed, TraySpot spot)
    {
        QueueManager.WaitingQueue.Dequeue();
        foreach (Vector3 point in path)
        {
            while (Vector3.Distance(QueueManager.WaitingQueue.Peek().transform.position, point) > 0.1f)
            {
                QueueManager.WaitingQueue.Peek().transform.position = Vector3.MoveTowards(QueueManager.WaitingQueue.Peek().transform.position, point, moveSpeed * Time.deltaTime);
                yield return null;
            }
        }
        spot.isFree = true;
    }


}
