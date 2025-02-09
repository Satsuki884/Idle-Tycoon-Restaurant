using UnityEngine;

public class Resident : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private bool _isActive;
    public bool IsActive{
        get{
            return _isActive;
        }
        set{
            _isActive = value;
        }
    }
}