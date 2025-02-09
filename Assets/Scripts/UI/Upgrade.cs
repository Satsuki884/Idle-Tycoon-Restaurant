using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    [SerializeField] private UpgradeTray _upgradeTray;
    [SerializeField] private GameObject _windowCanvas;
    [SerializeField] private TMP_Text _upgradeText;
    [SerializeField] private Button _closeUpgradeButton;

    private void Start()
    {
        if (_windowCanvas != null)
        {
            _windowCanvas.SetActive(false);
        }
        _closeUpgradeButton.onClick.RemoveAllListeners();
        _closeUpgradeButton.onClick.AddListener(CloseUpgradePanel);
    }

    void OnMouseDown() 
    {
        if (_windowCanvas != null)
        {
            bool isActive = _windowCanvas.activeSelf;
            _upgradeText.text = "Selected: " + gameObject.name;
            _windowCanvas.SetActive(!isActive);
            _upgradeTray.UpdatePanel(gameObject.name);
        }
    }

    void CloseUpgradePanel()
    {
        if (_windowCanvas != null)
        {
            _windowCanvas.SetActive(false);
        }
    }
}
