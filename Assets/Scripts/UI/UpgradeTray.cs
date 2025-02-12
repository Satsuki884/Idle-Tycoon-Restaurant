using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeTray : MonoBehaviour
{
    [SerializeField] private Button _buyNewResidentButton;
    [SerializeField] private Button _upgradeTrayButton;
    [SerializeField] private TMP_Text _costOfUpgradeText;
    [SerializeField] private TMP_Text _levelUpPriceText;

    private PlayerData playerData;
    private TrayDataSO trayData;

    private TrayData currentTrayData;
    private List<Resident> residents;

    private void Start()
    {
        // SetData();

        _buyNewResidentButton.onClick.RemoveAllListeners();
        _buyNewResidentButton.onClick.AddListener(BuyNewResident);
        _upgradeTrayButton.onClick.RemoveAllListeners();
        _upgradeTrayButton.onClick.AddListener(LevelUpTray);
    }
    private string _trayName;

    public void UpdatePanel(string trayName)
    {
        // Debug.Log(trayName);
        _trayName = trayName;
        SetData();
        SetPanelData();
    }


    private void SetData()
    {
        // Debug.Log(SaveManager.Instance);
        // Debug.Log(SaveManager.Instance.PlayerData);
        playerData = SaveManager.Instance.PlayerData;
        // Debug.Log(playerData.PlayerCoins);
        trayData = SaveManager.Instance.TrayData;
        // foreach (var tray in trayData.TrayData)
        // {
        //     Debug.Log(tray.TrayData.TrayName);
        // }
    }

    private void LevelUpTray()
    {
        trayData.TrayData.Find(tray => tray.TrayData.TrayName == _trayName).TrayData.UpgradeLevel++;
        SaveUpdate();
    }

    private void BuyNewResident()
    {
        foreach (var resident in residents)
        {
            if (!resident.IsActive)
            {
                resident.IsActive = true;
                break;
            }
        }
        trayData.TrayData.Find(tray => tray.TrayData.TrayName == _trayName).TrayData.SecondResidents = true;
        SaveUpdate();
    }

    private void SaveUpdate()
    {
        SaveManager.Instance.SaveTrayData(trayData);
        // SetAvailableResident();
        playerData.PlayerCoins -= currentTrayData.ProductUpgradeData.UpgradeCost[currentTrayData.UpgradeLevel];
        SaveManager.Instance.SavePlayerData(playerData);
        UpdatePanel(_trayName);
    }

    private void SetPanelData()
    {
        currentTrayData = trayData.TrayData
            .Find(tray => tray.TrayData.TrayName == _trayName)
            .TrayData;

        int upgradeLevel = currentTrayData.UpgradeLevel;
        var upgradeData = currentTrayData.ProductUpgradeData;

        _costOfUpgradeText.text = upgradeData.UpgradeCost[upgradeLevel].ToString();
        _levelUpPriceText.text = $"{upgradeData.UpgradePrice[upgradeLevel]} -> {upgradeData.UpgradePrice[upgradeLevel + 1]}";

        _buyNewResidentButton.interactable = !currentTrayData.SecondResidents &&
                                              currentTrayData.CostForSecondResidents <= playerData.PlayerCoins;

        bool canUpgrade = upgradeData.UpgradeCost[upgradeLevel] <= playerData.PlayerCoins;
        _upgradeTrayButton.interactable = canUpgrade;

        if (upgradeLevel == upgradeData.UpgradeCost.Count - 1)
        {
            _costOfUpgradeText.text = "Max Level";
            _upgradeTrayButton.interactable = false;
        }


    }
}