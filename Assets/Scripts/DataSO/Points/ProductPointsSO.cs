using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProductPoints", menuName = "ScriptableObjects/ProductPointsSO", order = 1)]
public class ProductPointsSO : ScriptableObject
{
    [SerializeField] private string _productName;
    [SerializeField] private List<ProductPointSO> _productPoints;

    public string ProductName => _productName;
    public List<ProductPointSO> ProductPoints => _productPoints;
}