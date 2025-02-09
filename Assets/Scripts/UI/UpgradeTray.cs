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
    private List<TrayData> trayData;

    private TrayData currentTrayData;
    private List<Resident> residents;

    private void Start()
    {
        SetData();

        _buyNewResidentButton.onClick.RemoveAllListeners();
        _buyNewResidentButton.onClick.AddListener(BuyNewResident);
        _upgradeTrayButton.onClick.RemoveAllListeners();
        _upgradeTrayButton.onClick.AddListener(LevelUpTray);
    }
    private string _trayName;

    public void UpdatePanel(string trayName)
    {
        _trayName = trayName;
        SetData();
        SetPanelData(trayName);
    }


    private void SetData()
    {
        playerData = GameManager.Instance.SaveManager.PlayerData;
        trayData = GameManager.Instance.SaveManager.TrayData;
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
        trayData.Find(tray => tray.TrayName == gameObject.name).Residents = residents;
        GameManager.Instance.SaveManager.SaveTrayData(trayData);
        UpdatePanel(_trayName);
    }

    private void SetPanelData(string trayName)
    {
        Debug.Log(trayData[0]);
        foreach (var tray in trayData)
        {
            Debug.Log(tray.TrayName);
        }
        currentTrayData = trayData.Find(tray => tray.TrayName == trayName);
        Debug.Log(currentTrayData);

        if(currentTrayData.UpgradeLevel == currentTrayData.ProductUpgradeData.UpgradeCost.Count)
        {
            _costOfUpgradeText.text = "Max Level";
            _upgradeTrayButton.interactable = false;
        } else {
            _costOfUpgradeText.text = currentTrayData.ProductUpgradeData.UpgradeCost[currentTrayData.UpgradeLevel].ToString();
            _upgradeTrayButton.interactable = true;
        }

        if(currentTrayData.ProductUpgradeData.UpgradeCost[currentTrayData.UpgradeLevel] > playerData.playerCoins)
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