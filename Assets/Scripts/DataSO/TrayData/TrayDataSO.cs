using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "TrayDataSO", menuName = "ScriptableObjects/TrayDataSO", order = 1)]
[Serializable]
public class TrayDataSO : ScriptableObject
{
    [SerializeField] private List<TraySO> _trayData;
    public List<TraySO> TrayData
    {
        get
        {
            return _trayData;
        }
        set
        {
            _trayData = value;
        }
    }
}

[System.Serializable]
public class TrayDataWrapper
{
    [SerializeField] private List<TraySO> _trayData;
    public List<TraySO> TrayData
    {
        get
        {
            return _trayData;
        }
        set
        {
            _trayData = value;
        }
    }
}
