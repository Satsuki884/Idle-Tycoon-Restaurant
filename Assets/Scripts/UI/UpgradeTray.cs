using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeTray : MonoBehaviour
{
    // [SerializeField] private Button _buyNewResidentButton;
    [SerializeField] private Button _upgradeTrayButton;
    [SerializeField] private TMP_Text _costOfUpgradeText;
    [SerializeField] private TMP_Text _levelUpPriceText;

    private TrayDataSO trayData;

    private TrayData currentTrayData;

    [Header("UI view")]
    [SerializeField] private TMP_Text _currentLevelTrayText;
    [SerializeField] private TMP_Text _maxLevelTrayText;
    [SerializeField] private Image _productImage;
    // [SerializeField] private TMP_Text _countOfSellerText;
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private Slider _slider;

    [Header("Tray Name")]
    [SerializeField] private List<Tray> _tray;

    private void Start()
    {
        // SetData();

        // _buyNewResidentButton.onClick.RemoveAllListeners();
        // _buyNewResidentButton.onClick.AddListener(BuyNewResident);
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
        if (currentTrayData.UpgradeLevel == currentTrayData.ProductUpgradeData.UpgradeCost.Count - 2)
        {
            _costOfUpgradeText.text = "Max Level";
            _upgradeTrayButton.interactable = false;
        }
        else
        {
            trayData.TrayData.Find(tray => tray.TrayData.TrayName == _trayName).TrayData.UpgradeLevel++;
            SaveManager.Instance.SaveTrayData(trayData);
        }

        UpdatePanel(_trayName);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.UpgradeTrayMusic);

    }

    private void BuyNewResident()
    {
        PlayerProgressionSystem.Instance.BuySmth(trayData.TrayData.Find(tray => tray.TrayData.TrayName == _trayName).TrayData.CostForSecondResidents);
        trayData.TrayData.Find(tray => tray.TrayData.TrayName == _trayName).TrayData.SecondResidents = true;
        AudioManager.Instance.PlaySFX(AudioManager.Instance.BuySecondResidentMusic);
        SaveManager.Instance.SaveTrayData(trayData);
        // _tray.Find(tray => tray.TrayName == _trayName).SetSecondAvailableSpots();
        UpdatePanel(_trayName);
    }

    private void SetPanelData()
    {
        currentTrayData = trayData.TrayData
            .Find(tray => tray.TrayData.TrayName == _trayName)
            .TrayData;

        int upgradeLevel = currentTrayData.UpgradeLevel;
        var upgradeData = currentTrayData.ProductUpgradeData;

        // _costOfUpgradeText.text = upgradeData.UpgradeCost[upgradeLevel].ToString();


        // _buyNewResidentButton.interactable = !currentTrayData.SecondResidents &&
        //                                       currentTrayData.CostForSecondResidents <= PlayerProgressionSystem.Instance.GetPlayerMoney();

        bool canUpgrade = upgradeData.UpgradeCost[upgradeLevel] <= PlayerProgressionSystem.Instance.GetPlayerMoney();
        _upgradeTrayButton.interactable = canUpgrade;

        if (upgradeLevel == upgradeData.UpgradeCost.Count - 2)
        {
            _costOfUpgradeText.text = "Max Level";
            _upgradeTrayButton.interactable = false;
            _levelUpPriceText.text = "Current price is max.";
        }
        else
        {
            _costOfUpgradeText.text = upgradeData.UpgradeCost[upgradeLevel].ToString();
            _levelUpPriceText.text = $"{upgradeData.UpgradePrice[upgradeLevel]} -> {upgradeData.UpgradePrice[upgradeLevel + 1]}";
        }


        _currentLevelTrayText.text = upgradeLevel.ToString();
        _maxLevelTrayText.text = (upgradeData.UpgradeCost.Count - 2).ToString();
        _slider.minValue = 0;
        _slider.maxValue = upgradeData.UpgradeCost.Count-2;
        _slider.value = upgradeLevel;
        _productImage.sprite = currentTrayData.ProductImage;
        // if (currentTrayData.SecondResidents)
        // {
        //     _countOfSellerText.text = "2 / 2";
        // }
        // else
        // {
        //     _countOfSellerText.text = "1 / 2";
        // }
        _priceText.text = upgradeData.UpgradePrice[upgradeLevel].ToString();



    }
}