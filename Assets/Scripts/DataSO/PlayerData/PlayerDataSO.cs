using System;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerDataSO", menuName = "ScriptableObjects/PlayerDataSO", order = 1)]
[Serializable]
public class PlayerDataSO : ScriptableObject
{
    [SerializeField] private PlayerData _playerData;
    public PlayerData PlayerData { get => _playerData; set => _playerData = value; }

    // public void AddExperience(int amount)
    // {
    //     playerExperience += amount;
    //     CheckLevelUp();
    // }

    // public void AddCoins(int amount)
    // {
    //     playerCoins += amount;
    // }

    // public void AddBlueBottle(int amount)
    // {
    //     BlueBottle += amount;
    // }

    // public void AddGreenBottle(int amount)
    // {
    //     GreenBottle += amount;
    // }

    // public void AddRedBottle(int amount)
    // {
    //     RedBottle += amount;
    // }

    // public void AddBrounBottle(int amount)
    // {
    //     BrounBottle += amount;
    // }

    // public void AddFood(int amount)
    // {
    //     Food += amount;
    // }

    // public void AddMushrooms(int amount)
    // {
    //     Mushrooms += amount;
    // }

    // private void CheckLevelUp()
    // {
    //     int experienceToNextLevel = playerLevel * 100;
    //     if (playerExperience >= experienceToNextLevel)
    //     {
    //         playerExperience -= experienceToNextLevel;
    //         playerLevel++;
    //     }
    // }
}

[System.Serializable]
public class PlayerData
{
    [SerializeField] private int _playerLevel;
    public int PlayerLevel { get => _playerLevel; set => _playerLevel = value; }
    [SerializeField] private int _maxLevel;
    public int MaxLevel { get => _maxLevel;}
    [SerializeField] private int _playerExperience;
    public int PlayerExperience { get => _playerExperience; set => _playerExperience = value; }
    [SerializeField] private int _playerCoins;
    public int PlayerCoins { get => _playerCoins; set => _playerCoins = value; }
    [SerializeField] private int _blueBottle;
    public int BlueBottle { get => _blueBottle; set => _blueBottle = value; }
    [SerializeField] private int _greenBottle;
    public int GreenBottle { get => _greenBottle; set => _greenBottle = value; }
    [SerializeField] private int _redBottle;
    public int RedBottle { get => _redBottle; set => _redBottle = value; }
    [SerializeField] private int _brounBottle;
    public int BrounBottle { get => _brounBottle; set => _brounBottle = value; }
    [SerializeField] private int _chiken;
    public int Chicken { get => _chiken; set => _chiken = value; }
    [SerializeField] private int _mushrooms;
    public int Mushrooms { get => _mushrooms; set => _mushrooms = value; }

    [SerializeField] private long _lastLongTime;
    public long LastLongTime { get => _lastLongTime; set => _lastLongTime = value; }
}