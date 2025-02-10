using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTray", menuName = "ScriptableObjects/TraySO", order = 1)]
public class TraySO : ScriptableObject
{
    [SerializeField] private TrayData _trayData;
    public TrayData TrayData
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
public class TrayData
{
    [SerializeField] private GameObject _trayPrefab;
    public GameObject TrayPrefab => _trayPrefab;
    [SerializeField] private string _trayName;
    public string TrayName => _trayName;
    [SerializeField] private ProductType _productType;
    public ProductType ProductType => _productType;
    [SerializeField] private List<Resident> _residents;
    public List<Resident> Residents
    {
        get
        {
            return _residents;
        }
        set
        {
            _residents = value;
        }
    }
    [SerializeField] private int _levelForUnlock;
    public int LevelForUnlock => _levelForUnlock;
    [SerializeField] private float _timeToServe;
    public float TimeToServe => _timeToServe;
    [SerializeField] private ProductUpgradeDataSO _productUpgradeData;
    public ProductUpgradeDataSO ProductUpgradeData => _productUpgradeData;
    [SerializeField] private int _upgradeLevel;
    public bool IsActiveForPurchase{
        get
        {
            if (_levelForUnlock <= SaveManager.Instance.PlayerData.PlayerLevel)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public int UpgradeLevel
    {
        get
        {
            return _upgradeLevel;
        }
        set
        {
            _upgradeLevel = value;
        }
    }
    [SerializeField] private bool _isActive;
    public bool IsActive{
        get
        {
            return _isActive;
        }
        set
        {
            _isActive = value;
        }
    }

    [SerializeField] private int _cost;
    public int Cost => _cost;
}
