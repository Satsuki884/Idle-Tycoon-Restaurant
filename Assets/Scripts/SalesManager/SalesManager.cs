using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SalesManager : MonoBehaviour
{
    PlayerData _playerData;
    TrayDataSO _trayData;
    PlayerProgressionSystem _playerProgressionSystem;
    public static SalesManager _instance;
    public static SalesManager Instance => _instance;

    public async Task Initialize(params object[] param)
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;


        await Task.Delay(100);
    }
    void Start()
    {
        _playerData = SaveManager.Instance.PlayerData;
        _trayData = SaveManager.Instance.TrayData;
        _playerProgressionSystem = PlayerProgressionSystem.Instance;
    }

    public void BuyProduct(int exp, int money, TrayData trayData)
    {
        _trayData.TrayData.Find(tray => tray.TrayData == trayData).TrayData.ItemCount++;
        SaveManager.Instance.SaveTrayData(_trayData);
        _playerProgressionSystem.SellProduct(exp, money);
        InventorySystem.Instance.RefreshInventory();
    }
}