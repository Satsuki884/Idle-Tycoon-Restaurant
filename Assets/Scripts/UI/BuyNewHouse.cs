using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuyNewHouse : MonoBehaviour
{
    [SerializeField] private GameObject _buyPanel;
    [SerializeField] private TMP_Text _productText;
    [SerializeField] private Button _closePanelButton;
    [SerializeField] private Button _buyButton;
    [SerializeField] private TMP_Text _costText;
    [SerializeField] private string _nameText;

    [SerializeField] private List<GameObject> _trays;

    private TrayDataSO _trayData;

    private void Start()
    {
        _trayData = SaveManager.Instance.TrayData;
        if (_buyPanel != null)
        {
            _buyPanel.SetActive(false);
        }
        if (_buyPanel != null)
        {
            _buyPanel.SetActive(false);
        }
        _closePanelButton.onClick.RemoveAllListeners();
        _closePanelButton.onClick.AddListener(CloseUpgradePanel);
        _buyButton.onClick.RemoveAllListeners();
        _buyButton.onClick.AddListener(BuyHouse);
    }

    private void BuyHouse()
    {
        Debug.Log("Buy " + _nameText);
        CreationManager.Instance.BuyTray(_nameText);
        gameObject.SetActive(false);
        foreach (var tray in _trayData.TrayData)
        {
            if (tray.TrayData.TrayName == _nameText)
            {
                tray.TrayData.IsActive = true;
                break;
            }
        }
        foreach (var tray in _trays)
        {
            if (tray.name == _nameText)
            {
                tray.SetActive(true);
                break;
            }
        }
        SaveManager.Instance.SaveTrayData(_trayData);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.BuyHomeMusic);
        CloseUpgradePanel();
    }

    private string selectedTray;

    void OnMouseDown() 
    {
        if (_buyPanel != null && !IsPointerOverUIObject())
        {
            bool isActive = _buyPanel.activeSelf;
            _productText.text = "Buy " + gameObject.name + " ?";
            _costText.text = CreationManager.Instance.GetTrayCost(gameObject.name).ToString();
            selectedTray = gameObject.name;
            if (CreationManager.Instance.GetTrayCost(gameObject.name) > PlayerProgressionSystem.Instance.GetPlayerMoney())
            {
                _buyButton.interactable = false;
            }
            else
            {
                _buyButton.interactable = true;
            }
            _buyPanel.SetActive(!isActive);
        }
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    void CloseUpgradePanel()
    {
        if (_buyPanel != null)
        {
            _buyPanel.SetActive(false);
        }
    }
}
