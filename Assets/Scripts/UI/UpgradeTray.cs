using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeTray : MonoBehaviour
{
    [SerializeField] private Button _upgradeTrayButton;
    [SerializeField] private TMP_Text _costOfUpgradeText;
    [SerializeField] private TMP_Text _levelUpPriceText;

    private TrayDataSO trayData;

    private TrayData currentTrayData;

    [Header("UI view")]
    [SerializeField] private TMP_Text _currentLevelTrayText;
    [SerializeField] private TMP_Text _maxLevelTrayText;
    [SerializeField] private Image _productImage;
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private Slider _slider;

    private void Start()
    {
        _upgradeTrayButton.onClick.RemoveAllListeners();
        _upgradeTrayButton.onClick.AddListener(LevelUpTray);
    }
    private string _trayName;

    public void UpdatePanel(string trayName)
    {
        _trayName = trayName;
        SetData();
        SetPanelData();
    }


    private void SetData()
    {
        trayData = SaveManager.Instance.TrayData;
    }

    private void LevelUpTray()
    {
        PlayerProgressionSystem.Instance.BuySmth(currentTrayData.ProductUpgradeData.UpgradeCost[currentTrayData.UpgradeLevel]);
        trayData.TrayData.Find(tray => tray.TrayData.TrayName == _trayName).TrayData.UpgradeLevel++;
        SaveManager.Instance.SaveTrayData(trayData);
        UpdatePanel(_trayName);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.UpgradeTrayMusic);

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

        bool canUpgrade = upgradeData.UpgradeCost[upgradeLevel] <= PlayerProgressionSystem.Instance.GetPlayerMoney();
        _upgradeTrayButton.interactable = canUpgrade;

        if (upgradeLevel == upgradeData.UpgradeCost.Count - 1)
        {
            _costOfUpgradeText.text = "Max Level";
            _upgradeTrayButton.interactable = false;
        }

        _currentLevelTrayText.text = upgradeLevel.ToString();
        _maxLevelTrayText.text = upgradeData.UpgradeCost.Count.ToString();
        _slider.minValue = 0;
        _slider.maxValue = upgradeData.UpgradeCost.Count;
        _slider.value = upgradeLevel;
        _productImage.sprite = currentTrayData.ProductImage;
        _priceText.text = upgradeData.UpgradePrice[upgradeLevel].ToString();



    }
}