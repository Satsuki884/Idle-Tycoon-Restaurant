using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerProgressionSystem : MonoBehaviour
{
    [SerializeField] private NeededExpForNextLevelSO _neededExpForNextLevelSO;
    private PlayerData _playerData;
    [SerializeField] private Transform _LevelUpPanel;
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private TMP_Text _currentLevel;
    [SerializeField] private Slider _expBar;
    [SerializeField] private TMP_Text _moneyText;
    [SerializeField] private TMP_Text _moneyTextUpgrade;
    [Header("Info Panel")]
    [SerializeField] private TMP_Text _currentLevelText;
    [SerializeField] private TMP_Text _expirienceText;
    [SerializeField] private TMP_Text _nextLevelProductText;
    [SerializeField] private Image _productImage;
    private bool _isMaxLevel = false;
    TrayDataSO _trayData;

    public static PlayerProgressionSystem Instance { get; private set; }

    public async Task Initialize(params object[] param)
    {
        if (Instance != null)
        {
            return;
        }
        Instance = this;
        await Task.Delay(100);
    }

    void Start()
    {
        _playerData = SaveManager.Instance.PlayerData;
        _trayData = SaveManager.Instance.TrayData;
        // InventorySystem.Instance.RefreshInventory();
        _expBar.minValue = 0;
        SetUI();

        UpdatedPlayerMoney();

    }

    public void BuySmth(int money)
    {
        _playerData.PlayerCoins -= money;
        SaveManager.Instance.SavePlayerData(_playerData);
        UpdatedPlayerMoney();
    }

    public int GetPlayerMoney()
    {
        return _playerData.PlayerCoins;
    }

    public int GetPlayerLevel()
    {
        return _playerData.PlayerLevel;
    }

    public void BuyProduct(int exp, int money, ProductType productType)
    {
        // _playerData = SaveManager.Instance.PlayerData;
        _playerData.PlayerExperience = _playerData.PlayerExperience + exp;
        _playerData.PlayerCoins = _playerData.PlayerCoins + money;
        CheckNewLevel();
        GetMoneyAndProduct(money, productType);
        // SaveManager.Instance.SavePlayerData(_playerData);

    }

    public void CheckNewLevel()
    {

        if (_isMaxLevel)
        {
            return;
        }

        while (!_isMaxLevel && _playerData.PlayerExperience >= GetExpForNextLevel(_playerData.PlayerLevel + 1))
        {
            int nextLevelExp = GetExpForNextLevel(_playerData.PlayerLevel + 1);
            _playerData.PlayerExperience -= nextLevelExp;

            if (_playerData.PlayerLevel == _playerData.MaxLevel - 1)
            {
                _playerData.PlayerExperience = nextLevelExp;
                _isMaxLevel = true;
            }
            else
            {
                _playerData.PlayerLevel++;
                UpdateUI();

            }

            SetUI();
            // SaveManager.Instance.SavePlayerData(_playerData);
            CreationManager.Instance.UpdateView();
        }
        SetUI();

        _expBar.value = _playerData.PlayerExperience;
    }

    private void UpdateUI()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.LevelUpMusic);
        _LevelUpPanel.gameObject.SetActive(true);
        SetUI();
    }

    private void SetUI()
    {
        _levelText.text = _playerData.PlayerLevel.ToString();
        _currentLevel.text = _playerData.PlayerLevel.ToString();
        _expBar.maxValue = GetExpForNextLevel(_playerData.PlayerLevel + 1);
        _currentLevelText.text = _playerData.PlayerLevel.ToString();
        if (_isMaxLevel)
        {
            _expirienceText.text = "Max Level";
            _nextLevelProductText.text = "All products are unlocked";
            _productImage.gameObject.SetActive(false);
            _productImage.sprite = GetProductSprite(_playerData.PlayerLevel);
            return;
        }
        else
        {
            _expirienceText.text = _playerData.PlayerExperience.ToString() + " / " + GetExpForNextLevel(_playerData.PlayerLevel + 1);
            _nextLevelProductText.text = GetNextLevelsProduct(_playerData.PlayerLevel + 1);
            _productImage.gameObject.SetActive(true);
            _productImage.sprite = GetProductSprite(_playerData.PlayerLevel + 1);
        }
        _expBar.value = _playerData.PlayerExperience;
    }

    private string GetNextLevelsProduct(int nextLevel)
    {
        var nextLevelProduct = _trayData.TrayData.First(tray => tray.TrayData.LevelForUnlock == nextLevel);
        return nextLevelProduct.TrayData.TrayName;
    }

    private Sprite GetProductSprite(int nextLevel)
    {
        var nextLevelProduct = _trayData.TrayData.First(tray => tray.TrayData.LevelForUnlock == nextLevel);
        return nextLevelProduct.TrayData.ProductImage;
    }

    private int GetExpForNextLevel(int level)
    {
        return _neededExpForNextLevelSO.NeededExpForNextLevel.Find(x => x.Level == level).Exp;
    }

    public void UpdatedPlayerMoney()
    {
        int coins = _playerData.PlayerCoins;
        _moneyText.text = FormatNumber(coins);
        _moneyTextUpgrade.text = FormatNumber(coins);
    }
    private string FormatNumber(int num)
    {
        if (num >= 1_000_000)
            return (num / 1_000_000f).ToString("0.#") + "m";
        if (num >= 1_000)
            return (num / 1_000f).ToString("0.#") + "k";

        return num.ToString();
    }

    private void GetMoneyAndProduct(int money, ProductType productType)
    {
        _trayData = SaveManager.Instance.TrayData;
        _playerData.PlayerCoins += money;
        _trayData.TrayData.Find(tray => tray.TrayData.ProductType == productType).TrayData.ItemCount++;
        SaveManager.Instance.SaveTrayData(_trayData);
        SaveManager.Instance.SavePlayerData(_playerData);
        UpdatedPlayerMoney();
        InventorySystem.Instance.RefreshInventory();
    }
}