using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _closeSettingsButton;
    [SerializeField] private GameObject _settingsPanel;
    [Header("Inventory")]
    [SerializeField] private Button _inventoryButton;
    [SerializeField] private Button _closeInventoryButton;
    [SerializeField] private GameObject _inventoryPanel;
    [Header("Synthesis")]
    [SerializeField] private Button _synthesisButton;
    [SerializeField] private Button _closeSynthesisButton;
    [SerializeField] private GameObject _synthesisPanel;

    [Header("Upgrade Tray")]
    [SerializeField] private GameObject _upgradeTray;
    [SerializeField] private Button _closeUpgradeButton;

    [Header("Buy")]
    [SerializeField] private GameObject _buyPanel;
    [SerializeField] private Button _buyButtonClose;

    [Header("GetNewLevel")]
    [SerializeField] private GameObject _getNewLevelPanel;
    [SerializeField] private Button _getNewLevelButtonClose;

    [Header("Experience")]

    [SerializeField] private GameObject _infoPanel;
    [SerializeField] private Button _openInfoButton;
    [SerializeField] private Button _closeInfoButton;

    void Start()
    {
        _infoPanel.SetActive(false);
        _settingsPanel.SetActive(false);
        _buyPanel.SetActive(false);
        _inventoryPanel.SetActive(false);
        _synthesisPanel.SetActive(false);
        _getNewLevelPanel.SetActive(false);
        _upgradeTray.SetActive(false);
        _settingsButton.onClick.RemoveAllListeners();
        _settingsButton.onClick.AddListener(() => Open(_settingsPanel));
        _buyButtonClose.onClick.RemoveAllListeners();
        _buyButtonClose.onClick.AddListener(() => Close(_settingsPanel));
        _closeSettingsButton.onClick.RemoveAllListeners();
        _closeSettingsButton.onClick.AddListener(() => Close(_settingsPanel));
        _inventoryButton.onClick.RemoveAllListeners();
        _inventoryButton.onClick.AddListener(() => Open(_inventoryPanel));
        _closeInventoryButton.onClick.RemoveAllListeners();
        _closeInventoryButton.onClick.AddListener(() => Close(_inventoryPanel));
        _synthesisButton.onClick.RemoveAllListeners();
        _synthesisButton.onClick.AddListener(() => Open(_synthesisPanel));
        _closeSynthesisButton.onClick.RemoveAllListeners();
        _closeSynthesisButton.onClick.AddListener(() => Close(_synthesisPanel));
        _getNewLevelButtonClose.onClick.RemoveAllListeners();
        _getNewLevelButtonClose.onClick.AddListener(() => Close(_getNewLevelPanel));
        _openInfoButton.onClick.RemoveAllListeners();
        _openInfoButton.onClick.AddListener(() => Open(_infoPanel));
        _closeInfoButton.onClick.RemoveAllListeners();
        _closeInfoButton.onClick.AddListener(() => Close(_infoPanel));
        _closeUpgradeButton.onClick.RemoveAllListeners();
        _closeUpgradeButton.onClick.AddListener(() => Close(_upgradeTray));
    }

    private void Close(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

    private void Open(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }
}
