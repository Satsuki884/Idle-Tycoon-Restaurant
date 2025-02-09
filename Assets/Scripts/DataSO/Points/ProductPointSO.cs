using UnityEngine;

[CreateAssetMenu(fileName = "NewProductPoint", menuName = "ScriptableObjects/ProductPoint", order = 1)]
public class ProductPointSO : ScriptableObject
{
    [SerializeField] private string _productName;
    [SerializeField] private Vector3 _productPoint;
    [SerializeField] private bool _isProductFree;
}