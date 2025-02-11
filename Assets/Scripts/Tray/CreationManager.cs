// using System.Collections;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using TMPro;
// using UnityEngine;

// public class CreationManager : MonoBehaviour
// {
//     public static CreationManager _instance;
//     public static CreationManager Instance => _instance;
//     [SerializeField] private GameObject[] _addTray;
//     [SerializeField] private Tray[] _trays;

//     private TrayDataSO trayData;

//     public async Task Initialize(params object[] param)
//     {
//         if (_instance != null)
//         {
//             return;
//         }

//         _instance = this;


//         await Task.Delay(100);
//     }

//     public void Start()
//     {
//         trayData = SaveManager.Instance.TrayData;
//         Debug.Log(trayData.TrayData.Count);

//         foreach (var tr in _addTray)
//         {
//             tr.SetActive(false);
//         }

//         foreach (var tray in trayData.TrayData)
//         {
//             if (!tray.TrayData.IsActive)
//             {
//                 Debug.Log(tray.TrayData.TrayName);
//                 foreach (var tr in _trays)
//                 {
//                     if (tr.TrayName == tray.TrayData.TrayName)
//                     {
//                         tr.gameObject.SetActive(false);
//                         break;
//                     }
//                 }
//                 foreach (var tr in _addTray)
//                 {
//                     if (tr.name == tray.TrayData.TrayName && IsAvailableForPurchase(tray.TrayData.LevelForUnlock))
//                     {
//                         tr.SetActive(true);
//                         break;
//                     }

//                 }
//             }
//             else
//             {
//                 foreach (var tr in _trays)
//                 {
//                     if (tr.TrayName == tray.TrayData.TrayName)
//                     {
//                         tr.gameObject.SetActive(true);
//                         break;
//                     }
//                 }

//                 foreach (var tr in _addTray)
//                 {
//                     if (tr.name == tray.TrayData.TrayName)
//                     {
//                         tr.SetActive(false);
//                         break;
//                     }
//                 }
//             }
//         }
//     }

//     public bool IsAvailableForPurchase(int levelForUnlock)
//     {
//         if (levelForUnlock <= SaveManager.Instance.PlayerData.PlayerLevel)
//         {
//             return true;
//         }
//         return false;
//     }

//     public int GetTrayCost(string trayName)
//     {
//         foreach (var tray in trayData.TrayData)
//         {
//             if (tray.TrayData.TrayName == trayName)
//             {
//                 return tray.TrayData.Cost;
//             }
//         }
//         return 0;
//     }

//     public void BuyTray(string trayName)
//     {
//         foreach (var tray in trayData.TrayData)
//         {
//             if (tray.TrayData.TrayName == trayName)
//             {
//                 tray.TrayData.IsActive = true;
//                 SaveManager.Instance.SaveTrayData(trayData);
//                 var playerData = SaveManager.Instance.PlayerData;
//                 playerData.PlayerCoins -= tray.TrayData.Cost;
//                 SaveManager.Instance.SavePlayerData(playerData);
//                 foreach (var tr in _trays)
//                 {
//                     if (tr.TrayName == trayName)
//                     {
//                         tr.gameObject.SetActive(true);
//                     }
//                 }

//                 foreach (var tr in _addTray)
//                 {
//                     if (tr.name == trayName)
//                     {
//                         tr.SetActive(false);
//                     }
//                 }

//             }
//         }

//     }
// }
