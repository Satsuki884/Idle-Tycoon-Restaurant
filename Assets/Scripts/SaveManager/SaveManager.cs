using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;

public class SaveManager : MonoBehaviour
{
    private string _savePlayerDataPath;
    private string _saveTrayDataPath;

    public static SaveManager _instance;
    public static SaveManager Instance => _instance;

    public async Task Initialize(params object[] param)
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;


        await Task.Delay(100);
    }


    [SerializeField] private TrayDataSO _trayData;
    public TrayDataSO TrayData
    {
        get
        {
            // if (_playerData == null)
            // {
            //     _trayData = LoadTrayData();
            // }
            // return _trayData;
            return LoadTrayData();
        }
    }

    [SerializeField] private PlayerDataSO _playerData;
    public PlayerData PlayerData
    {
        get
        {
            // if (_playerData == null)
            // {
            //     _playerData.PlayerData = LoadPlayerData();
            // }
            // return _playerData.PlayerData;
            return LoadPlayerData();
        }
    }


    private void Awake()
    {
        _savePlayerDataPath = Path.Combine(Application.persistentDataPath, "playerdata.json");
        Debug.Log(_savePlayerDataPath);
        _saveTrayDataPath = Path.Combine(Application.persistentDataPath, "traydata.json");
        SavePlayerData(_playerData.PlayerData);
        SaveTrayData(_trayData);
        LoadPlayerData();
        LoadTrayData();
    }

    public void SavePlayerData(PlayerData value)
    {
        if (!File.Exists(_savePlayerDataPath))
        {
            File.Create(_savePlayerDataPath).Dispose();
        }

        string json = JsonUtility.ToJson(value, true);

        File.WriteAllText(_savePlayerDataPath, json);
        _playerData.PlayerData = value;
        // Debug.Log("Player data saved successfully.");
    }

    public PlayerData LoadPlayerData()
    {
        if (!File.Exists(_savePlayerDataPath))
        {
            SavePlayerData(_playerData.PlayerData);
        }
        string json = File.ReadAllText(_savePlayerDataPath);
        PlayerData playerData = JsonUtility.FromJson<PlayerData>(json);
        // Debug.Log("Player data loaded successfully.");
        return playerData;
    }
    public void SaveTrayData(TrayDataSO value)
    {
        if (!File.Exists(_saveTrayDataPath))
        {
            File.Create(_saveTrayDataPath).Dispose();
        }
        string json = JsonUtility.ToJson(new TrayDataWrapper{
            TrayData = value.TrayData
        }, true);
        File.WriteAllText(_saveTrayDataPath, json);
        _trayData = value;
        // Debug.Log("Tray data saved successfully.");
    }

    private TrayDataWrapper _trayDataWrapper;

    public TrayDataSO LoadTrayData()
    {
        if (!File.Exists(_saveTrayDataPath))
        {
            SaveTrayData(_trayData);
        }
        string json = File.ReadAllText(_saveTrayDataPath);
        _trayDataWrapper = JsonUtility.FromJson<TrayDataWrapper>(json);
        // Debug.Log("Tray data loaded successfully.");
        return ConvertToTrayDataSO();
    }

    private TrayDataSO ConvertToTrayDataSO()
        {
            TrayDataSO trayData = null;
            if (_trayDataWrapper != null)
            {
                trayData = ScriptableObject.CreateInstance<TrayDataSO>();
                trayData.TrayData = _trayDataWrapper.TrayData;
            }
            return trayData;
        }

}