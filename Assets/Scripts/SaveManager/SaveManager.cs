using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private string _savePlayerDataPath;
    private string _saveTrayDataPath;

    [SerializeField] private List<Tray> _trayData;
    public List<TrayData> TrayData
    {
        get
        {
            return LoadTrayData();
        }
    }

    [SerializeField] private Player Player;
    public PlayerData PlayerData
    {
        get
        {
            return LoadPlayerData();
        }
        set
        {
            Player.PlayerData = value;
        }
    }


    private void Awake()
    {
        _savePlayerDataPath = Path.Combine(Application.persistentDataPath, "playerdata.json");
        _saveTrayDataPath = Path.Combine(Application.persistentDataPath, "traydata.json");
    }

    public void SavePlayerData(PlayerData playerData)
    {
        if (!File.Exists(_savePlayerDataPath))
        {
            File.Create(_savePlayerDataPath).Dispose();
        }

        string json = JsonUtility.ToJson(playerData, true);

        File.WriteAllText(_savePlayerDataPath, json);
        Player.PlayerData = playerData;
        Debug.Log("Player data saved successfully.");
    }

    public PlayerData LoadPlayerData()
    {
        if (!File.Exists(_savePlayerDataPath))
        {
            SavePlayerData(Player.PlayerData);
        }
        string json = File.ReadAllText(_savePlayerDataPath);
        PlayerData playerData = JsonUtility.FromJson<PlayerData>(json);
        Debug.Log("Player data loaded successfully.");
        return playerData;
    }
    public void SaveTrayData(List<TrayData> trayData)
    {
        if (!File.Exists(_saveTrayDataPath))
        {
            File.Create(_saveTrayDataPath).Dispose();
        }
        string json = JsonUtility.ToJson(trayData, true);
        File.WriteAllText(_saveTrayDataPath, json);
        Debug.Log("Tray data saved successfully.");
    }

    public List<TrayData> LoadTrayData()
    {
        if (!File.Exists(_saveTrayDataPath))
        {
            List<TrayData> trayDataList = new List<TrayData>();
            foreach (var tray in _trayData)
            {
                trayDataList.Add(tray.TrayData);
            }
            SaveTrayData(trayDataList);
        }
        string json = File.ReadAllText(_saveTrayDataPath);
        List<TrayData> trayData = JsonUtility.FromJson<List<TrayData>>(json);
        Debug.Log("Tray data loaded successfully.");
        return trayData;
    }

}