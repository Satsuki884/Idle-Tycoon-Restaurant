using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class TrayData
{
    [SerializeField] private string _trayName;
    public string TrayName => _trayName;
    [SerializeField] private ProductType _productType;
    public ProductType ProductType => _productType;
    [SerializeField] private List<Resident> _residents;
    public List<Resident> Residents => _residents;
    [SerializeField] private float _timeToServe;
    public float TimeToServe => _timeToServe;
    [SerializeField] private int _unlockLevel;
    public int UnlockLevel => _unlockLevel;
    [SerializeField] private ProductUpgradeDataSO _productUpgradeData;
    public ProductUpgradeDataSO ProductUpgradeData => _productUpgradeData;
    [SerializeField] private int _upgradeLevel;
    public int UpgradeLevel => _upgradeLevel;
    [SerializeField] private bool _isActive;
    public bool IsActive
    {
        get
        {
            return CheckIsActive();
        }
    }

    private bool CheckIsActive()
    {
        return _isActive;
    }

    



}