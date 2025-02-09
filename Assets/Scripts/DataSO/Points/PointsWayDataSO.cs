using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PointsWayData", menuName = "ScriptableObjects/PointsWay", order = 1)]
public class PointsWayDataSO : ScriptableObject
{
    [SerializeField] private string wayName;
    [SerializeField] private List<Vector3> _startPoints;
    [SerializeField] private ProductPointsSO _productPoints;
    [SerializeField] private List<Vector3> _endPoints;

    public string WayName => wayName;
    public List<Vector3> StartPoints => _startPoints;
    public ProductPointsSO ProductPoints => _productPoints;
    public List<Vector3> EndPoints => _endPoints;
}
