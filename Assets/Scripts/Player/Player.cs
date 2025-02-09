using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;
    public PlayerData PlayerData { get => _playerData; set => _playerData = value; }
}