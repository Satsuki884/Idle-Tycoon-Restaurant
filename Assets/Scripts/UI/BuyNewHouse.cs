// using System;
// using System.Collections;
// using System.Collections.Generic;
// using TMPro;
// using UnityEngine;
// using UnityEngine.UI;

// public class BuyNewHouse : MonoBehaviour
// {
//     [SerializeField] private GameObject _buyPanel;
//     [SerializeField] private TMP_Text _productText;
//     [SerializeField] private Button _closePanelButton;
//     [SerializeField] private Button _buyButton;
//     [SerializeField] private TMP_Text _costText;

//     private void Start()
//     {
//         if (_buyPanel != null)
//         {
//             _buyPanel.SetActive(false);
//         }
//         _closePanelButton.onClick.RemoveAllListeners();
//         _closePanelButton.onClick.AddListener(CloseUpgradePanel);
//         _buyButton.onClick.RemoveAllListeners();
//         _buyButton.onClick.AddListener(BuyHouse);
//     }

//     private void BuyHouse()
//     {
//         CreationManager.Instance.BuyTray(gameObject.name);
//         CloseUpgradePanel();
//     }

//     void OnMouseDown() 
//     {
//         if (_buyPanel != null)
//         {
//             bool isActive = _buyPanel.activeSelf;
//             _productText.text = "Buy " + gameObject.name + " ?";
//             _costText.text = CreationManager.Instance.GetTrayCost(gameObject.name).ToString();
//             if (CreationManager.Instance.GetTrayCost(gameObject.name) > SaveManager.Instance.PlayerData.PlayerCoins)
//             {
//                 _buyButton.interactable = false;
//             }
//             else
//             {
//                 _buyButton.interactable = true;
//             }
//             _buyPanel.SetActive(!isActive);
//         }
//     }

//     void CloseUpgradePanel()
//     {
//         if (_buyPanel != null)
//         {
//             _buyPanel.SetActive(false);
//         }
//     }
// }
