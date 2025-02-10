using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private CharacterBrain _characterBrain;
    private ProductType _selectedProductType;
    private Tray _selectedTray;
    public float moveSpeed = 2f;
    private Vector3[] _allPath;
    private string _pathToQueue = "PathToQueue";
    private string _pathfrom = "PathFromQueue";

    public void Initialize()
    {
        // _characterBrain.Initialize(this);
        _selectedProductType = _characterBrain.SelectProduct();
        _selectedTray = FindTray();
        Debug.Log("Character: "+this + " selected: "+_selectedTray);

        AddCharacterToQueue();

    }

    public IEnumerator InitializeAsync()
    {
        _characterBrain.Initialize(this);
        _selectedProductType = _characterBrain.SelectProduct();
        _selectedTray = FindTray();
        yield return null;
        
        AddCharacterToQueue();
    }

    private void SetPath()
    {
        List<Vector3> newPath = new List<Vector3>();
        foreach (Vector3 point in _selectedTray.StartPositions)
        {
            newPath.Add(point);
        }
        newPath.Add(_selectedTray.QueuePoints);
        StartCoroutine(MoveTo(newPath.ToArray()));
    }

    IEnumerator MoveTo(Vector3[] path)
    {
        foreach (Vector3 point in path)
        {
            while (Vector3.Distance(transform.position, point) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, point, moveSpeed * Time.deltaTime);
                yield return null;
            }
        }
    }

    private Tray FindTray()
    {
        Tray[] trays = FindObjectsOfType<Tray>();
        foreach (Tray tray in trays)
        {
            if (tray.ProductType == _selectedProductType)
            {
                return tray;
            }
        }
        return null;
    }

    public void AddCharacterToQueue()
    {
        if (_selectedTray.IsQueueFool)
        {
            return;
        }
        else
        {
            _selectedTray.QueueManager.AddToQueue(this);
            SetPath();
        }
    }
}