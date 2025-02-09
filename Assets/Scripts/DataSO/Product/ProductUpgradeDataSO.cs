using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProductUpgradeData", menuName = "ScriptableObjects/ProductUpgradeData", order = 1)]
public class ProductUpgradeDataSO : ScriptableObject
{
    [SerializeField] private ProductType _trayData;
    public ProductType TrayData => _trayData;

    [SerializeField] private List<int> _upgradeCost;
    public List<int> UpgradeCost => _upgradeCost;

    [SerializeField] private List<int> _upgradePrice;
    public List<int> UpgradePrice => _upgradePrice;
}