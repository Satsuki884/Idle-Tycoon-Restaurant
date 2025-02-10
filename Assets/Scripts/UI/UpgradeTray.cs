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
        SetPanelData(trayName);
    }


    private void SetData()
    {
        Debug.Log(SaveManager.Instance);
        Debug.Log(SaveManager.Instance.PlayerData);
        playerData = SaveManager.Instance.PlayerData;
        Debug.Log(playerData.PlayerCoins);
        trayData = SaveManager.Instance.TrayData;
        foreach (var tray in trayData.TrayData)
        {
            Debug.Log(tray.TrayData.TrayName);
        }
    }

    private void LevelUpTray()
    {
        throw new NotImplementedException();
    }

    private void BuyNewResident()
    {
        foreach (var resident in residents)
        {
            if(!resident.IsActive)
            {
                resident.IsActive = true;
                break;
            }
        }
        trayData.TrayData.Find(tray => tray.TrayData.TrayName == gameObject.name).TrayData.Residents = residents;
        trayData.TrayData.Find(tray => tray.TrayData.TrayName == gameObject.name).TrayData.LevelUp();
        SaveManager.Instance.SaveTrayData(trayData);
        playerData.PlayerCoins -= currentTrayData.ProductUpgradeData.UpgradeCost[currentTrayData.UpgradeLevel];
        SaveManager.Instance.SavePlayerData(playerData);
        UpdatePanel(_trayName);
    }

    private void SetPanelData(string trayName)
    {
        Debug.Log(trayData);
        currentTrayData = trayData.TrayData.Find(tray => tray.TrayData.TrayName == trayName).TrayData;
        Debug.Log(currentTrayData);

        if(currentTrayData.UpgradeLevel == currentTrayData.ProductUpgradeData.UpgradeCost.Count)
        {
            _costOfUpgradeText.text = "Max Level";
            _upgradeTrayButton.interactable = false;
        } else {
            _costOfUpgradeText.text = currentTrayData.ProductUpgradeData.UpgradeCost[currentTrayData.UpgradeLevel].ToString();
            _upgradeTrayButton.interactable = true;
        }

        if(currentTrayData.ProductUpgradeData.UpgradeCost[currentTrayData.UpgradeLevel] > playerData.PlayerCoins)
        {
            _upgradeTrayButton.interactable = false;
        }
        {
            _buyNewResidentButton.interactable = true;
        }

        residents = currentTrayData.Residents;
        foreach (var resident in residents)
        {
            if(!resident.IsActive)
            {
                _buyNewResidentButton.interactable = true;
                break;
            }
        }
    }
}