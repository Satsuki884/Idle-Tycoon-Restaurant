using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SalesManager : MonoBehaviour
{
    TrayDataSO _trayData;
    PlayerProgressionSystem _playerProgressionSystem;
    SaveManager _saveManager;
    InventorySystem _inventorySystem;
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
        _saveManager = SaveManager.Instance;
        _trayData = SaveManager.Instance.TrayData;
        _playerProgressionSystem = PlayerProgressionSystem.Instance;
        _inventorySystem = InventorySystem.Instance;
    }

    // public void BuyProduct(int exp, int money, TrayData trayData)
    // {
    //     _trayData.TrayData.Find(tray => tray.TrayData == trayData).TrayData.ItemCount++;
    //     _saveManager.SaveTrayData(_trayData);
    //     _playerProgressionSystem.SellProduct(exp, money);
    //     _inventorySystem.RefreshInventory();
    // }
}