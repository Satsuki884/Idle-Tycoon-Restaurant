using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class TraySpot
{
    public Vector3 position;
    public bool isFree;
}

public class Tray : MonoBehaviour
{
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
