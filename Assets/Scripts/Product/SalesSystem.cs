using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class SalesSystem : MonoBehaviour
{
    public static SalesSystem _instance;
    public static SalesSystem Instance => _instance;
    [SerializeField] private TMP_Text _moneyText;
    private PlayerData _playerData;

    public async Task Initialize(params object[] param)
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;


        await Task.Delay(100);
    }

//     public void Start()
//     {
//         _playerData = SaveManager.Instance.PlayerData;
//         UpdatedPlayerMoney();
//     }

//     public void UpdatedPlayerMoney()
//     {
//         int coins = _playerData.PlayerCoins;
//         _moneyText.text = FormatNumber(coins);
//     }
//     public string FormatNumber(int num)
//     {
//         if (num >= 1_000_000)
//             return (num / 1_000_000f).ToString("0.#") + "m";
//         if (num >= 1_000)
//             return (num / 1_000f).ToString("0.#") + "k";

//         return num.ToString();
//     }

//     public void BuyProduct(int money, ProductType productType)
//     {
//         _playerData.PlayerCoins += money;
//         switch (productType)
//         {
//             case ProductType.BlueBottle:
//                 _playerData.BlueBottle++;
//                 break;
//             case ProductType.GreenBottle:
//                 _playerData.GreenBottle++;
//                 break;
//             case ProductType.RedBottle:
//                 _playerData.RedBottle++;
//                 break;
//             case ProductType.BrownBottle:
//                 _playerData.BrounBottle++;
//                 break;
//             case ProductType.Chicken:
//                 _playerData.Chicken++;
//                 break;
//             case ProductType.Mushrooms:
//                 _playerData.Mushrooms++;
//                 break;
//         }
//         SaveManager.Instance.SavePlayerData(_playerData);
//         UpdatedPlayerMoney();
//         InventorySystem.Instance.RefreshInventory();
//     }
}